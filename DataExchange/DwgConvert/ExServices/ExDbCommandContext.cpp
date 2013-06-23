#include "OdaCommon.h"
#include "DbUserIO.h"
#include "DbDatabase.h"
#include "DbHostAppServices.h"
#include "ExDbCommandContext.h"
#include "Ge/GeExtents2d.h"
#include "DbLine.h"

inline OdString next(OdString& list, const OdChar* delim = ",")
{
  OdString res = list.spanExcluding(delim);
  if(res.getLength() != list.getLength())
    list = list.mid(res.getLength()+1);
  else
    list.empty();
  return res;
}

OdRxObjectPtr ExDbCommandContext::getObject(const OdChar* consolePrompt, bool bAllowNull, bool bReadOnly , OdEdUserIOTracker<OdRxObjectPtr>* )
{
  OdRxObjectPtr pRes;

  OdString pmt;
  if(!consolePrompt || !consolePrompt[0])
  {
    consolePrompt = "Enter object handle:";
  }
  else
  {
    pmt = consolePrompt;
    pmt += " (type handle):";
    consolePrompt = pmt;
  }

  OdDbDatabase* pDb = database();
  do
  {
    try
    {
      OdDbHandle h(getString(false, consolePrompt, "0"));
      pRes = pDb->getOdDbObjectId(h).openObject(bReadOnly ? OdDb::kForRead : OdDb::kForWrite, true);
    }
    catch(const OdError& e)
    {
      putString(e.description());
    }
  }
  while(!bAllowNull && pRes.isNull());
  return pRes;
}

OdDbCommandContextPtr ExDbCommandContext::createObject(OdEdBaseIO* pIOStream, OdDbDatabase* pDb)
{
  OdDbCommandContextPtr pRes = OdRxObjectImpl<ExDbCommandContext, OdDbCommandContext>::createObject();
  ExDbCommandContext* pDbCmdCtx = static_cast<ExDbCommandContext*>(pRes.get());

  pDbCmdCtx->m_pDb = pDb;
  pDbCmdCtx->m_pIoStream = pIOStream;
  return pRes;
}

OdDbDatabase* ExDbCommandContext::database()
{
  return m_pDb;
}

OdDbSelectionSet& ExDbCommandContext::selectionSet()
{
  if(m_pSSet.isNull())
  {
    m_pSSet = OdDbSelectionSet::createObject(database());
  }
  return *m_pSSet.get();
}

void ExDbCommandContext::select(const OdChar* prompt, bool clearSelection)
{
  if ( m_bMacroIOPresent )
  {
    if (clearSelection) selectionSet().clear();
    for(;;)
    {
      OdString s = getString(true,prompt," ");
      if (s.isEmpty() || s == " ") break;
      else if ( s == "~" && m_pViewer ) m_pViewer->select( prompt, selectionSet(), false );
      else if ( s.left(9).makeLower() == "(handent ")
      {
        s.remove('\"');
        s.remove('\'');
        int handle = 0; 
        if( sscanf(s,"(handent %x)", &handle) )
        {
          OdDbObjectId id = database()->getOdDbObjectId(OdDbHandle(handle));
          if (id.isValid())
            selectionSet().append(id);
          else
          {
            putString( "Handle not found" );
          }
        }
        else
          putString( "Invalid syntax: " + s);
      }
      else 
      {
        putString( "Invalid syntax: " + s);
      }
    }
  }
  else if ( m_pViewer ) m_pViewer->select( prompt, selectionSet(), clearSelection );
}

void ExDbCommandContext::setKeywords(const OdChar* szKwList)
{
  m_KwIndex.clear();
  int i = 0;
  while(szKwList && szKwList[i])
  {
    while(szKwList[i]==' ') // trim
      ++i;
    if(!szKwList[i])
      break;
    if(szKwList[i]=='_')
    {
      ++i;
      break;
    }

    KWIndexData& data = *m_KwIndex.append();
    data.m_pKey = szKwList + i;
    int j;
    for(j = i; szKwList[j]!=' ' && szKwList[j]!=',' && szKwList[j]!='\0'; ++j)
    { }
    data.m_nKeyLen = j-i;
    i=j;
    if(szKwList[i]==',')
    {
      ++i;
      while(szKwList[i]==' ') // trim
        ++i;
      data.m_pKey2 = szKwList + i;
      int j;
      for(j = i; szKwList[j]!=' ' && szKwList[j]!=',' && szKwList[j]!='\0'; ++j)
      { }
      data.m_nKeyLen2 = j-i;
    }
    else
    {
      data.m_pKey2 = "";
      data.m_nKeyLen2 = 0;
    }
  }
  unsigned int kwi = 0;
  while(szKwList && szKwList[i] && kwi < m_KwIndex.size())
  {
    while(szKwList[i]==' ') // trim
      ++i;
    if(!szKwList[i])
      break;

    KWIndexData& data = m_KwIndex[kwi++];
    data.m_pKword = szKwList + i;
    int j;
    for(j = i; szKwList[j]!=' ' && szKwList[j]!=',' && szKwList[j]!='\0'; ++j)
    { }
    data.m_nKwordLen = j-i;
    i=j;
    if(szKwList[i]==',')
    {
      ++i;
      while(szKwList[i]==' ') // trim
        ++i;
      int j;
      for(j = i; szKwList[j]!=' ' && szKwList[j]!=',' && szKwList[j]!='\0'; ++j)
      { }
      i=j;
    }
  }
}

int ExDbCommandContext::findKeyword(const OdString& input, OdString* pRes)
{
  for(unsigned int i=0; i<m_KwIndex.size(); ++i)
  {
    KWIndexData& data = m_KwIndex[i];
    if(data.match(input))
    {
      if(pRes)
        *pRes = data.keyword();
      return i;
    }
  }
  return -1;
}

OdString ExDbCommandContext::getString(bool bAllowSpaces)
{
  return m_pIoStream->getString(bAllowSpaces);
}


double ExDbCommandContext::getAngle(const OdChar* prompt, const OdGePoint3d* pBasePt,OdEdUserIOTracker<double>* tracker)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter an angle in degrees:";
  return getOrient(pmt,pBasePt,tracker) - m_pDb->getANGBASE();
}

double ExDbCommandContext::getAngle(const OdChar* prompt, double defVal, const OdGePoint3d* pBasePt,OdEdUserIOTracker<double>* )
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter an angle in degrees:";
  return getOrient(pmt, defVal + m_pDb->getANGBASE(), pBasePt) - m_pDb->getANGBASE();
}

OdGePoint3d ExDbCommandContext::getCorner(const OdChar* prompt, bool bNoLimCheck,
                                          const OdGePoint3d* pBasePt, const OdGePoint3d* pDefVal,OdEdUserIOTracker<OdGePoint3d>* tracker)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter a point:";
  return getPoint(pmt, bNoLimCheck, pBasePt, pDefVal,tracker);
}

double ExDbCommandContext::getDist(const OdChar* prompt, const OdGePoint3d* pBasePt,OdEdUserIOTracker<double>* tracker)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter a distance:";
  if ( pBasePt ) m_pBasePoint = *pBasePt;
  return getReal(pmt,tracker);
}

double ExDbCommandContext::getDist(const OdChar* prompt, double defVal, const OdGePoint3d* pBasePt,OdEdUserIOTracker<double>* )
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter a distance:";
  if ( pBasePt ) m_pBasePoint = *pBasePt;
  return getReal(pmt, defVal);
}

double ExDbCommandContext::getOrient(const OdChar* prompt, const OdGePoint3d* pBasePt,OdEdUserIOTracker<double>* tracker)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter an orientation:";
  if ( pBasePt ) m_pBasePoint = *pBasePt;
  return getReal(pmt,tracker) / 180. * OdaPI;
}

double ExDbCommandContext::getOrient(const OdChar* prompt, double defVal, const OdGePoint3d* pBasePt,OdEdUserIOTracker<double>*)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter an orientation:";
  if ( pBasePt ) m_pBasePoint = *pBasePt;
  return getReal(pmt, defVal / OdaPI * 180.) / 180. * OdaPI;
}

static OdGePoint3d pointFromString(OdString& sInput)
{
  OdGePoint3d res;
  for(int i=0; i<3 && !sInput.isEmpty(); ++i)
  {
    int n = sInput.find(',');
    OdString sCoord;
    if(n>-1)
    {
      sCoord = sInput.left(n);
      sInput = sInput.right(sInput.getLength() - n - 1);
    }
    else
    {
      sCoord = sInput;
      sInput.empty();
    }
    res[i] = ::atof(sCoord);
  }
  return res;
}

OdGePoint3d ExDbCommandContext::getPoint(const OdChar* prompt,
                                         const OdGePoint3d* pBasePt, const OdGePoint3d* pDefault,OdEdUserIOTracker<OdGePoint3d>* tracker)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter a point:";
  
  OdString sInput;
  if ( m_pViewer && !m_bMacroIOPresent )
  {
    if ( pBasePt ) m_pBasePoint = *pBasePt;
    struct PTracker : OdEdPointTracker
    {
      ExDbCommandContext* _this;
      OdEdUserIOTracker<OdGePoint3d>* _tracker;
      OdGePoint3d _base;
      PTracker(OdEdUserIOTracker<OdGePoint3d>* tracker,const OdGePoint3d& base,ExDbCommandContext* pthis):_tracker(tracker),_base(base),_this(pthis){}
      virtual void setValue(const OdGePoint3d& p)
      {
        if (_tracker) _tracker->setValue(p);
      }
      virtual OdGiDrawable* drawable()
      {
        if (_tracker) return _tracker->drawable();
        else return 0;
      }
      virtual OdString formatPrompt(const OdString& prompt, const OdGePoint3d& p )
      {
        OdString s; s.format("%s %g,%g,%g", prompt, p.x,p.y,p.z );
        return s;
      }
      virtual int findKeyword(const OdString& input, OdString* pRes)
      {
        return _this->findKeyword(input, pRes);
      }
    }
    ptracker(tracker,m_pBasePoint,this);
    OdGePoint3d p;
    if ( m_pViewer->trackUserInput( pmt, &ptracker, sInput, p ) )
      return p;
    else if (sInput.isEmpty() && pDefault)
      return *pDefault;
    else return pointFromString(sInput);
  }

  if(pDefault)
  {
    sInput = ExDbCommandContext::getString(false, pmt, "");
    if(sInput.isEmpty())
      return *pDefault;
  }
  else
  {
    sInput = ExDbCommandContext::getString(false, pmt);
  }
  if ( sInput == "*cancelled*" ) throw OdEdUserIO::InterruptException();
  m_pBasePoint = pointFromString(sInput);
  return m_pBasePoint;
}

OdGePoint3d ExDbCommandContext::getPoint(const OdChar* prompt, bool bNoLimCheck,
                                         const OdGePoint3d* pBasePt, const OdGePoint3d* pDefault,OdEdUserIOTracker<OdGePoint3d>* tracker)
{
  OdGePoint3d res = getPoint(prompt, pBasePt, pDefault, tracker);
  if(!bNoLimCheck)
  {
    OdGeExtents2d ext(m_pDb->getLIMMIN(), m_pDb->getLIMMAX());
    while(ext.isDisjoint((const OdGeExtents2d&)res))
    {
      putString("Point exceeds limits. Enter new value");
      res = getPoint(prompt, pBasePt, pDefault);
    }
  }
  m_pBasePoint = res;
  return res;
}

int ExDbCommandContext::getInt(const OdChar* prompt,OdEdUserIOTracker<int>*)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter an integer value:";
  OdString sInput = ExDbCommandContext::getString(false, pmt);
  if ( sInput == "*cancelled*" ) throw OdEdUserIO::InterruptException();
  return ::atoi(sInput.c_str());
}

int ExDbCommandContext::getInt(const OdChar* prompt, int defVal,OdEdUserIOTracker<int>*)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter an integer value:";
  OdString sInput = ExDbCommandContext::getString(false, pmt, "");
  if(sInput.isEmpty())
    return defVal;
  if ( sInput == "*cancelled*" ) throw OdEdUserIO::InterruptException();
  return ::atoi(sInput.c_str());
}

int ExDbCommandContext::getKeyword(const OdChar* prompt, const OdChar* defVal, OdString* pKeyword)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter one of keywords:";
  int n;
  while((n = findKeyword(ExDbCommandContext::getString(false, pmt, defVal), pKeyword))==-1)
  {}
  return n;
}

double ExDbCommandContext::getReal(const OdChar* prompt,OdEdUserIOTracker<double>* tracker)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter a real value:";
  if ( m_pViewer && !m_bMacroIOPresent ) 
  {
    struct DTracker : OdEdPointTracker
    {
      ExDbCommandContext* _this;
      OdEdUserIOTracker<double>* _tracker;
      OdGePoint3d _base;
      OdDbLinePtr _line;
      DTracker(OdEdUserIOTracker<double>* tracker,const OdGePoint3d& base,ExDbCommandContext* pthis)
        :_tracker(tracker)
        ,_base(base)
        ,_this(pthis)
      {
        if ( !tracker )
        {
          _line = OdDbLine::createObject();
          _line->setStartPoint(_base);
          _line->setEndPoint(_base);
        }
      }
      virtual void setValue(const OdGePoint3d& p)
      {
        if (_tracker) _tracker->setValue(p.distanceTo(_base));
        else _line->setEndPoint(p);
      }
      virtual OdGiDrawable* drawable()
      {
        return _tracker ? _tracker->drawable() : (OdGiDrawable*)_line;
      }
      virtual OdString formatPrompt(const OdString& prompt, const OdGePoint3d& p )
      {
        OdString s; s.format("%s %g", prompt, p.distanceTo(_base) );
        return s;
      }
      virtual int findKeyword(const OdString& input, OdString* pRes)
      {
        return _this->findKeyword(input, pRes);
      }
    }
    dtracker(tracker,m_pBasePoint,this);

    OdString s;
    OdGePoint3d p;
    if ( !m_pViewer->trackUserInput( pmt, &dtracker, s, p ) )
      return ::atof( s );
    else 
      return p.distanceTo(m_pBasePoint);
  }
  OdString sInput = ExDbCommandContext::getString(false, pmt);
  if ( sInput == "*cancelled*" ) throw OdEdUserIO::InterruptException();
  return ::atof(sInput.c_str());
}

double ExDbCommandContext::getReal(const OdChar* prompt, double defVal,OdEdUserIOTracker<double>*)
{
  OdString pmt = prompt;
  if(pmt.isEmpty())
    pmt = "Enter a real value:";
  OdString sInput = ExDbCommandContext::getString(false, pmt, "");
  if(sInput.isEmpty())
    return defVal;
  if ( sInput == "*cancelled*" ) throw OdEdUserIO::InterruptException();
  return ::atof(sInput.c_str());
}

OdString ExDbCommandContext::getStringInternal(bool bAllowSpaces, const OdChar* prompt)
{
  m_pIoStream->putString(prompt);
  return m_pIoStream->getString(bAllowSpaces);
}

OdString ExDbCommandContext::getString(bool bAllowSpaces, const OdChar* prompt, const OdChar* pDefValue, bool bThrowKeyword, OdEdUserIOTracker<OdString>*)
{
  OdString res = getStringInternal(bAllowSpaces, prompt);
  if(!pDefValue)
  {
    while(res.isEmpty())
    {
      putString("Enter non-empty value");
      res = getStringInternal(bAllowSpaces, prompt);
    }
  }
  else if(res.isEmpty())
  {
    res = pDefValue;
  }
  if(bThrowKeyword)
  {
    int n = ExDbCommandContext::findKeyword(res, 0);
    if(n > -1)
      throw KeywordException(n, m_KwIndex[n].keyword());
  }
  return res;
}

OdString ExDbCommandContext::getFilePath(int nFlags,
                                         const OdChar* consolePrompt, 
                                         const OdChar* dialogCaption, 
                                         const OdChar* defExt,
                                         const OdChar* fileName,
                                         const OdChar* filter)
{
  OdString sInput;
  if(m_bMacroIOPresent || !database()->appServices()->getFILEDIA())
  {
    sInput = OdDbUserIO::getFilePath(nFlags, consolePrompt, defExt, fileName, filter);
    if(sInput != "~")
    {
      sInput.trimLeft(' ');
      sInput.trimRight(' ');
      return sInput;
    }
  }

  sInput = database()->appServices()->fileDialog(nFlags, dialogCaption, defExt, fileName, filter);
  if(sInput == "*cancelled*")
    throw OdEdUserIO::InterruptException();
  if(sInput == "*unsupported*")
    sInput = OdDbUserIO::getFilePath(nFlags, consolePrompt, defExt, fileName, filter);
  return sInput;
}

void ExDbCommandContext::putString(const OdChar* string)
{
  m_pIoStream->putString(string);
}


