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
// DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GE_LIB_VERSION
#define OD_GE_LIB_VERSION /* {Secret} */

#include "DD_PackPush.h"

#define IMAGE_MAJOR_VER 2           /* {Secret} */
#define IMAGE_MINOR_VER 0           /* {Secret} */
#define IMAGE_CORRECTIVE_VER 0      /* {Secret} */
#define IMAGE_INTERNAL_VER 0        /* {Secret} */

/**
Description:
    This class provides management of GeLib versions.

    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeLibVersion
{
public:
  /**
    Arguments:
    major (I) Major version.
    minor (I) Minor version.
    corrective (I) Corrective version.
    schema (I) Schema version.
  */
  OdGeLibVersion ();
  OdGeLibVersion (
    const OdGeLibVersion& source);
  OdGeLibVersion (
    unsigned char major, 
    unsigned char minor,
    unsigned char corrective,
    unsigned char schema);

  /**
    Description:
    Returns the major version of GeLib.
  */
  unsigned char majorVersion () const; 

  /**
    Description:
    Returns the minor version of GeLib.
  */
  unsigned char minorVersion () const; 

  /**
    Description:
    Returns the corrective version of GeLib.
  */
  unsigned char correctiveVersion () const;
   
  /**
    Description:
    Returns the schema version of GeLib.
  */
  unsigned char schemaVersion () const;

  /**
    Description:
    Sets the *major* version of GeLib.

    Arguments:
    major (I) Major version.
  */
  OdGeLibVersion& setMajorVersion (
    unsigned char major); 

  /**
    Description:
    Sets the *minor* version of GeLib.

    Arguments:
    minor (I) Minor version.
  */
  OdGeLibVersion& setMinorVersion (
    unsigned char minor); 

  /**
    Description:
    Sets the *corrective* version of GeLib.

    Arguments:
    corrective (I) Corrective version.
  */
  OdGeLibVersion& setCorrectiveVersion (
    unsigned char corrective); 

  /**
    Description:
    Sets the *schema* version of GeLib.

    Arguments:
    schema (I) Schema version.
  */
  OdGeLibVersion& setSchemaVersion (
    unsigned char schema);

  bool operator == (
    const OdGeLibVersion& libVersion) const; 

  bool operator != (
    const OdGeLibVersion& libVersion) const; 

  bool operator < (
    const OdGeLibVersion& libVersion) const;

  bool operator <= (
    const OdGeLibVersion& libVersion) const; 

  bool operator > (
    const OdGeLibVersion& libVersion) const; 

  bool operator >= (
    const OdGeLibVersion& libVersion) const;

  static const OdGeLibVersion kRelease0_95; // GeLib release 0.

  static const OdGeLibVersion kReleaseSed; // GeLib 14.0 release. 

  static const OdGeLibVersion kReleaseTah; // GeLib 15.0 release.

private:
  unsigned char mVersion[10];
};

#include "DD_PackPop.h"

#endif // OD_GE_LIB_VERSION


