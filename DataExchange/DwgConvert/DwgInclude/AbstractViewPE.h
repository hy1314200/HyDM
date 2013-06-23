///////////////////////////////////////////////////////////////////////////////
// Copyright ?2002, Open Design Alliance Inc. ("Open Design") 
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
//      DWGdirect ?2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_ABSTRACTVIEWPE_H
#define OD_ABSTRACTVIEWPE_H


#include "Ge/GePoint3d.h"
#include "Ge/GeVector3d.h"
#include "ViewportDefs.h"
#include "IdArrays.h"

class OdGeBoundBlock3d;

/** Description:
    Protocol extention class that represents either view characteristics of
    an OdDbViewport, an OdDbViewportTableRecord, an OdDbViewTableRecord
    or an OdGsView.

    {group:Other_Classes} 
*/
class TOOLKIT_EXPORT OdAbstractViewPE : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdAbstractViewPE);

  // Viewport methods:

  virtual OdGePoint2d lowerLeftCorner(const OdRxObject* pVpFrom) const = 0;
  virtual OdGePoint2d upperRightCorner(const OdRxObject* pVpFrom) const = 0;
  virtual void setViewport(OdRxObject* pVpTo, const OdGePoint2d& lowerLeft, const OdGePoint2d& upperRight) const = 0;

  // View methods:

  virtual OdGePoint3d  target   (const OdRxObject* pVpFrom) const = 0;
  virtual OdGeVector3d direction(const OdRxObject* pVpFrom) const = 0;
  virtual OdGeVector3d upVector (const OdRxObject* pVpFrom) const = 0;
  virtual double fieldWidth (const OdRxObject* pVpFrom) const = 0;
  virtual double fieldHeight(const OdRxObject* pVpFrom) const = 0;
  virtual bool isPerspective(const OdRxObject* pVpFrom) const = 0;
  virtual OdGeVector2d viewOffset(const OdRxObject* pVpFrom) const = 0;

  virtual void setView(
      OdRxObject* pVpTo
    , const OdGePoint3d& target
    , const OdGeVector3d& direction
    , const OdGeVector3d& upVector
    , double fieldWidth
    , double fieldHeight
    , bool bPerspective
    , const OdGeVector2d& viewOffset = OdGeVector2d::kIdentity
    ) const = 0;

  virtual void setLensLength(OdRxObject* pVpTo, double dLensLength) const = 0;
  virtual double lensLength(const OdRxObject* pVpFrom) const = 0;

  virtual bool isFrontClipOn(const OdRxObject* pVpFrom) const = 0;
  virtual void setFrontClipOn(OdRxObject* pVpTo, bool bOn) const = 0;

  virtual bool isBackClipOn(const OdRxObject* pVpFrom) const = 0;
  virtual void setBackClipOn(OdRxObject* pVpTo, bool bOn) const = 0;

  virtual bool isFrontClipAtEyeOn(const OdRxObject* pVpFrom) const = 0;
  virtual void setFrontClipAtEyeOn(OdRxObject* pVpTo, bool bOn) const = 0;

  virtual double frontClipDistance(const OdRxObject* pVpFrom) const = 0;
  virtual void setFrontClipDistance(OdRxObject* pVpTo, double newVal) const = 0;

  virtual double backClipDistance(const OdRxObject* pVpFrom) const = 0;
  virtual void setBackClipDistance(OdRxObject* pVpTo, double newVal) const = 0;

  virtual void setRenderMode(OdRxObject* pVpTo, OdDb::RenderMode mode) const = 0;
  virtual OdDb::RenderMode renderMode(const OdRxObject* pVpFrom) const = 0;

  virtual void frozenLayers(const OdRxObject* pVp, OdDbObjectIdArray& ids) const;
  virtual void setFrozenLayers(OdRxObject* pVp, const OdDbObjectIdArray& ids) const;

  virtual void setView(OdRxObject* pViewTo, const OdRxObject* pViewFrom) const;

  // UCS methods:

  virtual bool hasUcs(const OdRxObject* pVpFrom) const = 0;

  virtual OdDb::OrthographicView orthoUcs(const OdRxObject* pVpFrom) const = 0;
  virtual bool setUcs(OdRxObject* pVpTo, OdDb::OrthographicView ucs) const = 0;

  virtual OdDbObjectId ucsName(const OdRxObject* pVpFrom) const = 0;
  virtual bool setUcs(OdRxObject* pVpTo, const OdDbObjectId& ucsId) const = 0;

  virtual void getUcs(const OdRxObject* pVpFrom, OdGePoint3d& origin, OdGeVector3d& xAxis, OdGeVector3d& yAxis) const = 0;
  virtual void setUcs(OdRxObject* pVpTo, const OdGePoint3d& origin, const OdGeVector3d& xAxis, const OdGeVector3d& yAxis) const = 0;

  virtual double elevation(const OdRxObject* pVpFrom) const = 0;
  virtual void setElevation(OdRxObject* pVpTo, double elev ) const = 0;

  virtual void setUcs(OdRxObject* pViewTo, const OdRxObject* pViewFrom) const;

  // Util methods:

  /** Description:
    Returns the eye coordinate system *extents* of the specified Viewport object.
    
    Arguments:
    pVp (I) Pointer to the Viewport object.
    extents (O) Receives the *extents*.

    Remarks:
    Returns true if and only if the *extents* are defined.
    
    Notes:
    pVp must specify one of the objects that supports this protocol extentsion class:
    
    o  OdGsView
    o  OdDbViewport
    o  OdDbAbstractViewTableRecord
    o  A derivitive of one of the above.     
  */
  virtual bool viewExtents(
    const OdRxObject* pVp, 
    OdGeBoundBlock3d& extents) const = 0;

  /** Description:
    Modifies the specified Viewport object to fit the specified eye coordinate system *extents*.
    
    Arguments:
    pVp (I) Pointer to the Viewport object.
    pExtents (I) Pointer to the *extents*.

    Remarks:
    Returns true if and only if the *extents* are defined.

    Remarks:
    If pViewExtents is NULL, this function calls viewExtents() to determines the *extents*.
        
    Notes:
    pVp must specify one of the objects that supports this protocol extentsion class:
    
    o  OdGsView
    o  OdDbViewport
    o  OdDbAbstractViewTableRecord
    o  A derivitive of one of the above.     
  */
  virtual bool zoomExtents(
    OdRxObject* pVp, 
    const OdGeBoundBlock3d* pExtents = 0) const;

  virtual OdGeMatrix3d worldToEye(OdRxObject* pVp) const;
  virtual OdGeMatrix3d eyeToWorld(OdRxObject* pView) const;
};

typedef OdSmartPtr<OdAbstractViewPE> OdAbstractViewPEPtr;


#endif // OD_ABSTRACTVIEWPE_H


