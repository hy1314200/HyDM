#include "OdaCommon.h"
#include "ExPageController.h"
#include "DbSystemServices.h"
#include "DbDatabase.h"
#include "OdString.h"
#include "FlatMemStream.h"

//----------------------------------------------------------
//
// ExUnloadController
//
//----------------------------------------------------------
ExUnloadController::ExUnloadController()
  : m_pDb(0)
{
}

OdStreamBufPtr ExUnloadController::read(Key)
{
  ODA_FAIL(); return (OdStreamBuf*)0;
}

bool ExUnloadController::write(Key&, OdStreamBuf*)
{
  ODA_FAIL(); return false;
}

void ExUnloadController::setDatabase(OdDbDatabase* pDb) 
{
  m_pDb = pDb;
}

OdDbDatabase* ExUnloadController::database() 
{
  return m_pDb; 
}

int ExUnloadController::pagingType() const
{
  return OdDb::kUnload;
}

//----------------------------------------------------------
//
// ExPageController
//
//----------------------------------------------------------
ExPageController::ExPageController()
  : m_fp(0)
{
}

ExPageController::~ExPageController()
{
  if (m_fp)
  {
    fclose(m_fp);
  }
}

OdStreamBufPtr ExPageController::read(Key key)
{
  OdUInt32 nCountRes = 0;
  if (!m_fp)
    return (OdStreamBuf*)0;

  if (fseek(m_fp, key, SEEK_SET) != 0)
    return (OdStreamBuf*)0;

  OdUInt32 len;
  if (fread(&len, 1, sizeof(OdUInt32), m_fp) != sizeof(OdUInt32))
    return (OdStreamBuf*)0;

  m_buff.resize(len);
  if (fread(m_buff.asArrayPtr(), 1, len, m_fp) != len)
    return (OdStreamBuf*)0;

  return OdFlatMemStream::createNew(m_buff.asArrayPtr(), len);
}

bool ExPageController::write(Key& key, OdStreamBuf* pStreamSrc)
{
  if (!m_fp)
    return false;

  if (fseek(m_fp, 0, SEEK_END) != 0)
    return false;

  key = ftell(m_fp);

  OdUInt32 len = pStreamSrc->length();
  if (fwrite(&len, 1, sizeof(OdUInt32), m_fp) != sizeof(OdUInt32))
    return false;

  m_buff.resize(len);
  pStreamSrc->getBytes(m_buff.asArrayPtr(), len);
  if (fwrite(m_buff.asArrayPtr(), 1, len, m_fp) != len)
    return false;

  return true;
}

void ExPageController::setDatabase(OdDbDatabase* pDb)
{
  ExUnloadController::setDatabase(pDb);

#if !defined(_WIN32_WCE)
  m_fp = tmpfile();
#else
  OdString fname;
  fname.format("page%d.tmp", (int)database());
  m_fp = fopen(fname.c_str(), "w+b");
#endif
}

int ExPageController::pagingType() const
{
  return OdDb::kPage | OdDb::kUnload;
}


