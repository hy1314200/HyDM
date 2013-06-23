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
// Message codes definition container


//              Code                        Message string
OD_MESSAGE_DEF( sidLayoutNo,                "Layout%d")
OD_MESSAGE_DEF( sidDwgFile,                 "Dwg file")
OD_MESSAGE_DEF( sidDb,                      "Database")
OD_MESSAGE_DEF( sidDbHeader,                "Database header")
OD_MESSAGE_DEF( sidObjectsAudited,          "%d objects audited") // param: Number of objects
OD_MESSAGE_DEF( sidErrorsFound,             "Total errors found during audit %d, fixed %d") // param: Number of errors
OD_MESSAGE_DEF( sidDictionary,              "%s Dictionary") // param: Dictionary name
OD_MESSAGE_DEF( sidTable,                   "%s Table") // param: Table name
OD_MESSAGE_DEF( sidDbLayerZeroMissed,       "Layer Zero missed")
OD_MESSAGE_DEF( sidDbLayerZeroBadIndex,     "Layer Zero has invalid index %d")
OD_MESSAGE_DEF( sidDbLtMissed,              "Linetype %s missed") // param: line type name
OD_MESSAGE_DEF( sidDbLtContsBadIndex,       "Linetype Continuous has invalid index %d")
OD_MESSAGE_DEF( sidDbRegAppTableMissed,     "RegApp Table missed")
OD_MESSAGE_DEF( sidDbRegAppAcadMissed,      "RegApp ACAD missed")
OD_MESSAGE_DEF( sidDbRegAppAcadBadIndex,    "RegApp ACAD has invalid index %d")
OD_MESSAGE_DEF( sidDbTxtStStandardMissed,   "TextStyle Standard missed")
OD_MESSAGE_DEF( sidDbDimStStandardMissed,   "DimStyle Standard missed")
OD_MESSAGE_DEF( sidDbMlStStandardMissed,    "MlineStyle Standard missed")
OD_MESSAGE_DEF( sidDbVpActiveMissed,        "Viewport *Active missed")
OD_MESSAGE_DEF( sidDbLtByBlockName,         "Linetype ByBlock has invalid name \"%s\"")
OD_MESSAGE_DEF( sidDbLtByBlockInList,       "Linetype ByBlock placed in ordinary list")
OD_MESSAGE_DEF( sidDbLtByLayerName,         "Linetype ByLayer has invalid name %s")
OD_MESSAGE_DEF( sidDbLtByLayerInList,       "Linetype ByLayer placed in ordinary list")
OD_MESSAGE_DEF( sidDbNamedObjectsDictionaryMissed,"Named objects dictionary missed")
OD_MESSAGE_DEF( sidDbDictionaryMissed,      "%s dictionary missed")   // param: dictionary name (ACAD_MLSTYLE, ACAD_GROUP, ...)
OD_MESSAGE_DEF( sidInvalidLayoutId,         "LayoutId %s is invalid") // param: LayoutId
OD_MESSAGE_DEF( sidInvalidLayoutAssoc,      "Layout %s associated with invalid BlockTableRecord") // param: LayoutId
OD_MESSAGE_DEF( sidInvalidLayoutBackPtr,    "Layout %s associated with BlockTableRecord %s that doesn't point back") // param: LayoutId, RecordId
OD_MESSAGE_DEF( sidSysVar,                  "System Variable \"%s\"") // param: Name of system variable
OD_MESSAGE_DEF( sidLayoutActVp,             "LastActiveVportId %s is invalid")
OD_MESSAGE_DEF( sidInvalidObjectId,         "ObjectId is invalid")
OD_MESSAGE_DEF( sidIsNotTextStyleRec,       "Is not a TextStyleTableRecord")
OD_MESSAGE_DEF( sidIsNotInTable,            "Is not in table")
OD_MESSAGE_DEF( sidIsShapeFile,             "Is Shape file")
OD_MESSAGE_DEF( sidVxRecRef,                "VXTableRecord reference")
// General validation
OD_MESSAGE_DEF( sidVarValidDouble,          "%g")
OD_MESSAGE_DEF( sidVarValidInt,             "%d")
OD_MESSAGE_DEF( sidVarValidPositive,        ">0")
OD_MESSAGE_DEF( sidVarValidZeroPositive,    ">=0")
OD_MESSAGE_DEF( sidVarValidNegative,        "<0")
OD_MESSAGE_DEF( sidVarValidZeroNegative,    "<=0")
OD_MESSAGE_DEF( sidVarValidRangeInt,        "%d..%d")
OD_MESSAGE_DEF( sidVarValidRangeDouble,     "%g..%g")
OD_MESSAGE_DEF( sidVarValidRangeDegree,     "%f..%f degree")
OD_MESSAGE_DEF( sidVarValidMinInt,          ">=%d")
OD_MESSAGE_DEF( sidVarValidMaxInt,          "<=%d")
OD_MESSAGE_DEF( sidVarValidInvalid,         "Invalid")
OD_MESSAGE_DEF( sidVarValid,                "%s")
OD_MESSAGE_DEF( sidVarValidInts2,           "%d,%d")
OD_MESSAGE_DEF( sidVarValidInts4,           "%d,%d,%d,%d")
OD_MESSAGE_DEF( sidVarValidDisparity,       "Disparity")
// General default values
OD_MESSAGE_DEF( sidVarDefDouble,            "%g")
OD_MESSAGE_DEF( sidVarDefInt,               "%d")
OD_MESSAGE_DEF( sidVarDefObj,               "%s")
OD_MESSAGE_DEF( sidVarDefDegree,            "%f degree")
OD_MESSAGE_DEF( sidVarDefColor,             "%d")
OD_MESSAGE_DEF( sidVarDefLayer,             "%s")
OD_MESSAGE_DEF( sidVarDefLinetype,          "%s")
OD_MESSAGE_DEF( sidVarDefZAxis,             "0.0 0.0 1.0")
OD_MESSAGE_DEF( sidVarDefViewMode,          "Keep 5 bits")
OD_MESSAGE_DEF( sidVarDefFalse,             "False")
OD_MESSAGE_DEF( sidVarDefTrue,              "True")
OD_MESSAGE_DEF( sidVarDefNull,              "Null")
OD_MESSAGE_DEF( sidVarDefDefault,           "Default")
OD_MESSAGE_DEF( sidVarDefCreate,            "Create")
OD_MESSAGE_DEF( sidVarDefRemove,            "Remove")
OD_MESSAGE_DEF( sidVarDefUpdate,            "Update")
OD_MESSAGE_DEF( sidVarDefRepair,            "Repair")
OD_MESSAGE_DEF( sidVarDefUnerase,           "Un-erase")
OD_MESSAGE_DEF( sidVarDefLayerColorMethod,  "kByACI")
// Color
OD_MESSAGE_DEF( sidColorInvalidIndex,       "Color index %d is invalid")
OD_MESSAGE_DEF( sidColorInvalidMethod,      "Color method %d is invalid")
OD_MESSAGE_DEF( sidValidColorIndices,       "1..255, ByBlock, ByLayer")
OD_MESSAGE_DEF( sidValidLayerColorIndices,  "1..255")
OD_MESSAGE_DEF( sidValidColorMethods,       "ByLayer, ByBlock, ByPen, Foreground, "
                                            "LayerOff, LayerFrozen, None, ByColor, ByACI")
// Objects
OD_MESSAGE_DEF( sidObjProp,                 "%s") // param: Name of object property
OD_MESSAGE_DEF( sidObjPropInt,              "%s is invalid - %d")
OD_MESSAGE_DEF( sidObjPropDouble,           "%s is invalid - %g")
OD_MESSAGE_DEF( sidObjPropStr,              "%s is invalid - %s")
OD_MESSAGE_DEF( sidObjPropColor,            "Color value is invalid")
OD_MESSAGE_DEF( sidObjPropLayerId,          "LayerId %s is invalid")
OD_MESSAGE_DEF( sidObjPropLinetypeId,       "LinetypeId %s is invalid")
OD_MESSAGE_DEF( sidObjPropLinetypeName,     "Linetype name \"%s\" is invalid")
OD_MESSAGE_DEF( sidObjPropPlotStyleNameId,  "PlotStyleNameId %s is invalid")
OD_MESSAGE_DEF( sidObjPropReactorId,        "Reactor id %s is invalid")
OD_MESSAGE_DEF( sidObjPropExtDicId,         "Extension dictionary id %s is invalid")
OD_MESSAGE_DEF( sidObjPropOwnerId,          "Owner id %s is invalid")
OD_MESSAGE_DEF( sidObjPropClassVersion,     "Invalid class version %d")
OD_MESSAGE_DEF( sidXDataRegAppId,           "XData item RegApp \"%s\" is invalid")
OD_MESSAGE_DEF( sidXDataHandle,             "XData handle %s is invalid")
OD_MESSAGE_DEF( sidXDataInvalidGrcode,      "XData contains invalid groupcode %d")
OD_MESSAGE_DEF( sidInvalidChildId,          "Unable to open entry object %s")
OD_MESSAGE_DEF( sidDicClonFlag,             "Duplicate record cloning (merge) flag (%d) is invalid")
OD_MESSAGE_DEF( sidObjPropColorByLayer,     "ByLayer")
OD_MESSAGE_DEF( sidObjPropPlotStByLayer,    "ByLayer")

// SortEntsTable
OD_MESSAGE_DEF( sidFoundDuplicateKeys,      "Duplicate keys were found")

// Hatch
OD_MESSAGE_DEF( sidHatchAssocInvalid,       "Associative object %s is invalid")
OD_MESSAGE_DEF( sidHatchAssocHatch,         "Associative object %s is Hatch")
OD_MESSAGE_DEF( sidHatchAssocReactor,       "Associative object %s missing Hatch as reactor")
OD_MESSAGE_DEF( sidHatchRemoveAssoc,        "Remove Associativity")
OD_MESSAGE_DEF( sidHatchAssocFlag,          "Associative flag is true but associative objects are missing")
OD_MESSAGE_DEF( sidHatchNoPatternDef,       "No hatch pattern definition")
OD_MESSAGE_DEF( sidHatchConflictGradFlags,  "Hatch is gradient, but either not solid or pattern type is not pre-defined")
OD_MESSAGE_DEF( sidHatchGradFlagsFixed,     "Solid flag was set to true. Pattern type was set to pre-defined.")
// MText
OD_MESSAGE_DEF( sidMTextLinespacingStyle,   "Line spacing style (%d) is invalid")
OD_MESSAGE_DEF( sidMTextAttachPt,           "Attachment point (%d) is invalid")
OD_MESSAGE_DEF( sidMTextDrawDir,            "Drawing direction (%d) is invalid")
OD_MESSAGE_DEF( sidMTextLinespacingFactor,  "Line spacing factor (%g) is invalid")
OD_MESSAGE_DEF( sidMTextXDir,               "X-axis direction is invalid")
OD_MESSAGE_DEF( sidMTextExtDic,             "Extension dictionary entry \"%s\" is invalid %s")
// MLine
OD_MESSAGE_DEF( sidMLineJustification,      "Justification (%d) is invalid")
OD_MESSAGE_DEF( sidMLineScale,              "Scale (%g) is invalid")
OD_MESSAGE_DEF( sidMLineStyleId,            "MLineStyleId %s is invalid")
OD_MESSAGE_DEF( sidMLineStyleNumElements,   "The number of elements in style (%d) is invalid")
OD_MESSAGE_DEF( sidMLineStFillColor,        "Fill color")
OD_MESSAGE_DEF( sidMLineStStartAngle,       "Start angle %g is invalid")
OD_MESSAGE_DEF( sidMLineStEndAngle,         "End angle %g is invalid")
OD_MESSAGE_DEF( sidMLineStSegmentsNum,      "Segments amount %d is invalid")
OD_MESSAGE_DEF( sidMLineStElemLinetypeId,   "Element(%d) has invalid linetype id %s")
OD_MESSAGE_DEF( sidMLineStElemColor,        "Element(%d) has invalid color")
OD_MESSAGE_DEF( sidMLineStyleName,          "MLine Style name \"%s\" is invalid")

// Raster Image
OD_MESSAGE_DEF( sidNumClipPoints,           "Number of points in clipping boundary %d")
OD_MESSAGE_DEF( sidImgInvalidReactorId,     "An image entity's reactor \"%s\" is invalid")

// View, Vp
OD_MESSAGE_DEF( sidViTilemodeCorners,       "Tilemode Viewport Corners (%g,%g..%g,%g) is invalid")
OD_MESSAGE_DEF( sidViTilemodeCornersValid,  "Fill unit square")
OD_MESSAGE_DEF( sidViTilemodeCornersDef,    "0.0,0.0..1.0,1.0")
OD_MESSAGE_DEF( sidViSnapXIncr,             "Snap X increment %g is invalid")
OD_MESSAGE_DEF( sidViSnapYIncr,             "Snap Y increment %g is invalid")
OD_MESSAGE_DEF( sidViGridXIncr,             "Grid X increment %g is invalid")
OD_MESSAGE_DEF( sidViGridYIncr,             "Grid Y increment %g is invalid")
OD_MESSAGE_DEF( sidViViewWidth,             "ViewWidth %g is invalid")
OD_MESSAGE_DEF( sidViViewHeight,            "ViewHeight %g is invalid")
OD_MESSAGE_DEF( sidViLensLength,            "LensLength %g is invalid")
OD_MESSAGE_DEF( sidViViewDirection,         "ViewDirection is invalid")
OD_MESSAGE_DEF( sidViViewMode,              "ViewMode %d is invalid")
OD_MESSAGE_DEF( sidViRenderMode,            "RenderMode %d is invalid")
OD_MESSAGE_DEF( sidVpHeight,                "Height (%g) is invalid")
OD_MESSAGE_DEF( sidVpWidth,                 "Width (%g) is invalid")
// Tables, recs
OD_MESSAGE_DEF( sidTblVxOneEmptyRec,        "There is only one empty VX record")
OD_MESSAGE_DEF( sidTblBlockBeginInvalid,    "BlockBeginId is invalid")
OD_MESSAGE_DEF( sidTblBlockBeginErased,     "BlockBeginId is erased")
OD_MESSAGE_DEF( sidTblBlockBeginNotOurs,    "BlockBegin has invalid OwnerId %s")
OD_MESSAGE_DEF( sidTblBlockEndInvalid,      "BlockEndId is invalid")
OD_MESSAGE_DEF( sidTblBlockEndErased,       "BlockEndId is erased")
OD_MESSAGE_DEF( sidTblBlockEndNotOurs,      "BlockEnd has invalid OwnerId %s")
OD_MESSAGE_DEF( sidTblBlockInvalidSortents, "ObjectId %s is not an OdDbSortentsTable object")
OD_MESSAGE_DEF( sidRecNameEmpty,            "Record name is empty")
OD_MESSAGE_DEF( sidRecNameInvalidChars,     "Invalid character(s) in record name")
OD_MESSAGE_DEF( sidRecCommentInvalidChars,  "Invalid character(s) in record comment")
OD_MESSAGE_DEF( sidRecDuplicateName,        "Duplicate record name \"%s\"")
OD_MESSAGE_DEF( sidRecXRefBlockInvalid,     "Is externally dependent on an xref, but XRef BlockRecord (%s) is invalid")
OD_MESSAGE_DEF( sidRecXRefBlockIdNotNull,   "Is not externally dependent on an xref, but XRefBlockId (%s) is not Null")
OD_MESSAGE_DEF( sidRecXRefBlockIdInvalid,   "Is externally dependent on an xref, but XRefBlockId (%s) is invalid")
OD_MESSAGE_DEF( sidRecXRefInvalidFlag,      "Is not externally dependent on an xref, but XRefBlockId (%s) is valid and name has vertical bar")
OD_MESSAGE_DEF( sidRecXRefDepNameInvalid,   "Is externally dependent on an xref, but name (%s) has no vertical bar")
OD_MESSAGE_DEF( sidRecNameVertBar,          "Non XRef-dependent record contains vertical bar.")
OD_MESSAGE_DEF( sidRecRemoveDependence,     "Remove dependence")
OD_MESSAGE_DEF( sidRecAttrDefFalse,         "hasAttributeDefinitions flag is false, but record has AttributeDefinitions")
OD_MESSAGE_DEF( sidRecAttrDefTrue,          "hasAttributeDefinitions flag is true, but record has no AttributeDefinitions")
OD_MESSAGE_DEF( sidRecAttrDefAlwaysFalse,   "Layout BlockTableRecord has alerted hasAttributeDefinitions flag")
OD_MESSAGE_DEF( sidRecLtSegmentsNum,        "Dash Count Less than 2")
OD_MESSAGE_DEF( sidRecLtBadSegmentType,     "Bad complex linetype element type")
OD_MESSAGE_DEF( sidRecLtContPattern,        "Continuous pattern length is not zero")
OD_MESSAGE_DEF( sidRecTxtStLastHeight,      "Last height %g is invalid")
OD_MESSAGE_DEF( sidRecTxtStScaleFactor,     "Scale factor %g not in range")
OD_MESSAGE_DEF( sidRecTxtStTextSize,        "Text size %g not in range")
OD_MESSAGE_DEF( sidRecVxNullVp,             "Viewport entity id is Null")
OD_MESSAGE_DEF( sidRecVxBadVp,              "Viewport entity id is invalid")
OD_MESSAGE_DEF( sidUnknownSysVar,           "Unknown System Variable %s")
OD_MESSAGE_DEF( sidUnknownSymbolTable,      "Unknown Symbol Table %s")
OD_MESSAGE_DEF( sidIgnored,                 "Ignored")


// Dimension
OD_MESSAGE_DEF( sidInvalidDimOverridesData, "Invalid dimension overrides data with group code %d")
OD_MESSAGE_DEF( sidInvalidDimOverrides,     "Invalid dimension overrides")
OD_MESSAGE_DEF( sidDimBlockRotation,        "Invalid dimension block rotation (%g)")
// Leader
OD_MESSAGE_DEF( sidLeaderAnntEnbl,          "Annotation enabled but id is Null")
OD_MESSAGE_DEF( sidLeaderAnntDisbl,         "Annotation disabled but id is Not Null")
OD_MESSAGE_DEF( sidLeaderAnntDisable,       "Disable annotation")
OD_MESSAGE_DEF( sidLeaderAnntToNull,        "Set id to Null")
OD_MESSAGE_DEF( sidLeaderAnntId,            "Annotation id %s is invalid")
OD_MESSAGE_DEF( sidDimStyle,                "Dimension Style %s")
// Text
OD_MESSAGE_DEF( sidTextHeight,              "Height (%g) is invalid")
OD_MESSAGE_DEF( sidTextWidthFactor,         "WidthFactor (%g) is invalid")
OD_MESSAGE_DEF( sidTextObliqueAngle,        "ObliqueAngle (%g) is invalid")
OD_MESSAGE_DEF( sidTextTextStyle,           "Text Style (%s) is invalid")
// DimAssoc
OD_MESSAGE_DEF( sidDimAssocDimId,           "Dimension id %s is invalid")
// Group
OD_MESSAGE_DEF( sidGroupEntryReactor,       "Entity %s in group does not have group as persistent reactor")
OD_MESSAGE_DEF( sidGroupDefAddReactor,      "Add persistent reactor")
// Polylines
OD_MESSAGE_DEF( sidPolyVertNumErr,          "Has only %d vertices")
OD_MESSAGE_DEF( sidPolyVertNumAdd,          "%d vertices added")
OD_MESSAGE_DEF( sidPolyVertLayerMatch,      "Layer %s doesn't match to owner")
OD_MESSAGE_DEF( sidPolyVertLinetypeMatch,   "Linetype %s doesn't match to owner")
OD_MESSAGE_DEF( sidPolyVertColorMatch,      "Color %d doesn't match to owner")
OD_MESSAGE_DEF( sidPolyVertValid,           "Should match to owner")
OD_MESSAGE_DEF( sidPolylineSurfType,        "Curves and smooth surface type (%d) is invalid")
OD_MESSAGE_DEF( sidPolyBulgeErr,            "Has %d vertices, but %d bulges")
OD_MESSAGE_DEF( sidPolyBulgeValid,          "bulges >= vertices")
OD_MESSAGE_DEF( sidPolyBulgeDef,            "Fit bulges to vertices")
OD_MESSAGE_DEF( sidPolyWidthErr,            "Has %d vertices, but %d widths")
OD_MESSAGE_DEF( sidPolyWidthValid,          "widths >= vertices")
OD_MESSAGE_DEF( sidPolyWidthDef,            "Fit widths to vertices")
// Containers
OD_MESSAGE_DEF( sidCntrEntryInvalid,        "Entry object %s is invalid")
OD_MESSAGE_DEF( sidCntrCrossRefInvalid,     "Cross references %s in entities list is invalid")
OD_MESSAGE_DEF( sidCntrLastRefMatch,        "Last entity %s does not match to last reference %s in container")
OD_MESSAGE_DEF( sidCntrEntryTypeInvalid,    "Invalid use of entity %s")
OD_MESSAGE_DEF( sidCntrSubentTypeInvalid,   "Invalid subentity type %s")
OD_MESSAGE_DEF( sidPolyfaceMeshVertexIndex, "Invalid vertex index")
OD_MESSAGE_DEF( sidPolyfaceMeshHasNoFaces,  "Polyface Mesh has no valid faces")
OD_MESSAGE_DEF( sidPolyfaceMeshVertexAfterFace, "Polyface Mesh vertex after face")
OD_MESSAGE_DEF( sidPolyfaceMeshVertexNumInvalid, "Polyface Mesh vertex's number is invalid")
OD_MESSAGE_DEF( sidPolyfaceMeshFaceNumInvalid  , "Polyface Mesh face's number is invalid")

// Block reference
OD_MESSAGE_DEF( sidBlkRefBlockRecErased,    "BlockTableRecord %s is erased")
OD_MESSAGE_DEF( sidBlkRefBlockRecInvalid,   "BlockTableRecord %s is invalid")
OD_MESSAGE_DEF( sidMissingBlockRecord,      "BlockTableRecord %s is missed")

OD_MESSAGE_DEF( sidBlkRefScaleHasZero,      "Scale have zero component (%g %g %g)")
OD_MESSAGE_DEF( sidBlkRefScaleValid,        "Scale must have no zero components")

// Shapes
OD_MESSAGE_DEF( sidUnknownShapeName,        "Can't resolve Shape name \"%s\"")
OD_MESSAGE_DEF( sidNullShapeFile,           "Shape style (font file) is not set")
OD_MESSAGE_DEF( sidNullShapeNumber,         "Shape number is not set")
OD_MESSAGE_DEF( sidCantGetShapeName,        "Can't get Shape name without \"%s\"")

// Ellipse
OD_MESSAGE_DEF( sidNormalDirectionIsInvalid,"Normal direction (%g %g %g) is invalid")
OD_MESSAGE_DEF( sidPerpToMajor,             "Must be perpendicular to Major Axis")
OD_MESSAGE_DEF( sidAdjusted,                "Adjusted")
OD_MESSAGE_DEF( sidEllipseRatio,            "Ratio %lf is invalid")
OD_MESSAGE_DEF( sidEllipseEqualAngle,       "Start angle (%lf) and end angle are equal (degenerate geometry)")

// Spline
OD_MESSAGE_DEF( sidCoincidentControlPoints, "Control points of spline is coincident")
OD_MESSAGE_DEF( sidInvalidKnotVector,       "Knot vector of spline is invalid")

// XRecord
OD_MESSAGE_DEF( sidXRecordInvalidGrcode,    "XRecord contains invalid groupcode")

// MInsertBlock
OD_MESSAGE_DEF( sidMInsertNumRows,          "Number of rows (%d) is invalid")
OD_MESSAGE_DEF( sidMInsertNumColumns,       "Number of columns (%d) is invalid")

// 2d Entities
OD_MESSAGE_DEF( sidNormalized,              "Normalized")
OD_MESSAGE_DEF( sidThickness,               "Thickness")
OD_MESSAGE_DEF( sidZeroed,                  "Set to zero")

// Recover
OD_MESSAGE_DEF( sidRecvStart,               "Recover dwg file.")
OD_MESSAGE_DEF( sidRecvSecLocHeader,        "Header section-locator is invalid")
OD_MESSAGE_DEF( sidRecvSecLocClasses,       "Classes section-locator is invalid")
OD_MESSAGE_DEF( sidRecvSecLocObjectMap,     "ObjectMap section-locator is invalid")
OD_MESSAGE_DEF( sidRecvSecLoc,              "Section-locator records is invalid")
OD_MESSAGE_DEF( sidRecvBadCrc,              "CRC does not match in %s")
OD_MESSAGE_DEF( sidRecvSkip,                "Skip")
OD_MESSAGE_DEF( sidRecvBadAddr,             "%s address %08X is invalid")
OD_MESSAGE_DEF( sidRecvFileTooShort,        "File too short")
OD_MESSAGE_DEF( sidRecvFailed,              "Failed")
OD_MESSAGE_DEF( sidRecvFinishFailed,        "Recovery failed.")
OD_MESSAGE_DEF( sidRecvStartAudit,          "Audit recovered database.")
OD_MESSAGE_DEF( sidRecvRestoreObjectMap,    "Handle table (ObjectMap) was reconstructed from objects.")
OD_MESSAGE_DEF( sidRecvTotalObs,            "Total objects in the handle table found %d.")
OD_MESSAGE_DEF( sidRecvLoadedObs,           "Loaded objects %d (with errors %d). Invalid objects %d.")
OD_MESSAGE_DEF( sidRecvErrorsFound,         "Total errors found during recover: %d.")
OD_MESSAGE_DEF( sidRecvDbHeaderErr,         "Database header has errors")
OD_MESSAGE_DEF( sidRecvSections,            "Sections")
OD_MESSAGE_DEF( sidRecvObjectMap,           "ObjectMap")
OD_MESSAGE_DEF( sidRecvHeader,              "Header")
OD_MESSAGE_DEF( sidRecv2ndHeader,           "Second header")
OD_MESSAGE_DEF( sidRecvClasses,             "Classes")
OD_MESSAGE_DEF( sidRecvInvalidHandseed,     "Database handseed (%s) is less than next available handle (%s)")
OD_MESSAGE_DEF( sidRecvObject,              "Object %s")
OD_MESSAGE_DEF( sidRecvObjFreeSpaceBadSize, "ObjFreeSpace data does not match section-locator ObjFreeSpace size")
OD_MESSAGE_DEF( sidRecvObjFreeSpaceErr,     "ObjFreeSpace section is invalid")
OD_MESSAGE_DEF( sidRecvTemplateErr,         "Template section is invalid")
OD_MESSAGE_DEF( sidRecvPreviewImgErr,       "Preview image is invalid")
OD_MESSAGE_DEF( sidRecvUnknownSecErr,       "Unknown section is invalid")
OD_MESSAGE_DEF( sidRecv2ndHdrErr,           "Second header after ObjFreeSpace data is invalid")
OD_MESSAGE_DEF( sidRecv2ndHdrAddrErr,       "Second header by address from file header (ObjFreeSpace end) is invalid")
OD_MESSAGE_DEF( sidRecvSs2ndHdr,            "Start sentinel of SecondHeader not found")
OD_MESSAGE_DEF( sidRecvResize,              "Resize")
OD_MESSAGE_DEF( sidRecvReconstruct,         "Reconstruct")
OD_MESSAGE_DEF( sidRecvObjectMapErr,        "ObjectMap has errors")
OD_MESSAGE_DEF( sidRecvRecover,             "Recover")
OD_MESSAGE_DEF( sidRecvAddObj,              "Add")
OD_MESSAGE_DEF( sidRecvBadRemainsEntry,     "Remains entry is invalid")
OD_MESSAGE_DEF( sidRecvClassMissed,         "Class %s is missed")
OD_MESSAGE_DEF( sidRecvAddClass,            "Add class")
OD_MESSAGE_DEF( sidRecvClassListErr,        "Classes list is invalid")
OD_MESSAGE_DEF( sidRecvObjInvalid,          "Object %s is invalid")
OD_MESSAGE_DEF( sidRecvObjInvalidOffset,    "Object %s has invalid offset %08X")
OD_MESSAGE_DEF( sidRecvObjInvalidSize,      "Object %s has invalid size %d")
OD_MESSAGE_DEF( sidRecvObjAddrInvalidSize,  "Object at address %08X has invalid size %d")
OD_MESSAGE_DEF( sidRecvObjHandleUnmatched,  "Object %s has handle different from ObjectMap Handle %s")
OD_MESSAGE_DEF( sidRecvObjInvalidData,      "Object %s has invalid data")
OD_MESSAGE_DEF( sidRecvSkippedRec,          "Record %s is skipped")
OD_MESSAGE_DEF( sidRecvRootObjInvalid,      "Root object %s is invalid") // class name
OD_MESSAGE_DEF( sidRecvBadRootTables,       "Unable to recover root tables")
OD_MESSAGE_DEF( sidRecvBadDictionary,       "%s (%s) is invalid")  // 1 - dic class, 2 - dic name
OD_MESSAGE_DEF( sidRecvBadRecClass,         "Invalid class type of %s")
OD_MESSAGE_DEF( sidRecvBadMlineStandard,    "MlineStyle Standard is invalid")
OD_MESSAGE_DEF( sidRecvBadPlotStNormal,     "PlotStyle Normal is invalid")
OD_MESSAGE_DEF( sidRecvRegAppAcad,          "RegApp ACAD is invalid")
OD_MESSAGE_DEF( sidRecvLinetypeByBlock,     "LinetypeByBlock is invalid")
OD_MESSAGE_DEF( sidRecvLinetypeByLayer,     "LinetypeByLayer is invalid")
OD_MESSAGE_DEF( sidRecvLinetypeContinuous,  "LinetypeContinuous is invalid")
OD_MESSAGE_DEF( sidRecvLayerZero,           "LayerZero is invalid")
OD_MESSAGE_DEF( sidRecvPaperSpaceLayout,    "PaperSpace has no Layout")
OD_MESSAGE_DEF( sidRecvModelSpaceLayout,    "ModelSpace has no Layout")
OD_MESSAGE_DEF( sidRecvPaperSpaceInvalid,   "PaperSpace is invalid")
OD_MESSAGE_DEF( sidRecvModelSpaceInvalid,   "ModelSpace is invalid")
OD_MESSAGE_DEF( sidRecvBadSectionAddr,      "Unable to recover address of section %s")
OD_MESSAGE_DEF( sidRecvGrDataLength,        "GrData of object %s has invalid length %d")
OD_MESSAGE_DEF( sidCantRestoreProxy,        "Can't restore %s from Proxy")
OD_MESSAGE_DEF( sidEmptyProxyEntity,        "Empty proxy entity %s")
// Recover R12
OD_MESSAGE_DEF( sidRecvHeaderErr,           "File header has errors")
OD_MESSAGE_DEF( sidRecv2ndHeaderErr,        "Second file header has errors")
OD_MESSAGE_DEF( sidRecvTableRecsErr,        "Table %s records section has error(s)")
OD_MESSAGE_DEF( sidRecvTableRecordErr,      "%s record (index = %d) has error: %s")
OD_MESSAGE_DEF( sidRecvEntityErr,           "Load entity %s on address %X error")
OD_MESSAGE_DEF( sidRecvDuplHandle,          "Duplicate or invalid handle %s")
OD_MESSAGE_DEF( sidRecvBadRecIndex,         "Invalid record index %d")
// Warnings
OD_MESSAGE_DEF( sidNoDimBlockGenerated,     "Dimension block missed. %s will be removed.") // param: Dimension description
OD_MESSAGE_DEF( sidUnknownDXFversion,       "Unknown DXF file version. Trying to treat as R12.")
OD_MESSAGE_DEF( sidBinaryIncompatible,      "Module %s has incompatible version")
OD_MESSAGE_DEF( sidErrorUnloadingModule,    "Error unloading module \"%s\"\nOdError: %s")
// Table
OD_MESSAGE_DEF( sidTableStyle,              "Table Style (%s) is invalid")

OD_MESSAGE_DEF( sidAuditFailed,             "Audit Failed")

// todo -- move these to appropriate place (hatch) in 1.14
OD_MESSAGE_DEF( sidInvalidHatchBoundaryData,"Hatch has invalid boundary data.")
OD_MESSAGE_DEF( sidHatchBndDupVerts,        "Polyline hatch boundary has duplicated vertices.")

