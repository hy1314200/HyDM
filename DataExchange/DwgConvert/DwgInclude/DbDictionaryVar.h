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



#ifndef OD_DBDICTIONARYVAR_H
#define OD_DBDICTIONARYVAR_H

#include "DD_PackPush.h"

#include "DbObject.h"

/** Description:
    Represents a dictionary variable object in an OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDictionaryVar : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbDictionaryVar);

  /** Description:
      Constructor (no arguments).
  */
  OdDbDictionaryVar();

  /** Description:
      Returns the object schema number (DXF 280).
  */
  OdInt16 getSchema() const;

  /** Description:
      Sets the object schema number (DXF 280).
  */
  void setSchema(OdInt16 Schema);

  /** Description:
      Returns the value of this dictionary variable (DXF 1).
  */
  OdString value() const;
  
  /** Description:
      Returns the value of this dictionary variable as a string.

      Arguments:
      val (O) String which receives the value of this variable.

      Return Value:
      Always true.
  */
  bool valueAs(OdString& val) const { val = value(); return true; }

  /** Description:
      Returns the value of this dictionary variable as an integer.

      Arguments:
      val (O) Integer which receives the value of this variable.

      Return Value:
      true if this variable can be converted to an integer, false otherwise.
  */
  bool valueAs(int& val) const;

  /** Description:
      Returns the value of this dictionary variable as a bool.

      Arguments:
      val (O) Boolean which receives the value of this variable.

      Return Value:
      true if this variable can be converted to a boolean, false otherwise.
  */
  bool valueAs(bool& val) const;

  /** Description:
      Sets the value of this dictionary variable (DXF 1).
  */
  void setValue(const OdString& Value);
  void setValue(int);
  void setValue(bool);

  virtual OdResult dxfIn(OdDbDxfFiler* pFiler);

  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;

};
 
typedef OdSmartPtr<OdDbDictionaryVar> OdDbDictionaryVarPtr;

#include "DD_PackPop.h"

#endif  // OD_DBDICTIONARYVAR_H


