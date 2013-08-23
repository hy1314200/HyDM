// stdafx.h : 标准系统包含文件的包含文件，
// 或是经常使用但不常更改的
// 特定于项目的包含文件

#pragma once

#ifndef STRICT
#define STRICT
#endif

#include "targetver.h"

#define _ATL_APARTMENT_THREADED

#define _ATL_NO_AUTOMATIC_NAMESPACE

//#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS	// 某些 CString 构造函数将是显式的

#include <atlstr.h>

#define ATL_NO_ASSERT_ON_DESTROY_NONEXISTENT_WINDOW

#include <comsvcs.h>
#include "resource.h"
#include <atlbase.h>
#include <atlcom.h>
#include <atlctl.h>


#include <windows.h>


#include "OdaCommon.h"
#include "DbDatabase.h"
#include "DbAudit.h"

#include "Db2LineAngularDimension.h"
#include "Db2dPolyline.h"
#include "Db2dVertex.h"
#include "Db3PointAngularDimension.h"
#include "Db3dPolyline.h"
#include "Db3dPolylineVertex.h"
#include "Db3dSolid.h"
#include "DbAlignedDimension.h"
#include "DbArc.h"
#include "DbArcAlignedText.h"
#include "DbAttribute.h"
#include "DbAttributeDefinition.h"
#include "DbBlockTableRecord.h"
#include "DbBlockReference.h"
#include "DbBlockTable.h"
#include "DbBody.h"
#include "DbCircle.h"
#include "DbDiametricDimension.h"
#include "DbDimAssoc.h"
#include "DbDimStyleTable.h"
#include "DbEllipse.h"
#include "DbFace.h"
#include "DbFaceRecord.h"
#include "DbFcf.h"
#include "DbField.h"
#include "DbGroup.h"
#include "DbHyperlink.h"
#include "DbLayerTable.h"
#include "DbLayout.h"
#include "DbLeader.h"
#include "DbLine.h"
#include "DbLinetypeTable.h"
#include "DbMText.h"
#include "DbOrdinateDimension.h"
#include "DbPoint.h"
#include "DbPolyFaceMesh.h"
#include "DbPolyFaceMeshVertex.h"
#include "DbPolygonMesh.h"
#include "DbPolygonMeshVertex.h"
#include "DbPolyline.h"
#include "DbRasterImage.h"
#include "DbRasterImageDef.h"
#include "DbRasterVariables.h"
#include "DbRay.h"
#include "DbRegion.h"
#include "DbRotatedDimension.h"
#include "DbShape.h"
#include "DbSolid.h"
#include "DbSortentsTable.h"
#include "DbSpline.h"
#include "DbTable.h"
#include "DbText.h"
#include "DbTextStyleTable.h"
#include "DbTrace.h"
#include "DbViewport.h"
#include "DbViewportTable.h"
#include "DbViewportTableRecord.h"
#include "DbWipeout.h"
#include "DbXline.h"
#include "DbXrecord.h"
#include "Ge/GeCircArc2d.h"
#include "Ge/GeScale3d.h"
#include "Ge/GeExtents3d.h"
#include "Gi/GiMaterial.h"
#include "DbSymUtl.h"
#include "DbHostAppServices.h"
#include "HatchPatternManager.h"
#include "DbLayerTableRecord.h"



using namespace ATL;