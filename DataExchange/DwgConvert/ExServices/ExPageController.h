#ifndef _EX_DBPAGECONTROLLER_H_
#define _EX_DBPAGECONTROLLER_H_

#include "DbPageController.h"
#include "OdBinaryData.h"

//
// The class supports unloading of objects in partially opened database.
//
class ExUnloadController : public OdDbPageController
{
public:
  ExUnloadController();

  int pagingType() const;
  OdStreamBufPtr read(Key key);
  bool write(Key& key, OdStreamBuf* pStreamSrc);
  void setDatabase(OdDbDatabase* pDb);
  OdDbDatabase* database();

private:
  OdDbDatabase* m_pDb;
};

//
// The class demonstrates a joint using of unloading and paging to external storage.
// Contains very simple implementation of paging with consecutive writing in a file.
//
class ExPageController : public ExUnloadController
{
public:
  ExPageController();
  ~ExPageController();

  int pagingType() const;
  OdStreamBufPtr read(Key key);
  bool write(Key& key, OdStreamBuf* pStreamSrc);
  void setDatabase(OdDbDatabase* pDb);

private:
  FILE*         m_fp;
  OdBinaryData  m_buff;
};

#endif // _EX_DBPAGECONTROLLER_H_

