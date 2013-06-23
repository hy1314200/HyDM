#ifndef _EX_DBUNDOCONTROLLER_H_
#define _EX_DBUNDOCONTROLLER_H_

#include "DbUndoController.h"

class ExUndoController : public OdDbUndoController
{
public:
  SIZE pageSize() const;

  PAGE allocPage();
  void freePage(PAGE page);
  void read(PAGE page, void* pDest, POS nStart, SIZE nCount) const;
  void write(PAGE page, const void* pSorce, POS nStart, SIZE nCount) const;

  void setDatabase(OdDbDatabase* pDb);
  int maxUndoSteps() const;
};

#endif // _EX_DBUNDOCONTROLLER_H_

