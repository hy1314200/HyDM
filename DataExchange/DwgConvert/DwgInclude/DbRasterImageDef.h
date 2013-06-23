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



#ifndef __DBRASTERIMAGEDEF_H
#define __DBRASTERIMAGEDEF_H

#include "DD_PackPush.h"

// Forward declarations
//

class OdDbDictionary;

#include "DbObjectReactor.h"
#include "DbObject.h"
#include "Gi/GiRasterImage.h"
#include "Ge/GeVector2d.h"
#include "RxObjectImpl.h"

/** Description:
    Dummy implementation of OdGiRasterImage interface.
    Class is intended to be used to preset image parameters to 
    OdDbRasterImageDef object without actual raster file loading.

    {group:OdGi_Classes} 
*/
class TOOLKIT_EXPORT OdGiRasterImageDesc : public OdGiRasterImage
{
  OdUInt32      m_width,
                m_height;
  Units         m_units;
  double        m_xPelsPerUnut,
                m_yPelsPerUnut;
public:
  static OdGiRasterImagePtr createObject(OdUInt32 width, OdUInt32 height, Units units = kNone, const OdGeVector2d& defResolution = OdGeVector2d::kIdentity);
  
  OdRxObjectPtr clone() const
  {
    OdSmartPtr<OdGiRasterImageDesc> pRes = OdRxObjectImpl<OdGiRasterImageDesc>::createObject();
    pRes->m_width  = m_width;
    pRes->m_height = m_height;
    pRes->m_units  = m_units;
    pRes->m_xPelsPerUnut = m_xPelsPerUnut;
    pRes->m_yPelsPerUnut = m_yPelsPerUnut;
    return (OdRxObject*)pRes;
  }

  OdUInt32 pixelWidth() const;
  OdUInt32 pixelHeight() const;

  Units defaultResolution(double& xPelsPerUnut, double& yPelsPerUnut) const;

  // Dummy implementations, since it is needn't here
  
  /** Description:
      Retrieves the number of bits per pixel used for colors on the destination device or buffer.
  */
  virtual int colorDepth() const;

  /** Description:
      Returns the number of colors in the palette of the image.
  */
  virtual OdUInt32 numColors() const;

  /** Description:
      Returns the color by index from the palette of the image.
  */
  virtual ODCOLORREF color(OdUInt32 nIndex) const;

  virtual OdUInt32 paletteDataSize() const;

  /** Description:
      Returns the palette of the image in BMP format.
  */
  virtual void paletteData(OdUInt8* pBytes) const;

  /** Description:
      The number of bytes between the beginning of scan line N and
      the beginning of scan line N+1
  */
  virtual OdUInt32 scanLineSize() const;

  /** Description:
      Returns pointer on the scanline of the image by index without any stuff.

      Remarks:
      The number of accessible scanlines is equal to value 'height' (see pixelHeight() call).
      The number of accessible bytes  in the scanline is equal to scanLineSize() returning value.
      The scanline gotten by an index 0 is the first.
      The scanline gotten by an index (height - 1) is the last.
  */
  virtual void scanLines(OdUInt8* pBytes, OdUInt32 index, OdUInt32 numLines = 1) const;

  virtual const OdUInt8* scanLines() const;
  
  /** Description:
      applying Brightness, Contrast, Fade to the image's pixels  
  */
  virtual void applyBCF(double brightness, double contrast, double fade);

  /** Description:
      applying palette transformation to bitanal images.
      If image isn't bitonal - does nothing
  */
  virtual void applyBitonalPaletteTransform(ODCOLORREF traitsColor, ODCOLORREF bgColor);
};



/** Description:
    This class represents Raster Image Definition objects in an OdDbDatabase.
    
    Library:
    Db

    Remarks:
    Raster Image Definitions (OdDbRasterImageDef objects) work with Raster Image (OdDbRasterImage) entities) 
    in much the same way that BlockTable records (OdDbBlockTableRecord objects) work with Block References
    (OdDbBlockReference entities).

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRasterImageDef : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbRasterImageDef);

  typedef OdGiRasterImage::Units Units;

  OdDbRasterImageDef();
  
  OdResult subErase(
    bool erasing);
  virtual void subHandOverTo (
    OdDbObject* newObject);
  void subClose();

  OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  /** Description:
    Sets the *name* of the source file containing the raster image for this Raster Image Definition object (DXF 1).
    
    Arguments:
    pathName (I) Path *name*.
  */
  void setSourceFileName(const OdChar* pathName);

  /** Description:
    Returns the name of the source file containing the raster image for this Raster Image Definition object (DXF 1).
    
    Remarks:
    This function calls searchForActivePath() to determine the active path.
    
    Note:
    This OdDbRasterImageDef must be open for reading
  */
  OdString sourceFileName() const;

  /** Description:
    Loads the source image file for this Raster Image Definition object.
    
    Arguments:
    modifyDatabase (I) If and only if true, undo recording will be done for this object.

    Remarks:
    This function calls searchForActivePath() to determine the active path.
    
    If the image file is currently loaded, the file will not be read.
    
    "Lazy loading" implies that the image file will not be loaded until it is required.
    
  */
  bool load(
    bool modifyDatabase = true);

  /** Description:
    Unloads the image for this Raster Image Definition object. 
    
    Arguments:
    modifyDatabase (I) If and only if true, undo recording will be done for this object.

    Remarks:

    Note:
    This OdDbRasterImageDef must be open for writing.

  */
  void unload(
    bool modifyDatabase = true);

  /** Description:
    Returns true if and only if the image file for this Raster Image Definition object is loaded (DXF 280).
  */
  bool isLoaded() const;

  /** Description:
    Returns the XY pixel size of the image for this Raster Image Definition (DXF 10).
  */
  OdGeVector2d size() const;


  /** Description:
    Returns the default physical pixel size, in mm/pixel, of the image for this Raster Image Definition object (DXF 10).
    
    Note:
    If the image has no default pixel size, 
    this function returns 1.0/(image width in pixels) for XY resolutions.
  */
  OdGeVector2d resolutionMMPerPixel() const;

  /** Description:
      TBC.
  int entityCount(bool* pbLocked = NULL) const;
  void updateEntities() const;
  */

  /** Description:
    Returns the resolution units for the image for this Raster Image Definition object (DXF 281).
    
    Remarks:
    
  */
  OdGiRasterImage::Units resolutionUnits() const;

  /** Description:
      TBC.
  */
  OdGiRasterImagePtr image();

  /** Description:
      Create directly from in-memory image.
      pImage will be attached to this object.
      If pImage is not null sets 'loaded' flag to true.
  */
  void setImage(OdGiRasterImage* pImage, bool modifyDatabase = true);

  /** Description:
      Creates an image dictionary in the passed in database, if one is not already present.

      Arguments:
      pDb (I/O) Database in which to create the image dictionary.

      Return Value:
      The Object ID of the image dictionary in pDb (either the existing dictionary
      if one was already present, or the newly created dictionary).
  */
  static OdDbObjectId createImageDictionary(OdDbDatabase* pDb);

  /** Description:
      Returns the Object ID of the image dictionary in the passed in database.
  */
  static OdDbObjectId imageDictionary(OdDbDatabase* pDb);

  enum 
  { 
    kMaxSuggestNameSize = 2049 
  };

  /** Description:
      TBC.
  */
  static OdString suggestName(const OdDbDictionary* pImageDictionary, OdString pNewImagePathName);

  /*   comment this out for a while

  int colorDepth() const;
  OdGiSentScanLines* makeScanLines(
    const OdGiRequestScanLines* pReq,
    const OdGeMatrix2d& pixToScreen,
    OdGePoint2dArray* pActiveClipBndy = 0, // Data will be modified!
    bool draftQuality = false,
    bool isTransparent = false,
    const double brightness = 50.0,
    const double contrast = 50.0,
    const double fade = 0.0
    ) const;
  void setActiveFileName(const OdChar* pPathName);
  OdString searchForActivePath();
  OdString activeFileName() const;
  void embed();
  bool isEmbedded() const;
  OdString fileType() const;
  void setUndoStoreSize(unsigned int maxImages = 10);
  unsigned int undoStoreSize() const;
  bool imageModified() const;
  void setImageModified(bool modified);
  void loadThumbnail(OdUInt16& maxWidth, OdUInt16& maxHeight,
    OdUInt8* pPalette = 0, int nPaletteEntries = 0);
  void unloadThumbnail();
  void* createThumbnailBitmap(BITMAPINFO*& pBitmapInfo,
    OdUInt8 brightness = 50, OdUInt8 contrast = 50, OdUInt8 fade = 0);
  IeFileDesc* fileDescCopy() const;
  void getScanLines(OdGiBitmap& bitmap,
    double brightness = 50.0,
    double contrast = 50.0,
    double fade = 0.0) const;
  void openImage(IeImg*& pImage);
  void closeImage();
  static Oda::ClassVersion classVersion();
  RasterImageDefImp* ptrImp() const;
  RasterImageDefImp* setPtrImp(RasterImageDefImp* pImp);


private:
  RasterImageDefImp* mpImp;
     static Oda::ClassVersion mVersion;
  */
};

  /*   comment this out for a while
inline RasterImageDefImp*
OdDbRasterImageDef::ptrImp() const
{
  return mpImp;
}

inline RasterImageDefImp*
OdDbRasterImageDef::setPtrImp(RasterImageDefImp* pImp)
{
  RasterImageDefImp* oldImp=mpImp;
  mpImp=pImp;
  return oldImp;
}
  */



/** Description:
    This class implements Raster Image Definition Reactor objects in an OdDbDatabase.
    
    Library:
    Db
    
    Remarks:
    Raster Image Definition Reactor (OdDbRasterImageDefReactor) objects are used 
    to notify Raster Image (OdDbRasterImage) objects 
    of changes to their associated Raster Image Definition (OdDbRasterImage) objects.
    
    Modifications of Image Definition objects redraw their dependent Raster Image entities. 
    Deletion of Image Definition objects delete their dependent Raster Image entities.
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRasterImageDefReactor : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbRasterImageDefReactor);

  OdDbRasterImageDefReactor();

  OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  void erased(
    const OdDbObject* pObject, 
    bool erasing = true);

  void modified(
    const OdDbObject* pObject);

  enum DeleteImageEvent
  {
    kUnload = 1,
    kErase = 2
  };
  /** Description:
    Controls notification of OdDbRasterImage entities.
    
    Arguments:
  */
  static void setEnable(
    bool enable);

  /** Description:
    Notification function called whenever an OdDbRasterImageDef object is about to be unloaded or erased.
    
    Arguments:
    pImageDef (I) Pointer to the OdDbRasterImageDef object sending this notification.
    event (I) Event triggering the notification.
    cancelAllowed (I) True to *enable* user cancellation, false to *disable*.
    
    Remarks:
    Returns true if and only if not cancelled. 
    
    event will be one of the following:
    
    @table
    Name      Value
    kUnload   1
    kErase    2
    
    Note:
    Use OdDbRasterImageDef::imageModified() to determine if the Image Defintion has been modified.
  */
  virtual bool onDeleteImage( 
    const OdDbRasterImageDef* pImageDef,
    DeleteImageEvent event,
    bool cancelAllowed);

  /* coment this out for a while
     static Oda::ClassVersion classVersion();
private:

  OdDbRasterImageDefReactorImpl* mpImp;
     static Oda::ClassVersion mVersion;
  */
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbRasterImageDefReactor object pointers.
*/
typedef OdSmartPtr<OdDbRasterImageDefReactor> OdDbRasterImageDefReactorPtr;

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbRasterImageDefTransReactor : public OdDbObjectReactor
{
protected:
  OdDbRasterImageDefTransReactor() {}
public:
  ODRX_DECLARE_MEMBERS(OdDbRasterImageDefTransReactor);

  virtual bool onDeleteImage( const OdDbRasterImageDef* pImageDef,
                              OdDbRasterImageDefReactor::DeleteImageEvent event,
                              bool cancelAllowed ) = 0;
};

/*   comment this for a while

// TBC.
class TOOLKIT_EXPORT OdDbRasterImageDefFileAccessReactor : public OdDbObjectReactor
{
protected:
  OdDbRasterImageDefFileAccessReactor() {}
public:
  ODRX_DECLARE_MEMBERS(OdDbRasterImageDefFileAccessReactor);

  virtual void onAttach(const OdDbRasterImageDef*, const OdChar* pPath) = 0;
  virtual void onDetach(const OdDbRasterImageDef*, const OdChar* pPath) = 0;
  virtual bool onOpen(const OdDbRasterImageDef*, const OdChar* pPath,
    const OdChar* pActivePath, bool& replacePath, OdString& replacementPath) = 0;

  virtual bool onPathChange(const OdDbRasterImageDef*,
    const OdChar* pPath, const OdChar* pActivePath,
    bool& replacePath, OdString& replacementPath) = 0;

  virtual void onClose(const OdDbRasterImageDef*, const OdChar* pPath) = 0;

  virtual void onDialog(OdDbRasterImageDef*,
    const OdChar* pCaption, const OdChar* pFileExtensions) = 0;
};

inline Oda::ClassVersion
OdDbRasterImageDef::classVersion()
{ return mVersion; }

inline Oda::ClassVersion
OdDbRasterImageDefReactor::classVersion()
{ return mVersion; }
*/

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbRasterImageDef object pointers.
*/
typedef OdSmartPtr<OdDbRasterImageDef> OdDbRasterImageDefPtr;

#include "DD_PackPop.h"

#endif // __DBRASTERIMAGEDEF_H


