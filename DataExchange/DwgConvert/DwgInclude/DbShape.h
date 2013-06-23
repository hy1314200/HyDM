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



#ifndef _OD_DB_SHAPE_
#define _OD_DB_SHAPE_

#include "DD_PackPush.h"

#include "DbEntity.h"


/*
    This class represents Shape entities in an OdDbDatabase instance.
    
    Remarks:
    A Shape entity is a single character of an SHX font. It is specified by name() and/or shapeNumber() and styleId(). 
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbShape : public OdDbEntity
{
public:

  /*
    OdDbShape(const OdGePoint3d& position,
      double size,
      const char* name,
      double rotation = 0,
      double widthFactor = 0);
  */

  ODDB_DECLARE_MEMBERS(OdDbShape);

  OdDbShape();
  /** Description:
    Returns the *position* of this entity (WCS equivalent of DXF 10).
  */
  OdGePoint3d position() const;
  
  /** Description:
    Sets the *position* of this entity (WCS equivalent of DXF 10).

    Arguments:
    position (I) Position.
  */
  void setPosition(
    const OdGePoint3d& position);

  /** Description:
    Returns the *size* of this Shape entity (DXF 40).
  */
  double size() const;

  /** Description:
    Sets the *size* of this Shape entity (DXF 40).
    Arguments:
    size (I) Shape *size*.
  */
  void setSize(
    double size);

  /** Description:
    Returns the *name* of this Shape entity (DXF 2).
    Remarks:
    The *name* is not part of a Shape entity, but is derived from shapeNumber() and styleId().
  */
  OdString name() const;

  /** Description:
    Sets the *name* of this Shape entity (DXF 2).
    
    Remarks:
    The *name* is not part of a Shape entity, but is derived from shapeNumber() and shapeIndex().
    It is more efficient to use setShapeNumber() and setStyleId() directly.
  */
  OdResult setName(
    const OdString& name);

  /** Description:
    Returns the *rotation* angle of this Shape entity (DXF 50).
    Note:
    All angles are expressed in radians.
 */
  double rotation() const;
  
  /** Description:
    Sets the *rotation* angle of this Shape entity (DXF 50).
    Arguments:
    rotation (I) Rotation angle.
    Note:
    All angles are expressed in radians.
  */
  void setRotation(
    double rotation);

  /** Description:
    Returns the relative X scale factor (width factor) for this Shape entity (DXF 41).
  */
  double widthFactor() const;

  /** Description:
    Sets the relative X scale factor (width factor) for this Shape entity (DXF 41).
    
    Arguments:
    widthFactor (I) Width factor.
  */
  void setWidthFactor(
    double widthFactor);

 /** Description:
    Returns the *oblique* angle of this Shape entity (DXF 51).

    Remarks:
    The range of oblique is ±1.48335 radians (±85°).
    
    Oblique angles are measured clockwise from the vertical.

    Note:
    All angles are expressed in radians.  
  */
  double oblique() const;
  
  /** Description:
    Sets the *oblique* angle of this Shape entity (DXF 51).
    Arguments:
    oblique (I) Oblique angle.
    
    Remarks:
    The range of oblique is ±1.48335 radians (±85°).
    
    Oblique angles are measured clockwise from the vertical.
     
    Note:
    All angles are expressed in radians.  
  */
  void setOblique(
    double oblique);

  /** Description:
    Returns the *thickness* of this entity (DXF 39).
    
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  double thickness() const;
  
  /** Description:
    Sets the *thickness* of this entity (DXF 39).
    Arguments:
    thickness (I) Thickness.
    Remarks:
    Thickness is the extrusion length along the *normal*.
  */
  void setThickness(
    double thickness);

  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  OdGeVector3d normal() const;
  
  /** Description:
    Sets the WCS *normal* to the plane of this entity (DXF 210).
    Arguments:
    normal (I) Normal.
  */
  void setNormal(
    const OdGeVector3d& normal);

  bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  /** Description:
    Returns the shape number of this Shape entity.
    Remarks:
    A Shape entity is a single character of an SHX font. 
    This function returns the code of that character.  
  */
  OdInt16 shapeNumber() const;

  /** Description:
    Sets the shape number of this Shape entity.
    Remarks:
    A Shape entity is a single character of an SHX font. 
    This function sets the code of that character.
    Arguments:
    shapeNumber (I) Shape number.  
  */
  void setShapeNumber(
    OdInt16 shapeNumber);

  /** Description:
    Returns the Object ID of the OdDbTextStyleTableRecord containing
    the SHX font file for this Shape entity.

    Remarks:
    A Shape entity is a single character of an SHX font. This function
    returns a reference to that font.
  */
  OdDbObjectId styleId() const;

  /** Description:
    Sets the Object ID of the OdDbTextStyleTableRecord containing
    the SHX font file for this Shape entity.

    Remarks:
    A Shape entity is a single character of an SHX font. This function
    sets a reference to that font.
    Arguments:
    styleId (I) Style Object ID.  
  */
  OdResult setStyleId(
    OdDbObjectId styleId);

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  void dxfOut(
    OdDbDxfFiler* pFiler) const;

  OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  void getClassID(
    void** pClsid) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbShape object pointers.
*/
typedef OdSmartPtr<OdDbShape> OdDbShapePtr;

#include "DD_PackPop.h"

#endif


