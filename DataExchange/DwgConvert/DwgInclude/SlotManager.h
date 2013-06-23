// SlotManager.h: interface for the OdSlotManager class.
//
//////////////////////////////////////////////////////////////////////

#ifndef _SLOTMANAGER_H_INCLUDED_
#define _SLOTMANAGER_H_INCLUDED_

#include "IntArray.h"


typedef unsigned int OdSlotId;
#define kOdNullSlotId OdSlotId(-1)


/** Description:

    {group:Other_Classes}
*/
class OdSlotManager  
{
  OdIntArray  m_freeSlots;
  unsigned int  m_numSlots;
public:
  OdSlotManager() : m_numSlots(0) {}
  inline OdSlotId newSlot()
  {
    int res;
    if(m_freeSlots.isEmpty())
    {
      res = m_numSlots++;
    }
    else
    {
      res = m_freeSlots.last();
      m_freeSlots.removeLast();
    }
    return res;
  }

  inline void freeSlot(OdSlotId id)
  {
    if(id+1==m_numSlots)
    {
      --m_numSlots;
    }
    else
    {
      m_freeSlots.append(id);
    }
  }

  inline bool contains(OdSlotId id) const
  {
    return (id < m_numSlots && !m_freeSlots.contains(id, 0));
  }
};

/** Description:

    {group:Other_Classes}
*/
template <class TVal, class TAlloc = OdObjectsAllocator<TVal> >
class OdSlots : public OdArray<TVal, TAlloc>
{
  void ensureSpace(OdSlotId id)
  {
    if(id >= this->size())
    {
      this->resize(id+1);
    }
  }
public:
  typedef typename OdArray<TVal, TAlloc>::size_type size_type;

  OdSlots() {}

  OdSlots(size_type nPhysicalLength, int nGrowBy = 8)
    : OdArray<TVal, TAlloc>(nPhysicalLength, nGrowBy) {}

  static const TVal* emptySlotValue() { static const TVal def = TVal(); return &def; }

  const TVal& operator[](OdSlotId id) const
  {
    return (id < this->size() ? this->getPtr()[id] : *emptySlotValue());
  }
  TVal& operator[](OdSlotId id)
  {
    ensureSpace(id);
    return this->asArrayPtr()[id];
  }
  const TVal& getAt(OdSlotId id) const
  {
    return (id < this->size() ? this->getPtr()[id] : *emptySlotValue());
  }
  void setAt(OdSlotId id, const TVal& val)
  {
    ensureSpace(id);
    this->asArrayPtr()[id] = val;
  }
};


/** Description:

    {group:Other_Classes}
*/
template <class TVal, class TAlloc = OdObjectsAllocator<TVal> >
class OdManagedSlots
  : public OdSlots<TVal, TAlloc>
  , public OdSlotManager
{
public:
  typedef typename OdSlots<TVal, TAlloc>::size_type size_type;

  OdManagedSlots() {}

  OdManagedSlots(size_type nPhysicalLength, int nGrowBy = 8)
    : OdSlots<TVal, TAlloc>(nPhysicalLength, nGrowBy) {}

#ifdef _DEBUG
  const TVal& operator[](OdSlotId id) const
  {
    ODA_ASSERT(OdSlotManager::contains(id)); // invalid id
    return OdSlots<TVal, TAlloc>::operator[](id);
  }
  TVal& operator[](OdSlotId id)
  {
    ODA_ASSERT(OdSlotManager::contains(id)); // invalid id
    return OdSlots<TVal, TAlloc>::operator[](id);
  }
  const TVal& getAt(OdSlotId id) const
  {
    ODA_ASSERT(OdSlotManager::contains(id)); // invalid id
    return OdSlots<TVal, TAlloc>::getAt(id);
  }
  void setAt(OdSlotId id, const TVal& val)
  {
    ODA_ASSERT(OdSlotManager::contains(id)); // invalid id
    return OdSlots<TVal, TAlloc>::setAt(id, val);
  }
#endif //_DEBUG
};

#endif // #ifndef _SLOTMANAGER_H_INCLUDED_

