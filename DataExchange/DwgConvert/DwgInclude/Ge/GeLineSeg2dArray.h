///////////////////////////////////////////////////////////////////////////////
// Copyright © 2002, Open Design Alliance Inc. ("Open Design") 
// 
// This software is owned by Open Design, and may only be incorporated into 
// application programs owned by members of Open Design subject to a signed 
// Membership Agreement and Supplemental Software License Agreement with 
// Open Design. The structure and organization of this Software are the valuable 
// trade secrets of Open Design and its suppliers. The Software is also protected 
// by copyright law and international treaty provisions. You agree not to 
// modify, adapt, translate, reverse engineer, decompile, disassemble or 
// otherwise attempt to discover the source code of the Software. Application 
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef GELINESEG2DARRAY_INCLUDED
#define GELINESEG2DARRAY_INCLUDED /* {Secret} */

#include "OdArray.h"
#include "GeLineSeg2d.h"

/**
    Description:
    This template class is a specialization of the OdArray class template for 2D line segments.

*/
typedef OdArray<OdGeLineSeg2d> OdGeLineSeg2dArray;

// typedef OdArray<OdGeLineSeg2d/*, OdMemoryAllocator<OdGeLineSeg2d>*/ > OdGeLineSeg2dArray;

#endif // GELINESEG2DARRAY_INCLUDED


