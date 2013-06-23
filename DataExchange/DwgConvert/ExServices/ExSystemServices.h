#ifndef EXSYSTEMSERVICES_DEFINED
#define EXSYSTEMSERVICES_DEFINED

#include "DbSystemServices.h"


class ExSystemServices : public OdDbSystemServices
{
public:
  ExSystemServices();

  OdStreamBufPtr createFile(
    const OdChar* pcFilename,                             // file name
    Oda::FileAccessMode nDesiredAccess = Oda::kFileRead,  // access mode
    Oda::FileShareMode nShareMode = Oda::kShareDenyNo,    // share mode
    Oda::FileCreationDisposition nCreationDisposition = Oda::kOpenExisting);

  bool accessFile(const OdChar* pcFilename, int nMode);

  long getFileCTime(const OdChar* name);                  // Time of creation of file.
  long getFileMTime(const OdChar* name);                  // Time of last modification of file.
  OdInt64 getFileSize(const OdChar* name);

  OdString formatMessage(unsigned int id, va_list* argList = 0);
  
  OdCodePageId systemCodePage() const;
  void setSystemCodePage(OdCodePageId id);

protected:
  OdCodePageId m_CodePageId;
};

#endif	// EXSYSTEMSERVICES_DEFINED

