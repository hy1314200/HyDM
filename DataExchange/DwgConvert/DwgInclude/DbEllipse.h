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
// programs incorporating this software must include the following statement 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef GEOMENT_DBELIPSE_H
#define GEOMENT_DBELIPSE_H /* {Secret} */

#include "DD_PackPush.h"

#include "DbCurve.h"

/** Description:
    This class represents Ellipse entities in an OdDbDatabase instance.

    Library:
    Db
    Remarks:
    All angles are expressed in radians.
    
    A closed Ellipse entity will have a startAngle of 0 and an endAngle of Oda2PI.

    (endAngle - startAngle) > 1e-6.

    The radiusRatio must be in the range [1e-6 to 1.0].

    The majorAxis must perpendicular to the *normal* within 1e-6.
    
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbEllipse : public OdDbCurve
{
public:
  ODDB_DECLARE_MEMBERS(OdDbEllipse);

 OdDbEllipse();

  /** Description:
    Returns the WCS *center* of this curve (DXF 10 in WCS).
  */
  OdGePoint3d center() const;

  /** Description:
    Sets the WCS *center* of this curve (DXF 10 in WCS).
    Arguments:
    center (I) Center.
  */
  void setCenter(
    const OdGePoint3d& center);

  /** Description:
    Returns the WCS *normal* to the plane of this entity (DXF 210).
  */
  OdGeVector3d normal() const;

  /** Description:
     Returns the WCS major axis vector for this Ellipse entity.
  */
  OdGeVector3d majorAxis() const;

  /** Description:
    Returns the WCS minor axis vector for this Ellipse entity.
  */
  OdGeVector3d minorAxis() const;

  /** Description:
    Returns the ratio of the minor radius to the major radius of this Ellipse entity.
  */
  double radiusRatio() const;

  /** Description:
    Sets the ratio of the minor radius to the major radius of this Ellipse entity.
    Arguments:
    radiusRatio (I) Radius ratio.
    Note:
    All angles are expressed in radians.

    The radiusRatio must be in the range [1e-6 to 1.0].

  */
  void setRadiusRatio(
    double radiusRatio);

  /** Description:
    Returns the start *angle* of this curve (DXF 50).

    Remarks:
    The *angle* is measured counterclockwise from the major axis.

    Note:
    All angles are expressed in radians.
  */
  double startAngle() const;

  /** Description:
    Sets the start *angle* of this curve (DXF 50).
    
    Arguments:
    startAngle (I) Start *angle*.
    
    Remarks:
    The *angle* is measured counterclockwise from the major axis.

    Note:
    All angles are expressed in radians.

    A closed Ellipse entity will have a startAngle of 0 and an endAngle of Oda2PI.

    (endAngle - startAngle) > 1e-6.

  */
  void setStartAngle(
    double startAngle);

  /** Description:
    Returns the end *angle* of this curve (DXF 51).

    Remarks:
    The *angle* is measured counterclockwise from the major axis.

    Note:
    All angles are expressed in radians.
  */
  double endAngle() const;

  /** Description:
    Sets the end *angle* of this curve (DXF 51).
    Arguments:
    endAngle (I) End *angle*.

    Remarks:
    The *angle* is measured counterclockwise from the major axis.

    Note:
    All angles are expressed in radians.

    A closed Ellipse entity will have a startAngle of 0 and an endAngle of Oda2PI.

    (endAngle - startAngle) > 1e-6.

  */
  void setEndAngle(
    double endAngle);

  /** Description:
    Sets the start parameter for this curve (DXF 41).
    Arguments:
    startParam (I) Start parameter.
  */
  void setStartParam(
    double startParam);

  /** Description:
    Sets the end parameter for this curve (DXF 42).
    Arguments:
    endParam (I) End parameter.
  */
  void setEndParam(
    double endParam);

  /** Description:
    Returns the parameter value corresponding to the specified *angle*.
    Arguments:
    angle (I) Angle to be queried.
    Remarks:
    Can be used to retrieve the DXF 41 & 42 values.
    Note:
    All angles are expressed in radians.
  */
  double paramAtAngle(
    double angle) const;

  /** Description:
    Returns the *angle* corresponding to the specified parameter value.
    Arguments:
    param (I) Parameter value to be queried.
    Note:
    All angles are expressed in radians.
 */
  double angleAtParam(
    double param) const;

  /** Description:
    Returns the properties for this Ellipse entity.

    Arguments:
    center (O) Receives the WCS *center*.
    unitNormal (O) Receives the WCS unit *normal*.
    majorAxis (O) Receives the WCS major axis.
    radiusRatio (O) Receives the radius ratio.
    startAngle (O) Receives the start *angle*.
    endAngle (O) Receives the end *angle*.
    Note:
    All angles are expressed in radians.
  */
  void get(OdGePoint3d& center,
    OdGeVector3d& unitNormal,
    OdGeVector3d& majorAxis,
    double& radiusRatio,
    double& startAngle,
    double& endAngle) const;

  /** Description:
    Sets the properties for this Ellipse entity.

    Arguments:
    center (I) WCS *center*.
    unitNormal (I) WCS unit *normal*.
    majorAxis (I) WCS major axis.
    radiusRatio (I) Radius ratio.
    startAngle (I) Start *angle*.
    endAngle (I) End *angle*.    

    Note:
    All angles are expressed in radians.
    
    A closed Ellipse entity will have a startAngle of 0 and an endAngle of Oda2PI.

    (endAngle - startAngle) > 1e-6.

    The radiusRatio must be in the range [1e-6 to 1.0].

    The majorAxis must perpendicular to the *normal* within 1e-6.
    
  */
  void set(const OdGePoint3d& center,
    const OdGeVector3d& unitNormal,
    const OdGeVector3d& majorAxis,
    double radiusRatio,
    double startAngle = 0.0,
    double endAngle = Oda2PI);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual bool worldDraw(
    OdGiWorldDraw* pWd) const;

  virtual void getClassID(
    void** pClsid) const;

  virtual bool isPlanar() const;

  virtual OdResult getPlane(
    OdGePlane& plane, 
    OdDb::Planarity& planarity) const;

  virtual bool isClosed() const;

  virtual bool isPeriodic() const;

  virtual OdResult getStartParam(
    double& startParam) const;

  virtual OdResult getEndParam (
    double& endParam) const;

  virtual OdResult getStartPoint(
    OdGePoint3d& startPoint) const;

  virtual OdResult getEndPoint(
    OdGePoint3d& endPoint) const;

  virtual OdResult getPointAtParam(
    double param, 
    OdGePoint3d& pointOnCurve) const;

  virtual OdResult getParamAtPoint(
    const OdGePoint3d& pointOnCurve, 
    double& param) const;


  virtual OdResult transformBy(
    const OdGeMatrix3d& xfm);
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbEllipse object pointers.
*/
typedef OdSmartPtr<OdDbEllipse> OdDbEllipsePtr;

#include "DD_PackPop.h"

#endif


