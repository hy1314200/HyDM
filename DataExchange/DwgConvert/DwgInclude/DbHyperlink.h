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


#ifndef _ODDBHYPERLINK_INCLUDED_
#define _ODDBHYPERLINK_INCLUDED_

#include "DD_PackPush.h"
#include "RxObject.h"
#include "DbObject.h"

/** Description
    Hyperlink Object.

    {group:OdDb_Classes}
*/
class OdDbHyperlink
{
public:
  OdDbHyperlink() {};
  virtual ~OdDbHyperlink(){};
  
  virtual const OdChar * name() const = 0;   
  virtual void setName(const OdChar * cName) = 0; 
  
  virtual const OdChar * description() const = 0;  
  virtual void setDescription(const OdChar * cDescription) = 0;
  
  virtual const OdChar * subLocation() const = 0;  
  virtual void setSubLocation(const OdChar * cSubLocation) = 0;
  
  virtual const OdChar * getDisplayString() const = 0;  
  
  virtual bool isOutermostContainer() const = 0;
  
  virtual int getNestedLevel() const = 0;
};

/** Description
    Hyperlink Collection

    {group:OdDb_Classes}
*/
class OdDbHyperlinkCollection : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbHyperlinkCollection);
  OdDbHyperlinkCollection() {};
  virtual ~OdDbHyperlinkCollection(){}; 
  
  virtual void addHead(const OdChar * sName, const OdChar * sDescription, const OdChar * sSubLocation = 0) = 0;
  virtual void addTail(const OdChar * sName, const OdChar * sDescription, const OdChar * sSubLocation = 0) = 0;
  virtual void addAt(const int nIndex, const OdChar * sName, const OdChar * sDescription, const OdChar * sSubLocation = 0) = 0;
  
  virtual void removeHead() = 0;
  virtual void removeTail() = 0;
  virtual void removeAt(const int nIndex) = 0;
  
  virtual int count() const = 0;
  
  virtual OdDbHyperlink * item(const int nIndex) const = 0;
};

typedef OdSmartPtr<OdDbHyperlinkCollection> OdDbHyperlinkCollectionPtr;


/** Description
    Hyperlink Protocol Extension.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbEntityHyperlinkPE : public OdRxObject
{
public:
  
  OdDbEntityHyperlinkPE(){}
  virtual ~OdDbEntityHyperlinkPE(){}

  ODRX_DECLARE_MEMBERS(OdDbEntityHyperlinkPE);
  
  virtual OdDbHyperlinkCollectionPtr getHyperlinkCollection(
    OdDbObject * pObj, 
    bool bOneOnly = false, 
    bool bIgnoreBlockDefinition = true) = 0;
  
  virtual OdDbHyperlinkCollectionPtr getHyperlinkCollection(
    const OdArray<OdDbObjectId> *& idContainers,
    bool bOneOnly = false,                                               
    bool bIgnoreBlockDefinition = true) = 0;
  
  virtual void setHyperlinkCollection(
    OdDbObject * pObj, 
    OdDbHyperlinkCollection * pcHCL) = 0;
  
  virtual unsigned int getHyperlinkCount(
    OdDbObject * pObj, 
    bool bIgnoreBlockDefinition = true) = 0;
  
  virtual unsigned int getHyperlinkCount(
    const OdArray<OdDbObjectId> *& idContainers, 
    bool bIgnoreBlockDefinition = true) = 0;
  
  
  virtual bool hasHyperlink(
    OdDbObject * pObj, 
    bool bIgnoreBlockDefinition = true) = 0;
  
  
  virtual bool hasHyperlink(
    const OdArray<OdDbObjectId> *& idContainers, 
    bool bIgnoreBlockDefinition = true) = 0;
  
};
typedef OdSmartPtr<OdDbEntityHyperlinkPE> OdDbEntityHyperlinkPEPtr;

#include "DD_PackPop.h"
#endif // _ODDBHYPERLINK_INCLUDED_

