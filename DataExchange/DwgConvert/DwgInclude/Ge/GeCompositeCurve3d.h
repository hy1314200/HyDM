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



#ifndef OD_GECOMP3D_H
#define OD_GECOMP3D_H /* {Secret} */

#include "GeCurve3d.h"
#include "GeIntervalArray.h"
#include "SharedPtr.h"
#include "OdPlatform.h"

#include "DD_PackPush.h"

/**
    Description:
    This template class is a specialization of the OdSharedPtr class template for 3D curves.
*/
typedef OdSharedPtr<OdGeCurve3d> OdGeCurve3dPtr;

/**
    Description:
    This template class is a specialization of the OdArray class template for 3D curve shared pointers.
*/
typedef OdArray<OdGeCurve3dPtr> OdGeCurve3dPtrArray;

/**
Description:
    This class represents composite curves in 3D space.

    Remarks:
    Composite curves consists of pointers to any number of subcurves that
    are joined end to end. Each subcurve must be bounded.

    The parameter at the start of the composite curve is 0.0. The parameter at any
    point along the the composite curve is the approximate length of the
    composite curve up to that *point*.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeCompositeCurve3d : public OdGeCurve3d
{
public:
  /**
    Arguments:
    curvelist (I) Array of pointers to subcurves comprising the composite *curve*.
    pCurve    (I) Pointer to the first item of the array of the subcurves comprising 
    the composite curve 
    number    (I) Number of array items
    source (I) Object to be cloned.

    Remarks:
    The default constructor creates a composite curve that consists 
    of a single subcurve: a line segment from (0,0,0) to (1,0,0). 
  */
  OdGeCompositeCurve3d ();
  OdGeCompositeCurve3d (
    OdGeCurve3dPtrArray curveList);
  OdGeCompositeCurve3d (
    const OdGeCurve3d* pCurve,
    OdUInt32 number);
  OdGeCompositeCurve3d (
    const OdGeCompositeCurve3d& source);

  /**
    Description:
    Returns an array of pointers to subcurves comprising the composite *curve*.

    Arguments:
    curvelist (O) Receives an array of pointers to subcurves comprising the composite *curve*.
  */
  void getCurveList (
    OdGeCurve3dPtrArray& curveList) const;

  /**
    Description:
    Sets the curve list of the composite *curve*.

    Arguments:
    curveList (I) Array of pointers to subcurves comprising the composite *curve*.
    pCurve    (I) Pointer to the first item of the array of the subcurves comprising 
                  the composite curve 
    number    (I) Number of array items
  */
  OdGeCompositeCurve3d& setCurveList (
    OdGeCurve3dPtrArray curveList);
  OdGeCompositeCurve3d& setCurveList (
    const OdGeCurve3d* pCurve,
    OdUInt32 number);

  /**
    Description:
    Returns the parameter on a subcurve, and the index of that subcurve,
    corresponding to the specified parameter on the composite *curve*.

    Arguments:
    param (I) Parameter value on composite *curve*.
    crvNum (O) Receives the *curve* number of the subcurve.
  */
  double globalToLocalParam (
    double param, 
    int& crvNum) const; 

  /**
    Description:
    Returns the parameter on the composite curve, corresponding
    to the specified parameter on the specifed subcurve.

    Arguments:
    param (I) Parameter value on the subcurve.
    crvNum (I) Curve number of the subcurve.
  */
  double localToGlobalParam (
    double param, 
    int crvNum) const; 

  /**
    Remarks:
    All of the subcurves of the input curve are copied.         
  */
  OdGeCompositeCurve3d& operator = (
    const OdGeCompositeCurve3d& compCurve);

  virtual bool isKindOf (
    OdGe::EntityId entType) const;

  virtual OdGe::EntityId type () const;

  virtual OdGeEntity3d& transformBy (
    const OdGeMatrix3d& xfm);

  virtual bool hasStartPoint (
    OdGePoint3d& startPoint) const;

  virtual bool hasEndPoint (
    OdGePoint3d& endPoint) const;

  virtual void getInterval (
    OdGeInterval& interval) const;

  virtual bool setInterval (
    const OdGeInterval& interval);

  virtual double length (
    double fromParam, 
    double toParam, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  virtual double paramAtLength (
    double datumParam, 
    double length, 
    bool posParamDir = true, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  virtual OdGePoint3d evalPoint (
    double param) const;
  
  virtual OdGePoint3d evalPoint (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& derivatives) const;

  virtual void appendSamplePoints (
    double fromParam, 
    double toParam, 
    double approxEps, 
    OdGePoint3dArray& pointArray, 
    OdGeDoubleArray* pParamArray = 0) const;

  DD_USING(OdGeCurve3d::appendSamplePoints);

private:
  void updateLengths();

  OdGeCurve3dPtrArray m_curveList;
  OdGeDoubleArray     m_lengths;
  OdGeIntervalArray m_intervals;
  OdGeInterval        m_interval;
};

#include "DD_PackPop.h"

#endif


