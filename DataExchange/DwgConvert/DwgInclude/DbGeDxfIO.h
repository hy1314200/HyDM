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



#ifndef OD_GE_DXF_IO
#define OD_GE_DXF_IO

#include "Ge/GeLibVersion.h"

class OdDbDxfFiler;

class OdGeLineSeg2d;
class OdGeCircArc2d;
class OdGeEllipArc2d;
class OdGeNurbCurve2d;
class OdGePolyline2d;

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbGeDxfIO
{
public:
/*
    static
    void outFields(OdDbDxfFiler*, const OdGePoint2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeVector2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeMatrix2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeScale2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGePoint2dArray&);
    static
    void outFields(OdDbDxfFiler*, const OdGeVector2dArray&);
    static
    void outFields(OdDbDxfFiler*, const OdGePoint3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeVector3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeMatrix3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeScale3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGePoint3dArray&);
    static
    void outFields(OdDbDxfFiler*, const OdGeVector3dArray&);
    static
    void outFields(OdDbDxfFiler*, const OdGeTol&);
    static
    void outFields(OdDbDxfFiler*, const OdGeInterval&);
    static
    void outFields(OdDbDxfFiler*, const OdGeKnotVector&);
    static
    void outFields(OdDbDxfFiler*, const OdGeDoubleArray&);
    static
    void outFields(OdDbDxfFiler*, const OdIntArray&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCurveBoundary&);
    static
    void outFields(OdDbDxfFiler*, const OdGePosition2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGePointOnCurve2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeLine2d&);
*/
    static
    void outFields(OdDbDxfFiler*, const OdGeLineSeg2d&);
/*
    static
    void outFields(OdDbDxfFiler*, const OdGeRay2d&);
*/
    static
    void outFields(OdDbDxfFiler*, const OdGeCircArc2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeEllipArc2d&);
/*
    static
    void outFields(OdDbDxfFiler*, const OdGeExternalCurve2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCubicSplineCurve2d&);
*/
    static
    void outFields(OdDbDxfFiler*, const OdGeNurbCurve2d&);
/*    static
    void outFields(OdDbDxfFiler*, const OdGeCompositeCurve2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeOffsetCurve2d&);
*/
    static
    void outFields(OdDbDxfFiler*, const OdGePolyline2d&);
/*
    static
    void outFields(OdDbDxfFiler*, const OdGePosition3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGePointOnCurve3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGePointOnSurface&);
    static
    void outFields(OdDbDxfFiler*, const OdGeLine3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeRay3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeLineSeg3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGePlane&);
    static
    void outFields(OdDbDxfFiler*, const OdGeBoundedPlane&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCircArc3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeEllipArc3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCubicSplineCurve3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeNurbCurve3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCompositeCurve3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeOffsetCurve3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGePolyline3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeAugPolyline3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeExternalCurve3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCone&);
    static
    void outFields(OdDbDxfFiler*, const OdGeEllipCone&);	// AE 03.09.2003 
    static
    void outFields(OdDbDxfFiler*, const OdGeCylinder&);
    static
    void outFields(OdDbDxfFiler*, const OdGeEllipCylinder&);	// AE 09.09.2003 
    static
    void outFields(OdDbDxfFiler*, const OdGeTorus&);
    static
    void outFields(OdDbDxfFiler*, const OdGeExternalSurface&);
    static
    void outFields(OdDbDxfFiler*, const OdGeOffsetSurface&);
    static
    void outFields(OdDbDxfFiler*, const OdGeNurbSurface&);
    static
    void outFields(OdDbDxfFiler*,const OdGeExternalBoundedSurface&);
    static
    void outFields(OdDbDxfFiler*, const OdGeSphere&);
    static
    void outFields(OdDbDxfFiler*, const OdGeBoundBlock2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeBoundBlock3d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCurveCurveInt2d&);
    static
    void outFields(OdDbDxfFiler*, const OdGeCurveCurveInt3d&);

    static
    void inFields(OdDbDxfFiler*, OdGePoint2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeVector2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeMatrix2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeScale2d&);
    static
    void inFields(OdDbDxfFiler*, OdGePoint2dArray&);
    static
    void inFields(OdDbDxfFiler*, OdGeVector2dArray&);
    static
    void inFields(OdDbDxfFiler*, OdGePoint3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeVector3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeMatrix3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeScale3d&);
    static
    void inFields(OdDbDxfFiler*, OdGePoint3dArray&);
    static
    void inFields(OdDbDxfFiler*, OdGeVector3dArray&);
    static
    void inFields(OdDbDxfFiler*, OdGeTol&);
    static
    void inFields(OdDbDxfFiler*, OdGeInterval&);
    static
    void inFields(OdDbDxfFiler*, OdGeKnotVector&);
    static
    void inFields(OdDbDxfFiler*, OdGeDoubleArray&);
    static
    void inFields(OdDbDxfFiler*, OdIntArray&);
    static
    void inFields(OdDbDxfFiler*, OdGeCurveBoundary&);
    static
    void inFields(OdDbDxfFiler*, OdGePosition2d&);
    static
    void inFields(OdDbDxfFiler*, OdGePointOnCurve2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeLine2d&);
*/

    static
    void inFields(OdDbDxfFiler*, OdGeLineSeg2d&);

/*
    static
    void inFields(OdDbDxfFiler*, OdGeRay2d&);
*/

    static
    void inFields(OdDbDxfFiler*, OdGeCircArc2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeEllipArc2d&);

/*
    static
    void inFields(OdDbDxfFiler*, OdGeExternalCurve2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeCubicSplineCurve2d&);
*/

    static
    void inFields(OdDbDxfFiler*, OdGeNurbCurve2d&);

/*
    static
    void inFields(OdDbDxfFiler*, OdGeCompositeCurve2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeOffsetCurve2d&);
*/

    static
    void inFields(OdDbDxfFiler*, OdGePolyline2d&);

/*
    static
    void inFields(OdDbDxfFiler*, OdGePosition3d&);
    static
    void inFields(OdDbDxfFiler*, OdGePointOnCurve3d&);
    static
    void inFields(OdDbDxfFiler*, OdGePointOnSurface&);
    static
    void inFields(OdDbDxfFiler*, OdGeLine3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeRay3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeLineSeg3d&);
    static
    void inFields(OdDbDxfFiler*, OdGePlane&);
    static
    void inFields(OdDbDxfFiler*, OdGeBoundedPlane&);
    static
    void inFields(OdDbDxfFiler*, OdGeCircArc3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeEllipArc3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeCubicSplineCurve3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeNurbCurve3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeCompositeCurve3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeOffsetCurve3d&);
    static
    void inFields(OdDbDxfFiler*, OdGePolyline3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeAugPolyline3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeExternalCurve3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeCone&);
    static
    void inFields(OdDbDxfFiler*, OdGeEllipCone&);	// AE 03.09.2003 
    static
    void inFields(OdDbDxfFiler*, OdGeCylinder&);
    static
    void inFields(OdDbDxfFiler*, OdGeEllipCylinder&);	// AE 09.09.2003 
    static
    void inFields(OdDbDxfFiler*, OdGeTorus&);
    static
    void inFields(OdDbDxfFiler*, OdGeExternalSurface&);
    static
    void inFields(OdDbDxfFiler*, OdGeOffsetSurface&);
    static
    void inFields(OdDbDxfFiler*, OdGeNurbSurface&);
    static
    void inFields(OdDbDxfFiler*, OdGeExternalBoundedSurface&);
    static
    void inFields(OdDbDxfFiler*, OdGeSphere&);
    static
    void inFields(OdDbDxfFiler*, OdGeBoundBlock2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeBoundBlock3d&);
    static
    void inFields(OdDbDxfFiler*, OdGeCurveCurveInt2d&);
    static
    void inFields(OdDbDxfFiler*, OdGeCurveCurveInt3d&);
*/

  static const OdGeLibVersion  OdGeDxfIOVersion;
};

#endif // OD_GE_DXF_IO


