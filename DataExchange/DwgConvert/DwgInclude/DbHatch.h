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



#ifndef OD_DBHATCH_H
#define OD_DBHATCH_H

#include "DD_PackPush.h"

#include "DbEntity.h"
#include "Ge/GePoint2dArray.h"
#include "IntArray.h"
#include "Ge/GeVoidPointerArray.h"
#include "CmColorArray.h"

#define HATCH_PATTERN_NAME_LENGTH 32

typedef OdArray<OdGeCurve2d*> EdgeArray;


/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdHatchPatternLine
{
public:
  double            m_dLineAngle;
  OdGePoint2d       m_basePoint;
  OdGeVector2d      m_patternOffset;
  OdGeDoubleArray   m_dashes;

  OdHatchPatternLine() : m_dLineAngle(0.0) { }
};

typedef OdArray<OdHatchPatternLine> OdHatchPattern;


/** Description:
    This class represents Hatch entities in an OdDbDatabase instance.

    Library:
    Db
    
    Note:
    
    Loops must be closed, simple, and continuous. 
    They must be self-intersecting itself only at their endpoints.
    Their start points and end points must coincide. 
    The outer loops must be appended before all of their inner loops.
    
    DWGdirect provides limited validation of the hatch boundary in order to maintain API efficiency
    and performance.
    
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbHatch : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbHatch);

  OdDbHatch();

  enum HatchLoopType
  {
    kDefault            = 0,        // Not yet specified.
    kExternal           = 1,        // Defined by external entities.
    kPolyline           = 2,        // Defined by OdGe polyline.
    kDerived            = 4,        // Derived from a picked point.
    kTextbox            = 8,        // Defined by text.
    kOutermost          = 0x10,     // Outermost loop.
    kNotClosed          = 0x20,     // Open loop.
    kSelfIntersecting   = 0x40,     // Self-intersecting loop.
    kTextIsland         = 0x80,     // Text loop surrounded by an even number of loops.
    kDuplicate          = 0x100     // Duplicate loop.
  };

  enum HatchEdgeType
  {
    kLine     = 1,  // Linear.         
    kCirArc   = 2,  // Circular arc.
    kEllArc   = 3,  // Elliptical arc.
    kSpline   = 4   // Spline curve.
  };

  enum HatchPatternType
  {
    kUserDefined     = 0, // User-defined hatch.
    kPreDefined      = 1, // Defined in acad.pat and acadiso.pat. 
    kCustomDefined   = 2  // In its own PAT file.
  };

  enum HatchStyle
  {
    kNormal   = 0, // Hatch toggles on each boundary.
    kOuter    = 1, // Hatch turns off after first inner loop.
    kIgnore   = 2  // Hatch ignores inner loop
  };

  enum HatchObjectType 
  {
    kHatchObject      = 0, // Classic hatch
    kGradientObject   = 1  // Color gradient
  };

  enum GradientPatternType 
  {
    kPreDefinedGradient    = 0, // Predefined gradient pattern.
    kUserDefinedGradient   = 1  // User-defined gradient pattern.
  };

  /** Description:
    Returns the *elevation* of this entity in the OCS (DXF 30).
    
    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  double elevation() const;

  /** Description:
    Sets the *elevation* of this entity in the OCS (DXF 30).

    Arguments:
    elevation (I) Elevation.    

    Remarks:
    The *elevation* is the *distance* from the WCS origin to the plane of this entity.
  */
  void setElevation(
    double elevation);

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

  virtual bool isPlanar() const { return true; }

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  /** Description:
    Returns the number of loops in this hatch (DXF 91).
  */
  int numLoops() const;

  /** Description:
    Returns the type of loop at the specified index (DXF 92). 
    
    Arguments:
    loopIndex (I) Loop index. 
    
    Remarks:
    The loop type consists of a combination of bits from the HatchLoopType enumeration.
  */
  OdInt32 loopTypeAt(
    int loopIndex) const;

   /** Description:
    Returns the specified loop from this Hatch entity.

    Arguments:
    loopIndex (I) Loop index.
    edgePtrs (O) Receives a set of OdGeCurve pointers to the edges that comprise this loop.
    vertices (O) Receives the *vertices* that comprise this loop.
    bulges (O) Receives a set of *bulges* corresponding to the *vertices* array.
    
    Remarks:
    Should be called with edgePtrs called if the loop is not a polyline loop, and with
    *vertices* and *bulges* if it is.
  */
  void getLoopAt(
    int loopIndex, 
    EdgeArray& edgePtrs) const;
    
  void getLoopAt(
    int loopIndex, 
    OdGePoint2dArray& vertices, 
    OdGeDoubleArray& bulges) const;

  /** Description:
    Appends a loop onto this Hatch entity.

    Arguments:
    loopType (I) Type of loop being appended.
    edgePtrs (I) Array OdGeCurve pointers to the edges that comprise this loop.
    vertices (I) The *vertices* that comprise this loop.
    bulges (I) The *bulges* corresponding to the *vertices* array.
    dbObjIds (I) Array of OdDbEntity object IDs that comprise the loop.
    
    Remarks:
    
    loopType is one of the following:
    
    @table
    Name        Value
    kDefault    0
    kExternal   1
    
  */
  void appendLoop(
    OdInt32 loopType,
    const EdgeArray& edgePtrs);

  void appendLoop(
    OdInt32 loopType,
    const OdGePoint2dArray& vertices,
    const OdGeDoubleArray& bulges);

  void appendLoop(
    OdInt32 loopType, 
    const OdDbObjectIdArray& dbObjIds);

 
  /** Description:
    Inserts a loop into this Hatch entity.

    Arguments:
    loopIndex (I) Loop index.
    loopType (I) Type of loop being appended.
    edgePtrs (I) Array OdGeCurve pointers to the edges that comprise this loop.
    vertices (I) The *vertices* that comprise this loop.
    bulges (I) The *bulges* corresponding to the *vertices* array.
    dbObjIds (I) Array of OdDbEntity object IDs that comprise the loop.

    Remarks:
    loopType is one of the following:
    
    @table
    Name        Value
    kDefault    0
    kExternal   1
  */
  void insertLoopAt(
    int loopIndex, 
    OdInt32 loopType,
    const EdgeArray& edgePtrs);

  void insertLoopAt(
    int loopIndex, 
    OdInt32 loopType,
    const OdGePoint2dArray& vertices,
    const OdGeDoubleArray& bulges);

  void insertLoopAt(
    int loopIndex, OdInt32 loopType,
    const OdDbObjectIdArray& dbObjIds);

  /** Description:
    Removes the specified loop from this Hatch entity.

    Arguments:
    loopIndex (I) Loop index.
  */
  void removeLoopAt(
    int loopIndex);

  /** Description:
    Returns true if and only if this hatch is *associative* (DXF 71).
    Remarks:
    Associative hatch is automatically recalculated 
    when its boundaries are *modified*.
  */
  bool associative() const;

  /** Description:
    Controls the *associative* flag for this hatch (DXF 71).
    Arguments:    
    isAssociative (I) Controls the *associative* flag.

    Remarks:
    Associative hatch is automatically recalculated 
    when its boundaries are *modified*.
  */
  void setAssociative(
    bool isAssociative);

  /** Description:
    Returns the object IDs comprising the specified *associative* loop in this Hatch entity.

    Arguments:
    loopIndex (I) Loop index.
    dbObjIds (O) Receives the boundary object IDs.
    
    Remarks:
    Returns nothing if this Hatch entity is not *associative*.
  */
  void getAssocObjIdsAt(
    int loopIndex, 
    OdDbObjectIdArray& dbObjIds) const;

  /** Description:
    Returns the object IDs comprising all the *associative* boundaries in this Hatch entity.

    Arguments:
    dbObjIds (O) Receives the boundary object IDs.

    Remarks:
    Returns nothing if this Hatch entity is not *associative*.
  */
  void getAssocObjIds(
    OdDbObjectIdArray& dbObjIds) const;

  /** Description:
    Sets the object IDs comprising the specified *associative* loop in this Hatch entity.

    Arguments:
    loopIndex (I) Loop *index*.
    dbObjIds (I) The set of boundary Object ID's that make up the specified loop.

    Remarks:
    Adds the Object ID of this hatch to the reactors of the boundary objects.
  */
  void setAssocObjIdsAt(
    int loopIndex, 
    const OdDbObjectIdArray& dbObjIds);

  /** Description:
    Removes all object IDs that are associated with this Hatch entity.
  */
  void removeAssocObjIds();

  /** Description:
    Returns the hatch pattern type for this Hatch entity (DXF 76).
    Remarks:
    
    patternType will return one of the following:
    
    @table
    Name              Value
    kUserDefined      0
    kPreDefined       1
    kCustomDefined    2 
  */
  OdDbHatch::HatchPatternType patternType() const;

  /** Description:
    Returns true if and only if this hatch is solid fill (DXF 70).
  */
  bool isSolidFill() const;

  
  /** Description:
      Returns the name of the pattern for this Hatch entity (DXF 2).
  */
  OdString patternName() const;

  /** Description:
    Sets the pattern data for this Hatch entity.
    
    Arguments:
    patType (I) Pattern type.
    patName (I) Pattern name.
    angle (I) Pattern *angle*.
    scale (I) Pattern *scale*.
    pat (I) Pattern definition (as in PAT file)
    basePt (I) Base point.
    
    Remarks:
    patType will be one of the following:
    
    @table
    Name              Value
    kUserDefined      0
    kPreDefined       1
    kCustomDefined    2
     
    patName is ignored for patType == kUserDefined; appearance is defined by setPatternAngle(),
    setPatternSpace() and setPatternDouble(). 
  */
  void setPattern(
    OdDbHatch::HatchPatternType patType, 
    const char* patName);
  void setPattern(
    OdDbHatch::HatchPatternType patType, 
    const OdString& patName,
    double angle, 
    double scale,
    const OdHatchPattern& pat,
    OdGePoint2d basePt = OdGePoint2d());

  /** Description:
    Returns the pattern *angle* for this Hatch entity (DXF 52).
    
    Note:
    All angles are expressed in radians.
  */
  double patternAngle() const;

  /** Description:
    Sets the pattern *angle* for this Hatch entity (DXF 52).

    Note:
    All angles are expressed in radians.
  */
  void setPatternAngle(
    double angle);

  /** Description:
    Returns the pattern spacing for this Hatch entity (DXF 41).
    
    Note:
    Pattern spacing is the distance between parallel lines for kUserDefined hatch.
  */
  double patternSpace() const;

  /** Description:
    Sets the pattern spacing for this Hatch entity (DXF 41).

    Arguments:
    space (I) Pattern spacing.
    
    Note:
    Pattern spacing is the distance between parallel lines for kUserDefined hatch.
  */
  void setPatternSpace(
    double space);

  /** Description:
    Returns the pattern scale for this Hatch entity (DXF 41).
  */
  double patternScale() const;

  /** Description:
    Sets the pattern scale for this Hatch entity (DXF 41).
    
    Arguments:
    scale (I) Pattern *scale*.
  */
  void setPatternScale(
    double scale);

  /** Description:
    Returns the pattern double flag for this Hatch entity (DXF 77).
      
    Note:
    Setting the pattern double flag causes a second set of lines, at 90° to the first, for kUserDefined hatch.
  */
  bool patternDouble() const;

  /** Description:
    Sets the pattern double flag for this Hatch entity (DXF 77).

    Arguments:
    isDouble (I) Sets the pattern double flag if true, clears it otherwise.
    
    Note:
    Setting the pattern double flag causes a second set of lines, at 90° to the first, for kUserDefined hatch.
  */
  void setPatternDouble(
    bool isDouble);

 /** Description:
    Returns the number of pattern definition lines for this Hatch entity (DXF 78).
  */
  int numPatternDefinitions() const;

  /** Description:
    Returns the specified pattern definition line for this Hatch entity.

    Arguments:
    lineIndex (I) Line index.
    lineAngle (O) Receives the line *angle* (DXF 53).
    baseX (O) Receives the line base point X (DXF 43).
    baseY (O) Receives the line base point Y (DXF 44).
    offsetX (O) Receives the line offset X (DXF 45).
    offsetY (O) Receives the line offset Y (DXF 46).
    dashes (O) Receives the line dash lengths (DXF 79, 49).
  */
  void getPatternDefinitionAt(
    int lineIndex, 
    double& lineAngle, 
    double& baseX,
    double& baseY, 
    double& offsetX, 
    double& offsetY,
    OdGeDoubleArray& dashes) const;

  /** Description:
    Returns the hatch style of this hatch engity (DXF 75).
      
    Remarks:
    hatchStyle is one of the following:
    
    @table
    Name          Value
    kNormal       0
    kOuter        1
    kIgnore       2
  */
  OdDbHatch::HatchStyle hatchStyle() const;

  /** Description:
    Sets the hatch style of this Hatch entity (DXF 75).
    
    Arguments:
    hatchStyle (I) Hatch style.
    
    Remarks:
    hatchStyle is one of the following:
    
    @table
    Name          Value
    kNormal       0
    kOuter        1
    kIgnore       2
  */
  void setHatchStyle(
    OdDbHatch::HatchStyle hatchStyle);

  /** Description:
    Returns the number of seed points for this Hatch entity (DXF 98).
  */
  int numSeedPoints() const;

  /** Description:
    Returns the specified seed *point* from this Hatch entity (DXF 10).
    Arguments:
    seedIndex (I) Seed *point* index.
  */
  const OdGePoint2d& getSeedPointAt(
    unsigned seedIndex) const;

  /** Description:
    Sets the specified seed *point* for this Hatch entity (DXF 10).

    Arguments:
    seedIndex (I) Seed *point* index.
    point (I) Seed *point*.
  */
  void setSeedPointAt(
    unsigned seedIndex, 
    OdGePoint2d& point);

  /** Description:
    Appends a seed *point* to this hatch.
    
    Arguments:
    Appends the specified seed *point* to this Hatch entity (DXF 10).
  */
  void appendSeedPoint(const OdGePoint2d& point);

  /** Description:
    Returns the pixel size for intersection and ray casting.
  */
  double pixelSize() const;

  /** Description:
    Sets the pixel size for intersection and ray casting.
    Arguments:
    pixelSize (I) Pixel size.
  */
  void setPixelSize(
    double pixelSize);

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

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);

  virtual OdResult getTransformedCopy(
    const OdGeMatrix3d& xfm, 
    OdDbEntityPtr& pCopy) const;

  OdDbObjectPtr decomposeForSave(
    OdDb::DwgVersion ver,
    OdDbObjectId& replaceId,
    bool& exchangeXData);

  /** Description:
    Evaluates the hatch for this Hatch entity.
    Arguments:
    bUnderestimateNumLines (I) Underestimates the hatch count before aborting.

    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  OdResult evaluateHatch(
    bool bUnderestimateNumLines = false);

  /** Description:
    Returns the number of hatch lines in this Hatch entity.
    
    Remarks 
    Returns zero if the hatch pattern is SOLID. 
  */
  int numHatchLines() const;
  
  /** Description:
    Returns the hatch line data for the specified hatch line in this Hatch entity.
      
    Arguments:
    lineIndex (I) Line index.
    startPoint (O) Receives the start *point*.
    endPoint (O) Receives the end *point*.
  */
  void getHatchLineDataAt(
    int lineIndex, 
    OdGePoint2d& startPoint, 
    OdGePoint2d& endPoint) const;
  
  /** Description:
    Returns all the hatch line data for this Hatch entity.
    
    Arguments:
    startPoints (O) Receives the start points.
    endPoints (O) Receives the end points.
  */
  void getHatchLinesData(
    OdGePoint2dArray& startPoints, 
    OdGePoint2dArray& endPoints) const;
  
  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

  virtual void modifiedGraphics(
    const OdDbObject* pObject);

  /* virtual void subSwapIdWith(OdDbObjectId otherId, bool swapXdata = false, bool swapExtDict = false);
  virtual void swapReferences(const OdDbIdMapping& idMap);*/


  /** Description:
    Returns the hatch object type of this Hatch entity.
    
    Remarks:
    hatchObjectType is one of the following:
    
    @table
    Name                Value     Description
    kHatchObject        0         Classic hatch
    kGradientObject     1         Color gradient
  */
  OdDbHatch::HatchObjectType hatchObjectType() const;

  /** Description:
    Sets the hatch object type of this Hatch entity.
    
    Arguments:
    hatchObjectType (I) Hatch object type.
    
    Remarks:
    hatchObjectType is one of the following:
    
    @table
    Name                Value     Description
    kHatchObject        0         Classic hatch
    kGradientObject     1         Color gradient
  */
  void setHatchObjectType(
    OdDbHatch::HatchObjectType hatchObjectType);
  
  /** Description:
    Returns true if and only if this Hatch entity is a *color* gradient.
  */
  virtual bool isGradient() const;

  /** Description:
    Returns true if and only if this Hatch entity is of type kHatchObject.
  */
  virtual bool isHatch() const;
  
  /** Description:
    Returns the gradient type of this Hatch entity.
    
    Remarks:
    gradientType is one of the following:
    
    @table
    Name                    Value
    kPreDefinedGradient     0
    kUserDefinedGradient    1
  */
  OdDbHatch::GradientPatternType gradientType() const;
  
  /** Description:
      Returns the name of the gradient of this Hatch entity.
  */
  OdString gradientName() const;
  
  /** Description:
    Sets the gradient type and name for this Hatch entity.
    Arguments:
    gradientType (I) Gradient type.
    gradientName (I) Gradient name.
    
    Remarks:
    gradientType is one of the following:
    
    @table
    Name                    Value
    kPreDefinedGradient     0
    kUserDefinedGradient    1
  */
  void setGradient(
    OdDbHatch::GradientPatternType gradientType, 
    const char* gradientName);
  
  /** Description:
    Returns the *angle* of the gradient for this Hatch entity.
      
    Remarks:
    All angles are expressed in radians.  
  */
  double gradientAngle() const;

  /** Description:
    Returns the *angle* of the gradient for this Hatch entity.

    Arguments:
    angle (I) Gradient *angle*.
          
    Remarks:
    All angles are expressed in radians.  
  */
  void setGradientAngle(double angle);
  
  /** Description:
    Returns the *colors* and interpolation *values* describing the gradient fill for this Hatch entity.
    
    Arguments:
    colors (O) Array of *colors* defining the gradient.
    values (O) Array of interpolation *values* for the gradient.
  */
  void getGradientColors(
    OdCmColorArray& colors, 
    OdGeDoubleArray& values) const;
  
  /** Description:
    Returns the *colors* and interpolation values describing the gradient fill for this Hatch entity.
    
    Arguments:
    colors (I) Array of colors defining the gradient.
    values (I) Array of interpolation values for the gradient.

    Note:
    count must be two for the current implementation.
    
    Throws:
    @table
    Exception             Cause
    eInvalidInput         count < 2 || values[0] != 0. || values[count-1] != 1.
    eNotImplementedYet    count > 2
  */
  void setGradientColors(
    OdUInt32 count, 
    const OdCmColor* colors, 
    const double* values);
  
  /** Description:
    Returns the *oneColorMode* for this Hatch entity.
  */
  bool getGradientOneColorMode() const;

  /** Description:
    Controls the *oneColorMode* for this Hatch entity.
    Arguments:
    oneColorMode (I) Controls the *oneColorMode*.
  */
  void setGradientOneColorMode(
    bool oneColorMode);
  
  /** Description:
    Returns the *luminance* *value* for this Hatch entity.
    
    Remarks:
    Returns a *value* in the range 0.0 to 1.0.
    
    If the gradient is using *oneColorMode*, this function returns 
    the *luminance* *value* applied to the first *color*.
  */
  double getShadeTintValue() const;
  
  /** Description:
    Sets the *luminance* *value* for this Hatch entity.
    
    Remarks:
    luminance is in the range 0.0 to 1.0.
    
    If the gradient is using *oneColorMode*, this function sets 
    the *luminance* *value* applied to the first color.
  */
  void setShadeTintValue(
    double luminance);
  
  /** Description:
    Returns the interpolation *value* between the 
    default and shifted values of the gradient's definition. 
    
    Remarks:
    A gradientShift of 0 indicates a fully unshifted gradient.
    A gradientShift of 1 indicates a fully shifted gradient.
  */
  double gradientShift() const;
  
  /** Description:
      Sets the interpolation *value* between the 
      default and shifted values of the gradient's definition. 

      Arguments:
      gradientShift (I) Shift *value*.      
      Remarks:
      A gradientShift of 0 indicates a fully unshifted gradient.
      A gradientShift of 1 indicates a fully shifted gradient.
  */
  void setGradientShift(double gradientShift);
  
  /** Description:
    Returns the interpolated *color* of the gradient definition.
    Arguments:
    value (I) Interpolation *value*.
    color (O) Receives the interpolated *color*.
  */
  void evaluateGradientColorAt(
    double value, 
    OdCmColor& color) const;

  void appendToOwner(
    OdDbIdPair& idPair, 
    OdDbObject* pOwnerObject, 
    OdDbIdMapping& ownerIdMap);

  void subClose();

  /** Description:
    Sets the pattern data for this Hatch entity directly, bypassing OdHatchPatternManager (DXF 76 and DXF 2).
    
    Arguments:
    patType (I) Pattern type.
    patName (I) Pattern name.
    angle (I) Pattern *angle*.
    scale (I) Pattern *scale*.
    pat (I) Pattern definition (as in PAT file)
    
    Remarks:
    patType will be one of the following:
    
    @table
    Name              Value
    kUserDefined      0
    kPreDefined       1
    kCustomDefined    2
     
    patName is ignored for patType == kUserDefined; appearance is defined by setPatternAngle(),
    setPatternSpace() and setPatternDouble().
    
    Note:
    angle and scale are not applied to the pattern.
  */
  void setRawPattern(
    OdDbHatch::HatchPatternType patType, 
    const OdString& patName,
    double angle, 
    double scale,
    const OdHatchPattern& pat);


  /** Description:
    Returns the hatch pattern definition for this Hatch entity as it appears in the PAT file.
  */
  OdHatchPattern getPattern() const;


  /** Description:
    Returns the hatch pattern definition for this Hatch entity as it appears in the DWG/DXF file.
  */
  OdHatchPattern getRawPattern() const;
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbHatch object pointers.
*/
typedef OdSmartPtr<OdDbHatch> OdDbHatchPtr;

#include "DD_PackPop.h"

#endif /* OD_DBHATCH_H */

