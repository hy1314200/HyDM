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



#ifndef __ODGSSELECTIONREACTOR_H_INCLUDED_
#define __ODGSSELECTIONREACTOR_H_INCLUDED_


class OdDbStub;
 
/** Description:
    Contains information related to an OdGiDrawable object, including the object itself,
    the parent object, and the some flags.

    {group:OdGi_Classes} 
*/
struct OdGiDrawableDesc 
{
  OdGiDrawableDesc()
    : pParent(0)
    , persistId(0)
    , pTransientDrawable(0)
    , nDrawableFlags(0)
  {}

  /** Description:
      OdGiDrawableDesc of this drawable's parent object.
  */
  OdGiDrawableDesc*   pParent;


  /** Description:
      Returns the persistent ID of the database object associated with this drawable.
  */
  OdDbStub*           persistId;

  /** Description:
      Returns the OdGiDrawable object itself.
  */
  const OdGiDrawable* pTransientDrawable;

  /** Description:
      Returns the drawable flags for this object (normally the flags returned
      by the call to OdGiDrawable::setAttributes for this drawable).
  */
  OdUInt32            nDrawableFlags;

  /** Description:
      Sets the "skip" flag for this object, which, if true, will cause the object
      to be skipped during vectorization.
  */
  void markToSkip(bool val) const;

  /** Description:
      Returns true if this drawable is to be skipped during vectorization, false
      otherwise.
  */
  bool isMarkedToSkip() const;
};

/** Description:

    {group:OdGs_Classes} 
*/
class OdGsSelectionReactor
{
public:
  // returns false to abort operation

  virtual bool selected(const OdGiDrawableDesc& drawableDesc) = 0;
};

inline void OdGiDrawableDesc::markToSkip(bool val) const
{
  SETBIT(const_cast<OdGiDrawableDesc*>(this)->nDrawableFlags, 0x80000000, val);
}

inline bool OdGiDrawableDesc::isMarkedToSkip() const
{
  return GETBIT(nDrawableFlags, 0x80000000);
}

#endif // __ODGSSELECTIONREACTOR_H_INCLUDED_


