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



#ifndef GEOMENT_DBREGION_H
#define GEOMENT_DBREGION_H

#include "DD_PackPush.h"
#include "ModelerDefs.h"
#include "DbEntity.h"

class OdBrBrep;
class OdModelerGeometry;

/** Description:
    This class represents Region entities in an OdDbDatabase instance.

    Library:
    Db
    
    An OdDbRegion entity is a wrapper for an ACIS model that represents
    the geometry of the OdDbRegion entity.
    
   {group:OdDb_Classes} 

*/
class TOOLKIT_EXPORT OdDbRegion : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbRegion);

  OdDbRegion();

  /** Description:
    Writes the ACIS data of this entity to the specified OdStreamBuf object.

    Arguments:
    pStream (I) Pointer to output stream.    
    typeVer (I) Type and version of the ACIS data to write.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not. 
  */
  OdResult acisOut(
    OdStreamBuf* pStream, 
    AfTypeVer typeVer = kAfTypeVerAny);

  /** Description:
    Reads the ACIS data for this entity from the specified OdStreamBuf object.

    Arguments:
    pStream (I) Pointer to input stream.    
    pTypeVer (I) Pointer to the type and version of the ACIS data to return.
    
    Remarks:
    
    If typeVer is NULL, this function uses the type and version 
    of this Region entity.
  */
  OdResult acisIn(
    OdStreamBuf* pStream, 
    AfTypeVer *pTypeVer = NULL);

  /** Description:
    Returns the boundary representation of the 3D Solid Entity.
    Arguments:
    brep (O) Receives the boundary representation.
  */
  void brep(
    OdBrBrep& brep);

  /** Description:
    Returns true if and only if there is no ACIS model associated with this entity. 
  */
  bool isNull() const;

  /** Description:
    Creates OdDbRegion entities from the closed loops defined by the specified curve segments.
    
    Arguments:
    curveSegments (I) Array of curve segments.
    regions (O) Receives an array of pointers to the *regions*.
    
    Remarks:
    Each curve segment must be one of the following
    
    @untitled table
    OdDb3dPolyline
    OdDbArc
    OdDbCircle
    OdDbEllipse
    OdDbLine
    OdDbPolyline
    OdDbSpline
    
    The newly created *regions* are non- *database* -resident. It is up to the caller to either add them
    to an OdDbDatabase or to delete them.
    
    Returns eOk if successful, or an appropriate error code if not. 
  */
  static OdResult createFromCurves( 
    const OdRxObjectPtrArray& curveSegments,
    OdRxObjectPtrArray& regions );

  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).

    Arguments:
    normal (O) Receives the *normal*.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not. 
  */
  virtual OdResult getNormal(
  OdGeVector3d& normal) const;
  
  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  DD_USING(OdDbEntity::getPlane);

  /*

  virtual void getPerimeter(double&) const;
  virtual void getArea(double& regionArea) const;
  virtual void getAreaProp( const OdGePoint3d& origin, const OdGeVector3d& xAxis,
                            const OdGeVector3d& yAxis, double& perimeter,
                            double& area, OdGePoint2d& centroid,
                            double momInertia[2], double& prodInertia,
                            double prinMoments[2], OdGeVector2d prinAxes[2],
                            double radiiGyration[2], OdGePoint2d& extentsLow,
                            OdGePoint2d& extentsHigh )const;



  virtual void intersectWith( const OdDbEntity* ent, OdDb::Intersect intType,
                              OdGePoint3dArray& points, int thisGsMarker,
                              int otherGsMarker ) const;
  virtual void intersectWith( const OdDbEntity* ent, OdDb::Intersect intType,
                              const OdGePlane& projPlane, OdGePoint3dArray& points,
                              int thisGsMarker, int otherGsMarker ) const;

  virtual OdDbSubentId internalSubentId(void* ent) const;
  virtual void* internalSubentPtr(const OdDbSubentId& id) const;

  virtual void getSubentPathsAtGsMarker( OdDb::SubentType type,
                      int gsMark, const OdGePoint3d& pickPoint,
                      const OdGeMatrix3d& viewXform, int& numPaths,
                      OdDbFullSubentPath*& subentPaths, int numInserts = 0,
                      OdDbObjectId* entAndInsertStack = NULL) const;

  virtual void getGsMarkersAtSubentPath( const OdDbFullSubentPath& subPath,
                                         OdDbIntArray& gsMarkers) const;

  virtual OdDbEntity* subentPtr(const OdDbFullSubentPath& id) const;

  virtual void booleanOper(OdDb::BoolOperType operation, OdDbRegion* otherRegion);

  virtual OdUInt32 numChanges() const;
  */

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual void getClassID(
    void** pClsid) const;

  virtual OdDbObjectPtr decomposeForSave(
    OdDb::DwgVersion ver, 
    OdDbObjectId& replaceId, 
    bool& exchangeXData);

  /** Description:
     Directly sets the contained ACIS geometry of this entity.
     Arguments:
     g (I) Pointer to the modeler geometry.
  */
  void setModelerGeometry( 
    OdModelerGeometry* g );

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;
  
  virtual OdResult transformBy( 
    const OdGeMatrix3d& xfm );
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbRegion object pointers.
*/
typedef OdSmartPtr<OdDbRegion> OdDbRegionPtr;

#include "DD_PackPop.h"

#endif


