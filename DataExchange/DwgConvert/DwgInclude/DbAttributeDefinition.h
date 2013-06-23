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



#ifndef _OD_DB_ATTRDEF_
#define _OD_DB_ATTRDEF_

#include "DD_PackPush.h"

#include "DbText.h"

/** Description:
    This class represents attribute defintion (Attdef) entities in an OdDbDatabase instance.
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbAttributeDefinition : public OdDbText
{
public:

  ODDB_DECLARE_MEMBERS(OdDbAttributeDefinition);
  OdDbAttributeDefinition();

  /** Description:
    Returns the *prompt* string of this Attdef entity (DXF 3).
  */
  const OdString prompt() const;

  /** Description:
    Sets the *prompt* string of this Attdef entity (DXF 3).
    Arguments:
    promptString (I) Prompt string
  */
  void setPrompt(
    const char* promptString);

  /** Description:
    Returns the *tag* string of this Attdef entity (DXF 2).
  */
  const OdString tag() const;

  /** Description:
    Sets the *tag* string of this Attdef entity (DXF 2).
    Arguments:
    tagString (I) Tag string.  
  */
  void setTag(
    const OdChar* tagString);

  /** Description:
    Returns true if and only if this Attdef entity is *invisible* (DXF 70, bit 0x01).
  */
  bool isInvisible() const;

  /** Description:
    Controls the invisibility of this Attdef entity (DXF 70, bit 0x01).

    Arguments:
    invisible (I) Controls invisibility.
  */
  void setInvisible(
    bool invisible);

  /** Description:
    Returns true if and only if this Attdef entity is constant (DXF 70, bit 0x02).
  */
  bool isConstant() const;

  /** Description:
    Controls the *constant* status of this Attdef entity (DXF 70, bit 0x02).
    Arguments:
    constant (I) Controls *constant* status.
  */
  void setConstant(bool constant);

  /** Description:
    Returns true if and only if this Attdef entity is verifiable (DXF 70, bit 0x04).
  */
  bool isVerifiable() const;

  /** Description:
    Controls the *verifiable* status of this Attdef entity (DXF 70, bit 0x04).
    Arguments:
    verifiable (I) Controls verifiable status.
  */
  void setVerifiable(bool);

  /** Description:
    Returns true if and only if this Attdef entity is *preset* (DXF 70, bit 0x08).
  */
  bool isPreset() const;

  /** Description:
    Controls the *preset* status of this Attdef entity (DXF 70, bit 0x08).
    Arguments:
    preset (I) Controls *preset* status.
  */
  void setPreset(bool);

  /** Description:
    Returns the field length of this Attdef entity (DXF 73).
  */
  OdUInt16 fieldLength() const;

  /** Description:
    Sets the field length of this Attdef entity (DXF 73).
    Arguments:
    fieldLength (I) Field length.
  */
  void setFieldLength(
    OdUInt16 fieldLength);

  virtual void getClassID(
    void** pClsid) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;

  bool worldDraw(
    OdGiWorldDraw* pWd) const;

  void appendToOwner(
    OdDbIdPair& Idpair, 
    OdDbObject* pOwnerObject, 
    OdDbIdMapping& ownerIdMap);
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbAttributeDefinition object pointers.
*/
typedef OdSmartPtr<OdDbAttributeDefinition> OdDbAttributeDefinitionPtr;

#include "DD_PackPop.h"

#endif


