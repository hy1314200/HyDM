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



#ifndef _ODBINARYDATA_INCLUDED_
#define _ODBINARYDATA_INCLUDED_

#include "DD_PackPush.h"

#include "OdArray.h"

class OdDbDwgFiler;

/** Description:

    {group:Other_Classes}
*/
class OdBinaryData : public OdArray<OdUInt8, OdMemoryAllocator<OdUInt8> >
{
};


/** Description:

    {group:Other_Classes}
*/
class OdBitBinaryData : public OdBinaryData
{
  OdUInt32 m_nBitSize;
public:
  OdBitBinaryData() : m_nBitSize(0) {}

  OdUInt32 getBitSize() const { return m_nBitSize; }
  void setBitSize(OdUInt32 value) { m_nBitSize = value; resize((m_nBitSize+7)/8); }

  void updateBitSize() {setBitSize( size() * 8 );}
};

#include "DD_PackPop.h"

#endif //_ODBINARYDATA_INCLUDED_


