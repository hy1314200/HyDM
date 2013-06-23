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



#ifndef FILER_INCLUDED
#define FILER_INCLUDED

#include <float.h>
#include "RxObject.h"
#include "OdStreamBuf.h"
#include "DbObjectId.h"

class OdString;
class OdGeScale3d;
class OdBinaryData;
class OdDbFilerController;
class OdDbDatabase;
class OdResBuf;
typedef OdSmartPtr<OdResBuf> OdResBufPtr;
class OdDbObjectId;
class OdGePoint2d;
class OdGePoint3d;
class OdGeVector2d;
class OdGeVector3d;
class OdDbAuditInfo;

#include "DD_PackPush.h"

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbFiler : public OdRxObject
{
public:
  OdDbFiler() {}

  ODRX_DECLARE_MEMBERS(OdDbFiler);

  virtual OdResult filerStatus() const;

  virtual void     resetFilerStatus();

  enum FilerType
  {
    kFileFiler         = 0,
    kCopyFiler         = 1,
    kUndoFiler         = 2,
    kBagFiler          = 3,
    kIdXlateFiler      = 4,
    kPageFiler         = 5,
    kDeepCloneFiler    = 6,
    kIdFiler           = 7,
    kPurgeFiler        = 8,
    kWblockCloneFiler  = 9
  };

  virtual FilerType filerType() const = 0;

  /** Description:
      Returns the working database being read or written by this filer.
  */
  virtual OdDbDatabase* database() const;

  /** Description:
      Returns the version of the drawing file being read or written by this filer.
  */
  virtual OdDb::DwgVersion dwgVersion(OdDb::MaintReleaseVer* = NULL) const;

  /** Description:
      Returns pointer to AuditInfo (may be NULL).
  */
  OdDbAuditInfo * getAuditInfo() const;

  virtual void setController(OdDbFilerController * pController);

  virtual OdDbFilerController* controller() const;
};

///////////////////////////////////////////////////////////////////////
/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdDxfCode
{
public:
  typedef enum
  {
    Unknown,
    Name,
    String,
    Bool,
    Integer8,
    Integer16,
    Integer32,
    Double,
    Angle,
    Point,
    BinaryChunk,
    LayerName,
    Handle,
    ObjectId,
    SoftPointerId,
    HardPointerId,
    SoftOwnershipId,
    HardOwnershipId,
    Color
  } Type;

  static Type _getType(int code);
};

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDxfFiler : public OdDbFiler
{
public:
  ODRX_DECLARE_MEMBERS(OdDbDxfFiler);
  OdDbDxfFiler() {}

  virtual void seek(OdInt32 offset, OdDb::FilerSeekType whence);
  virtual OdUInt32 tell() const;

  virtual int  precision() const;
  virtual void setPrecision(int prec);

  /** Description:
      Number of decimal digits printed in ASCII DXF file.
  */
  enum 
  { 
    kDfltPrec = -1, 
    kMaxPrec = DBL_DIG + 1 
  };

  virtual void writeXDataStart();
  virtual bool includesDefaultValues() const;
  virtual bool atEOF();
  virtual bool atEndOfObject();
  virtual bool atExtendedData();
  virtual bool atSubclassData(const char * subClassName);

  virtual int nextItem();
  virtual OdResBufPtr nextRb();
  virtual void writeRb(const OdResBuf* pRb);
  virtual void pushBackItem();

  OdString            rdString();

  virtual void        rdString(OdString &string) = 0;
  virtual bool        rdBool() = 0;
  virtual OdInt8      rdInt8() = 0;
  virtual OdInt16     rdInt16() = 0;
  virtual OdInt32     rdInt32() = 0;
  virtual OdUInt8     rdUInt8() = 0;
  virtual OdUInt16    rdUInt16() = 0;
  virtual OdUInt32    rdUInt32() = 0;
  virtual OdDbHandle  rdHandle() = 0;
  virtual OdDbObjectId rdObjectId() = 0;
  virtual double      rdAngle() = 0;
  virtual double      rdDouble() = 0;
  virtual void        rdPoint2d(OdGePoint2d& pt) = 0;
  virtual void        rdPoint3d(OdGePoint3d& pt) = 0;
  virtual void        rdVector2d(OdGeVector2d& pt) = 0;
  virtual void        rdVector3d(OdGeVector3d& pt) = 0;
  virtual void        rdScale3d(OdGeScale3d& pt) = 0;
  virtual void        rdBinaryChunk(OdBinaryData&) = 0;

  virtual void copyItem(OdDbDxfFiler * pFrom);

  virtual void wrName(int groupCode, const OdChar *string) = 0;
  virtual void wrString(int groupCode, const OdChar *string) = 0;
  void wrStringOpt(int groupCode, const OdChar *string);
  void wrSubclassMarker(const OdString &string);

  virtual void wrBool(int groupCode, bool val) = 0;
  void wrBoolOpt(int groupCode, bool val, bool def);

  virtual void wrInt8(int groupCode, OdInt8 val) = 0;
  void wrInt8Opt(int groupCode, OdInt8 val, OdInt8 def);

  virtual void wrUInt8(int groupCode, OdUInt8 val) = 0;
  void wrUInt8Opt(int groupCode, OdUInt8 val, OdUInt8 def);

  virtual void wrInt16(int groupCode, OdInt16 val) = 0;
  void wrInt16Opt(int groupCode, OdInt16 val, OdInt16 def);

  virtual void wrUInt16(int groupCode, OdUInt16 val) = 0;
  void wrUInt16Opt(int groupCode, OdUInt16 val, OdUInt16 def);

  virtual void wrInt32(int groupCode, OdInt32 val) = 0;
  void wrInt32Opt(int groupCode, OdInt32 val, OdInt32 def);

  virtual void wrUInt32(int groupCode, OdUInt32 val) = 0;
  void wrUInt32Opt(int groupCode, OdUInt32 val, OdUInt32 def);

  virtual void wrHandle(int groupCode, OdDbHandle val) = 0;

  virtual void wrObjectId(int groupCode, OdDbObjectId val) = 0;
  void wrObjectIdOpt(int groupCode, OdDbObjectId val);

  virtual void wrAngle(int groupCode, double val, int precision = kDfltPrec) = 0;
  void wrAngleOpt(int groupCode, double val, double def = 0., int precision = kDfltPrec);

  virtual void wrDouble(int groupCode, double val, int precision = kDfltPrec) = 0;
  void wrDoubleOpt(int groupCode, double val, double def = 0., int precision = kDfltPrec);

  virtual void wrPoint2d(int groupCode, const OdGePoint2d& pt, int precision = kDfltPrec) = 0;
  void wrPoint2dOpt(int groupCode, const OdGePoint2d& pt, const OdGePoint2d& def, int precision = kDfltPrec);

  virtual void wrPoint3d(int groupCode, const OdGePoint3d& pt, int precision = kDfltPrec) = 0;
  void wrPoint3dOpt(int groupCode, const OdGePoint3d& pt, const OdGePoint3d& def, int precision = kDfltPrec);

  virtual void wrVector2d(int groupCode, const OdGeVector2d& pt, int precision = kDfltPrec) = 0;
  void wrVector2dOpt(int groupCode, const OdGeVector2d& pt, const OdGeVector2d& def, int precision = kDfltPrec);

  virtual void wrVector3d(int groupCode, const OdGeVector3d& pt, int precision = kDfltPrec) = 0;
  void wrVector3dOpt(int groupCode, const OdGeVector3d& pt, const OdGeVector3d& def, int precision = kDfltPrec);

  virtual void wrScale3d(int groupCode, const OdGeScale3d& pt, int precision = kDfltPrec) = 0;

  virtual void wrBinaryChunk(int groupCode, const OdUInt8* pBuff, OdUInt32 nSize) = 0;
  void wrBinaryChunk(int groupCode, const OdBinaryData& bd);
};
typedef OdSmartPtr<OdDbDxfFiler> OdDbDxfFilerPtr;

/** Description:

    {group:Error_Classes}
*/
class TOOLKIT_EXPORT OdError_DwgObjectImproperlyRead : public OdError
{
public:
  OdError_DwgObjectImproperlyRead() : OdError(eDwgObjectImproperlyRead){}
};

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDwgFiler : public OdDbFiler
{
public:
  OdDbDwgFiler() {}

  ODRX_DECLARE_MEMBERS(OdDbDwgFiler);

  virtual void seek(OdInt32 offset, OdDb::FilerSeekType whence) = 0;
  virtual OdUInt32 tell() const = 0;

  virtual bool         rdBool() = 0;
  virtual OdString     rdString() = 0;
  virtual void         rdBytes(void* buffer, OdUInt32 nLen) = 0;

  virtual OdInt8       rdInt8() = 0;
  virtual OdUInt8      rdUInt8() = 0;

  virtual OdInt16      rdInt16() = 0;
  virtual OdInt32      rdInt32() = 0;
  virtual void*        rdAddress();
  virtual double       rdDouble() = 0;
  virtual OdDbHandle   rdDbHandle() = 0;

  virtual OdDbObjectId rdSoftOwnershipId() = 0;
  virtual OdDbObjectId rdHardOwnershipId() = 0;
  virtual OdDbObjectId rdHardPointerId() = 0;
  virtual OdDbObjectId rdSoftPointerId() = 0;

  virtual OdGePoint2d  rdPoint2d() = 0;
  virtual OdGePoint3d  rdPoint3d() = 0;
  virtual OdGeVector2d rdVector2d() = 0;
  virtual OdGeVector3d rdVector3d() = 0;
  virtual OdGeScale3d  rdScale3d() = 0;

  virtual void wrBool(bool) = 0;
  virtual void wrString(const OdString &string) = 0;
  virtual void wrBytes(const void* buffer, OdUInt32 nLen) = 0;

  virtual void wrInt8(OdInt8 val) = 0;
  virtual void wrUInt8(OdUInt8 val) = 0;

  virtual void wrInt16(OdInt16 val) = 0;
  virtual void wrInt32(OdInt32 val) = 0;
  virtual void wrAddress(const void* val);
  virtual void wrDouble(double val) = 0;
  virtual void wrDbHandle(const OdDbHandle& val) = 0;

  virtual void wrSoftOwnershipId(const OdDbObjectId& id) = 0;
  virtual void wrHardOwnershipId(const OdDbObjectId& id) = 0;
  virtual void wrSoftPointerId(const OdDbObjectId& id) = 0;
  virtual void wrHardPointerId(const OdDbObjectId& id) = 0;

  virtual void wrPoint2d(const OdGePoint2d& pt) = 0;
  virtual void wrPoint3d(const OdGePoint3d& pt) = 0;
  virtual void wrVector2d(const OdGeVector2d& vec) = 0;
  virtual void wrVector3d(const OdGeVector3d& vec) = 0;
  virtual void wrScale3d(const OdGeScale3d& point) = 0;

  virtual bool usesReferences() const;
  virtual void addReference(OdDbObjectId id, OdDb::ReferenceType rt);
};

typedef OdSmartPtr<OdDbDwgFiler> OdDbDwgFilerPtr;


/** Description:
    Empty implementation of OdIdFiler to use as base for ID Filers.

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdIdFiler : public OdDbDwgFiler
{
public:
  OdIdFiler() : m_pController(NULL) {}

  void seek(OdInt32 , OdDb::FilerSeekType );
  OdUInt32 tell() const;
  OdResult filerStatus() const;
  void resetFilerStatus();
  FilerType filerType() const;
  OdDb::DwgVersion dwgVersion(OdDb::MaintReleaseVer* pMRV = NULL) const;

  bool        rdBool();
  OdString    rdString();
  void        rdBytes(void* , OdUInt32 );

  OdInt8      rdInt8();
  OdUInt8     rdUInt8();

  OdInt16     rdInt16();
  OdInt32     rdInt32();
  double      rdDouble();
  OdDbHandle  rdDbHandle();

  OdDbObjectId rdSoftOwnershipId();
  OdDbObjectId rdHardOwnershipId();
  OdDbObjectId rdHardPointerId();
  OdDbObjectId rdSoftPointerId();

  OdGePoint2d rdPoint2d();
  OdGePoint3d rdPoint3d();
  OdGeVector3d rdVector3d();
  OdGeVector2d rdVector2d();
  OdGeScale3d rdScale3d();

  void wrBool(bool);
  void wrString(const OdString &);
  void wrBytes(const void*, OdUInt32);

  void wrInt8(OdInt8 );
  void wrUInt8(OdUInt8 );

  void wrInt16(OdInt16 );
  void wrInt32(OdInt32 );
  void wrDouble(double );
  void wrDbHandle(const OdDbHandle& );

  void wrPoint2d(const OdGePoint2d& );
  void wrPoint3d(const OdGePoint3d& );
  void wrVector2d(const OdGeVector2d& );
  void wrVector3d(const OdGeVector3d& );
  void wrScale3d(const OdGeScale3d& );

  void setController(OdDbFilerController* pController) { m_pController = pController; }
  OdDbFilerController* controller() const { return m_pController; }
private:
  OdDbFilerController* m_pController;
};

#include "DD_PackPop.h"

#endif  // FILER_INCLUDED


