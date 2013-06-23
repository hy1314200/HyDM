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



#ifndef OD_DBVBAPROJECT_H
#define OD_DBVBAPROJECT_H

#include "DD_PackPush.h"

#include "DbObject.h"

/** Description:
    Represents a VBA Project object in an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbVbaProject : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbVbaProject);

  OdDbVbaProject();

  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;

  /** Description:
      Return VBA project data for this object.

      Arguments:
      data Array to which project data is written.
  */
  void getVbaProject(OdBinaryData& data) const;

  /** Description:
      Return VBA project data for this object.

      Arguments:
      data Array to which project data is written.
  */
  void setVbaProject(const OdBinaryData& data);
};

typedef OdSmartPtr<OdDbVbaProject> OdDbVbaProjectPtr;

#include "DD_PackPop.h"

#endif //OD_DBVBAPROJECT_H


