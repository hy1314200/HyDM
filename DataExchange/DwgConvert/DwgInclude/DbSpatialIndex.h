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



#ifndef OD_DBSPINDX_H
#define OD_DBSPINDX_H

#include "DD_PackPush.h"

#include "DbIndex.h"
#include "DbBlockIterator.h"

/** Description:
    This class implements Spatial Index objects in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSpatialIndex : public OdDbIndex
{
public:
  ODDB_DECLARE_MEMBERS(OdDbSpatialIndex);

  OdDbSpatialIndex();

  OdDbFilteredBlockIteratorPtr newIterator(
    const OdDbFilter* pFilter) const;
  
  void rebuildFull(
    OdDbIndexUpdateData* pIdxData);
  
  OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

protected:

  void rebuildModified(
    OdDbBlockChangeIterator* iterator);
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbSpatialIndex object pointers.
*/
typedef OdSmartPtr<OdDbSpatialIndex> OdDbSpatialIndexPtr;

class OdDbSpatialIndexIteratorImpl;
class OdDbSpatialFilter;


class OdDbSpatialIndexIterator;

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbSpatialIndexIterator object pointers.
*/
typedef OdSmartPtr<OdDbSpatialIndexIterator> OdDbSpatialIndexIteratorPtr;

/** Description:
    This class implements Iterator objects that traverse queries defined by OdDbSpatialFilter objects.
    Library:
    Db
    
    Remarks:
    This class is used by OcDbSpatialIndex.
     
    {group:OdDb_Classes}
*/
class OdDbSpatialIndexIterator : public OdDbFilteredBlockIterator
{
protected:
  OdDbSpatialIndexIterator();
public:
  ODRX_DECLARE_MEMBERS(OdDbSpatialIndexIterator);

  static OdDbSpatialIndexIteratorPtr newIterator(
    const OdDbSpatialIndex* pIndex, 
    const OdDbSpatialFilter* pFilter);
};


#include "DD_PackPop.h"

#endif // OD_DBSPINDX_H

