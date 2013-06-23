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



#ifndef GEOMENT_DBBODY_H
#define GEOMENT_DBBODY_H

#include "DD_PackPush.h"
#include "ModelerDefs.h"
#include "DbEntity.h"

class OdModelerGeometry;
class OdStreamBuf;
class OdBrBrep;
/** Description:
    This class represents 3D Body entities in an OdDbDatabase instance.

    Library:
    Db
    
    Remarks:
    An OdDbBody entity is a wrapper for an ACIS model cannot be
    represented as an OdDb3dSolid entity or anOdDbRegion entity.
    
   {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbBody : public OdDbEntity
{
public:
  ODDB_DECLARE_MEMBERS(OdDbBody);

  OdDbBody();
  
  /** Description:
    Returns true if and only if there is no ACIS model associated with this entity. 
  */
  bool isNull() const;

  /** Description:
    Writes the ACIS data of this entity to the specified OdStreamBuf object, 
    or the specified ACIS solids to the specified file.

    Arguments:
    pStream (I) Pointer to the output stream.    
    typeVer (I) Type and version of the ACIS data to write.
    fileName (I) File name to write.
    pSolids (I) Array of pointers to ACIS solids.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not. 
  */
  OdResult acisOut(
    OdStreamBuf* pStream, 
    AfTypeVer typeVer = kAfTypeVerAny);
  static OdResult acisOut(
    const char *fileName, 
    const OdDbEntityPtrArray& pSolids, 
    AfTypeVer typeVer = kAfVer700|kAfTypeASCII);

  /** Description:
    Reads the ACIS data for this entity from the specified OdStreamBuf object,
    or an array of ACIS solids from the specified file.

    Arguments:
    pStream (I) Pointer to the input stream.    
    typeVer (I) Pointer to the type and version of the ACIS data to read.
    fileName (I) File name to read.
    pSolids (O) Receives the pointers to the ACIS solids.
   
    Remarks:
    
    If typeVer is NULL, this function returns the version of the ACIS data
    of this 3D solid entity.
  */
  OdResult acisIn(
    OdStreamBuf* pStream, 
    AfTypeVer *typeVer = NULL);
  static OdResult acisIn(
    const char *fileName, 
    OdDbEntityPtrArray& pSolids);

  /** Description:
    Returns the boundary representation of the 3D Solid Entity.
    Arguments:
    brep (O) Receives the boundary representation.
  */
  void brep(
    OdBrBrep& brep);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual void viewportDraw(
    OdGiViewportDraw* pVd) const;

  /*
  virtual OdDbSubentId internalSubentId(void*) const;
  virtual void* internalSubentPtr(const OdDbSubentId& id) const;
  */
  /*
  virtual void getSubentPathsAtGsMarker( OdDb::SubentType type,
                    int gsMark, const OdGePoint3d& pickPoint,
                    const OdGeMatrix3d& viewXform, int& numPaths,
                    OdDbFullSubentPath** subentPaths, int numInserts = 0,
                    OdDbObjectId* entAndInsertStack = NULL) const;
  
  virtual void getGsMarkersAtSubentPath( const OdDbFullSubentPath& subPath,
                                         OdDbIntArray& gsMarkers) const;
  
  virtual OdUInt32 numChanges() const;

  virtual OdDbEntity* subentPtr(const OdDbFullSubentPath& id) const;
  */
  
  virtual OdDbObjectPtr decomposeForSave(
    OdDb::DwgVersion ver, 
    OdDbObjectId& replaceId, 
    bool& exchangeXData);
  
  
  /** Description:
     Directly sets the contained ACIS geometry of this entity.
     Arguments:
     g (I) Pointer to the modeler geometry.
  */
  void setModelerGeometry( 
    OdModelerGeometry* g );

  virtual OdResult explode(
    OdRxObjectPtrArray& entitySet) const;

  virtual OdResult transformBy( 
    const OdGeMatrix3d& xfn );
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbBody object pointers.
*/
typedef OdSmartPtr<OdDbBody> OdDbBodyPtr;

#include "DD_PackPop.h"

#endif


