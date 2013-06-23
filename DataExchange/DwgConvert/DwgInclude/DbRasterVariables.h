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



#ifndef __IMGVARS_H
#define __IMGVARS_H /* {Secret} */

#include "DD_PackPush.h"

class OdDbRasterVariables;

#include "DbRasterImageDef.h"



typedef OdSmartPtr<OdDbRasterVariables> OdDbRasterVariablesPtr;

/** Description:

    Represents a raster variables object in an OdDbDatabase, which contains settings that apply
    to all image entities in the database.

    {group:OdDb_Classes}
*/
/** Description:
    This class represents RasterVariables objects in an OdDbDatabase.
    
    Library:
    Db
   
    Remarks:
    RasterVariables objects contain settings applicable to raster images.   
    A single instance of this class is stored with every OdDbDatabase that contains raster images.
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRasterVariables : public OdDbObject
{
public:  
  
  ODDB_DECLARE_MEMBERS(OdDbRasterVariables);

  OdDbRasterVariables();
  
  enum FrameSettings
  {
    kImageFrameInvalid  = -1, // Invalid
    kImageFrameOff      = 0,  // Frame is off
    kImageFrameAbove    = 1,  // Frame is above the image
    kImageFrameBelow    = 2   // Frame is below the image
  };

  enum ImageQuality
  {
    kImageQualityInvalid  = -1, // Invalid
    kImageQualityDraft    = 0,  // Draft quality
    kImageQualityHigh     = 1   // High quality
  };
    
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields
    (OdDbDxfFiler* pFiler) const;
  
  /** Description:
    Returns the image frame display (DXF 70).
    
    Remarks:
    imageFrame will return one of the following:
    
    @table
    Name                  Value   Description
    kImageFrameInvalid    -1      Invalid
    kImageFrameOff        0       Frame is off
    kImageFrameAbove      1       Frame is above the image
    kImageFrameBelow      2       Frame is below the image
    
  */
  virtual FrameSettings imageFrame() const;

  /** Description:
    Sets the image frame display (DXF 70).
    
    Arguments:
    imageFrame (I) Image frame display.

    Remarks:
    imageFrame will be one of the following:
    
    @table
    Name                  Value   Description
    kImageFrameOff        0       Frame is off
    kImageFrameAbove      1       Frame is above the image
    kImageFrameBelow      2       Frame is below the image
  */
  virtual void setImageFrame( 
    FrameSettings imageFrame );

  /** Description:
    Returns the image display quality (DXF 71).

    Remarks:
    imageQuality will return one of the following:
    
    @table
    Name                    Value   Description
    kImageQualityInvalid    -1      Invalid
    kImageQualityDraft       0      Draft quality
    kImageQualityHigh        1      High quality
  */
  virtual ImageQuality imageQuality() const;

  /** Description:
    Sets the image display quality (DXF 71).
    Arguments:
    imageQuality (I) Image Quality.

    Remarks:
    imageQuality will be one of the following:
    
    @table
    Name                    Value   Description
    kImageQualityInvalid    -1      Invalid
    kImageQualityDraft       0      Draft quality
    kImageQualityHigh        1      High quality
  */
  virtual void setImageQuality(
    ImageQuality imageQuality );
  
  /** Description:
    Returns the real-world *units* corresponding to drawing units (DXF 72).

    Remarks:
    units will return one of the following:
    
    @table
    Name             Value
    kNone            0 
    kMillimeter      1 
    kCentimeter      2 
    kMeter           3 
    kKilometer       4 
    kInch            5 
    kFoot            6 
    kYard            7 
    kMile            8 
    kMicroinches     9 
    kMils            10 
    kAngstroms       11 
    kNanometers      12 
    kMicrons         13 
    kDecimeters      14 
    kDekameters      15 
    kHectometers     16 
    kGigameters      17 
    kAstronomical    18 
    kLightYears      19 
    kParsecs         20

  */
  virtual OdDbRasterImageDef::Units userScale() const;

  /** Description:
    Specifies the real-world *units* corresponding to drawing units  (DXF 72).
    
    Arguments:
    units (I) Real-world *units*.
    
    Remarks:
    units will be one of the following:
    
    @table
    Name             Value
    kNone            0 
    kMillimeter      1 
    kCentimeter      2 
    kMeter           3 
    kKilometer       4 
    kInch            5 
    kFoot            6 
    kYard            7 
    kMile            8 
    kMicroinches     9 
    kMils            10 
    kAngstroms       11 
    kNanometers      12 
    kMicrons         13 
    kDecimeters      14 
    kDekameters      15 
    kHectometers     16 
    kGigameters      17 
    kAstronomical    18 
    kLightYears      19 
    kParsecs         20
  */
  virtual void setUserScale(
    OdDbRasterImageDef::Units units);
  
  /** Description:
    Opens the RasterVariables object in the specified *database*.
    
    Arguments:
    openMode (I) Mode in which to open the RasterVariables object.
    pDb (I) Pointer to the *database* containg the RasterVariables object.


    Remarks:
    Creates a RasterVariables object if one does not exist.
    Returns a SmartPointer to the RasterVariables object.
  */
  static OdDbRasterVariablesPtr openRasterVariables(
    OdDbDatabase* pDb,
    OdDb::OpenMode openMode = OdDb::kForRead);
};

#include "DD_PackPop.h"

#endif // __IMGVARS_H


