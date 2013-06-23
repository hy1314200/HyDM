#include "OdaCommon.h"
#include "ExStringIO.h"

OdSmartPtr<ExStringIO> ExStringIO::create(const OdString& sInput)
{
  return OdRxObjectImpl<ExStringIO>::createObject()->init(sInput);
}

OdString ExStringIO::getString(bool bAllowSpaces)
{
  ODA_ASSERT(!isEof());
  OdString res;
  int n = m_sInput.findOneOf(bAllowSpaces ? "\r\n" : " \t\r\n");
  if(n > -1)
  {
    res = m_sInput.left(n);
    m_sInput = m_sInput.right(m_sInput.getLength() - n - 1);
  }
  else
  {
    res = m_sInput;
    m_sInput.empty();
  }
  return res;
}

void ExStringIO::putString(const OdChar* )
{
}
OdSmartPtr<ExStringIO> ExStringIO::init(const OdString& sInput)
{
  m_sInput = sInput;
  m_sInput.replace("\r\n","\n");
  return this;
}

