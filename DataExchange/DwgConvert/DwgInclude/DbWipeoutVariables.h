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



#ifndef OD_DBWIPEOUTVARIABLES_H
#define OD_DBWIPEOUTVARIABLES_H

#include "DD_PackPush.h"

#include "DbObject.h"


class OdDbWipeoutVariables;
typedef OdSmartPtr<OdDbWipeoutVariables> OdDbWipeoutVariablesPtr;

/** Description:
    Represents a wipeout variables object in an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbWipeoutVariables : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbWipeoutVariables);

  OdDbWipeoutVariables();

  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;

  /** Description:
      Returns the wipeout frame display flag (DXF 70).
  */
  bool showFrame() const;

  /** Description:
      Sets the wipeout frame display flag (DXF 70).
  */
  void setShowFrame(bool value);

  /** Description:
      Opens the wipeout variables object in the the specified *database*, creating
      a wipeout variables object if one is not present.

      Arguments:
      mode (I) Mode in which to open the wipeout variables object.
      pDatabase (I) Database that contains the wipeout variables object.

      Return Value:
      Smart pointer to the wipeout variables object.
  */
  static OdDbWipeoutVariablesPtr openWipeoutVariables(OdDbDatabase* pDatabase,
    OdDb::OpenMode mode = OdDb::kForRead);
};

#include "DD_PackPop.h"

#endif //OD_DBWIPEOUTVARIABLES_H


