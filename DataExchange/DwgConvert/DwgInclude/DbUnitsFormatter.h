#ifndef _ODDBUNITSFORMATTER_H_INCLUDED_
#define _ODDBUNITSFORMATTER_H_INCLUDED_

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


#include "OdPlatform.h"
#include "DbExport.h"
#include "RxObject.h"
#include "OdString.h"


class OdGePoint3d;
class OdDbObjectId;
class OdCmColor;


/** Description:

    {group:OdEd_Classes}
*/
class FIRSTDLL_EXPORT OdDbUnitsFormatter : public OdRxObject
{ 
public:
  ODRX_DECLARE_MEMBERS(OdDbUnitsFormatter);

  virtual OdString      formatReal  (double val) = 0;
  virtual double      unformatReal  (const OdString& str) = 0;

  virtual OdString    formatPoint   (const OdGePoint3d& val) = 0;
  virtual OdGePoint3d unformatPoint (const OdString& str) = 0;

  virtual OdString    formatAngle   (double val) = 0;
  virtual double      unformatAngle (const OdString& str) = 0;

  virtual OdString    formatOrient  (double val) = 0;
  virtual double      unformatOrient(const OdString& str) = 0;

  virtual OdString    formatCorner  (const OdGePoint3d& val) = 0;
  virtual OdGePoint3d unformatCorner(const OdString& str) = 0;

  virtual OdString    formatDist    (double val) = 0;
  virtual double      unformatDist  (const OdString& str) = 0;

  virtual OdString    formatColor   (const OdCmColor& val) = 0;
  virtual OdString    formatColor   (const OdDbObjectId& dbColorId) = 0;
  virtual OdCmColor   unformatColor (const OdString& str, OdDbObjectId* pDbColor = 0) = 0;
};

typedef OdSmartPtr<OdDbUnitsFormatter> OdDbUnitsFormatterPtr;


#define ODDB_UNITS_FORMATER "OdDbUnitsFormatter"


#endif //#ifndef _ODDBUNITSFORMATTER_H_INCLUDED_

