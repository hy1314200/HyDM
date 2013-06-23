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



#ifndef OD_GELNSG3D_H
#define OD_GELNSG3D_H /* {Secret} */

class OdGeLineSeg2d;

#include "GeLinearEnt3d.h"
#include "GeInterval.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents line segments in 3D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeLineSeg3d : public OdGeLinearEnt3d
{
  OdGeInterval m_Interval;
public:

  virtual ~OdGeLineSeg3d () {}

  /**
    Arguments:
    line (I) Any 3D *line*.
    point (I) Any 3D *point*.
    point1 (I) Any 3D *point*.
    point2 (I) Any 3D *point*.
    vect (I) Any 3D vector.
    source (I) Object to be cloned.

    Remarks:
    point and vect construct a *line* segment between points point and point + vect. vect cannot have a zero *length*.

    point1 and point2 construct a *line* segment between points point1 and point2. The 
    points cannot be coincident.

    If called with no arguments, constructs a *line* segment between the points (0,0) and (1,0).
  */
  OdGeLineSeg3d ();
  OdGeLineSeg3d (
    const OdGeLineSeg3d& source);
  OdGeLineSeg3d (
    const OdGePoint3d& point, 
    const OdGeVector3d& vect);
  OdGeLineSeg3d (
    const OdGePoint3d& point1, 
    const OdGePoint3d& point2);

  virtual OdGeEntity3d* copy () const;
  // void getBisector (OdGePlane& plane) const;

  // OdGePoint3d baryComb (double blendCoeff) const;


  /**
    Description:
    Returns the *start point* of this *line*.
  */
  OdGePoint3d startPoint () const;

  /**
    Description:
    Returns the *midpoint* of this *line*.
  */
  OdGePoint3d midPoint () const;

  /**
    Description:
    Returns the *end point* of this *line*.
  */
  OdGePoint3d endPoint () const;


  /**
    Description:
    Returns the *length* of this *line*.
  */
  double length () const;

  // double length (double fromParam, double toParam,
  // double tol = OdGeContext::gTol.equalPoint ()) const;

  /**
    Description:
    Sets the parameters for this *line* according to the arguments, and returns a reference to this *line*.

    Arguments:
    point (I) Any 3D *point*.
    point1 (I) Any 3D *point*.
    point2 (I) Any 3D *point*.
    vect (I) Any 3D vector.

    Remarks:
    point and vect construct a *line* segment between points point and point + vect. vect cannot have a zero *length*.

    point1 and point2 construct a *line* segment between points point1 and point2. The 
    points cannot be coincident.
  */
  OdGeLineSeg3d& set (
    const OdGePoint3d& point, 
    const OdGeVector3d& vect);
  OdGeLineSeg3d& set (
    const OdGePoint3d& point1, 
    const OdGePoint3d& point2);

  // OdGeLineSeg3d& set (const OdGeCurve3d& curve1,
  // const OdGeCurve3d& curve2,
  // double& param1, double& param2,
  // bool& success);
  // OdGeLineSeg3d& set (const OdGeCurve3d& curve,
  // const OdGePoint3d& point, double& param,
  // bool& success);


  OdGeLineSeg3d& operator = (
    const OdGeLineSeg3d& line);

  OdGe::EntityId type () const;

  OdGeVector3d direction () const;

  // NB: this implementation doesn't return overlap region,
  // since it is needn't for us now.

  /**
    Description:
    Returns true, and the *overlap* region, if and only if the specified
    line overlaps with this one.

    Arguments:
    line (I) Any 3D *line*.
    overlap (O) Receives a pointer to the *overlap* region.
    tol (I) Geometric tolerance.

    Remarks:
    The *overlap* region is created with the new operator. It is up to the caller to 
    delete the *overlap* region.
  */
  bool overlap (
    const OdGeLinearEnt3d& line,
    OdGeLinearEnt3d& overlap,
    const OdGeTol& tol = OdGeContext::gTol) const;

  // Matrix multiplication (virtual oberride)

  OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  bool isOn (
    const OdGePoint3d& point, const OdGeTol& tol = OdGeContext::gTol) const;

  OdGeCurve3d& reverseParam ();

  virtual void getInterval (
    OdGeInterval&) const;
  virtual void getInterval (
    OdGeInterval& interval, 
    OdGePoint3d& start, 
    OdGePoint3d& end) const;
  virtual bool setInterval (const OdGeInterval&);
  OdGeCurve3d& setInterval ();

  virtual double length (
    double fromParam, 
    double toParam, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  virtual double paramAtLength (
    double datumParam, 
    double length, 
    bool posParamDir = true, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  OdGePoint3d evalPoint (
    double param) const;
  virtual OdGePoint3d evalPoint (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& derivatives) const;
  virtual double paramOf (
    const OdGePoint3d& , 
    const OdGeTol& = OdGeContext::gTol) const;

  bool intersectWith (
    const OdGeLinearEnt3d& line,
    OdGePoint3d& intPt,
    const OdGeTol& tol = OdGeContext::gTol) const;

  // Polygonize curve to within a specified tolerance.
  //

  void appendSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const;

  DD_USING (OdGeLinearEnt3d::appendSamplePoints);

  bool hasStartPoint (
    OdGePoint3d& startPoint) const;
  bool hasEndPoint (
    OdGePoint3d& endPoint) const;
};

#include "DD_PackPop.h"

#endif


