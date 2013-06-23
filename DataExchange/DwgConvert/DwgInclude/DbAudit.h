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



#ifndef _OD_DB_AUDIT_
#define _OD_DB_AUDIT_

#include "DD_PackPush.h"

#include "OdString.h"
#include "CmColor.h"

class OdDbAuditInfoImpl;
class OdDbRecover;
class OdDbObject;
class OdDbObjectId;
class OdDbDatabase;
class OdDbAuditInfo;
class OdDbHostAppServices;

TOOLKIT_EXPORT OdString odDbGetObjectName(const OdDbObject* pObj);
TOOLKIT_EXPORT OdString odDbGetObjectIdName(const OdDbObjectId& id);
TOOLKIT_EXPORT OdString odDbGetHandleName(const OdDbHandle& handle);
TOOLKIT_EXPORT OdString odDbGenerateName(const OdDbObjectId& id);
TOOLKIT_EXPORT OdString odDbGenerateName(OdUInt32 i);
TOOLKIT_EXPORT bool     odDbAuditColorIndex(OdInt16& colorIndex, OdDbAuditInfo* pAuditInfo, OdDbHostAppServices* pHostApp = 0);
TOOLKIT_EXPORT bool     odDbAuditColor(OdCmColor& color, OdDbAuditInfo* pAuditInfo, OdDbHostAppServices* pHostApp = 0);

/** Description:
  This class tracks audit information during a *database* audit.

  Library:
  Db
  
  {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbAuditInfo
{
  friend class OdDbAuditController;
public:

  enum PrintDest
  {
    kSilent  = 0, // None.
    kCmdLine = 1, // Command line.
    kFile    = 2, // File.
    kBoth    = 3  // Both command line and file.
  };
  struct MsgInfo
  {
    OdString strName;
    OdString strValue; 
    OdString strValidation;
    OdString strDefaultValue; 
    bool     bIsError;
  };

  OdDbAuditInfo();
  
  ~OdDbAuditInfo();

  /** Description:
    Returns true if and only if errors are to be fixed during the
    audit that uses this AuditInfo object.
  */
  bool fixErrors() const;
  
  /** Description:
    Controls the fixing of errors are to be fixed during the
    audit that uses this AuditInfo object.
    
    Arguments:
    fixErrors (I) Fix errors.
  */
  void setFixErrors(
    bool fixErrors);

  /** Description:
    Returns the number of errors reported to this AuditInfo object.
  */
  int numErrors() const;

  /** Description:
    Returns the number of errors reported as fixed to this AuditInfo object.
  */
  int numFixes() const;

  /** Description:
    Increments the *count* of errors reported to this AuditInfo object.
    Arguments:
    increment (I) Increment.
  */
  void errorsFound(
    int count);

  /** Description:
    Increments the *count* of errors reported as fixed to this AuditInfo object.
    Arguments:
    increment (I) Increment.
  */
  void errorsFixed(
    int count);

  /** Description:
    Prints log information about an error detected or fixed during an audit.

    Arguments:
    name(I) Type of erroneous data found. 
    value (I) Value of the bad data.
    validation (I) Reason the data were bad.
    defaultValue (I) Default *value* to which the were set.
    
    Note:
    As implemented, these functions do nothing.  It will be fully implemented in a future *release*.
  */
  virtual void printError(
    const OdChar* name, 
    const OdChar* value, 
    const OdChar* validation, 
    const OdChar* defaultValue);

  /**
    Arguments:
    pObject (I) Pointer to the object which generated the error.
      
    Remarks:
    odDbGetObjectName(pObject) is used to generate the *name*.       
  */
  virtual void printError(
    const OdDbObject* pObject, 
    const OdChar* value, 
    const OdChar* validation, 
    const OdChar* defaultValue);

  /** Description:
    Prints log information about the audit.
    
    Arguments:
    logInfo (I) Log information.
  */
  virtual void printInfo (
    const OdChar* logInfo);

  /** Description:
    Allows fixed objects to specify a regen is required.
  */
  void requestRegen();

  /** Description:
    Resets the number of errors reported to this AuditInfo object.
  */
  void resetNumEntities();
  
  /** Description:
    Increments by one the number of errors reported as processed to this AuditInfo object.
  */
  void incNumEntities();

  /** Description:
    Returns the number of entities reported as processed to this AuditInfo object.
  */
  int numEntities();

  /** Description:
    Returns the last error information that is intended for printing.
  */
  virtual const MsgInfo& getLastInfo();
  
  /** Description:
    Sets the last error information that is intended for printing.
    Arguments:
    lastInfo (I) Last error information .
  */
  virtual void setLastInfo(
    MsgInfo &lastInfo);

  /*  If the current count of entities being maintained in the instance 
      of OdDbAuditInfo is a multiple of 100, and msg is not NULL, 
      then this function will print the string pointed to by msg 
      followed by the current entity count out to the audit log file.
     void printNumEntities(const char* msg);
  */

  /** Description:
    Sets the destination for log printing.
    Arguments:
    printDest (I) Print destination.

    Remarks:
    printDest will be one of the following:
    
    @table
    Name        Value  Description
    kSilent     0      None.
    kCmdLine    1      Command line.
    kFile       2      File.
    kBoth       3      Both command line and file.
  */
  void setPrintDest(
    PrintDest printDest);

  /** Description:
    Returns the destination for log printing.
 
    Remarks:
    getPrintDest will return one of the following:
    
    @table
    Name        Value  Description
    kSilent     0      None.
    kCmdLine    1      Command line.
    kFile       2      File.
    kBoth       3      Both command line and file.
  */
  PrintDest  getPrintDest();

private:
  OdDbAuditInfoImpl* m_pImpl;
};

#include "DD_PackPop.h"

#endif


