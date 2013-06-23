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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GE_FILE_IO_H
#define OD_GE_FILE_IO_H /* {Secret} */


class OdGeEntity2d;
class OdGeEntity3d;
class OdGePoint2d;
class OdGeVector2d;
class OdGeMatrix2d;
class OdGeScale2d;
class OdGePoint3d;
class OdGeVector3d;
class OdGeMatrix3d;
class OdGeScale3d;
class OdGeTol;
class OdGeInterval;
class OdGeKnotVector;
class OdGeCurveBoundary;
class OdGePosition2d;
class OdGePointOnCurve2d;
class OdGeLine2d;
class OdGeLineSeg2d;
class OdGeRay2d;
class OdGeCircArc2d;
class OdGeEllipArc2d;
class OdGeExternalCurve2d;
class OdGeCubicSplineCurve2d;
class OdGeCompositeCurve2d;
class OdGeOffsetCurve2d;
class OdGeNurbCurve2d;
class OdGePolyline2d;
class OdGePosition3d;
class OdGePointOnCurve3d;
class OdGePointOnSurface;
class OdGeLine3d;
class OdGeRay3d;
class OdGeLineSeg3d;
class OdGePlane;
class OdGeBoundedPlane;
class OdGeBoundBlock2d;
class OdGeBoundBlock3d;
class OdGeCircArc3d;
class OdGeEllipArc3d;
class OdGeCubicSplineCurve3d;
class OdGeCompositeCurve3d;
class OdGeOffsetCurve3d;
class OdGeNurbCurve3d;
class OdGePolyline3d;
class OdGeAugPolyline3d;
class OdGeExternalCurve3d;
class OdGeSurface;
class OdGeCone;
class OdGeCylinder;
class OdGeTorus;
class OdGeExternalSurface;
class OdGeOffsetSurface;
class OdGeNurbSurface;
class OdGeExternalBoundedSurface;
class OdGeSphere;
class OdGeCurveCurveInt2d;
class OdGeCurveCurveInt3d;
class OdGeEllipCone; 
class OdGeEllipCylinder; 

class OdGeFiler;
class OdGeLibVersion;

#include "GeLibVersion.h"
#include "Ge.h"
#include "GeIntArray.h"
#include "GeDoubleArray.h"
#include "GePoint2dArray.h"
#include "GeVector2dArray.h"
#include "GeVector3dArray.h"

/**
    Description:
    This class reads and writes information to a file.

    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeFileIO
{
public:
  /**
    Description:
    Writes information to a file.

    Arguments:
    filer (I) Pointer to OdGeFiler *object*.
    object (I) OdGe *object* to be written.
    libVersion (I) OdGe library version.
  */
  static void outFields (
    OdGeFiler* filer,
    const OdGePoint2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeVector2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeMatrix2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeScale2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePoint2dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeVector2dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePoint3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeVector3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeMatrix3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeScale3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePoint3dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeVector3dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeTol& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeInterval& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeKnotVector& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeDoubleArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer, 
    const OdGeIntArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCurveBoundary& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePosition2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);  
  static void outFields (
    OdGeFiler* filer,
    const OdGePointOnCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeLine2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeLineSeg2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeRay2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCircArc2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeEllipArc2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeExternalCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCubicSplineCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCompositeCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeOffsetCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeNurbCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePolyline2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePosition3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePointOnCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePointOnSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeLine3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeRay3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeLineSeg3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePlane& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeBoundedPlane& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCircArc3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeEllipArc3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCubicSplineCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCompositeCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeOffsetCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeNurbCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGePolyline3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeAugPolyline3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeExternalCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCone& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeEllipCone& object,    
    const OdGeLibVersion& = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCylinder& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeEllipCylinder& object, 
    const OdGeLibVersion& = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeTorus& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeExternalSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeOffsetSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeNurbSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeExternalBoundedSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeSphere& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeBoundBlock2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeBoundBlock3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCurveCurveInt2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeCurveCurveInt3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void outFields (
    OdGeFiler* filer,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);

  /**
    Description:
    Reads information from a file.

    Arguments:
    filer (I) Pointer to OdGeFiler *object*.
    object (O) Receives the OdGe *object* to be read.
    libVersion (I) OdGe library version.
  */
  static void inFields (
    OdGeFiler* filer,
    OdGePoint2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeVector2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeMatrix2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeScale2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePoint2dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeVector2dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePoint3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeVector3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeMatrix3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeScale3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePoint3dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeVector3dArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeTol& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeInterval& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeKnotVector& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeDoubleArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer, 
    OdGeIntArray& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCurveBoundary& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePosition2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePointOnCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeLine2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeLineSeg2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeRay2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCircArc2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeEllipArc2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeExternalCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCubicSplineCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCompositeCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeOffsetCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeNurbCurve2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePolyline2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePosition3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePointOnCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePointOnSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeLine3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeRay3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeLineSeg3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePlane& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeBoundedPlane& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCircArc3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeEllipArc3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCubicSplineCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCompositeCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeOffsetCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeNurbCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGePolyline3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeAugPolyline3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeExternalCurve3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCone& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeEllipCone& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion); 
  static void inFields (
    OdGeFiler* filer,
    OdGeCylinder& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeEllipCylinder& object,  
    const OdGeLibVersion& = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeTorus& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeExternalSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeOffsetSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeNurbSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeExternalBoundedSurface& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeSphere& object,
    const OdGeLibVersion& libVersionsion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeBoundBlock2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeBoundBlock3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCurveCurveInt2d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeCurveCurveInt3d& object,
    const OdGeLibVersion& libVersion = OdGe::gLibVersion);
  static void inFields (
    OdGeFiler* filer,
    OdGeLibVersion& libVersion = OdGe::gLibVersion);

private:
  static
    void writebool(OdGeFiler*, bool,
    const OdGeLibVersion&);
  static
    void readbool(OdGeFiler*, bool*,
    const OdGeLibVersion&);
  void writeBool(OdGeFiler*, bool,
    const OdGeLibVersion&);
  static
    void readBool(OdGeFiler*, bool*,
    const OdGeLibVersion&);
  static
    void writeLong(OdGeFiler*, long,
    const OdGeLibVersion&);
  static
    void readLong(OdGeFiler*, long*,
    const OdGeLibVersion&);
  static
    void writeDouble(OdGeFiler*, double,
    const OdGeLibVersion&);
  static
    void readDouble(OdGeFiler*, double*,
    const OdGeLibVersion&);
  static
    void writePoint2d(OdGeFiler*, const OdGePoint2d&,
    const OdGeLibVersion&);
  static
    void readPoint2d(OdGeFiler*, OdGePoint2d*,
    const OdGeLibVersion&);
  static
    void writeVector2d(OdGeFiler*, const OdGeVector2d&,
    const OdGeLibVersion&);
  static
    void readVector2d(OdGeFiler*, OdGeVector2d*,
    const OdGeLibVersion&);
  static
    void writePoint3d(OdGeFiler*, const OdGePoint3d&,
    const OdGeLibVersion&);
  static    void readPoint3d(OdGeFiler*, OdGePoint3d*,
    const OdGeLibVersion&);
  static
    void writeVector3d(OdGeFiler*, const OdGeVector3d&,
    const OdGeLibVersion&);
  static
    void readVector3d(OdGeFiler*, OdGeVector3d*,
    const OdGeLibVersion&);
  static
    void writeAcGeSurface(OdGeFiler*, const OdGeSurface&,
    const OdGeLibVersion& version);
  static
    void readAcGeSurface(OdGeFiler*, OdGeSurface&,
    const OdGeLibVersion& version);
  static
    void writeAcGeEntity2d(OdGeFiler* filer,
    const OdGeEntity2d& ent, const OdGeLibVersion& version);
  static
    void readAcGeEntity2d(OdGeFiler* filer, OdGeEntity2d*& ent,
    OdGe::EntityId id, const OdGeLibVersion& version);
  static
    void writeAcGeEntity3d(OdGeFiler* filer,
    const OdGeEntity3d& ent, const OdGeLibVersion& version);
  static
    void readAcGeEntity3d(OdGeFiler* filer, OdGeEntity3d*& ent,
    OdGe::EntityId id, const OdGeLibVersion& version);

  friend class OdGeFileIOX;

};

#endif // OD_GE_FILE_IO_H



