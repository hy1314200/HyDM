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



// (C) Copyright 2001 by Open Design Alliance. All rights reserved.

#ifndef _ODDBOBJECTID_INCLUDED_
#define _ODDBOBJECTID_INCLUDED_ /* {Secret } */

#include "DD_PackPush.h"


class OdDbDatabase;
class OdDbObject;
template <class T> class OdSmartPtr;

/**
    Description:
    This template class is a specialization of the OdSharedPtr class for OdDbObject pointers.
*/
typedef OdSmartPtr<OdDbObject> OdDbObjectPtr;

class OdDbStub;

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum OpenMode
  {
    kNotOpen           = -1,  // Object is not open.
    kForRead           = 0,   // Object is open for reading.
    kForWrite          = 1,   // Object is open for reading and/or writing.
    kForNotify         = 2    // Object is open for notify.
  };

  enum ReferenceType
  {
    kSoftPointerRef     = 0,  // Soft Pointer Reference
    kHardPointerRef     = 1,  // Hard Pointer Reference
    kSoftOwnershipRef   = 2,  // Soft Ownership Reference
    kHardOwnershipRef   = 3   // Hard Ownership Reference
  };
}

/** 
    Description:
    This class implements memory-resident ObjectId objects for OdDbDatabase objects.  

    Remarks:
    Database objects reference
    other *database* objects using ObjectId objects, and a *database* object pointer
    can always be obtained from a valid ObjectId objects. The effect of this mechanism is
    that *database* objects do not have to reside in memory unless they are explicitly
    being examined or modified by the user.  
    
    The user must explicitly open an object
    before reading or writing to it, and should release it when the operation
    is completed.  This functionality allows DWGdirect to support partial loading of 
    a *database*, where ObjectId objects exist for all objects in the *database*, but 
    the actual *database* objects need not be loaded until they are accessed.  
    It also allows *database* objects that are not in use to be swapped out of memory, 
    and loaded back in when they are accessed.  ObjectId objects are not written out to a 
    DWG/DXF file.  If a reference must be preserved to a *database* object that has been 
    serialized, the object's *database* handle (OdDbHandle) should be used.

    See Also:
    o  OdDbHandle
    o  OdDbObject

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbObjectId
{
public:
  /**
    Remarks:
    This function guarantees that isNull() will 
    return true if it is the first operation applied to this instance.
  */
  OdDbObjectId () : m_Id (0) { }

  /** 
    Description:
    For DWGdirect internal use only. 
  */

  /* {Secret} */
  OdDbObjectId (
    OdDbStub* objectId) : m_Id (objectId) { }

  /** 
    Description:
    The null ObjectId object.
  */
  static const OdDbObjectId kNull;

  /** 
    Description:
    Returns true and only if this ObjectId object is NULL.
  */
  bool isNull () const;

  /** 
    Description:
    Sets this Object ID to null.
  */
  void setNull () { m_Id = 0; }

  /** 
    Description:
    Returns true and only if this ObjectId object references a valid object.
  */
  bool isValid () const;

  OdDbObjectId& operator = (
    const OdDbObjectId& objectId) { m_Id = objectId.m_Id; return *this; }

  /* { Secret } */
  OdDbObjectId& operator = (
    OdDbStub* objectId) { m_Id = objectId; return *this; }

  bool operator < (
    const OdDbObjectId& objectId) const;

  bool operator > (
    const OdDbObjectId& objectId) const;

  bool operator >= (
    const OdDbObjectId& objectId) const;

  bool operator <= (
    const OdDbObjectId& objectId) const;

  bool operator == (
    const OdDbObjectId& objectId) const;

  bool operator != (
    const OdDbObjectId& objectId) const;

  bool operator ! () const;

  /** 
    Description:
    For DWGdirect internal use only.
  */
  
  /* {Secret} */
  inline operator OdDbStub* () const { return m_Id; }

  /** 
    Description:
    For DWGdirect internal use only. 
  */
  
  /* {Secret} */
  inline OdDbStub* operator -> () const { return (OdDbStub*)m_Id; }

  /** 
    Description:
    Returns a pointer to the *database* with which this ObjectId object is associated.
    
    Remarks:
    Returns NULL if this ObjectId object is not associated with any *database*.
  */
  OdDbDatabase* database () const;

  /** Description:
    Returns the original *database* with which this ObjectId object is associated.
    
    Remarks:
    If the object associated with this ObjectId object has been redirected to
    a host *database* from an xref *database*, this function returns
    a pointer to the xref *database*. 
    
    Otherwise, it returns a pointer to the *database* with which this ObjectId object is associated.
  */
  OdDbDatabase* originalDatabase () const;

  /** Description:
    If this ObjectId object has been redirected from another *database* (possibly an xref), this function
    returns the actual ObjectId object for that *database*.
  */
  void convertToRedirectedId ();

  /** Description:
    Returns true if and only if the object associated with this ObjectId object is erased.
  */
  bool isErased () const;

  /** 
    Description:
    Returns true if and only if this object associated with this ObjectId object is erased, or any of its ownership hierarchy
    have been erased.
  */
  bool isEffectivelyErased () const;

  /** 
    Description:
    For DWGdirect internal use only. 
  */
  
  /* {Secret} */
  bool objectLeftOnDisk () const;

  /** Description:
    Returns the *database* handle of the object referenced by this ObjectId object.
    
    Remarks:
    Returns NULL if no *database* object is referenced by this ObjectId object.

    If this ObjectId object has been redirected from another *database* (possibly an xref), this function
    returns the handle for this *database*.
  */
  const OdDbHandle& getHandle () const;

  /** Description:
    Returns the *database* handle of the object referenced by this ObjectId object.
    
    Remarks:
    Returns NULL if no *database* object is referenced by this ObjectgId object.

    If this ObjectId object has been redirected from another *database* (possibly an xref), this function
    returns the handle for the original *database*.
  */
  const OdDbHandle& getNonForwardedHandle () const;

  /** Description:
    Opens the *database* object associated with this ObjectId object, in the specified mode.

    Arguments:
    openMode (I) Mode in which to open the object.
    openErasedOne (I) If and only if true, *erased* objects will be opened.

    Remarks:
    Returns a SmartPointer to the opened object if successful, otherwise a null SmartPointer.
    
    openMode will be one of the following:
    
    @table
    Name          Value     Description
    kForRead      0         Object is open for reading.
    kForWrite     1         Object is open for reading and/or writing.
    kForNotify    2         Object is open for notify.
    
  */
  OdDbObjectPtr openObject (
    OdDb::OpenMode openMode = OdDb::kForRead, 
    bool openErasedOne = false) const;
  /** Description:
    Opens the *database* object associated with this ObjectId object, in the specified mode, or throws and exception if unsucessful.

    Arguments:
    openMode (I) Mode in which to open the object.
    openErasedOne (I) If and only if true, *erased* objects will be opened.

    Remarks:
    Returns a SmartPointer to the opened object.
    
    openMode will be one of the following:
    
    @table
    Name          Value     Description
    kForRead      0         Object is open for reading.
    kForWrite     1         Object is open for reading and/or writing.
    kForNotify    2         Object is open for notify.

    Throws:
    
    @table
    Exception           Cause
    eNullObjectId       This ObjectId object is null.
    ePermanentlyErased  Not opened and openErasedOne == 1
    eWasErased          Not opened and openErasedOne == 0
    
  */  
  OdDbObjectPtr safeOpenObject (
    OdDb::OpenMode openMode = OdDb::kForRead, 
    bool openErasedOne = false) const;

  /** Description:
    Binds the specified object.
    
    pObj (I) Pointer to the object to bind.
  */
  void bindObject (
    OdDbObject* pObj);

protected:
  friend class OdDbStub;
  OdDbStub* m_Id;
};

/** Description:
    This class is a specialization of OdDbObjectId indicating a hard owner relationship.

    Remarks:
    An OdDbHardOwnershipId reference to another object is used when the the owner requires the owned
    object, and the owned object cannot exist on its own.
    
    An OdDbHardOwnershipId reference to an object dictates that the owned object is written to 
    DWG and DXF files whenever the owner object is written.
    
    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbHardOwnershipId : public OdDbObjectId
{
public:

  OdDbHardOwnershipId () {}

  OdDbHardOwnershipId (
    const OdDbObjectId& objectId) : OdDbObjectId (objectId) {}

  /* {Secret} */
  OdDbHardOwnershipId (
    OdDbStub* objectId) : OdDbObjectId (objectId) {}

  OdDbHardOwnershipId& operator = (
    const OdDbObjectId& objectId) { OdDbObjectId::operator= (objectId); return *this; }


  /** { Secret } */
  OdDbHardOwnershipId& operator = (
    OdDbStub* objectId) { m_Id = objectId; return *this; }

  bool operator != (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator!= (objectId); }

  /** { Secret } */
  bool operator != (
    OdDbStub* objectId) const { return OdDbObjectId::operator!= (objectId); }

  bool operator == (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator== (objectId); }

  /** { Secret } */
  bool operator == (
    OdDbStub* objectId) const { return OdDbObjectId::operator== (objectId); }
};

/** Description:
    This class is a specialization of OdDbObjectId indicating a soft owner relationship.

    Remarks:
    An OdDbSoftOwnershipId reference to another object is used when the the owner does not requires the owned
    object, and the owned object cannot exist on its own.

    An OdDbSoftOwnershipId reference to an object dictates that the owned object is written to 
    DWG and DXF files whenever the owner object is written.
    
    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSoftOwnershipId : public OdDbObjectId
{
public:

  OdDbSoftOwnershipId () {}

  OdDbSoftOwnershipId (
    const OdDbObjectId& objectId) : OdDbObjectId (objectId) {}

  /** { Secret } */
  OdDbSoftOwnershipId (
    OdDbStub* objectId) : OdDbObjectId (objectId) {}

  OdDbSoftOwnershipId& operator = (
    const OdDbObjectId& objectId) { OdDbObjectId::operator= (objectId); return *this; }

  /** { Secret } */
  OdDbSoftOwnershipId& operator = (
    OdDbStub* objectId) { m_Id = objectId; return *this; }
    
  bool operator != (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator!= (objectId); }

  /** { Secret } */
  bool operator != (
    OdDbStub* objectId) const { return OdDbObjectId::operator!= (objectId); }

  bool operator == (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator== (objectId); }

  /** { Secret } */
  bool operator == (
    OdDbStub* objectId) const { return OdDbObjectId::operator== (objectId); }

};

/** Description:
    This class is a specialization of OdDbObjectId indicating a hard pointer relationship.

    Remarks:
    An OdDbHardPointerId reference to another object is used when one object depends on the existance of another, but
    both are owned by other objects.

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbHardPointerId : public OdDbObjectId
{
public:

  OdDbHardPointerId () {}

  OdDbHardPointerId (
    const OdDbObjectId& objectId) : OdDbObjectId (objectId) {}

  /** { Secret } */
  OdDbHardPointerId (
    OdDbStub* objectId) : OdDbObjectId (objectId) {}

  OdDbHardPointerId& operator = (
    const OdDbHardPointerId& objectId) { OdDbObjectId::operator= (objectId); return *this; }

  OdDbHardPointerId& operator = (
    const OdDbObjectId& objectId) { OdDbObjectId::operator= (objectId); return *this; }

  /** { Secret } */
  OdDbHardPointerId& operator = (
    OdDbStub* objectId) { m_Id = objectId; return *this; }

  bool operator != (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator!= (objectId); }

  /** { Secret } */
  bool operator != (
    OdDbStub* objectId) const { return OdDbObjectId::operator!= (objectId); }

  bool operator == (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator== (objectId); }

  /** { Secret } */
  bool operator == (
    OdDbStub* objectId) const { return OdDbObjectId::operator== (objectId); }
};

/** Description:
    This class is a specialization of OdDbObjectId indicating a soft pointer relationship.

    Remarks:
    An OdDbSoftPointerId reference another object is used when neither object depends on the existance of the other.

    Library: Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbSoftPointerId : public OdDbObjectId
{
public:
  OdDbSoftPointerId () {}

  OdDbSoftPointerId (
    const OdDbObjectId& objectId) : OdDbObjectId (objectId) {}

  /** { Secret } */
  OdDbSoftPointerId (
    OdDbStub* objectId) : OdDbObjectId (objectId) {}

  OdDbSoftPointerId& operator = (
    const OdDbSoftPointerId& objectId) { OdDbObjectId::operator= (objectId); return *this; }

  OdDbSoftPointerId& operator = (
    const OdDbObjectId& objectId) { OdDbObjectId::operator= (objectId); return *this; }

  /** { Secret } */
  OdDbSoftPointerId& operator = (
    OdDbStub* objectId) { m_Id = (OdDbStub*)objectId; return *this; }

  bool operator != (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator!= (objectId); }

  /** { Secret } */
  bool operator != (
    OdDbStub* objectId) const { return OdDbObjectId::operator!= (objectId); }

  bool operator == (
    const OdDbObjectId& objectId) const { return OdDbObjectId::operator== (objectId); }
  /** { Secret } */
  bool operator == (
    OdDbStub* objectId) const { return OdDbObjectId::operator== (objectId); }

};


////////////////////// OdDbObjectId inlines ////////////////////

////////////////////// OdDbHardOwnershipId inlines ////////////////////

/*
inline
OdDbHardOwnershipId::OdDbHardOwnershipId (const OdDbObjectId& id)
: OdDbObjectId (id) {}

inline
OdDbHardOwnershipId::OdDbHardOwnershipId (OdDbStub* objId)
: OdDbObjectId (objId) {}

inline
OdDbHardOwnershipId& OdDbHardOwnershipId::operator = (const OdDbObjectId& id)
{ OdDbObjectId::operator= (id); return *this; }

inline
OdDbHardOwnershipId& OdDbHardOwnershipId::operator = (OdDbStub* objId)
{ m_Id = objId; return *this; }

inline
bool OdDbHardOwnershipId::operator != (const OdDbObjectId& id) const
{ return OdDbObjectId::operator!= (id); }

inline
bool OdDbHardOwnershipId::operator != (OdDbStub* objId) const
{ return OdDbObjectId::operator!= (objId); }

inline
bool OdDbHardOwnershipId::operator == (const OdDbObjectId& id) const
{ return OdDbObjectId::operator== (id); }

inline
bool OdDbHardOwnershipId::operator == (OdDbStub* objId) const
{ return OdDbObjectId::operator== (objId); }


///////////////////// OdDbSoftOwnershipId inlines ////////////////////

inline
OdDbSoftOwnershipId::OdDbSoftOwnershipId (const OdDbObjectId& id)
: OdDbObjectId (id) {}

inline
OdDbSoftOwnershipId::OdDbSoftOwnershipId (OdDbStub* objId)
: OdDbObjectId (objId) {}

inline
OdDbSoftOwnershipId& OdDbSoftOwnershipId::operator = (const OdDbObjectId& id)
{ OdDbObjectId::operator= (id); return *this; }

inline
OdDbSoftOwnershipId& OdDbSoftOwnershipId::operator = (OdDbStub* objId)
{ m_Id = objId; return *this; }

inline
bool OdDbSoftOwnershipId::operator != (const OdDbObjectId& id) const
{ return OdDbObjectId::operator!= (id); }

inline
bool OdDbSoftOwnershipId::operator != (OdDbStub* objId) const
{ return OdDbObjectId::operator!= (objId); }

inline
bool OdDbSoftOwnershipId::operator == (const OdDbObjectId& id) const
{ return OdDbObjectId::operator== (id); }

inline
bool OdDbSoftOwnershipId::operator == (OdDbStub* objId) const
{ return OdDbObjectId::operator== (objId); }


///////////////////// OdDbHardPointerId inlines ////////////////////

inline
OdDbHardPointerId::OdDbHardPointerId (const OdDbObjectId& id)
: OdDbObjectId (id) {}

inline
OdDbHardPointerId::OdDbHardPointerId (OdDbStub* objId)
: OdDbObjectId (objId) {}

inline
OdDbHardPointerId& OdDbHardPointerId::operator = (const OdDbHardPointerId& id)
{ OdDbObjectId::operator= (id); return *this; }

inline
OdDbHardPointerId& OdDbHardPointerId::operator = (const OdDbObjectId& id)
{ OdDbObjectId::operator= (id); return *this; }

inline
OdDbHardPointerId& OdDbHardPointerId::operator = (OdDbStub* objId)
{ m_Id = objId; return *this; }

inline
bool OdDbHardPointerId::operator != (const OdDbObjectId& id) const
{ return OdDbObjectId::operator!= (id); }

inline
bool OdDbHardPointerId::operator != (OdDbStub* objId) const
{ return OdDbObjectId::operator!= (objId); }

inline
bool OdDbHardPointerId::operator == (const OdDbObjectId& id) const
{ return OdDbObjectId::operator== (id); }

inline
bool OdDbHardPointerId::operator == (OdDbStub* objId) const
{ return OdDbObjectId::operator== (objId); }



//////////////////// OdDbSoftPointerId inlines ////////////////////

inline
OdDbSoftPointerId::OdDbSoftPointerId (const OdDbObjectId& id)
: OdDbObjectId (id) {}

inline
OdDbSoftPointerId::OdDbSoftPointerId (OdDbStub* objId)
: OdDbObjectId (objId) {}

inline
OdDbSoftPointerId& OdDbSoftPointerId::operator = (const OdDbSoftPointerId& id)
{ OdDbObjectId::operator= (id); return *this; }

inline
OdDbSoftPointerId& OdDbSoftPointerId::operator = (const OdDbObjectId& id)
{ OdDbObjectId::operator= (id); return *this; }

inline
OdDbSoftPointerId& OdDbSoftPointerId::operator = (OdDbStub* objId)
{ m_Id = (OdDbStub*)objId; return *this; }

inline
bool OdDbSoftPointerId::operator != (const OdDbObjectId& id) const
{ return OdDbObjectId::operator!= (id); }

inline
bool OdDbSoftPointerId::operator != (OdDbStub* objId) const
{ return OdDbObjectId::operator!= (objId); }

inline
bool OdDbSoftPointerId::operator == (const OdDbObjectId& id) const
{ return OdDbObjectId::operator== (id); }

inline
bool OdDbSoftPointerId::operator == (OdDbStub* objId) const
{ return OdDbObjectId::operator== (objId); }
*/

#include "DD_PackPop.h"

#endif //_ODDBOBJECTID_INCLUDED_



