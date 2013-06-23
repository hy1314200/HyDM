#ifndef _IDARRAY_H_
#define _IDARRAY_H_

#include "OdArray.h"
#include "DbTypedId.h"

typedef OdArray<OdDbSoftPointerId,    OdMemoryAllocator<OdDbSoftPointerId> >    OdSoftPtrIdArray;
typedef OdArray<OdDbHardPointerId,    OdMemoryAllocator<OdDbHardPointerId> >    OdHardPtrIdArray;
typedef OdArray<OdDbSoftOwnershipId,  OdMemoryAllocator<OdDbSoftOwnershipId> >  OdSoftOwnIdArray;
typedef OdArray<OdDbHardOwnershipId,  OdMemoryAllocator<OdDbHardOwnershipId> >  OdHardOwnIdArray;
typedef OdArray<OdDbObjectId,         OdMemoryAllocator<OdDbObjectId> >         OdDbObjectIdArray;

typedef OdArray<OdDbTypedId,          OdMemoryAllocator<OdDbTypedId> >          OdTypedIdsArray;


#endif //_IDARRAY_H_

