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



#ifndef _INC_ODAINT64_38FF72C90128_INCLUDED
#define _INC_ODAINT64_38FF72C90128_INCLUDED

#include "DD_PackPush.h"
#include "OdPlatformSettings.h"
#include "OdHeap.h"

/** Description:
    Signed 64-bit integer.

    {group:Other_Classes}
*/
class OdInt64
{
public:
  /** Description:
      to assign 0 or 1
  */
	inline OdInt64& operator=(const OdInt64& n);
	OdInt64& operator=(int n);
  OdInt64() : low(0), hi(0) { }
	OdInt64(int n) { hi = (n>=0 ? 0 : -1); low = n; }
  OdInt64(const OdInt64& n) { *this = n; }
	OdInt64& operator+=(const OdInt64& n);
	inline OdInt64 operator+(const OdInt64& n) const {return (OdInt64(*this)+=n);}
	inline OdInt64& operator-=(const OdInt64 n) { return (*this += -n); }
	inline OdInt64 operator-(const OdInt64& n) const {return (OdInt64(*this)-=n);}
	inline OdInt64& operator++();
	inline OdInt64 operator++(int /*dummy*/);
	inline OdInt64& operator--();
	inline bool operator>(const OdInt64& n) const;
	inline bool operator<(const OdInt64& n) const;
	inline bool operator!=(const OdInt64& n) const;
	inline bool operator==(const OdInt64& n) const;

  OdUInt32 operator & (OdUInt32 mask) const
  { return low & mask;}

  /** Description:
      to compare with 0 or 1
  */
	inline bool operator!=(int n) const;
  /** Description:
      to compare with 0 or 1
  */
	inline bool operator==(int n) const;

	bool operator>(int n) const { return operator>(OdInt64(n)); }
	bool operator<(int n) const { return operator<(OdInt64(n)); }

	inline OdInt64 operator-() const;
	OdInt64& operator >>=(int n);
	OdInt64& operator <<=(int n)
	{
		while(n--) { *this += *this; }
		return *this;
	}
	OdInt64 operator >>(int n) const
	{
		OdInt64 res = *this;
		return (res>>=n);
	}
	OdInt64 operator <<(int n) const
	{
		OdInt64 res = *this;
		return (res<<=n);
	}

  OdInt64 operator | (const OdInt64& n)
	{
    OdInt64 res;
		res.low = low | n.low;
		res.hi = hi | n.hi;
		return res;
	}

	OdInt64& operator |=(const OdInt64& n)
	{
		low |= n.low;
		hi |= n.hi;
		return *this;
	}

protected:
#ifdef ODA_BIGENDIAN
  OdInt32   hi;
  OdUInt32  low;
#else
  OdUInt32  low;
  OdInt32   hi;
#endif
};

/** Description:
    Unsigned 64-bit integer.

    {group:Other_Classes}
*/
class OdUInt64 : public OdInt64
{
public :
  OdUInt64 () { }
  OdUInt64 (int n) : OdInt64(n) { }
  OdUInt64 (const OdInt64& n) : OdInt64(n) { }
  OdUInt64 (const OdUInt64& n) : OdInt64(n) { }

  bool operator > (const OdUInt64& n) const
  {
	  if (hi == n.hi)
		  return low > n.low;
	  // else
	  return OdUInt32(hi) > OdUInt32(n.hi);
  }
  bool operator < (const OdUInt64& n) const
  {
	  return (!operator>(n) && operator!=(n));
  }
	bool operator>(int n) const { return operator>(OdUInt64(n)); }
	bool operator<(int n) const { return operator<(OdUInt64(n)); }

	OdUInt64 operator >>(int n) const
	{
		OdUInt64 res = *this;
		return (res>>=n);
	}
  OdUInt64& operator >>= (int n);
};

//----------------------------------------------------------
//
// OdInt64 inline methods
//
//----------------------------------------------------------
inline OdInt64& OdInt64::operator=(int n)
{
	hi = (n>=0 ? 0 : -1);
  low = OdUInt32(n);
	return *this;
}

inline OdInt64& OdInt64::operator++()
{
	return (*this += 1);
}

inline OdInt64 OdInt64::operator++(int /*dummy*/)
{
  OdInt64 t = *this;
	*this += 1;
  return t;
}

inline OdInt64& OdInt64::operator--()
{
	return (*this += -1);
}

inline bool OdInt64::operator > (const OdInt64& n) const
{
	if (hi == n.hi)
		return low > n.low;
  /** Description:
      else
  */
	return hi > n.hi;
}

inline bool OdInt64::operator < (const OdInt64& n) const
{
	return (!operator>(n) && operator!=(n));
}

inline bool OdInt64::operator!=(int n) const
{
	return (!operator==(n));
}

inline bool OdInt64::operator!=(const OdInt64& n) const
{
	return (hi != n.hi || low != n.low);
}

inline bool OdInt64::operator==(int n) const
{
  return (hi == (n >= 0 ? 0 : -1) && low == OdUInt32(n));
}

inline bool OdInt64::operator==(const OdInt64& n) const
{
	return (hi == n.hi && low == n.low);
}

inline OdInt64 OdInt64::operator-() const
{
	OdInt64 res;
	res.hi = ~hi; res.low =~low;
	return (res += 1);
}

inline OdInt64& OdInt64::operator=(const OdInt64& n)
{
	low = n.low;
	hi = n.hi;
	return *this;
}

#endif /* _INC_ODAINT64_38FF72C90128_INCLUDED */


