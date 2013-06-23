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



#ifndef   _XREFMAN_H_
#define   _XREFMAN_H_

#include "RxObject.h"
#include "SmartPtr.h"
#include "DbLayerTableRecord.h"
#include "DbBlockTableRecord.h"
#include "DbLinetypeTableRecord.h"
#include "DbTextStyleTableRecord.h"
#include "DbSecurity.h"

#include "DD_PackPush.h"

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbXRefMan
{
public:
  /** Description:
      Performs loading of specified xref if it is not loaded yet.
  */
  static OdResult load(OdDbDatabase* pHostDb, const OdChar* szXrefBlockname);

  /** Description:
      Performs loading of specified xrefs if they are not loaded yet.
  */
  static OdResult load(OdDbObjectIdArray& xrefBTRids);

  /** Description:
      Performs loading of specified xref if it is not loaded yet.
  */
  static OdResult load(OdDbBlockTableRecord* pBTR);

  /** Description:
      Performs loading of all not loaded xrefs.
  */
  static OdResult loadAll(OdDbDatabase* pHostDb);

  /** Description:
      Performs unloading of specified xref.
  */
  static void unload(OdDbBlockTableRecord* pBTR);

  /** Description:
      Performs unloading of specified xrefs.
  */
  static void unload(OdDbObjectIdArray& xrefBTRids);

  /** Description:
      Performs unloading of all loaded xrefs.
  */
  static void unloadAll(OdDbDatabase* pHostDb);

  /** Description:
      Performs binding of specified xref.
  */
  static OdResult bind(OdDbBlockTableRecord* pBTR, bool bInsertBind = false);

  /** Description:
      Performs detaching of specified xref.
  */
  static OdResult detach(OdDbBlockTableRecord* pBTR);

  /** Description:
      Mark the specified xref as an overlaid or an attached xref depend on bOverlaid flag
  */
  static void setOverlaid(OdDbBlockTableRecord* pXRefBlock, bool  bOverlaid = true);

};

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbXRefManExt
{
public:
  /** Description:
      Creates a new block representing an external reference, and adds it to the passed in database.

      Arguments:
      pDb Database to which the external reference block will be added.
      xrefPathName Path of the external reference file.
      xrefBlockName Name for the newly external reference block.
      overlay True if the external reference should be an overlay, false otherwise.
      password - [optional] password for encrypted file.
      han - [optional] Handle value to be assigned to the new object.
  */
  static OdDbBlockTableRecordPtr     
    addNewXRefDefBlock(OdDbDatabase* pDb, const OdString& xrefPathName, 
                        const OdString& xrefBlockName, bool overlay, 
                        const OdPassword& password = OdPassword(),
                        OdDbHandle han = 0);

  /** Description:
      Adds a layer that is dependent on the passed in external reference block, and sets the appropriate
      internal properties of the layer.

      Arguments:
      pXRefBlock External reference on which the newly added layer will be dependent.
      szLayerName Name of layer to be added.

      Remarks:
      For example, if the passed in external reference block has a name of "XR1", and the specified layer
      name is "layerSix", then a layer called "XR1|layerSix" will be added to the database which contains
      the specified external reference block.
  */
  static OdDbLayerTableRecordPtr     
    addNewXRefDependentLayer(const OdDbBlockTableRecord* pXRefBlock, const OdChar* szLayerName);

  static inline OdDbLayerTableRecordPtr addNewXRefDependentLayer(OdDbObjectId xRefBlockId, const OdChar* szLayerName)
  { return addNewXRefDependentLayer(OdDbBlockTableRecordPtr(xRefBlockId.safeOpenObject()), szLayerName); }

  static OdDbLinetypeTableRecordPtr     
    addNewXRefDependentLinetype(const OdDbBlockTableRecord* pXRefBlock, const OdChar* szLinetypeName);

  static inline OdDbLinetypeTableRecordPtr
    addNewXRefDependentLinetype(OdDbObjectId xRefBlockId, const OdChar* szLinetypeName)
  { return addNewXRefDependentLinetype(OdDbBlockTableRecordPtr(xRefBlockId.safeOpenObject()), szLinetypeName); }

  static OdDbTextStyleTableRecordPtr     
    addNewXRefDependentTextStyle(const OdDbBlockTableRecord* pXRefBlock, const OdChar* szTextStyleName);

  static inline OdDbTextStyleTableRecordPtr
    addNewXRefDependentTextStyle(OdDbObjectId xRefBlockId, const OdChar* szTextStyleName)
  { return addNewXRefDependentTextStyle(OdDbBlockTableRecordPtr(xRefBlockId.safeOpenObject()), szTextStyleName); }
};

#include "DD_PackPop.h"

#endif //_XREFMAN_H_



