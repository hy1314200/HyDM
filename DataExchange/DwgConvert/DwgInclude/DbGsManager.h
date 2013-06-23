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



#ifndef __DBGSMANAGER_H_INCLUDED_
#define __DBGSMANAGER_H_INCLUDED_

class OdDbObjectId;
class OdGiContextForDbDatabase;

#include "Gs/Gs.h"


#include "DD_PackPush.h"

/** Description:

    {group:OdGs_Classes} 
*/
class TOOLKIT_EXPORT OdGsLayoutHelper : public OdGsDevice
{
public:
  ODRX_DECLARE_MEMBERS(OdGsLayoutHelper);

  virtual OdDbObjectId layoutId() const = 0;
  virtual OdGsViewPtr activeView() const = 0;
  virtual void makeViewActive(OdGsView* pView) = 0;
  virtual OdGsModel* gsModel() = 0;

  /** Description:
      Returns pointer to underlaying device object.
  */
  virtual OdGsDevicePtr underlayingDevice() const = 0;
};

typedef OdSmartPtr<OdGsLayoutHelper> OdGsLayoutHelperPtr;


/** Description:

    {group:OdGs_Classes} 
*/
class TOOLKIT_EXPORT OdGsPaperLayoutHelper : public OdGsLayoutHelper
{
public:
  ODRX_DECLARE_MEMBERS(OdGsPaperLayoutHelper);

  virtual OdGsViewPtr overallView() const = 0;
  virtual void makeViewOverall(OdGsView* pView) = 0;
};

typedef OdSmartPtr<OdGsPaperLayoutHelper> OdGsPaperLayoutHelperPtr;


/** Description:

    {group:OdGs_Classes} 
*/
class TOOLKIT_EXPORT OdGsModelLayoutHelper : public OdGsLayoutHelper
{
public:
  ODRX_DECLARE_MEMBERS(OdGsModelLayoutHelper);
};

typedef OdSmartPtr<OdGsModelLayoutHelper> OdGsModelLayoutHelperPtr;


/** Description:

    {group:DD_Namespaces}
*/
namespace OdDbGsManager
{
  /** Description:
      Populates OdGsDevice-derived object with OdGsViews-derived objects.
      Returns OdGsDevice-derived wrapper that handles some OdGsDevice's calls.
  */
  TOOLKIT_EXPORT OdGsLayoutHelperPtr setupActiveLayoutViews(OdGsDevice* pDevice,
    OdGiContextForDbDatabase* pGiCtx);

  /** Description:
      Populates OdGsDevice-derived object with OdGsViews-derived objects.
      Returns OdGsDevice-derived wrapper that handles some OdGsDevice's calls.
  */
  TOOLKIT_EXPORT OdGsLayoutHelperPtr setupLayoutViews(OdDbObjectId layoutId, OdGsDevice* pDevice, 
    OdGiContextForDbDatabase* pGiCtx);
}

#include "DD_PackPop.h"

#endif // __DBGSMANAGER_H_INCLUDED_


