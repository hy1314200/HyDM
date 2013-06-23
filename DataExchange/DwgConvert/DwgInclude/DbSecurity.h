#ifndef _ODDBSECURITY_INCLUDED_
#define _ODDBSECURITY_INCLUDED_

#include "RxObject.h"
#include "OdArray.h"
#include "DbExport.h"
#include "OdWString.h"

enum
{
  SECURITYPARAMS_ENCRYPT_DATA     = 0x00000001,
  SECURITYPARAMS_ENCRYPT_PROPS    = 0x00000002,

  SECURITYPARAMS_SIGN_DATA        = 0x00000010,
  SECURITYPARAMS_ADD_TIMESTAMP    = 0x00000020,

  SECURITYPARAMS_ALGID_RC4        = 0x00006801
};


typedef OdWString OdPassword;

/** Description:

    {group:Other_Classes}
*/
class OdSecurityParams
{
public:
  OdSecurityParams()
    : nFlags(0)
    , nProvType(0)
    , nAlgId (SECURITYPARAMS_ALGID_RC4)
    , nKeyLength(40)
  {}

  OdUInt32    nFlags;
  OdPassword  password;
  OdUInt32    nProvType;
  OdWString   provName;
  OdUInt32    nAlgId;
  OdUInt32    nKeyLength;

  // data relevant to digital signatures
  OdWString   sCertSubject;
  OdWString   sCertIssuer;
  OdWString   sCertSerialNum;
  OdWString   sComment;
  OdWString   sTimeServer;

};

/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdCrypt : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdCrypt);

  virtual bool initialize(const OdSecurityParams& secParams) = 0;

  virtual bool encryptData(OdUInt8* buffer, OdUInt32 nBufferSize) = 0;

  virtual bool decryptData(OdUInt8* buffer, OdUInt32 nBufferSize) = 0;
};

typedef OdSmartPtr<OdCrypt> OdCryptPtr;

/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdPwdIterator : public OdRxObject
{
public:
  virtual bool done() const = 0;
  virtual void next() = 0;
  virtual void get(OdPassword& pwd) const = 0;
};

typedef OdSmartPtr<OdPwdIterator> OdPwdIteratorPtr;

/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdPwdCache : public OdRxObject
{
public:
  virtual void add(const OdPassword& pwd) = 0;
  virtual OdPwdIteratorPtr newIterator() = 0;
};

typedef OdSmartPtr<OdPwdCache> OdPwdCachePtr;

#endif  // _ODDBSECURITY_INCLUDED_
