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



#ifndef OD_GEPONC3D_H
#define OD_GEPONC3D_H /* {Secret} */


#include "GePointEnt3d.h"

class OdGeCurve3d;

#include "DD_PackPush.h"
/**
    Description:
    This class represents points along 3D curves.

    Library: Ge

    {group:OdGe_Classes} 

    Remarks:
    The *point* representation includes its *parameter* value, its coordinates, and the
    the *derivatives* of the *curve* at that *point*.
*/
class OdGePointOnCurve3d : public OdGePointEnt3d
{
public:
  /**
    Arguments:
    curve3d (I) Any 3D *curve*.
    param (I) *Parameter* to specify a *point* on curve3d.

    Remarks:
    The default constructor constructs a *point* on the *curve* OdGeLine2d::kXAxis with a *parameter* value of 0
  */
  OdGePointOnCurve3d ();
  OdGePointOnCurve3d (
    const OdGeCurve3d& curve3d, 
    double param = 0.0);

  /** Description:
    Returns a pointer to the *curve* on which the *point* lies.
  */
  const OdGeCurve3d* curve () const;

  /**
    Description:
    Returns the *parameter* value on the *curve* corresponding to the *point*.
  */
  double parameter () const;

  /**
    Description:
    Returns the *point* on the *curve*.

    Arguments:
    param (I) Sets the current *parameter*.
    curve3d (I) Any 3D *curve*. Sets the current *curve*.
    */
  OdGePoint3d point () const;
  OdGePoint3d point (
    double param);
  OdGePoint3d point (
    const OdGeCurve3d& curve3d, 
    double param);

  /**
    Description:
    Returns the derivitive of the *curve* at the *point* on the *curve*.

    Arguments:
    order (I) the *order* of the derivitive [1-2].
    param (I) Sets the current *parameter*.
    curve3d (I) Any 3D *curve*. Sets the current *curve*.
  */
  OdGeVector3d deriv (
    int order) const;
  OdGeVector3d deriv (
    int order, 
    double param);
  OdGeVector3d deriv (
    int order, 
    const OdGeCurve3d& curve3d, 
    double param);

  /**
    Description:
    Returns true if and only if the first derivative *vector* 
    has a length of zero.

    Arguments:
    tol (I) Geometric tolerance.
  */
  bool isSingular (
    const OdGeTol& tol = OdGeContext::gTol) const;

  /**
    Description:
    Returns true if and only if the first derivative *vector* has a non-zero length, and
    the *curvature* of the *curve* at the *point* on the *curve*. 

    Arguments:
    param (I) *Parameter* to specify a *point* on curve3d.
    res (O) Receives the *curvature* of the *curve* at the specified *point*.
  */
  bool curvature (
    double& res);
  bool curvature (
    double param, 
    double& res);

  /**
    Description:
    Sets the current *curve*.

    Arguments:
    curve3d (I) Any 3D *curve*.
  */
  OdGePointOnCurve3d& setCurve (
    const OdGeCurve3d& curve3d);

  /**
    Description:
    Sets the current *parameter*.

    Arguments:
    param (I) Sets the current *parameter*.
  */
  OdGePointOnCurve3d& setParameter (
    double param);

  OdGe::EntityId type () const;

  //  OdGePoint3d point3d () const;

  operator OdGePoint3d () const;

private:
  const OdGeCurve3d* m_pCurve;
  double m_param;
};

inline OdGePointOnCurve3d::OdGePointOnCurve3d ()
: m_pCurve (0)
, m_param (0.0)
{
}

inline const OdGeCurve3d* OdGePointOnCurve3d::curve () const
{
  return m_pCurve;
}

inline OdGePointOnCurve3d& OdGePointOnCurve3d::setCurve (
    const OdGeCurve3d& curve3d)
{
  m_pCurve = &curve3d;
  return *this;
}

inline OdGePointOnCurve3d& OdGePointOnCurve3d::setParameter (
    double param)
{
  m_param = param;
  return *this;
}

inline double OdGePointOnCurve3d::parameter () const
{
  return m_param;
}

inline OdGePoint3d OdGePointOnCurve3d::point () const
{
  if (!m_pCurve)
    throw OdError (eNotInitializedYet);
  return m_pCurve->evalPoint (m_param);
}

#include "DD_PackPop.h"

#endif // OD_GEPONC3D_H

