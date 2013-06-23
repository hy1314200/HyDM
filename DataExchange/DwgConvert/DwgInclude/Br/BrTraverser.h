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


#ifndef _INC_DDBRTRAVERSER_3F82DF7201C5_INCLUDED
#define _INC_DDBRTRAVERSER_3F82DF7201C5_INCLUDED

#include "BrEnums.h"
#include "RxObject.h"

#include "DD_PackPush.h"


/** Description:
    Interface class for B-Rep traversers (defines functionality common to
    all traverser classes).

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrTraverser
{
public:
  /** Description:
      Returns true if the full topological adjacency list has been traversed, 
      based on the most recent starting position.
  */
  bool done() const;

  /** Description:
      Adjusts this traverser to point to the next object in the topological adjacency list.
      Adjacency lists are circular ordered lists.
  */
  OdBrErrorStatus next();

  /** Description:
      Resets the starting point of this traverser object to the first object in the 
      topological adjacency list.
  */
  OdBrErrorStatus restart();

  virtual ~OdBrTraverser();

  bool isEqualTo(const OdBrTraverser* other) const;

  bool isNull() const;

protected:
  OdRxObjectPtr m_pImp;

  OdBrTraverser();

  OdBrTraverser(const OdBrTraverser& orig);

  friend class OdBrTraverserInternals;
};

#include "DD_PackPop.h"

#endif /* _INC_DDBRTRAVERSER_3F82DF7201C5_INCLUDED */


