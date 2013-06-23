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


#ifndef BLKVAR_DEF
#define BLKVAR_DEF  VAR_DEF
#endif


/************************************************
 *
 * Dimension variables definitions
 */

//          Type              Name    DXF     Default value         Metric Def Value      Reserve1   Reserve2
VAR_DEF(OdInt16,              adec,   179,    (0),                  (0),                  (),        ValidateRange(0,8))
VAR_DEF(bool,                 alt,    170,    (false),              (false),              (),        ValidateNone())
VAR_DEF(OdUInt16,             altd,   171,    (2),                  (3),                  (),        ValidateNone())
VAR_DEF(double,               altf,   143,    (25.4),               (0.03937007874016),   (),        ValidateNone())
VAR_DEF(double,               altrnd, 148,    (0.0),                (0.0),                (),        ValidateNone())
VAR_DEF(OdInt16,              alttd,  274,    (2),                  (3),                  (),        ValidateNone())
VAR_DEF(OdUInt8,              alttz,  286,    (0),                  (0),                  (),        ValidateRange(0,8))
VAR_DEF(OdInt16,              altu,   273,    (2),                  (2),                  (),        ValidateNone())
VAR_DEF(OdUInt8,              altz,   285,    (0),                  (0),                  (),        ValidateNone())
VAR_DEF(OdString,             apost,  4,      (""),                 (""),                 (),        ValidateNone())
VAR_DEF(double,               asz,    41,     (0.18),               (2.5),                (),        ValidateNone())
VAR_DEF(OdInt16,              atfit,  289,    (3),                  (3),                  (),        ValidateRange(0,3))
VAR_DEF(OdInt16,              aunit,  275,    (0),                  (0),                  (),        ValidateRange(0,3))
VAR_DEF(OdInt16,              azin,   79,     (0),                  (0),                  (),        ValidateRange(0,3))
BLKVAR_DEF(OdDbHardPointerId, blk,    342,    (NULL),               (NULL),               (),        ValidateNone())
BLKVAR_DEF(OdDbHardPointerId, blk1,   343,    (NULL),               (NULL),               (),        ValidateNone())
BLKVAR_DEF(OdDbHardPointerId, blk2,   344,    (NULL),               (NULL),               (),        ValidateNone())
VAR_DEF(double,               cen,    141,    (0.09),               (2.5),                (),        ValidateNone())
VAR_DEF(OdCmColor,            clrd,   176,(OdCmEntityColor::kByBlock),(OdCmEntityColor::kByBlock),(),ValidateNone())
VAR_DEF(OdCmColor,            clre,   177,(OdCmEntityColor::kByBlock),(OdCmEntityColor::kByBlock),(),ValidateNone())
VAR_DEF(OdCmColor,            clrt,   178,(OdCmEntityColor::kByBlock),(OdCmEntityColor::kByBlock),(),ValidateNone())
VAR_DEF(OdInt16,              dec,    271,    (4),                  (2),                  (),        ValidateNone())
VAR_DEF(double,               dle,    46,     (0.0),                (0.0),                (),        ValidateNone())
VAR_DEF(double,               dli,    43,     (0.38),               (3.75),               (),        ValidateNone())
VAR_DEF(double,               exe,    44,     (0.18),               (1.25),               (),        ValidateNone())
VAR_DEF(OdInt16,              dsep,   278,    ('.'),                (','),                (),        ValidateWChar())
VAR_DEF(double,               exo,    42,     (0.0625),             (0.625),              (),        ValidateNone())
VAR_DEF(OdInt16,              frac,   276,    (0),                  (0),                  (),        ValidateRange(0,2))
VAR_DEF(double,               gap,    147,    (0.09),               (0.625),              (),        ValidateNone())
VAR_DEF(OdUInt16,             just,   280,    (0),                  (0),                  (),        ValidateRange(0,4))
VAR_DEF(OdDbHardPointerId,    ldrblk, 341,    (NULL),               (NULL),               (),        ValidateNone())
VAR_DEF(double,               lfac,   144,    (1.0),                (1.0),                (),        ValidateNone())
VAR_DEF(bool,                 lim,    72,     (false),              (false),              (),        ValidateNone())
VAR_DEF(OdInt16,              lunit,  277,    (2),                  (2),                  (),        ValidateRange(1,6))
VAR_DEF(OdDb::LineWeight,     lwd,    371,    (OdDb::kLnWtByBlock), (OdDb::kLnWtByBlock), (),        ValidateNone())
VAR_DEF(OdDb::LineWeight,     lwe,    372,    (OdDb::kLnWtByBlock), (OdDb::kLnWtByBlock), (),        ValidateNone())
VAR_DEF(OdString,             post,   3,      (""),                 (""),                 (),        ValidateNone())
VAR_DEF(double,               rnd,    45,     (0.0),                (0.0),                (),        ValidateNone())
VAR_DEF(bool,                 sah,    173,    (false),              (false),              (),        ValidateNone())
VAR_DEF(double,               scale,  40,     (1.0),                (1.0),                (),        ValidateNone())
VAR_DEF(bool,                 sd1,    281,    (false),              (false),              (),        ValidateNone())
VAR_DEF(bool,                 sd2,    282,    (false),              (false),              (),        ValidateNone())
VAR_DEF(bool,                 se1,    75,     (false),              (false),              (),        ValidateNone())
VAR_DEF(bool,                 se2,    76,     (false),              (false),              (),        ValidateNone())
VAR_DEF(bool,                 soxd,   175,    (false),              (false),              (),        ValidateNone())
VAR_DEF(OdInt16,              tad,    77,     (0),                  (1),                  (),        ValidateRange(0,3))
VAR_DEF(OdInt16,              tdec,   272,    (4),                  (2),                  (),        ValidateNone())
VAR_DEF(double,               tfac,   146,    (1.0),                (1.0),                (),        ValidateNone())
VAR_DEF(bool,                 tih,    73,     (true),               (false),              (),        ValidateNone())
VAR_DEF(bool,                 tix,    174,    (false),              (false),              (),        ValidateNone())
VAR_DEF(double,               tm,     48,     (0.0),                (0.0),                (),        ValidateNone())
VAR_DEF(OdInt16,              tmove,  279,    (0),                  (0),                  (),        ValidateRange(0,2))
VAR_DEF(bool,                 tofl,   172,    (false),              (true),               (),        ValidateNone())
VAR_DEF(bool,                 toh,    74,     (true),               (false),              (),        ValidateNone())
VAR_DEF(bool,                 tol,    71,     (false),              (false),              (),        ValidateNone())
VAR_DEF(OdUInt8,              tolj,   283,    (1),                  (0),                  (),        ValidateRange(0,2))
VAR_DEF(double,               tp,     47,     (0.0),                (0.0),                (),        ValidateNone())
VAR_DEF(double,               tsz,    142,    (0.0),                (0.0),                (),        ValidateNone())
VAR_DEF(double,               tvp,    145,    (0.0),                (0.0),                (),        ValidateNone())
VAR_DEF(OdDbHardPointerId,    txsty,  340,    (NULL),               (NULL),               (),        ValidateNone())
VAR_DEF(double,               txt,    140,    (0.18),               (2.5),                (),        ValidateNone())
VAR_DEF(OdUInt8,              tzin,   284,    (0),                  (8),                  (),        ValidateRange(0,12))
VAR_DEF(bool,                 upt,    288,    (false),              (false),              (),        ValidateNone())
VAR_DEF(OdUInt8,              zin,    78,     (0),                  (8),                  (),        ValidateRange(0,12))


