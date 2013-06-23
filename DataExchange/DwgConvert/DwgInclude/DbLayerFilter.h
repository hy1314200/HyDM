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



#ifndef OD_DBLYFILT_H
#define OD_DBLYFILT_H

#include "DD_PackPush.h"

#include "DbFilter.h"

/** Description:
    This class implements LayerFilter objects in an OdDbDatabase instance.
    
    Library:
    Db

    {group:OdDb_Classes}
    
    Remarks:
    A LayerFilter is a set of layers whose corresponding entities are  
    traversed during filtered block traversal. 
    
    LayerFilter objects objects may optionally be saved in the drawing database 
    for efficient xref demand loading.
*/
class TOOLKIT_EXPORT OdDbLayerFilter : public  OdDbFilter
{
public:
  ODDB_DECLARE_MEMBERS(OdDbLayerFilter);

  OdDbLayerFilter();

  virtual OdRxClass* indexClass() const;

  /** Description:
    Returns true if and only if all the layer names in this LayerFilter object
    are present in the the specified OdDbDatabase.
    
    Arguments:
    pDb (I) Pointer to the *database* to be queried.
      
    Remarks:
    If pDb is NULL, the *database* containing this object is used.
  */
  virtual bool isValid(
    OdDbDatabase* pDb = NULL) const;

  /** Description:
    Adds the specified layer name to this LayerFilter object.
    
    Arguments:
    layerName (I) Layer name to be added. 
  */
  void add(
    const OdString& layerName);

  /** Description:
    Removes the specified layer name from this LayerFilter object. 

    Arguments:
    layerName (I) Layer name to be removed. 
  */
  void remove(
    const OdString& layerName);

  /** Description:
    Returns the layer name associated with the specified *index* in this LayerFilter object.
    
    Arguments:
    index (I) Index to be queried. 
  */
  OdString getAt(
    int index) const;

  /** Description:
    Returns the number of layers stored in this LayerFilter object. 
  */
  int layerCount() const;

  OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  void dwgOutFields(
    OdDbDwgFiler* pFiler) const;
  
  OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbLayerFilter object pointers.
*/
typedef OdSmartPtr<OdDbLayerFilter> OdDbLayerFilterPtr;

#include "DD_PackPop.h"

#endif // OD_DBLYFILT_H


