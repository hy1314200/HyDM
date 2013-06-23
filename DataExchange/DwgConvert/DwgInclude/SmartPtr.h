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



#ifndef _ODASMARTPOINTER_INCLUDED_
#define _ODASMARTPOINTER_INCLUDED_

#include "DD_PackPush.h"

class OdRxObject;

/** Description:
    This class is the base class for DWGdirect SmartPointers.

    Library:
    Db:
    
    Remarks:
    SmartPointers relieve the programmer
    of having to determine when objects are no longer needed, or having to delete
    them at that time.

    Each object referenced by a SmartPointer (henceforth 'referenced object') 
    maintains a reference count; i.e., how many SmartPointers are referencing it.
    When the reference count reaches zero, the referenced object is deleted.


    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdBaseObjectPtr
{
protected:
  OdBaseObjectPtr() : m_pObject(NULL) {}
  
  OdRxObject* m_pObject;
  
public:
  /** Description:
    Returns the object referenced by this SmartPointer object. 
    
    Remarks:
    This SmartPointer maintains its reference to the referenced object.
    
    The reference count of the referenced object is unchanged.
  */
  OdRxObject* get() const {return m_pObject;}
  
  /** Description:
    Returns true if and only if this SmartPointer contains a null reference.
  */
  bool isNull() const { return m_pObject == 0; }
};

/** Description:
    This template class implements SmartPointers for instances derived from OdRxObject.

    Remarks:
    SmartPointers relieve the programmer
    of having to determine when objects are no longer needed, or having to delete them
    them at that time.

    Each object referenced by a SmartPointer (henceforth 'referenced object') 
    maintains a reference count; i.e., how many SmartPointers are referencing it.
    When the reference count reaches zero, the referenced object is deleted.

    Library:
    Db
    
    {group:Other_Classes}
*/
template <class T> class OdSmartPtr : public OdBaseObjectPtr
{
  /** Description:
    Increments the reference count of the referenced object.
  */
  void internalAddRef() { if(m_pObject) { ((T*)m_pObject)->addRef(); } }
  
  /** Description:
    Performs a "safe" assignment of the specified object to this SmartPointer object.  
      
    Remarks:
    The reference count of the specified object is incremented.

    If this SmartPointer is currently referencing another object, that object 
    is released prior to the assignment.  

    Arguments:
    pObject (I) Pointer to the object to be assigned.
  */
  void assign(
    const T* pObject)
  {
    release();
    m_pObject = (OdRxObject*)pObject;
    internalAddRef();
  }
  
  /** Description:
    Performs a "safe" assignment of the specified object to this SmartPointer object. 
      
    Arguments:
    pObject (I) Pointer to the object to be assigned.

    Throws:
    eNotThatKindOfClass if not successful. 
  */
  void internalQueryX(
    const OdRxObject* pObject)
  {
    if(pObject)
    {
      OdRxObject* pX = pObject->queryX(T::desc());
      if(pX)
        m_pObject = pX;
      else
        throw OdError(eNotThatKindOfClass);
    }
  }
  
  void assign(const OdRxObject* pObject)
  {
    release();
    internalQueryX(pObject);
  }
  
  // Note: Using of SmartPtr<T> as bool expression produce ambiguous call with some compilers.
  // Use isNull() method instead.

  /** Description:
    Declared private to prevent use.
    Note: 
    Use of SmartPtr<T> as a bool expression produces ambiguous calls with some compilers. Use isNull() instead. 
  */
  bool operator !() const { ODA_FAIL(); return false; }
  
  /** Description:
    Declared private to prevent use.
    Note: 
    Use of SmartPtr<T> as a bool expression produces ambiguous calls with some compilers. Use isNull() instead. 
  */
  operator bool() const { ODA_FAIL(); return false; }

  /** Description:
    Declared private to prevent use.
    Note: 
    Use of SmartPtr<T> as a bool expression produces ambiguous calls with some compilers. Use isNull() instead. 
  */
  operator bool() { ODA_FAIL(); return false; }
  
public:
  /** Description:
    Arguments:
    pSource (I) Pointer to the object to be assigned to the new SmartPointer object.

    Remarks:
    If pSource is specified, the specified object is assigned to this SmartPointer object.
    
    Remarks:
    If OdRxObjMod or const OdBaseObjectPtr& are specified, the reference count of the referenced object 
    is *not* incremented. 
  */
  OdSmartPtr() { }

  OdSmartPtr(
    const T* pSource, 
    OdRxObjMod) {m_pObject = (OdRxObject*)pSource; }
  
  OdSmartPtr(
    const T* pSource) {m_pObject = (OdRxObject*)pSource; internalAddRef(); }
  
  OdSmartPtr(
    const OdRxObject* pSource) { internalQueryX(pSource); }

  OdSmartPtr(
    OdRxObject* pSource, 
    OdRxObjMod)
  {
    internalQueryX(pSource);
    if(pSource)
      pSource->release();
  }
  
  OdSmartPtr(
    const OdSmartPtr& pSource) {m_pObject = (OdRxObject*)pSource.get(); internalAddRef(); }

  OdSmartPtr(
    const OdRxObjectPtr& pSource) { internalQueryX(pSource.get()); }

  OdSmartPtr(
    const OdBaseObjectPtr& pSource) { internalQueryX(pSource.get()); }

  /** Description:
    Assigns the specifed object to this SmartPointer object.  
      
    Arguments:
    pObject (I) Pointer to the object to be assigned.

    Remarks:
    The reference count of the specified object is not incremented.

    If this SmartPointer is currently referencing another object, that object 
    is released prior to the assignment.
  */
  void attach(
    const T* pObject) { release(); m_pObject = (OdRxObject*)pObject; }
  void attach(
    OdRxObject* pObject)
  {
    release();
    internalQueryX(pObject);
    if(pObject)
      pObject->release();
  }
  
  /**
    Remarks:
    Decrements the reference count of the object referenced by this
    SmartPointer object.

    When the reference count reaches zero, the referenced object is deleted.
  */
  ~OdSmartPtr() { release(); }
  
  /** Description:
    Releases this SmartPointer's reference to the referenced object.
    
    Remarks:
    Decrements the reference count of the referenced object. 
    
    When the reference count reaches zero, the referenced object is deleted.
  */
  void release()
  {
    if (m_pObject)
    {
      ((T*)m_pObject)->release();
      m_pObject = NULL;
    }
  }
  
  /** Description:
    Releases this SmartPointer's reference to the referenced object.
      
    Remarks:
    Returns a pointer to the object referenced by this SmartPointer object.
    
    The referenced object's reference count is not modified.
  */
  T* detach()
  {
    T* res = (T*)m_pObject;
    m_pObject = NULL;
    return res;
  }
  
  /**
    Remarks:
    The reference count of the referenced object is incremented.
      
    If this SmartPointer is currently referencing another object, that object 
    is released prior to the assignment.  
  */
  OdSmartPtr& operator = (
    const OdSmartPtr& pObject)
  { assign(pObject); return *this; }
    
  OdSmartPtr& operator = (
    const OdBaseObjectPtr& pObject)
  { assign(pObject.get()); return *this; }
  
  OdSmartPtr& operator = (
    const T* pObject)
  { assign(pObject); return *this; }
  
  /** Description:
    Returns a pointer to the referenced object.

    Remarks:
    This SmartPointer maintains its reference to the referenced object.
    
    The reference count of the referenced object is unchanged.
  */
  const T* get() const { return (T*)m_pObject; }
  
  T* get() { return (T*)m_pObject; }
  
  /** Description:
    Returns a pointer to the referenced object.
      
    Remarks:
    The reference count of the referenced object is unchanged.
  */
  T* operator ->() { return (T*)m_pObject; }
  
  const T* operator ->() const { return (T*)m_pObject; }
  
#ifdef ODA_GCC_2_95
  /** Description:
    Returns a pointer to the referenced object.
    
    Remarks:
    This SmartPointer maintains its reference to the referenced object.
    
    The reference count of the referenced object is unchanged.

    Remarks:
    This non-standard form is used to eliminate a large number of compiler
    warnings produced by GCC 2.95.X (GCC 3.X no longer produces these warnings).
  */
  operator T*() const { return (T*)m_pObject; }
  
#else
  /** Description:
    Returns a pointer to the referenced object.
    
    Remarks:
    This SmartPointer maintains its reference to the referenced object.
    
    The reference count of the referenced object is unchanged.
  */
  operator T*() { return (T*)m_pObject; }
  
  operator const T*() const { return (T*)m_pObject; }

#endif
    
  bool operator==(
    const void* pObject) const { return (m_pObject==pObject); }

  bool operator==(
    const OdSmartPtr& pObject) const { return operator==((void*)pObject.get()); }
  
  bool operator!=(
    const void* pObject) const { return (m_pObject!=pObject); }

  bool operator!=(
    const OdSmartPtr& pObject) const { return operator!=((void*)pObject.get()); }
};

#include "DD_PackPop.h"

#endif //_ODASMARTPOINTER_INCLUDED_

