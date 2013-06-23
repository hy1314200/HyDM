#ifndef _OdErrorContextWithId_h_Included_
#define _OdErrorContextWithId_h_Included_

#include "OdErrorContext.h"

/** Description:

    {group:Other_Classes}
*/
class OdErrorContextWithId : public OdErrorContext
{
  OdDbStub*   m_id;
  OdDbHandle  m_handle;
  OdResult    m_res;
protected:
  OdErrorContextWithId() {}
public:
  OdSmartPtr<OdErrorContext> init(OdResult res, const OdDbObjectId& id)
  {
    m_id = id;
    m_handle = id.getHandle();
    m_res = res;
    return this;
  }
  OdString description() const
  {
    OdString s = odSystemServices()->formatMessage((OdMessage)m_res);
    s += " object: ";
    s += odDbGetHandleName(m_handle);
    return s;
  }
  OdResult code() const { return m_res; }
  OdDbHandle handle() const { return m_handle; }

  // An exception handler is responsible for using the object id.
  // It must be sure that id is valid at the time of handling the exception.
  OdDbObjectId id() const { return m_id; }
};

#endif
