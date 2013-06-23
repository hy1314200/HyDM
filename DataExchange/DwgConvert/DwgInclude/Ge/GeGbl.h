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



#ifndef OD_GEGBL_H
#define OD_GEGBL_H /* {Secret} */


#include "GeTol.h"

class OdGePoint3d;
#include "Ge.h"
#include "OdArrayPreDef.h"
#include "GePoint3dArray.h"

class OdGeVector3d;
class OdGePlane;
class OdGeCurve3d;
class OdGeNurbCurve2d;
class OdGeNurbCurve3d;

#include "DD_PackPush.h"

/**
    Description:
    This structure provides a namespace for tolerance values and functions ubiquitous to the OdGe library. 

    Library:
    Ge
    
    {group:OdGe_Classes}
*/      
struct GE_TOOLKIT_EXPORT OdGeContext
{
  /** 
    Provides the global default OdGeTol tolerance object.
   
                equalPoint == 1e-10 and equalVector == 1.e-10.
  */
  static OdGeTol gTol;              

  /** 
    Provides the global 0.0 default OdGeTol tolerance object.
  
                equalPoint == 0.0 and equalVector == 1.e-10.
  */
  static OdGeTol gZeroTol;
  
  /**
    A function pointer to a user-defined error handler. 
    By default, points to a function that does nothing but return
  */          
  static void (*gErrorFunc)();     

  /** 
    A function pointer to a user-defined function that returns returns orthoVector orthogonal to the vect.
    By default, points to a function that computes orthoVector with the arbitrary axis algorithm:
    
                if ( (vect.x < 0.015625) && (vect.x < 0.015625)) {
                  orthoVector.x = vect.z;
                  orthoVector.y = 0.0;
                  orthoVector.z = -vect.x;
                }
                else {
                  orthoVector.x = -vect.y;
                  orthoVector.y = vect.x;
                  orthoVector.z = 0.0;
                } 
  */
  static void (*gOrthoVector) (const OdGeVector3d& vect, OdGeVector3d& orthoVector);

  /**
    A function pointer to a user-defined memory allocation function 
    for all *new* operations in the OdGe library. 
    Allows the OdGe library to use the same memory manager as the user application.
  */
  static void* (*gAllocMem) (unsigned long);
      

  /**
    A function pointer to a user-defined memory allocation function 
    for all *delete* operations in the OdGe library.

    Allows the OdGe library to use the same memory manager as the user application.
  */
  static void (*gFreeMem) (void*);
};

/**
    Description:
    Returns the normal to the *plane* defined by the specified points.
    
    Arguments:
    pts (I) Array of 3D points.
    numPts (I) Number of points.
    pNormal (O) Receives the normal to the calculated *plane*.
    tol (I) Geometric tolerance.
    
    Remarks:
    Possible return values are as follows:

    @untitled table
    kOk                  
    kNonCoplanarGeometry
    kDegenerateGeometry
    kSingularPoint
    
    Library: Ge
*/
GE_TOOLKIT_EXPORT OdGeError geCalculateNormal (
    const OdGePoint3dArray& numPts, 
    OdGeVector3d * pNormal,
    const OdGeTol& tol = OdGeContext::gTol);

GE_TOOLKIT_EXPORT OdGeError geCalculateNormal (
    const OdGePoint3d *pts, 
    OdUInt32 numPts, 
    OdGeVector3d * pNormal,
    const OdGeTol& tol = OdGeContext::gTol);

// Same with previous functions, but returns result as plane

/**
    Description:
    Calculates *plane* defined by the specified points.
    
    Arguments:
    pts (I) Array of 3D points or curves.
    numPts (I) Number of points.
    plane (O) Receives the calculated *plane*.
    tol (I) Geometric tolerance.
    
    Remarks:
    Supported curves are OdGeCircArc3d, OdGeEllipArc3d, OdGeNurbCurve3d, and OdGeLineSeg3d.
    
    Possible return values are as follows:

    @untitled table
    kOk                  
    kNonCoplanarGeometry
    kDegenerateGeometry
    kSingularPoint

    Library: Ge
*/
GE_TOOLKIT_EXPORT OdGeError geCalculatePlane (
    const OdGePoint3dArray& pts, 
    OdGePlane& plane,
    const OdGeTol& tol = OdGeContext::gTol);

GE_TOOLKIT_EXPORT OdGeError geCalculatePlane (
    const OdGePoint3d *pts, 
    OdUInt32 numPts, 
    OdGePlane& plane,
    const OdGeTol& tol = OdGeContext::gTol);

GE_TOOLKIT_EXPORT OdGeError geCalculatePlane (
    const OdGeCurve3d * const* pts, 
    OdUInt32 numPts, 
    OdGePlane& plane,
    const OdGeTol& tol = OdGeContext::gTol);

/**
    Description:
    Converts a 3D nurb curve to a 2D nurb curve by projecting it onto the specified *plane*.
    
    Arguments:
    nurb3d (I) Any 3D nurb curve.
    plane (I) Projection *plane*.     
    nurb2d (O) Receives the 2D nurb curve.
    tol (I) Geometric tolerance.
    
    library: Ge
*/
GE_TOOLKIT_EXPORT bool geNurb3dTo2d (
    const OdGeNurbCurve3d &nurb3d, 
    OdGePlane& plane, 
    OdGeNurbCurve2d &nurb2d,
    const OdGeTol& tol = OdGeContext::gTol);

#include "DD_PackPop.h"

#endif // OD_GEGBL_H


