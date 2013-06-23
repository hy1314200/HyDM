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



#ifndef OD_GESCL3D_H
#define OD_GESCL3D_H /* {Secret} */


class OdGeMatrix3d;
#include "GeGbl.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents scaling transformations (scale vectors) in 3D space.
    
    Remarks:
    OdGeScale3d may be viewed as an array[3] of doubles.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeScale3d
{
public:

 /**
    Arguments:
    factor (I) Uniform scale *factor*.
    xFactor (I) The X scale *factor*.
    yFactor (I) The Y scale *factor*.
    zFactor (I) The Z scale *factor*.
    source (I) Object to be cloned.
  */
  OdGeScale3d () 
    : sx (1.0), sy (1.0), sz (1.0) {}
    
  OdGeScale3d (
    const OdGeScale3d& source) 
    : sx (source.sx), sy (source.sy), sz (source.sz) {}
    
  OdGeScale3d (
    double factor) 
    : sx (factor), sy (factor), sz (factor) {}
    
  OdGeScale3d (
    double xFactor, 
    double yFactor, double zFactor) 
    : sx (xFactor), sy (yFactor), sz (zFactor) {}

  OdGeScale3d& operator = (
    const OdGeScale3d& scaleVec)
  {
    sx = scaleVec.sx;
    sy = scaleVec.sy;
    sz = scaleVec.sz;
    return *this;
  }

  static const OdGeScale3d kIdentity; // Multplicitive identity scale.
  
  /**
    Arguments:
    scaleVec (I) Any 3D scale vector.
    factor (I) Uniform scale *factor*.
  */
  OdGeScale3d operator * (
    const OdGeScale3d& scaleVec) const;
  OdGeScale3d operator * (
    double factor) const;

  friend OdGeScale3d operator * (
    double factor, 
    const OdGeScale3d& scaleVec);
    
  /**
    Arguments:
    scaleVec (I) Any 3D scale vector.
    factor (I) Uniform scale *factor*.

    Remarks:
    Multiplication of scale vectors is defined as follows:
    
              scl * [xs ys zs]              = [scl*xs scl*ys scl*zs]
              [xs1 ys1 zs1] * [xs2 ys2 zs2] = [xs1*xs2 ys1*ys2 zs1*zs2]
  */
  OdGeScale3d& operator *= (
    const OdGeScale3d& scaleVec);
  OdGeScale3d& operator *= (
    double factor);
    
  /**
    Description:
    Sets this scale vector to the product leftSide * (this scale vector), and returns
    a reference to this scale vector.
    
    Arguments:
    leftSide (I) Any 3D scale vector.

    Remarks:
    Scale muliplications is commutative. 
  */
  OdGeScale3d& preMultBy (
    const OdGeScale3d& leftSide);

  /**
    Description:
    Sets this scale vector to the product (this scale vector) * (rightSide), and returns
    a reference to this scale vector.
    
    Arguments:
    rightSide (I) Any 3D scale vector.

    Remarks:
    Scale muliplications is commutative. 
  */
   OdGeScale3d& postMultBy (
    const OdGeScale3d& rightSide);
 
  /**
    Description:
    Sets this scale vector to the product scaleVec1 * scaleVec2 or factor * scaleVec, and returns
    a reference to this scale vector.
    
    Arguments:
    factor (I) Uniform scale *factor*.
    scaleVec (I) Any 2D scale vector.
    scaleVec1 (I) Any 2D scale vector.
    scaleVec2 (I) Any 2D scale vector.
    
    Remarks:
    Multiplication of scale vectors is defined as follows:
    
              scl * [xs ys zs]              = [scl*xs scl*ys scl*zs]
              [xs1 ys1 zs1] * [xs2 ys2 zs2] = [xs1*xs2 ys1*ys2 zs1*zs2]
  */
  OdGeScale3d& setToProduct (
    const OdGeScale3d& scaleVec1, 
    const OdGeScale3d& scaleVec2);
  OdGeScale3d& setToProduct (
    const OdGeScale3d& scaleVec, double factor);
 
  /**
    Description:
    Returns the *inverse* of this scale vector. 
  */
  OdGeScale3d inverse () const;

  /**
    Description:
    Sets this scale vector to its *inverse*, and returns
    a reference to this scale vector. 
  */
  OdGeScale3d& invert ();
  
  /**
    Description:
    Returns true if and only if the scaling matrix corresponding to this scale vector isUniScaledOrtho ().
    
    Arguments:
    tol (I) Geometric tolerance.
  */
  bool isProportional (
    const OdGeTol& tol = OdGeContext::gTol) const;
  
  bool operator == (
    const OdGeScale3d& scaleVec) const;
  bool operator != (
    const OdGeScale3d& scaleVec) const;

  /**
    Description:
    Returns true if and only if scaleVec is identical to this one,
    within the specified tolerance.

    Arguments:
    scaleVec (I) Any 2D scale vector.
    tol (I) Geometric tolerance.
  */
  bool isEqualTo (
    const OdGeScale3d& scaleVec, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Arguments:
    i (I) Index of component.
    
    Remarks:
    Returns or references the ith component of the scale vector.

    o 0 returns or references the X scale factor.
    o 1 returns or references the Y scale factor.
    o 2 returns or references the Z scale factor.
  */
  double operator [] (
    unsigned int i) const { return * (&sx+i); }
  double& operator [] (
    unsigned int i) { return * (&sx+i); }
    
  /**
    Description:
    Sets this scale vector to the specified X and Y factors, and 
    returns a reference to this vectors.

    Arguments:
    xFactor (I) The X scale *factor*.
    yFactor (I) The Y scale *factor*.
    zFactor (I) The Z scale *factor*.
  */
  OdGeScale3d& set (
    double xFactor, 
    double yFactor, 
    double zFactor) 
  { sx = xFactor; sy = yFactor; sz = zFactor; return *this; }
  
  /**
    Remarks:
    Returns the equivalent 3D tranformation matrix.
  */
  operator OdGeMatrix3d () const;
  
  /**
    Description:
    Returns the transformation matrix equivalent to this scale vector.
    
    Arguments:
    xfm (O) Receives the 3D transformation matrix.
    
  */
  void getMatrix (
    OdGeMatrix3d& xfm) const;
    
  /**
    Description:
    Returns the scale vector corresponding to the
    lengths of the column vectors of the transformation matrix.  
    
    Arguments:
    xfm (O) Receives the 3D transformation matrix.
    
    Remarks:
    xfm must be scaled ortho; i.e., xfm.isScaledOrtho () == true.   
  */  
  OdGeScale3d& extractScale (
    const OdGeMatrix3d& xfm);
    
  /**
    Description:
    Returns the scale vector corresponding to the
    lengths of the column vectors of the transformation matrix,
    and sets the scale factor of the matrix to 1 .  
    
    Arguments:
    xfm (O) Receives the 2D transformation matrix.
    
    Remarks:
    xfm must be scaled ortho; i.e., xfm.isScaledOrtho () == true.   
  */  
   OdGeScale3d& removeScale (
    OdGeMatrix3d& xfm);
  
  bool isValid () const { return OdNonZero (sx) && OdNonZero (sy) && OdNonZero (sz); }

  double sx; // X scale *factor*.
  double sy; // Y scale *factor*.
  double sz; // Z scale *factor*.
};

#include "DD_PackPop.h"

#endif // OD_GESCL3D_H


