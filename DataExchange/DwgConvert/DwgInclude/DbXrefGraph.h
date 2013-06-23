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



#ifndef _ODDBXREFGRAPH_H_INCLUDED_
#define _ODDBXREFGRAPH_H_INCLUDED_

#include "DD_PackPush.h"

#include "DbGraph.h"
#include "DbObjectId.h"
#include "OdString.h"

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum XrefStatus
  {
    kXrfNotAnXref     = 0,  // Not an Xref
    kXrfResolved      = 1,  // Resolved
    kXrfUnloaded      = 2,  // Unloaded
    kXrfUnreferenced  = 3,  // Unreferenced
    kXrfFileNotFound  = 4,  // File Not Found
    kXrfUnresolved    = 5   // Unresolved
  };  
}

class OdDbXrefGraphNode;
typedef OdSmartPtr<OdDbXrefGraphNode> OdDbXrefGraphNodePtr;

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbXrefGraphNode : public OdDbGraphNode
{
protected:
  OdDbXrefGraphNode();
public:

  ODRX_DECLARE_MEMBERS(OdDbXrefGraphNode);

  virtual ~OdDbXrefGraphNode();
  
  OdString name() const;
  OdDbObjectId blockId() const;
  OdDbDatabase* database() const;
  
  void setName(const OdString& sName);
  void setBlockId(OdDbObjectId blockId);
  void setDatabase(OdDbDatabase* pDb);
  
  bool isNested() const;
  
  OdDb::XrefStatus xrefStatus() const;
  void setXrefStatus(OdDb::XrefStatus);
  
private:
  OdString          m_sName;
  OdDbObjectId      m_blockId;
  OdDb::XrefStatus  m_status;
  // OdDbDatabase* uses base class data() member
};


/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbXrefGraph : public OdDbGraph
{
  OdDbXrefGraph(const OdDbXrefGraph&);
  OdDbXrefGraph& operator =(const OdDbXrefGraph&);
public:
  OdDbXrefGraph() {}
  virtual ~OdDbXrefGraph();
  
  OdDbXrefGraphNode* xrefNode(const OdString& sName) const;
  OdDbXrefGraphNode* xrefNode(OdDbObjectId btrId) const;
  OdDbXrefGraphNode* xrefNode(const OdDbDatabase* pDb) const;
  
  OdDbXrefGraphNode* xrefNode(int idx) const;
  OdDbXrefGraphNode* hostDwg() const;
  
  bool markUnresolvedTrees();
  
  // cycle detection
  
  // virtual bool findCycles(OdDbGraphNode* pStart = 0);

  static void getFrom(OdDbDatabase* pDb, OdDbXrefGraph& out, bool bIncludeGhosts = false);
};

inline OdDbXrefGraphNode::OdDbXrefGraphNode() : m_status(OdDb::kXrfNotAnXref) {}

inline OdString OdDbXrefGraphNode::name() const { return m_sName; }
inline void OdDbXrefGraphNode::setName(const OdString& sName) { m_sName = sName; }

inline OdDbObjectId OdDbXrefGraphNode::blockId() const { return m_blockId; }
inline void OdDbXrefGraphNode::setBlockId(OdDbObjectId id) { m_blockId = id; }

inline OdDbDatabase* OdDbXrefGraphNode::database() const { return (OdDbDatabase*)data(); }
inline void OdDbXrefGraphNode::setDatabase(OdDbDatabase* pDb) { setData(pDb); }

inline bool OdDbXrefGraphNode::isNested() const
{ return !isMarkedAs(kFirstLevel); }

inline OdDb::XrefStatus OdDbXrefGraphNode::xrefStatus() const { return m_status; }
inline void OdDbXrefGraphNode::setXrefStatus(OdDb::XrefStatus stat) { m_status = stat; }

inline OdDbXrefGraphNode* OdDbXrefGraph::xrefNode(int idx) const
{ return(OdDbXrefGraphNode*)node(idx); }

inline OdDbXrefGraphNode* OdDbXrefGraph::hostDwg() const
{ return(OdDbXrefGraphNode*)rootNode(); }

#include "DD_PackPop.h"

#endif // _ODDBXREFGRAPH_H_INCLUDED_



