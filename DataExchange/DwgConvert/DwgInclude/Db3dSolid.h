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



#ifndef GEOMENT_DBSOL3D_H
#define GEOMENT_DBSOL3D_H /* {Secret} */

#include "DD_PackPush.h"
#include "ModelerDefs.h"

class OdDbRegion;
class OdDbSubentId;
class OdDbCurve;
class OdBrBrep;
class OdModelerGeometry;

#include "DbEntity.h"
#include "OdArray.h"

//#define ACIS_LIBRARY

/** Description:
    This class represents 3D Solid entities in an OdDbDatabase instance.

    Library:
    Db
    
    An OdDb3dSolid entity is a wrapper for an ACIS model that represents
    the geometry of the OdDb3dSolid entity.
    
   {group:OdDb_Classes} 

*/
class TOOLKIT_EXPORT OdDb3dSolid : public OdDbEntity
{
public:

  ODDB_DECLARE_MEMBERS(OdDb3dSolid);
  
  OdDb3dSolid();
  
  /** Description:
    Returns true if and only if there is no ACIS model associated with this entity. 
  */
  bool isNull() const;

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
    typeVer (I) Pointer to the type and version of the ACIS data to read.
    
    Remarks:
    
    If typeVer is NULL, this function returns the version of the ACIS data
    of this 3D solid entity.
  */
  OdResult acisIn(
    OdStreamBuf* pStream, 
    AfTypeVer *typeVer = NULL);

  /** Description:
    Returns the boundary representation of the 3D Solid Entity.
    Arguments:
    brep (O) Receives the boundary representation.
  */
  void brep(
    OdBrBrep& brep);

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual void viewportDraw(
    OdGiViewportDraw* pVd) const;

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

  /** Description:
    Creates a WCS aligned solid box centered about the world origin. 
    
    Arguments:
    xLen (I) Dimension along the WCS X-axis.
    yLen (I) Dimension along the WCS Y-axis. 
    zLen (I) Dimension along the WCS Z-axis. 

    Remarks:
    Returns eOk if successful, or an appropriate error code if not. 
    
    Note:
    The following constraints apply
    
    o xLen >= 1e-6.
    o yLen >= 1e-6.
    o zLen >= 1e-6.

  */
  virtual void createBox(
    double xLen, 
    double yLen, 
    double zLen);
    
  /** Description:
    Creates a WCS aligned frustrum centered about the world origin. 
    
    Arguments:
    height (I) Dimension along the WCS Z-axis.
    xRadius (I) Base radius along the WCS X-axis. 
    yRadius (I) Base radius along the WCS Y-axis.
    topXRadius (I) Top radius along the WCS X-axis. 

    Remarks:
    The function can generate a frustrum that is any one the following
    
    o A circular cylinder
    o An elliptical cylinder
    o A circular cone
    o An elliptical cone
    o A truncated circular cone
    o A truncated elliptical cone
    
    Returns eOk if successful, or an appropriate error code if not. 

    Note:    
    The following constraints apply
    
    o height >= 1e-6.
    o xRadius >= 1e-6.
    o yRadius >= 1e-6.
    o topXRadius >= 1e-6. 
  */
  virtual void createFrustum(
    double height, 
    double xRadius,
    double yRadius, 
    double topXRadius);
    
  /** Description:
    Creates a sphere with centered about the world origin. 
    
    Arguments:
    radius (I) Radius.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not. 

    Note:
    The following constraints apply
    
    o radius >= 1e-6.

  */
  virtual void createSphere(
    double radius);

  /** Description:
    Creates a WCS aligned torus with centered about the world origin
    and the Z-axis as its axis of rotation. 
    
    Arguments:
    majorRadius (I) Radius of the torus
    minorRadius (I) Radius of the tube.

    Remarks:

    Returns eOk if successful, or an appropriate error code if not. 

    Note:
    The following constraints apply
    
    o minorRadius >= 1e-6.
    o |majorRadius| >= 1e-6
    o if majorRadius < 0, then minorRadius > |majorRadius| + 1e-6
  */
  virtual void createTorus(
    double majorRadius, 
    double minorRadius);
    
  /** Description:
    Creates a WCS aligned wedge centered about the world origin. 
    
    Arguments:
    xLen (I) Dimension along the WCS X-axis.
    yLen (I) Dimension along the WCS Y-axis. 
    zLen (I) Dimension along the WCS Z-axis. 

    Remarks:
    Returns eOk if successful, or an appropriate error code if not. 
    
    The inclined face is defined by the points 
    
              ( xLen/2, -yLen/2, -zLen/2)
              (-xLen/2, -yLen/2,  zLen/2)  
              ( xLen/2,  yLen/2, -zLen/2)
              (-xLen/2, +yLen/2,  zLen/2)  
    
    Note:
    The following constraints apply
    
    o xLen >= 1e-6.
    o yLen >= 1e-6.
    o zLen >= 1e-6.
  */
  virtual void createWedge(
    double xLen, 
    double yLen, 
    double zLen);

  /** Description:
    Creates a solid by extruding the specified region.
   
    Arguments:
    region (I) Pointer to the *region* to be extruded.
    height (I) Height of the extrusion.
    taperAngle (I) Taper angle.

    Remarks:
    The extrusion direction is along the normal of the region. height may be negative.     


    Note:
    All angles are expressed in radians.
     
    The following constraints apply
    
    o |height| >= 1e-6.
    o |taperAngle| < OdaPI2 - 1e-6.
     
    As implemented, taperAngle is ignored.
    It will be fully implemented in a future *release*.

  */
  virtual OdResult extrude(
    const OdDbRegion* region, 
    double height, 
    double taperAngle = 0.0);

  /** Description:
    Creates a solid by revolving the specified region.
   
    Arguments:
    region (I) Pointer to the *region* to be extruded.
    axisPoint (I) Point on the axis of revolution.
    axisDir (I) Vector defining the axis of revolution.
    angleOfRevolution (I) Angle of revolution.
    
    Remarks:
    The axis of revolution is projected onto the plane of the region, parallel to its normal.

    Note:
    All angles are expressed in radians.
    
    The following constraints apply
    
    o The projected axis of revolution cannot intersect the region. 
    o angleOfRevolution >= 1e-6

  */
  virtual OdResult revolve(
    const OdDbRegion* region,
    const OdGePoint3d& axisPoint, 
    const OdGeVector3d& axisDir,
    double angleOfRevolution);

  /*
  virtual void extrudeAlongPath(const OdDbRegion* region, const OdDbCurve* path);
  
  virtual void getArea(double& area) const;
  virtual void checkInterference(const OdDb3dSolid* otherSolid,
    bool createNewSolid, 
    bool& solidsInterfere,
    OdDb3dSolid** commonVolumeSolid)
    const;
  
  virtual void getMassProp(double& volume,
    OdGePoint3d& centroid,
    double momInertia[3],
    double prodInertia[3],
    double prinMoments[3],
    OdGeVector3d prinAxes[3], 
    double radiiGyration[3],
    OdGeExtents& extents) const;
  
  virtual void getSection(const OdGePlane& plane, OdDbRegion** sectionRegion) const;
  
  virtual void stlOut(const char* fileName, bool asciiFormat) const;
  
  virtual OdDbSubentId internalSubentId(void* ent) const;
  virtual void* internalSubentPtr(const OdDbSubentId& id) const;
  
  virtual void getSubentPathsAtGsMarker(OdDb::SubentType type,
    int gsMark, 
    const OdGePoint3d& pickPoint,
    const OdGeMatrix3d& viewXform, 
    int& numPaths,
    OdDbFullSubentPath** subentPaths, 
    int numInserts = 0,
    OdDbObjectId* entAndInsertStack = NULL) const;
  
  virtual void getGsMarkersAtSubentPath(const OdDbFullSubentPath& subPath, 
                                        OdDbIntArray& gsMarkers) const;
  virtual OdDbEntity* subentPtr(const OdDbFullSubentPath& id) const;
  
  virtual void booleanOper(OdDb::BoolOperType operation, OdDb3dSolid* solid);
  
  virtual void getSlice(const OdGePlane& plane, bool getNegHalfToo, 
                        OdDb3dSolid** negHalfSolid);
  
  virtual void copyEdge(const OdDbSubentId &subentId, OdDbEntity **newEntity);
  virtual void copyFace(const OdDbSubentId &subentId, OdDbEntity **newEntity);
  virtual void extrudeFaces(const OdArray<OdDbSubentId *> &faceSubentIds, 
                            double height, double taper);
  virtual void extrudeFacesAlongPath(const OdArray<OdDbSubentId *> &faceSubentIds, 
                                     const OdDbCurve* path);
  virtual void imprintEntity(const OdDbEntity *pEntity);
  virtual void cleanBody();
  virtual void offsetBody(double offsetDistance);
  virtual void offsetFaces(const OdArray<OdDbSubentId *> &faceSubentIds, 
                           double offsetDistance);
  virtual void removeFaces(const OdArray<OdDbSubentId *> &faceSubentIds);
  virtual void separateBody(OdArray<OdDb3dSolid *> &newSolids);
  virtual void shellBody(const OdArray<OdDbSubentId *> &faceSubentIds, 
                         double offsetDistance);
  virtual void taperFaces(const OdArray<OdDbSubentId *> &faceSubentIds, 
                          const OdGePoint3d &basePoint, 
                          const OdGeVector3d &draftVector,
                          double draftAngle);
  virtual void transformFaces(const OdArray<OdDbSubentId *> &faceSubentIds,
                              const OdGeMatrix3d &matrix);

  virtual void setSubentColor(const OdDbSubentId &subentId,
                              const OdCmColor &color);

  virtual OdUInt32 numChanges() const;
  */
  
  virtual OdDbObjectPtr decomposeForSave (
    OdDb::DwgVersion ver, 
    OdDbObjectId& replaceId, 
    bool& exchangeXData);

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm );
  
  /** Description:
     Directly sets the contained ACIS geometry of this entity.
     Arguments:
     g (I) Pointer to the modeler geometry.
  */
  void setModelerGeometry( 
    OdModelerGeometry* g );
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDb3dSolid object pointers.
*/
typedef OdSmartPtr<OdDb3dSolid> OdDb3dSolidPtr;

#include "DD_PackPop.h"

#endif


