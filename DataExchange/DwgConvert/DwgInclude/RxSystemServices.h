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



// (C) Copyright 2001 by Open Dwg Alliance. All rights reserved.
//

#ifndef _ODRXSYSTEMSERVICES_INCLUDED_
#define _ODRXSYSTEMSERVICES_INCLUDED_

#include <stdarg.h>

#include "RxObject.h"
#include "OdStreamBuf.h"
#include "OdCodePage.h"

class OdTimeStamp;
class OdRxModule;
class OdRxDictionary;
typedef OdSmartPtr<OdRxDictionary> OdRxDictionaryPtr;

#include "DD_PackPush.h"

/** Description:

    {group:DD_Namespaces}
*/
namespace Oda
{
  ///////////////// FILE SYSTEM SERVICES //////////////////


  enum FileAccessMode   // Access
  {
    kFileRead    = (OdInt32)0x80000000,// Specifies read access to the object.
                              // Data can be read from the file and the file pointer can be moved.
                              // Combine with kForWrite for read/write access.
    kFileWrite   = 0x40000000 // Specifies write access to the object.
                              // Data can be written to the file and the file pointer can be moved.
                              // Combine with kForRead for read/write access.
  };

  enum FileShareMode
  {
		kShareDenyReadWrite = 0x10, // deny read/write mode
		kShareDenyWrite     = 0x20, // deny write mode
		kShareDenyRead      = 0x30, // deny read mode
		kShareDenyNo        = 0x40  // deny none mode
  };


  enum FileCreationDisposition
  {
    kCreateNew        = 1,  // Creates a new file.
                            // The function fails if the specified file already exists.
    kCreateAlways     = 2,  // Creates a new file.
                            // If the file exists, the function overwrites the file and clears the existing attributes.
    kOpenExisting     = 3,  // Opens the file.
                            // The function fails if the file does not exist.
    kOpenAlways       = 4,  // Opens the file, if it exists.
                            // If the file does not exist,
                            // the function creates the file as if nCreationDisposition were kCreateNew.
    kTruncateExisting = 5   // Opens the file. Once opened, the file is truncated so that its size is zero bytes.
                            // The caller must open the file with at least kForWrite access.
                            // The function fails if the file does not exist.
  };
}

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError_FileException : public OdError
{
public:
  OdError_FileException(OdResult error, const OdString& fileName);
  OdString getFileName() const;
};

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError_FileNotFound : public OdError_FileException
{
public:
  OdError_FileNotFound(const OdString& fileName);
};

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError_CantOpenFile : public OdError_FileException
{
public:
  OdError_CantOpenFile(const OdString& fileName);
};

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError_FileWriteError : public OdError_FileException
{
public:
  OdError_FileWriteError(const OdString& fileName);
};

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError_InvalidIndex : public OdError
{
public:
  OdError_InvalidIndex();
};

/** Description:

    {group:Error_Classes}
*/
class FIRSTDLL_EXPORT OdError_InvalidKey: public OdError
{
public:
  OdError_InvalidKey();
};


/** Description:
    Class that can be derived by clients to provide platform specific operations that are
    required by DWGdirect.  
    
    Remarks:
    These operations include file name resolution, load/save
    progress monitoring, etc.

    {group:OdRx_Classes}
*/
class FIRSTDLL_EXPORT OdRxSystemServices : public OdRxObject
{
public:
  ///////////////// FILE SYSTEM SERVICES //////////////////

  /** Description:
      Creates or opens the file.
  */
  virtual OdStreamBufPtr createFile(
    const OdChar* pcFilename,                     // file name
    Oda::FileAccessMode nDesiredAccess = Oda::kFileRead,           // access mode
    Oda::FileShareMode  nShareMode = Oda::kShareDenyNo,                           // share mode
    Oda::FileCreationDisposition nCreationDisposition = Oda::kOpenExisting) = 0;

  /** Description:
      Returns true if the file can be accessed in the specified mode, false otherwise.

      Arguments:
      pcFilename Path of file to be accessed.
      nAccess Mode in which to access the file, one of: 0 - existence, Oda::kFileRead, Oda::kFileWrite
  */
  virtual bool accessFile(const OdChar* pcFilename, int nAccess) = 0;

  // following functions are implemented as "stat" on most platforms
  //
  // file creation time (stat::st_ctime)
  virtual long getFileCTime(const OdChar* name) = 0;
  // file modification time (stat::st_mtime)
  virtual long getFileMTime(const OdChar* name) = 0;
  // file size (stat::st_size)
  virtual OdInt64 getFileSize(const OdChar* name) = 0;

  virtual OdString formatMessage(unsigned int id, va_list* = 0) = 0;
  virtual OdString formatMsg(unsigned int id, ...);

  virtual OdCodePageId systemCodePage() const = 0;
  virtual OdString createGuid();

  /** Description:
      Performs physical (platform dependent) loading of specified module.
      Returns pointer to abstract (platform independent) module object
      which represents module functionality in DWGdirect.

      Arguments:
      szModuleFileName (I) module file name to load.
  */
  virtual OdRxModule* loadModule(const OdChar* szModuleFileName, bool bSilent);

  /** Description:
      Performs physical (platform dependent) unloading of specified module.

      Arguments:
      pModuleObj (I) module object to be unloaded.
  */
  virtual void unloadModule(OdRxModule* pModuleObj);

  /** Description:
      Performs user-defined mapping between application name and module file name.

      Arguments:
      szApplicationName (I) DRX application name to load.
  */
  virtual OdString findModule(const OdChar* szApplicationName);

  /** Description:

      Arguments:
  */
  virtual void warning(const OdString& sMsg);
};

typedef OdSmartPtr<OdRxSystemServices> OdRxSystemServicesPtr;

FIRSTDLL_EXPORT OdRxSystemServices* odrxSystemServices();

#endif // #ifndef _ODRXSYSTEMSERVICES_INCLUDED_

