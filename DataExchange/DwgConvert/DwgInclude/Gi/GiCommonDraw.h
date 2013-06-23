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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef __ODGICOMMONDRAW_H__
#define __ODGICOMMONDRAW_H__

#include "DD_PackPush.h"

#include "RxObject.h"
#include "Gi.h"

class OdGiGeometry;
class OdGiContext;
class OdGiSubEntityTraits;
class OdDbDatabase;
class OdDbStub;
class OdGePoint3d;
class OdGiDrawable;
/** Description:
  This template class is a specialization of OdSmartPtr for OdGiDrawable object pointers.
*/
typedef OdSmartPtr<OdGiDrawable> OdGiDrawablePtr;

/** Description:
    Viewport regeneration modes. 
*/
typedef enum
{
  eOdGiRegenTypeInvalid         = 0,
  kOdGiStandardDisplay          = 2,
  kOdGiHideOrShadeCommand       = 3,
  kOdGiRenderCommand            = 4,
  kOdGiForExplode               = 5,
  kOdGiSaveWorldDrawForProxy    = 6,
  kOdGiForExtents               = 7
} OdGiRegenType;

/** Description: 
    Deviation types used for tessellation.
*/
typedef enum
{
  kOdGiMaxDevForCircle      = 0,
  kOdGiMaxDevForCurve       = 1,
  kOdGiMaxDevForBoundary    = 2,
  kOdGiMaxDevForIsoline     = 3,
  kOdGiMaxDevForFacet       = 4
} OdGiDeviationType;


class OdGiCommonDraw;
class OdGiTextStyle;
class OdPsPlotStyleData;
class OdGiConveyorGeometry;


enum // temporary fix : to be removed
{
  kOdGiIncludeScores    = 2,
  kOdGiRawText          = 4,
  kOdGiIncludePenups    = 8,
  kOdGiPlotGeneration   = 16
};

/** Description:
    This class defines common operations and properties that are used in the
    DWGdirect vectorization process.
    
    Remarks:
    An instance of an OdGiContext subclass is normally created as a preliminary step in the vectorization process, to be
    used throughout the vectorization.

    Most of the virtual functions in this class (the ones that are not pure virtual) are no-ops, serving only to define an interface.

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiContext : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiContext);

  /** Description:
    Returns a pointer to the *database* that is currently being vectorized.
  */
  virtual OdDbDatabase* database() const = 0;

  /** Description:
    Opens a drawable object belonging to the *database* associated with this Context object.
    
    Arguments:
    objectId (I) Object ID of the drawable to be opened.
    
    Remarks: 
    Returns a pointer to the drawable object.
  */
  virtual OdGiDrawablePtr openDrawable(
    OdDbStub* objectId) = 0;

  /** Description:
      Returns the default lineweight associated with this Context object.
  */
  virtual OdDb::LineWeight defaultLineWeight() const;

  /** Description:
    Returns the common LinetypeScale for this Context object.
    
  */
  virtual double commonLinetypeScale() const;

  /** Description:
    Returns the default text style associated with this Context object.
    
    Arguments:
    textStyle (O) Receives the TextStyle object.
  */
  virtual void getDefaultTextStyle(
    OdGiTextStyle& textStyle);

  /** Description:
    Vectorizes the specified shape into the specified interface object.

    Arguments:
    pDraw (I) Pointer to the CommonDraw object.
    position (I) Position of the shape.
    shapeNumber (I) Shape number.
    pStyle (I) Pointer to the TextStyle for the shape.
  */
  virtual void drawShape(
    OdGiCommonDraw* pDraw, 
    OdGePoint3d& position, 
    int shapeNumber, 
    const OdGiTextStyle* pStyle);

  /** Arguments:
      pDest (I) Pointer to the Conveyor object.
      u (I) Baseline direction for the text.
      v (I) Up direction for the text.
      pExtrusion (I) Pointer to the Extrusion vector for the text.
  */
  virtual void drawShape(
    OdGiConveyorGeometry* pDest,
    const OdGePoint3d& position,
    const OdGeVector3d& u, 
    const OdGeVector3d& v,
    int shapeNumber, 
    const OdGiTextStyle* pStyle,
    const OdGeVector3d* pExtrusion);

  /** Description:
    Vectorizes the specified text string into the supplied CommonDraw object.

    Arguments:
    pDraw (I) Pointer to the CommonDraw object.
    position (I) Position of the text string.
    msg (I) Text string.
    length (I) Number of bytes in msg (not including the optional null byte).
    pStyle (I) Pointer to the TextStyle for msg.
    flags (I) Flags.
    
    Remarks:
    flags will be a combination of the following:
    
    @table
    Name                  Value
    kOdGiIncludeScores    2
    kOdGiRawText          4
    kOdGiIncludePenups    8
    kOdGiPlotGeneration   16
  */
  virtual void drawText(
    OdGiCommonDraw* pDraw, 
    OdGePoint3d& position,
    const OdChar* msg, 
    OdInt32 length,
    const OdGiTextStyle* pStyle, 
    OdUInt32 flags = 0);

  /** Arguments:
    height (I) Height of the text.
    width (I) Width of text.
    oblique (I) Oblique angle.
      
    Note:
    All angles are expressed in radians.
    
    As currently implemented, this function ignores width and oblique.
    They will be fully implemented in a future *release*.
  */
  virtual void drawText(
    OdGiCommonDraw* pDraw, 
    OdGePoint3d& position,
    double height, 
    double width, 
    double oblique, 
    const OdChar* msg);

  /** Arguments:
      pDest (I) Pointer to the Conveyor object.
      u (I) Baseline direction for the text.
      v (I) Up direction for the text.
      pExtrusion (I) Pointer to the extrusion vector for the text.
      raw (I) If and only if true, escape sequences, such as %%P, will not be converted to special characters.
  */
  virtual void drawText(
    OdGiConveyorGeometry* pDest,
    const OdGePoint3d& position,
    const OdGeVector3d& u, 
    const OdGeVector3d& v,
    const OdChar* msg, 
    OdInt32 length, 
    bool raw,
    const OdGiTextStyle* pStyle,
    const OdGeVector3d* pExtrusion);


  /** Description:
    Returns the extents box for the specified text.
       
    Arguments:
    msg (I) Text string.
    length (I) Number of bytes in msg (not including the optional null byte).
    style (I) TextStyle for msg.
    flags (I) Flags.
    min (O) Receives the lower-left corner of the extents box.
    max (O) Receives the upper-right corner of the extents box.
    pEndPos (O) If non-NULL, receives the end position of the text string.

    Remarks:
    flags will be a combination of the following:
    
    @table
    Name                  Value
    kOdGiIncludeScores    2
    kOdGiRawText          4
    kOdGiIncludePenups    8
    kOdGiPlotGeneration   16
  */
  virtual void textExtentsBox(
    const OdGiTextStyle& style, 
    const OdChar* msg, 
    int nStrLen,
    OdUInt32 flags, 
    OdGePoint3d& min, 
    OdGePoint3d& max, 
    OdGePoint3d* pEndPos = 0);

  /** Description:
    Returns the extents box for the specified shape.
    
    Arguments:
    style (I) Pointer to the TextStyle for the shape.
    shapeNumber (I) Shape number.
    min (O) Receives the lower-left corner of the extents box.
    max (O) Receives the upper-right corner of the extents box.
  */
  virtual void shapeExtentsBox(
    const OdGiTextStyle& style, 
    int shapeNumber, 
    OdGePoint3d& min, 
    OdGePoint3d& max);

  /** Description:
    Returns the circle zoom percent for this vectorization process.
    
    Arguments:
    objectID (I) Object of the Viewport entity to be queried.
    
    Remarks:
    circleZoomPercent will be in the range [1,20000]. 100 is the default.
  */
  virtual unsigned int circleZoomPercent(
    OdDbStub* objectId) const;

  /** Description:
    Returns true if and only if this vectorization is intended for hard copy output.
  */
  virtual bool isPlotGeneration() const;

  /** Description:
    Returns true if and only if TrueType text should be filled during this vectorization.
  */
  virtual bool fillTtf() const;

  /** Description:
    Returns the number of isolines to be drawn on surfaces during this vectorization.
  */
  virtual OdUInt32 numberOfIsolines() const;

  /** Description:
    Returns true and only if this vectorization process should be aborted.
  */
  virtual bool regenAbort() const;

  enum PStyleType
  {
    kPsNone       = 0,
    kPsByColor    = 1,
    kPsByName     = 2
  };
  
  /** Description:
    Returns the plot style type of this Context object.
    
    Remarks:
    plotStyleType will return one of the following:
    
    @table
    Name          Value
    kPsNone       0
    kPsByColor    1
    kPsByName     2
  */
  virtual PStyleType plotStyleType() const;
  
  /** Description:
    Retrurns the PaperSpace PlotStyle data for this vectorization.
    Arguments:
    
    penNumber (I) Pen number.
    plotStyleData (O) Receives the PlotStyle data.
    objectId (I) Object ID of plot style.
    
  */
  virtual void plotStyle(
    int penNumber, 
    OdPsPlotStyleData& plotStyleData) const;

  virtual void plotStyle(
    OdDbStub* objectId, 
    OdPsPlotStyleData& plotStyleData) const;
};


/** Description:
    This class is the base class for entity-level vectorization within DWGdirect.

    Library:
    Gi

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiCommonDraw : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiCommonDraw);

  /** Description:
    Returns the regeneration type of the current vectorization process.

    Remarks:
    regenType will return one of the following:
    
    @table
    Name                            Value 
    eOdGiRegenTypeInvalid           0
    kOdGiStandardDisplay            2
    kOdGiHideOrShadeCommand         3
    kOdGiRenderCommand              4
    kOdGiForExplode                 5
    kOdGiSaveWorldDrawForProxy      6
    kOdGiForExtents                 7

  */
  virtual OdGiRegenType regenType() const = 0;

  /** Description:
    Returns true and only if this vectorization process should be aborted.
  */
  virtual bool regenAbort() const = 0;

  /** Description:
    Provides access to this object's the sub-entity traits.

    Remarks:
    This allows the modification of the vectorization attributes such as *color*, linetype, etc.
  */
  virtual OdGiSubEntityTraits& subEntityTraits() const = 0;

  /** Description:
    Provides access to this object's "drawing interface".
    
    Remarks:
    The "drawing interface" is a set of *geometry* functions used during the vectorization process.
  */
  virtual OdGiGeometry& rawGeometry() const = 0;

  /** Description:
    Returns true and only if this vectorization process is the result of a "drag" operation.
    
    Remarks:
  */
  virtual bool isDragging() const = 0;
  
  /** Description:
    Returns the recommended maximum *deviation* of the
    current vectorization, and a *point* on the curve or surface being tesselated.

    Arguments:
    deviationType (I) Deviation type.
    point (O) Receives a *point* on the curve.
        
    Remarks:
    This function returns the recommended maximum difference (with respect to the current active viewport) between the actual curve or surface, 
    and the tessellated curve or surface. 
    
    deviationType will be one of the following:
    
    @table
    Name                       Value
    kOdGiMaxDevForCircle       0      
    kOdGiMaxDevForCurve        1      
    kOdGiMaxDevForBoundary     2      
    kOdGiMaxDevForIsoline      3
    kOdGiMaxDevForFacet        4

    Note: 
    deviation uses circle zoom percent or FacetRes as appropriate.
  */
  virtual double deviation(
    const OdGiDeviationType deviationType, 
    const OdGePoint3d& point) const = 0;

  /** Description:
    Returns the number of isolines to be drawn on surfaces during this vectorization.
  */
  virtual OdUInt32 numberOfIsolines() const = 0;

  /** Description:
      Returns the OdGiContext instance associated with this object.
  */
  virtual OdGiContext* context() const = 0;
};

#include "DD_PackPop.h"

#endif


