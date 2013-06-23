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



#ifndef _OD_DB_OLE2FRAME_
#define _OD_DB_OLE2FRAME_

#include "DD_PackPush.h"

#include "DbOleFrame.h"
#include "Ge/GePoint3d.h"

class OdOleItemHandler;

class TOOLKIT_EXPORT OdRectangle3d
{
public:
  OdGePoint3d upLeft;
  OdGePoint3d upRight;
  OdGePoint3d lowLeft;
  OdGePoint3d lowRight;
};

/** Description:
    This class represents OLE2 entities in an OdDbDatabase instance.

    Library:
    Db
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbOle2Frame : public OdDbOleFrame
{
public:
  ODDB_DECLARE_MEMBERS(OdDbOle2Frame);

  OdDbOle2Frame();
  
  OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
  
  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  void viewportDraw(
    OdGiViewportDraw* pVd) const;

  //bool getGeomExtents(OdGeExtents3d& extents) const;

  OdResult transformBy(
    const OdGeMatrix3d& xfm);
  
  /** Description:
    Returns the upper-left corner of the OLE object (DXF 10).
    
    Arguments:
    point3d (O) Receives the upper-left corner.
  */
  void getLocation(OdGePoint3d& 
    point3d) const;

  /** Description:
    Returns the corner points of the OLE object (DXF 10 & 11).
    
    Arguments:
    rect3d (O) Receives the corner points.
  */
  void position(
    OdRectangle3d& rect3d) const;
  
  /** Description:
      Sets the corner points of the OLE object (DXF 10 & 11).
    
    Arguments:
    rect3d (I) Corner points.
  */
  void setPosition(
    const OdRectangle3d& rect3d);

  /** Description:
    Returns the user type-string for the OLE object (DXF 3).
    
    Examples:  
    "Word Document"  
    "Excel Chart"  
  */
  OdString getUserType() const;
  
  enum Type
  {
      kUnknown  = 0,
      kLink     = 1,
      kEmbedded = 2,
      kStatic   = 3
  };
  /** Description:
    Returns the type of this OLE object (DXF 71).  
    
    Remarks:
    Possible for values for type are as follows:
    
    @table
    Name              Value
    OdDb::kUnknown    0
    OdDb::kLink       1
    OdDb::kEmbedded   2
    OdDb::kStatic     3
  */
  Type getType() const;

  /** Description:
    Returns the filename and item to which this OLE object is linked.
    
    Example:
    D:\My Documents\My Workbook.xls!Sheet2!R1C1:R10C10

    See also:
    getLinkPath()
  */
  OdString getLinkName() const;
  
  /** Description:
    Returns the filename to which this OLE object is linked.

    Example:
    D:\My Documents\My Workbook.xls
             
    See also:
    getLinkName()
  */
  OdString getLinkPath() const;
  
  /** Description:
    Returns the output *quality* for this OLE object (DXF 73).
    
    Remarks:
    Controls the *color* depth and resolution when plotted.
    
    Note:
    quality will be documented in the future.
  */
  OdUInt8 outputQuality() const;

  /** Description:
    Sets the output *quality* for this OLE object (DXF 73).
    
    Arguments:
    quality (I) Output *quality*.
      
    Remarks:
    Controls the *color* depth and resolution when plotted.

    Note:
    quality will be documented in the future.
  */
  void setOutputQuality(
    OdUInt8 quality);

  /** Description:
    Returns the compound document data size for this OLE object.
  */
  OdUInt32 getCompoundDocumentDataSize() const;

  /** Description:
    Writes the compound document data to the specified stream.
    
    Arguments:
    stream (O) Stream to receive the compound document data.
  */
  void getCompoundDocument(
    OdStreamBuf& stream) const;

  /** Description:
    Reads the compound document data from the specified stream.
    
    Arguments:
    nSize (I) Size of compound data.
    stream (I) Stream from which to read the compound document data.  
  */
  void setCompoundDocument(
    OdUInt32 nSize, 
    OdStreamBuf& stream);

  void getClassID(void** pClsid) const;
  
  const OdOleItemHandler* itemHandler() const;

  OdOleItemHandler* getItemHandler();

  void subClose();

  /** Description:
    Returns unknown field.

    Remarks:
    Was not accessible in DD1.12 (OdDbOle2FrameImpl::m_UnknownFromDWG)

    Note:
    Currently DWGdirect does not handle this field.
    This method will be removed or renamed in future releases.
  */
  OdInt32 unhandled_unknown0() const;

  /** Description:
    Returns unknown field.

    Remarks:
    Still unknown. OdDbOle2Frame::getUnknown1() in DD1.12

    Note:
    Currently DWGdirect does not handle this field.
    This method will be removed or renamed in future releases.
  */
  OdUInt8 unhandled_unknown1() const;

  /** Description:
    Returns unknown field.

    Remarks:
    Still unknown. OdDbOle2Frame::getUnknown2() in DD1.12

    Note:
    Currently DWGdirect does not handle this field.
    This method will be removed or renamed in future releases.
  */
  OdUInt8 unhandled_unknown2() const;

  /** Description:
    Sets unknown fields.

    Note:
    Currently DWGdirect does not handle these fields.
    This method will be removed or renamed in future releases.
  */
  void unhandled_setUnknown(OdInt32 unk0 = 0, OdUInt8 unk1 = 0x80, OdUInt8 unk2 = 0x55);

  /** Description:

    Remarks:
    OdDbOle2Frame::getPixelWidth() in DD1.12

    Note:
    Currently DWGdirect does not handle this field.
    This method will be removed or renamed in future releases.
  */
  OdUInt16 unhandled_himetricWidth() const;

  /** Description:

    Remarks:
    OdDbOle2Frame::getPixelHeight() in DD1.12

    Note:
    Currently DWGdirect does not handle this field.
    This method will be removed or renamed in future releases.
  */
  OdUInt16 unhandled_himetricHeight() const;

  /** Description:
    Sets unhandled fields.

    Note:
    Currently DWGdirect does not handle these fields.
    This method will be removed or renamed in future releases.
  */
  void unhandled_setHimetricSize(OdUInt16 w, OdUInt16 h);

};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbOld2Frame object pointers.
*/
typedef OdSmartPtr<OdDbOle2Frame> OdDbOle2FramePtr;

#include "DD_PackPop.h"

#endif

