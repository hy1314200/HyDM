#ifndef _EX_HOSTAPPSERVICES_H_
#define _EX_HOSTAPPSERVICES_H_

#include "DbHostAppServices.h"
#include "HatchPatternManager.h"
#include "StaticRxObject.h"
#include "DbDatabaseReactor.h"
#define STL_USING_MAP
#include "OdaSTL.h"

class ExHostAppServices : public OdDbHostAppServices, public OdDbHostAppProgressMeter
{
  // MKU 06/17/05 - This optimization is obsolete. But it can be reason of bug 
  //                  when old database is found by name instead of creationg anew.
  /*
  class DwgCollection : public OdStaticRxObject<OdDbDatabaseReactor>
  {
    std::map<OdString, OdDbDatabase*> m_loadedDwgs;
    void goodbye(const OdDbDatabase* pDb);
  public:
    OdDbDatabase* lookUp(const OdString& keyFileName);
    void add(OdDbDatabase* pDb);
  }         m_dwgCollection;
  */
  OdString  m_Prefix;
  long      m_MeterLimit;
  long      m_MeterCurrent;
  long      m_MeterOld;
  bool      m_disableOutput;

  OdHatchPatternManagerPtr m_patternManager;
public:
  ExHostAppServices();

  OdDbHostAppProgressMeter* newProgressMeter();

  void releaseProgressMeter(OdDbHostAppProgressMeter* pMeter);

  void warning(const OdString& ) { }

  DD_USING(OdDbHostAppServices::warning); 

	// OdDbHostAppProgressMeter functions
  void start(const char* displayString = NULL);

  void stop();

  void meterProgress();

  void setLimit(int max);

  bool ttfFileNameByDescriptor(const OdTtfDescriptor& descr, OdString& fileName);

  void disableOutput(bool bDisable) { m_disableOutput = bDisable; }

  void setPrefix(const OdString& prefix) { m_Prefix = prefix; }

  OdHatchPatternManager* patternManager();

  OdDbDatabasePtr readFile(const OdChar* fileName,
    bool bAllowCPConversion = false,
    bool bPartial = false,
    Oda::FileShareMode shmode = Oda::kShareDenyNo,
    const OdPassword& password = OdPassword());

  OdGsDevicePtr gsBitmapDevice();


  DD_USING(OdDbHostAppServices::readFile);
};

#endif

