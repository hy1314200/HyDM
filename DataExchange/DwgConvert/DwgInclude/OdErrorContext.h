#ifndef _OdErrorContext_h_Included_
#define _OdErrorContext_h_Included_

#include "OdaDefs.h"
#include "RxObjectImpl.h"
#include "OdString.h"

#include "DD_PackPush.h"

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdErrorContext : public OdRxObject
{
protected:
  OdErrorContext* m_pPreviousError;
  OdErrorContext() : m_pPreviousError(0) {}
  OdErrorContext(OdErrorContext* prev);
public:
  virtual ~OdErrorContext();
  virtual OdString description() const = 0;
  virtual OdResult code() const = 0;
  OdString completeDescription() const;
  OdErrorContext* getPreviousError() const { return m_pPreviousError; }
  void setPreviousError(OdErrorContext*);
};

#include "DD_PackPop.h"

#endif
