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



#ifndef OD_GENURBSF_H
#define OD_GENURBSF_H /* {Secret} */


#include "GeSurface.h"
#include "GeKnotVector.h"
#include "OdPlatformSettings.h"
#include "DD_PackPush.h"

class OdGeNurbCurve3d;

static const int derivArraySize = 3;
/*
    Description:
    Defines VectorDerivArray type.
*/
typedef OdGeVector3d VectorDerivArray[derivArraySize][derivArraySize];
/*
    Description:
    Defines the WDerivArray type.
*/
typedef double WDerivArray [derivArraySize][derivArraySize];

/**
    Description:
    This class represents non-uniform, rational B-Spline (NURB) surfaces.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeNurbSurface : public OdGeSurface
{
public:
  /** 
    Arguments:
    degreeInU (I) Degree in the U direction. 
    degreeInV (I) Degree in the V direction.
    propsInU (I) Properties in the U direction.
    propsInV (I) Properties in the V direction.
    numControlPointsInU (I) Number of control points in the U direction.
    numControlPointsInV (I) Number of control points in the V direction.
    controlPoints (I) Array of 3D control points.
    weights (I) Array of *weights*
    uKnots (I) Knot vector in the U direction.
    vKnots (I) Knot vector in the V direction.
    tol (I) Geometric tolerance.
    source (I) Object to be cloned.
   
    Remarks:
    propsInU and propsInV utilize OdGe::NurbSurfaceProperties values.
  */
  OdGeNurbSurface ();
  OdGeNurbSurface (
    int degreeInU, 
    int degreeInV, 
    int propsInU, 
    int propsInV,
    int numControlPointsInU, 
    int numControlPointsInV,
    const OdGePoint3dArray& controlPoints,
    const OdGeDoubleArray& weights,
    const OdGeKnotVector& uKnots,
    const OdGeKnotVector& vKnots,
    const OdGeTol& tol = OdGeContext::gTol);
  OdGeNurbSurface (
    const OdGeNurbSurface& source);
  virtual ~OdGeNurbSurface ();

  // OdGeEntity3d functions

  virtual bool isKindOf (
  OdGe::EntityId entType) const;
  virtual OdGe::EntityId type () const;

  // OdGeSurface functions

  virtual void getEnvelope (
    OdGeInterval& intrvlX, 
    OdGeInterval& intrvlY) const;

  /**
    Description:
    Returns true if and only if this surface is closed in the U direction.

    Arguments:
    tol (I) Geometric tolerance.
  */  
  virtual bool isClosedInU (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if this surface is closed in the V direction.

    Arguments:
    tol (I) Geometric tolerance.
  */  
  virtual bool isClosedInV (
    const OdGeTol& tol = OdGeContext::gTol) const;

  // Assignment.
  //
  
  OdGeNurbSurface& operator = (
    const OdGeNurbSurface& nurb);

  // Geometric properties.
  //
  
  /**
    Description:
    Returns true if and only if this surface is rational in the U direction.

    Arguments:
    tol (I) Geometric tolerance.
  */  
  bool isRationalInU () const;

  /**
    Description:
    Returns true if and only if this surface is periodic in the U direction, and returns the *period*.

    Arguments:
    period (I) Period.
  */  
  bool isPeriodicInU (double& period) const;

  /**
    Description:
    Returns true if and only if this surface is rational in the V direction.

    Arguments:
    tol (I) Geometric tolerance.
  */  
  bool isRationalInV () const;

  /**
    Description:
    Returns true if and only if this surface is periodic in the U direction, and returns the *period*.

    Arguments:
    period (I) Period.
  */  
  bool isPeriodicInV (double& period) const;
  
  /**
    Description:
    Returns a description of the singularity in the U direction:
    
    Remarks:
    singularityInU utilizes OdGe::NurbSurfaceProperties values.
  */
  int singularityInU () const;

  /**
    Description:
    Returns a description of the singularity in the V direction:
    
    Remarks:
    singularityInV utilizes OdGe::NurbSurfaceProperties values.
  */
  int singularityInV () const;

  // Definition data.
  //
  
  /**
    Desctiption:
    Returns the degree in the U direction.
  */
  int degreeInU () const;

  /**
    Desctiption:
    Returns the number of control points in the U direction.
  */
  int numControlPointsInU () const;
  
  /**
    Desctiption:
    Returns the degree in the V direction.
  */
  int degreeInV () const;
  
  /**
    Desctiption:
    Returns the number of control points in the V direction.
  */
  int numControlPointsInV () const;

  /**
    Description:
    Returns the array of control points.
    
    controlPoints (O) Receives an array of 3D control points.
  */  
  void getControlPoints (
    OdGePoint3dArray& controlPoints) const;
    
  /**
    Description:
    Returns the array of *weights*.
    
    Arguments:
    weights (I) Array of *weights*
  */  
  bool getWeights (
    OdGeDoubleArray& weights) const;
  
  /**
    Description:
    Returns the number of knots in the U direction.
  */  
  int numKnotsInU () const;
  
  /**
    Description:
    Returns the knot vector in the U direction.
    
    Arguments:
    uKnots (I) Knot vector in the U direction.
  */
  void getUKnots (
    OdGeKnotVector& uKnots) const;
  
  /**
    Description:
    Returns the number of knots in the V direction.
  */  
  int numKnotsInV () const;
  
  /**
    Description:
    Returns the knot vector in the V direction.
    
    Arguments:
    vKnots (I) Knot vector in the V direction.
  */
  void getVKnots (
    OdGeKnotVector& vKnots) const;
    
  /**
    Description:
    Returns the data used to define this surface.
    
    Arguments:
    degreeInU (O) Receives the Degree in the U direction. 
    degreeInV (O) Receives the Degree in the V direction.
    propsInU (O) Receives the properties in the U direction.
    propsInV (O) Receives the properties in the V direction.
    numControlPointsInU (O) Receives the number of control points in the U direction.
    numControlPointsInV (O) Receives the number of control points in the V direction.
    controlPoints (O) Receives an array of 3D control points.
    weights (O) Receives an array of *weights*
    uKnots (O) Receives the knot vector in the U direction.
    vKnots (O) Receives the knot vector in the V direction.
   
    Remarks:
    propsInU and propsInV utilize OdGe::NurbSurfaceProperties values.
  */  
  void getDefinition (
    int& degreeInU, 
    int& degreeInV,
    int& propsInU, 
    int& propsInV,
    int& numControlPointsInU,
    int& numControlPointsInV,
    OdGePoint3dArray& controlPoints,
    OdGeDoubleArray& weights,
    OdGeKnotVector& uKnots,
    OdGeKnotVector& vKnots) const;

  // Reset surface
  //
  
  /**
    Description:
    Sets the parameters for this spline according to the arguments, 
    and returns a reference to this spline.

    Arguments:
    degreeInU (I) Degree in the U direction. 
    degreeInV (I) Degree in the V direction.
    propsInU (I) Properties in the U direction.
    propsInV (I) Properties in the V direction.
    numControlPointsInU (I) Number of control points in the U direction.
    numControlPointsInV (I) Number of control points in the V direction.
    controlPoints (I) Array of 3D control points.
    weights (I) Array of *weights*
    uKnots (I) Knot vector in the U direction.
    vKnots (I) Knot vector in the V direction.
    tol (I) Geometric tolerance.
    source (I) Object to be cloned.
   
    Remarks:
    propsInU and propsInV utilize OdGe::NurbSurfaceProperties values.
  */
  OdGeNurbSurface& set (
    int degreeInU, 
    int degreeInV,
    int propsInU, 
    int propsInV,
    int numControlPointsInU,
    int numControlPointsInV,
    const OdGePoint3dArray& controlPoints,
    const OdGeDoubleArray& weights,
    const OdGeKnotVector& uKnots,
    const OdGeKnotVector& vKnots,
    const OdGeTol& tol = OdGeContext::gTol);

  // OdGePoint3d evalPoint (const OdGePoint2d& param, int hintU = -1, int hintV = -1,
  // const OdGeNurbCurve3d* pLocalCurves = NULL) const;
  
  OdGePoint2d paramOf (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& p) const;
    
  virtual OdGePoint3d evalPoint (
    const OdGePoint2d& param, 
    int numDeriv,
    OdGeVector3dArray& 
    derivatives, 
    OdGeVector3d& normal) const;
  DD_USING (OdGeSurface::evalPoint);

  virtual OdGeEntity3d* copy () const;

  // computes isolines
  
  /**
    Description:
    Returns the *isoline* for the specified value of V.
    
    Arguments:
    V (I) Value of V.
    isoline (O) Receives the isoline for the specified value of V. 
  */
  void computeVIsoLine (
    double V, 
    OdGeNurbCurve3d& isoline) const;

  /**
    Description:
    Returns the *isoline* for the specified value of U.
    
    Arguments:
    V (I) Value of U.
    isoline (O) Receives the isoline for the specified value of U. 
  */
  void computeUIsoLine (
    double U, 
    OdGeNurbCurve3d& isoline) const;

  /**
    Description:
    Returns the *derivatives* at the point specified by param.

    Arguments:
    param (I) Parameter to be evaluated.
    numDeriv (I) Number of *derivatives* to be computed.
    derivatives (O) Receives an array of *derivatives* at the *point* corresponding to param.
  */
  bool getDerivativesAt (
    const OdGePoint2d& param, 
    OdUInt32 numDeriv,
    VectorDerivArray derivatives) const;

protected:
  int m_degreeU;
  int m_degreeV;
  int m_propsInU;
  int m_propsInV;
  int m_numControlPointsInU;
  int m_numControlPointsInV;
  OdGePoint3dArray m_controlPoints;
  OdGeDoubleArray m_weights; 
  OdGeKnotVector m_uKnots;
  OdGeKnotVector m_vKnots;
  OdGeTol m_tol;
 
  /** { Secret } */
  class SpanGridData
  {
  public:
    SpanGridData () : m_nSamples (0) {}
    OdUInt32 m_nSamples; // number of samples in kvants
    // full number of samples (in samples) is 
    // (1 << (m_nSamples-1)) * numSamplesKvant
    OdGePoint3dArray m_gridPoints;
  };
  mutable OdArray<SpanGridData*> m_grid;
  /* mutable OdGePoint3dArray* m_gridPointsPtr;
  mutable OdUInt32 m_nSamples; */

  /** { Secret } */
  bool compute_Aders_wders (
    const OdGePoint2d& param, 
    OdUInt32 nDerivs, 
    VectorDerivArray Aders, 
    WDerivArray wders) const;

  // the index in "m_controlPoints" of control *point* (i,j):

  /** { Secret } */
  int loc (
    int i, 
    int j) const 
  { return i*m_numControlPointsInV+j; }

  // the index in "m_grid" of span (hintU,hintV)

  /** { Secret } */
  OdUInt32 locGrid (
    int hintU, 
    int hintV) const 
  {return (hintU-m_degreeU)* (m_numControlPointsInV-m_degreeV) + (hintV-m_degreeV); }

  // internal helpful function

  /** { Secret } */
  bool isInConvexHullPrevPts (
    const OdGePoint3d& point, 
    OdUInt32 hintU, 
    OdUInt32 hintV, 
    const OdGeTol& tol) const;

  /** { Secret } */
  void calculateNURBSProperties ();
};

#include "DD_PackPop.h"

#endif // OD_GENURBSF_H

