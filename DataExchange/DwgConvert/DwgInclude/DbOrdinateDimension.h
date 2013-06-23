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



#ifndef _OD_DB_ORDINATE_DIMENSION_
#define _OD_DB_ORDINATE_DIMENSION_

#include "DD_PackPush.h"

#include "DbDimension.h"

/** Description:
    This class represents Ordinate Dimension entities in an OdDbDatabase instance.

    Remarks:
    An Ordinate Dimension entity dimensions the horizontal or vertical distance between 
    between the specified origin and the specified definingPoint.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbOrdinateDimension : public OdDbDimension
{
public:
  ODDB_DECLARE_MEMBERS(OdDbOrdinateDimension);

  OdDbOrdinateDimension();
  
  /* OdDbOrdinateDimension(
       bool useXAxis,
       const OdGePoint3d& definingPoint,
       const OdGePoint3d& leaderEndPoint,
       const char* dimText = NULL,
       OdDbObjectId dimStyle = OdDbObjectId::kNull);
  */

  /** Description:
    Returns true if and only if this Dimension entity measures the OCS X-axis distance
    between the origin and the definingPoint. 
    (DXF 70, bit 0x40 set).
  */
  bool isUsingXAxis() const;

  /** Description:
    Returns true if and only if this Dimension entity measures the OCS Y-axis distance
    between the origin and the definingPoint. 
    (DXF 70, bit 0x40 cleared).
  */
  bool isUsingYAxis() const;

  /** Description:
    Sets this Dimension entity to measure the OCS X-axis distance
    between the origin and the definingPoint. 
    (DXF 70, bit 0x40 set).
  */
  void useXAxis();

  /** Description:
    Sets this Dimension entity to measure the OCS Y-axis distance
    between the origin and the definingPoint. 
    (DXF 70, bit 0x40 cleared).
  */
  void useYAxis();
  
  /** Description:
    Returns the WCS origin and primary definition point of this Dimension entity (DXF 10).
    
    Remarks:
    This Dimension entity measures the OCS X-axis or OCS Y-axis distance from the origin to the definingPoint.    
  */
  OdGePoint3d origin() const;

  /** Description:
    Sets the WCS origin and primary definition point of this Dimension entity (DXF 10).

    Arguments:
    origin (I) Origin.
  
    Remarks:
    This Dimension entity measures the OCS X-axis or OCS Y-axis distance from the origin to the definingPoint.    
  */
  void setOrigin(
    const OdGePoint3d& origin);
  
  /** Description:
    Returns the WCS defining point for this Dimension entity (DXF 13).
    Remarks:
    This Dimension entity measures the OCS X-axis or OCS Y-axis distance from the origin to the definingPoint.    
  */
  OdGePoint3d definingPoint() const;

  /** Description:
    Sets the WCS defining point for this Dimension entity (DXF 13).
    
    Arguments:
    definingPoint (I) Defining point.
    
    Remarks:
    This Dimension entity measures the OCS X-axis or OCS Y-axis distance from the origin to the definingPoint.    
  */
  void setDefiningPoint(
    const OdGePoint3d& definingPoint);
  
  /** Description:
    Returns the WCS leader end point for this Dimension entity (DXF 14).
  */
  OdGePoint3d leaderEndPoint() const;

  /** Description:
    Sets the WCS leader end point for this Dimension entity (DXF 14).
   
    Arguments:
    leaderEndPoint (I) Leader end point. 
  */
  void setLeaderEndPoint(
    const OdGePoint3d& leaderEndPoint);

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
  
  virtual void getClassID(
    void** pClsid) const;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbOrdinateDimension object pointers.
*/
typedef OdSmartPtr<OdDbOrdinateDimension> OdDbOrdinateDimensionPtr;

#include "DD_PackPop.h"

#endif

