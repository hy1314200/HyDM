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



#ifndef _ODDBDATABASE_INCLUDED_
#define _ODDBDATABASE_INCLUDED_

#include "DD_PackPush.h"

#include "RxObject.h"
#include "DbObjectId.h"
#include "RxObjectImpl.h"
#include "OdString.h"
#include "DbDate.h"
#include "OdCodePage.h"
#include "OdToolKit.h"
#include "DbObject.h"
#include "CmColor.h"
#include "DbSecurity.h"
#include "DbSystemServices.h"
#include "ViewportDefs.h"

class OdDbObject;

class OdDbSymbolTable;
class OdDbBlockTable;
class OdDbTextStyleTable;
class OdDbLinetypeTable;
class OdDbLayerTable;
class OdDbViewTable;
class OdDbUCSTable;
class OdDbUCSTable;
class OdDbViewportTable;
class OdDbRegAppTable;
class OdDbDimStyleTable;
class OdDbDimStyleTableRecord;
class OdDbBlockTableRecord;
class OdDbTextStyleTableRecord;
class OdDbRegAppTableRecord;
class OdDbLinetypeTableRecord;
class OdDbHostAppServices;
class OdDbLayout;
class OdDbLayoutManagerReactor;
class OdGsView;
class OdGsDevice;
class OdGsDCRect;

class OdDbDictionary;
class OdDbDictionaryWithDefault;
class OdDbFiler;
class OdDbIdMapping;
class OdDbDatabaseReactor;
class OdDbSpatialFilter;
class OdDbLayerFilter;
class OdDbAuditInfo;
class OdDbUndoController;
class OdDbTransactionReactor;
class OdDbEntity;

class OdGePoint2d;
class OdGePoint3d;
class OdGeMatrix3d;
class OdGeVector3d;
class OdDbDwgFiler;

class OdGsModel;

class OdStreamBuf;
class OdThumbnailImage;

class OdDbDatabaseImpl;
class OdResBuf;
typedef OdSmartPtr<OdResBuf> OdResBufPtr;
typedef OdSmartPtr<OdDbDictionary> OdDbDictionaryPtr;
typedef OdSmartPtr<OdDbDictionaryWithDefault> OdDbDictionaryWithDefaultPtr;
typedef OdSmartPtr<OdDbSymbolTable> OdDbSymbolTablePtr;
typedef OdSmartPtr<OdDbBlockTable> OdDbBlockTablePtr;
typedef OdSmartPtr<OdDbLayerTable> OdDbLayerTablePtr;
typedef OdSmartPtr<OdDbTextStyleTable> OdDbTextStyleTablePtr;
typedef OdSmartPtr<OdDbViewTable> OdDbViewTablePtr;
typedef OdSmartPtr<OdDbUCSTable> OdDbUCSTablePtr;
typedef OdSmartPtr<OdDbViewportTable> OdDbViewportTablePtr;
typedef OdSmartPtr<OdDbRegAppTable> OdDbRegAppTablePtr;
typedef OdSmartPtr<OdDbDimStyleTable> OdDbDimStyleTablePtr;
typedef OdSmartPtr<OdDbLinetypeTable> OdDbLinetypeTablePtr;
typedef OdSmartPtr<OdDbBlockTableRecord> OdDbBlockTableRecordPtr;
typedef OdSmartPtr<OdDbDimStyleTableRecord> OdDbDimStyleTableRecordPtr;
typedef OdSmartPtr<OdDbTextStyleTableRecord> OdDbTextStyleTableRecordPtr;
typedef OdSmartPtr<OdDbRegAppTableRecord> OdDbRegAppTableRecordPtr;
typedef OdSmartPtr<OdDbDimStyleTableRecord> OdDbDimStyleTableRecordPtr;
typedef OdSmartPtr<OdDbLinetypeTableRecord> OdDbLinetypeTableRecordPtr;

class OdSecurityParams;
typedef OdSmartPtr<OdSecurityParams> OdSecurityParamsPtr;

class OdFileDependencyManager;
typedef OdSmartPtr<OdFileDependencyManager> OdFileDependencyManagerPtr;

typedef OdSmartPtr<OdDbDatabase> OdDbDatabasePtr;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum UnitsValue
  {
    kUnitsUndefined    = 0,
    kUnitsInches       = 1,
    kUnitsFeet         = 2,
    kUnitsMiles        = 3,
    kUnitsMillimeters  = 4,
    kUnitsCentimeters  = 5,
    kUnitsMeters       = 6,
    kUnitsKilometers   = 7,
    kUnitsMicroinches  = 8,
    kUnitsMils         = 9,
    kUnitsYards        = 10,
    kUnitsAngstroms    = 11,
    kUnitsNanometers   = 12,
    kUnitsMicrons      = 13,
    kUnitsDecimeters   = 14,
    kUnitsDekameters   = 15,
    kUnitsHectometers  = 16,
    kUnitsGigameters   = 17,
    kUnitsAstronomical = 18,
    kUnitsLightYears   = 19,
    kUnitsParsecs      = 20,
    kUnitsMax          = kUnitsParsecs
  };

  enum SaveType
  {
    kDwg,
    kDxf,
    kDxb
  };

  enum EndCaps
  {
    kEndCapNone       =  0,
    kEndCapRound      =  1,
    kEndCapAngle      =  2,
    kEndCapSquare     =  3
  };

  enum JoinStyle
  {
    kJnStylNone       =  0,
    kJnStylRound      =  1,
    kJnStylAngle      =  2,
    kJnStylFlat       =  3
  };

  enum DuplicateLinetypeLoading
  {
    kDltNotApplicable = 0,
    kDltIgnore        = 1,
    kDltReplace       = 2
  };
}

/** Description:
    Represents the contents of an entire drawing. 

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDatabase : public OdDbObject
{
protected:

  OdDbDatabase();

public:

  void getClassID(void** pClsid) const;
  
  ODRX_DECLARE_MEMBERS(OdDbDatabase);

  /** Description:
      Increments the reference count for this object.
  */
  void addRef();
  
  /** Description:
      Decrements the reference count for this object, and deletes the object
      when no references remain.
  */
  void release();

  /** Description:
      Returns a pointer to the OdDbHostAppServices object associated with this database.
  */
  OdDbHostAppServices* appServices() const;

  /** Description:
      Adds a minimal default set of objects to the database.  This includes model space and
      paper space blocks, default table entries, etc.

      Arguments:
      measurement (I) TBC.
  */
  void initialize(OdDb::MeasurementValue measurement = OdDb::kEnglish);

  /** Description:
      Destructor.
  */
  virtual ~OdDbDatabase();

  /** Description:
      Adds an object to this database, which establishes an object ID for this object.

      Arguments:
      pObj (I) Pointer to object that will be added to this database.
      ownerId (I) Object ID of pObj's owner object.
      han (I) Handle value of pObj (a unique handle will be generated if this value
                     is not supplied, or is 0).

      Return Value:
      The object ID of the newly added object.
  */
  OdDbObjectId addOdDbObject(OdDbObject* pObj, 
                             OdDbObjectId ownerId = OdDbObjectId::kNull, 
                             OdDbHandle han = 0);

  /** Description:
      Adds a new entry to the APPID table of this database.

      Arguments:
      regAppName (I) Name of registered application to add to the APPID table.

      Return Value:
      True if the specified name has not been registered before and the
      entry was added successfully, false otherwise.
  */
  bool newRegApp(const OdString& regAppName);

  /** Description:
      Returns the BlockTable ID of this database.

      Return Value:
      This database's BlockTable object ID.
  */
  OdDbObjectId getBlockTableId() const;

  /** Description:
      Returns the LayerTable ID of this database.

      Return Value:
      This database's LayerTable object ID.
  */
  OdDbObjectId getLayerTableId() const;

  /** Description:
      Returns the TextStyleTable ID of this database.

      Return Value:
      This database's TextStyleTable object ID.
  */
  OdDbObjectId getTextStyleTableId() const;

  /** Description:
      Returns the LinetypeTable ID of this database.

      Return Value:
      This database's LinetypeTable object ID.
  */
  OdDbObjectId getLinetypeTableId() const;

  /** Description:
      Returns the ViewTable ID of this database.

      Return Value:
      This database's ViewTable object ID.
  */
  OdDbObjectId getViewTableId() const;

  /** Description:
      Returns the UCSTable ID of this database.

      Return Value:
      This database's UCSTable object ID.
  */
  OdDbObjectId getUCSTableId() const;

  /** Description:
      Returns the ViewportTable ID of this database.

      Return Value:
      This database's ViewportTable object ID.
  */
  OdDbObjectId getViewportTableId() const;

  /** Description:
      Returns the RegAppTable ID of this database.

      Return Value:
      This database's RegAppTable object ID.
  */
  OdDbObjectId getRegAppTableId() const;

  /** Description:
      Returns the DimStyleTable ID of this database.

      Return Value:
      This database's DimStyleTable object ID.
  */
  OdDbObjectId getDimStyleTableId() const;

  /** Description:
      Returns the MLineStyle dictionary ID of this database.

      Return Value:
      This database's MLineStyle dictionary object ID.
  */
  OdDbObjectId getMLStyleDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns the Group dictionary ID of this database.

      Return Value:
      This database's Group dictionary object ID.
  */
  OdDbObjectId getGroupDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns the Layout dictionary ID of this database.

      Return Value:
      This database's Layout dictionary object ID.
  */
  OdDbObjectId getLayoutDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns the PlotStyleName dictionary ID of this database.

      Return Value:
      This database's PlotStyleName dictionary object ID.
  */
  OdDbObjectId getPlotStyleNameDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns the Named Objects dictionary ID of this database.

      Return Value:
      This database's Named Objects dictionary object ID.
  */
  OdDbObjectId getNamedObjectsDictionaryId() const;

  /** Description:
      Returns the PlotSettings dictionary ID of this database.

      Return Value:
      This database's PlotSettings dictionary object ID.
  */
  OdDbObjectId getPlotSettingsDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns the Color dictionary ID of this database.

      Return Value:
      This database's Color dictionary object ID.
  */
  OdDbObjectId getColorDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns the Material dictionary ID of this database.

      Return Value:
      This database's Material dictionary object ID.
  */
  OdDbObjectId getMaterialDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns the Table Style dictionary ID of this database.

      Return Value:
      This database's Table Style dictionary object ID.
  */
  OdDbObjectId getTableStyleDictionaryId(bool bCreate = true) const;

  /** Description:
      Returns current Table Style Id.
  */
  OdDbObjectId tablestyle() const;

  /** Description:
      Sets current Table Style Id.
  */
  void setTablestyle(OdDbObjectId id);

  /** Description:
      Returns the "ACAD" RegApp object ID of this database.
  */
  OdDbObjectId getRegAppAcadId() const;

  /** Description:
      Returns the "Continuous" Linetype object ID of this database.
  */
  OdDbObjectId getLinetypeContinuousId() const;

  /** Description:
      Returns the "ByLayer" Linetype object ID of this database.
  */
  OdDbObjectId getLinetypeByLayerId() const;

  /** Description:
      Returns the "ByBlock" Linetype object ID of this database.
  */
  OdDbObjectId getLinetypeByBlockId() const;

  /** Description:
      Returns the Model Space Block object ID of this database.
  */
  OdDbObjectId getModelSpaceId() const;

  /** Description:
      Returns the Paper Space Block object ID of this database.
  */
  OdDbObjectId getPaperSpaceId() const;

  /** Description:
      Returns the text style "Standard" object ID of this database.
  */
  OdDbObjectId getTextStyleStandardId() const;

  /** Description:
      Returns the dimension style "Standard" object ID of this database.
  */
  OdDbObjectId getDimStyleStandardId() const;

  /** Description:
      Returns the layer "0" object ID of this database.
  */
  OdDbObjectId getLayerZeroId() const;

  /** Description:
      Returns the layer "DEFPOINTS" object ID of this database.
  */
  OdDbObjectId getLayerDefpointsId(bool bCreateIfNotFound = false) const;

  /** Description:
      Returns the Class Dxf Name for the passed in class.

      Arguments:
        pClass (I) Pointer to class object.

      Return Value:
      Dxf Class Name of pClass.

      See Also:
      OdRxClass
  */
  const char* classDxfName(const OdRxClass* pClass);

  /** Description:
      Returns the object ID corresponding to this database handle.

      Arguments:
        objHandle (I) Database handle to create and object ID for.
        createIfNotFound (I) If true, an object ID for the specified handle will be
             created if one does not already exist.
        xRefId (I) Currently not used.

      Return Value:
      object ID corresponding to passed in handle.

      Remarks:
      If objHandle is 0, getOdDbObjectId will create a new unique handle and return a
      newly created object ID corresponding to this handle.

      See Also:
      OdDbObjectId
  */
  OdDbObjectId getOdDbObjectId(const OdDbHandle& objHandle,
                               bool createIfNotFound = false, 
                               OdUInt32 xRefId = 0);

  /** Description:
      Writes the contents of this database to the specified OdStreamBuf object.

      Arguments:
        pFileBuff (O) Receives the serialized contents of the database.
        type (I) Type of file to save.
        version (I) Version of file to save.
        saveThumbnailImage (I) If true, save a thumbnail image to the file, otherwise
             do not save a thumbnail.
        dxfPrecision (I) If DXF file is being saved - floating-point accuracy (0 - 16).

      Remarks:
      The type argument should be one of the following:

        o kDwg - Save as DWG.
        o kDxf - Save as DXF.

      The version argument should be one of the following for DWG:

        o vAC12
        o vAC13
        o vAC14
        o vAC15
        o vAC18

      and one of the following for DXF:

        o vAC09
        o vAC10
        o vAC12
        o vAC13
        o vAC14
        o vAC15
        o vAC18
      
     Exceptions:
     OdError if the write is unsuccessful.
  */
  void writeFile(OdStreamBuf* pFileBuff, OdDb::SaveType type, OdDb::DwgVersion version, 
                 bool saveThumbnailImage = false, int dxfPrecision = 16);

  /** Description:
      Loads the contents of the specified file into this database.

      Arguments:
      fileBuff (I/O) Pointer to OdStreamBuf object from which the data is to be read.
      bPartial (I) If true, a partial load of the file is performed (DWG files only).
      pAuditInfo (I/O) If non-0, a recover will be performed on the specified file 
             rather than a load, and this OdDbAuditInfo object will receive the output from the recover.
      
      Exceptions:
      OdError if the load is unsuccessful.
  */
  void readFile(OdStreamBuf* fileBuff,
                bool bPartial = false, 
                OdDbAuditInfo *pAuditInfo = 0, 
                const OdPassword& password = OdPassword(),
                bool bAllowCPConversion = false );

  /** Description:
      Loads the contents of the specified file into this database.

      Arguments:
      fileName (I) Name of the file from which the data is to be read.
      bPartial (I) If true, a partial load of the file is performed (DWG files only).
      shmode (I) Share mode to use when opening the specified file.
      @throw OdError if the load is unsuccessful.
  */
  void readFile(const char* fileName,
                bool bPartial = false, 
                Oda::FileShareMode shmode = Oda::kShareDenyWrite,
                const OdPassword& password = OdPassword(),
                bool bAllowCPConversion = false );

  /** Description:
      Forces all data to be loaded from the input data source associated with this database. 
      Client applications will normally not need to call this function.
  */
  void closeInput();

  /** Description:
      Returns a value guaranteed to be greater than or equal to the number of objects
      in this database.
  */
  OdInt32 approxNumObjects() const;

  /** Description:
      Returns the current version of this database.

      Arguments:
      pMaintVer (O) If non-0, receives the current maintenance version.
  */
  OdDb::DwgVersion version(OdDb::MaintReleaseVer* pMaintVer = 0) const;

  /** Description:
      Returns the number of times this database has been saved, which may be 0.
  */
  OdInt32 numberOfSaves() const;

  /** Description:
      Returns the file version that this database was last saved as.

      Arguments:
      pMaintVer (O) If non-0, receives the last saved maintenance version.
  */
  OdDb::DwgVersion lastSavedAsVersion(OdDb::MaintReleaseVer* pMaintVer = 0) const;

  /** Description:
      Returns the file type of the original source file used to create this database object.
  */
  OdDb::SaveType originalFileType() const;

  /** Description:
      Returns the file version of the original source file used to create this database object.

      Arguments:
      pMaintVer (O) If non-0, receives the original maintenance version.
  */
  OdDb::DwgVersion originalFileVersion(OdDb::MaintReleaseVer* pMaintVer = 0) const;

  /** Description:
      Returns the version of the application that originally created the file associated with
      this datase.

      Arguments:
      pMaintVer (O) If non-0, receives the associated maintenance version.
  */
  OdDb::DwgVersion originalFileSavedByVersion(OdDb::MaintReleaseVer* pMaintVer = 0) const;

  /** Description:
      Adds the specified reactor to this database's reactor list.

      Arguments:
      pReactor (I) Reactor to add.
  */
  void addReactor(OdDbDatabaseReactor* pReactor) const;

  /** Description:
      Removes the specified reactor from this database's reactor list.

      Arguments:
      pReactor (I) Reactor to remove.
  */
  void removeReactor(OdDbDatabaseReactor* pReactor) const;

  /** Description:
      Returns the obsolete DIMFIT system variable.

      Remarks:
      This function is useful for converting system variable data back to R14 or earlier.
  */
  int dimfit() const;

  /** Description:
      Returns the obsolete DIMUNIT system variable.

      Remarks:
      This function is useful for converting system variable data back to R14 or earlier.
  */
  int dimunit() const;

  /** Description:
      Sets the obsolete DIMFIT system variable.

      Remarks:
      The DIMFIT value set will be converted to DIMATFIT and DIMTMOVE values.
  */
  void setDimfit(int fit);

  /** Description:
      Sets the obsolete DIMUNIT system variable.

      Remarks:
      The DIMUNIT value set will be converted to DIMLUNIT and DIMFRAC values.
  */
  void setDimunit(int unit);

  /** Description:
      Clones a set of objects, and appends the clones to the passed in owner container.

      Arguments:
      objectIds (I) Object ID's of the objects to be cloned.
      owner (I/O) Object ID of owner object which will be given ownership of the clones.
      idMap (O) ID map that associates the object ID's from the original objects with
             the object ID's of the newly created clones.
      deferXlation (I) If true, then ID translation is not performed during cloning.
  */
  void deepCloneObjects(const OdDbObjectIdArray& objectIds,
                        OdDbObjectId owner, 
                        OdDbIdMapping& idMap, 
                        bool deferXlation = false);


  /** Description:
      Clones a set of objects, and appends the clones to the passed in owner container.

      Arguments:
      objectIds (I) Object ID's of the objects to be cloned.
      owner (I/O) Object ID of owner object which will be given ownership of the clones.
      idMap (I/O) ID map that associates the object ID's from the original objects with
             the object ID's of the newly created clones.
      drc (I) Action for duplicate records.
      deferXlation (I) If true, then ID translation is not performed during cloning.
  */
  void wblockCloneObjects(const OdDbObjectIdArray& objectIds,
                          OdDbObjectId owner, 
                          OdDbIdMapping& idMap,
                          OdDb::DuplicateRecordCloning drc, 
                          bool deferXlation = false);

  /** Description:
      A DDT program needs to call this function only if it has made calls 
      to deepCloneObjects() or wblockCloneObjects() with the deferXlation argument set to true and does not plan 
      to finish the operation by making a final call with deferXlation set to false.  
  */
  void abortDeepClone(OdDbIdMapping& idMap);

  /** Description:
      Audits the header information in this database, scanning for and optionally fixing any errors found.

      Arguments:
      pInfo (I/O) Provides information about the audit, and receives output 
             from the audit.
  */
  void audit(OdDbAuditInfo* pInfo);

  /** Description:
      Starts a new transaction for this database.
  */
  virtual void startTransaction();

  /** Description:
      Ends the current transaction associated with this database.
  */
  virtual void endTransaction();

  /** Description:
      Aborts the current transaction.
  */
  virtual void abortTransaction();

  /** Description:
      Returns the number of active transactions associated with this database.
  */
  virtual int numActiveTransactions();

  /** Description:
      Adds a transaction reactor to this database.
  */
  virtual void addTransactionReactor(OdDbTransactionReactor* reactor);

  /** Description:
      Removes the specified transaction reactor from this database.
  */
  virtual void removeTransactionReactor(OdDbTransactionReactor* reactor);


  /** Description:
      Returns the thumbnail bitmap associated with the database, in Windows BITMAPINFO*
      format.
  */
  const void* thumbnailBitmap(OdUInt32& nDataLength) const;

  /** Description:
      Sets the thumbnail bitmap associated with the database, in Windows BITMAPINFO*
      format.
  */
  void setThumbnailBitmap(const void* pBMPData, OdUInt32 nDataLength);

  /** Description:
      Returns the "retain original thumbnail bitmap flag".  TBC.
  */
  bool retainOriginalThumbnailBitmap() const;

  /** Description:
      Sets the "retain original thumbnail bitmap flag".  TBC.
  */
  void setRetainOriginalThumbnailBitmap(bool retain);

  /** Description:
      Writes out the DWG data for this object.

      Arguments:
      pFiler Filer object to which data is written.
  */
  void dwgOutFields(OdDbDwgFiler* pFiler) const;
  OdResult dwgInFields(OdDbDwgFiler* pFiler);

#define VAR_DEF(type, name, def_value, metric_def_value, reserve1, reserve2) \
  /** Description:
      Gets the name header variable. */ \
  type get##name() const;

#include "SysVarDefs.h"

#undef VAR_DEF
#undef RO_VAR_DEF

#define RO_VAR_DEF(type, name, def_value, metric_def_value, reserve1, reserve2)
#define VAR_DEF(type, name, def_value, metric_def_value, reserve1, reserve2) \
  /** Description:
      Sets the name header variable. */ \
  void set##name(type val);

#include "SysVarDefs.h"

#undef RO_VAR_DEF
#undef VAR_DEF

#define VAR_DEF(type, name, dxf, def_value, metric_def_value, reserve1, reserve2) \
  /** Description:
      Gets the dimension style name header variable. */ \
  type dim##name() const;\
  /** Description:
      Sets the dimension style name header variable. */ \
  void setDim##name(type val);

#include "DimVarDefs.h"

#undef VAR_DEF

  /** Description:
      Gets the local date the database was created.
  */
  OdDbDate getTDCREATE() const;

  /** Description:
      Gets the local date of the last update.
  */
  OdDbDate getTDUPDATE() const;

  /** Description:
      Returns the value of the specified header variable.

      Arguments:
      szName (I) Name of the header variable for which the value is to be retrieved, 
             for example EXTMAX, CHAMFERA, etc.

      Return Value:
      Smart pointer to an OdResBuf object that contains the value.
  */
  OdResBufPtr getSysVar(const char* szName) const;
  
  /** Description:
      Sets the value of the specified header variable.

      Arguments:
      szName (I) Name of the header variable for which the value is to be set, 
             for example EXTMAX, CHAMFERA, etc.
      pValue (I) Pointer to an OdResBuf object that contains the value.
  */
  void setSysVar(const char* szName, const OdResBuf* pValue);

  /** Description:
      Returns the next available unused handle value for this database. 
  */
  OdDbHandle handseed() const;

  /** Description:
      Copies the header dimension style data for this database into the passed in 
      OdDbDimStyleTableRecord.
  */
  void getDimstyleData(OdDbDimStyleTableRecord* pRes) const;

  /** Description:
      Copies the dimension style data from the passed in OdDbDimStyleTableRecord into
      this database's dimension style header variables.
  */
  void setDimstyleData(const OdDbDimStyleTableRecord* pRec);

  /** Description:
      Copies the dimension style data from the OdDbDimStyleTableRecord associated with
      the passed in object ID, into this database's dimension style header variables.
  */
  void setDimstyleData(OdDbObjectId id);

  /** Description:
      Loads a linetype into this database.

      Arguments:
      ltName (I) Name of the linetype to load. If "*" is passed as linetype name all
      linetypes from the file will be loaded.
      fileName (I) Name of the linetype file from which to load the 
      specified linetype.

      Return Value:
      eOk if successful, otherwise an appropriate error value.
  */
  void loadLineTypeFile(const OdChar* ltName, const OdChar* fileName, OdDb::DuplicateLinetypeLoading dlt = OdDb::kDltNotApplicable);

  /** Description:
      Returns the name of the file that was loaded into this database.
  */
  OdString getFilename() const;
  
  /** Description:
      Determines if there are hard pointer references in this database to
      any of the passed in object ID's.  object ID's for which hard references
      exist are removed from the passed in array, leaving only the object ID's 
      that are NOT hard referenced.  Upon return from this function, any 
      object ID's remaining in the passed in array can be safely erased from
      this database.  The database is not modified.

      Arguments:
      ids (I/O) Array of object ID's.
  */
  void purge(OdDbObjectIdArray& ids) const;

  /** Description:
      Computes a count of hard references to each of the object ID's in the 
      passed in array.  

      Arguments:
      ids (I) Array of object ID's for which hard reference counts will be computed.
      pCount (I) Array that will receive the hard reference counts for the objects in 
             the ids array.

      Remarks:
      and the caller should set the entries in pCount to 0 before calling this function.
  */
  void countHardReferences(const OdDbObjectIdArray& ids, OdUInt32* pCount) const;

  /** Description:
      Returns the object ID of the active layout for this database.
  */
  OdDbObjectId currentLayoutId() const;

  /** Description:
      Sets the current layout for this database.

      Arguments:
      newname (I) Name of layout to make current.
  */
  virtual void setCurrentLayout(const OdChar* newname);

  /** Description:
      Sets the current layout for this database.

      Arguments:
      layoutId (I) Object ID of layout to make current.
  */
  virtual void setCurrentLayout(const OdDbObjectId& layoutId);

  /** Description:
      Returns the name of the active layout for this database.

      Arguments:
      allowModel (I) If true, allow the ModelSpace layout name to be returned, otherwise
             an empty string will be returned if the active layout is ModelSpace.
  */
  virtual OdString findActiveLayout(bool allowModel) const;

  /** Description:
      Returns the object ID of the OdDbBlockTableRecord associated with the active
      layout of this database.
  */
  virtual OdDbObjectId getActiveLayoutBTRId() const;

  /** Description:
      Returns the object ID of the specified layout in this database, or a null
      object ID if the specified layout can not be found.
  */
  virtual OdDbObjectId findLayoutNamed(const OdString& name) const;

  /** Description:
      Deletes the specified layout from this database.
  */
  virtual void deleteLayout(const OdString& delname);

  /** Description:
      Creates a new layout with the specified name, and adds it to this database along
      with a new associated OdDbBlockTableRecord.  

      Arguments:
      newname (I) Name of the new layout.
      pBlockTableRecId (O) If non-0, receives the object ID of the newly created
             OdDbBlockTableRecord.
  */
  virtual OdDbObjectId createLayout(const OdString& newname, OdDbObjectId* pBlockTableRecId = 0);

  /** Description:
      Returns the number of layouts in this database.
  */
  virtual int countLayouts() const;

  virtual void renameLayout(const OdString& oldname, const OdString& newname);

  /** Description:
      Starts undo recording for this database.
  */
  void startUndoRecord();

  /** Description:
      Returns true if undo information exists for this database. 
  */
  bool hasUndo() const;

  /** Description:
      Performs an undo operation on this database.  All operations performed since the 
      last call to startUndoRecording will be undone.
  */
  void undo();

  /** Description:
      Returns true if redo information exists for this database.
  */
  bool hasRedo() const;

  /** Description:
      Performs a redo operation on this database. 
  */
  void redo();

  /** Description:
      Audits this entire database, scanning for and optionally fixing any errors found.

      Arguments:
      pInfo (I/O) Provides information about the audit, and receives output 
      from the audit.
  */
  void auditDatabase(OdDbAuditInfo *pInfo);

  void applyPartialUndo(OdDbDwgFiler* pFiler, OdRxClass* pClass);

  OdDbDwgFiler* undoFiler();
  
  /** Description:
      Returns object ID for the new block table record created by this function

      Arguments:
      sBlockName (I) name to be used by the new block table record created by this function.
      pDb (I) pointer to database to insert entities from.
      preserveSourceDatabase (I) determines whether the source database pDb will be left intact.

      Remarks:
      This function mimics the AutoCAD INSERT command. First a new block table record
      is created in the database executing this function. This new block table record
      is given the name sBlockName. Then, all the Model Space entities in the database
      pointed to by pDb are copied into the new block table record.
  */
  OdDbObjectId insert(const OdString& sBlockName, OdDbDatabase* pDb, bool bPreserveSourceDatabase = true);

  /** Description:
      Returns object ID for the new block table record created by this function

      Arguments:
      sSourceBlockName (I) name of block table record of source database to insert entities from.
      sDestinationBlockName (I) name to be used by the new block table record created by this function.
      pDb (I) pointer to database to insert entities from.
      preserveSourceDatabase (I) determines whether the source database pDb will be left intact.
  */
  OdDbObjectId insert(const OdString& sSourceBlockName,
                      const OdString& sDestinationBlockName,
                      OdDbDatabase* pDb,
                      bool bPreserveSourceDatabase = true);

  /** Description:
      Inserts the Model Space of the database pointed to by pDb into the Model Space of
      the database invoking the insert() function. 
      
      Remarks:
      All objects being inserted have the xform matrix passed into their transformBy() 
      function during the insertion process.

      Arguments:
      xform (I) transformation matrix applied to all objects being inserted.
      pDb (I) pointer to database to insert entities from.
      preserveSourceDatabase (I) determines whether the source database pDb will be left intact.
  */
  void insert(const OdGeMatrix3d& xform, OdDbDatabase* pDb, bool bPreserveSourceDatabase = true);

  OdDbDatabasePtr wblock(const OdDbObjectIdArray& outObjIds, const OdGePoint3d& basePoint);

  OdDbDatabasePtr wblock(OdDbObjectId blockId);

  OdDbDatabasePtr wblock();

  OdDbObjectPtr wblockClone(OdDbIdMapping& idMap) const;

  void setSecurityParams(const OdSecurityParams& secParams, bool bSetDbMod = true);

  /*
  Returns true if secParams.nFlags!=0 && secParams.password is not blank.
  */
  bool securityParams(OdSecurityParams& secParams) const;

  OdFileDependencyManagerPtr fileDependencyManager() const;

  void updateExt();

  /** Description:
      Returns true if this database was created by AutoCAD's educational version.
  */
  bool isEMR() const;

  OdDbObjectId xrefBlockId() const;

  bool isPartiallyOpened() const;

  void setCurrentUCS(OdDb::OrthographicView ortho);
  void setCurrentUCS(const OdDbObjectId& namedUCS);
  void setCurrentUCS(const OdGePoint3d& origin, const OdGeVector3d& xAxis, const OdGeVector3d& yAxis);

  OdGePoint3d getUCSBASEORG(OdDb::OrthographicView ortho) const;
  void setUCSBASEORG(OdDb::OrthographicView ortho, const OdGePoint3d& origin);

  OdGePoint3d getPUCSBASEORG(OdDb::OrthographicView ortho) const;
  void setPUCSBASEORG(OdDb::OrthographicView ortho, const OdGePoint3d& origin);

  /** Description:
      Do not use this method. It will be removed.
      Use header variable accessors instead.
  */
  void getCurrentUCS(OdGePoint3d& origin, OdGeVector3d& xAxis, OdGeVector3d& yAxis) const;
  /*  
  bool plotStyleMode() const;

  static bool isValidLineWeight(int weight);

  static OdDb::LineWeight getNearestLineWeight(int weight);

  void forceWblockDatabaseCopy();
 
  void auditXData(OdDbAuditInfo* pInfo);

  OdDbUndoController* undoController() const;

  void restoreOriginalXrefSymbols();

  void restoreForwardingXrefSymbols();

  void setDimblk(const OdChar*);
  void setDimblk1(const OdChar*);
  void setDimblk2(const OdChar*);
  void setDimldrblk(const OdChar*);

  void getDimstyleChildData(const OdRxClass *pDimClass,
                            OdDbDimStyleTableRecordPtr& pRec,
                            OdDbObjectId &style) const;

  OdDbObjectId getDimstyleChildId(const OdRxClass *pDimClass,
                                  OdDbObjectId &parentStyle) const;

  OdDbObjectId getDimstyleParentId(OdDbObjectId &childStyle) const;

  void getDimRecentStyleList(OdDbObjectIdArray& objIds) const;

  void applyPartialOpenFilters(const OdDbSpatialFilter* pSpatialFilter,
                               const OdDbLayerFilter* pLayerFilter);

  void disablePartialOpen();

  void newFingerprintGuid();

  void newVersionGuid();

  double viewportScaleDefault() const;

  void setViewportScaleDefault(double newDefaultVPScale);

  OdDbObjectId getPaperSpaceVportId() const;

  virtual void copyLayout(const OdChar* copyname, const OdChar* newname);

  virtual void cloneLayout(const OdDbLayout* pLBTR, const OdChar* newname, int newTabOrder);

  virtual OdDbObjectId getNonRectVPIdFromClipId(const OdDbObjectId& clipId);

  virtual bool isViewportClipped(short index);

  */
  
private:
  friend class OdDbDatabaseImpl;
  OdDbDatabaseImpl* m_pImpl;
};


/** Fills in OdThumbnailImage object from stream. Throws appropriate exception if an error occurred.
*/
TOOLKIT_EXPORT void odDbGetPreviewBitmap(OdStreamBuf* pFileBuff, OdThumbnailImage* pPreview);


// The functions below provide write access to "read-only" database variables.
// Actually they are a kludge for bypassing non-implemented DD functionality
// or to repair invalid drawings.
// They should be used with care.

TOOLKIT_EXPORT void odDbSetDWGCODEPAGE(OdDbDatabase& db, OdCodePageId cp);
TOOLKIT_EXPORT void odDbSetTDUCREATE(OdDbDatabase& db, OdDbDate date);
TOOLKIT_EXPORT void odDbSetTDUUPDATE(OdDbDatabase& db, OdDbDate date);
TOOLKIT_EXPORT void odDbSetTDINDWG(OdDbDatabase& db, OdDbDate date);
TOOLKIT_EXPORT void odDbSetTDUSRTIMER(OdDbDatabase& db, OdDbDate date);
TOOLKIT_EXPORT void odDbSetPSTYLEMODE(OdDbDatabase& db, bool psmode);
TOOLKIT_EXPORT void odDbSetUCSORG(OdDbDatabase& db, OdGePoint3d org);
TOOLKIT_EXPORT void odDbSetUCSXDIR(OdDbDatabase& db, OdGeVector3d dir);
TOOLKIT_EXPORT void odDbSetUCSYDIR(OdDbDatabase& db, OdGeVector3d dir);
TOOLKIT_EXPORT void odDbSetPUCSORG(OdDbDatabase& db, OdGePoint3d org);
TOOLKIT_EXPORT void odDbSetPUCSXDIR(OdDbDatabase& db, OdGeVector3d dir);
TOOLKIT_EXPORT void odDbSetPUCSYDIR(OdDbDatabase& db, OdGeVector3d dir);


inline void OdDbDatabase::readFile(const char* fileName, bool bPartial, Oda::FileShareMode shmode, const OdPassword& password, bool bAllowCPConversion )
{
  readFile(odSystemServices()->createFile(fileName, Oda::kFileRead, shmode), bPartial, 0, password, bAllowCPConversion);
}


#include "DD_PackPop.h"

#endif /* _ODDBDATABASE_INCLUDED_ */


