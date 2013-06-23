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


#ifndef _ODDBGPIPPOINTS_INCLUDED_
#define _ODDBGPIPPOINTS_INCLUDED_

#include "RxObject.h"
#include "RxModule.h"
#include "IntArray.h"
#include "DbEntity.h"
#include "Gi/GiDrawable.h"

/** Description:

    Library:
    Db

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbGripPointsPE : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbGripPointsPE);
  
  virtual OdResult getGripPoints( const OdDbEntity* ent, OdGePoint3dArray& gripPoints ) const = 0;
  virtual OdResult moveGripPointsAt( OdDbEntity* ent, const OdGePoint3dArray& gripPoints, const OdIntArray& indices ) = 0;

  virtual OdResult getStretchPoints( const OdDbEntity* ent, OdGePoint3dArray& stretchPoints ) const = 0;
  virtual OdResult moveStretchPointsAt( OdDbEntity* ent, const OdGePoint3dArray& stretchPoints, const OdIntArray& indices ) = 0;

  virtual OdResult getOsnapPoints( 
    const OdDbEntity* ent, 
    OdDb::OsnapMode osnapMode, 
    int gsSelectionMark, 
    const OdGePoint3d& pickPoint,
    const OdGePoint3d& lastPoint, 
    const OdGeMatrix3d& viewXform, 
    const OdGeMatrix3d& ucs,
    OdGePoint3dArray& snapPoints ) const = 0;
};
typedef OdSmartPtr<OdDbGripPointsPE> OdDbGripPointsPEPtr;

/** Description:
  {group:Structs}
*/      
struct TOOLKIT_EXPORT OdDbGripPointManager : OdGiDrawable
{
  virtual void addEntity( const OdDbObjectId& id ) = 0;
  virtual bool onMouseMove( const OdGePoint2d& point, const OdGeMatrix3d& view ) = 0;
  virtual OdDbObjectId getActiveEntity() const = 0;
  virtual void onStartDrag() = 0;
  virtual void onDrag( const OdGeVector3d& offset ) = 0;
  // final point is passed in case of snapping
  virtual void onEndDrag( OdGePoint3d* finalPoint = 0 ) = 0;
  virtual void transformBy( const OdGeMatrix3d& xfm ) = 0;
};

typedef OdSmartPtr<OdDbGripPointManager> OdDbGripPointManagerPtr;

/** Description:
  {group:Structs}
*/      
struct TOOLKIT_EXPORT OdDbSnapPointManager : OdGiDrawable
{
  virtual void init( unsigned snapModes, int snapRectSizeDC = 5 ) = 0;
  virtual void startDrag( const OdDbObjectId& id, const OdGeMatrix3d& ucs, const OdGePoint3d* p = 0 ) = 0;
  virtual void onDrag( const OdDbObjectIdArray& ids, const OdGeMatrix3d& view, const OdGePoint3d& point ) = 0;
  virtual bool endDrag( OdGePoint3d& finalPoint ) = 0;
};

typedef OdSmartPtr<OdDbSnapPointManager> OdDbSnapPointManagerPtr;

#endif //_ODDBGPIPPOINTS_INCLUDED_
