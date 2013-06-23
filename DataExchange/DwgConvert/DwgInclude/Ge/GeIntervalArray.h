
#ifndef __OD_GE_INTERVAL_ARRAY__
#define __OD_GE_INTERVAL_ARRAY__ /* {Secret} */

#include "OdArray.h"
#include "GeInterval.h"


/**
    Description:
    This template class is a specialization of the OdArray class template for OdGeInterval values.
*/
typedef OdArray<OdGeInterval, OdMemoryAllocator<OdGeInterval> > OdGeIntervalArray;

#endif // __OD_GE_INTERVAL_ARRAY__

