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

#ifndef _ODDB_EVALWATCHERS_H_
#define _ODDB_EVALWATCHERS_H_

#include "RxObject.h"
#include "RxModule.h"
#include "DbEntity.h"
#include "DbHatch.h"

/** Description:
    This class is the base Protocol Extension class, used for catching 
    evaluation requests from associated objects.

    Library:
    Db

    Remarks:
    This class can be used for OdDbHatch, OdDbDimAssoc, OdDbLeader and OdDbDimension classes.

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbEvalWatcherPE : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbEvalWatcherPE);

  /**
    Description:
    This function is called when an object receives a "modified" notification 
    from an associated object.

    Arguments:
      pObj (I) Pointer to the object that received the notification.
      pAssocObj (I) Pointer to the object that is being closed after being modified.
  */
  virtual void modified(OdDbObject* pObj, const OdDbObject* pAssocObj) = 0;
};

typedef OdSmartPtr<OdDbEvalWatcherPE> OdDbEvalWatcherPEPtr;


/** Description:
    This class contains a default implementation of methods for updating 
    an associative OdDbHatch.

    Library:
    Db
    
    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbHatchWatcherPE : public OdDbEvalWatcherPE
{
public:
  /**
    Remarks:
    The default implementation of this function does nothing.
    This function can be overridden in custom classes.
  */
  virtual void modified(OdDbObject* pObj, const OdDbObject* pAssocObj);

  /**
    Description:
    This function is called when a hatch is modified.

    Arguments:
      pHatch (I) Pointer to the modified hatch.

    Remarks:
    The default implementation of this function does nothing.
    This function can be overridden in custom classes.
  */
  virtual void modifiedItself(OdDbHatch* pHatch);

  /** 
    Description:
    Updates the boundary of an associative hatch.
    
    Arguments:
      pHatch (I) The OdDbHatch object.
      assocObjIds (I) Array of associative OdDbEntity object IDs that were modified.
  */
  virtual void evaluate(OdDbHatch* pHatch, const OdDbObjectIdArray& assocObjIds);

  /** Description:
    Get a loop from the specified IDs, for the specified hatch entity.

    Arguments:
      pHatch (I) The OdDbHatch object.
      loopType (I/O) Type of loop being updated.
      objIds (I) Array of OdDbEntity object IDs that comprise the loop.
      edges (I/O) Array of OdGeCurve pointers to the edges that comprise the loop.
  */
  virtual void getLoopFromIds(const OdDbHatch* pHatch, 
    OdUInt32& loopType, 
    OdDbObjectIdArray& objIds, 
    EdgeArray& edges);
};

typedef OdSmartPtr<OdDbHatchWatcherPE> OdDbHatchWatcherPEPtr;


/** Description:
    This class contains a default implementation of methods
    for updating an OdDbLeader object with an associative annotation.

    Library:
    Db
    
    Remarks:
    This protocol extension is added to OdDbLeader class by DDT while initializing.

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbLeaderWatcherPE : public OdDbEvalWatcherPE
{
public:
  /**
    Remarks:
    This default implementation evaluates the leader immediately.
    This function can be overridden in custom classes.
  */
  virtual void modified(OdDbObject* pObj, const OdDbObject* pAssocObj);
};

typedef OdSmartPtr<OdDbLeaderWatcherPE> OdDbLeaderWatcherPEPtr;


/** Description:
    This class contains a default implementation of methods
    for updating an OdDbDimension object if OdDimStyleTableRecod is modified.

    Library:
    Db
    
    Remarks:
    This protocol extension is added to OdDbDimension class by DDT while initializing.

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbDimensionWatcherPE : public OdDbEvalWatcherPE
{
public:
  /**
    Remarks:
    This default implementation recompute the dimension immediately.
    This function can be overridden in custom classes.
  */
  virtual void modified(OdDbObject* pObj, const OdDbObject* pDimStyle);
};

typedef OdSmartPtr<OdDbDimensionWatcherPE> OdDbDimensionWatcherPEPtr;

#endif // _ODDB_EVALWATCHERS_H_
