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

#ifndef _ODFILEDEPMGR_INCLUDED_
#define _ODFILEDEPMGR_INCLUDED_

#include "OdString.h"
#include "RxDictionary.h"

/** Description:

    {group:Other_Classes} 
*/
class TOOLKIT_EXPORT OdFileDependencyInfo : public OdRxObject
{
protected:
  OdFileDependencyInfo();

public:
  ODRX_DECLARE_MEMBERS(OdFileDependencyInfo);
  virtual void copyFrom(const OdRxObject* pOtherObj);
  OdString m_FullFileName;
  OdString m_FileName;
  OdString m_FoundPath;
  OdString m_FingerprintGuid;
  OdString m_VersionGuid;
  OdString m_Feature;
  bool     m_bIsModified;
  bool     m_bAffectsGraphics;
  OdInt32  m_nIndex;
  OdInt32  m_nTimeStamp;
  OdInt32  m_nFileSize;
  OdInt32  m_nReferenceCount;
};

typedef OdSmartPtr<OdFileDependencyInfo> OdFileDependencyInfoPtr;

/** Description:

    {group:Other_Classes} 
*/
class TOOLKIT_EXPORT OdFileDependencyManager : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdFileDependencyManager);

  OdFileDependencyManager() {};

  virtual OdUInt32 createEntry(const OdChar* feature,
                               const OdChar* fullFileName,
                               const bool affectsGraphics = false,
                               const bool noIncrement = false) = 0;

  virtual OdResult getEntry(const OdChar* feature,
                            const OdChar* fullFileName,
                            OdFileDependencyInfoPtr& fileInfo, 
                            const bool useCachedInfo = false) = 0;
  virtual OdResult getEntry(OdUInt32 index,
                            OdFileDependencyInfoPtr& fileInfo, 
                            const bool useCachedInfo = false) = 0;

  virtual OdResult updateEntry(const OdChar* feature,
                               const OdChar* fullFileName) = 0;
  virtual OdResult updateEntry(OdUInt32 index) = 0;

  virtual OdResult eraseEntry(const OdChar* feature,
                              const OdChar* fullFileName,
                              const bool forceRemove = false) = 0;
  virtual OdResult eraseEntry(OdUInt32 index,
                              const bool forceRemove = false) = 0;

  virtual OdUInt32 countEntries() = 0;

  virtual void iteratorInitialize(const OdChar* feature = NULL, 
                                  const bool modifiedOnly = false,
                                  const bool affectsGraphicsOnly = false,
                                  const bool walkXRefTree = false) = 0;

  virtual OdUInt32 iteratorNext() = 0;

  // get feature list for saving
  virtual void getFeatures(OdRxDictionaryPtr& features) = 0;
  // clear entries entered by xref dep list traversal (before saving)
  virtual void clearXRefEntries() = 0;
};

typedef OdSmartPtr<OdFileDependencyManager> OdFileDependencyManagerPtr;

#endif
