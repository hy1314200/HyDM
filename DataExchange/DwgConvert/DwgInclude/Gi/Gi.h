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



#ifndef _OD_GI_H_
#define _OD_GI_H_

#include "RxObject.h"

class OdDbStub;
class OdCmEntityColor;
class OdGeVector3d;

typedef enum
{
  kOdGiFillAlways = 1,  // Fill closed graphic primitives.
  kOdGiFillNever  = 2   // Don't fill closed graphic primitives.
} OdGiFillType;

typedef enum
{
  kOdGiInvisible      = 0, // Invisible
  kOdGiVisible        = 1, // Visible
  kOdGiSilhouette     = 2  // Silhouette edge
} OdGiVisibility;

#include "DD_PackPush.h"

/** Description:
    This class provides an interface to the graphical attributes of graphic
    primitives.
  
    Remarks:
    OdGiDrawableTraits is a superset of OdGiSubEntityTraits, and exposes
    additional traits that are consistant for the entire OdGiDrawable.
    
    An OdGiDrawableTraits instance is available during calls to setAttributes.
    Graphical attribute settings are used for all graphical primitives until they are changed.
    
    Library:
    Gi
    
    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiDrawableTraits : public OdRxObject
{ 
public:
  ODRX_DECLARE_MEMBERS(OdGiDrawableTraits);
};

/** Description:
    This class provides an interface to the graphical attributes of graphic
    primitives.
  
    Remarks:
    An OdGiSubEntityTraits instance is available during calls to worldDraw and viewportDraw,
    so that drawable classes can control attributes during the vectorization process. 
    Graphical attribute settings are used for all graphical primitives until they are changed.
    
    The pure virtual functions in this class are implemented by the OdGiAbstractVectorizer
    class.

    Library:
    Gi
    
    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiSubEntityTraits : public OdGiDrawableTraits
{ 
public:
  ODRX_DECLARE_MEMBERS(OdGiSubEntityTraits);

  /** Description:
    Sets the current drawing *color* index (ACI).

    Arguments:
    color (I) *Color* index.
    
    Remarks:
    color will be one of the following:
    
    @table
    Name              Value   Description
    kACIbyBlock       0       ByBlock.
    kACIforeground    7       Foreground *color*.
    kACIbyLayer       256     ByLayer. 
    kACIRed           1       Red. 
    kACIYellow        2       Yellow. 
    kACIGreen         3       Green. 
    kACICyan          4       Cyan. 
    kACIBlue          5       Blue. 
    kACIMagenta       6       Magenta. 
    ..                8-255   Defined by AcadPalette.h
  */
  virtual void setColor(
    OdUInt16 color) = 0;

 /** Description:
    Sets the current drawing *color* from an OdCmEntityColor instance.
    
    Arguments:
    color (I) OdCmEntityColor object.
  */
  virtual void setTrueColor(
    const OdCmEntityColor& color) = 0;

  /** Description:
    Sets the current drawing *layer*.
    Arguments:
    layerId (I) Pointer to the object ID of the LayerTableRecord.  
  */
  virtual void setLayer(
    OdDbStub* layerId) = 0;

  /** Description:
    Sets the current drawing linetype.
    Arguments:
    lineTypeId (I) Pointer to the object ID of the LinetypeTableRecord.  
  */
  virtual void setLineType(
    OdDbStub* lineTypeId) = 0;

  /** Description:
    Sets the current drawing selection marker.
    Arguments:
    marker (I) Selection marker.
  */
  virtual void setSelectionMarker(
    OdInt32 marker) = 0;

  /** Description:
    Sets the current drawing fill type.
    Arguments:
    fillType (I) Fill type.
    
    Remarks:
    fillType will be one of the following:
    
    @table
    Name              Value   Description
    kOdGiFillAlways   1       Fill closed graphic primitives.
    kOdGiFillNever    2       Don't fill closed graphic primitives.
       
    Closed graphic primitives consist of the following:
    
    o  arcs with (OdGiArcType == kOdGiArcSector) || (OdGiArcType == kOdGiArcChord) 
    o  circles 
    o  meshes 
    o  polygons 
    o  shells 

  */
  virtual void setFillType(
    OdGiFillType fillType) = 0;

  /** Description:
    Sets the current drawing fill plane.
    
    Arguments:
    pNormal (I) Pointer to the *normal* to the plane.
    
    Note:
    As implemented, this function does nothing but return.
    It will be fully implemented in a future *release*.
  */
  virtual void setFillPlane(
    const OdGeVector3d* pNormal = 0) {}

  /** Description:
    Sets the current drawing LineWeight.

    Arguments:
    lineWeight (I) LineWeight.
  */
  virtual void setLineWeight(
    OdDb::LineWeight lineWeight) = 0;

  /** Description:
    Sets the current drawing LineType scale.
    Arguments:
    lineTypeScale (I) LineType scale factor.
  */
  virtual void setLineTypeScale(
    double lineTypeScale = 1.0) = 0;

  /** Description:
    Sets the current drawing linetype ScaleToFit property.
    
    Arguments:
    enable (I) Controls ScaleToFit.
    
    Note:
    As implemented, this function does nothing but return.
    It will be fully implemented in a future *release*.
  */
  virtual void setLineTypeScaleToFit(
    bool enable) /*= 0*/ {};

  /** Description:
    Sets the current drawing *thickness*.
    Arguments:
    thickness (I) Thickness.
  */
  virtual void setThickness(
    double thickness) = 0;

  /** Description:
    Sets the current drawing plotstyle.

    Arguments:
    plotStyleNameType (I) Plot Style Name Type.
    plotStyleId (I) Pointer to the object ID of the plot style.
    
    Remarks:
    plotStyleId is used only when plotStyleNameType == kPlotStyleNameById.
    
    plotStyleNameType will be one of the following:
    
    @table
    Name                           Value                         
    kPlotStyleNameByLayer          0
    kPlotStyleNameByBlock          1
    kPlotStyleNameIsDiceDefault    2
    kPlotStyleNameById             3
  */
  virtual void setPlotStyleName(
    OdDb::PlotStyleNameType plotStyleNameType, 
    OdDbStub* plotStyleId = 0) = 0;

  /** Description:
    Sets the current drawing *material*.
    Arguments:
    materialId (I) Pointer to the object ID of the *material*.
  */
  virtual void setMaterial(
    OdDbStub* materialId) = 0;
  

  /** Description:
    Returns the current drawing *color* index (ACI).

    Remarks:
    color will return one of the following:
    
    @table
    Name              Value   Description
    kACIbyBlock       0       ByBlock.
    kACIforeground    7       Foreground *color*.
    kACIbyLayer       256     ByLayer. 
    kACIRed           1       Red. 
    kACIYellow        2       Yellow. 
    kACIGreen         3       Green. 
    kACICyan          4       Cyan. 
    kACIBlue          5       Blue. 
    kACIMagenta       6       Magenta. 
    ..                8-255   Defined by AcadPalette.h
  */
  virtual OdUInt16 color() const = 0;

  /** Description:
    Returns the current drawing *color* as an OdCmEntityColor instance.
  */
  virtual OdCmEntityColor trueColor() const = 0;

  /** Description:
      Returns a pointer to the current drawing *layer*.
  */
  virtual OdDbStub* layer() const = 0;

  /** Description:
      Returns a pointer to the current drawing linetype.
  */
  virtual OdDbStub* lineType() const = 0;

  /** Description:
    Returns the current drawing fill type.
      
    Remarks:
    fillType will return one of the following:
    
    @table
    Name              Value   Description
    kOdGiFillAlways   1       Fill closed graphic primitives.
    kOdGiFillNever    2       Don't fill closed graphic primitives.
       
    Closed graphic primitives consist of the following:
    
    o  arcs with (OdGiArcType == kOdGiArcSector) || (OdGiArcType == kOdGiArcChord) 
    o  circles 
    o  meshes 
    o  polygons 
    o  shells 
 */
  virtual OdGiFillType fillType() const = 0;

  /** Description:
    Returns the *normal* to the current drawing fill plane.
    
    Arguments:
    normal (O) Receives the *normal*.
    
    Note:
    As implemented, this function does nothing but return false.
    It will be fully implemented in a future *release*.
  */
  virtual bool fillPlane(
    OdGeVector3d& normal) { return false; }

  /** Description:
    Returns the current drawing LineWeight.
  */
  virtual OdDb::LineWeight lineWeight() const = 0;

  /** Description:
    Returns the current drawing LineType scale.
  */
  virtual double lineTypeScale() const = 0;

  /** Description:
    Returns the current drawing *thickness*.
  */
  virtual double thickness() const = 0;

  /** Description:
    Returns the current PlotStyleName type.
    
    Remarks:
    plotStyleNameType will return one of the following:
    
    @table
    Name                           Value                         
    kPlotStyleNameByLayer          0
    kPlotStyleNameByBlock          1
    kPlotStyleNameIsDiceDefault    2
    kPlotStyleNameById             3
  */
  virtual OdDb::PlotStyleNameType plotStyleNameType() const = 0;

  /** Description:
    Returns a pointer to the the current PlotStyle object ID.
  */
  virtual OdDbStub* plotStyleNameId() const = 0;

  /** Description:
    Returns a pointer to the object ID of the *material*.
  */
  virtual OdDbStub* material() const = 0;
};

typedef OdSmartPtr<OdGiSubEntityTraits> OdGiSubEntityTraitsPtr;

#include "DD_PackPop.h"

#endif //_OD_GI_H_

