
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

#ifndef _ODBDOLE2FRAME_INCLUDED_
#define _ODBDOLE2FRAME_INCLUDED_

#include "DD_PackPush.h"

class OdDbOle2Frame;

  /** Description:
      Was not accessible in DD1.12 (OdDbOle2FrameImpl::m_UnknownFromDWG)
  */

OdInt32 odbdOle2FrameUnknown0(const OdDbOle2Frame* pOle2Frame);

  /** Description:
      Stil unknown. OdDbOle2Frame::getUnknown1() in DD1.12
  */
OdUInt8 odbdOle2FrameUnknown1(const OdDbOle2Frame* pOle2Frame);

  /** Description:
      Stil unknown. OdDbOle2Frame::getUnknown2() in DD1.12
  */
OdUInt8 odbdOle2FrameUnknown2(const OdDbOle2Frame* pOle2Frame);

void odbdOle2FrameSetUnknown(OdDbOle2Frame* pOle2Frame, OdInt32 unk0, OdUInt8 unk1, OdUInt8 unk2);

  /** Description:
      OdDbOle2Frame::getPixelWidth() in DD1.12
  */
OdUInt16 odbdOle2FrameHimetricWidth(const OdDbOle2Frame* pOle2Frame);

  /** Description:
      OdDbOle2Frame::getPixelHeight() in DD1.12
  */
OdUInt16 odbdOle2FrameHimetricHeight(const OdDbOle2Frame* pOle2Frame);

  /** Description:
      OdDbOle2Frame::setPixelWidth/Height() in DD1.12
  */
void odbdOle2FrameSetHimetricSize(OdDbOle2Frame* pOle2Frame, OdUInt16 w, OdUInt16 h);

#endif // #ifndef _ODBDOLE2FRAME_INCLUDED_

