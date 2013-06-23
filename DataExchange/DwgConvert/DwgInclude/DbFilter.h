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



#ifndef OD_DBFILTER_H
#define OD_DBFILTER_H

#include "DbObject.h"

class OdRxClass;

/** Description:
    The class is the base class for all OdDb Filter objects.

    Library: 
    Db

    Remarks:
    This class defines a query and supplies a key to the 
    OdDbCompositeFilteredBlockIterator object. The index corresponding
    to this key obtained through indexClass().

    See Also:
    o  OdDbIndexFilterManager
    o  OdDbIndex
    o  OdDbFilteredBlockIterator
    o  OdDbCompositeFilteredBlockIterator    
     
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbFilter : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbFilter);

  OdDbFilter();
  
  /** Description:
    Returns the class descriptor of the OdDbIndex for this Filter object.
    
    Remarks:
    This function is used by the init() method of the OdDbCompositeFilteredBlockIterator object.
  */
  virtual OdRxClass* indexClass() const = 0;

  OdResult dxfInFields(
    OdDbDxfFiler* pFiler);
  
  void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbFilter object pointers.
*/
typedef OdSmartPtr<OdDbFilter> OdDbFilterPtr;

#endif // OD_DBFILTER_H


