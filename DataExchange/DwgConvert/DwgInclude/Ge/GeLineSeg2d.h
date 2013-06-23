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
//   DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GE_LINE_SEG_2D_H
#define OD_GE_LINE_SEG_2D_H /* {Secret} */

#include "GeLinearEnt2d.h"

#include "DD_PackPush.h"

/**
    Description:
    This class represents line segments in 2D space.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeLineSeg2d : public OdGeLinearEnt2d
{
public:
  /**
    Arguments:
    line (I) Any 2D *line*.
    point (I) Any 2D *point*.
    point1 (I) Any 2D *point*.
    point2 (I) Any 2D *point*.
    vect (I) Any 2D vector
    source (I) Object to be cloned.

    Remarks:
    point and vect construct a *line* segment between points point and point + vect. vect cannot have a zero *length*.

    point1 and point2 construct a *line* segment between points point1 and point2. The 
    points cannot be coincident.

    If called with no arguments, constructs a *line* segment between the points (0,0) and (1,0).
  */
  OdGeLineSeg2d () {}
  OdGeLineSeg2d (
    const OdGeLineSeg2d& source);
  OdGeLineSeg2d (
    const OdGePoint2d& point1, 
    const OdGePoint2d& point2);
  OdGeLineSeg2d (
    const OdGePoint2d& point, 
    const OdGeVector2d& vect);

  virtual ~OdGeLineSeg2d () {}
  virtual OdGeEntity2d* copy () const  
  { return new OdGeLineSeg2d (*this); }

  OdGe::EntityId type () const     { return OdGe::kLineSeg2d;}
  /**
    Description:
    Sets the parameters for this *line* according to the arguments, and returns a reference to this *line*.

    Arguments:
    point (I) Any 2D *point*.
    point1 (I) Any 2D *point*.
    point2 (I) Any 2D *point*.
    vect (I) Any 2D vector
    curve (I) Any 2D *curve*.
    curve1 (I) Any 2D *curve* 
    curve2 (I) Any 2D *curve* 
    param (I) Point on curve1 where this *line* segment is tangent to curve 
    param1 (I) Point on curve1 where this *line* segment is tangent to curve1 
    param2 (I) Point on curve2 where this *line* segment is tangent to curve2 
    success (O) Receives true if and only if the tangent was constructed. If false, this *line* segment is unchanged.


    Remarks:
    point and vect construct a *line* segment between points point and point + vect. vect cannot have a zero *length*.

    point1 and point2 construct a *line* segment between points point1 and point2. The 
    points cannot be coincident.
    
    curve1 and curve2 construct a tangent *line* segment between curve1 and curve2. param1 and param2 are  
    the approximate tangent points on curve1 and curve2, respectively.
    
    curve and point construct a *line* segment starting at point, and tangent to *curve*. 
    param is the approximate tangent point on *curve*.
  */
  OdGeLineSeg2d& set (
    const OdGePoint2d& point, 
    const OdGeVector2d& vect);
  OdGeLineSeg2d& set (
    const OdGePoint2d& point1, 
    const OdGePoint2d& point2);
  OdGeLineSeg2d& set (
    const OdGeCurve2d& curve1, 
    const OdGeCurve2d& curve2,
    double& param1, 
    double& param2, 
    bool& success);
  OdGeLineSeg2d& set (
    const OdGeCurve2d& curve, 
    const OdGePoint2d& point, 
    double& param, 
    bool& success);

  /**
    Description:
    Returns the infinite perpendicular bisector of this *line* segment.
    
    Arguments:
    line (O) Receives the perpendicular bisector.
  */
  void getBisector (
    OdGeLine2d& line) const;
    
  /**
    Description:
    Returns the weighted average of the start point and end point of this *line* segment.
    
    Arguments:
    blendCoeff (I) Blend coefficient.
    
    Remarks:
    @table
    blendCoeff      Returns
    0               start point
    1               end point
    0 to 1          *point* on this *line* segment
    < 0 or > 1      *point* not on this *line* segment, but colinear with it.
  */
  OdGePoint2d baryComb (
    double blendCoeff) const;


  /**
    Description:
    Returns the start point of this *line* segment.
  */
  OdGePoint2d startPoint () const 
  { return m_point1; }
  
  /**
    Description:
    Returns the midpoint of this *line* segment.
  */
  OdGePoint2d midPoint () const;
  
  /**
    Description:
    Returns the end point of this *line* segment.
  */
  OdGePoint2d endPoint () const 
  { return m_point2; }
  /**
    Arguments:
    line (I) Any 2D *line*.
    tol (I) Geometric tolerance.
  */
  bool isEqualTo (
    const OdGeLineSeg2d& line, 
    const OdGeTol& tol = OdGeContext::gTol) const;
    
  bool operator == (
    const OdGeLineSeg2d& entity) const;
  bool operator != (
    const OdGeLineSeg2d& entity) const;

  /**
    Description:
    Returns the *length* of this *line* over the specified parameter range.

    Arguments:
    fromParam (I) Starting parameter value.
    toParam (I) Ending parameter value.
    tol (I) Geometric tolerance.
  */      
  double length () const;
  double length (
    double fromParam, 
    double toParam, 
    double tol = OdGeContext::gTol.equalPoint ()) const;

  //virtual bool overlap (const OdGeLinearEnt2d& line, OdGeLinearEnt2d*& overlap,
  //           const OdGeTol& tol = OdGeContext::gTol) const;

  virtual OdGeEntity2d& transformBy (
    const OdGeMatrix2d& xfm);
    
  virtual void getInterval (
    OdGeInterval& interval) const;
    
  virtual bool isOn (
    const OdGePoint2d& point, 
    const OdGeTol& tol = OdGeContext::gTol) const;

  bool hasStartPoint (
    OdGePoint2d& startPoint) const;
  bool hasEndPoint (
    OdGePoint2d& endPoint) const;
  bool isClosed (
    const OdGeTol& tol = OdGeContext::gTol) const;

};

#include "DD_PackPop.h"

#endif // OD_GE_LINE_SEG_2D_H


