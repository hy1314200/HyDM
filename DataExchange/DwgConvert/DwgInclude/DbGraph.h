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



#ifndef _ODDBGRAPH_H_INCLUDED_
#define _ODDBGRAPH_H_INCLUDED_

#include "DD_PackPush.h"

#include "OdaDefs.h"
#include "RxObject.h"
#include "OdArray.h"

class OdDbGraph;
class OdDbGraphNode;

typedef OdArray<OdDbGraphNode*, OdMemoryAllocator<OdDbGraphNode*> > OdDbGraphNodeArray;

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbGraphNode : public OdRxObject
{
  friend class OdDbGraph;
public:
  OdDbGraphNode();

  ODRX_DECLARE_MEMBERS(OdDbGraphNode);

  virtual ~OdDbGraphNode();
  
  // Enum values used to mark nodes using markAs(), etc.
  enum Flags
  {
    kNone         = 0x00,
    kVisited      = 0x01, // Cycle: detection
    kOutsideRefed = 0x02, // List: cannot Detach
    kSelected     = 0x04, // List: user's selection
    kInList       = 0x08, // List: is on it
    kListAll      = 0x0E, // List: for clear all 
    kFirstLevel   = 0x10, // Read only: has edge from root
    kUnresTree    = 0x20, // In an Unresolved tree
    kAll          = 0x2F  // Note, this does not clear kFirstLevel, which is read-only
  };
  
  void* data() const;
  void setData(void* pData);
  
  int numOut() const;
  int numIn() const;
  
  OdDbGraphNode* in(int) const;
  OdDbGraphNode* out(int) const;
  
  void addRefTo(OdDbGraphNode* pTo);
  void removeRefTo(OdDbGraphNode* pNode);
  void disconnectAll();
  
  OdDbGraph* owner() const;
  
  bool isMarkedAs(OdUInt8 flags) const;
  void markAs(OdUInt8 flags);
  void clear(OdUInt8 flags);
  void markTree(OdUInt8 flags, OdDbGraphNodeArray* pNodeArray = 0);
  
  // Circularity detection methods
  
  int numCycleOut() const;
  int numCycleIn() const;
  
  OdDbGraphNode* cycleIn(int) const;
  OdDbGraphNode* cycleOut(int) const;
  
  OdDbGraphNode* nextCycleNode() const;
  
  bool isCycleNode() const;
  
private:
  void setOwner(OdDbGraph* pGraph);
  
  // Circularity detection 
  friend struct if_leaf_push_to;
  friend struct clear_cycles;
  friend void break_edge(OdDbGraphNode* , OdDbGraphNode* );
  
  void*               m_pData;
  OdUInt8             m_flags;
  OdDbGraphNodeArray  m_outgoing;
  OdDbGraphNodeArray  m_incoming;
  OdDbGraph*          m_pOwner;
  OdDbGraphNodeArray  m_cycleOut;
  OdDbGraphNodeArray  m_cycleIn;
};

typedef OdSmartPtr<OdDbGraphNode> OdDbGraphNodePtr;

/** Description:

    {group:OdDb_Classes}
*/
class OdDbGraphStack
{
public:
  OdDbGraphStack(int initPhysicalLength = 0, int initGrowLength = 8)
    : m_stack(initPhysicalLength, initGrowLength) {}

  ~OdDbGraphStack() {}

  void push(OdDbGraphNode* pNode);
  OdDbGraphNode* pop();
  OdDbGraphNode* top() const;
  bool isEmpty() const;
private:
  OdDbGraphNodeArray  m_stack;
};

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbGraph
{
  friend class OdDbGraphNode;
public:
  OdDbGraph();
  virtual ~OdDbGraph();
  
  OdDbGraphNode* node(int index) const;
  OdDbGraphNode* rootNode() const;
  
  int numNodes() const;
  bool isEmpty() const;
  
  void addNode(OdDbGraphNode* pNode);
  void addEdge(OdDbGraphNode* pFrom, OdDbGraphNode* pTo);
  
  void delNode(OdDbGraphNode* pNode);
  
  void reset();
  void clearAll(OdUInt8 flags);
  
  void getOutgoing(OdDbGraphNodeArray& outgoing);
  
  // Cycle detection
  
  virtual bool findCycles(OdDbGraphNode* pStart = 0);
  void breakCycleEdge(OdDbGraphNode* pFrom, OdDbGraphNode* pTo);
  
protected:
  void clearAllCycles();
private:
  
  // These are currently not supported
  OdDbGraph(const OdDbGraph&);
  OdDbGraph& operator =(const OdDbGraph&);
  
  OdDbGraphNodeArray m_nodes;
  
  // Cycle detection
  void removeLeaves(OdDbGraphStack& stack);
  void setDirty();
  bool isDirty() const { return m_bDirty; }
  bool m_bDirty;
  OdDbGraphNodeArray::size_type m_nNonCycleNodes;
};


// OdDbGraphNode inlines ...

inline OdDbGraphNode::OdDbGraphNode() : m_pData(0), m_flags(0), m_pOwner(0) {}

inline void* OdDbGraphNode::data() const { return m_pData; }
inline void OdDbGraphNode::setData(void* pData) { m_pData = pData; }

inline int OdDbGraphNode::numOut() const { return m_outgoing.size(); }
inline int OdDbGraphNode::numIn() const { return m_incoming.size(); }

inline OdDbGraphNode* OdDbGraphNode::in(int idx) const { return m_incoming.at(idx); }
inline OdDbGraphNode* OdDbGraphNode::out(int idx) const { return m_outgoing.at(idx); }

inline bool OdDbGraphNode::isMarkedAs(OdUInt8 flag) const { return ((m_flags & flag)==flag); }
inline void OdDbGraphNode::markAs(OdUInt8 flags)
{
  if(!GETBIT(flags, kFirstLevel))
  {
    m_flags |= flags;
  }
  else
  {
    throw OdError(eInvalidInput);
  }
}
inline void OdDbGraphNode::clear(OdUInt8 flags)
{
  if(!GETBIT(flags, kFirstLevel))
  {
    m_flags &= (~flags);
  }
  else
  {
    throw OdError(eInvalidInput);
  }
}

inline OdDbGraph* OdDbGraphNode::owner() const { return m_pOwner; }
inline void OdDbGraphNode::setOwner(OdDbGraph* pOwn)
{
  if(m_pOwner)
  {
    ODA_FAIL();
    throw OdError(eInvalidOwnerObject);
  }
  m_pOwner = pOwn;
}

inline int OdDbGraphNode::numCycleOut() const { return m_cycleOut.size(); }
inline int OdDbGraphNode::numCycleIn() const { return m_cycleIn.size(); }

inline OdDbGraphNode* OdDbGraphNode::cycleOut(int idx) const { return m_cycleOut[idx]; }
inline OdDbGraphNode* OdDbGraphNode::cycleIn(int idx) const { return m_cycleIn[idx]; }
inline OdDbGraphNode* OdDbGraphNode::nextCycleNode() const { return cycleOut(0); }

inline bool OdDbGraphNode::isCycleNode() const
{ return (numCycleOut() != 0 || numCycleIn() != 0); }


// OdDbGraph inlines ...

inline OdDbGraph::OdDbGraph() : m_bDirty(false), m_nNonCycleNodes(0) {}
inline int OdDbGraph::numNodes() const { return m_nodes.size(); }
inline OdDbGraphNode* OdDbGraph::node(int idx) const { return m_nodes.at(idx); }

inline OdDbGraphNode* OdDbGraph::rootNode() const { return(numNodes() > 0) ? node(0) : 0; }

inline bool OdDbGraph::isEmpty() const { return numNodes() == 0; }
inline void OdDbGraph::setDirty() { m_bDirty = true; }


// XreGraphStack inlines ...

inline bool OdDbGraphStack::isEmpty() const { return m_stack.empty(); }
inline OdDbGraphNode* OdDbGraphStack::top() const { return isEmpty() ? 0 : m_stack.last(); }
inline void OdDbGraphStack::push(OdDbGraphNode* pNode) { m_stack.push_back(pNode); }
inline OdDbGraphNode* OdDbGraphStack::pop()
{
  if(!isEmpty())
  {
    OdDbGraphNode* pRes = top();
    m_stack.removeLast();
    return pRes;
  }
  return 0;
}

#include "DD_PackPop.h"

#endif // _ODDBGRAPH_H_INCLUDED_

