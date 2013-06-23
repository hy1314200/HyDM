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



#ifndef OD_DBSPFILT_H
#define OD_DBSPFILT_H

#include "DD_PackPush.h"

/** Description:
   Defines the disabled back or front clipping plane distance.
*/   
#define ODDB_INFINITE_XCLIP_DEPTH (1.0e+300)

#include "DbFilter.h"
#include "Ge/GePoint3d.h"
#include "Ge/GePoint2dArray.h"

class OdGeExtents3d;
class OdDbBlockReference;

class OdDbFilteredBlockIterator;
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbFilteredBlockIterator object pointers.
*/
typedef OdSmartPtr<OdDbFilteredBlockIterator> OdDbFilteredBlockIteratorPtr;

struct OdGiClipBoundary;

/** Description:
    This class implements Spatial Filter objects in an OdDbDatabase instance.

    Remarks:
    Spatial Filter objects are extruded volumes based on a 2D *boundary*,
    an extrusion direction, and front and back clipping distances.
    
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSpatialFilter : public OdDbFilter
{
public:
  ODDB_DECLARE_MEMBERS(OdDbSpatialFilter);

  OdDbSpatialFilter();
  
  OdRxClass* indexClass() const; 
  
  /** Description:
      TBC.
  */
  //void queryBounds(OdGeExtents3d& ext) const;

  /** Description:
    Returns the clip *boundary* definition of this Spatial Filter object.
    
    Arguments:
    fromPoint (O) Receives the WCS 'from' point.
    toPoint (O) Receives the  WCS 'to' point.
    upDir (O) Receives the WCS *normal* vector.
    viewField (O) Receives the WCS view field vector.
  */
  void getVolume(
      OdGePoint3d& fromPoint, 
      OdGePoint3d& toPoint, 
      OdGeVector3d& upDir,
      OdGeVector2d& viewField ) const;

  /** Description:
    Sets the definition of this Spatial Filter object.

    Arguments:
    points (I) Boundary definition.
    normal (I) WCS Positive extrusion vector.
    elevation (I) Elevation.
    frontClip (I) Front clip distance in the normal direction.
    backClip (I) Back clip distance in the -normal direction.
    enabled (I) Enables the clip volume. 
    
    Remarks:
    The *elevation* is the *distance* from the WCS *origin* to the plane of the clip *boundary*.
 
    Together elevation and normal define the ECS plane of the clip *boundary*.
       
    If only two *points* are provided in points, these *points* define the diagonal of a rectangle. Otherwise,
    they define a non-self-intersecting polygon.
    
    The points are in the ECS defined by normal and elevation. 
    
    If the clip volume is disabled, this Spatial Filter object includes all of 3D space.
  */
  void setDefinition( 
    const OdGePoint2dArray& points, 
    const OdGeVector3d& normal = OdGeVector3d::kZAxis,
    double elevation = 0.0,
    double frontClip = ODDB_INFINITE_XCLIP_DEPTH,
    double backClip = ODDB_INFINITE_XCLIP_DEPTH,
    bool enabled = true); 

  /**
    Arguments:
    xToClipSpace (I) The transformation matrix from WCS to ECS for the clip *boundary*.
  */
  void setDefinition(
    const OdGePoint2dArray& points,
    const OdGeMatrix3d& xToClipSpace, 
    double frontClip = ODDB_INFINITE_XCLIP_DEPTH,
    double backClip = ODDB_INFINITE_XCLIP_DEPTH,
    bool enabled = true);

  /** Description:
    Returns the definition of this Spatial Filter object.

    Arguments:
    points (O) Receives the *boundary* definition.
    normal (O) Receives the WCS positive extrusion vector.
    elevation (I) Receives the *elevation*.
    frontClip (I) Receives the front clip distance in the normal direction.
    backClip (I) Receives the back clip distance in the -normal direction.
    enabled (I) Receives the status of the clip volume. 

    Remarks:
    The *elevation* is the *distance* from the WCS *origin* to the plane of the clip *boundary*.
 
    Together elevation and normal define the ECS plane of the clip *boundary*.
       
    If only two *points* are provided in points, these *points* define the diagonal of a rectangle. Otherwise,
    they define a non-self-intersecting polygon.
    
    The points are in the ECS defined by normal and elevation. 
    
    If the clip volume is disabled, this Spatial Filter object includes all of 3D space.
  */
  void getDefinition( 
    OdGePoint2dArray& points, 
    OdGeVector3d& normal,
    double& elevation, 
    double& frontClip, 
    double& backClip,
    bool& enabled ) const; 

  /**
    Arguments:
    clipBoundary (I) Clip *boundary*.
  */
  void setDefinition(
    const OdGiClipBoundary& clipBoundary);

  /**
    Arguments:
    clipBoundary (O) Clip *boundary*.
  */
  void getDefinition(
    OdGiClipBoundary& clipBoundary) const;

  /**
    Description:
    Returns the *boundary* defintion of this Spatial Filter object.

    Arguments:
    points (O) Receives the *boundary* definition.
  */
  void boundary(
    OdGePoint2dArray& points) const;
    
  /** Description:
    Returns the WCS positive extrusion vector of this Spatial Filter object.
  */
  OdGeVector3d normal() const;
  
  /** Description:
    Returns the WCS *origin* of this Spatial Filter object.
  */
  OdGePoint3d origin() const;

  /** Description:
    Returns true if and only if front clipping is enabled for this Spatial Filter object.
  */  
  bool frontClipEnabled() const;
  
  /** Description:
    Returns the front clipping distance for this Spatial Filter object.
  */  
  double frontClipDist() const;
  
  /** Description:
    Returns true if and only if back clipping is enabled for this Spatial Filter object.
  */  
  bool backClipDistEnabled() const;
  /** Description:
    Returns the back clipping distance for this Spatial Filter object.
  */  
  double backClipDist() const;
  /** Description:
    Returns true if and only if the clip volume for this Spatial Filter object is enabled.
    Remarks:
    If disabled, this Spatial Filter object includes all of 3D space.
  */  
  bool isEnabled() const; 

  /** Description:
    Sets the perspective camera position of this Spatial Filter object.

    Arguments:
    fromPoint (I) Perspective camera position.
  */
  void setPerspectiveCamera(
    const OdGePoint3d& fromPoint);
  
  /** Description:
    Returns true if and only if the specified extents intersect the clip *boundary* of the Spatial Filter object.
  */
  bool clipVolumeIntersectsExtents(
    const OdGeExtents3d& extents) const;
  
  /** Description:
    Returns true if and only if setPerspectiveCamera() has been called for this Spatial Filter object.
  */
  bool hasPerspectiveCamera() const;
  
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbSpatialFilter object pointers.
*/
typedef OdSmartPtr<OdDbSpatialFilter> OdDbSpatialFilterPtr;

#include "DD_PackPop.h"

#endif // OD_DBSPFILT_H


