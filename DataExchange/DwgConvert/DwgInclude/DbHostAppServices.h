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



#ifndef _ODDBHOSTAPPLICATIONSERVICES_INCLUDED_
#define _ODDBHOSTAPPLICATIONSERVICES_INCLUDED_

#include "DD_PackPush.h"

#include "SmartPtr.h"

class OdDbAuditInfo;
class OdDbDatabase;
class OdDbUndoController;
class OdDbGlobals;
class OdDbAppSystemVariables;
class COleClientItem;
class CArchive;
struct flagmat;
class OdGsDevice;

typedef OdSmartPtr<OdGsDevice> OdGsDevicePtr;
typedef OdSmartPtr<OdDbDatabase> OdDbDatabasePtr;

class OdDbAbstractClipBoundaryDefinition;
class ClipBoundaryArray;

class OdHatchPatternManager;

class OdDbPageController;
typedef OdSmartPtr<OdDbPageController> OdDbPageControllerPtr;


#include "DbLayoutManager.h"
#include "OdStreamBuf.h"
#include "DbDatabase.h"
#include "OdFont.h"
#include "OdBinaryData.h"
#include "DbSecurity.h"
#include "DbPlotSettingsValidator.h"

typedef unsigned long LCID;

enum OdSDIValues
{
  kMDIEnabled = 0,
  kSDIUserEnforced,
  kSDIAppEnforced,
  kSDIUserAndAppEnforced
};

enum ProdIdCode
{
  kProd_ACAD = 1,
  kProd_LT,
  kProd_OEM,
  kProd_OdDb
};

/** Description:
    Class that can receive progress notifications during various database operations, such
    as loading or saving a file.  
    
    Remarks:
    Calls to an OdDbHostAppProgressMeter instance will come in the following order:

      o OdDbHostAppProgressMeter::setLimit (called once).
      o OdDbHostAppProgressMeter::start (called once).
      o OdDbHostAppProgressMeter::meterProgress (called repeatedly).
      o OdDbHostAppProgressMeter::stop (called once).

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbHostAppProgressMeter
{
public:
  /** Description:
      Called to indicate that the operation associated with this progress meter has
      started.

      Arguments:
      displayString (I) Message pertaining to the type of operation being performed.
  */
  virtual void start(const char* displayString = NULL) = 0;

  /** Description:
      Called to indicate that the operation associated with this progress meter has stopped.
  */
  virtual void stop() = 0;

  /** Description:
      Called each time this meter's progress is updated.  By comparing the number of times
       this function is called, to the value set by setLimit, a completion percentage
       can be calculated. Throwing an exception from this function will terminate the associated
       operation.
       Throws exception if the associated operation should be halted.
  */
  virtual void meterProgress() = 0;

  /** Description:
      Called to indicate the maximum number of times meterProgress will be called during
      this operation.
   */
  virtual void setLimit(int max) = 0;
};

class OdDbUndoController;
typedef OdSmartPtr<OdDbUndoController> OdDbUndoControllerPtr;
class OdTtfDescriptor;

/** Description:
    Class that can be derived by clients to provide platform specific operations that are
    required by DWGdirect.  
    
    Remarks:
    These operations include file name resolution, load/save
    progress monitoring, etc.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbHostAppServices : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbHostAppServices);

  /** Description:
      Constructor (no arguments).
  */
  OdDbHostAppServices();

  enum RemapFileContext
  {
    kDrawingOpen,
    kXrefResolution,
    kRasterResolution
  };

  enum FindFileHint
  {
    kDefault = 0,
    kFontFile, /* Could be either */
    kCompiledShapeFile, /* shx */
    kTrueTypeFontFile, /* ttf */
    kEmbeddedImageFile,
    kXRefDrawing,
    kPatternFile,
    kDRXApplication,
    kFontMapFile
  };

  /** Description:
      Given a file name, returns a string value that uniquely identifies this file.
      This function is called by DWGdirect when access is needed to a
      file, such as a font file, an externally referenced file, etc.  
      Normally, the return value will be the full path of requested file.  
      However, the value returned
      by this function will be later passed to OdDbSystemServices::createFile, so 
      a client application can return any non-null string, as long as 
      the application can handle that string in OdDbSystemServices::createFile.

      Arguments:
      pcFilename (I) Name of the file to find.
      pDb (I) Database pointer.
      hint (I) Hint that indicates what type of file is required.
  */
  virtual OdString findFile(const char* pcFilename,
    OdDbDatabase* pDb = NULL,
    OdDbHostAppServices::FindFileHint hint = kDefault);

  /** Description:
      Returns a pointer to an instance of an OdDbHostAppProgressMeter.  DWGdirect will
      call this function whenever it requires a progress meter (for example, right before
      a file load operation).
   */
  virtual OdDbHostAppProgressMeter* newProgressMeter();

  /** Description:
      Called by DWGdirect to give the client a chance to free any resources associated
      with a progress meter.  DWGdirect will call this function when it no longer
      needs the specified OdDbHostAppProgressMeter instance.

      Arguments:
      pMeter (I) Progress meter instance no longer needed by DWGdirect.
  */
  virtual void releaseProgressMeter(OdDbHostAppProgressMeter* pMeter);

  virtual OdRxClass* databaseClass() const;

  /** Description:
      Creates an instance of an OdDbDatabase.  This OdDbHostAppServices instance
      will be associated with the newly created database.

      Arguments:
      bCreateDefault (I) If true, the newly created database will be initialized
             with a minimal set of objects (all tables, a model space and paper space block,
             default layer & linetypes, etc.).  If false, the newly created database will
             be empty.
      measurement (I) Measurement value to use for creating the database.

      Return Value:
      A smart pointer to the newly created database.
   */
  virtual OdDbDatabasePtr createDatabase(bool bCreateDefault = true,
    OdDb::MeasurementValue measurement = OdDb::kEnglish) const;

  /** Description:
      Loads the contents of the specified DWG/DXF file into the supplied database.

      Arguments:
      fileBuff (I) OdStreamBuf pointer used to access DWG/DXF file.
      bAllowCPConversion (I) Currently not used.
      bPartial (I) True if partial loading is desired, false otherwise.
      password - pointer to a "wide-char" string containing a DWG password. If pPassword is NULL, 
          the getPassword function will be called for password

      Return Value:
      Smart pointer to a database that contains the contents of the file.

      Remarks:
      If bPartial is true, the database will be partially loaded, database objects
      will be loaded only when they are accessed by the client, and the data source
      must remain open throughout the lifetime of the database.  If partial loading is
      not used, the data source may be closed immediately after readFile returns.

      See Also:
      OdStreamBuf
  */
  virtual OdDbDatabasePtr readFile(OdStreamBuf* fileBuff,
    bool bAllowCPConversion = false,
    bool bPartial = false,
    const OdPassword& password = OdPassword());

  /** Description:
      Peforms a recover operation on the specified DWG/DXF file.  Auditing information
      is sent to the specified OdDbAuditInfo object.

      Arguments:
      pFileBuff (I) Data source from which to read the file data.
      pAuditInfo (I) Audit information.
      password (I) password for opening a 2004+ password protected drawing. If this is
                 not provided, the getPassword() function will be called to obtain
                 a password.
  */
  virtual OdDbDatabasePtr recoverFile(OdStreamBuf* pFileBuff, 
    OdDbAuditInfo *pAuditInfo = NULL, 
    const OdPassword& password = OdPassword());

  /** Description:
      Loads the contents of the specified DWG/DXF file into the supplied database.

      Arguments:
      fileName (I) Path of the DWG/DXF file to read.
      bAllowCPConversion (I) Currently not used.
      bPartial (I) True if partial loading is desired, false otherwise.
      shmode (I) Sharing mode to use when opening the file.
      password (I) password for opening a 2004+ password protected drawing. If this is
                 not provided, the getPassword() function will be called to obtain
                 a password.

      Return Value:
      Smart pointer to a database that contains the contents of the file.

      Remarks:
      If bPartial is true, the database will be partially loaded, database objects
      will be loaded only when they are accessed by the client, and the data source
      must remain open throughout the lifetime of the database.  If partial loading is
      not used, the data source may be closed immediately after readFile returns.

      See Also:
      OdStreamBuf
  */
  virtual OdDbDatabasePtr readFile(const OdChar* fileName,
    bool bAllowCPConversion = false,
    bool bPartial = false,
    Oda::FileShareMode shmode = Oda::kShareDenyNo,
    const OdPassword& password = OdPassword());

  virtual const char* program();

  virtual const char* product();

  virtual const char* companyName();

  virtual ProdIdCode prodcode();

  /** Description:
      Returns a string indicating the major and minor version of this instance of DWGdirect.
  */
  virtual const char* releaseMajorMinorString();

  virtual int releaseMajorVersion();

  virtual int releaseMinorVersion();

  virtual const char* versionString();

  virtual void warning(const OdString& sMsg) { odSystemServices()->warning(sMsg); };

  virtual void warning(OdWarning warn) { warning(getErrorDescription(warn)); }

  virtual void warning(OdWarning warning, OdDbObjectId id);

  /** Description:
      Returns the error description associated with the specified error code.
   */
  virtual OdString getErrorDescription(unsigned int code);

  /** Description:
      Returns a formatted message.
   */
  virtual OdString formatMessage(unsigned int,...);
#ifdef ODA_GCC
  virtual OdString formatMessage(unsigned int id, long i, const OdString& s) { return formatMessage(id, i, (void*)s.c_str()); }
#endif

  virtual bool doFullCRCCheck();

  virtual OdDbUndoControllerPtr newUndoController();

  /** Description:
      Print the audit info

      Arguments:
      pAuditInfo (I) Information about this audit so far (as already documented).
      strLine (I) The string to print.
      nPrintDest (I)
   */
  virtual void auditPrintReport(OdDbAuditInfo * pAuditInfo, const OdChar* strLine, int nPrintDest) const;

  /** Description:
      Returns the OdDbPlotSettingsValidator - class for transforming
      plotsetting's values.
  */
  virtual OdDbPlotSettingsValidator* plotSettingsValidator();

  /** Description:
      Takes a description of a Truetype font, and attempts to locate the .TTF or .TTC 
      file that contains the font information for this font. 
      See ExHostAppServices::ttfFileNameByDescriptor for a sample implementation of this 
      function.

      Return Value:
      Returns true if the file name was found, false otherwise.
  */
  virtual bool ttfFileNameByDescriptor(const OdTtfDescriptor& descr, OdString& fileName) = 0;

#define REGVAR_DEF(type, name, unused3, unused4, unused5) \
protected:type  m_##name;\
public:\
  virtual type get##name() const;\
  virtual void set##name(type val);
#include "SysVarDefs.h"
#undef REGVAR_DEF

  /*
  virtual const char* getRegistryProductRootKey();
  virtual LCID getRegistryProductLCID();
  void* internalGetVar(const char *name);
  virtual bool isURL(const char* pszURL) const;
  virtual bool isRemoteFile(const char* pszLocalFile, char* pszURL) const;
  virtual OdResult getRemoteFile(const char* pszURL,
    char* pszLocalFile, bool bIgnoreCache = false) const;
  virtual OdResult putRemoteFile(const char* pszURL,
    const char* pszLocalFile) const;
  virtual bool launchBrowserDialog(char* pszSelectedURL,
    const char* pszDialogTitle, const char* pszOpenButtonCaption,
    const char* pszStartURL, const char* pszRegistryRootKey = 0,
    bool bOpenButtonAlwaysEnabled = false) const;
  virtual void drawOleOwnerDrawItem(COleClientItem* pItem, long hdc,
    long left, long top, long right, long bottom);
  virtual void displayChar(char c) const;
  virtual void displayString(const char* string, int count) const;
  virtual bool readyToDisplayMessages();
  virtual void enableMessageDisplay(bool);
  virtual unsigned int getTempPath(OdUInt32 lBufferSize, char* pcBuffer);
  virtual const char* getEnv(const char* var);
  void setWorkingProgressMeter(OdDbHostAppProgressMeter* pNewMeter);
  virtual OdDbAbstractClipBoundaryDefinition* newClipBoundaryRectangular();
  virtual OdDbAbstractClipBoundaryDefinition* newClipBoundaryPolygonal();
  virtual ClipBoundaryArray* newClipBoundaryArray();
  OdDbAppSystemVariables* workingAppSysvars() const;
  void setWorkingAppSysvars(OdDbAppSystemVariables* pSysvars);
  virtual OdResult getNewOleClientItem(COleClientItem*& pItem);
  virtual OdResult serializeOleItem(COleClientItem* pItem, CArchive*);
  virtual bool entToWorldTransform(double normal[3], flagmat *tran);
  virtual void terminateScript();
  virtual void alert(const char* string) const;
  virtual bool cacheSymbolIndices() const { return false; }
  virtual void getDefaultPlotCfgInfo(const char* plotDeviceName, const char* plotStyleName);
  */

  /** Description:
      Returns the alternate or default font file name to substitute for fonts that are 
      not found by DWGdirect during the normal font locating mechanism.
      
      See Also:
      Font Handling
  */
  virtual OdString getAlternateFontName() const;

  /** Description:
      Returns the name of a font mapping file used by the getPreferableFont function.

      See Also:
      Font Handling
  */
  virtual OdString getFontMapFileName() const;

  /** Description:
      This function is called as the first step in the process of resolving a font file.
      The default implementation tries to locate a font mapping file by calling
      getFontMapFileName, and substitutes the font name based on the contents of this 
      file.

      See Also:
      Font Handling
  */
  virtual OdString getPreferableFont(const OdString& nameFont, OdFontType fontType);

  /** Description:
      This function is called by DWGdirect when a required font is not found. The
      default implmentation of this function calls getAlternateFontName for
      fonts that are not of type kFontTypeShape or kFontTypeBig (for these types it 
      returns nothing).  The user can override this function to perform custom substitution
      for these types of fonts.
      
      See Also:
      Font Handling
  */
  virtual OdString getSubstituteFont(const OdString& nameFont, OdFontType fontType);

  virtual OdHatchPatternManager* patternManager() = 0;

  // BEGIN: DWG Security-related services
  virtual bool getPassword(const OdString& dwgName, bool isXref, OdPassword& password);
  virtual OdPwdCachePtr getPasswordCache();

  /** Description:
      Returns an object controlling the paging of database objects.

      Remarks:
      Returning a NULL smart pointer from this function indicates that no paging should 
      be performed.  If a valid OdDbPageController instance is returned, the 
      paging type will be determined by the overriden OdDbPageController::pagingType
      function in the returned instance.
  */
  virtual OdDbPageControllerPtr newPageController();

  /** Description:
      Launchs file browse dialog.
      Returns file path or one of predefined strings:
        o "*unsupported*"
        o "*canceled*"

      Remarks:
      Default implementation returns "*unsupported*".  
  */
  virtual OdString fileDialog(int nFlags, // OdEd::GetFilePathFlags
    const OdChar* prompt = 0, 
    const OdChar* defExt = 0,
    const OdChar* fileName = 0,
    const OdChar* filter = 0);

  virtual OdGsDevicePtr gsBitmapDevice() = 0;


protected:
  OdDbPlotSettingsValidatorPtr  m_pValidator;
  OdPwdCachePtr                 m_pPwdCache;
};


inline OdDbDatabasePtr OdDbHostAppServices::readFile(const char* fileName,
                                                     bool bAllowCPConversion,
                                                     bool bPartial,
                                                     Oda::FileShareMode shmode,
                                                     const OdPassword& password)
{
  Oda::FileAccessMode nDesiredAccess = Oda::kFileRead;
  return readFile(odSystemServices()->createFile(fileName, nDesiredAccess, shmode), bAllowCPConversion, bPartial, password);
}

#include "DD_PackPop.h"

#endif /* _ODDBHOSTAPPLICATIONSERVICES_INCLUDED_ */




