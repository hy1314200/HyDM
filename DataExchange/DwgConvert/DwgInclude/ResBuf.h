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



#if !defined(_ODRESBUF_H___INCLUDED_)
#define _ODRESBUF_H___INCLUDED_

#include "DD_PackPush.h"

#include "RxObject.h"
#include "OdString.h"


class OdBinaryData;
class OdGePoint2d;
class OdGePoint3d;
class OdDbObjectId;
class OdDbDatabase;
class OdCmColor;
class OdResBuf;

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdResBuf object pointers.
*/
typedef OdSmartPtr<OdResBuf> OdResBufPtr;

/** Description:

    {group:Error_Classes}
*/
class TOOLKIT_EXPORT OdError_InvalidResBuf : public OdError
{
public:
  OdError_InvalidResBuf() : OdError(eInvalidResBuf){}
};

/** Description:
  This class implements ResBuf structures that handle all DWG basic data types.  

  Remarks:
  Each ResBuf object contains three data members:
  
  o  ResType: An integer which specifies the type of data stored in the ResBuf object.
  o  ResVal:  A container for the data stored in the ResBuf object.
  o  RbNext:  A SmartPointer to the *next* ResBuf object in a linked list.
  
  Library:
  Db
  
  
  {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdResBuf : public OdRxObject
{
public:

  /** 
    Description:
    Copies the contents of the specified ResBuf object into this ResBuf object.

    Arguments:
    pSource (I) Pointer to the source object.
    
    Throws:
    eInvalidGroupCode for invalid ResType values.
  */
  void copyFrom(
    const OdRxObject* pSource);
  bool operator==(
    const OdResBuf& resBuf) const;

  enum ValueType
  {
    kRtNone                     = 5000,
    kRtName                     = 5001,
    kRtString                   = 5002,
    kRtBool                     = 5003,
    kRtInt8                     = 5004,
    kRtInt16                    = 5005,
    kRtInt32                    = 5006,
    kRtColor                    = 5007,
    kRtDouble                   = 5008,
    kRtAngle                    = 5009,
    kRtPoint2d                  = 5010,
    kRtPoint3d                  = 5011,
    kRtVector3d                 = 5012,
    kRtBinaryChunk              = 5013,
    kRtLayerName                = 5014,
    kRtHandle                   = 5015,
    kRtObjectId                 = 5016,
    kRtSoftPointerId            = 5017,
    kRtHardPointerId            = 5018,
    kRtSoftOwnershipId          = 5019,
    kRtHardOwnershipId          = 5020,
    kRtLastType                 = 5021,

    kDxfInvalid                 = -9999,

    kDxfXDictionary             = -6,
    kDxfPReactors               = -5,
    kDxfUndo                    = -4,
    kDxfXDataStart              = -3,
    kDxfHeaderId                = -2,
    kDxfFirstEntId              = -2,
    kDxfEnd                     = -1,
    kDxfStart                   = 0,
    kDxfText                    = 1,
    kDxfXRefPath                = 1,
    kDxfShapeName               = 2,
    kDxfBlockName               = 2,
    kDxfAttributeTag            = 2,
    kDxfSymbolTableName         = 2,
    kDxfMstyleName              = 2,
    kDxfSymTableRecName         = 2,
    kDxfAttributePrompt         = 3,
    kDxfDimStyleName            = 3,
    kDxfLinetypeProse           = 3,
    kDxfTextFontFile            = 3,
    kDxfDescription             = 3,
    kDxfDimPostStr              = 3,
    kDxfTextBigFontFile         = 4,
    kDxfDimAPostStr             = 4,
    kDxfCLShapeName             = 4,
    kDxfSymTableRecComments     = 4,
    kDxfHandle                  = 5,
    kDxfDimBlk                  = 5,
    kDxfDimBlk1                 = 6,
    kDxfLinetypeName            = 6,
    kDxfDimBlk2                 = 7,
    kDxfTextStyleName           = 7,
    kDxfLayerName               = 8,
    kDxfCLShapeText             = 9,
    kDxfXCoord                 = 10,
    kDxfYCoord                 = 20,
    kDxfZCoord                 = 30,
    kDxfElevation              = 38,
    kDxfThickness              = 39,
    kDxfReal                   = 40,
    kDxfViewportHeight         = 40,
    kDxfTxtSize                = 40,
    kDxfTxtStyleXScale         = 41,
    kDxfViewWidth              = 41,
    kDxfViewportAspect         = 41,
    kDxfTxtStylePSize          = 42,
    kDxfViewLensLength         = 42,
    kDxfViewFrontClip          = 43,
    kDxfViewBackClip           = 44,
    kDxfShapeXOffset           = 44,
    kDxfShapeYOffset           = 45,
    kDxfViewHeight             = 45,
    kDxfShapeScale             = 46,
    kDxfPixelScale             = 47,
    kDxfLinetypeScale          = 48,
    kDxfDashLength             = 49,
    kDxfMlineOffset            = 49,
    kDxfLinetypeElement        = 49,
    kDxfAngle                  = 50,
    kDxfViewportSnapAngle      = 50,
    kDxfViewportTwist          = 51,
    kDxfVisibility             = 60,
    kDxfLayerLinetype          = 61,
    kDxfColor                  = 62,
    kDxfHasSubentities         = 66,
    kDxfViewportVisibility     = 67,
    kDxfViewportActive         = 68,
    kDxfViewportNumber         = 69,
    kDxfInt16                  = 70,
    kDxfViewMode               = 71,
    kDxfCircleSides            = 72,
    kDxfViewportZoom           = 73,
    kDxfViewportIcon           = 74,
    kDxfViewportSnap           = 75,
    kDxfViewportGrid           = 76,
    kDxfViewportSnapStyle      = 77,
    kDxfViewportSnapPair       = 78,
    kDxfRegAppFlags            = 71,
    kDxfTxtStyleFlags          = 71,
    kDxfLinetypeAlign          = 72,
    kDxfLinetypePDC            = 73,
    kDxfInt32                  = 90,
    kDxfSubclass               = 100,
    kDxfEmbeddedObjectStart    = 101,
    kDxfControlString          = 102,
    kDxfDimVarHandle           = 105,
    kDxfUCSOrg                 = 110,
    kDxfUCSOriX                = 111,
    kDxfUCSOriY                = 112,
    kDxfXReal                  = 140,
    kDxfXInt16                 = 170,
    kDxfNormalX                = 210,
    kDxfNormalY                = 220,
    kDxfNormalZ                = 230,
    kDxfXXInt16                = 270,
    kDxfInt8                   = 280,
    kDxfRenderMode             = 281,
    kDxfBool                   = 290,
    kDxfXTextString            = 300,
    kDxfBinaryChunk            = 310,
    kDxfArbHandle              = 320,
    kDxfSoftPointerId          = 330,
    kDxfHardPointerId          = 340,
    kDxfSoftOwnershipId        = 350,
    kDxfHardOwnershipId        = 360,  
    kDxfLineWeight             = 370,
    kDxfPlotStyleNameType      = 380,
    kDxfPlotStyleNameId        = 390,
    kDxfXXXInt16               = 400,
    kDxfLayoutName             = 410,
    kDxfComment                = 999,
    kDxfXdAsciiString          = 1000,
    kDxfRegAppName             = 1001,
    kDxfXdControlString        = 1002,
    kDxfXdLayerName            = 1003,
    kDxfXdBinaryChunk          = 1004,
    kDxfXdHandle               = 1005,
    kDxfXdXCoord               = 1010,
    kDxfXdYCoord               = 1020,
    kDxfXdZCoord               = 1030,
    kDxfXdWorldXCoord          = 1011,
    kDxfXdWorldYCoord          = 1021,
    kDxfXdWorldZCoord          = 1031,
    kDxfXdWorldXDisp           = 1012,
    kDxfXdWorldYDisp           = 1022,
    kDxfXdWorldZDisp           = 1032,
    kDxfXdWorldXDir            = 1013,
    kDxfXdWorldYDir            = 1023,
    kDxfXdWorldZDir            = 1033,
    kDxfXdReal                 = 1040,
    kDxfXdDist                 = 1041,
    kDxfXdScale                = 1042,
    kDxfXdInteger16            = 1070,
    kDxfXdInteger32            = 1071
  };

  ODRX_DECLARE_MEMBERS(OdResBuf);

  ~OdResBuf();

  /** Description:
    Returns the ResType of this ResBuf object.
    
    See also:
    ValueType
    
    Remarks:
    OdDxfCode::_getType() may be used to determine the type of data associated with a given *restype*.
  */
  int restype() const;
  
  /** Description:
    Sets the ResType of this ResBuf object.
    
    Arguments:
    resType (I) ResType.
        
    See also:
    ValueType
  */
  void setRestype(
    int restype);

  /** Description:
    Returns a SmartPointer to the *next* ResBuf object in this ResBuf object's ResBuf chain.
  */
  OdResBufPtr next() const;

  /** Description:
    Returns a SmartPointer to the *last* ResBuf object in this ResBuf object's ResBuf chain.
  */
  OdResBufPtr last() const;
  
  /** Description:
    Inserts the specified ResBuf object before the *next* ResBuf object in this ResBuf object's ResBuf chain.
    
    Arguments:
    pRb (I) Pointer to the ResBuf object.
    
    Remarks:
    Returns a SmartPointer to the ResBuf object before which it was inserted.
  */
  OdResBufPtr insert(
    OdResBuf* pRb);
  
  /** Description:
    Sets the specified Resbuf object as the *next* Resbuf Object in the ResBuf chain of this 
    Resbuf object after this ResBuf object.
    
    Arguments:
    pRb (I) Pointer to the ResBuf object.
  */
  OdResBufPtr setNext(
    OdResBuf* pRb);

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of data returned by this function.
  */
  OdString getString() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of data returned by this function.
  */
  bool getBool() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of data returned by this function.
  */
  OdInt8 getInt8() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of data returned by this function.
  */
  OdInt16 getInt16() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of data returned by this function.
  */
 OdInt32 getInt32() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of data returned by this function.
  */
  double getDouble() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    eInvalidGroupCode if ResType does not correspond to the type of data returned by this function.
  */
  const OdGePoint2d& getPoint2d() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    eInvalidGroupCode if ResType does not correspond to the type of data returned by this function.
  */
  const OdGePoint3d& getPoint3d() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    eInvalidGroupCode if ResType does not correspond to the type of data returned by this function.
  */
  const OdBinaryData& getBinaryChunk() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    eInvalidGroupCode if ResType does not correspond to the type of data returned by this function.
  */
  const OdCmColor& getColor() const;

  /** Description:
    Returns the ResVal in this ResBuf object.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of data returned by this function.
  */
  OdDbHandle getHandle() const;

  /** Description:
    Returns the object ID of the specified *database*.
    
    Arguments:
    pDb (I) Pointer to the database.
  */
  OdDbObjectId getObjectId(
    OdDbDatabase* pDb) const;

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setString(
    const OdString& resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setBool(
    bool resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setInt8(
    OdInt8 resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setInt16(
    OdInt16 resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setInt32(
    OdInt32 resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setDouble(
    double resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setPoint2d(
    const OdGePoint2d& resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setPoint3d(
    const OdGePoint3d& resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setBinaryChunk(
    const OdBinaryData& resVal);

  /** Description:
    Arguments:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setHandle(
    const OdDbHandle& resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setObjectId(
    const OdDbObjectId& resVal);

  /** Description:
    Sets the ResVal in this ResBuf object.
      
    Arguments:
    resVal (I) ResVal.

    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  void setColor(
    const OdCmColor& resVal);

  /** Description:
    Creates a ResBuf object of the specified ResType.
    
    Arguments:
    resType (I) ResType.
    resVal (I) ResVal.    
 
    Throws:
    OdError_InvalidResBuf if ResType does not correspond to the type of ResVal.
  */
  static OdResBufPtr newRb(
    int resType = OdResBuf::kRtNone);

  static OdResBufPtr newRb(
    int resType, 
    bool resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setBool(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    OdInt8 resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setInt8(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    OdUInt8 resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setInt8(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    OdInt16 resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setInt16(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    OdUInt16 resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setInt16(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    OdInt32 resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setInt32(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    OdUInt32 resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setInt32(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    double resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setDouble(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    const OdGePoint2d& resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setPoint2d(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    const OdGePoint3d& resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setPoint3d(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    const OdString& resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setString(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    const OdChar* resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setString(resVal);
    return pRes;
  }

  static OdResBufPtr newRb(
    int resType, 
    const OdCmColor& resVal)
  {
    OdResBufPtr pRes = newRb(resType);
    pRes->setColor(resVal);
    return pRes;
  }


  union Data
  {
    bool    Bool;
    OdInt16 Int16;
    OdInt32 Int32;
    double  Double;
    void*   Pointer;
    OdUInt8 Bytes[sizeof(OdInt64)];
  };
protected:
  OdResBuf();

  int m_nCode;  // ResType
  Data m_data;   // ResVal
  OdResBufPtr m_pNext;  // RbNext
};
#include "DD_PackPop.h"

#endif //_ODRESBUF_H___INCLUDED_


