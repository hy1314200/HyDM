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



#ifndef OD_GEVEC2D_H
#define OD_GEVEC2D_H /* {Secret} */

#include "GeGbl.h"
#include "GeTol.h"

#include "DD_PackPush.h"

class OdGeMatrix2d;

/**
    Description:
    This class represents vectors in 2D space. 

    Remarks:
    OdGeVector2d may be viewed as an array[2] of doubles.

    Library: Ge

   {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeVector2d
{
public:
  /**
    Arguments:
    source (I) Object to be cloned.
    xx (I) X coordinate.
    yy (I) Y coordinate.
    
    Remarks:
    If called with no arguments, constructs a zero-length vector.
  */
  OdGeVector2d () : x (0.0), y (0.0) {}
  
  OdGeVector2d (
    const OdGeVector2d& source) : x (source.x), y (source.y) {}
  
  OdGeVector2d (
    double xx, 
    double yy) : x (xx), y (yy) {}

  OdGeVector2d& operator= (
    const OdGeVector2d& vect)
  {
    x = vect.x;
    y = vect.y;
    return *this;
  }

  static const OdGeVector2d kIdentity; // Additive identity vector.
  static const OdGeVector2d kXAxis;    // X-Axis vector.
  static const OdGeVector2d kYAxis;    // Y-Axis vector.

  // Matrix multiplication.
  //
 
  friend GE_TOOLKIT_EXPORT OdGeVector2d operator* (
    const OdGeMatrix2d& xfm, 
    const OdGeVector2d& vect);
    

  /**
    Description:
    Sets this vector to the product matrix * vect  scale * vect, and returns
    a reference to this vector. 
    
    Arguments:
    matrix (I) Any 2D *matrix* 
    vect (I) Any 2D vector 
    scale (I) Scale factor.
  */
  OdGeVector2d& setToProduct (
    const OdGeMatrix2d& matrix, 
    const OdGeVector2d& vect)
  {
    *this = matrix * vect;
    return *this;
  }

  OdGeVector2d& setToProduct (
    const OdGeVector2d& vect, 
    double scale);
      
  /**
    Description:
    Applies the 2D transformation matrix to this vector,
    and returns a reference to this vector.

    Arguments:
    xfm (I) 2D transformation matrix.
  */
  OdGeVector2d& transformBy (
    const OdGeMatrix2d& xfm)  { return *this = xfm * *this;}
    
  /**
    Description:
    Rotates this vector the specified *angle*, and returns
    a reference to this vector. 

    Arguments:
    angle (I) Rotation *angle*.
  */
  OdGeVector2d& rotateBy (
    double angle);
    
  /**
    Description:
    Mirrors this vector about the specified 2D *line*
    and returns a reference to this vector.

    Arguments:
    line (I) Mirror *Line*.
  */
  OdGeVector2d& mirror (
    const OdGeVector2d& line);

  // Scale multiplication.
  //

  OdGeVector2d operator* (
    double scale) const;
  friend GE_TOOLKIT_EXPORT OdGeVector2d operator* (
    double scale, const OdGeVector2d& vector);

  OdGeVector2d& operator*= (
    double scale);
    
  OdGeVector2d  operator/ (
    double scale) const;
  OdGeVector2d& operator/= (double scale);

  // Addition and subtraction of vectors.
  //

  OdGeVector2d operator+ (
    const OdGeVector2d& vect) const;
  OdGeVector2d& operator+= (
    const OdGeVector2d& vect);
  OdGeVector2d operator- (
    const OdGeVector2d& vect) const;
  OdGeVector2d& operator-= (
    const OdGeVector2d& vect);
    
  /**
    Description:
    Sets this vector to vector1 + vector1, and returns a reference to this vector.
    
    Arguments:
    vector1 (I) Any 2D vector.
    vector2 (I) Any 2D vector.
  */  
  OdGeVector2d& setToSum (
    const OdGeVector2d& vector1, 
    const OdGeVector2d& vector2);
    
  OdGeVector2d operator- () const;
  
  /**
    Description:
    Negates this vector (-x, -y), and returns a reference to this vector.
  */
  OdGeVector2d& negate ();

  // Perpendicular vector
  //

  /**
    Description:
    Returns the vector perpendicular to this one (-y, x).
  */
  OdGeVector2d perpVector () const;

  // Angle argument.
  //
  
  /**
    Description:
    Returns the *angle* between this vector and the X-axis
    
    Remarks:
    The range of this method is [0, Oda2PI]
  */
  double angle () const;

  // Returns less angle between this vector and vector
  // This function is commutative, a.angleTo (b) == b.angleTo (a)

  /**
    Description:
    Returns the smallest *angle* to the specified vector.
    
    Arguments:
    vect (I) Any 2D vector.
    
    Remarks:
    The range of this method is [0, OdaPI]
    
    This function is commutative:
    
        a.angleTo (b) == b.angleTo (a)
  */
  double angleTo (
    const OdGeVector2d& vect) const;

  // Returns angle between this vector and vector in counterclockwise
  // direction
  

  /**
    Description:
    Returns the *angle* from this vector to the specified vector in the counterclockwise direction.
    
    Arguments:
    vect (I) Any 2D vector.
    
    Remarks:
    The range of this method is [0, Oda2PI]
  */
  double angleToCCW (
    const OdGeVector2d& vect) const
  {
    return crossProduct (vect) > 0.0 ? angleTo (vect) : -angleTo (vect);
  }
    

  // Vector length operations.
  //
  
  /**
    Description:
    Returns the unit vector codirectional with this vector.
    
    Arguments:
    tol (I) Geometric tolerance.
        
    Remarks:
    If the length() < tol, this vector is returned.
  */
  OdGeVector2d normal (
    const OdGeTol& tol = OdGeContext::gTol) const;
    
  /**
    Description:
    Sets this vector to the unit vector codirectional with this vector,
    and returns a reference to this vector
    
    Arguments:
    tol (I) Geometric tolerance.
    status (O) Receives the *status* of normalization.

    Remarks:
    If this.length() < tol, this vector is unchanged, and kThis is returned in status.

    Possible values for status are as follows:

    @untitled table
    kOk 
    k0This
  */
  OdGeVector2d& normalize (
    const OdGeTol& tol = OdGeContext::gTol);
  OdGeVector2d& normalize (
    const OdGeTol& tol, 
    OdGeError& status);
    
  /**
    Description:
    Sets this vector to the unit vector codirectional with this vector,
    and returns the *length* prior to normalization.
    
    Remarks:
    If this.length() <= 0.0, this vector is unchanged.
  */
   double normalizeGetLength ();

  // Possible errors:  k0This.  Returns object unchanged on error.

  /**
    Description:
    Returns the *length* of this vector.
  */      
  double length () const;
  
  /**
    Description:
    Returns the square of the *length* of this vector.
  */      
  double lengthSqrd () const;
  
  /**
    Description:
    Returns true if and only if the *length* of this vector is 1.0 within the specified tolerance.

    Arguments:
    tol (I) Geometric tolerance.
  */      
  bool isUnitLength (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if the *length* of this vector is 0.0 within the specified tolerance.

    Arguments:
    tol (I) Geometric tolerance.
  */      
  bool isZeroLength (
    const OdGeTol& tol = OdGeContext::gTol) const;

  // Direction tests.
  //

  
  /**
    Description:
    Returns true if and only if the specified vector is parallel to this vector within the specified tolerance.

    Arguments:
    vect (I) Any 2D vector.
    tol (I) Geometric tolerance.
    status (O) Receives the *status* of test.

    Remarks:
    If the length of either vector is < tol, kThis is returned in status.

    Possible values for status are as follows:

    @untitled table
    kOk
    k0This
    k0Arg1
  */      
  bool isParallelTo (
    const OdGeVector2d& vect,
    const OdGeTol& tol = OdGeContext::gTol) const;
    
  bool isParallelTo (
    const OdGeVector2d& vect, 
    const OdGeTol& tol,
    OdGeError& status) const;
    
  // Possible errors:  k0This, k0Arg1.
  // Returns false on error.
  
  /**
    Description:
    Returns true if and only if the specified vector is codirectional to this vector within the specified tolerance.

    Arguments:
    vect (I) Any 2D vector.
    tol (I) Geometric tolerance.
    status (O) Receives the *status* of test.

    Remarks:
    If the length of either vector is < tol, kThis is returned in status.

    Possible values for status are as follows:

    @untitled table
    kOk       
    k0This    
    k0Arg1    
  */      
  bool isCodirectionalTo (
    const OdGeVector2d& vect,
    const OdGeTol& tol=OdGeContext::gTol) const;
  bool isCodirectionalTo (
    const OdGeVector2d& vect, 
    const OdGeTol& tol,
    OdGeError& status) const;
    
  // Possible errors:  k0This, k0Arg1.
  // Returns false on error.
  
  
  /**
    Description:
    Returns true if and only if the specified vector is perpendicular to this vector within the specified tolerance.

    Arguments:
    vect (I) Any 2D vector.
    tol (I) Geometric tolerance.
    status (O) Receives the *status* of test.

    Remarks:
    If the length of either vector is < tol, kThis is returned in status.

    Possible values for status are as follows:

    @untitled table
    kOk       
    k0This    
    k0Arg1    
  */      
  bool isPerpendicularTo (
    const OdGeVector2d& vect,
    const OdGeTol& tol = OdGeContext::gTol) const;
  bool isPerpendicularTo (
    const OdGeVector2d& vect, 
    const OdGeTol& tol,
    OdGeError& status) const;
    
  // Possible errors:  k0This, k0Arg1.
  // Returns false on error.

  // Dot product.
  //
  
  /**
    Description:
    Returns the dot product of this vector and the specified vector.
    
    Arguments:
    vect (I) Any 2D vector.
  */
  double dotProduct (
    const OdGeVector2d& vect) const;

  // Z-projection of cross product
  
  /**
    Description:
    Returns the z value of the cross product of this vector with
    the specified vector.
    
    Arguments:
    vect (I) Any 2D vector.
  */
  double  crossProduct (
    const OdGeVector2d& vect) const
  {
    return x * vect.y - y * vect.x;
  }

  // Tests for equivalence using the Euclidean norm.
  //
  
  bool operator== (
    const OdGeVector2d& vect) const;
  bool operator!= (
    const OdGeVector2d& vect) const;
    
  /**
    Description:
    Returns true if and only if vector is identical to this vector,
    within the specified tolerance.

    Arguments:
    vect (I) Any 2D vector.
    tol (I) Geometric tolerance.
  */
  bool isEqualTo (
    const OdGeVector2d& vect,
    const OdGeTol& tol = OdGeContext::gTol) const;

  // For convenient access to the data.
  //
  
  /**
    Arguments:
    i (I) Index of coordinate.

    Remarks:
    Returns or references the ith coordinate of this vector.

    o 0 returns or references the X coordinate.
    o 1 returns or references the Y coordinate.
  */
  double operator[] (
    unsigned int i) const;
  double& operator[] (
    unsigned int i) ;
  
  /**
    Description:
    Sets this vector to the specified X and Y coordinates, 
    and returns a reference to this vector.

    Arguments:
    xx (I) X coordinate.
    yy (I) Y coordinate.
  */
  OdGeVector2d& set (
    double xx, 
    double yy)
  {
    x = xx;
    y = yy;
    return *this;
  }    

  // Convert to/from matrix form.
  //

  /**
    Remarks:
    Returns the equivalent 2D tranformation matrix.
  */
  operator OdGeMatrix2d () const;

  // Co-ordinates.
  //
  double x; // X coordinate.
  double y; // Y coordinate.
};


#include "DD_PackPop.h"

#endif


