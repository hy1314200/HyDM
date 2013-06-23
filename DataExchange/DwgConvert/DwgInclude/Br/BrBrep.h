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


#ifndef _INC_DDBRBREP_3F82B32C036B_INCLUDED
#define _INC_DDBRBREP_3F82B32C036B_INCLUDED

#include "BrEntity.h"
#include "BrEnums.h"
#include "DbEntity.h"

#include "DD_PackPush.h"

/** Description:
    Defines the highest level interface to the B-Rep subsystem of DWGdirect.  Given
    an ACIS file contained in an OdDb3dSolid entity (or other ACIS entity), this 
    class provides the top level interface for traversing the B-Rep structure contained
    in the ACIS data.

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrBrep : public OdBrEntity
{
public:

  /** Description: 
      Sets the ACIS data for this object.  See OdBrEx.cpp in the Examples/OdBrEx sample
      directory for sample usage.
  */
  void set(const void* acisFile);

  bool isValid() const;

  bool getTransformation( OdGeMatrix3d& m ) const;

  OdBrBrep();
};

#include "DD_PackPop.h"

#endif /* _INC_DDBRBREP_3F82B32C036B_INCLUDED */


