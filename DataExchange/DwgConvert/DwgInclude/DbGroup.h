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



#ifndef _ODDBGROUP_INCLUDED_
#define _ODDBGROUP_INCLUDED_

#include "DD_PackPush.h"

#include "DbObject.h"

class OdDbGroupIterator;
class OdCmColor;
class OdDbGroupImpl;
class OdDbGroup;

/** Description:
    This class implements Iterator objects that traverse entries in OdDbGroup objects in an OdDbDatabase.
    
    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbGroupIterator : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbGroupIterator);

  /** Description:
    Returns a SmartPointer to the current entity referenced by this Iterator object.
  
    Arguments:
    readOrWrite (I) Mode in which to open the current entity.

    Remarks:
    Returns a NULL SmartPointer if not successful.
  */
  virtual OdDbObjectPtr getObject(
    OdDb::OpenMode readOrWrite) = 0;

  /** Description:
    Returns the Object ID of the current entity referenced by this Iterator object.
  */
  virtual OdDbObjectId objectId() const = 0;

  /** Description:
    Returns true if and only if the traversal by this Iterator object is complete.
  */  
  virtual bool done() const = 0;

  /** Description:
    Sets this Iterator object to reference the entity following the current entity.
  */  
  virtual bool next() = 0;

  /*
     virtual void getClassID(void** pClsid) const;
  */
protected:
  OdDbGroupIterator() {}
};
/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbGroupIterator object pointers.
*/
typedef OdSmartPtr<OdDbGroupIterator> OdDbGroupIteratorPtr;

/** Description:
    This class represents Group objects in an OdDbDatabase instance.

    Remarks:
    Only top level entities in ModelSpace or PaperSpace can be placed in a Group.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbGroup: public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbGroup);

  OdDbGroup();

  /** Description:
    Creates an Iterator object that provides access to the entities in this Group object.
  */
  OdDbGroupIteratorPtr newIterator();

  /** Description:
    Returns the *description* of this Group object (DXF 300).
  */
  OdString description() const;

  /** Description:
    Sets the *description* of this Group object (DXF 300).
    Arguments:
    description (I) Description.
  */
  void setDescription(
    const OdString& description);

  /** Description:
    Returns true if and only if this Group object is *selectable* (DXF 71).
  */
  bool isSelectable() const;

  /** Description:
    Sets this Group object as *selectable* (DXF 71).

    Arguments:
    selectable (I) Selectable flag.
  */
  void setSelectable(
    bool selectable);

  /** Description:
    Returns the *name* of this Group object.
  */
  OdString name() const;

  /** Description:
    Sets the *name* for this Group object.
    Arguments:
    name (I) Name.
  */
  void setName(const OdString& name);

  /** Description:
    Returns true if and only if this Group object is inaccessible.
      
    Remarks:
    Currently, only groups in Xrefs are flagged as inaccessable.  
  */
  bool isNotAccessible() const;

  /** Description:
    Returns true if and only if this Group object is anonymous (DXF 70).
  */
  bool isAnonymous() const;

  /** Description:
    Sets this Group object as anonymous (DXF 70).
    
    Remarks:
    The *name* of this Group object will be set to an automatically generated anonymous group *name*.
  */
  void setAnonymous();

  /** Description:
    Appends the specified entity or entities to this Group object.  

    Arguments:
    objectId (I) Object ID of the entity to be appended.
  */
  void append(
    OdDbObjectId objectId);

  /**
    Arguments:
    objectIds (I) Object IDs of the entities to be appended.
  */
  void append(
    const OdDbObjectIdArray& objectIds);

  /** Description:
    Prepends the specified entity or entities to this Group object.  

    Arguments:
    objectId (I) Object ID of the entity to be prepended.
  */
  void prepend(
    OdDbObjectId objectId);

  /**
    Arguments:
    objectIds (I) Object IDs of the entities to be prepended.
  */
  void prepend(
    const OdDbObjectIdArray& objectIds);

  /** Description:
    Inserts the specified entity or entities to this Group object at the specified *index*.  

    Arguments:
    objectId (I) Object ID of the entity to be inserted.
    index (I) Insertion *index*.
    
    Remarks:
    Entities will be inserted just after the entity at the specified *index*.
  */
  void insertAt(
    OdUInt32 index, 
    OdDbObjectId objectId);

  /**
    Arguments:
    objectIds (I) Object IDs of the entities to be inserted.
  */
  void insertAt(
    OdUInt32 index, 
    const OdDbObjectIdArray& objectIds);

  /** Description:
    Removes the specified entity or entities from this Group object.
    
    Arguments:
    objectId (I) Object ID of the entity to be removed.
  */
  void remove(
    OdDbObjectId objectId);

  /** Description:
    Removes the entity at the specified *index*, 
    or the specified entities at or above the specified *index*,
    from this Group object.

    Arguments:
    index (I) Entity *index*.
    
  */
  void removeAt(
    OdUInt32 index);

  /**
    Arguments:
    objectIds (I) Object IDs of the entities to be removed.
  */
  void remove(
    const OdDbObjectIdArray& objectIds);

  /** 
    Arguments:
    objectIds (I) Object IDs of the entities to be inserted.

    Remarks:
    If any of the entities in objectIds are not in this Group object,
    or are at an *index* > index, none of the entities will be removed.
        
  */
  void removeAt(
    OdUInt32 index, 
    const OdDbObjectIdArray& objectIds);

  /** Description:
    Replaces the specified entity in this Group object with 
    the specified entity currently not in this Group object.

    Arguments:
    oldId (I) Object ID of the entity to be 
    newId (I) Object ID of the entity to replace oldId.
  */
  void replace(
    OdDbObjectId oldId, 
    OdDbObjectId newId);

  /** Description:
    Rearranges the entities within this Group object.

    Arguments:
    fromIndex (I) Index of the first entity to be moved.
    toIndex (I) Destination *index* of the first entity moved.
    numItems (I) Number of objects to move.
      
    Remarks:
    This function transfers numItems starting at the fromIndex to the toIndex. 
  */
  void transfer(
    OdUInt32 fromIndex, 
    OdUInt32 toIndex, 
    OdUInt32 numItems);

  /** Description:
    Clears the contents of this Group object.
    
    Remarks:
    After clearing, this Group object contains no entities.
  */
  void clear();

  /** Description:
    Returns the number of entities in this Group object.
  */
  OdUInt32 numEntities() const;

  /** Description:
    Returns true if and only if this Group object contains the specified entity.
    
    Arguments:
    pEntity (I) Pointer to the entity being tested.
  */
  bool has(
    const OdDbEntity* pEntity) const;

  /** Description:
    Returns the number of entities in this Group object, and their Object IDs.

    Arguments:
    objectIds (O) Receives the Object IDs.
  */
  OdUInt32 allEntityIds(
    OdDbObjectIdArray& objectIds) const;

  /** Description:
    Returns the *index* of the specified entity within this Group object.

    Arguments:
    objectId (I) Object ID of the entity.
    index (O) Index of objectId within this Group.

    Throws:
    @table
    Exception            Cause
    eInvalidInput        objectId is not in this Group object.
  */
  void getIndex(
    OdDbObjectId objectId, 
    OdUInt32& index) const;

  /** Description:
    Reverses the order of the entities in this Group object.
  */
  void reverse();

  /** Description:
      Sets the color of all entities in this group to the specified color.
  void setColor(const OdCmColor &color);
  */

  /** Description:
    Sets the *color* *index* of all entities in this Group object to the specified value.
    
    Arguments:
    colorIndex (I) Color *index*.
  */
  void setColorIndex(
    OdUInt16 colorIndex);

  /** Description:
    Sets the *color* of all entities in this Group object to the specified value.

    Arguments:
    color (I) Color.
  */
  void setColor(
    const OdCmColor& color);

  /** Description:
    Sets the *layer* of all entities in this Group object to the specified value.
    
    Arguments:
    layer (I) Layer *name*.
  */
  void setLayer(
    const OdString& layer);

  /**
    Arguments:
    layerId (I) Object ID of *layer*.
  */
  void setLayer(
    OdDbObjectId layerId);

  /** Description:
    Sets the *linetype* of all entities in this Group object to the specified value.
    
    Arguments:
    linetype (I) Linetype *name*.
  */
  void setLinetype(
    const OdString& linetype);

  /**
    Arguments:
    linetypeId (I) Object ID of *linetype*.
  */
  void setLinetype(
    OdDbObjectId linetypeID);

  /** Description:
    Sets the *linetype* scale of all entities in this Group object to the specified value.
    
    Arguments:
    linetypeScale (I) Linetype scale.
  */
  void setLinetypeScale(
    double linetypeScale);

  /** Description:
    Sets the *visibility* of all entities in this Group object to the specified value.
    
    Arguments:
    visibility (I) Visibility.
  */
  void setVisibility(
    OdDb::Visibility visibility);

  /** Description:
    Sets the PlotStyleName of all entities in this Group object to the specified value.

    Arguments:
    plotStyleName (I) PlotStyleName.
  */
  void setPlotStyleName(
    const OdString& plotStyleName);

  /** Description:
    Sets the LineWeight of all entities in this Group object to the specified value.
    
    Arguments:
    lineWeight (I) LineWeight.
  */
  void setLineweight(
    OdDb::LineWeight lineWeight);

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  virtual void copied (
    const OdDbObject* pObject, 
    const OdDbObject* pNewObject);

  virtual void getClassID(
    void** pClsid) const;

  
  /*
      void setHighlight(bool newVal);
      virtual void applyPartialUndo(OdDbDwgFiler* undoFiler, OdRxClass* classObj);
      virtual void subClose();
      virtual OdResult subErase(bool erasing = true);
      virtual void goodbye(const OdDbObject* dbObj);
  */

};

/** Description:
  This template class is a specialization of the OdSmartPtr class for OdDbGroup object pointers.
*/
typedef OdSmartPtr<OdDbGroup> OdDbGroupPtr;

#include "DD_PackPop.h"

#endif //_ODDBGROUP_INCLUDED_


