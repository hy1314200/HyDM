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



#ifndef OD_DBLEOD_H
#define OD_DBLEOD_H 1

#include "DD_PackPush.h"

#include "DbCurve.h"
#include "DbDimStyleTableRecord.h"

/** Description:
    This class represents Leader entities in an OdDbDatabase instance.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLeader : public OdDbCurve
{
public:
  ODDB_DECLARE_MEMBERS(OdDbLeader);

  OdDbLeader();
    
  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  virtual OdGeVector3d normal() const;
  
  /** Description:
    Returns the number of vertices in this Leader entity (DXF 76).
 */
  virtual int numVertices() const;

  /** Description:
    Appends the specified WCS *vertex* to this Leader entity.
     
    Arguments:
    vertex (I) Vertex to append.
       
    Remarks:
    Returns true if and only if the *vertex* was appended.

    The specified *vertex* is projected onto the plane of this Leader, parallel to its *normal*.

    Note:
    If the specified *vertex* is inside a 2e-10 cube surrounding the
    previous *vertex*, it will not be appended.
  */
  virtual bool appendVertex(
    const OdGePoint3d& vertex);

  /** Description:
    Removes the last *vertex* of this Leader entity.
  */
  virtual void removeLastVertex();

  /** Description:
    Returns the WCS first *vertex* of this Leader entity.
  */
  virtual OdGePoint3d firstVertex() const;

  /** Description:
    Returns the WCS last *vertex* of this Leader entity.
  */
  virtual OdGePoint3d lastVertex() const;

  /** Description:
    Returns the specified WCS *vertex* of this Leader entity.
    Arguments:
    index (I) Vertex *index*.
  */
  virtual OdGePoint3d vertexAt(
    int index) const;

  /** Description:
    Sets the specified WCS *vertex* of this Leader entity.

    Arguments:
    index (I) Vertex *index*.
    vertex (I) WCS value for the *vertex*.

    Remarks:
    Returns true if and only if the *vertex* was set.
  */
  virtual bool setVertexAt(
    int index, 
    const OdGePoint3d& vertex);
  
  /** Description:
    Returns true if and only if this Leader entity has an arrowhead (DXF 71).
  */
  virtual bool hasArrowHead() const;

  /** Description:
    Enables the arrowhead for this Leader entity (DXF 71).
  */
  virtual void enableArrowHead();

  /** Description:
    Disables the arrowhead for this Leader entity (DXF 71).
  */
  virtual void disableArrowHead();

  /** Description:
    Returns true if and only if this Leader entity has a hookline (DXF 75).
  */
  virtual bool hasHookLine() const;

  /** Description:
    Returns true if the hookline is codirectional with the
    OCS X-axis of this Leader entity, or false if it is antidirectional with it.
  */
  virtual bool isHookLineOnXDir() const;

  /** Description:
    Sets this Leader entity to use a spline-fit leader line (DXF 72).
  */
  virtual void setToSplineLeader();

  /** Description:
    Sets this Leader to use a straight line segments (DXF 72).
  */
  virtual void setToStraightLeader();

  /** Description:
    Returns true if and only if this Leader entity uses a spline-fit leader line (DXF 72).
  */
  virtual bool isSplined() const;
  
  /** Description:
    Returns the Object ID of the dimension style (OdDbDimStyleTableRecord) used by this Leader entity (DXF 3).
  */
  virtual OdDbHardPointerId dimensionStyle() const;

  /** Description:
    Sets the Object ID of the dimension style (OdDbDimStyleTableRecord) to used by this Leader entity (DXF 3).
    
    Arguments:
    dimensionStyle (I) Object ID of the dimension style.
  */
  virtual void setDimensionStyle(
    const OdDbHardPointerId& dimStyleID);
    
  /** Description:
    Sets the annotation entity of this Leader entity (DXF 340).
      
    Arguments:
    annoId (I) The Object ID of the annotation entity.
    xDir (I) WCS X-axis of the annotation entity.
    annotationWidth (I) Annotation entity width.
    annotationHeight (I) Annotation entity height.
    hookLineOnXDir (I) True to set hookline codirectional with the
                       OCS X-axis of this annotation entity, or false to set it antidirectional with it.
    
    Remarks:
    This Leader entity is appended to the persistent reactor list of the annotation entity.
    
    The annotation entity must be one of, or a subclass of one of, the following
    
    @table
    Name        Entity type          Description
    kMText      OdDbMText            MText 
    kFcf        OdDbFcf              Feature control frame (Tolerance)  
    kBlockRef   OdDbBlockReference   Block reference

    Other than annoId, the parameters are usually set with evaluateLeader().      
  */
  virtual void attachAnnotation(
    OdDbObjectId annoId);

  virtual void attachAnnotation(
    OdDbObjectId annoId,
    OdGeVector3d xDir,
    double annotationWidth,
    double annotationHeight,
    bool hookLineOnXDir);

  /** Description:
    Removes this Leader entity from the persistent reactor list of its annotation entity,
    and sets the annotation Object ID to NULL.
  */
  virtual void detachAnnotation();

  /** Description:
     Returns the annotation Object ID of the annotation entity associated with this Leader entity (DXF 340).
  */
  virtual OdDbObjectId annotationObjId() const;

  /** Description:
    Returns the OCS X-axis of the annotation entity associated with this Leader entity (DXF 211).
  */
  virtual OdGeVector3d annotationXDir() const;

  /** Description:
    Returns the annotation *offset* of this Leader entity (DXF 213).
    
    Remarks:
    The annotation *offset* determines the the final leader endpoint of this Leader entity
    
    @table
    annoType()     Final Leader Endpoint
    kMText         annotationOffset() + OdDbMText::location() ± OdGeVector3d(dimgap(), 0, 0) 
    kFcf           annotationOffset() + OdDbFcf::location()   ± OdGeVector3d(dimgap(), 0, 0)
    kBlockRef      annotationOffset() + OdDbBlockReference::position()
    kNoAnno        lastVertex() 
    
    Note:
    dimgap() is subtracted if the annotation is to the right of the Leader entity (isHookLikeOnXDir() == true)
    and added if it is to the left.
  */
  virtual OdGeVector3d annotationOffset() const;

  /** Description:
    Sets the annotation *offset* of this leader (DXF 213).
    
    Arguments:
    offset (I) Annotation offset.

    Remarks:
    The annotation *offset* determines the the final leader endpoint of this Leader entity
    
    @table
    annoType()     Final Leader Endpoint
    kMText         annotationOffset() + OdGeVector3d(± dimgap(), 0, 0) + OdDbMText::location()
    kFcf           annotationOffset() + OdGeVector3d(± dimgap(), 0, 0) + OdDbFcf::location()
    kBlockRef      annotationOffset() + OdDbBlockReference::position()
    kNoAnno        lastVertex() 
    
    Note:
    dimgap() is added if isHookLikeOnXDir() is true, subtracted if it is false.
  */
  virtual void setAnnotationOffset(
    const OdGeVector3d& offset);
  
  /** Description:
    The type of annotation used by a Leader entity. 
  */
  
  enum AnnoType
  { 
    kMText      = 0,   // OdDbMText
    kFcf        = 1,   // OdDbFcf
    kBlockRef   = 2,   // OdDbBlockReference.
    kNoAnno     = 3    // No annotation.
  };

  /** Description:
    Returns the annotation type associated with this Leader entity (DXF 73).

    Remarks:   
    The annoType will return one of the following
    
    @table
    Name        Value    Entity type          Description
    kMText      0        OdDbMText            MText 
    kFcf        1        OdDbFcf              Feature control frame (Tolerance)  
    kBlockRef   2        OdDbBlockReference   Block reference
    kNoAnno     3        --                   No annotation
  */
  AnnoType annoType() const;

  /** Description:
      Returns the height of the annotation entity associated with this Leader entity (DXF 40).
  */
  double annoHeight() const;

  /** Description:
      Returns the width of the annotation entity associated with this Leader entity (DXF 41).
  */
  double annoWidth() const;
  
  /** Description:
    Returns the DIMASZ value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual double dimasz() const;

  /** Description:
    Returns the DIMCLRD value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual OdCmColor dimclrd() const;

  /** Description:
    Returns the DIMGAP value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual double dimgap() const;

  /** Description:
    Returns the DIMLWD value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual OdDb::LineWeight dimlwd() const;

  /** Description:
    Returns the DIMLDRBLK value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual OdDbObjectId dimldrblk() const;

  /** Description:
    Returns the DIMSAH value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual bool dimsah() const;

  /** Description:
    Returns the DIMSCALE value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual double dimscale() const;

  /** Description:
    Returns the DIMTAD value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual int dimtad() const;

  /** Description:
    Returns the DIMTXSTY value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual OdDbObjectId dimtxsty() const;

  /** Description:
    Returns the DIMTXT value for this Leader entity.  
    
    Remarks:
    The value from this Leader's dimension style will be returned unless that value is overridden, 
    in which case the override value will be returned.
  */
  virtual double dimtxt() const;
  
  /** Description:
    Sets the DIMASZ value for this Leader entity.
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimasz(
    double newValue);

  /** Description:
    Sets the DIMCLRD value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimclrd(
    const OdCmColor& newValue);

  /** Description:
    Sets the DIMGAP value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimgap(
    double newValue);

  /** Description:
    Sets the DIMLDRBLK value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimldrblk(
    OdDbObjectId newValue);
  virtual void setDimldrblk(
    const char* newValue);

  /** Description:
    Sets the DIMLWD value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimlwd(
    OdDb::LineWeight newValue); 

  /** Description:
    Sets the DIMSAH value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimsah(
    bool newValue);

  /** Description:
    Sets the DIMSCALE value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimscale(
    double newValue);

  /** Description:
    Sets the DIMTAD value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimtad(
    int newValue);

  /** Description:
    Sets the DIMTXSTY value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimtxsty(
    OdDbObjectId newValue);

  /** Description:
    Sets the DIMTXT value for this Leader entity.  
    
    Arguments:
    newValue (I) New value.  
  */
  virtual void setDimtxt(
    double newValue);
    
  virtual void getClassID(
    void** pClsid) const;
  
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

  /* Replace OdRxObjectPtrArray */

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const; 

  virtual void subClose();

  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;
 
  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  virtual bool isClosed() const;

  virtual bool isPeriodic() const;

  virtual OdResult getStartParam(
    double& startParam) const;

  virtual OdResult getEndParam (
  double& endParam) const;

  virtual OdResult getStartPoint(
    OdGePoint3d& startPoint) const;

  virtual OdResult getEndPoint(
    OdGePoint3d& endPoint) const;

  virtual OdResult getPointAtParam(
    double param, 
    OdGePoint3d& pointOnCurve) const;

  virtual OdResult getParamAtPoint(
    const OdGePoint3d& pointOnCurve, 
    double& param) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  /** Note:
     This function is an override for OdDbEntity::subSetDatabaseDefaults() to set 
     the dimension style of this entity to the current style for the specified *database*.
  */
  void subSetDatabaseDefaults(
    OdDbDatabase *pDb);
  
  /** Description:
    Updates this the geometry of this leader per its relationship to its
    annotation entity.
  */
  virtual OdResult evaluateLeader();

  
  /** Note:
    Support for persistent reactor to annotation entity.
  */
  virtual void modifiedGraphics(
    const OdDbObject* pObject);
  
  virtual void erased(
    const OdDbObject* pObject, 
    bool erasing = true);

  /** Description:
    Copies the dimension style settings, including overrides, of this entity into the specified
    dimension style table record.
    
    Arguments:
    pRecord (O) Receives the effective dimension style data associated with entity.
    
    Remarks:
    The *copied* data includes the dimension style data with all applicable overrides. 
  */
  void getDimstyleData(
    OdDbDimStyleTableRecord *pRecord) const;

  /** Description:
    Copies the dimension style settings, including overrides, from the specified
    dimension style table record to this entity.
    
    Arguments:
    pDimstyle (I) Pointer to non- *database* -resident dimension style record.
    dimstyleID (I) Database-resident dimension style record.

    Remarks:
    The *copied* data includes the dimension style with all applicable overrides. 
  */
  void setDimstyleData(
    const OdDbDimStyleTableRecord* pDimstyle);
  void setDimstyleData(
    OdDbObjectId dimstyleID);

  /** Description:
      Sets the plane to contain this Leader entity.

      Arguments:
      leaderPlane (I) Leader plane.
      
      Note: 
      Any associativity of this Leader entity will be broken.
  */
  virtual void setPlane(
    const OdGePlane& leaderPlane);

/*
     virtual void goodbye(const OdDbObject*);
     virtual void copied(const OdDbObject*, const OdDbObject*);
     
     virtual void intersectWith(const OdDbEntity*,
       OdDb::Intersect, 
       OdGePoint3dArray&,
       int thisGsMarker = 0,
       int otherGsMarker = 0) const;
     virtual void intersectWith(const OdDbEntity*,
       OdDb::Intersect, 
       const OdGePlane& projPlane, 
       OdGePoint3dArray&,
       int thisGsMarker = 0, 
       int otherGsMarker = 0) const;

  */

};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbLeader object pointers.
*/
typedef OdSmartPtr<OdDbLeader> OdDbLeaderPtr;

#include "DD_PackPop.h"

#endif


