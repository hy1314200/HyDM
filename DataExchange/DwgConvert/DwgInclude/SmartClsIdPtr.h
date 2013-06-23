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



#ifndef _ODSMARTCLSIDPTR_INCLUDED_
#define _ODSMARTCLSIDPTR_INCLUDED_

#include "DD_PackPush.h"
#include "SmartPtr.h"

class OdRxObject;

#define ODRX_DEFINE_CLSID_RETRIEVER(Class) \
class OdRxClsIdHolderFor_##Class\
{\
public:\
  static inline const OdRxClass* classId()\
  {\
    OdRxClass* pClass = static_cast<OdRxClass*>(::odrxClassDictionary()->getAt(#Class).get());\
    if(!pClass)\
      throw OdError(eNotInitializedYet);\
    return pClass;\
  }\
}

#define ODRX_DEFINE_CLSID_SMARTPTR(Class) \
ODRX_DEFINE_CLSID_RETRIEVER(Class);\
typedef OdClsIdSmartPtr<OdRxClsIdHolderFor_##Class, Class> Class##ClsIdPtr


/** Description:
    DWGdirect smart pointer template class.

    {group:Other_Classes}
*/
template <class ClsIdRetriever, class T> class OdClsIdSmartPtr : public OdBaseObjectPtr
{
  /** Description:
      Increments the reference count for the object referenced by this smart pointer.
  */
  inline void internalAddRef() { if(m_pObject) { static_cast<T*>(m_pObject)->addRef(); } }
  
  /** Description:
      Assigns the specifed object to this smart pointer.  If this smart pointer 
      is currently referencing another object, that object is released prior to 
      the assignment.  The reference count on the passed in object is incremented
      by one.

      Arguments:
      pObj (I) Pointer to be assigned to this smart pointer.
  */
  inline void assign(const T* pObj)
  {
    release();
    m_pObject = (OdRxObject*)pObj;
    internalAddRef();
  }
  
  /** Description:
      Performs a "safe" assignment of the passed in object to this smart pointer. If the 
      passed in object is of a compatible type, the assigment succeed, otherwise an 
      OdError(eNotThatKindOfClass) exception is thrown.
  */
  inline void internalQueryX(const OdRxObject* pObj)
  {
    if(pObj)
    {
      OdRxObject* pX = pObj->queryX(ClsIdRetriever::classId());
      if(pX)
        m_pObject = pX;
      else
        throw OdError(eNotThatKindOfClass);
    }
  }
  
  /** Description:
      Performs a "safe" assignement of the specifed object to this smart pointer.  
      If this smart pointer is currently referencing another object, that object 
      is released prior to the assignment.  The reference count on the passed in 
      object is incremented by one.

      Arguments:
      pObj (I) Pointer to be assigned to this smart pointer.
  */
  inline void assign(const OdRxObject* pObj)
  {
    release();
    internalQueryX(pObj);
  }
  
  // Note: Using of SmartPtr<T> as bool expression produce ambiguous call with some compilers.
  // Use isNull() method instead.

  /** Description:
      Declared private to prevent use. */
  bool operator !() const { ODA_FAIL(); return false; }
  
  /** Description:
      Declared private to prevent use. */
  operator bool() const { ODA_FAIL(); return false; }

  /** Description:
      Declared private to prevent use. */
  operator bool() { ODA_FAIL(); return false; }
  
public:
  /** Description:
      Constructor (no arguments).
  */
  inline OdClsIdSmartPtr() { }
  
  /** Description:
      Constructor (T*, OdRxObjMod).  Sets this smart pointer to reference the specified
      object, but DOES NOT increment the reference count of the specfied object.
  */
  inline OdClsIdSmartPtr(const T* pObj, OdRxObjMod) {m_pObject = (OdRxObject*)pObj; }
  
  /** Description:
      Constructor (T*).  Sets this smart pointer to reference the specified
      object, and increments the reference count of the specfied object.
  */
  inline OdClsIdSmartPtr(const T* pObj) {m_pObject = (OdRxObject*)pObj; internalAddRef(); }
  
  /** Description:
      Constructor (const OdRxObject*).  Sets this smart pointer to reference the specified
      object, and increments the reference count of the specfied object.
  */
  inline OdClsIdSmartPtr(const OdRxObject* pObj) { internalQueryX(pObj); }
  
  static inline const OdRxClass* classId()
  {
    return ClsIdRetriever::classId();
  }

  /** Description:
      Constructor (OdRxObject*, OdRxObjMod).  Sets this smart pointer to reference the specified
      object, but DOES NOT increment the reference count of the specfied object.
  */
  inline OdClsIdSmartPtr(OdRxObject* pObj, OdRxObjMod)
  {
    internalQueryX(pObj);
    if(pObj)
      pObj->release();
  }
  
  /** Description:
      Constructor (const OdClsIdSmartPtr&).  Sets this smart pointer to reference the specified
      object, and increments the reference count of the specfied object.
  */
  inline OdClsIdSmartPtr(const OdClsIdSmartPtr& pObj)
  {
    m_pObject = const_cast<T*>(static_cast<const T*>(pObj.get()));
    internalAddRef();
  }

  /** Description:
      Constructor (const OdRxObjectPtr&).  Sets this smart pointer to reference the specified
      object, and increments the reference count of the specfied object.
  */
  inline OdClsIdSmartPtr(const OdRxObjectPtr& pObj) { internalQueryX(pObj.get()); }
  
  /** Description:
      Constructor (const OdBaseObjectPtr&).  Sets this smart pointer to reference the specified
      object, but DOES NOT increment the reference count of the specfied object.
  */
  inline OdClsIdSmartPtr(const OdBaseObjectPtr& pObj) { internalQueryX(pObj.get()); }

  /** Description:
      Releases the reference currently held by this smart pointer (if present), and 
      sets this smart pointer to reference the passed in object.  Does not increment the 
      reference count of the specified object.
  */
  static inline OdClsIdSmartPtr cast(const OdRxObject* pObj)
  {
    OdClsIdSmartPtr pRes;
    if (pObj)
      pRes.attach(static_cast<T*>(pObj->queryX(ClsIdRetriever::classId())));
    return pRes;
  }
  
  /** Description:
      Releases the reference currently held by this smart pointer (if present), and 
      sets this smart pointer to reference the passed in object.  Does not increment the 
      reference count of the specified object.
  */
  inline void attach(const T* pObj) { release(); m_pObject = const_cast<T*>(pObj); }
  
  /** Description:
      Releases the reference currently held by this smart pointer (if present), and 
      sets this smart pointer to reference the passed in object.
  */
  inline void attach(OdRxObject* pObj)
  {
    release();
    internalQueryX(pObj);
    if(pObj)
      pObj->release();
  }
  
  /** Description:
      Destructor. Decrements the reference count of the object referenced by this
      smart pointer.
  */
  inline ~OdClsIdSmartPtr() { release(); }
  
  /** Description:
      Decrements the reference count for the object referenced by this smart
      pointer, and gives up this smart pointer's reference to the object.
  */
  inline void release()
  {
    if (m_pObject)
    {
      static_cast<T*>(m_pObject)->release();
      m_pObject = 0;
    }
  }
  
  /** Description:
      Returns a pointer to the object referenced by this smart pointer, and 
      gives up this smart pointer's reference to the object.  The object's reference
      count is not modified.
  */
  inline T* detach()
  {
    T* res = static_cast<T*>(m_pObject);
    m_pObject = 0;
    return res;
  }
  
  /** Description:
      Assignment operator.
  */
  inline OdClsIdSmartPtr& operator = (const OdClsIdSmartPtr& pObj)
  { assign(pObj); return *this; }
    
  /** Description:
      Assignment operator.
  */
  inline OdClsIdSmartPtr& operator = (const OdBaseObjectPtr& Obj)
  { assign(Obj.get()); return *this; }
  
  /** Description:
      Assignment operator.
  */
  inline OdClsIdSmartPtr& operator = (const T* pObj)
  { assign(pObj); return *this; }
  
  /** Description:
      Returns a const pointer to the object referenced by this smart pointer.
  */
  inline const T* get() const { return const_cast<T*>(static_cast<const T*>(m_pObject)); }
  
  /** Description:
      Returns a pointer to the object referenced by this smart pointer.  This
      smart pointer object maintains its reference to the object, and the object's
      reference count is not modified.
  */
  inline T* get() { return static_cast<T*>(m_pObject); }
  
  /** Description:
      Returns a pointer to the object referenced by this smart pointer.
  */
  inline T* operator ->() { return static_cast<T*>(m_pObject); }
  
  /** Description:
      Returns a const pointer to the object referenced by this smart pointer.
  */
  inline const T* operator ->() const { return const_cast<T*>(static_cast<const T*>(m_pObject)); }
  
#ifdef ODA_GCC_2_95
  /** Description:
      Returns a pointer to the object referenced by this smart pointer.
      This non-standard form is used to eliminate compiler
      warnings produced by GCC 2.95.X (GCC 3.X no longer produces these warnings).
  */
  inline operator T*() const { return static_cast<T*>(m_pObject); }
  
#else
  /** Description:
      Returns a pointer to the object referenced by this smart pointer.
  */
  inline operator T*() { return static_cast<T*>(m_pObject); }
  
  /** Description:
      Returns a const pointer to the object referenced by this smart pointer.
  */
  inline operator const T*() const { return static_cast<const T*>(m_pObject); }
#endif
    
  /** Description:
      Equality operator.
  */
  inline bool operator==(const void* p) const { return (m_pObject==p); }

  /** Description:
      Equality operator.
  */
  inline bool operator==(const OdClsIdSmartPtr& ptr) const { return operator==((void*)ptr.get()); }
  
  /** Description:
      Inequality operator.
  */
  inline bool operator!=(const void* p) const { return (m_pObject!=p); }

  /** Description:
      Inequality operator.
  */
  inline bool operator!=(const OdClsIdSmartPtr& ptr) const { return operator!=((void*)ptr.get()); }
};

#include "DD_PackPop.h"

#endif //_ODSMARTCLSIDPTR_INCLUDED_

