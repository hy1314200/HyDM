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



#ifndef OD_GEPLIN3D_H
#define OD_GEPLIN3D_H /* {Secret} */

#include "OdPlatform.h"

#include "GeSplineEnt3d.h"

#include "DD_PackPush.h"

// maybe will be child of OdGeSplineEnt3d (not OdGeCurve3d) in future

/**
    Description:
    This class represents piecewise linear splines in 3D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGePolyline3d : public OdGeCurve3d //OdGeSplineEnt3d
{
private:
  OdGeInterval     m_domain;
  OdGePoint3dArray m_points;
  OdGeDoubleArray  m_lengths;
  void setDomainFromPoints();
  void updateLengths();

public:

 /**
    Arguments:
    points (I) An OdGePoint3dArray of fit *points*.
    pPoints (I) An array of fit *points*.
    nbPoints (I) The number of *points* in pPoints.
    crv (I) A 2D curve to be approximated as a polyline.
    approxEps (I) Approximate geometric tolerance. 
    source (I) Object to be cloned.
  */
  OdGePolyline3d ();
  OdGePolyline3d (
    const OdGePolyline3d& source);
  OdGePolyline3d (
    const OdGePoint3dArray& points);
  OdGePolyline3d (
    OdInt32 nbPoints, 
    const OdGePoint3d* pPoints);
  // OdGePolyline3d (const OdGeKnotVector& knots,
  // const OdGePoint3dArray& controlPoints);
  
  OdGePolyline3d (
    const OdGeCurve3d& crv, 
    double approxEps);

  /**
    Description:
    Returns the number of fit points.
  */
 int numFitPoints () const;
 
  /**
    Description:
    Returns the fit *point* at the specified index.

    Arguments:
    idx (I) Index of fit *point*.
  */
    OdGePoint3d fitPointAt (
      int idx) const;

 // OdGeSplineEnt3d& setFitPointAt (int idx, const OdGePoint3d& point);

 // Assignment operator.
 
 OdGePolyline3d& operator = (
   const OdGePolyline3d& pline);

 OdGe::EntityId type () const 
 { return OdGe::kPolyline3d; }

  void getInterval (
    OdGeInterval& interval) const;

  bool hasStartPoint (
    OdGePoint3d& startPoint) const;

  bool hasEndPoint (
    OdGePoint3d& endPoint) const;

  OdGePoint3d evalPoint (
    double param) const;
  OdGePoint3d evalPoint (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& derivatives) const;

  /**
    Description:
    Returns the *point* on the specified segment corresponding to the specified parameter value.
    
    Arguments:
    param (I) Parameter on specified segment.
    numSeg (I) The segment to be queried.
  */
  OdGePoint3d evalPointSeg(
    double param, 
    int& numSeg) const;

  double length (
    double fromParam, 
    double toParam, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  double paramAtLength (
    double datumParam, 
    double length, 
    bool posParamDir = true, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  void appendSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const;

  DD_USING(OdGeCurve3d::appendSamplePoints);
};

#include "DD_PackPop.h"

#endif // OD_GEPLIN3D_H

