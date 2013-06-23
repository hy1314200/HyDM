#include "OdaCommon.h"
#include "ExUndoController.h"

const ExUndoController::SIZE nExPageSize = 0x0001000;

ExUndoController::SIZE ExUndoController::pageSize() const
{
  return nExPageSize;
}

ExUndoController::PAGE ExUndoController::allocPage()
{
  return new OdUInt8[nExPageSize];
}

void ExUndoController::freePage(PAGE page)
{
  delete [] ((OdUInt8*)page);
}

void ExUndoController::read(PAGE page, void* pDest, POS nStart, SIZE nCount) const
{
  ODA_ASSERT(nStart + nCount <= nExPageSize);
  ::memcpy(pDest, ((OdUInt8*)page)+nStart, nCount);
}

void ExUndoController::write(PAGE page, const void* pSorce, POS nStart, SIZE nCount) const
{
  ODA_ASSERT(nStart + nCount <= nExPageSize);
  ::memcpy(((OdUInt8*)page)+nStart, pSorce, nCount);
}

void ExUndoController::setDatabase(OdDbDatabase* )
{
}

int ExUndoController::maxUndoSteps() const
{
  return 10;
}

