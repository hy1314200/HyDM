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



#ifndef DD_VERSION_DEFINED
#define DD_VERSION_DEFINED

#define COMPANY_NAME   "Open Design Alliance (ODA)\0"
#define PRODUCT_NAME   "DWGdirect\0"


// Full DWGdirect version is:
//   DD_MAJOR_VERSION.DD_MINOR_VERSION.DD_MAJOR_BUILD_NUMBER.DD_MINOR_BUILD_NUMBER
// Example: 1.11.00.00 (initial 1.11 release).
// OpenDesign maintenance updates to 1.11 will increase the major build number, 
// for example, 1.11.01.00, 1.11.02.00, etc.  Minor build number is reserved for
// client builds.

/* When Incrementing these be sure to make the appropriate changes
 * to the Version string and build comments.
 */
#define DD_MAJOR_VERSION 1
#define DD_MINOR_VERSION   13
#define DD_TYPELIB_VER(MAJ,MIN) version(##MAJ##.##MIN##)
#define DD_MAJOR_BUILD_VERSION 2
#define DD_MINOR_BUILD_VERSION 0

#define PRODUCT_VER_STR "1, 13, 2, 0\0"
#define BUILD_COMMENTS  "DWGdirect 1.13.02.00\0"


#endif  // ODA_VERSION_DEFINED


