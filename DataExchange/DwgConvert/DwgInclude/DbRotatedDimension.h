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



#ifndef _DB_ROTATED_DIMENSION_
#define _DB_ROTATED_DIMENSION_

#include "DD_PackPush.h"

#include "DbDimension.h"

/** Description
    This class represents Rotated Dimension entities in an OdDbDatabase instance.

    Remarks:
    A Rotated Dimension entity dimensions the distance between between any two points in space
    as projected onto a line at the specfied rotation angle in the plane of the dimension.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRotatedDimension : public OdDbDimension
{
public:
  ODDB_DECLARE_MEMBERS(OdDbRotatedDimension);

  OdDbRotatedDimension();
  
  /* OdDbRotatedDimension(
       double rotation,
       const OdGePoint3d& xLine1Point,
       const OdGePoint3d& xLine2Point,
       const OdGePoint3d& dimLinePoint,
       const char* dimText = NULL,
       OdDbObjectId dimStyle = OdDbObjectId::kNull);
  */

  /** Description:
    Returns the WCS start point of the first extension line of this Dimension entity (DXF 13).
      
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  OdGePoint3d xLine1Point() const;

  /** Description:
    Sets the WCS start point of the first extension line of this Dimension entity (DXF 13).
  
    Arguments:
    xLine1Point (I) Start point.
        
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  void setXLine1Point(
    const OdGePoint3d& xLine1Point);
  
  /** Description:
    Returns the WCS start point of the second extension line of this Dimension entity (DXF 14).
      
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  OdGePoint3d xLine2Point() const;

  /** Description:
    Sets the WCS start point of the second extension line of this Dimension entity (DXF 14).
  
    Arguments:
    xLine2Point (I) Start point.
        
    Remarks:
    This point and the centerPoint define one side of 
    the angle being dimensioned.
  */
  void setXLine2Point(
    const OdGePoint3d& xLine2Point);
  
  /** Description:
    Returns the WCS point defining the location of dimension line for this Dimension entity (DXF 10).
  */
  OdGePoint3d dimLinePoint() const;

  /** Description:
    Sets the WCS point defining the location of dimension line for this Dimension entity (DXF 10).
    
    Arguments:
    dimLinePoint (I) Dimension line point.
  */
  void setDimLinePoint(
    const OdGePoint3d& dimLinePoint);
  
  /** Description:
    Returns the obliquing angle for this Dimension entity (DXF 52).
    
    Note:
    All angles are expressed in radians.
  */
  double oblique() const;

  /** Description:
    Sets the obliquing angle for this Dimension entity (DXF 52).
    
    Arguments:
    oblique (I) Obliquing angle.
    
    Note:
    All angles are expressed in radians.
  */
  void setOblique(
    double oblique);
  
  /** Description:
    Returns the *rotation* angle for this Dimension entity (DXF 50).
    
    Remarks:
    The *rotation* angle is measured from the OCS X-axis to the dimension line of
    this Dimension entity.

    Note:
    All angles are expressed in radians.
  */
  double rotation() const;

  /** Description:
    Sets the *rotation* angle for this Dimension entity (DXF 50).
    
    Arguments:
    rotation (I) Rotation angle.

    Remarks:
    The *rotation* angle is measured from the OCS X-axis to the dimension line of
    this Dimension entity.

    Note:
    All angles are expressed in radians.
  */
  void setRotation(double rotation);

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
  This template class is a specialization of the OdSmartPtr class for OdDbRotatedDimension object pointers.
*/
typedef OdSmartPtr<OdDbRotatedDimension> OdDbRotatedDimensionPtr;

#include "DD_PackPop.h"

#endif

