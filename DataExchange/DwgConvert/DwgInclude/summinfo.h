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



#ifndef _SUMMINFO_H_
#define _SUMMINFO_H_

#include "RxObject.h"
#include "OdString.h"
#include "DbDatabase.h"

#include "DD_PackPush.h"

/** Description:
    This class encapulates a set of character strings containing 
    additional information for an OdDbDatabase as Summary Information.

    Remarks:
    In addition to the predefined fields, you add create any number of custom fields to the Summary Information.
    Library: Db
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDatabaseSummaryInfo : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbDatabaseSummaryInfo);

  OdDbDatabaseSummaryInfo();
  
  /** Description:
    Returns the value of the Title field of the Summary Information.
  */
  virtual OdString getTitle() const = 0;

  /** Description:
    Sets the value of the Title field of the Summary Information.
    Arguments:
    title (I) Title.
  */
  virtual void setTitle(
    const OdString& title) = 0;
  
  /** Description:
    Returns the value of the Subject field of the Summary Information.
  */
  virtual OdString getSubject() const = 0;

  /** Description:
    Sets the value of the Subject field of the Summary Information.
    Arguments:
    subject (I) Subject.
  */
  virtual void setSubject(
    const OdString& subject) = 0;
  
  /** Description:
    Returns the value of the Author field of the Summary Information.
  */
  virtual OdString getAuthor() const = 0;

  /** Description:
    Sets the value of the Author field of the Summary Information.
    Arguments:
    author (I) Author.
  */
  virtual void setAuthor(
    const OdString& author) = 0;

  /** Description:
    Returns the value of the Keywords field of the Summary Information.
    
    Remarks:
    Keywords are delimited by spaces.
  */
  virtual OdString getKeywords() const = 0;

  /** Description:
    Sets the value of the Keywords field of the Summary Information.
    Arguments:
    keywords (I) Keywords.
    Remarks:
    Keywords are delimited by spaces.
  */
  virtual void setKeywords(
    const OdString& keywords) = 0;

  /** Description:
    Returns the value of the Comments field of the Summary Information.
  */
  virtual OdString getComments() const = 0;

  /** Description:
    Sets the value of the Comments field of the Summary Information.
    Arguments:
    comments (I) Comments.
  */
  virtual void setComments(
    const OdString& comments) = 0;


  /** Description:
    Returns the value of the LastSavedBy field of the Summary Information.
  */
  virtual OdString getLastSavedBy() const = 0;

  /** Description:
    Sets the value of the LastSavedBy field of the Summary Information.
    Arguments:
    lastSavedBy (I) Last Saved By.
  */
  virtual void setLastSavedBy(
    const OdString& lastSavedBy) = 0;

  /** Description:
    Returns the value of the RevisionNumber field of the Summary Information.
    
    Note:
    The RevisionNumber field is returned as a string.
  */
  virtual OdString getRevisionNumber() const = 0;

  /** Description:
    Sets the value of the RevisionNumber field of the Summary Information.
    Arguments:
    revisionNumber (I) Revision number.
    Note:
    The RevisionNumber field is set as a string.
  */
  virtual void setRevisionNumber(
    const OdString& revisionNumber) = 0;
    
  /** Description:
    Returns the value of the HyperlinkBase field of the Summary Information.
  */
  virtual OdString getHyperlinkBase() const = 0;

  /** Description:
    Sets the value of the HyperlinkBase field of the Summary Information.
    Arguments:
    hyperlinkBase (I) HyperlinkBase.
  */
  virtual void setHyperlinkBase(
    const OdString& hyperlinkBase) = 0;
  /** Description:
    Returns the number of custom fields in the Summary Information  
  */
  virtual int numCustomInfo() const = 0;
  
  /** Description:
    Appends a custom field to the Summary Information.
    Arguments:
    key (I) Name of the custom field.
    value (I) Value of the custom field.

  */
  virtual void addCustomSummaryInfo(
    const OdString& key, 
    const OdString& value ) = 0;
  
  /** Description:
    Deletes the specified custom field from the Summary Information.
    Arguments:
    index (I) Index of the field to delete [1..numCustomInfo()].
    key (I) Name of the field.
  */
  virtual void deleteCustomSummaryInfo(
    int index) = 0;
  virtual bool deleteCustomSummaryInfo(
    const OdString& key) = 0;
    
  /** Description:
    Returns the value of the specified custom field of the Summary Information.
    Arguments:
    index (I) Index of the field to retrieve [1..numCustomInfo()].
    key (O) Receives the name.
    value (O) Receives the value.
  */  
  virtual void getCustomSummaryInfo(
    int index, 
    OdString& key, 
    OdString& value) const = 0;
  
  /** Description:
    Sets the value of the specified custom field of the Summary Information.
    Arguments:
    index (I) Index to of the field set [1..numCustomInfo()].
    key (I) Name of the field.
    value (I) Value for the field.
  */
  virtual void setCustomSummaryInfo(
    int index, const OdString& key, 
    const OdString& value) = 0;
  
  /**
    Arguments:
    customInfoKey (I) Name of the field to retrieve.
  */
  virtual bool getCustomSummaryInfo(
    const OdString& customInfoKey, 
    OdString& value ) const = 0;
  
  /**
    Arguments:
    customInfoKey (I) Name of the the field to set.
  */
  virtual void setCustomSummaryInfo(
    const OdString& customInfoKey, 
    const OdString& value) = 0;
  
  /** Description:
    Returns a pointer to the OdDbDatabase associated with this Summary Information.
  */
  virtual OdDbDatabase* database() const = 0; 
  
  /** Description:
    Sets the OdDbDatabase associated with this Summary Information.
    Arguments:
    pDb (I) Pointer to the *database*.
  */
  virtual void setDatabase(
    OdDbDatabase *pDb) = 0;
};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbDatabaseSummaryInfo object pointers.
*/
typedef OdSmartPtr<OdDbDatabaseSummaryInfo> OdDbDatabaseSummaryInfoPtr;

/** Description:
    This class encapulates a set of character strings containing 
    describing the OpenDWG library.

    Remarks:

    Library: Db
    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbLibraryInfo
{
public:
  /** Description:
    Returns the Library Name (DWGdirect).
  */
  virtual const OdChar* getLibName() const = 0;
  /** Description:
    Returns the Library Version (#.##.##).
  */
  virtual const OdChar* getLibVersion() const = 0;
  
  /** Description:
    Returns the Company Name (Open Design Alliance Inc. ("Open Design")).
  */
  virtual const OdChar* getCompanyName() const = 0;
  /** Description:
    Returns the Copyright Message (Copyright © yyyy, Open Design Alliance Inc. ("Open Design")).
  */
  virtual const OdChar* getCopyright() const = 0;
};

TOOLKIT_EXPORT OdDbLibraryInfo*           oddbGetLibraryInfo(void);
TOOLKIT_EXPORT OdDbDatabaseSummaryInfoPtr oddbGetSummaryInfo(OdDbDatabase* pDb);
TOOLKIT_EXPORT void                       oddbPutSummaryInfo(const OdDbDatabaseSummaryInfo* pInfo);


#include "DD_PackPop.h"

#endif // _SUMMINFO_H_

