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



#ifndef OD_MULTIMAP_H
#define OD_MULTIMAP_H

#define  STL_USING_MAP
#include "OdaSTL.h"

#include "OdPlatform.h"

/** Description:
    This class extends functionality of the standard STL class std::multimap.

    Remarks:
    It just contains a number of methods for advanced element search.
    The default constructor creates an empty set.
    The template arguments are equal to STL's ones.

    {group:Other_Classes}
*/
template<class Key, 
         class T, 
				 class Pred = OD_TYPENAME std::less<Key>, 
				 class A = OD_TYPENAME2 std::allocator<OD_TYPENAME2 std::pair<const Key, T> > >
class OdMultimap : public std::multimap<Key, T, Pred, A>
{
public:
	typedef typename std::multimap<Key, T, Pred, A>::iterator iterator;
	typedef typename std::multimap<Key, T, Pred, A>::const_iterator const_iterator;

  // 01.06.2002 G. Udov trying to eliminate using static variables
  OdMultimap(const Pred& comp = Pred(), const A& al = A()) 
    : std::multimap<Key, T, Pred, A>(comp, al) {}

  OdMultimap(const typename std::multimap<Key, T, Pred, A>::value_type *first, 
             const typename  std::multimap<Key, T, Pred, A>::value_type *last, 
             const Pred& comp = Pred(), 
             const A& al = A())
    : std::multimap<Key, T, Pred, A>(first, last, comp, al) {}
  
  // Some keys can be unequal exactly but they can be indistinguishable by the comparer Pred.
  // The method find of the class std::multimap doesn't care about this case.
  // It just returns an iterator to the first element whose key is indistinguishable
  // from the method's argument by the comparer Pred.
  // This method cares about this case too.
  // It returns iterator whose key is equal (operator ==) to the method's argument.
  iterator find_ex(const Key& key)
  {
    iterator i = find (key),
             iend = this->end();
    while (i != iend && !this->key_comp() (key, i->first))
    {
      if (i->first == key)
        return i;
      ++i;
    }
    return iend;
  }

  // This method and the previous one do the same. But the former is used for constant objects.
  const_iterator find_ex(const Key& key) const
  {
    const_iterator i = find (key),
                   iend = this->end();
    while (i != iend && !this->key_comp() (key, i->first))
    {
      if (i->first == key)
        return i;
      ++i;
    }
    return iend;
  }

  // The method returns an iterator to the last element whose key is indistinguishable
  // from the method's argument by the comparer Pred.
  iterator find_last(const Key& key)
  {
    iterator i = find (key), j,
             iend = this->end();
    while (i != iend && !this->key_comp() (key, i->first))
    {
      j = i;
      ++i;
    }
    return j;
  }
};


#endif // OD_MULTIMAP_H


