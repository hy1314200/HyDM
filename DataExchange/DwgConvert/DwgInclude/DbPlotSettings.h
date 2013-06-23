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



#ifndef OD_DBPLOTSETTINGS_H
#define OD_DBPLOTSETTINGS_H

#include "DD_PackPush.h"

#include "DbObject.h"
#include "Ge/GePoint2d.h"

/** Description:
    This class represents PlotSettings objects in an OdDbDatabase.
    
    Library:
    Db
   
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbPlotSettings : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbPlotSettings);

  OdDbPlotSettings();

  enum PlotPaperUnits
  {
    kInches         = 0,   // Inches
    kMillimeters    = 1,   // Millimeters
    kPixels         = 2    // Pixels
  };

  enum PlotRotation
  {
    k0degrees       = 0,   // No rotation
    k90degrees      = 1,   // 90° CCW
    k180degrees     = 2,   // Inverted
    k270degrees     = 3    // 90° CW
  };

  enum PlotType
  {
    kDisplay        = 0,   // Display
    kExtents        = 1,   // Extents
    kLimits         = 2,   // Limits
    kView           = 3,   // View
    kWindow         = 4,   // Window
    kLayout         = 5    // Layout
  };

  enum StdScaleType
  {
    kScaleToFit     = 0,   // Scaled to Fit
    k1_128in_1ft    = 1,   // 1/128" = 1'
    k1_64in_1ft     = 2,   // 1/64" = 1'
    k1_32in_1ft     = 3,   // 1/32" = 1'
    k1_16in_1ft     = 4,   // 1/16" = 1'
    k3_32in_1ft     = 5,   // 3/32" = 1'
    k1_8in_1ft      = 6,   // 1/8" = 1'
    k3_16in_1ft     = 7,   // 3/16" = 1'
    k1_4in_1ft      = 8,   // 1/4" = 1'
    k3_8in_1ft      = 9,   // 3/8" = 1'
    k1_2in_1ft      = 10,  // 1/2" = 1'
    k3_4in_1ft      = 11,  // 3/4" = 1'
    k1in_1ft        = 12,  // 1" = 1'
    k3in_1ft        = 13,  // 3" = 1'
    k6in_1ft        = 14,  // 6" = 1'
    k1ft_1ft        = 15,  // 1' = 1'
    k1_1            = 16,  // 1:1
    k1_2            = 17,  // 1:2
    k1_4            = 18,  // 1:4
    k1_8            = 19,  // 1:8
    k1_10           = 20,  // 1:10
    k1_16           = 21,  // 1:16
    k1_20           = 22,  // 1:20
    k1_30           = 23,  // 1:30
    k1_40           = 24,  // 1:40
    k1_50           = 25,  // 1:50
    k1_100          = 26,  // 1:100
    k2_1            = 27,  // 2:1
    k4_1            = 28,  // 4:1
    k8_1            = 29,  // 8:1
    k10_1           = 30,  // 10:1
    k100_1          = 31,  // 100:1
    k1000_1         = 32   // 1000:1
  };

  enum ShadePlotType 
  {
    kAsDisplayed    = 0, // As displayed
    kWireframe      = 1, // Wireframe
    kHidden         = 2, // Hidden lines removed
    kRendered       = 3// Rendered
  };

  enum ShadePlotResLevel 
  {
    kDraft          = 0, // Draft
    kPreview        = 1, // Preview
    kNormal         = 2, // Normal
    kPresentation   = 3, // Presentation
    kMaximum        = 4, // Maximum
    kCustom         = 5  // Custom
  };

  /** Description:
    Adds this PlotSettings object PlotSettings dictionary in the specified *database*.

    Arguments:
    pDb (I) Pointer to the *database*.
  */
  void addToPlotSettingsDict(
    OdDbDatabase *pDb);

  /** Description:
    Returns the name of this PlotSettings object (DXF 1).
  */
  OdString getPlotSettingsName() const;

  /** Description:
    Sets the name of this PlotSettings object (DXF 1).
    
    Arguments:
    plotSettingsName (I) PlotSettings name.  
  */
  void setPlotSettingsName(
    const OdChar* plotSettingsName);

  /** Description:
    Returns the name of the system printer or plot configuration file for this PlotSettings object (DXF 2).
  */
  OdString getPlotCfgName() const;

  /** Description:
    Returns the margins of the paper for this PlotSettings object.
    
    Arguments:
    leftMargin (O) Receives the left margin. 
    bottomMargin(O) Receives the bottom margin.
    rightMargin (O) Receives the right margin. 
    topMargin (O) Receives the top margin.
    
    Remarks:
    Margins are measured from their respective edges to the plot area, and are independent of plot rotation.
    
    Note:
    All distances are returned in millimeters, regardless of the drawing units. 
  */
  void getPlotPaperMargins(
    double& leftMargin, 
    double& bottomMargin,
    double& rightMargin, 
    double& topMargin) const;

  /** Description:
    Returns the size of the paper for this PlotSettings object (DXF 44, 45).

    Remarks:
    Paper size are the physical paper size, and includes the margins.

    Note:
    All distances are returned in millimeters, regardless of the drawing units. 
  */
  void getPlotPaperSize(
    double& paperWidth, 
    double& paperHeight) const;

  /** Description:
    Returns the canonical (locale-independent) name of the paper for this PlotSettings object (DXF 4).
    
    See Also:
    OdDbPlotSettingsValidator::getLocaleMediaName()
  */
  OdString getCanonicalMediaName() const;

  /** Description:
    Returns the plot origin offset for this PlotSettings object (DXF 46, 47).

    Arguments:
    xCoordinate (O) Receives the x-coordinate of the origin offset.
    yCoordinate (O) Receives the y-coordinate of the origin offset.
    
    Remarks:
    The plot origin offset is measured with respect to the plot margin.
     
    Note:
    All distances are returned in millimeters, regardless of the drawing units. 
  */
  void getPlotOrigin(
    double& xCoordinate, 
    double& yCoordinate) const;

  /** Description:
    Returns the plot paper units for this PlotSettings object (DXF 72).

    Remarks:
    The plot paper units determine the units of the margins, offsets, paper size, and drawing units.
    
    plotPaperUnits will return one of the following:
    
    @table
    Name            Value   Description
    kInches         0       Inches
    kMillimeters    1       Millimeters
    kPixels         2       Pixels
  */
  OdDbPlotSettings::PlotPaperUnits plotPaperUnits() const;

  /** Description:
    Returns the plotting of viewport borders for this PlotSettings object (DXF 70, bit 0x01).
    
    Remarks:
    Returns true if and only if viewport borders are to be plotted.
  */
  bool plotViewportBorders() const;

  /** Description:
    Controls the plotting of viewport borders for this PlotSettings object (DXF 70, bit 0x01).

    Arguments:
    plotViewportBorders (I) True if and only if viewport borders are to be plotted.
  */
  void setPlotViewportBorders(
    bool plotViewportBorders);

  /** Description:
    Returns the plotting of plotstyles for this PlotSettings object (DXF 70, bit 0x20).
      
    Remarks:
    Returns true if and only if plot styles are to be plotted.
  */
  bool plotPlotStyles() const;

  /** Description:
    Controls the plotting of plotstyles for this PlotSettings object (DXF 70, bit 0x20).
    
    Arguments:
    plotPlotStyles (I) True if and only if plot styles are to be plotted.
  */
  void setPlotPlotStyles(
    bool plotPlotStyles);

  /** Description:
    Returns the showing of plotstyles for this PlotSettings object during layout mode (DXF 70, bit 0x02).
  */
  bool showPlotStyles() const;

  /** Description:
    Controls the showing of plotstyles for this PlotSettings object during layout mode (DXF 70, bit 0x02).

    Arguments:
    showPlotStyles (I) True if and only if plot styles are to be shown.
  */
  void setShowPlotStyles(
    bool showPlotStyles);

  /** Description:
    Returns the plot rotation for this PlotSettings object (DXF 73).
    
    Remarks:
    plotRotation will return one of the following:
    
    @table
    Name         Value    Description
    k0degrees    0        No rotation
    k90degrees   1        90° CCW
    k180degrees  2        Inverted°
    k270degrees  3        90° CW
  */
  OdDbPlotSettings::PlotRotation plotRotation() const;

  /** Description:
    Returns true if an only if the plot is to be centered for this PlotSettings object (DXF 70, bit 0x04).
  */
  bool plotCentered() const;

  /** Description:
    Returns true if and only if the hidden line removal algorithm 
    is to be applied to PaperSpace entities for this PlotSettings object (DXF 70, bit 0x08). 
  */
  bool plotHidden() const;

  /** Description:
    Controls the application of the the hidden line removal algorithm 
    to PaperSpace entities for this PlotSettings object (DXF 70, bit 0x08). 

    Arguments:
    plotHidden (I) True if and only hidden lines are to be removed.
  */
  void setPlotHidden(bool plotHidden);

  /** Description:
    Returns the plot type for this PlotSettings Object (DXF 74).

    Remarks:
    plotType will return one of the following:
    
    @table
    Name        Value   Description
    kDisplay    0       Display
    kExtents    1       Extents
    kLimits     2       Limits
    kView       3       View
    kWindow     4       Window
    kLayout     5       Layout
  */
  OdDbPlotSettings::PlotType plotType() const;

  /** Description:
    Returns the corners of the plot window area for this PlotSettings object (DXF 48, 49, 140, 141).

    Arguments:
    xMin (I) Receives the x-coordinate of the lower-left corner.      
    yMin (I) Receives the y-coordinate of the lower-left corner.      
    xMax (I) Receives the x-coordinate of the upper-right corner.      
    yMax (I) Receives the y-coordinate of the upper-right corner.      
    
    Remarks:
    The corners define the area to be plotted if and only if plotType == kWindow.
  */
  void getPlotWindowArea(
    double& xMin, 
    double& yMin,
    double& xMax, 
    double& yMax) const;

  /** Description:
    Returns the plot view name for this PlotSettings object (DXF 6).

    Remarks:
    This string specifies the named view to be plotted if and only if plotType == kView. 
  */
  OdString getPlotViewName() const;

  /** Description:
    Returns true if and only if this PlotSettings object uses a standard *scale* (DXF 70, bit 0x10).
  */
  bool useStandardScale() const;

  /** Description:
    Returns the custom print *scale* for this PlotSettings object (DXF 142, 143).
    
    Arguments:
    numerator (I) Receives the PaperSpace units.
    denominator (I) Receives the media units.
    
    Remarks:  
    The custom print *scale* always reflects the *scale* that this PlotSettings object will use to plot.
  */
  void getCustomPrintScale(
    double& numerator, 
    double& denominator) const;

  /** Description:
    Returns the current style sheet (DXF 7).
  */
  OdString getCurrentStyleSheet() const;

  /** Description:
    Returns the standard *scale* type for this PlotSettings object (DXF 75).
    
    Remarks:
    stdScaleType will return one of the of the following
    
    @table
    Name            Value   Scale
    kScaleToFit     0       Scaled to Fit
    k1_128in_1ft    1       1/128=1'
    k1_64in_1ft     2       1/64=1'
    k1_32in_1ft     3       1/32=1'
    k1_16in_1ft     4       1/16=1'
    k3_32in_1ft     5       3/32=1'
    k1_8in_1ft      6       1/8=1'
    k3_16in_1ft     7       3/16=1'
    k1_4in_1ft      8       1/4=1'
    k3_8in_1ft      9       3/8=1'
    k1_2in_1ft      10      1/2=1'
    k3_4in_1ft      11      3/4=1'
    k1in_1ft        12      1=1'
    k3in_1ft        13      3=1'
    k6in_1ft        14      6=1'
    k1ft_1ft        15      1'=1'
    k1_1            16      1:1
    k1_2            17      1:2
    k1_4            18      1:4
    k1_8            19      1:8
    k1_10           20      1:10
    k1_16           21      1:16
    k1_20           22      1:20
    k1_30           23      1:30
    k1_40           24      1:40
    k1_50           25      1:50
    k1_100          26      1:100
    k2_1            27      2:1
    k4_1            28      4:1
    k8_1            29      8:1
    k10_1           30      10:1
    k100_1          31      100:1
    k1000_1         32      1000:1

  */
  OdDbPlotSettings::StdScaleType stdScaleType() const;


  /** Description:
    Returns the standard *scale*, as a floating point value, for this PlotSettings object (DXF 147).
    
    Arguments:
    standardScale (O) Receives the standard *scale*.
  */
  void getStdScale(
    double& standardScale) const;

  /** Description:
    Returns true if an only if lineweights are scaled for this PlotSettings object (DXF 70, bit 0x40).
  */
  bool scaleLineweights() const;

  /** Description:
    Controls the scaling of lineweights for this PlotSettings object (DXF 70, bit 0x40).

    Arguments:
    scaleLineweights (I) True if and only if lineweights are to be scaled.
  */
  void setScaleLineweights(
    bool scaleLineweights);

  /** Description:
    Returns true if an only if lineweights are printed for this PlotSettings object (DXF 70, bit 0x80).
  */
  bool printLineweights() const;

  /** Description:
    Controls the printing of lineweights are scaled for this PlotSettings object (DXF 70, bit 0x80).

    Arguments:
    printLineweights (I) True if and only if lineweights are to be printed.
  */
  void setPrintLineweights(bool printLineweights);

  /** Description:
      TBC.  
  bool textFill() const;

  */

  /** Description:
      TBC.
  void setTextFill(bool textFill);
  */

  /** Description:
      TBC.
  int getTextQuality() const;
  */

  /** Description:
      TBC.
  void setTextQuality(int quality);
  */

  /** Description:
    Returns true if and only if viewports are to be plotted before other objects in PaperSpace
    for this PlotSettings object (DXF 70, bit 0x200).
  */
  bool drawViewportsFirst() const;

  /** Description:
    Controls the plotting of viewports before other other objects in in PaperSpace
    for this PlotSettings object (DXF 70, bit 0x200).
    
    Arguments:
    drawViewportsFirst (I) True if and only if viewports are to be plotted first.
  */
  void setDrawViewportsFirst(
    bool drawViewportsFirst);

  /** Description:
    Returns true if and only if this PlotSettings object of ModelSpace type (DXF 70, bit 0x400).
    
    Remarks:
    A PlotSettings object is of either ModelSpace type or PaperSpace type.
  */
  bool modelType() const;

  /** Description:
    Controls the ModelSpace type of this PlotSettings object (DXF 70, bit 0x400).
    
    Arguments:
    modelType (I) True if and only if it is of ModelSpace type.
    
    Remarks:
    A PlotSettings object is of either ModelSpace type or PaperSpace type.
  */
  void setModelType(bool modelType);

  /** Description:
    Returns the top margin of the paper for this PlotSettings object (DXF 43).
  */
  double getTopMargin() const;

  /** Description:
    Returns the right margin of the paper for this PlotSettings object (DXF 42).
  */
  double getRightMargin() const;

  /** Description:
    Returns the bottom margin of the paper for this PlotSettings object (DXF 41).
  */
  double getBottomMargin() const;

  /** Description:
    Returns the left margin of the paper for this PlotSettings object (DXF 40).
  */
  double getLeftMargin() const;

  /** Description:
    Returns the paper image origin of the paper for this PlotSettings object (DXF 148, 149).
  */
  OdGePoint2d getPaperImageOrigin() const;

  /** Description:
    Sets the paper image origin (DXF 148, 149).
    Arguments:
    paperImageOrigin (I) Paper image origin. 
  */
  void setPaperImageOrigin(OdGePoint2d paperImageOrigin);

  // New to acad2004 api

  /** Description:
    Returns the shade plot type for this PlotSettings object (DXF 76).
    
    Remarks:
    shadePlotType will return one of the following:
    
    @table
    Name           Value  Description
    kAsDisplayed   0      As displayed
    kWireframe     1      Wireframe
    kHidden        2      Hidden lines removed
    kRendered      3      Rendered
    
  */
  OdDbPlotSettings::ShadePlotType shadePlot() const;

  /** Description:
    Sets the shade plot type for this PlotSettings object (DXF 76)
    
    Arguments:
    shadePlotType (I) Shade Plot type.

    Remarks:
    shadePlotType will be one of the following:
    
    @table
    Name           Value  Description
    kAsDisplayed   0      As displayed
    kWireframe     1      Wireframe
    kHidden        2      Hidden lines removed
    kRendered      3      Rendered
    
  */
  void setShadePlot(
    OdDbPlotSettings::ShadePlotType shadePlot);

  /** Description:
    Returns the shade plot resolution level for this PlotSettings object.
    
    Remarks:
    shadePlotResLevel controls the resolution at which shaded and 
    vectorized viewports will plot.
    
    shadePlotResLevel will return one of the following:
    
    @table
    Name            Value   Description
    kDraft          0       Draft
    kPreview        1       Preview
    kNormal         2       Normal
    kPresentation   3       Presentation
    kMaximum        4       Maximum
    kCustom         5       Custom

  */
  OdDbPlotSettings::ShadePlotResLevel shadePlotResLevel() const;

  /** Description:
    Sets the shade plot resolution level for this PlotSettings object.
    
    Arguments:
    shadePlotResLevel (I) Shade plot resolution level.
    
    Remarks:
    shadePlotResLevel controls the resolution at which shaded and 
    vectorized viewports will plot.

    shadePlotResLevel will be one of the following:
    
    @table
    Name            Value   Description
    kDraft          0       Draft
    kPreview        1       Preview
    kNormal         2       Normal
    kPresentation   3       Presentation
    kMaximum        4       Maximum
    kCustom         5       Custom

  */
  void setShadePlotResLevel(
    OdDbPlotSettings::ShadePlotResLevel shadePlotResLevel);

  /** Description:
    Returns the shade plot custom DPI for this PlotSettings object.
    
    Remarks:
    shadePlotCustomDPI specifies the custom resolution at which shaded and 
    vectorized viewports will plot if shadePlotResLevel == ShadePlotResLevel::kCustom.
    
    Note:
    shadePlotCustomDPI should be used only if an only if the shade plot resolution level 
    is set to ShadePlotResLevel::kCustom.
  */
  OdInt16 shadePlotCustomDPI() const;

  /** Description:
    Sets the shade plot custom DPI for this PlotSettings object.
    
    Remarks:
    shadePlotCustomDPI specifies the custom resolution at which shaded and 
    vectorized viewports will plot if shadePlotResLevel == ShadePlotResLevel::kCustom.
    
    Note:
    shadePlotCustomDPI should be used only if and only if the shade plot resolution level 
    is set to ShadePlotResLevel::kCustom.
  
    Arguments:
    shadePlotCustomDPI (I) Shade plot custom DPI.
  */
  void setShadePlotCustomDPI(
    OdInt16 shadePlotCustomDPI);

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

  virtual void subClose();

  virtual OdResult subErase(
    bool erasing);
  
  virtual void subHandOverTo(
    OdDbObject* newObject);

  virtual void copyFrom(
    const OdRxObject* pSource);

};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbPlotSettings object pointers.
*/
typedef OdSmartPtr<OdDbPlotSettings> OdDbPlotSettingsPtr;

#include "DD_PackPop.h"

#endif


