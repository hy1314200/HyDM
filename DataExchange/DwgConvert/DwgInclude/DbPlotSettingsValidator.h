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



#ifndef OD_DBPLOTSETVAL_H
#define OD_DBPLOTSETVAL_H

//////////////////////////////////////////////////////////////////////////////
//
// Name:    dbplotsetval.h
//
// Remarks: This class exports access methods which validate data on an
//          OdDbPlotSettings object before actually setting the data on
//          the object.
//
//////////////////////////////////////////////////////////////////////////////

#include "RxObject.h"
#include "DbPlotSettings.h"
#include "OdArray.h"

class OdDbPlotSettings;

typedef OdDbPlotSettings::PlotPaperUnits PlotPaperUnits;
typedef OdDbPlotSettings::PlotRotation   PlotRotation;
typedef OdDbPlotSettings::PlotType       PlotType;
typedef OdDbPlotSettings::StdScaleType   StdScaleType;

/** Description:
    This class implements access methods that validate data for
    OdDbPlotSettings objects prior to setting the data on the
    objects.
    
    Library:
    Db
    
    Remarks:
    Plot device and style lists should be refreshed with refreshLists() prior to
    using the other methods in this class, in case any plot devices, PC3 files, or plot style tables were changed
    after starting your application.
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbPlotSettingsValidator : public OdRxObject
{
public:
  /** Description:
    Sets the plot device and media names for the specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    plotDeviceName (I) Name of the system printer or PC3 file.
    mediaName (I) Media name.
    
    Note:
    (plotDeviceName == none_device) requires (mediaName == none_user_media)
  */
  virtual void setPlotCfgName(
    OdDbPlotSettings* pPlotSettings,
    const char* plotDeviceName,
    const char* mediaName = NULL) = 0;

  /** Description:
    Sets the canonical (locale-independent) media name for the specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    mediaName (I) Canonical media name.
    
    Remarks:
    The canonical media name is locale-independent.
    
    See Also:
    getLocaleMediaName()

    Note:
    Canonical media names are case-sensitive.
  */
  virtual bool setCanonicalMediaName(
    OdDbPlotSettings* pPlotSettings,
    const char* mediaName) = 0;

  /** Description:
    Sets the plot origin offset for the specified PlotSettings object.

    Arguments:
    xCoordinate (O) X-coordinate of the origin offset.
    yCoordinate (O) Y-coordinate of the origin offset.
    
    Remarks:
    The plot origin offset is measured with respect to the plot margin.
     
    Note:
    All distances are returned in millimeters, regardless of the drawing units. 
  */
  virtual void setPlotOrigin(
    OdDbPlotSettings *pPlotSettings,
    double xCoordinate,
    double yCoordinate) = 0;


  /** Description:
    Sets the plot paper units for the specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    plotPaperUnits (I) Plot paper units.
        
    Remarks:
    The plot paper units determine the units of the margins, offsets, paper size, and drawing units.
    
    plotPaperUnits will be one of the following:
    
    @table
    Name            Value   Description
    kInches         0       Inches
    kMillimeters    1       Millimeters
    kPixels         2       Pixels
  */
  virtual void setPlotPaperUnits(
    OdDbPlotSettings* pPlotSettings,
    const PlotPaperUnits plotPaperUnits) = 0;


  /** Description:
    Sets the plot rotation for specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    plotRotation (I) Plot rotation.
    
    Remarks:
    plotRotation will be one of the following:
    
    @table
    Name         Value    Description
    k0degrees    0        No rotation
    k90degrees   1        90° CCW
    k180degrees  2        Inverted°
    k270degrees  3        90° CW
  */
  virtual void setPlotRotation(
    OdDbPlotSettings* pPlotSettings,
    const PlotRotation plotRotation) = 0;

  /** Description:
    Controls the centering of the plot for the specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    plotCentered (I) Plot centered.
  */
  virtual void setPlotCentered(
    OdDbPlotSettings* pPlotSettings,
    bool plotCentered) = 0;

  /** Description:
    Sets the plot type for the specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    plotType (I) Plot type.

    Remarks:
    plotType will be one of the following:
    
    @table
    Name        Value   Description
    kDisplay    0       Display
    kExtents    1       Extents
    kLimits     2       Limits
    kView       3       View
    kWindow     4       Window
    kLayout     5       Layout
  */
  virtual void setPlotType(
    OdDbPlotSettings* pPlotSettings,
    const PlotType plotType) = 0;

  /** Description:
    Sets the corners of the plot window area for the specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    xMin (I) X-coordinate of the lower-left corner.      
    yMin (I) Y-coordinate of the lower-left corner.      
    xMax (I) X-coordinate of the upper-right corner.      
    yMax (I) Y-coordinate of the upper-right corner.      
    
    Remarks:
    The corners define the area to be plotted if and only if plotType == kWindow.
  */
  virtual void setPlotWindowArea(
    OdDbPlotSettings* pPlotSettings,
    double xMin,
    double yMin,
    double xMax,
    double yMax) = 0;

  /** Description:
    Sets the plot view name for the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    plotViewName (I) Plot view name.
  
    Remarks:
    This string specifies the named view to be plotted if and only if plotType == kView. 
  */
  virtual void setPlotViewName(
    OdDbPlotSettings* pPlotSettings,
    const char* plotViewName) = 0;

  /** Description:
    Controls the use of a standard *scale* for the specified PlotSettings object.

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    useStandardScale (I) Use standard scale.
  */
  virtual void setUseStandardScale(
    OdDbPlotSettings* pPlotSettings,
    bool useStandardScale) = 0;

  /** Description:
    Sets the custom print *scale* for the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    numerator (I) Receives the PaperSpace units.
    denominator (I) Receives the media units.
    
    Remarks:  
    The custom print *scale* always reflects the *scale* that this PlotSettings object will use to plot.
  */
  virtual void setCustomPrintScale(
    OdDbPlotSettings* pPlotSettings,
    double numerator,
    double denominator) = 0;

  /** Description:
    Sets the current style sheet for the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    currentStyleSheet (I) Name of current style sheet.
  */
  virtual void setCurrentStyleSheet(
    OdDbPlotSettings* pPlotSettings,
    const char* currentStyleSheet) = 0;

  /** Description:
    Sets the standard *scale* type for the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    stdScaleType (I) Name of current style sheet.

    Remarks:
    stdScaleType will be one of the of the following
    
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
  virtual void setStdScaleType(
    OdDbPlotSettings* pPlotSettings,
    const StdScaleType stdScaleType) = 0;

  /** Description:
    Sets the standard *scale*, as a floating point value, for the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    standardScale (I) Standard scale.
  */
  virtual void setStdScale(
    OdDbPlotSettings* pPlotSettings,
    double standardScale) = 0;
  
  /** Description:
    Returns an array of all available system printers and PC3 files.
    
    Remarks:
    Plot device and style lists should be refreshed with refreshLists() prior to
    using the other methods in this class, in case any plot devices, PC3 files, or plot style tables were changed
    after starting your application.
    
    Arguments:
    deviceList (O) Receives the plot device list.
  */
  virtual void plotDeviceList(
    OdArray<const char*> & deviceList) = 0;

  /** Description:
    Returns an array canonical (locale-independent) media names for the specified
    PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    mediaList (O) Receives the media name list.

    Remarks:
    Plot device and style lists should be refreshed with refreshLists() prior to
    using the other methods in this class, in case any plot devices, PC3 files, or plot style tables were changed
    after starting your application.
  */
  virtual void canonicalMediaNameList(
    OdDbPlotSettings* pPlotSettings,
    OdArray<const char*> & mediaList) = 0;

  /** Description:
    Returns the locale-dependent media name for the specified PlotSettings object and canonical media name or index.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    canonicalName (O) Locale-independent media name.
    
    Remarks:
    Plot device and style lists should be refreshed with refreshLists() prior to
    using the other methods in this class, in case any plot devices were added or removed
    after starting your application.
  */
  virtual OdString getLocaleMediaName(
    OdDbPlotSettings *pPlotSettings,
    const char*  canonicalName) = 0;

  /** Arguments:
    mediaIndex (I) Media index.
  */
  virtual OdString getLocaleMediaName(
    OdDbPlotSettings *pPlotSettings,
    int mediaIndex) = 0;

  /** Description:
    Sets the media name closest to the specified parameters for the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    paperWidth (I) Paper width.
    paperHeight (I) Paper height.
    plotPaperUnits (I) Plot paper units.
    matchPrintableArea (I) True to match against printable area or physical media size.

    Remarks:
    The plot paper units determine the units of the margins, offsets, paper size, and drawing units.

    plotPaperUnits will be one of the following:
    
    @table
    Name            Value   Description
    kInches         0       Inches
    kMillimeters    1       Millimeters
    kPixels         2       Pixels
  */ 
  virtual void setClosestMediaName(
    OdDbPlotSettings* pPlotSettings,
    double paperWidth,
    double paperHeight,
    const PlotPaperUnits plotPaperUnits,
    bool matchPrintableArea) = 0;

  // Not very good function
  
  /** Description:
    Sets the media size, margins, and units for the specified PlotSettings object. 

    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    mediaName (I) Media name.
    paperWidth (I) Paper width.
    paperHeight (I) Paper height.
    plotPaperUnits (I) Plot paper units.
    leftMargin (O) Left margin. 
    bottomMargin(O) Bottom margin.
    rightMargin (O) Right margin. 
    topMargin (O) Top margin.


    Remarks:
    The plot paper units determine the units of the margins, offsets, paper size, and drawing units.
        
    plotPaperUnits will be one of the following:
    
    @table
    Name            Value   Description
    kInches         0       Inches
    kMillimeters    1       Millimeters
    kPixels         2       Pixels
  */
  virtual void setMediaSize(
    OdDbPlotSettings* pPlotSettings,
    OdString paperSize,
    double paperWidth,
    double paperHeight,
    const PlotPaperUnits plotPaperUnits,
    double leftMargin,
    double topMargin,
    double rightMargin,
    double bottomMargin) = 0;

  /** Description:
    Returns an array of all available plot style tables.
    
    Remarks:
    Plot device and style lists should be refreshed with refreshLists() prior to
    using the other methods in this class, in case any plot devices, PC3 files, or plot style tables were changed
    after starting your application.
    
    Arguments:
    styleList (O) Receives the plot style list.
  */
  virtual void plotStyleSheetList(
    OdArray<const char*> & styleList) = 0;

  /** Description:
    Refreshes the plot device and style lists for the specified PlotSettings object.
    
    Remarks:
    Plot device and style lists should be refreshed with refreshLists() prior to
    using the other methods in this class, in case any plot devices, PC3 files, or plot style tables were changed
    after starting your application.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
  */
  virtual void refreshLists(
    OdDbPlotSettings* pPlotSettings) = 0;


  /** Description:
    Controls the Zoom to Paper on Update setting for the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
    zoomToPaperOnUpdate (I) Zoom to Paper on Update.
  */
  virtual void setZoomToPaperOnUpdate(
    OdDbPlotSettings* pPlotSettings,
    bool zoomToPaperOnUpdate) = 0;

  /** Description:
    Applies the default plot configuration settings to the specified PlotSettings object.
    
    Arguments:
    pPlotSettings (I) Pointer to the PlotSettings object.
  */
  virtual void setDefaultPlotConfig(
    OdDbPlotSettings* pPlotSettings) = 0;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbPlotSettingsValidator object pointers.
*/
typedef OdSmartPtr<OdDbPlotSettingsValidator> OdDbPlotSettingsValidatorPtr;

#endif // OD_DBPLOTSETVAL_H


