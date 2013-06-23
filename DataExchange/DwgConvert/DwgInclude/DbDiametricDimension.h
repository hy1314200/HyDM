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



#ifndef _OD_DB_DIAMETRIC_DIMENSION_
#define _OD_DB_DIAMETRIC_DIMENSION_

#include "DD_PackPush.h"

#include "DbDimension.h"

/** Description:
    This class represents Diametric Dimension entities in an OdDbDatabase instance.

    Remarks:
    Diametric Dimension entities require two points defining the diameter of the
    curve being dimensioned. In additional, an optional leaderLength may be 
    used to specify how far the dimension line extends beyond the curve before
    the dogleg to the annotation text. 
    
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDiametricDimension : public OdDbDimension
{
public:

  ODDB_DECLARE_MEMBERS(OdDbDiametricDimension);

  OdDbDiametricDimension();
  
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
    Returns the WCS chord point for this Dimension entity (DXF 15).
    
    Remarks:
    The chord point is the point at which the dimension line for this Dimension entity
    intersects the curve being dimensioned, and would extend beyond it if the annotation
    text is outside the curve.
    
    It is the point on the diameter opposite farChordPoint.
  */
  OdGePoint3d chordPoint() const;

  /** Description:
    Sets the WCS chord point for this Dimension entity (DXF 15).
    
    Arguments:
    chordPoint (I) Chord point.
    
    Remarks:
    The chord point is the point at which the dimension line for this Dimension entity
    intersects the curve being dimensioned, and would extend beyond it if the annotation
    text is outside the curve.

    It is the point on the diameter opposite farChordPoint.
  */
  void setChordPoint(
    const OdGePoint3d& chordPoint);
  
  /** Description:
    Returns the WCS far chord point for this Dimension entity (DXF 10).
    
    Remarks:
    The far chord point is the point on the diameter opposite chordPoint.  
  */
  OdGePoint3d farChordPoint() const;

  /** Description:
    Sets the WCS far chord point for this Dimension entity (DXF 10).

    Arguments:
    farChordPoint (I) Far chord point.  

    Remarks:
    The far chord point is the point on the diameter opposite chordPoint.  
  */
  void setFarChordPoint(
    const OdGePoint3d& farChordPoint);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;
  
  virtual void getClassID(
    void** pClsid) const;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbDiametricDimension object pointers.
*/
typedef OdSmartPtr<OdDbDiametricDimension> OdDbDiametricDimensionPtr;

#include "DD_PackPop.h"

#endif

