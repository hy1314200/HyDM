#ifndef _ExStringIO_h_Included_
#define _ExStringIO_h_Included_

#include "ExDbCommandContext.h"

class ExStringIO : public OdEdBaseIO
{
  OdString m_sInput;
  OdSmartPtr<ExStringIO> init(const OdString& sInput);
protected:
  ExStringIO() {}
public:
  static OdSmartPtr<ExStringIO> create(const OdString& sInput);
  OdString getString(bool bAllowSpaces);
  void putString(const OdChar* string);
  bool isEof() const { return m_sInput.isEmpty(); }
};

#endif //_ExStringIO_h_Included_
