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



#ifndef __ODGICONVEYORNODE_H__
#define __ODGICONVEYORNODE_H__


#include "GiConveyorGeometry.h"
#include "GiExport.h"

#include "DD_PackPush.h"

class OdGiConveyorOutput;

/** Description:

    {group:OdGi_Classes} 
*/
class OdGiConveyorInput
{
public:
  virtual void addSourceNode(OdGiConveyorOutput& sourceNode) = 0;
  virtual void removeSourceNode(OdGiConveyorOutput& sourceNode) = 0;
};

/** Description:

    {group:OdGi_Classes} 
*/
class OdGiConveyorOutput
{
public:
  virtual void setDestGeometry(OdGiConveyorGeometry& destGeometry) = 0;
  virtual OdGiConveyorGeometry& destGeometry() const = 0;
};

/** Description:

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiConveyorNode : public OdRxObject
{
protected:
  OdGiConveyorNode();
public:
  ODRX_DECLARE_MEMBERS(OdGiConveyorNode);

  virtual OdGiConveyorInput& input() = 0;
  virtual OdGiConveyorOutput& output() = 0;
};

typedef OdSmartPtr<OdGiConveyorNode> OdGiConveyorNodePtr;

#include "DD_PackPop.h"

#endif //#ifndef __ODGICONVEYORNODE_H__

