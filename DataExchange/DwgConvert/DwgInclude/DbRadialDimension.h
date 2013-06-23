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



#ifndef _OD_DB_RADIAL_DIMENSION_
#define _OD_DB_RADIAL_DIMENSION_

#include "DD_PackPush.h"

#include "DbDimension.h"

/** Description:
    This class represents Radial Dimension entities in an OdDbDatabase instance.

    Library:
    Db

    Remarks:
    Radial Dimension entities require a point the curve being dimensioned and
    a center point. In additional, leaderLength specifies
     how far the dimension line extends beyond the curve before
    the dogleg to the annotation text. 
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRadialDimension : public OdDbDimension
{
public:
  ODDB_DECLARE_MEMBERS(OdDbRadialDimension);

  OdDbRadialDimension();
  
  /* OdDbRadialDimension(  
       const OdGePoint3d& center,
       const OdGePoint3d& chordPoint,
       double leaderLength,
       const char* dimText = NULL,
       OdDbObjectId dimStyle = OdDbObjectId::kNull);
  */

  /** Description:
    Sets the length from the chordPoint to the dogleg for this Dimension entity
    (DXF 40).
    
    Arguments:
    leaderLength (I) Leader length.
  */
  void setLeaderLength(
    double leaderLength);

  /** Description:
    Returns the length from the chordPoint to the dogleg for this Dimension entity
    (DXF 40).
  */
  double leaderLength() const; 
  
  /** Description:
    Returns the WCS *center* of this Dimension entity (DXF 10).
  */
  OdGePoint3d center() const;

  /** Description:
    Sets the WCS *center* of this Dimension entity (DXF 10).
    Arguments:
    center (I) Center.
  */
  void setCenter(
    const OdGePoint3d& center);
  
  /** Description:
    Returns the WCS chord point for this Dimension entity (DXF 15).
    
    Remarks:
    The chord point is the point at which the dimension line for this Dimension entity
    intersects the curve being dimensioned.
  */
  OdGePoint3d chordPoint() const;

  /** Description:
    Sets the WCS chord point for this Dimension entity (DXF 15).
    
    Arguments:
    chordPoint (I) Chord point.
    
    Remarks:
    The chord point is the point at which the dimension line for this Dimension entity
    intersects the curve being dimensioned
  */
  void setChordPoint(
    const OdGePoint3d& chordPoint);

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
  This template class is a specialization of the OdSmartPtr class for OdDbRadialDimension object pointers.
*/
typedef OdSmartPtr<OdDbRadialDimension> OdDbRadialDimensionPtr;

#include "DD_PackPop.h"

#endif

