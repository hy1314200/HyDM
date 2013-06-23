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



#ifndef DBSYMUTL_DEFINED
#define DBSYMUTL_DEFINED

#include "DD_PackPush.h"

#include "OdString.h"
#include "DbObjectId.h"
#include "DbSymbolTable.h"
#include "DbSymbolTableRecord.h"
#include "DbTextStyleTableRecord.h"
#include "DbDictionary.h"
#include "DbDatabase.h"

// Predefined names
// Probably they should be moved to Services class
#define szModelSpaceStr         "*Model_Space"
#define szModelSpaceStr_R12     "$MODEL_SPACE"
#define szPaperSpaceStr         "*Paper_Space"
#define szPaperSpaceStr_R12     "$PAPER_SPACE"
#define layerDefpointsNameStr   "Defpoints"
#define layerZeroNameStr        "0"
#define linetypeByBlockNameStr  "ByBlock"
#define linetypeByLayerNameStr  "ByLayer";
#define linetypeContinuousNameStr  "Continuous"
#define regAppAcadName          "ACAD"
#define szStandardStr           "Standard"
#define szMetricDimStandardStr  "ISO-25"
#define szViewportActiveNameStr "*Active"
#define plotStyleNormalNameStr  "Normal"

/** Description:

    {group:DD_Namespaces}
*/
namespace OdDbSymUtil
{
  enum CompatibilityMode
  {
      kExtendedNames    = true,
      kPreExtendedNames = false
  };

  enum NameCaseMode
  {
      kPreserveCase     = true,
      kForceToUpper     = false
  };

  enum NewNameMode
  {
      kAsNewName        = true,
      kAsExistingName   = false
  };

  enum VerticalBarMode
  {
      kAllowVerticalBar = true,
      kNoVerticalBar    = false
  };

  inline OdString getSymbolName(const OdDbObjectId& objId)
  {
    OdDbSymbolTableRecordPtr pRec
      = OdDbSymbolTableRecord::cast(objId.openObject(OdDb::kForRead, true).get());
    if (!pRec.isNull())
    {
      return pRec->getName();
    }
    return OdString();
  }

  #define DBSYMUTL_MAKE_GETSYMBOLID_FUNC(T_TABLE) \
  inline OdDbObjectId \
  get ## T_TABLE ## Id(const OdString& name, const OdDbDatabase *pDb) \
  { \
    OdDbSymbolTablePtr pTable = pDb->get ## T_TABLE ## TableId().safeOpenObject(); \
    ODA_ASSERT(!pTable->isOdDbObjectIdsInFlux()); \
    return pTable->getAt(name); \
  }
  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(Viewport)
  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(Block)
  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(DimStyle)
  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(Layer)
  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(Linetype)
  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(RegApp)
  //DBSYMUTL_MAKE_GETSYMBOLID_FUNC(TextStyle)

  inline OdDbObjectId getTextStyleId(const OdString& name, OdDbDatabase *pDb)
  {
    OdDbSymbolTablePtr pTable = pDb->getTextStyleTableId().safeOpenObject();
    ODA_ASSERT(!pTable->isOdDbObjectIdsInFlux());
    OdDbTextStyleTableRecordPtr pRec = pTable->getAt(name, OdDb::kForRead);

    if (pRec.get())
      if (!pRec->isShapeFile())
        return pRec->objectId();
    return OdDbObjectId::kNull;
  }

  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(UCS)
  DBSYMUTL_MAKE_GETSYMBOLID_FUNC(View)
  #undef DBSYMUTL_MAKE_GETSYMBOLID_FUNC

  inline OdDbObjectId getPlotstyleId(const OdChar* name, OdDbDatabase *pDb)
	{
		OdDbDictionaryPtr pDic = pDb->getPlotStyleNameDictionaryId().safeOpenObject();
    ODA_ASSERT(!pDic->isOdDbObjectIdsInFlux());
		if (!pDic.isNull())
			return pDic->getAt(name);
    return OdDbObjectId::kNull;
	}

  inline const OdChar* linetypeByLayerName()
  { return  linetypeByLayerNameStr; }

  inline bool isLinetypeByLayerName(const OdChar* name)
  { return !odStrICmp(name, linetypeByLayerName()); }

  inline const OdChar* linetypeByBlockName()
  { return  linetypeByBlockNameStr; }

  inline bool isLinetypeByBlockName(const OdChar* name)
  { return !odStrICmp(name, linetypeByBlockName()); }

  inline const OdChar* linetypeContinuousName()
  { return  linetypeContinuousNameStr; }

  inline bool isLinetypeContinuousName(const OdChar* name)
  { return !odStrICmp(name, linetypeContinuousName()); }

  inline const OdChar* layerZeroName()
  { return  layerZeroNameStr; }

  inline bool isLayerZeroName(const OdChar* name)
  { return !odStrICmp(name, layerZeroName()); }

  inline const OdChar* layerDefpointsName()
  { return  layerDefpointsNameStr; }

  inline bool isLayerDefpointsName(const OdChar* name)
  { return !odStrICmp(name, layerDefpointsName()); }

  inline const OdChar* textStyleStandardName()
  { return szStandardStr; }

  inline const OdChar* MLineStyleStandardName()
  { return szStandardStr; }

  inline bool isMLineStandardName(const OdChar* name)
  { return !odStrICmp(name, MLineStyleStandardName()); }

  inline const OdChar* dimStyleStandardName(OdDb::MeasurementValue measurement)
  { return measurement == OdDb::kEnglish ? szStandardStr : szMetricDimStandardStr; }

  inline const OdChar* viewportActiveName()
  { return szViewportActiveNameStr; }

  inline bool isViewportActiveName(const OdChar* name)
  { return !odStrICmp(name, viewportActiveName()); }

  inline bool isTextStyleStandardName(const OdChar* name)
  { return !odStrICmp(name, textStyleStandardName()); }

  TOOLKIT_EXPORT const OdDbObjectId& textStyleStandardId(OdDbDatabase* pDb);
	TOOLKIT_EXPORT const OdDbObjectId& dimStyleStandardId(OdDbDatabase* pDb);
  TOOLKIT_EXPORT const OdDbObjectId  MLineStyleStandardId(OdDbDatabase* pDb);

  inline const OdChar* blockModelSpaceName(OdDb::DwgVersion version = OdDb::kDHL_CURRENT)
  { return version <= OdDb::vAC12 ? szModelSpaceStr_R12 : szModelSpaceStr; }

  inline bool isBlockModelSpaceName(const OdChar * pN, OdDb::DwgVersion version = OdDb::kDHL_CURRENT)
  { return odStrICmp(pN, blockModelSpaceName(version)) == 0; }

  inline const OdChar* blockPaperSpaceName(OdDb::DwgVersion version = OdDb::kDHL_CURRENT)
  { return version <= OdDb::vAC12 ? szPaperSpaceStr_R12 : szPaperSpaceStr; }

  inline bool isBlockPaperSpaceName(const OdChar * pN, OdDb::DwgVersion version = OdDb::kDHL_CURRENT)
  { return odStrICmp(pN, blockPaperSpaceName(version)) == 0; }

  inline const OdChar* plotStyleNormalName()
  { return plotStyleNormalNameStr;}

  inline const OdChar* TableStyleStandardName()
  { return szStandardStr; }

  inline bool isTableStandardName(const OdChar* name)
  { return !odStrICmp(name, TableStyleStandardName()); }

}  
// namespace OdDbSymUtil

#undef szModelSpaceStr
#undef szPaperSpaceStr
#undef linetypeByLayerNameStr
#undef linetypeByBlockNameStr
#undef szStandardStr
#undef layerDefpointsNameStr
#undef layerZeroNameStr
#undef szViewportActiveNameStr
#undef plotStyleNormalNameStr

#include "DD_PackPop.h"

#endif // DBSYMUTL_DEFINED


