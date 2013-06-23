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



#ifndef _OD_DB_ATTRIBUTE_
#define _OD_DB_ATTRIBUTE_

#include "DD_PackPush.h"

#include "DbText.h"

class OdDbAttributeDefinition;
/** Description:
    This class represents attribute (Attrib) entities in an OdDbDatabase instance.
  
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbAttribute : public OdDbText
{
public:
  
  ODDB_DECLARE_MEMBERS(OdDbAttribute);
  OdDbAttribute();

  /** Description:
    Returns the *tag* string of this Attrib entity (DXF 2).
  */
  const OdString tag() const;

  /** Description:
    Sets the *tag* string of this Attrib entity (DXF 2).
    Arguments:
    tagString (I) Tag string.  
  */
  void setTag(
    const OdChar* tagString);

  /** Description:
    Returns true if and only if this Attrib entity is *invisible* (DXF 70, bit 0x01).
  */
  bool isInvisible() const;

  /** Description:
    Controls the invisibility of this Attrib entity (DXF 70, bit 0x01).

    Arguments:
    invisible (I) Controls invisibility.
  */
  void setInvisible(
    bool invisible);

  /** Description:
    Returns true if and only if this Attrib entity is constant (DXF 70, bit 0x02).
  */
  bool isConstant() const;

  /** Description:
    Returns true if and only if this Attrib entity is verifiable (DXF 70, bit 0x04).
  */
  bool isVerifiable() const;

  /** Description:
    Returns true if and only if this Attrib entity is preset (DXF 70, bit 0x08).
  */
  bool isPreset() const;

  /** Description:
    Returns the field length of this Attrib entity (DXF 73).
  */
  OdUInt16 fieldLength() const;

  /** Description:
    Sets the field length of this Attrib entity (DXF 73).
    Arguments:
    fieldLength (I) Field length.
  */
  void setFieldLength(
    OdUInt16 fieldLength);

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

  virtual void getClassID(
    void** pClsid) const;

  OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;

  /** Description:
    Applies the settings from the specified attribute definition,
    then applies the specified block transformation matrix to
    to this Attrib entity.
    
    Arguments:
    pAttDef (I) Pointer to the attribute definition entity.
    blkXform (I) Block ransformation matrix.
    
    Remarks:
    Equivalent to the ATTREDEF and INSERT commands.
    
    blkXform is the transformation matrix returned by the OdDbBlockReference::blockTransform() function
    of the OdDbBlockReference that will own this attribute.
    
  */
  void setAttributeFromBlock(
    const OdGeMatrix3d& blkXform);
  
  void setAttributeFromBlock(
    const OdDbAttributeDefinition* pAttDef, 
    const OdGeMatrix3d& blkXform);

};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbAttribute object pointers.
*/
typedef OdSmartPtr<OdDbAttribute> OdDbAttributePtr;

#include "DD_PackPop.h"

#endif


