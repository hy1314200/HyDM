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



#ifndef OD_GE_CURVE_BOUNDARY_H
#define OD_GE_CURVE_BOUNDARY_H /* {Secret} */


class OdGeCurve2d;
class OdGeEntity3d;
class OdGePosition3d;

#include "DD_PackPush.h"
/**
    Description:
    This class represents the boundary geometry (face loops) on a bounded surface.

    Remarks:
    Each face loop consists of four arrays:
    
    o 3D curve or position pointers
    o 2D parameter space curve pointers
    o 3D orientations
    o 2D orientations
    
    There is a one-to-one correspondence between elements in the arrays, although certain 
    elements of a given array could be NULL, or meaningless.

    Loop degeneracies are represented as follows:
    
    o The entire loop degenerates to a single model space point, which is
      represented by the tuple (numElements = 1, position3d, curve2d).
      The curve2d may be NULL. The edge sense, and the curve2d sense are
      irrelevant. isDegenerate() method allows the dermination of this
      condition on a loop.
    o A loop consisting of one or more model space degeneracies
      is represented as the general case with those edges that are
      degenerate represented by position3d. This implies that in the
      general case, model space geometry of a curve boundary may consist of
      curve3d and/or position3d pointers. Consequently, this geometry is
      obtained by the user as entity3d pointers. The degeneracy of a
      constituent edge can be detected by the type of the model space
      geometry.
   
    This class also supports the ownership of its geometry. Being the
    owner of its geometry would cause it to remove the geometry on
    destruction of an instance of the class.
       
    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCurveBoundary
{
public:
  /**
    Arguments:
    numberOfCurves (I) Number of curves in the curve boundary.
    crv3d (I) Array of 3D curves of each element in the curve boundary.
    crv2d (I) Array of parameter space curves of each element in the curve boundary.
    orientation3d (I) Array of orientations for each element of crv3d. 
    orientation2d (I) Array of orientations for each element in crv2d.
    makeCopy (I)  If true, makes a copy of crv3d and crv2d. 
    source (I) Object to be cloned.

    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCurveBoundary();
  OdGeCurveBoundary(
    int numberOfCurves, 
    const OdGeEntity3d *const * crv3d,
    const OdGeCurve2d *const * crv2d, 
    bool* orientation3d,
    bool* orientation2d, 
    bool makeCopy = true);
  OdGeCurveBoundary(const OdGeCurveBoundary& source);

  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  ~OdGeCurveBoundary();

  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCurveBoundary& operator = (
    const OdGeCurveBoundary& crvBoundary);

  // Query the data.
  //

  /**
    Description:
    Returns true if and only if the the curve boundary degenerates to a single 3D point.
    
    Arguments:
    degenPoint (O) Receives the point to which the boundary degenerates.
    paramCurve (O) Receives the parameter space curve corresponding to degenPoint.
    
    Remarks:
    If this method returns true, paramCurve was created with the new operator, and it is the responsibility of the caller to delete it.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  bool isDegenerate() const;
  bool isDegenerate(
    OdGePosition3d& degenPoint, 
    OdGeCurve2d** paramCurve) const;
    
  /**
    Description:
    Return the number of elements in the curve boundary.
    
    Remarks:
    See also getContour().
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  int numElements() const;

  /**
    Description:
    Returns the representation information of the curve boundary.
    
    Arguments:
    numberOfCurves (O) Receives the number of curves in the curve boundary.
    crv3d (I/O) Array of 3D curves of each element in the curve boundary.
    crv2d (I/O) Array of parameter space curves of each element in the curve boundary.
    orientation3d (I/O) Array of orientations for each element of crv3d. 
    orientation2d (I/O) Array of orientations for each element in crv2d.
    
    Remarks:

    For each of the arrays, the user may either provide the space for the array, or allow the getCountour to allocate it. 
    If an array array is NULL, getCountour allocates space, and the caller must delete the corresponding array. 
    In either event, the caller must delete the elements of the arrays.

    orientation3d and orientation2d are valid if and only if the corresponding entry in crv3d is not NULL. 
    If they are NULL, then the method allocates memory, and the caller must delete it. Otherwise, the method assumes 
    that the caller has allocated memory for numberOfCurves.
    
    See also numElements().
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  void getContour(
    int& numberOfCurves, 
    OdGeEntity3d*** crv3d,
    OdGeCurve2d*** crv2d,
    bool** orientation3d,
    bool** orientation2d) const;

  /**
    Description:
    Sets the parameters for this curve boundary according to the arguments, 
    and returns a reference to it.
    
    Arguments:
    numberOfCurves (I) Number of curves in the curve boundary.
    crv3d (I) Array of 3D curves of each element in the curve boundary.
    crv2d (I) Array of parameter space curves of each element in the curve boundary.
    orientation3d (I) Array of orientations for each element of crv3d. 
    orientation2d (I) Array of orientations for each element in crv2d.
    makecopy (I)  If true, makes a copy of crv3d and crv2d. 
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCurveBoundary& set(
    int numberOfCurves, 
    const OdGeEntity3d *const * crv3d,
    const OdGeCurve2d *const * crv2d, 
    bool* orientation3d,
    bool* orientation2d, 
    bool makeCopy = true);

  // Curve ownership.
  //
  
  /**
    Description:
    Returns true if and only if this curve boundary is the owner of the 
    curve boundary representation data.
  */  
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  bool isOwnerOfCurves() const;

  /**
    Description:
    Forces this curve boundary to own the curve data, and returns a reference to this boundary.

    Remarks:
    If the external boundary owns the data, it will be destroyed when
    the boundary is destroyed.
  */
  /**
    Note:
    This function is not implemented, and will generate a link error if you reference it.
    It will be implemented in a future *release*.
  */
  OdGeCurveBoundary& setToOwnCurves();

protected:
  friend class OdGeImpCurveBoundary;

  OdGeImpCurveBoundary *mpImpBnd;
  int mDelBnd;
};

#include "DD_PackPop.h"

#endif // OD_GE_CURVE_BOUNDARY_H


