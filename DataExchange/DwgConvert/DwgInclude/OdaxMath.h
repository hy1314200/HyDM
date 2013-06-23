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
// programs incorporating this software must include the following statement 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



/******************************************************************************
 * FILE NAME  ODAXMATH.H
 * PURPOSE    
 *
 * SPEC       6/21/2002 Serge Kuligin
 * NOTES      
 *******************************************************************************/
#ifndef __ODAX_MATH_H_
#define __ODAX_MATH_H_

#include "Ge/GePoint3d.h"
#include "IntArray.h"
#include "Ge/GePoint2dArray.h"

#ifndef  DWGDIRECTXEXP
#ifdef ODAXAUTO_DLL
	#define DWGDIRECTXEXP __declspec(dllexport)
#else
	#define DWGDIRECTXEXP __declspec(dllimport)
#endif
#endif


/**********************************************************************************************
 **********           data conversion                                                        */

inline BSTR OdOxToBSTR(const char* str)
{
  return _com_util::ConvertStringToBSTR(str);
}

inline OdString OdOxToOdString(BSTR str)
{
  return OdString((const char*)_bstr_t(str));
}

DWGDIRECTXEXP OdGeVector3d OdOxToVector(const VARIANT& pVector);
DWGDIRECTXEXP OdGePoint3d  OdOxToPoint(const VARIANT& pPoint);
DWGDIRECTXEXP OdGeVector2d OdOxToVector2(const VARIANT& pVector);
DWGDIRECTXEXP OdGePoint2d  OdOxToPoint2(const VARIANT& pPoint);

DWGDIRECTXEXP void OdOxToVariant(VARIANT* pPoint, const OdGePoint3d& value);
DWGDIRECTXEXP void OdOxToVariant(VARIANT* pPoint, const OdGeVector3d& value);
DWGDIRECTXEXP void OdOxToVariant(VARIANT* pPoint, const OdGePoint2d& value);
DWGDIRECTXEXP void OdOxToVariant(VARIANT* pPoint, const OdGeVector2d& value);

DWGDIRECTXEXP void OdOxIntArrayToVariant(VARIANT* arrVar, OdArray<OdUInt32>&  src);
DWGDIRECTXEXP void OdOxMatrixToVariant(OdGeMatrix3d &mTrans, VARIANT *transMatrix);

DWGDIRECTXEXP void OdOxToArray(const VARIANT& array, OdIntArray     &odarray);
DWGDIRECTXEXP void OdOxToArray(const VARIANT& array, OdGePoint3dArray &odarray);
DWGDIRECTXEXP void OdOxToArray(const VARIANT& array, OdGePoint2dArray &odarray);

DWGDIRECTXEXP HRESULT OdOxSetResBuf(OdResBufPtr& pResBuf, VARIANT& type, VARIANT& data);
DWGDIRECTXEXP HRESULT OdOxGetResBuf(OdResBuf* pResBuf, VARIANT* type, VARIANT* data);

DWGDIRECTXEXP HRESULT RaiseOdaException(HRESULT hRes, unsigned int es, void *pService);


#endif __ODAX_MATH_H_
/*---------------- end of odaxmath.h -----------------------------------------*/


