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



#ifndef OD_GE_DWG_IO
#define OD_GE_DWG_IO

#include "Ge/GeLibVersion.h"

class OdDbDwgFiler;

class OdGeLineSeg2d;
class OdGeCircArc2d;
class OdGeEllipArc2d;
class OdGeNurbCurve2d;
class OdGePolyline2d;


/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbGeDwgIO
{
public:
/*	
	static
    void outFields(OdDbDwgFiler*, const OdGePoint2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeVector2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeMatrix2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeScale2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGePoint2dArray&);
	static
    void outFields(OdDbDwgFiler*, const OdGeVector2dArray&);
	static
    void outFields(OdDbDwgFiler*, const OdGePoint3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeVector3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeMatrix3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeScale3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGePoint3dArray&);
	static
    void outFields(OdDbDwgFiler*, const OdGeVector3dArray&);
	static
    void outFields(OdDbDwgFiler*, const OdGeTol&);
	static
    void outFields(OdDbDwgFiler*, const OdGeInterval&);
	static
    void outFields(OdDbDwgFiler*, const OdGeKnotVector&);
	static
    void outFields(OdDbDwgFiler*, const OdGeDoubleArray&);
	static
    void outFields(OdDbDwgFiler*, const OdIntArray&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCurveBoundary&);
	static
    void outFields(OdDbDwgFiler*, const OdGePosition2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGePointOnCurve2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeLine2d&);
*/

	static
    void outFields(OdDbDwgFiler*, const OdGeLineSeg2d&);

/*
	static
    void outFields(OdDbDwgFiler*, const OdGeRay2d&);
*/

	static
    void outFields(OdDbDwgFiler*, const OdGeCircArc2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeEllipArc2d&);

/*
	static
    void outFields(OdDbDwgFiler*, const OdGeExternalCurve2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCubicSplineCurve2d&);
*/

	static
    void outFields(OdDbDwgFiler*, const OdGeNurbCurve2d&);

/*
	static
    void outFields(OdDbDwgFiler*, const OdGeCompositeCurve2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeOffsetCurve2d&);
*/

	static
    void outFields(OdDbDwgFiler*, const OdGePolyline2d&);

/*
	static
    void outFields(OdDbDwgFiler*, const OdGePosition3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGePointOnCurve3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGePointOnSurface&);
	static
    void outFields(OdDbDwgFiler*, const OdGeLine3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeRay3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeLineSeg3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGePlane&);
	static
    void outFields(OdDbDwgFiler*, const OdGeBoundedPlane&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCircArc3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeEllipArc3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCubicSplineCurve3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeNurbCurve3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCompositeCurve3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeOffsetCurve3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGePolyline3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeAugPolyline3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeExternalCurve3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCone&);
	static
    void outFields(OdDbDwgFiler*, const OdGeEllipCone&);	// AE 03.09.2003 
	static
    void outFields(OdDbDwgFiler*, const OdGeCylinder&);
	static
    void outFields(OdDbDwgFiler*, const OdGeEllipCylinder&);	// AE 09.09.2003 
	static
    void outFields(OdDbDwgFiler*, const OdGeTorus&);
	static
    void outFields(OdDbDwgFiler*, const OdGeExternalSurface&);
	static
    void outFields(OdDbDwgFiler*, const OdGeOffsetSurface&);
	static
    void outFields(OdDbDwgFiler*, const OdGeNurbSurface&);
	static
		void outFields(OdDbDwgFiler*,const OdGeExternalBoundedSurface&);
	static
    void outFields(OdDbDwgFiler*, const OdGeSphere&);
	static
    void outFields(OdDbDwgFiler*, const OdGeBoundBlock2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeBoundBlock3d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCurveCurveInt2d&);
	static
    void outFields(OdDbDwgFiler*, const OdGeCurveCurveInt3d&);
	
	static
    void inFields(OdDbDwgFiler*, OdGePoint2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeVector2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeMatrix2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeScale2d&);
	static
    void inFields(OdDbDwgFiler*, OdGePoint2dArray&);
	static
    void inFields(OdDbDwgFiler*, OdGeVector2dArray&);
	static
    void inFields(OdDbDwgFiler*, OdGePoint3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeVector3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeMatrix3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeScale3d&);
	static
    void inFields(OdDbDwgFiler*, OdGePoint3dArray&);
	static
    void inFields(OdDbDwgFiler*, OdGeVector3dArray&);
	static
    void inFields(OdDbDwgFiler*, OdGeTol&);
	static
    void inFields(OdDbDwgFiler*, OdGeInterval&);
	static
    void inFields(OdDbDwgFiler*, OdGeKnotVector&);
	static
    void inFields(OdDbDwgFiler*, OdGeDoubleArray&);
	static
    void inFields(OdDbDwgFiler*, OdIntArray&);
	static
    void inFields(OdDbDwgFiler*, OdGeCurveBoundary&);
	static
    void inFields(OdDbDwgFiler*, OdGePosition2d&);
	static
    void inFields(OdDbDwgFiler*, OdGePointOnCurve2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeLine2d&);
*/

	static
    void inFields(OdDbDwgFiler*, OdGeLineSeg2d&);

/*
	static
    void inFields(OdDbDwgFiler*, OdGeRay2d&);
*/

	static
    void inFields(OdDbDwgFiler*, OdGeCircArc2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeEllipArc2d&);

/*
	static
    void inFields(OdDbDwgFiler*, OdGeExternalCurve2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeCubicSplineCurve2d&);
*/

	static
    void inFields(OdDbDwgFiler*, OdGeNurbCurve2d&);

/*
	static
    void inFields(OdDbDwgFiler*, OdGeCompositeCurve2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeOffsetCurve2d&);
*/

	static
    void inFields(OdDbDwgFiler*, OdGePolyline2d&);

/*
	static
    void inFields(OdDbDwgFiler*, OdGePosition3d&);
	static
    void inFields(OdDbDwgFiler*, OdGePointOnCurve3d&);
	static
    void inFields(OdDbDwgFiler*, OdGePointOnSurface&);
	static
    void inFields(OdDbDwgFiler*, OdGeLine3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeRay3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeLineSeg3d&);
	static
    void inFields(OdDbDwgFiler*, OdGePlane&);
	static
    void inFields(OdDbDwgFiler*, OdGeBoundedPlane&);
	static
    void inFields(OdDbDwgFiler*, OdGeCircArc3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeEllipArc3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeCubicSplineCurve3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeCompositeCurve3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeOffsetCurve3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeNurbCurve3d&);
	static
    void inFields(OdDbDwgFiler*, OdGePolyline3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeAugPolyline3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeExternalCurve3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeCone&);
	static
    void inFields(OdDbDwgFiler*, OdGeEllipCone&);	// AE 03.09.2003 
	static
    void inFields(OdDbDwgFiler*, OdGeCylinder&);
	static
    void inFields(OdDbDwgFiler*, OdGeEllipCylinder&);	// AE 09.09.2003 
	static
    void inFields(OdDbDwgFiler*, OdGeTorus&);
	static
    void inFields(OdDbDwgFiler*, OdGeExternalSurface&);
	static
    void inFields(OdDbDwgFiler*, OdGeOffsetSurface&);
	static
    void inFields(OdDbDwgFiler*, OdGeNurbSurface&);
	static
    void inFields(OdDbDwgFiler*, OdGeExternalBoundedSurface&);
	static
    void inFields(OdDbDwgFiler*, OdGeSphere&);
	static
    void inFields(OdDbDwgFiler*, OdGeBoundBlock2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeBoundBlock3d&);
	static
    void inFields(OdDbDwgFiler*, OdGeCurveCurveInt2d&);
	static
    void inFields(OdDbDwgFiler*, OdGeCurveCurveInt3d&);
*/	

	static const OdGeLibVersion  OdGeDwgIOVersion;
};

#endif // OD_GE_DWG_IO


