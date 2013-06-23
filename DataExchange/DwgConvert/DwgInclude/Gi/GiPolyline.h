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



#ifndef __ODGIPOLYLINE_H__
#define __ODGIPOLYLINE_H__

class OdGeVector3d;
class OdGeLineSeg2d;
class OdGeLineSeg3d;
class OdGeCircArc2d;
class OdGeCircArc3d;

#include "DD_PackPush.h"

/** Description:
    Inteface defining the Gi equivalent of an OdDbPolyline or "lightweight" polyline. 
    Instances of this class are used to pass lightweight polyline data through the 
    DWGdirect vectorization framework.

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiPolyline : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiPolyline);

  enum SegType
  {
    kLine,        // Straight segment with length greater than 0.
    kArc,         // Arc segment with length greater than 0.
    kCoincident,  // Segment with 0 length.
    kPoint,       // Polyline with a single vertex.
    kEmpty        // No vertices are present.
  };

  /** Description:
      Returns the number of vertices in the entity.
  */
  virtual unsigned int numVerts() const = 0;

  /** Description:
      Returns true if this polyline is closed, false otherwise. 
  */
  virtual bool isClosed() const = 0;

  /** Description:
      Returns the normal vector for this entity.
  */
  virtual OdGeVector3d normal() const = 0;

  /** Description:
      Returns the thickness for this entity.
  */
  virtual double thickness() const = 0;

  /** Description:
      Returns the constant width for this entity.  
  */
  virtual double getConstantWidth() const = 0;

  /** Description:
      Returns true if the vertices in this entity contain start and end width data, false otherwise.
  */
  virtual bool hasWidth() const = 0;

  /** Description:
      Returns the PLINEGEN flag for this entity. If PLINEGEN is true,
      linetype generation will be patterned across the entire polyline, rather than 
      being done for each segment individually.
  */
  virtual bool hasPlinegen() const = 0;

  /** Description:
      Returns the elevation for this entity.
  */
  virtual double elevation() const = 0;

  /** Description:
      Returns the segment type at the specified index.

      See Also:
      SegType
  */
  virtual SegType segType(unsigned int index) const = 0;
  
  /** Description:
  */
  virtual void getLineSegAt(unsigned int index, OdGeLineSeg2d& ln) const = 0;

  /** Description:
      Returns the line segment starting at the specified index.
  */
  virtual void getLineSegAt(unsigned int index, OdGeLineSeg3d& ln) const = 0;

  /** Description:
      Returns the arc segment starting at the specified index.
  */
  virtual void getArcSegAt(unsigned int index, OdGeCircArc2d& arc) const = 0;

  virtual void getArcSegAt(unsigned int index, OdGeCircArc3d& arc) const = 0;

  /** Description:
      Returns a specified point from this entity.
  */
  virtual void getPointAt(unsigned int index, OdGePoint2d& pt) const = 0;

  /** Description:
      Returns the bulge value for a specified point.
  */
  virtual double getBulgeAt(unsigned int index) const = 0;

  /** Description:
      Returns the start and end widths for a specified point.
  */
  virtual void getWidthsAt(unsigned int index, double& startWidth,  double& endWidth) const = 0;

  /** Description:
      Returns a pointer to the original OdDbPolyline object from which this OdGiPolyline was 
      created.
  */
  virtual OdRxObjectPtr getDbPolyline() const = 0;
};

typedef OdSmartPtr<OdGiPolyline> OdGiPolylinePtr;

#include "DD_PackPop.h"

#endif  // __ODGIPOLYLINE_H__


