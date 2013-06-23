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



#ifndef OD_DBOLE_H
#define OD_DBOLE_H

#include "DD_PackPush.h"

#include "DbFrame.h"

/** Description:
    This class is the abstract base class for OdDbOle2Frame graphical entities 
    contained in an OdDbDatabase instance.
    
    Library:
    Db
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbOleFrame : public OdDbFrame
{
public:
  ODDB_DECLARE_MEMBERS(OdDbOleFrame);

  OdDbOleFrame();
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbOleFrame object pointers.
*/
typedef OdSmartPtr<OdDbOleFrame> OdDbOleFramePtr;

#include "DD_PackPop.h"

#endif // OD_DBOLE_H_


