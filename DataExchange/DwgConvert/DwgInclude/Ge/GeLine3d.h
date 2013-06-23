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



#ifndef OD_GELINE3D_H
#define OD_GELINE3D_H /* {Secret} */

class OdGeLine2d;

#include "GeLinearEnt3d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents infinite lines in 3D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeLine3d : public OdGeLinearEnt3d
{
public:
  virtual ~OdGeLine3d () {}

  /**
    Arguments:
    line (I) Any 3D *line*.
    point (I) Any 3D *point*.
    point1 (I) Any 3D *point*.
    point2 (I) Any 3D *point*.
    source (I) Object to be cloned.
    vect (I) Any 3D vector.
    
    Remarks:
    point and vect construct an infinite *line* passing through point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct an infinite *line* passing through both points. The 
    points cannnot be coincident.

    If called with no arguments, constructs in infinite *line* coincident with the X-axis.
  */
  OdGeLine3d () {}
  OdGeLine3d (
    const OdGeLine3d& source);
  OdGeLine3d (
    const OdGePoint3d& point, 
    const OdGeVector3d& vect)
  { set (point, vect);}
  OdGeLine3d (
    const OdGePoint3d& point1, 
    const OdGePoint3d& point2)
  { set (point1, point2);}


  virtual OdGeEntity3d* copy () const;

  static const OdGeLine3d kXAxis; // X-axis *line*.
  static const OdGeLine3d kYAxis; // Y-axis *line*.
  static const OdGeLine3d kZAxis; // Z-axis *line*.

  virtual OdGe::EntityId type () const 
  { return OdGe::kLine3d;};

  /**
    Description:
    Sets the parameters for this *line* according to the arguments, and returns a reference to this *line*.

    Arguments:
    point (I) Any 3D *point*.
    vect (I) Any 3D vector.
    point1 (I) Any 3D *point*.
    point2 (I) Any 3D *point*.

    Remarks:
    point and vect construct an infinite *line* passing through point with 
    a *direction* of vect. vect cannot have a zero *length*.

    point1 and point2 construct an infinite *line* passing through both points. The 
    points should no be coincident.
  */
  OdGeLine3d& set (
    const OdGePoint3d& point, 
    const OdGeVector3d& vect);
  OdGeLine3d& set (
    const OdGePoint3d& point1, 
    const OdGePoint3d& point2)
  { return set (point1, point2 - point1);}

  // there is no such method!!
  // OdGeVector3d direction () const {return m_Direction.normal ();}

  OdGeLine3d& operator = (
    const OdGeLine3d& line);

  virtual bool isOn (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGePoint3d evalPoint (
    double param) const;
  virtual OdGePoint3d evalPoint (
    double param, 
    int numDeriv, 
    OdGeVector3dArray& derivatives) const;
    
  virtual double paramOf (
    const OdGePoint3d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;
  virtual void getInterval (
    OdGeInterval& interval) const;

  virtual void appendSamplePoints (
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
protected:
  // OdGeVector3d  m_Direction;
};

#include "DD_PackPop.h"

#endif // OD_GELINE3D_H

