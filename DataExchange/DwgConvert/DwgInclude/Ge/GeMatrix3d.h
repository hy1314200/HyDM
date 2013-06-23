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
// DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GE_MATRIX_3D_H
#define OD_GE_MATRIX_3D_H /* {Secret} */

#include "GeGbl.h"
#include "GeMatrix2d.h"
#include "GePoint3d.h"

class OdGeLine3d;
class OdGeVector3d;
class OdGePlane;
class OdGeTol;
class OdGeScale3d;

#include "DD_PackPush.h"

/**
    Description:
    This class represents 3D transformation matrices that define affine 
    ( *translation*, *rotation*, and/or *scaling* ) transformations.
    
    Remarks:
    OdGeMatrix3d may be viewed as an array[4][4] of doubles.
    
    An OdGeMatrix3d, M, can be expressed as a 4 * 4 *matrix*, in the form
    
              a00   a01   a02   t0
              a10   a11   a12   t1
              a20   a21   a22   t2
                0     0     0    1
                
    The linear part of M is the *matrix*
                
              a00   a01   a02
              a10   a11   a12
              a20   a21   a22
                              
    The translational part of M is the column
    
              t0
              t1
              t2
              
    The origin of the coordinate system of M is (t0, t1, t2).

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeMatrix3d
{
public:
  OdGeMatrix3d ();

  static const OdGeMatrix3d kIdentity; // Multiplicative identity matrix.

  // Reset matrix.
  //

  /**
    Description:
    Sets this *matrix* to the identity *matrix*, and returns a reference to this *matrix*.
  */
  OdGeMatrix3d& setToIdentity ();
  
  /**
    Description:
    Sets any values within tol of 0 to 0.
  */
  void validateZero (
    const OdGeTol& tol = OdGeContext::gTol); // for correcting in case that Zero is corrupted

  // Multiplication.
  //

  /**
    Remarks:
    Returns the product (this *matrix*) * matrix. 
  */
  OdGeMatrix3d operator * (
    const OdGeMatrix3d& matrix) const;

  /**
    Remarks:
    Sets this *matrix* to the product (this *matrix*) * matrix, and returns
    a reference to this *matrix*. 
  */
  OdGeMatrix3d& operator *= (
    const OdGeMatrix3d& matrix);
  /**
    Description:
    Sets this *matrix* to the product leftSide * (this *matrix*), and returns
    a reference to this *matrix*. 
    
    Arguments:
    leftSide (I) Any 2D *matrix* 
  */
  OdGeMatrix3d& preMultBy (
    const OdGeMatrix3d& leftSide);

  /**
    Description:
    Sets this *matrix* to the product (this *matrix*) * rightSide, and returns
    a reference to this *matrix*. 
    
    Arguments:
    rightSide (I) Any 2D *matrix* 
  */
  OdGeMatrix3d& postMultBy (
    const OdGeMatrix3d& rightSide);

  /**
    Description:
    Sets this *matrix* to the product matrix1 * matrix2, and returns
    a reference to this *matrix*. 
    
    Arguments:
    matrix1 (I) Any 2D *matrix* 
    matrix2 (I) Any 2D *matrix* 
  */
  OdGeMatrix3d& setToProduct (
    const OdGeMatrix3d& matrix1, 
    const OdGeMatrix3d& matrix2);

  // Multiplicative inverse.
  //

  /**
    Description:
    Sets this *matrix* to its *inverse*, and returns
    a reference to this *matrix*. 
  */
  OdGeMatrix3d& invert ();

 
  // Test if it is a singular matrix. A singular matrix is not invertable.
  //

  /**
    Description:
    Returns true if and only if this *matrix* is singular.
    
    Remarks:
    o A *matrix* is singular if and only if its determinant == 0.
    o A singular *matrix* cannot be inverted.
  */
  bool isSingular (
    const OdGeTol& tol = OdGeContext::gTol) const;

  // Matrix transposition.
  //

  /**
    Description:
    Sets this *matrix* to its *transpose*, and returns
    a reference to this *matrix*. 
  */
  OdGeMatrix3d& transposeIt ();

  /**
    Description:
    Returns the *transpose* of this *matrix*. 
  */
  OdGeMatrix3d transpose () const;

  // Tests for equivalence using the infinity norm.
  //

  bool operator == (
    const OdGeMatrix3d& matrix) const { return isEqualTo (matrix); }
  bool operator != (
    const OdGeMatrix3d& matrix) const { return !isEqualTo (matrix); }

  /**
    Description:
    Returns true if and only if matrix is identical to this one,
    within the specified tolerance.

    Arguments:
    matrix (I) Matrix to be compared.
    tol (I) Geomentric tolerance.
  */
  bool isEqualTo (
    const OdGeMatrix3d& matrix, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  // Test scaling effects of matrix
  //

  /**
    Description:
    Returns true if and only the columns vectors of the linear part of
    this *matrix* are of equal length and mutually perpendicular within the
    specified tolerance. 

    Arguments:
    tol (I) Geomentric tolerance.
  */
  bool isUniScaledOrtho (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only the column vectors of the linear part of
    this *matrix* are mutually perpendicular within the
    specified tolerance.
    
    Arguments:
    tol (I) Geomentric tolerance.
  */
  bool isScaledOrtho (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /** { Secret } */
  bool isPerspective (
    const OdGeTol& tol = OdGeContext::gTol) const;

  // Determinant
  //
  
  /**
    Description:
    Returns the determinant of this *matrix*.
  */
  double det () const;

  // Set/retrieve translation.
  //
  
  /**
    Description:
    Sets the *translation* part of the *matrix* to the specified vector.
    
    Arguments:
    vect (I) Translation vector.
  */
  OdGeMatrix3d& setTranslation (
    const OdGeVector3d& vect);
    

  // Set/get coordinate system
  //
  
  /**
    Description:
    Sets this *matrix* to the *matrix* which maps
    the WCS to the coordinate system 
    defined by origin, X-axis, Y-axis, and Z-axis, and returns a reference
    to this *matrix*.
    
    Arguments:
    origin (I) Origin of the coordinate system.
    xAxis (I) X-axis of the coordinate system.
    yAxis (I) Y-axis of the coordinate system.
    zAxis (I) Z-axis of the coordinate system.
  */
  OdGeMatrix3d& setCoordSystem (
    const OdGePoint3d& origin, 
    const OdGeVector3d& xAxis,
    const OdGeVector3d& yAxis, 
    const OdGeVector3d& zAxis);

  /**
    Description:
    Returns the *origin*, X-axis, Y-axis, and Z-axis of the coordinate system 
    to which this *matrix* maps the WCS.
    
    Arguments:
    origin (O) Receives the origin of the coordinate system.
    xAxis (O) Receives the X-axis of the coordinate system.
    yAxis (O) Receives the Y-axis of the coordinate system.
    zAxis (O) Receives the Z-axis of the coordinate system.
  */
  void getCoordSystem (
    OdGePoint3d& origin, 
    OdGeVector3d& xAxis, 
    OdGeVector3d& yAxis, 
    OdGeVector3d& zAxis) const;
  
  /**
    Description:
    Returns the *origin* of the coordinate system 
    to which this *matrix* maps the WCS.
  */  
  OdGePoint3d getCsOrigin () const;

  /**
    Description:
    Returns the X-axis of the coordinate system 
    to which this *matrix* maps the WCS.
  */  
  OdGeVector3d getCsXAxis () const;

  /**
    Description:
    Returns the Y-axis of the coordinate system 
    to which this *matrix* maps the WCS.
  */  
  OdGeVector3d getCsYAxis () const;
  /**
    Description:
    Returns the Z-axis of the coordinate system 
    to which this *matrix* maps the WCS.
  */  
  OdGeVector3d getCsZAxis () const;

  // Set the matrix to be a specified transformation
  //
  
  /**
    Description:
    Sets this *matrix* to the *matrix* which translates
    by vect, and returns a reference
    to this *matrix*.
    
    Arguments:
    vect (I) Translation vector.
  */
  OdGeMatrix3d& setToTranslation (
    const OdGeVector3d& vect);

  /**
    Description:
    Sets this *matrix* to the *matrix* which rotates
    by angle about an axis passing through center, and returns a reference
    to this *matrix*.
    
    Arguments:
    angle (I) Rotation *angle*.
    axis (I) Rotation *axis*.
    center (I) Center of *rotation*.
  */
  OdGeMatrix3d& setToRotation (
    double angle, 
    const OdGeVector3d& axis,
    const OdGePoint3d& center = OdGePoint3d::kOrigin);

  /**
    Description:
    Sets this *matrix* to the *matrix* which scales
    by scale about center, and returns a reference
    to this *matrix*.
    
    Arguments:
    scale (I) Scale factor.
    center (I) Center of *scaling*.
  */
  OdGeMatrix3d& setToScaling (
    double scale, 
    const OdGePoint3d& center = OdGePoint3d::kOrigin);
  OdGeMatrix3d& setToScaling (
    const OdGeScale3d& scale, 
    const OdGePoint3d& center = OdGePoint3d::kOrigin);

  /**
    Description:
    Sets this *matrix* to the *matrix* which mirrors
    about the specified object, and returns a reference
    to this *matrix*.
    
    Arguments:
    mirrorPlane (I) Mirror plane.
    mirrorPoint (I) Mirror point.
    mirrorLine (I) Mirror line entity.
  */
  OdGeMatrix3d& setToMirroring (
    const OdGePlane& mirrorPlane);
  OdGeMatrix3d& setToMirroring (
    const OdGePoint3d& mirrorPoint);
  OdGeMatrix3d& setToMirroring (
    const OdGeLine3d& mirrorLine);
    
  /**
    Description:
    Sets this *matrix* to the *matrix* which parallel
    projects onto the specified plane in the 
    specified directionm and returns a reference
    to this *matrix*.
   
    Arguments:
    projectionPlane (I) Projection plane.
    projectDir (I) Projection direction.
  */
  OdGeMatrix3d& setToProjection (
    const OdGePlane& projectionPlane,
    const OdGeVector3d& projectDir);
    
  /**
    Description:
    Sets this *matrix* to the *matrix* which maps
    the coordinate system defined by fromOrigin, fromXAxis, fromYAxis, and fromZAxis,
    to the coordinate system 
    defined by toOrigin, toXAxis, toYAxis, and toZAxis, and returns a reference
    to this *matrix*.
    
    Arguments:
    fromOrigin (I) Origin of the initial coordinate system.
    fromXAxis (I) X-axis of the initial coordinate system.
    fromYAxis (I) Y-axis of the initial coordinate system.
    fromZAxis (I) Z-axis of the initial coordinate system.
    toOrigin (I) Origin of the initial coordinate system.
    toXAxis (I) X-axis of the initial coordinate system.
    toYAxis (I) Y-axis of the initial coordinate system.
    toZAxis (I) Z-axis of the initial coordinate system.
  */
  OdGeMatrix3d& setToAlignCoordSys (
    const OdGePoint3d& fromOrigin,
    const OdGeVector3d& fromXAxis,
    const OdGeVector3d& fromYAxis,
    const OdGeVector3d& fromZAxis,
    const OdGePoint3d& toOrigin,
    const OdGeVector3d& toXAxis,
    const OdGeVector3d& toYAxis,
    const OdGeVector3d& toZAxis);


  /**
    Description:
    Sets this *matrix* to the *matrix* which maps the WCS to
    the *plane* coordinate system, and returns a referece to this *matrix*.
    
    Arguments:
    normal (I) The *normal* to the *plane*.    
    plane (I) The *plane*.
    
    Remarks:
    If normal is specified, the *origin* of the *plane* is (0,0,0)    
  */    
  OdGeMatrix3d& setToWorldToPlane (
    const OdGeVector3d& normal);
  OdGeMatrix3d& setToWorldToPlane (
    const OdGePlane& plane);
    
  /**
    Description:
    Sets this *matrix* to the *matrix* which maps
    the *plane* coordinate system to the WCS, and returns a referece to this *matrix*.
    
    Arguments:
    normal (I) The *normal* to the *plane*.    
    plane (I) The *plane*.
    
    Remarks:
    If normal is specified, the *origin* of the *plane* is (0,0,0)    
  */    
  OdGeMatrix3d& setToPlaneToWorld (
    const OdGeVector3d& normal);
  OdGeMatrix3d& setToPlaneToWorld (
    const OdGePlane& plane);

  // Similar to above, but creates matrix on the stack.
  //
  
  /**
    Returns the *translation* vector of this *matrix*,
    or the *matrix* of the translation by vect.
    
    Arguments:
    vect (I) Translation vector.
  */
  static OdGeMatrix3d translation (
    const OdGeVector3d& vect);
  OdGeVector3d translation() const;
  
  /**
    Description:
    Returns the *matrix* which rotates
    by angle about an axis passing through center, and returns a reference
    to this *matrix*.
    
    Arguments:
    angle (I) Rotation *angle*.
    axis (I) Rotation *axis*.
    center (I) Center of *rotation*.
  */
  static OdGeMatrix3d rotation (
    double angle, 
    const OdGeVector3d& axis,
    const OdGePoint3d& center = OdGePoint3d::kOrigin);
    
  /**
    Description:
    Returns the *matrix* which scales
    by scale about center, and returns a reference
    to this *matrix*.
    
    Arguments:
    scale (I) Scale factor.
    center (I) Center of *scaling*.
  */
  static OdGeMatrix3d scaling (
    double scale, 
    const OdGePoint3d& center = OdGePoint3d::kOrigin);
  static OdGeMatrix3d scaling (
    const OdGeScale3d& scale, 
    const OdGePoint3d& center = OdGePoint3d::kOrigin);


  /**
    Description:
    Sets this *matrix* to the *matrix* which mirrors
    about the specified object, and returns a reference
    to this *matrix*.
    
    Arguments:
    mirrorPlane (I) Mirror plane.
    mirrorPoint (I) Mirror point.
    mirrorLine (I) Mirror line entity.
  */
  static OdGeMatrix3d mirroring (
    const OdGePlane& mirrorPlane);
  static OdGeMatrix3d mirroring (
    const OdGePoint3d& mirrorPoint);
  static OdGeMatrix3d mirroring (
    const OdGeLine3d& mirrorLine);

  /**
    Description:
    Returns the *matrix* which parallel
    projects onto the specified plane in the 
    specified direction.
   
    Arguments:
    projectionPlane (I) Projection plane.
    projectDir (I) Projection direction.
  */
  static OdGeMatrix3d projection (
    const OdGePlane& projectionPlane,
    const OdGeVector3d& projectDir);

  /**
    Description:
    Returns the *matrix* which maps
    the coordinate system defined by fromOrigin, fromXAxis, fromYAxis, and fromZAxis,
    to the coordinate system 
    defined by toOrigin, toXAxis, toYAxis, and toZAxis.
    
    Arguments:
    fromOrigin (I) Origin of the initial coordinate system.
    fromXAxis (I) X-axis of the initial coordinate system.
    fromYAxis (I) Y-axis of the initial coordinate system.
    fromZAxis (I) Z-axis of the initial coordinate system.
    toOrigin (I) Origin of the initial coordinate system.
    toXAxis (I) X-axis of the initial coordinate system.
    toYAxis (I) Y-axis of the initial coordinate system.
    toZAxis (I) Z-axis of the initial coordinate system.
  */
  static OdGeMatrix3d alignCoordSys (
    const OdGePoint3d& fromOrigin,
    const OdGeVector3d& fromXAxis,
    const OdGeVector3d& fromYAxis,
    const OdGeVector3d& fromZAxis,
    const OdGePoint3d& toOrigin,
    const OdGeVector3d& toXAxis,
    const OdGeVector3d& toYAxis,
    const OdGeVector3d& toZAxis);

  /**
    Description:
    Returns the *matrix* which maps the WCS to
    the *plane* coordinate system.
    
    Arguments:
    normal (I) The *normal* to the *plane*.    
    plane (I) The *plane*.
    
    Remarks:
    If normal is specified, the *origin* of the *plane* is (0,0,0)    
  */    
  static OdGeMatrix3d worldToPlane (
    const OdGeVector3d& normal);
  static OdGeMatrix3d worldToPlane (
    const OdGePlane&);
    
  /**
    Description:
    Returns the *matrix* which maps
    the *plane* coordinate system to the WCS.
    
    Arguments:
    normal (I) The *normal* to the *plane*.    
    plane (I) The *plane*.
    
    Remarks:
    If normal is specified, the *origin* of the *plane* is (0,0,0)    
  */    
  static OdGeMatrix3d planeToWorld (
    const OdGeVector3d& normal);
  static OdGeMatrix3d planeToWorld (
    const OdGePlane&);

  // Get the length of the MAXIMUM column of the
  // 3x3 portion of the matrix.
  //

  /**
    Description:
    Returns the scale factor of this *matrix*.
    
    Remarks:
    The scale factor is the square root of the longest column vector
    of the linear part of this *matrix*.
  */
  double scale (void) const;
  /**
    Returns the largest absolute value of the linear part of this *matrix*.
  */
  double norm () const;
  
  OdGeMatrix2d convertToLocal (
    OdGeVector3d& normal, 
    double& elev) const;

  /**
    Description:
    Returns true if and only if this *matrix* has an *inverse*,
    and the *inverse* of this *matrix*.
    
    Arguments:
    inverseMatrix (O) Receives the inverse *matrix*
    tol (I) Geometric tolerance. 
  */
  OdGeMatrix3d inverse () const;
  bool inverse (
    OdGeMatrix3d& inverseMatrix, 
    double tol) const;

  // For convenient access to the data.
  //
  
  /**
    Remarks:
    Returns or references entry[row] as matrix[row].

    Arguments:
    row (I) Row.
  */
  const double* operator[] (
    int row) const 
  { return entry[row]; }
  double* operator[] (
    int row) 
  { return entry[row]; }

  /**
    Remarks:
    Returns or references entry[row][column] as matrix(row,column).

    Arguments:
    row (I) Row.
    column (I) Column.
  */
  double operator () (
    int row, 
    int column) const 
  { return entry[row][column]; }
  double& operator () (
    int row, 
    int column) { return entry[row][column]; }

    // The components of the matrix.
    //
    double entry[4][4]; // [row][column] 

};

#include "DD_PackPop.h"

#endif // OD_GE_MATRIX_3D_H


