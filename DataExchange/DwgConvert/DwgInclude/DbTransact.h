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



#ifndef _ODDBTRANSACT_INCLUDED_
#define _ODDBTRANSACT_INCLUDED_

#include "DD_PackPush.h"

class OdDbDatabase;

#include "RxObject.h"


/** Description:
    This class is the base class for custom classes that receive notification
    of DbDatabase transaction events.

    Note:
    The default implementations of all methods in this class do nothing.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbTransactionReactor : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbTransactionReactor);
  
  OdDbTransactionReactor() {}
  /** Description
    Notification function called to indicate a new transaction is about to start.
    
    Remarks:
    Arguments:
    pTransactionManager (I) Pointer to the transaction manager sending the notification.
    
    Remarks:
    numTransactions does not include the transaction about to start.
    
    See Also:
    o  transactionAborted
    o  transactionAboutToAbort
    o  transactionAboutToEnd
    o  transactionEnded
    o  transactionStarted
  */
  virtual void transactionAboutToStart(
    OdDbDatabase* pTransactionManager);
  
  /** Description
    Notification function called to indicate a new transaction has started.
    
    Remarks:
    Arguments:
    pTransactionManager (I) Pointer to the transaction manager sending the notification.
    
    Remarks:
    numTransactions includes the transaction just started.

    See Also:
    o  transactionAborted
    o  transactionAboutToAbort
    o  transactionAboutToEnd
    o  transactionAboutToStart
    o  transactionEnded
  */
  virtual void transactionStarted(
    OdDbDatabase* pTransactionManager);

  /** Description
    Notification function called to indicate a transaction is about to end.
    
    Remarks:
    Arguments:
    pTransactionManager (I) Pointer to the transaction manager sending the notification.
    
    Remarks:
    numTransactions includes the transaction about to end.

    See Also:
    o  transactionAborted
    o  transactionAboutToAbort
    o  transactionAboutToStart
    o  transactionEnded
    o  transactionStarted
  */
  virtual void transactionAboutToEnd(
    OdDbDatabase* pTransactionManager);

  /** Description
    Notification function called to indicate a transaction has ended.
    
    Remarks:
    Arguments:
    pTransactionManager (I) Pointer to the transaction manager sending the notification.
    
    Remarks:
    numTransactions does not include the transaction just ended.
    See Also:
    o  transactionAborted
    o  transactionAboutToAbort
    o  transactionAboutToEnd
    o  transactionAboutToStart
    o  transactionStarted
  */
  virtual void transactionEnded(
    OdDbDatabase* pTransactionManager);

  /** Description
    Notification function called to indicate a transaction is about to be terminated.
    
    Remarks:
    Arguments:
    pTransactionManager (I) Pointer to the transaction manager sending the notification.
    
    Remarks:
    numTransactions includes the transaction about to be terminated.
    See Also:
    o  transactionAborted
    o  transactionAboutToEnd
    o  transactionAboutToStart
    o  transactionEnded
    o  transactionStarted
  */
  virtual void transactionAboutToAbort(
    OdDbDatabase* pTransactionManager);

  /** Description
    Notification function called to indicate a transaction has been terminated.
    
    Remarks:
    Arguments:
    pTransactionManager (I) Pointer to the transaction manager sending the notification.
    
    Remarks:
    numTransactions does not include the transaction just terminated.
    See Also:
    o  transactionAboutToAbort
    o  transactionAboutToEnd
    o  transactionAboutToStart
    o  transactionEnded
    o  transactionStarted
  */
  virtual void transactionAborted(
    OdDbDatabase* pTransactionManager);

  /** Description
    Notification function called to indicate the outermose transaction has ended.
    
    Remarks:
    numTransactions should equal 1.
    Arguments:
    pTransactionManager (I) Pointer to the transaction manager sending the notification.
  */
  virtual void endCalledOnOutermostTransaction(
    OdDbDatabase* pTransactionManager);

};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbTransactionReactor object pointers.
*/
typedef OdSmartPtr<OdDbTransactionReactor> OdDbTransactionReactorPtr;

#include "DD_PackPop.h"

#endif // _ODDBTRANSACT_INCLUDED_


