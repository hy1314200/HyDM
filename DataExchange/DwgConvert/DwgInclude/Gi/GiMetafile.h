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



#ifndef _ODGIMETAFILE_H_
#define _ODGIMETAFILE_H_

#include "RxObject.h"

#include "DD_PackPush.h"

/** Description:
    Represents a metafile within the DWGdirect vectorization framework.

    {group:OdGi_Classes} 
*/
class FIRSTDLL_EXPORT OdGiMetafile : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGiMetafile);

  /** Description:
      Returns the size of the metafile data within this object.
  */
  virtual OdUInt32 dataSize() const = 0;

  /** Description:
      Returns the metafile data from this object, either in the older Windows format
      or in the new enhanced format.

      Arguments:
        pBytes (O) Receives the metafile data (caller must ensure that enough memory has been allocated).
  */
  virtual void bitsData(OdUInt8* pBytes) const = 0;

  /** Remarks:
      If the OdUInt8* bitsData() version of this function returns NULL, 
      a direct pointer to the data could not be 
      returned, and the bitsData(OdUInt8* pBytes) version of this function
      should be called instead.
  */
  virtual const OdUInt8* bitsData() const { return 0; }
};

typedef OdSmartPtr<OdGiMetafile> OdGiMetafilePtr;

#include "DD_PackPop.h"

#endif //#ifndef _ODGIMETAFILE_H_
