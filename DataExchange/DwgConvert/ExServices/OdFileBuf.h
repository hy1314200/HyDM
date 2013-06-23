#ifndef ODFILEBUF_DEFINED
#define ODFILEBUF_DEFINED

#ifdef WIN32

#include "OdStreamBuf.h"
#include "RxObjectImpl.h"
#include "OdString.h"
#include "DbSystemServices.h"

class OdBaseFileBuf;
typedef OdSmartPtr<OdBaseFileBuf> OdBaseFileBufPtr;

class OdBaseFileBuf: public OdRxObjectImpl<OdStreamBuf>
{
protected:
	HANDLE		m_hFile;
	OdString	m_sFileName;
	int			  m_iFileShare;
	bool		  m_bFileWritten;

	OdBaseFileBuf()
	{
		m_hFile = INVALID_HANDLE_VALUE;
		m_iFileShare = 0;
		m_bFileWritten = false;
	}

public:

  virtual void open(const OdChar* szFileName,
		Oda::FileShareMode nShareMode,
		Oda::FileAccessMode nDesiredAccess,
		Oda::FileCreationDisposition nCreationDisposition);
	virtual void open(const wchar_t* wszFileName,
		Oda::FileShareMode nShareMode,
		Oda::FileAccessMode nDesiredAccess,
		Oda::FileCreationDisposition nCreationDisposition);

  virtual void close();

	virtual OdString fileName() { return m_sFileName; }
	virtual OdUInt32 getShareMode() { return OdUInt32(m_iFileShare); }
	virtual OdUInt32 length();
	virtual OdUInt32 seek(OdInt32 offset, OdDb::FilerSeekType whence);
	virtual OdUInt32 tell();
	virtual bool isEof();
	virtual OdUInt8 getByte();
	virtual void getBytes(void* buffer, OdUInt32 nLen);
	virtual void putByte(OdUInt8 val);
	virtual void putBytes(const void* buffer, OdUInt32 nLen);
	virtual void truncate();

	virtual void copyDataTo(OdStreamBuf* pDest, OdUInt32 nSrcStart, OdUInt32 nSrcEnd);
};

class OdRdFileBuf;
typedef OdSmartPtr<OdRdFileBuf> OdRdFileBufPtr;
/////////////////////////////////////////////////////////////
class OdRdFileBuf : public OdBaseFileBuf
{
  OdRdFileBuf(const OdRdFileBuf&);
  OdRdFileBuf& operator = (const OdRdFileBuf&);

protected:
	HANDLE			m_hFileMap;
	LPVOID			m_pFileMap;
	ULARGE_INTEGER	m_ulSize;
	ULARGE_INTEGER	m_ulPos;

  inline bool memBufferUsed() const { return m_pFileMap != 0; }

public:
  OdRdFileBuf(const OdChar *path) 
  { 
		m_hFileMap = INVALID_HANDLE_VALUE;
		m_pFileMap = NULL;
		m_ulSize.QuadPart = 0;
		m_ulPos.QuadPart = 0;

    open(path); 
  }

  OdRdFileBuf(
    const OdChar *path, 
    Oda::FileShareMode shMode, 
    Oda::FileAccessMode nDesiredAccess,
    Oda::FileCreationDisposition nCreationDisposition)
  { 
		m_hFileMap = INVALID_HANDLE_VALUE;
		m_pFileMap = NULL;
		m_ulSize.QuadPart = 0;
		m_ulPos.QuadPart = 0;

    open(path, shMode, nDesiredAccess, nCreationDisposition); 
  }

  OdRdFileBuf();

  ~OdRdFileBuf() { close(); }

  static OdRdFileBufPtr createObject()
  {
    return OdRdFileBufPtr(new OdRdFileBuf(), kOdRxObjAttach);
  }

  static OdRdFileBufPtr createObject(
    const OdChar *szFileName, 
    Oda::FileShareMode shMode = Oda::kShareDenyWrite,
    Oda::FileAccessMode nDesiredAccess = Oda::kFileRead,
		Oda::FileCreationDisposition nCreationDisposition = Oda::kOpenExisting)
  {
    return OdRdFileBufPtr(new OdRdFileBuf(szFileName, shMode, nDesiredAccess, nCreationDisposition), kOdRxObjAttach);
  }

  virtual void open(const OdChar* szFileName,
		Oda::FileShareMode nShareMode = Oda::kShareDenyWrite,
		Oda::FileAccessMode nDesiredAccess = Oda::kFileRead,
		Oda::FileCreationDisposition nCreationDisposition = Oda::kOpenExisting);
	virtual void open(const wchar_t* wszFileName,
		Oda::FileShareMode nShareMode = Oda::kShareDenyWrite,
		Oda::FileAccessMode nDesiredAccess = Oda::kFileRead,
		Oda::FileCreationDisposition nCreationDisposition = Oda::kOpenExisting);

	virtual void close();

	virtual OdUInt32 length();
	virtual OdUInt32 seek(OdInt32 offset, OdDb::FilerSeekType whence);
	virtual OdUInt32 tell();
	virtual bool isEof();
	virtual OdUInt8 getByte();
	virtual void getBytes(void* buffer, OdUInt32 nLen);
	virtual void putByte(OdUInt8 val);
	virtual void putBytes(const void* buffer, OdUInt32 nLen);
	virtual void truncate();

	virtual void copyDataTo(OdStreamBuf* pDest, OdUInt32 nSrcStart, OdUInt32 nSrcEnd);
};

class OdWrFileBuf;
typedef OdSmartPtr<OdWrFileBuf> OdWrFileBufPtr;
/////////////////////////////////////////////////////////////
class OdWrFileBuf : public OdBaseFileBuf
{
  OdWrFileBuf(const OdWrFileBuf&);
  OdWrFileBuf& operator = (const OdWrFileBuf&);

public:

  OdWrFileBuf(const OdChar *path) { open(path); }
  OdWrFileBuf(
    const OdChar *path, 
    Oda::FileShareMode shMode,
    Oda::FileAccessMode nDesiredAccess,
    Oda::FileCreationDisposition nCreationDisposition) 
  { 
    open(path, shMode, nDesiredAccess, nCreationDisposition); 
  }

  OdWrFileBuf();

  ~OdWrFileBuf() { close(); }

  static OdWrFileBufPtr createObject()
  {
    return OdWrFileBufPtr(new OdWrFileBuf(), kOdRxObjAttach);
  }
  static OdWrFileBufPtr createObject(const OdChar* szFileName,
		Oda::FileShareMode nShareMode = Oda::kShareDenyNo,
		Oda::FileAccessMode nDesiredAccess = Oda::kFileWrite,
		Oda::FileCreationDisposition nCreationDisposition = Oda::kCreateAlways)
  {
    return OdWrFileBufPtr(new OdWrFileBuf(szFileName, nShareMode, nDesiredAccess, nCreationDisposition), kOdRxObjAttach);
  }

  virtual void open(const OdChar* szFileName,
		Oda::FileShareMode nShareMode = Oda::kShareDenyNo,
		Oda::FileAccessMode nDesiredAccess = Oda::kFileWrite,
		Oda::FileCreationDisposition nCreationDisposition = Oda::kCreateAlways);
	virtual void open(const wchar_t* wszFileName,
		Oda::FileShareMode nShareMode = Oda::kShareDenyNo,
		Oda::FileAccessMode nDesiredAccess = Oda::kFileWrite,
		Oda::FileCreationDisposition nCreationDisposition = Oda::kCreateAlways);
};

#else // #ifdef WIN32

#include <stdio.h>
#include "DbSystemServices.h"
#include "OdString.h"
#include "RxObjectImpl.h"

////////////////////////////////////////////////////////////
#define ERR_VAL ((OdUInt32)-1)
class OdBaseFileBuf : public OdRxObjectImpl<OdStreamBuf>
{
  OdBaseFileBuf(const OdBaseFileBuf&);
  OdBaseFileBuf& operator = (const OdBaseFileBuf&);

public:
          OdBaseFileBuf();
  virtual ~OdBaseFileBuf()  {}

  virtual void      open(
    const OdChar* path, 
    Oda::FileShareMode shMode/* = Oda::kShareDenyNo*/, 
    Oda::FileAccessMode nDesiredAccess/* = Oda::kFileRead*/, 
    Oda::FileCreationDisposition nCreationDisposition/* = Oda::kOpenExisting*/) = 0;

  virtual void      close();
  virtual OdString  fileName() { return m_FileName; }

protected:
  FILE *              m_fp;
  OdString            m_FileName;
  OdUInt32            m_length;
  Oda::FileShareMode  m_shMode;

  void open(const OdChar *path, const char * mode);
};

class OdWrFileBuf;
typedef OdSmartPtr<OdWrFileBuf> OdWrFileBufPtr;
/////////////////////////////////////////////////////////////
class OdWrFileBuf : public OdBaseFileBuf
{
  OdWrFileBuf(const OdWrFileBuf&);
  OdWrFileBuf& operator = (const OdWrFileBuf&);

  OdUInt32  m_position;

public:
  OdWrFileBuf(const OdChar *path) { open(path); }
  OdWrFileBuf(const OdChar *path, Oda::FileShareMode shMode) { open(path, shMode); }
  OdWrFileBuf();
  ~OdWrFileBuf();
  static OdWrFileBufPtr createObject()
  {
    return OdWrFileBufPtr(new OdWrFileBuf(), kOdRxObjAttach);
  }
  static OdWrFileBufPtr createObject(const OdChar *path, Oda::FileShareMode shMode = Oda::kShareDenyNo)
  {
    return OdWrFileBufPtr(new OdWrFileBuf(path, shMode), kOdRxObjAttach);
  }

  virtual void open(
    const OdChar* path, 
    Oda::FileShareMode shMode = Oda::kShareDenyNo, 
    Oda::FileAccessMode nDesiredAccess = Oda::kFileWrite, 
    Oda::FileCreationDisposition nCreationDisposition = Oda::kCreateAlways);

  virtual void close();

  // OdStreamBuf methods
  virtual OdUInt32  length();
  virtual OdUInt32  seek(OdInt32 offset, OdDb::FilerSeekType whence);
  virtual OdUInt32  tell();
  virtual bool      isEof();
  virtual void      putByte(OdUInt8 val);
  virtual void      putBytes(const void* buffer, OdUInt32 nLen);
  virtual OdUInt32  getShareMode();
};


//////////////////////////////////////////////////////////////////////
#define NUM_BUFFERS 8

class OdRdFileBuf;
typedef OdSmartPtr<OdRdFileBuf> OdRdFileBufPtr;

class OdRdFileBuf : public OdBaseFileBuf
{
  OdRdFileBuf(const OdRdFileBuf&);
  OdRdFileBuf& operator = (const OdRdFileBuf&);

public:
  OdRdFileBuf(const OdChar *path) : m_Counter(0L) { init(); open(path); }
  OdRdFileBuf(const OdChar *path, Oda::FileShareMode shMode) : m_Counter(0L)
  { 
    init();
    open(path, shMode); 
  }
  OdRdFileBuf();
  static OdRdFileBufPtr createObject()
  {
    return OdRdFileBufPtr(new OdRdFileBuf(), kOdRxObjAttach);
  }
  static OdRdFileBufPtr createObject(const OdChar *path, Oda::FileShareMode shMode = Oda::kShareDenyNo)
  {
    return OdRdFileBufPtr(new OdRdFileBuf(path, shMode), kOdRxObjAttach);
  }
  ~OdRdFileBuf();

  virtual void open(
    const OdChar* path, 
    Oda::FileShareMode shMode = Oda::kShareDenyNo, 
    Oda::FileAccessMode nDesiredAccess = Oda::kFileRead, 
    Oda::FileCreationDisposition nCreationDisposition = Oda::kOpenExisting);

  virtual void close();

  virtual OdUInt32  length();
  virtual OdUInt32  seek(OdInt32 offset, OdDb::FilerSeekType whence);
  virtual OdUInt32  tell();
  virtual bool      isEof();
  virtual OdUInt8   getByte();
  virtual void      getBytes(void* buffer, OdUInt32 nLen);
  virtual OdUInt32  getShareMode();

protected:
  struct blockstru
  {
    OdUInt8*  buf;        /* this buffer */
    OdUInt32  startaddr;  /* address from which it came in the file */
    int       validbytes; /* number of valid bytes it holds */
    OdInt32   counter;    /* least recently used counter */
  };

  OdUInt32  m_PhysFilePos; /* where the file pointer is */
  OdUInt32  m_BufPos;      /* position from which buf was filled */
  int       m_BytesLeft;   /* bytes left in buf */
  int       m_BufBytes;    /* valid bytes read into buffer */
  OdUInt8*  m_pNextChar;   /* pointer to next char in buffer */
  OdUInt8*  m_pCurBuf;     /* pointer to the buffer currently being used */
  int       m_UsingBlock;  /* which block is currently in use */
  struct blockstru         /* the data being held */
            m_DataBlock[NUM_BUFFERS];

  static const int m_BufSize; /* size of each read buffer */
  static const int  m_PosMask; /* mask to allow position check */
  OdInt32    m_Counter;

  bool filbuf();
  void init();
};

#endif // #ifdef WIN32

#endif // ODFILEBUF_DEFINED

