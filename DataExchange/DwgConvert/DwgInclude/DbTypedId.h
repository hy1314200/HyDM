#ifndef _ODDBTYPEDID_INCLUDED_
#define _ODDBTYPEDID_INCLUDED_

#include "DbObjectId.h"


/** Description:

    {group:OdDb_Classes}
*/
class OdDbTypedId : public OdDbObjectId
{
  OdDb::ReferenceType m_rt;

public:
  OdDbTypedId( OdDbObjectId id = 0, OdDb::ReferenceType rt = OdDb::kSoftPointerRef ) 
      : OdDbObjectId(id), m_rt(rt) { }

	OdDb::ReferenceType getRefType() const { return m_rt; }
	void setRefType(OdDb::ReferenceType rt) { m_rt = rt; }
	OdDbTypedId& operator=(const OdDbObjectId& rhs)	{	*((OdDbObjectId*)this) = rhs; return *this; }
};

#endif //_ODDBTYPEDID_INCLUDED_

