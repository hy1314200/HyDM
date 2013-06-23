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



#ifndef OD_GEGBLGE_H
#define OD_GEGBLGE_H /* { Secret } */

#include "DD_PackPush.h"

#include "GeExport.h"
#include "GeLibVersion.h"

/**
  Description:
  The OdGe classes are used to carry out general 2D and 3D geometric operations. 

  Library: Ge
  
  {group:OdGe_Classes}
*/
struct OdGe
{
  static const OdGeLibVersion gLibVersion;

  enum 
  { 
    eGood, 
    eBad 
  };
  
  enum EntityId 
  {
    kEntity2d,
    kEntity3d,
    kPointEnt2d,
    kPointEnt3d,
    kPosition2d,
    kPosition3d,
    kPointOnCurve2d,
    kPointOnCurve3d,
    kPointOnSurface,
    kBoundedPlane,
    kCircArc2d,
    kCircArc3d,
    kConic2d,
    kConic3d,
    kCurve2d,
    kCurve3d,
    kEllipArc2d,
    kEllipArc3d,
    kLine2d,
    kLine3d,
    kLinearEnt2d,
    kLinearEnt3d,
    kLineSeg2d,
    kLineSeg3d,
    kPlanarEnt,
    kPlane,
    kRay2d,
    kRay3d,
    kSurface,
    kSphere,
    kCylinder,
    kTorus,
    kCone,
    kSplineEnt2d,
    kPolyline2d,
    kAugPolyline2d,
    kNurbCurve2d,
    kDSpline2d,
    kCubicSplineCurve2d,
    kSplineEnt3d,
    kPolyline3d,
    kAugPolyline3d,
    kNurbCurve3d,
    kDSpline3d,
    kCubicSplineCurve3d,
    kTrimmedCrv2d,
    kCompositeCrv2d,
    kCompositeCrv3d,
    kExternalSurface,
    kNurbSurface,
    kTrimmedSurface,
    kOffsetSurface,
    kEnvelope2d,
    kCurveBoundedSurface,
    kExternalCurve3d,
    kExternalCurve2d,
    kSurfaceCurve2dTo3d,
    kSurfaceCurve3dTo2d,
    kExternalBoundedSurface,
    kCurveCurveInt2d,
    kCurveCurveInt3d,
    kBoundBlock2d,
    kBoundBlock3d,
    kOffsetCurve2d,
    kOffsetCurve3d,
    kPolynomCurve3d,
    kBezierCurve3d,
    kObject,
    kFitData3d,
    kHatch,
    kTrimmedCurve2d,
    kTrimmedCurve3d,
    kCurveSampleData,
    kEllipCone,
    kEllipCylinder,
    kIntervalBoundBlock,
    kClipBoundary2d,
    kExternalObject,
    kCurveSurfaceInt,
    kSurfaceSurfaceInt
  };

  enum ExternalEntityKind 
  {
    kAcisEntity              = 0, // External Entity is an ACIS entity
    kExternalEntityUndefined      // External Entity is undefined. 
  }; 
  enum PointContainment 
  {
    kInside,              // Point is inside the boundary.
    kOutside,             // Point is outside the boundary.
    kOnBoundary           // Point on the boundary.
  };
  enum NurbSurfaceProperties 
  {
    kOpen       = 0x01,   // Open
    kClosed     = 0x02,   // Closed
    kPeriodic   = 0x04,   // Periodic
    kRational   = 0x08,   // Rational
    kNoPoles    = 0x10,   // No Poles
    kPoleAtMin  = 0x20,   // Pole at Min
    kPoleAtMax  = 0x40,   // Pole at Max
    kPoleAtBoth = 0x80    // Pole at Both
  };
  
  enum OffsetCrvExtType {
    kFillet, 
    kChamfer, 
    kExtend
  };
  enum OdGeXConfig 
  {
    kNotDefined      = 1 << 0,
    kUnknown         = 1 << 1,
    kLeftRight       = 1 << 2,
    kRightLeft       = 1 << 3,
    kLeftLeft        = 1 << 4,
    kRightRight      = 1 << 5,
    kPointLeft       = 1 << 6,
    kPointRight      = 1 << 7,
    kLeftOverlap     = 1 << 8,
    kOverlapLeft     = 1 << 9,
    kRightOverlap    = 1 << 10,
    kOverlapRight    = 1 << 11,
    kOverlapStart    = 1 << 12,
    kOverlapEnd      = 1 << 13,
    kOverlapOverlap  = 1 << 14
  };

  enum BooleanType 
  {
    kUnion,                          // Union
    kSubtract,                       // Subtraction
    kCommon                          // Intersection
  }; 
  enum ClipError 
  {
    eOk,                             // OK
    eInvalidClipBoundary,            // Invalid Clip Boundary
    eNotInitialized                  // Clip Boundary was not Initialized
  };

  enum ClipCondition 
  {
    kInvalid,                        // An error occurred, probably due to invalid initialization of clipping object.
    kAllSegmentsInside,              // All segments are inside the clip boundary.
    kSegmentsIntersect,              // At least one segment crosses the clip boundary.
    kAllSegmentsOutsideZeroWinds,    // The clip boundary is outside, and not encircled by, the clip boundary.
    kAllSegmentsOutsideOddWinds,     // The clip boundary is inside, and encircled by, the clip boundary.    
    kAllSegmentsOutsideEvenWinds     // The clip boundary is outside, but, encircled by, the clip boundary.    
  };

  /**
  */
  enum ErrorCondition 
  {
    kOk,                                // OK
    k0This,                             // This object is 0.
    k0Arg1,                             // Argument 1 is 0.
    k0Arg2,                             // Argument 2 is 0.
    kPerpendicularArg1Arg2,             // Arguments 1 and 2 are perpendicular to each other.
    kEqualArg1Arg2,                     // Arguments 1 and 2 are equal.
    kEqualArg1Arg3,                     // Arguments 1 and 3 are equal.
    kEqualArg2Arg3,                     // Arguments 2 and 3 are equal.
    kLinearlyDependentArg1Arg2Arg3,     // Arguments 1, 2, and 3 are linearly dependent.
    kArg1TooBig,                        // Argument 1 is too big.
    kArg1OnThis,                        // Argument 1 is on this object.
    kArg1InsideThis,                    // Argument 1 is inside this object.
    kNonCoplanarGeometry,               // Geometry is not coplanar.
    kDegenerateGeometry,                // Geometry is degenerate.
    kSingularPoint                      // Geometry is one point.
  };

  enum csiConfig 
  {
    kXUnknown,              // Unknown.
    kXOut,                  // Transverse -- Curve neighborhood is outside this surface.
    kXIn,                   // Transverse -- Curve neighborhood is inside this surface.
    kXTanOut,               // Tangent -- Curve neighborhood is outside this surface.
    kXTanIn,                // Tangent -- Curve neighborhood is inside this surface.
    kXCoincident,           // Non-zero length -- Point is on the intersection boundary.
    kXCoincidentUnbounded   // Non-zero length -- Point is on an arbitrary *point* on an unbounded intersection.
  };

  enum ssiType 
  {
    kSSITransverse,         // Non-tangent intersection.
    kSSITangent,            // Tangent intersection with surface normals codirectional at any point within the component.
    kSSIAntiTangent         // Tangent intersection with surface normals antidirectional at any point within the component.
  };

  enum ssiConfig 
  {
    kSSIUnknown,          // Unknown.
    kSSIOut,              // Neighborhood is outside this surface.
    kSSIIn,               // Neighborhood is inside this surface.
    kSSICoincident        // Non-zero area intersection.
  };

  enum OdGeIntersectError 
  {
    kXXOk,                      // OK
    kXXIndexOutOfRange,         // Index out of range
    kXXWrongDimensionAtIndex,   // Wrong dimension at index.
    kXXUnknown                  // Unknown.
  };

};

/**
    Description:
    Defines a synonym of the fully qualified name.

    Note:
    May be used only if there are no global name conflicts.
*/
typedef OdGe::ErrorCondition OdGeError;

/**
    Description:
    Defines a synonym of the fully qualified name.

    Note:
    May be used only if there are no global name conflicts.
*/
typedef OdGe::OdGeIntersectError OdGeIntersectError;

#include "DD_PackPop.h"

#endif  // AC_GEGBLGE_H


