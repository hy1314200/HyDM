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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef __OD_GI_METAFILER__
#define __OD_GI_METAFILER__

#include "Gi/GiConveyorNode.h"
#include "Ge/GeDoubleArray.h"

class OdGiDeviation;

#include "DD_PackPush.h"

/** Description:

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiGeometryMetafile : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiGeometryMetafile);
  
  class Record
  {
  protected:
    Record* m_pTail;
  public:
    ODRX_HEAP_OPERATORS();

    Record()
      : m_pTail(0)
    {}

    virtual ~Record() {}

    Record* tail()
    {
      return m_pTail;
    }

    const Record* tail() const
    {
      return m_pTail;
    }

    void setTail(Record* pTail)
    {
      m_pTail = pTail;
    }

    inline void deleteList()
    {
      Record* pCurr = this;
      while(pCurr)
      {
        Record* pTail = pCurr->m_pTail;
        delete pCurr;
        pCurr = pTail;
      }
    }

    virtual void play(OdGiConveyorGeometry* pGeom, OdGiConveyorContext* pCtx) const = 0;
  };

private:
  class Head : public Record
  {
  public:
    void play(OdGiConveyorGeometry*, OdGiConveyorContext*) const {}
    void destroy() { if(m_pTail) m_pTail->deleteList(); m_pTail = 0; }
  }
                        m_head;
  Record*               m_pLast;

public:

  OdGiGeometryMetafile();
  virtual ~OdGiGeometryMetafile();

  void clear();
  void saveTraits(const OdGiSubEntityTraitsData& entTraits);
  void play(OdGiConveyorGeometry* pGeom, OdGiConveyorContext* pCtx) const;
  void add(Record* pRec)
  {
    m_pLast->setTail(pRec);
    m_pLast = pRec;
  }
};

typedef OdSmartPtr<OdGiGeometryMetafile> OdGiGeometryMetafilePtr;

/** Description:

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiMetafiler : public OdGiConveyorNode
{
public:
  ODRX_DECLARE_MEMBERS(OdGiMetafiler);
  
  virtual void setMetafile(OdGiGeometryMetafile* pMetafile) = 0;
  virtual OdGiGeometryMetafile* metafile() = 0;

  /**
    Sets max deviation for curve tesselation.
  */
  virtual void setDeviation(const OdGeDoubleArray& deviations) = 0;

  /**
    Sets deviation object to obtain max deviation for curve tesselation.
  */
  virtual void setDeviation(const OdGiDeviation* pDeviation) = 0;

  /**
    Sets the draw context object (to access to traits, etc).
  */
  virtual void setDrawContext(OdGiConveyorContext* pDrawCtx) = 0;
};

typedef OdSmartPtr<OdGiMetafiler> OdGiMetafilerPtr;

#include "DD_PackPop.h"

#endif //#ifndef __OD_GI_METAFILER__
