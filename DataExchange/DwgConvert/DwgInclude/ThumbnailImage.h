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




#if !defined(_ODTHUMBNAILIMAGE_INCLUDED_)
#define _ODTHUMBNAILIMAGE_INCLUDED_

#include "DD_PackPush.h"

#include "OdBinaryData.h"

/** Description:
    This is a data container class used by odDbGetPreviewBitmap() and
    several other bitmap manipulation classes.  
    
    Remarks:
    See class OdThumbnailImage.

    {group:Other_Classes}
*/
class OdThumbnailImage
{
public:  
	OdBinaryData header;
	OdBinaryData bmp;
	OdBinaryData wmf;
  /** Description:
      True if header data is populated.
  */
	bool hasHeader() const { return !header.empty(); }
  /** Description:
      True if bmp data is populated.
  */
	bool hasBmp() const { return !bmp.empty(); }
  /** Description:
      True if wmf data is populated.
  */
	bool hasWmf() const { return !wmf.empty(); }

  /** Description:
      Returns the total count of header + bmp + wmf entries
  */
	int getNumEntries() const
  {
    return ((hasHeader()?1:0)+(hasBmp()?1:0)+(hasWmf()?1:0));
  }
};

#include "DD_PackPop.h"

#endif // !defined(_ODTHUMBNAILIMAGE_INCLUDED_)


