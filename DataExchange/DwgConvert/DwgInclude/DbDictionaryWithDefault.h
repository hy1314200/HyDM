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



#ifndef OD_DBDICTDFLT_H
#define OD_DBDICTDFLT_H

#include "DD_PackPush.h"

#include "DbDictionary.h"

/** Description:
    This class represents *database* -resident object dictionaries with 
    with default entries.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDictionaryWithDefault : public OdDbDictionary
{
public:
  ODDB_DECLARE_MEMBERS(OdDbDictionaryWithDefault);

  OdDbDictionaryWithDefault();
  
  /** Description:
      Sets the Object ID of the default object associated with this dictionary 
      (DXF 340).
  */
  void setDefaultId(
    const OdDbObjectId& newId);

  /** Description:
      Returns the Object ID of the default object associated with this dictionary 
      (DXF 340).
  */
  OdDbObjectId defaultId() const;
  
  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;

};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbDictionaryWithDefault.
*/
typedef OdSmartPtr<OdDbDictionaryWithDefault> OdDbDictionaryWithDefaultPtr;

#include "DD_PackPop.h"

#endif


