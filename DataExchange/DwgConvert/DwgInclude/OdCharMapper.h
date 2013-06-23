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



#ifndef _OD_CHARMAPPER_H_
#define _OD_CHARMAPPER_H_

#include "DD_PackPush.h"

#include "OdCodePage.h"

class OdStreamBuf;
class OdString;


/** Description:

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdCharMapper
{
  static OdString m_MapFile;
private:
	OdCharMapper();
public:
//	static OdResult initialize(OdStreamBuf* pIo);
	static OdResult initialize(const OdString& fMap);

  static OdResult unicodeToCodepage(OdUInt16 sourceChar,
		OdCodePageId codepage,
    OdUInt16& codepageChar);

  static OdResult codepageToUnicode(OdUInt16 sourceChar,
		OdCodePageId codepage,
    OdUInt16& unicodeChar);

	static bool isLeadByte(OdUInt8 theByte, OdCodePageId codepage);

	static OdResult codepageDescToId(const OdString& desc, OdCodePageId& id);

	static OdResult codepageIdToDesc(OdCodePageId id, OdString& desc);

	static OdUInt32 numValidCodepages();

	static OdCodePageId ansiCpToAcadCp(OdUInt32 ansi_cp);

	static OdUInt32 acadCpToAnsiCp(OdCodePageId id);

  static OdCodePageId getCodepageByCharset(OdUInt16 charset);
};

#include "DD_PackPop.h"

#endif


