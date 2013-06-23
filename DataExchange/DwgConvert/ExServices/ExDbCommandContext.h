#ifndef _ExDbCommandContext_h_Included_
#define _ExDbCommandContext_h_Included_

#include "DbUserIO.h"
#include "DbSSet.h"
#include "DbCommandContext.h"

class OdEdBaseIO : public OdRxObject
{
public:
  virtual OdString getString(bool bAllowSpaces) = 0;
  virtual void putString(const OdChar* string) = 0;
};

struct OdEdPointTracker
{
  virtual void setValue(const OdGePoint3d&) = 0;
  virtual OdGiDrawable* drawable() = 0;
  virtual OdString formatPrompt(const OdString& prompt, const OdGePoint3d& ) = 0;
  virtual int findKeyword(const OdString& input, OdString* pRes) = 0;
};

struct OdEdViewer
{
  // returns true if point is set
  virtual bool trackUserInput(
    const OdChar* prompt, OdEdPointTracker* tracker, OdString& s ,OdGePoint3d& point) = 0;
  virtual void select(const OdChar* prompt, OdDbSelectionSet& sset, bool bClearSelection) = 0;
};


class ExDbCommandContext : public OdDbCommandContext, protected OdDbUserIO
{
  ODRX_USING_HEAP_OPERATORS(OdDbCommandContext);
  OdEdUserIO* userIO() { return this; }
  OdRxObjectPtr getObject(const OdChar* consolePrompt, bool bAllowNull, bool bReadOnly = true, OdEdUserIOTracker<OdRxObjectPtr>* = 0);

  OdSmartPtr<OdEdBaseIO>      m_pIoStream;
  OdDbDatabase*               m_pDb;
  OdDbSelectionSetPtr         m_pSSet;
  OdEdViewer*                 m_pViewer;
  OdGePoint3d                 m_pBasePoint;
  bool                        m_bMacroIOPresent;

  class KWIndexData
  {
  public:
    KWIndexData()
      : m_pKey(0)
      , m_nKeyLen(0)
      , m_pKey2(0)
      , m_nKeyLen2(0)
      , m_pKword(0)
      , m_nKwordLen(0)
    {}
    const OdChar* m_pKey;
    int           m_nKeyLen;
    const OdChar* m_pKey2;
    int           m_nKeyLen2;
    const OdChar* m_pKword;
    int           m_nKwordLen;
    OdString key1(int n) const
    {
      return OdString(m_pKey, odmin(n,m_nKeyLen));
    }
    OdString key2(int n) const
    {
      return OdString(m_pKey2, odmin(n,m_nKeyLen2));
    }
    bool match(const OdString& str) const
    {
      return key1(odmin(str.getLength(), m_nKeyLen)).iCompare(str)==0 || key2(odmin(str.getLength(), m_nKeyLen2)).iCompare(str)==0;
    }
    OdString keyword() const
    {
      return m_nKwordLen ? OdString(m_pKword, m_nKwordLen) : OdString(m_pKey, m_nKeyLen);
    }
  };
  OdArray<KWIndexData, OdMemoryAllocator<KWIndexData> > m_KwIndex;
  
  OdString getStringInternal(bool bAllowSpaces, const OdChar* prompt);
public:
  int findKeyword(const OdString& input, OdString* pRes);
protected:
  

  // Leave this here--DO NOT move it out of the class definition.  Moving it 
  // outside causes an "internal compiler error" in CodeWarrior 8 & 9 on the mac.
  ExDbCommandContext()
    : m_pDb(0)
    , m_pViewer(0)
    , m_bMacroIOPresent(false)
  {
  }

public:
  static OdDbCommandContextPtr createObject(OdEdBaseIO* pIOStream, OdDbDatabase* pDb);
  OdDbDatabase* database();
  OdDbSelectionSet& selectionSet();
  void select(const OdChar* prompt, bool clearSelection = false);
  void setViewer(OdEdViewer* v){ m_pViewer = v; }
  void setMacroIOPresent( bool bPresent ){ m_bMacroIOPresent = bPresent; }

  // OdEdUserIO interface:
  void        putString(const OdChar* string);
  int         getInt(const OdChar* prompt,OdEdUserIOTracker<int>* = 0);
  int         getInt(const OdChar* prompt, int defVal,OdEdUserIOTracker<int>* = 0);
  double      getReal(const OdChar* prompt,OdEdUserIOTracker<double>* = 0);
  double      getReal(const OdChar* prompt, double defVal,OdEdUserIOTracker<double>* = 0);
  OdString    getString(bool bAllowSpaces);
  OdString    getString(bool bAllowSpaces, const OdChar* prompt, const OdChar* defVal = 0, bool bThrowKeyword = false, OdEdUserIOTracker<OdString>* = 0);
  void        setKeywords(const OdChar* pKwList);
  int         getKeyword(const OdChar* prompt, const OdChar* defVal = 0, OdString* pKeyword = 0);
  double      getOrient(const OdChar* prompt,
                        const OdGePoint3d* pBasePt = 0,
                        OdEdUserIOTracker<double>* = 0);
  double      getOrient(const OdChar* prompt,
                        double defVal,
                        const OdGePoint3d* pBasePt = 0,
                        OdEdUserIOTracker<double>* = 0);
  
  double      getAngle(const OdChar* prompt,
                       const OdGePoint3d* pBasePt = 0,
                       OdEdUserIOTracker<double>* = 0);
  double      getAngle(const OdChar* prompt, double defVal,
                       const OdGePoint3d* pBasePt = 0,
                       OdEdUserIOTracker<double>* = 0);
  
  OdGePoint3d getPoint(const OdChar* prompt,
                       const OdGePoint3d* pBasePt = 0, const OdGePoint3d* pDefVal = 0,OdEdUserIOTracker<OdGePoint3d>* = 0);
  OdGePoint3d getPoint(const OdChar* prompt, bool bNoLimCheck,
                       const OdGePoint3d* pBasePt = 0, const OdGePoint3d* pDefault = 0,OdEdUserIOTracker<OdGePoint3d>* = 0);
  
  OdGePoint3d getCorner(const OdChar* prompt, bool bNoLimCheck = false,
                        const OdGePoint3d* pBasePt = 0, const OdGePoint3d* pDefVal = 0,OdEdUserIOTracker<OdGePoint3d>* = 0);
  
  double      getDist(const OdChar* prompt, const OdGePoint3d* pBasePt = 0, OdEdUserIOTracker<double>* = 0);
  double      getDist(const OdChar* prompt, double defVal, const OdGePoint3d* pBasePt = 0, OdEdUserIOTracker<double>* = 0);
  OdString    getFilePath(int nFlags,
                          const OdChar* consolePrompt, 
                          const OdChar* dialogCaption, 
                          const OdChar* defExt,
                          const OdChar* fileName,
                          const OdChar* filter);
};

#endif // _ExDbCommandContext_h_Included_
