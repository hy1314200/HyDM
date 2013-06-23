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



////////////////////////////////////////////////////////////
// Error codes definition container

//        Code                Message string
OD_ERROR_DEF( eOk,                "No error")

// Codes returned by dxfIn functions
OD_ERROR_DEF( eMakeMeProxy,       "Make me proxy")
OD_ERROR_DEF( eInvalidDrawing,    "Invalid Drawing")

OD_ERROR_DEF( eNotImplementedYet, "Not implemented yet")
OD_ERROR_DEF( eNotApplicable,     "Not Applicable")
OD_ERROR_DEF( eInvalidInput,      "Invalid input")
OD_ERROR_DEF( eInvalidFiler,      "Invalid Filer")
//  eAmbiguousInput            =  4,
//  eAmbiguousOutput           =  5,
OD_ERROR_DEF( eOutOfMemory,       "Out of memory")
OD_ERROR_DEF( eNoInterface,       "No Interface")
//  eBufferTooSmall            =  7,
OD_ERROR_DEF( eInvalidOpenState,  "Invalid open state")
OD_ERROR_DEF( eUnsupportedMethod, "Unsupported Method")
//  eEntityInInactiveLayout    =  9,
OD_ERROR_DEF( eDuplicateHandle,   "Handle already exists")
//  eNullHandle                = 11,
//  eBrokenHandle              = 12,
OD_ERROR_DEF( eUnknownHandle,     "Unknown handle")
//  eHandleInUse               = 14,
OD_ERROR_DEF(eNullObjectPointer,  "Null object pointer")
OD_ERROR_DEF(eNullObjectId,       "Null object Id")
//  eNullBlockName             = 17,
OD_ERROR_DEF(eContainerNotEmpty,  "Container is not empty")

//  eNullEntityPointer         = 20,
OD_ERROR_DEF( eIllegalEntityType, "Illegal entity type")
OD_ERROR_DEF( eKeyNotFound,       "Key Not Found")
OD_ERROR_DEF( eDuplicateKey,       "Duplicate Key")
OD_ERROR_DEF( eInvalidIndex,      "Invalid index")
OD_ERROR_DEF( eCharacterNotFound, "Character Not Found")
//  eDuplicateIndex            = 25,
OD_ERROR_DEF( eAlreadyInDb, "Already in database")
//  eOutOfDisk                 = 27,
//  eDeletedEntry              = 28,
//  eNegativeValueNotAllowed   = 29,
OD_ERROR_DEF( eInvalidExtents,    "Invalid Extents")
//  eInvalidAdsName            = 31,
OD_ERROR_DEF( eInvalidSymbolTableName,"Invalid Symbol Table Name")
OD_ERROR_DEF( eInvalidKey,            "Invalid Key")
OD_ERROR_DEF( eWrongObjectType,       "Wrong Object Type")
OD_ERROR_DEF( eWrongDatabase,         "Wrong Database")
//  eObjectToBeDeleted         = 36,
OD_ERROR_DEF( eInvalidDwgVersion, "Invalid Dwg Version")
//  eAnonymousEntry            = 38,
OD_ERROR_DEF( eIllegalReplacement, "Illegal Replacement")

//  eEndOfObject               = 40,

// File errors
OD_ERROR_DEF( eEndOfFile,       "Unexpected end of file")
//  eFileExists                = 362,
OD_ERROR_DEF( eCantOpenFile,    "Can't open file")
OD_ERROR_DEF( eFileCloseError,  "File close error")
OD_ERROR_DEF( eFileWriteError,  "File write error")
OD_ERROR_DEF( eNoFileName,      "No file name")
//  eFilerError                = 53,
//  eFileLockedByACAD          = 71,
//  eFileAccessErr
//  eFileSystemErr             = 73,
OD_ERROR_DEF( eFileInternalErr, "File internal error")
//  eFileTooManyOpen           = 75,
OD_ERROR_DEF( eFileNotFound,    "File not found")
OD_ERROR_DEF( eUnknownFileType, "Unknown File Type")


//  eIsReading                 = 42,
//  eIsWriting                 = 43,
OD_ERROR_DEF( eNotOpenForRead,  "Not opened for read")
OD_ERROR_DEF( eNotOpenForWrite, "Not opened for write")
OD_ERROR_DEF( eNotThatKindOfClass, "Not that kind of class")
OD_ERROR_DEF( eInvalidBlockName, "Invalid block name")
//  eMissingDxfField           = 48,
//  eDuplicateDxfField         = 49,


// Dxf read errors
OD_ERROR_DEF( eInvalidGroupCode,  "Invalid group code")
OD_ERROR_DEF( eInvalidResBuf,  "Invalid ResBuf")
OD_ERROR_DEF( eBadDxfSequence,  "Bad Dxf sequence")
OD_ERROR_DEF( eInvalidRoundTripR14Data,  "Invalid RoundTripR14 data")
OD_ERROR_DEF( eVertexAfterFace,  "Polyface Mesh vertex after face")
OD_ERROR_DEF( eInvalidVertexIndex,  "Invalid vertex index")
OD_ERROR_DEF( eDwgObjectImproperlyRead,  "Dwg Object Improperly Read")
//  eOtherObjectsBusy          = 57,
//  eMustFirstAddBlockToDb     = 58,
//  eCannotNestBlockDefs       = 59,

//  eDwgRecoveredOK            = 60,
//  eDwgNotRecoverable         = 61,
//  eDxfPartiallyRead          = 62,
//  eDxfReadAborted            = 63,
//  eDxbPartiallyRead          = 64,
OD_ERROR_DEF( eDwgCRCError,       "CRC does not match")
//  eDwgSentinelDoesNotMatch   = 66,
//  eDwgObjectImproperlyRead   = 67,
//  eNoInputFiler              = 68,
//  eDwgNeedsAFullSave         = 69,

//  eDxbReadAborted            = 70,
//  eDwkLockFileFound          = 77,

OD_ERROR_DEF( eWasErased,         "Object was erased")
OD_ERROR_DEF( ePermanentlyErased, "Object was permanently erased")
OD_ERROR_DEF( eWasOpenForRead,    "Was open for read")
OD_ERROR_DEF( eWasOpenForWrite,   "Was open for write")
OD_ERROR_DEF( eWasOpenForUndo,    "Was open for undo")
OD_ERROR_DEF( eWasNotifying,      "Was notifying")
OD_ERROR_DEF( eWasOpenForNotify,  "Was open for notify")
//  eOnLockedLayer             = 87,
//  eMustOpenThruOwner         = 88,
//  eSubentitiesStillOpen      = 89,

//  eAtMaxReaders              = 90,
//  eIsWriteProtected          = 91,
//  eIsXRefObject              = 92,
//  eNotAnEntity               = 93,
//  eHadMultipleReaders        = 94,
OD_ERROR_DEF( eInvalidBlkRecordName,  "Invalid Block table record name")
OD_ERROR_DEF( eDuplicateRecordName,   "Duplicate record name")
OD_ERROR_DEF( eNotXrefBlock,          "Block is not an external reference definition")
OD_ERROR_DEF( eEmptyRecordName,       "Empty record name")
OD_ERROR_DEF( eXRefDependent,         "Block depend on other XRefs")
OD_ERROR_DEF( eSelfReference,         "Entity references itself")
OD_ERROR_DEF( eMissingSymbolTable,    "Missing symbol table")
OD_ERROR_DEF( eMissingSymbolTableRec, "Missing symbol table record")

//  eWasNotOpenForWrite        = 100,
//  eCloseWasNotifying         = 101,
//  eCloseModifyAborted        = 102,
//  eClosePartialFailure       = 103,
//  eCloseFailObjectDamaged    = 104,
OD_ERROR_DEF( eCannotBeErased,        "Object can't be erased")
//  eCannotBeResurrected       = 106,

//  eInsertAfter               = 110,

//  eFixedAllErrors            = 120,
//  eLeftErrorsUnfixed         = 122,
//  eUnrecoverableErrors       = 123,
OD_ERROR_DEF( eNoDatabase,    "No Database")
OD_ERROR_DEF( eXdataSizeExceeded, "XData size exceeded")
OD_ERROR_DEF( eCannotSaveHatchRoundtrip, "Cannot save hatch roundtrip data due to format limitations (they are too large)")
OD_ERROR_DEF( eHatchHasInconsistentPatParams, "Hatch is gradient, but either solid fill flag not set or pattern type is not pre-defined")
OD_ERROR_DEF( eRegappIdNotFound,  "Invalid RegApp")
//  eRepeatEntity              = 127,
OD_ERROR_DEF( eRecordNotInTable,  "Record not in table")
//  eIteratorDone              = 129,
OD_ERROR_DEF(eNullIterator,  "Null iterator")
OD_ERROR_DEF(eNotInBlock,  "Not in block")
OD_ERROR_DEF(eOwnerNotInDatabase,     "Owner not in database")
//  eOwnerNotOpenForRead       = 133,
//  eOwnerNotOpenForWrite      = 134,
//  eExplodeBeforeTransform    = 135,
OD_ERROR_DEF( eCannotScaleNonOrtho, "Cannot transform by non-ortho matrix")
OD_ERROR_DEF( eCannotScaleNonUniformly, "Cannot transform by non-uniform scaling matrix")
OD_ERROR_DEF( eNotInDatabase,  "Object not in database")
//  eNotCurrentDatabase        = 138,
//  eIsAnEntity                = 139,

OD_ERROR_DEF(eCannotChangeActiveViewport, "Cannot change properties of active viewport!")
OD_ERROR_DEF(eNotInPaperspace, "No active viewport in paperspace")
//  eCommandWasInProgress      = 142,

OD_ERROR_DEF( eGeneralModelingFailure, "General modeling failure")
OD_ERROR_DEF( eOutOfRange,            "Out of range")
OD_ERROR_DEF( eNonCoplanarGeometry,  "Non coplanar geometry")
OD_ERROR_DEF( eDegenerateGeometry,   "Degenerate geometry")
OD_ERROR_DEF( eInvalidAxis,           "Invalid axis")
//  ePointNotOnEntity          = 155,
//OD_ERROR_DEF(eSingularPoint, "Singular point")
//  eInvalidOffset             = 157,
//  eNonPlanarEntity           = 158,
OD_ERROR_DEF( eCannotExplodeEntity,   "Can not explode entity")

OD_ERROR_DEF( eStringTooLong,         "String too long")
//  eInvalidSymTableFlag       = 161,
OD_ERROR_DEF( eUndefinedLineType,     "Undefined line type")
OD_ERROR_DEF( eInvalidTextStyle,      "Text style is invalid")
//  eTooFewLineTypeElements    = 164,
//  eTooManyLineTypeElements   = 165,
//  eExcessiveItemCount        = 166,
//  eIgnoredLinetypeRedef      = 167,
//  eBadUCS                    = 168,
//  eBadPaperspaceView         = 169,

//  eSomeInputDataLeftUnread   = 170,
//  eNoInternalSpace           = 171,
OD_ERROR_DEF( eInvalidDimStyle,       "Invalid dimension style")
OD_ERROR_DEF( eInvalidLayer,          "Invalid layer")
OD_ERROR_DEF( eInvalidMlineStyle,     "Multiline style is invalid")
OD_ERROR_DEF( eDwgNeedsRecovery,      "Dwg file needs recovery")
OD_ERROR_DEF( eRecoveryFailed,        "Recovery failed")

//  eDeleteEntity              = 191,
//  eInvalidFix                = 192,
//  eFSMError                  = 193,

OD_ERROR_DEF( eBadLayerName,          "Bad layer name")
//  eLayerGroupCodeMissing     = 201,
OD_ERROR_DEF( eBadColorIndex,         "Bad color index")
OD_ERROR_DEF( eBadLinetypeName,       "Bad linetype name")
//  eBadLinetypeScale          = 204,
//  eBadVisibilityValue        = 205,
//  eProperClassSeparatorExpected = 206,
OD_ERROR_DEF( eBadLineWeightValue,    "Bad line weight value")

//  ePagerError                = 210,
//  eOutOfPagerMemory          = 211,
//  ePagerWriteError           = 212,
//  eWasNotForwarding          = 213,

//  eInvalidIdMap              = 220,
OD_ERROR_DEF( eInvalidOwnerObject,     "Invalid Owner Object")
OD_ERROR_DEF( eOwnerNotSet,            "Owner Not Set")

//  eWrongSubentityType        = 230,
//  eTooManyVertices           = 231,
//  eTooFewVertices            = 232,

OD_ERROR_DEF(eNoActiveTransactions, "No Active Transactions")
OD_ERROR_DEF(eTransactionIsActive,  "Transaction Is Active")
//  eNotTopTransaction         = 251,
//  eTransactionOpenWhileCommandEnded =  252,
//  eInProcessOfCommitting     = 253,
OD_ERROR_DEF(eNotNewlyCreated,      "Not newly created object")
//  eLongTransReferenceError   = 255,
//  eNoWorkSet                 = 256,

OD_ERROR_DEF(eAlreadyInGroup, "Entity already in group")
OD_ERROR_DEF(eNotInGroup,     "There is no entity with this id in group")
OD_ERROR_DEF(eBadDwgFile,     "Bad Dwg File")

//  eInvalidREFIID             = 290,
//  eInvalidNormal             = 291,
//  eInvalidStyle              = 292,

//  eCannotRestoreFromAcisFile = 300,
//  eNLSFileNotAvailable       = 302,
//  eNotAllowedForThisProxy    = 303,
OD_ERROR_DEF(eNotAllowedForThisProxy, "Not allowed for this proxy")

//  eNotSupportedInDwgApi      = 310,
//  ePolyWidthLost             = 311,
OD_ERROR_DEF( eNullExtents,    "Null Extents")
//  eExplodeAgain              = 313,
//  eBadDwgHeader              = 314,

//  eLockViolation             = 320,
//  eLockConflict              = 321,
//  eDatabaseObjectsOpen       = 322,
//  eLockChangeInProgress      = 323,

//  eVetoed                    = 325,

OD_ERROR_DEF(eNoDocument,      "ODAX no document")
//  eNotFromThisDocument       = 331,
//  eLISPActive                = 332,
//  eTargetDocNotQuiescent     = 333,
//  eDocumentSwitchDisabled    = 334,
OD_ERROR_DEF(eInvalidContext,  "Invalid context of execution")

//  eCreateFailed              = 337,
//  eCreateInvalidName         = 338,
OD_ERROR_DEF(eSetFailed,       "Seting active layout failed")
//  eDelDoesNotExist           = 342,
OD_ERROR_DEF(eDelIsModelSpace, "Model Space layout can't be deleted")
OD_ERROR_DEF(eDelLastLayout,   "Last Paper Space layout can't be deleted")
//  eDelUnableToSetCurrent     = 345,
//  eDelUnableToFind           = 346,
//  eRenameDoesNotExist        = 348,
//  eRenameIsModelSpace        = 349,
OD_ERROR_DEF(eRenameInvalidLayoutName,   "Invalid layout name")
OD_ERROR_DEF(eRenameLayoutAlreadyExists, "Layout already exists")
//  eRenameInvalidName         = 352,
//  eCopyDoesNotExist          = 354,
//  eCopyIsModelSpace          = 355,
//  eCopyFailed                = 356,
//  eCopyInvalidName           = 357,
//  eCopyNameExists            = 358,

//  eProfileDoesNotExist       = 359,
//  eInvalidProfileName        = 361,
//  eProfileIsInUse            = 363,
//  eRegistryAccessError       = 366,
//  eRegistryCreateError       = 367,

//  eBadDxfFile                = 368,
//  eUnknownDxfFileFormat      = 369,
//  eMissingDxfSection         = 370,
//  eInvalidDxfSectionName     = 371,
//  eNotDxfHeaderGroupCode     = 372,
//  eUndefinedDxfGroupCode     = 373,
OD_ERROR_DEF(eNotInitializedYet, "Not initialized yet")
//  eInvalidDxf2dPoint         = 375,
//  eInvalidDxf3dPoint         = 376,
//  eBadlyNestedAppData        = 378,
//  eIncompleteBlockDefinition = 379,
//  eIncompleteComplexObject   = 380,
//  eBlockDefInEntitySection   = 381,
OD_ERROR_DEF(eNoBlockBegin,      "No block begin")
//  eDuplicateLayerName        = 383,
OD_ERROR_DEF(eBadPlotStyleName, "Bad plotstyle name")
//  eDuplicateBlockName        = 385,
//  eBadPlotStyleType          = 386,
//  eBadPlotStyleNameHandle    = 387,
//  eUndefineShapeName         = 388,
//  eDuplicateBlockDefinition  = 389,
//  eMissingBlockName          = 390,
//  eBinaryDataSizeExceeded    = 391,
OD_ERROR_DEF(eObjectIsReferenced,   "Object is referenced")
OD_ERROR_DEF(eInvalidThumbnailBitmap,  "Invalid thumbnail bitmap")
//  eGuidNoAddress             = 394,

//  eMustBe0to2                = 395,    // setDimxxx() returns
//  eMustBe0to3                = 396,
//  eMustBe0to4                = 397,
//  eMustBe0to5                = 398,
//  eMustBe0to8                = 399,
//  eMustBe1to8                = 400,
//  eMustBe1to15               = 401,
//  eMustBePositive            = 402,
//  eMustBeNonNegative         = 403,
//  eMustBeNonZero             = 404,
//  eMustBe1to6                = 405,

//  eNoPlotStyleTranslationTable   = 406,
//  ePlotStyleInColorDependentMode = 407,
//  eMaxLayouts                = 408,
//  eNoClassId                 = 409,

OD_ERROR_DEF(eNoClassId, "No ClassId")
OD_ERROR_DEF(eUndoOperationNotAvailable,"Undo operation is not available")
//  eUndoNoGroupBegin          = 411,

OD_ERROR_DEF(eHatchTooDense, "Hatch is too dense - ignoring")

//  eOpenFileCancelled         = 430,
OD_ERROR_DEF(eNotHandled,     "Not Handled")
OD_ERROR_DEF(eNotImplemented, "Not Implemented")
OD_ERROR_DEF(eLibIntegrityBroken, "Library integrity is broken")

  // Here was internet-related groupcodes

  // OdDbCustomOsnapManager specific
//  eAlreadyActive,
//  eAlreadyInactive
OD_ERROR_DEF(eCodepageNotFound, "Codepage not found")
OD_ERROR_DEF(eIncorrectInitFileVersion, "Incorrect init file version")
OD_ERROR_DEF(eInternalFreetypeError, "Internal error in Freetype font library")
OD_ERROR_DEF(eNoUCSPresent, "No UCS present in object")
OD_ERROR_DEF(eBadObjType, "Object has wrong type")
OD_ERROR_DEF(eBadProtocolExtension, "Protocol extension object is bad")
OD_ERROR_DEF(eHatchInvalidPatternName, "Bad name for hatch pattern")

OD_ERROR_DEF(eNotTransactionResident, "Object is not transaction resident")

OD_ERROR_DEF(eDwgFileIsEncrypted,       "Dwg file is encrypted")
OD_ERROR_DEF(eInvalidPassword,          "The password is incorrect")
OD_ERROR_DEF(eDecryptionError,          "HostApp cannot decrypt data")
OD_ERROR_DEF(eExtendedError,            "Extended error")
OD_ERROR_DEF(eArithmeticOverflow,       "An arithmetic overflow")

OD_ERROR_DEF(eSkipObjPaging,            "Paging skips the object")
OD_ERROR_DEF(eStopPaging,               "Paging is stoped")

OD_ERROR_DEF(eInvalidDimStyleResBufData, "Invalid ResBuf with DimStyle data")

OD_ERROR_DEF(eDirectXError,              "Function of DirectX API returned some error")

