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



#ifndef _OD_GEOMETRYFROMPROXY_INCLUDED_
#define _OD_GEOMETRYFROMPROXY_INCLUDED_

#include "DD_PackPush.h"

#include "DbProxyEntity.h"
#include "ModelerGeometry.h"

/** Returns the SAT file associated with AcAdPart entities. 
    @param adPart (I) Entity pointer.
    @param sat (O) Receives the SAT file.
    @return True if adPart is an entity of type AcAdPart and a valid SAT file was
    returned in the sat parameter, otherwise false.
*/
TOOLKIT_EXPORT bool odGetSatFromProxy(const OdDbProxyEntityPtr& adPart, OdString& sat);

/** Returns the OdModelerGeometry object associated with AcAdPart entities. 
    @param adPart (I) Entity pointer.
    @param pModelerGeometry (O) Receives the OdModelerGeometry object.
    @return True if adPart is an entity of type AcAdPart and a valid OdModelerGeometry object was
    returned in the pModelerGeometry parameter, otherwise false.
*/
TOOLKIT_EXPORT bool odGetSatFromProxy(const OdDbProxyEntityPtr& adPart, OdModelerGeometryPtr& pModelerGeometry);

class OdGeNurbSurface;

/** Returns the OdGeNurbSurface associated with AcAsSurfBody entities. 
    @param asSurfBody (I) Entity pointer.
    @param surf (O) Receives the OdGeNurbSurface object.
    @return True if asSurfBody is an entity of type AcAsSurfBody and a valid OdGeNurbSurface object was
    returned in the surf parameter, otherwise false.
*/
TOOLKIT_EXPORT bool odGetSurfaceFromProxy(const OdDbProxyEntityPtr& asSurfBody, OdGeNurbSurface& surf);

#include "DD_PackPop.h"

#endif


