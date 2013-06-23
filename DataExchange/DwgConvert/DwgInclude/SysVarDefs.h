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



//$ACADVER  AC1015
//short ACADMAINTVER     6


#ifndef RO_VAR_DEF
#define RO_VAR_DEF  VAR_DEF /* {Secret} */
#endif

#ifndef VAR_DEF
/* {Secret} */
#define VAR_DEF(a,b,c,d,r1,r2)
/* {Secret} */
#define DOUNDEF_VAR_DEF       
#endif

//      Type                    Name          Default                         Metric default                    Reserve1                    Reserve2
VAR_DEF(   OdGePoint3d,            INSBASE,      (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            EXTMIN,       (1.E+20, 1.E+20, 1.E+20),       (1.E+20, 1.E+20, 1.E+20),         (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            EXTMAX,       (-1.E+20, -1.E+20, -1.E+20),    (-1.E+20, -1.E+20, -1.E+20),      (),                         ValidateNone())
VAR_DEF(   OdGePoint2d,            LIMMIN,       (0.0, 0.0),                     (0.0, 0.0),                       (),                         ValidateNone())
VAR_DEF(   OdGePoint2d,            LIMMAX,       (12.0, 9.0),                    (420.0, 297.0),                   (),                         ValidateNone())
VAR_DEF(   bool,                   ORTHOMODE,    (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   bool,                   REGENMODE,    (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   bool,                   FILLMODE,     (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   bool,                   QTEXTMODE,    (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   bool,                   MIRRTEXT,     (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   double,                 LTSCALE,      (1.0),                          (1.0),                            (),                         ValidateNone())
VAR_DEF(   OdInt16,                ATTMODE,      (1),                            (1),                              (),                         ValidateRange(0,2))
VAR_DEF(   double,                 TEXTSIZE,     (0.2),                          (2.5),                            (),                         ValidateNone())
VAR_DEF(   double,                 TRACEWID,     (0.05),                         (1.0),                            (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      TEXTSTYLE,    (NULL),                         (NULL),                           (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      CLAYER,       (NULL),                         (NULL),                           (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      CELTYPE,      (NULL),                         (NULL),                           (),                         ValidateNone())
VAR_DEF(   OdCmColor,              CECOLOR,      (OdCmEntityColor::kByLayer),    (OdCmEntityColor::kByLayer),      (),                         ValidateNone())
VAR_DEF(   double,                 CELTSCALE,    (1.0),                          (1.0),                            (),                         ValidateNone())
VAR_DEF(   bool,                   DISPSILH,     (false),                        (false),                          (),                         ValidateNone())
/*
VAR_DEF(   bool,                   SNAPMODE,     (false))
VAR_DEF(   OdGePoint2d,            SNAPUNIT,     (0.5000,0.5000))
VAR_DEF(   OdGePoint2d,            SNAPBASE,     (0.0,0.0))
VAR_DEF(   double,                 SNAPANG,      (0.0))
VAR_DEF(   bool,                   SNAPSTYLE,    (false))
VAR_DEF(   OdInt16,                SNAPISOPAIR,  (0))
VAR_DEF(   bool,                   GRIDMODE,     (false))
VAR_DEF(   OdGePoint2d,            GRIDUNIT,     (0.5000,0.5000))
*/
// Dimension variables

VAR_DEF(   OdDbHardPointerId,      DIMSTYLE,     (NULL),                         (NULL),                           (),                         ValidateNone())
VAR_DEF(   bool,                   DIMASO,       (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   bool,                   DIMSHO,       (true),                         (true),                           (),                         ValidateNone())

VAR_DEF(   OdInt16,                LUNITS,       (2),                            (2),                              (),                         ValidateRange(1,5))
VAR_DEF(   OdInt16,                LUPREC,       (4),                            (4),                              (),                         ValidateRange(0,8))
VAR_DEF(   double,                 SKETCHINC,    (0.1),                          (1.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 FILLETRAD,    (0.5),                          (10.0),                           (),                         ValidateNone())
VAR_DEF(   OdInt16,                AUNITS,       (0),                            (0),                              (),                         ValidateRange(0,4))
VAR_DEF(   OdInt16,                AUPREC,       (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   OdString,               MENU,         ("."),                          ("."),                            (),                         ValidateNone()) // Read only
VAR_DEF(   double,                 ELEVATION,    (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 PELEVATION,   (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 THICKNESS,    (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   bool,                   LIMCHECK,     (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   double,                 CHAMFERA,     (0.5),                          (10.0),                           (),                         ValidateNone())
VAR_DEF(   double,                 CHAMFERB,     (0.5),                          (10.0),                           (),                         ValidateNone())
VAR_DEF(   double,                 CHAMFERC,     (1.0),                          (20.0),                           (),                         ValidateNone())
VAR_DEF(   double,                 CHAMFERD,     (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   bool,                   SKPOLY,       (false),                        (false),                          (),                         ValidateNone())
RO_VAR_DEF(OdDbDate,               TDUCREATE,    (OdDbDate::kInitZero),          (OdDbDate::kInitZero),            (),                         ValidateReadOnly())
RO_VAR_DEF(OdDbDate,               TDUUPDATE,    (OdDbDate::kInitZero),          (OdDbDate::kInitZero),            (),                         ValidateReadOnly())
RO_VAR_DEF(OdDbDate,               TDINDWG,      (OdDbDate::kInitZero),          (OdDbDate::kInitZero),            (),                         ValidateReadOnly())
RO_VAR_DEF(OdDbDate,               TDUSRTIMER,   (OdDbDate::kInitZero),          (OdDbDate::kInitZero),            (),                         ValidateReadOnly())
VAR_DEF(   bool,                   USRTIMER,     (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   double,                 ANGBASE,      (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   bool,                   ANGDIR,       (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   OdInt16,                PDMODE,       (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   double,                 PDSIZE,       (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 PLINEWID,     (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   bool,                   SPLFRAME,     (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   OdInt16,                SPLINETYPE,   (6),                            (6),                              (),                         ValidateRange(5,6))
VAR_DEF(   OdInt16,                SPLINESEGS,   (8),                            (8),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                SURFTAB1,     (6),                            (6),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                SURFTAB2,     (6),                            (6),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                SURFTYPE,     (6),                            (6),                              (),                         ValidateRange(5,8))
VAR_DEF(   OdInt16,                SURFU,        (6),                            (6),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                SURFV,        (6),                            (6),                              (),                         ValidateNone())
// UCS and PUCS vars here
VAR_DEF(   OdInt16,                USERI1,       (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                USERI2,       (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                USERI3,       (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                USERI4,       (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                USERI5,       (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   double,                 USERR1,       (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 USERR2,       (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 USERR3,       (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 USERR4,       (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   double,                 USERR5,       (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   bool,                   WORLDVIEW,    (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   OdInt16,                SHADEDGE,     (3),                            (3),                              (),                         ValidateRange(0,3))
VAR_DEF(   OdInt16,                SHADEDIF,     (70),                           (70),                             (),                         ValidateNone())
VAR_DEF(   bool,                   TILEMODE,     (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   OdInt16,                MAXACTVP,     (64),                           (64),                             (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PINSBASE,     (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   bool,                   PLIMCHECK,    (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PEXTMIN,      (1.E+20, 1.E+20, 1.E+20),       (1.E+20, 1.E+20, 1.E+20),         (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PEXTMAX,      (-1.E+20, -1.E+20, -1.E+20),    (-1.E+20, -1.E+20, -1.E+20),      (),                         ValidateNone())
VAR_DEF(   OdGePoint2d,            PLIMMIN,      (0.0, 0.0),                     (0.0, 0.0),                       (),                         ValidateNone())
VAR_DEF(   OdGePoint2d,            PLIMMAX,      (12.0, 9.0),                    (420.0, 297.0),                   (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      UCSNAME,      (OdDbObjectId::kNull),          (OdDbObjectId::kNull),            (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      PUCSNAME,     (OdDbObjectId::kNull),          (OdDbObjectId::kNull),            (),                         ValidateNone())
VAR_DEF(   OdInt16,                UNITMODE,     (0),                            (0),                              (),                         ValidateRange(0,1))
VAR_DEF(   bool,                   VISRETAIN,    (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   bool,                   PLINEGEN,     (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   bool,                   PSLTSCALE,    (1),                            (1),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                TREEDEPTH,    (3020),                         (3020),                           (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      CMLSTYLE,     (NULL),                         (NULL),                           (),                         ValidateNone())
VAR_DEF(   OdInt16,                CMLJUST,      (0),                            (0),                              (),                         ValidateRange(0,2))
VAR_DEF(   double,                 CMLSCALE,     (1.0),                          (20.0),                           (),                         ValidateNone())
VAR_DEF(   OdInt16,                PROXYGRAPHICS,(1),                            (1),                              (),                         ValidateRange(0,1))
VAR_DEF(   OdDb::MeasurementValue, MEASUREMENT,  (OdDb::kEnglish),               (OdDb::kMetric),                  (),                         ValidateRange(OdDb::kEnglish,OdDb::kMetric))
VAR_DEF(   OdDb::LineWeight,       CELWEIGHT,    (OdDb::kLnWtByLayer),           (OdDb::kLnWtByLayer),             (),                         ValidateNone())
VAR_DEF(   OdDb::EndCaps,          ENDCAPS,      (OdDb::kEndCapNone),            (OdDb::kEndCapNone),              (),                         ValidateNone())
VAR_DEF(   OdDb::JoinStyle,        JOINSTYLE,    (OdDb::kJnStylNone),            (OdDb::kJnStylNone),              (),                         ValidateNone())
VAR_DEF(   bool,                   LWDISPLAY,    (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   OdDb::UnitsValue,       INSUNITS,     (OdDb::kUnitsInches),           (OdDb::kUnitsMillimeters),        (),                         ValidateRange(OdDb::kUnitsUndefined,OdDb::kUnitsMax))
VAR_DEF(   OdUInt16,               TSTACKALIGN,  (1),                            (1),                              (),                         ValidateRange(0,2))
VAR_DEF(   OdUInt16,               TSTACKSIZE,   (70),                           (70),                             (),                         ValidateNone())
VAR_DEF(   OdString,               HYPERLINKBASE,(""),                           (""),                             (),                         ValidateNone())
VAR_DEF(   OdString,               STYLESHEET,   (""),                           (""),                             (),                         ValidateNone())
VAR_DEF(   bool,                   XEDIT,        (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   OdDb::PlotStyleNameType,CEPSNTYPE,    (OdDb::kPlotStyleNameByLayer),  (OdDb::kPlotStyleNameByLayer),    (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      CEPSNID,      (NULL),                         (NULL),                           (),                         ValidateNone())
RO_VAR_DEF(bool,                   PSTYLEMODE,   (true),                         (true),                           (),                         ValidateReadOnly())
VAR_DEF(   OdString,               FINGERPRINTGUID,(odInitFINGERPRINTGUID()),    (odInitFINGERPRINTGUID()),        (),                         ValidateNone())
VAR_DEF(   OdString,               VERSIONGUID,  (odInitVERSIONGUID()),          (odInitVERSIONGUID()),            (),                         ValidateNone())
VAR_DEF(   bool,                   EXTNAMES,     (true),                         (true),                           (),                         ValidateNone())
VAR_DEF(   double,                 PSVPSCALE,    (0.0),                          (0.0),                            (),                         ValidateNone())
VAR_DEF(   bool,                   OLESTARTUP,   (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   bool,                   PELLIPSE,     (false),                        (false),                          (),                         ValidateNone())
VAR_DEF(   OdUInt16,               ISOLINES,     (4),                            (4),                              (),                         ValidateNone())
VAR_DEF(   OdUInt16,               TEXTQLTY,     (50),                           (50),                             (),                         ValidateNone())
VAR_DEF(   double,                 FACETRES,     (.5),                           (.5),                             (),                         ValidateNone())
RO_VAR_DEF(OdGePoint3d,            UCSORG,       (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateReadOnly()) // READ ONLY IN ACAD2000
RO_VAR_DEF(OdGeVector3d,           UCSXDIR,      (1.0, 0.0, 0.0),                (1.0, 0.0, 0.0),                  (),                         ValidateReadOnly()) // READ ONLY IN ACAD2000
RO_VAR_DEF(OdGeVector3d,           UCSYDIR,      (0.0, 1.0, 0.0),                (0.0, 1.0, 0.0),                  (),                         ValidateReadOnly()) // READ ONLY IN ACAD2000

VAR_DEF(   OdDbHardPointerId,      PUCSBASE,     (OdDbObjectId::kNull),          (OdDbObjectId::kNull),            (),                         ValidateNone())
RO_VAR_DEF(   OdGePoint3d,            PUCSORG,      (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
RO_VAR_DEF(   OdGeVector3d,           PUCSXDIR,     (1.0, 0.0, 0.0),                (1.0, 0.0, 0.0),                  (),                         ValidateNone())
RO_VAR_DEF(   OdGeVector3d,           PUCSYDIR,     (0.0, 1.0, 0.0),                (0.0, 1.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdInt16,                PUCSORTHOVIEW,(0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PUCSORGTOP,   (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PUCSORGBOTTOM,(0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PUCSORGLEFT,  (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PUCSORGRIGHT, (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PUCSORGFRONT, (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            PUCSORGBACK,  (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdDbHardPointerId,      UCSBASE,      (OdDbObjectId::kNull),          (OdDbObjectId::kNull),            (),                         ValidateNone())
VAR_DEF(   OdInt16,                UCSORTHOVIEW, (0),                            (0),                              (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            UCSORGTOP,    (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            UCSORGBOTTOM, (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            UCSORGLEFT,   (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            UCSORGRIGHT,  (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            UCSORGFRONT,  (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())
VAR_DEF(   OdGePoint3d,            UCSORGBACK,   (0.0, 0.0, 0.0),                (0.0, 0.0, 0.0),                  (),                         ValidateNone())

RO_VAR_DEF(OdCodePageId,           DWGCODEPAGE,  (CP_ANSI_1252),                 (CP_ANSI_1252),                   (),                         ValidateReadOnly())

// New in Red Deer
VAR_DEF(   OdInt16,                DBCSTATE,            (0),                     (0),                              (),                         ValidateRange(0,1))
VAR_DEF(   OdInt16,                INTERSECTIONCOLOR,   (257),                   (257),                            (),                         ValidateNone())
VAR_DEF(   bool,                   INTERSECTIONDISPLAY, (false),                 (false),                          (),                         ValidateNone())
VAR_DEF(   OdInt16,                HALOGAP,             (0),                     (0),                              (),                         ValidateNone())
VAR_DEF(   OdInt16,                OBSCUREDCOLOR,       (257),                   (257),                            (),                         ValidateNone())
VAR_DEF(   OdInt16,                OBSCUREDLTYPE,       (0),                     (0),                              (),                         ValidateRange(0,11))

// these was dictvardefs in old versions
VAR_DEF(OdInt16,                INDEXCTL,     (0),                  (0),                   (),       ValidateNone())
VAR_DEF(OdString,               PROJECTNAME,  (""),                 (""),                  (),       ValidateNone())
VAR_DEF(OdInt16,                SORTENTS,     (127),                (127),                 (),       ValidateNone())
VAR_DEF(bool,                   XCLIPFRAME,   (false),              (false),               (),       ValidateNone())

VAR_DEF(OdInt16,                DIMASSOC,     (2),                  (2),                   (),       ValidateNone())
VAR_DEF(OdInt16,                HIDETEXT,     (1),                  (1),                   (),       ValidateNone())

// AutoCAD 2005
VAR_DEF(OdInt8,                 DRAWORDERCTL, (3),                  (3),                   (),       ValidateRange(0,3))


#ifdef DOUNDEF_VAR_DEF
#undef RO_VAR_DEF
#undef VAR_DEF
#undef DOUNDEF_VAR_DEF
#endif

#ifndef REGVAR_DEF
/* {Secret} */
#define REGVAR_DEF(unused1, unused2, unused3, unused4, unused5)
/* {Secret} */
#define DOUNDEF_REGVAR_DEF
#endif
REGVAR_DEF(bool,                ATTREQ,         (true),               (),       ValidateNone())
REGVAR_DEF(bool,                ATTDIA,         (false),              (),       ValidateNone())
REGVAR_DEF(bool,                BLIPMODE,       (false),              (),       ValidateNone())
REGVAR_DEF(bool,                DELOBJS,        (true),               (),       ValidateNone())
REGVAR_DEF(bool,                FILEDIA,        (true),               (),       ValidateNone())
REGVAR_DEF(OdInt16,             COORDS,         (1),                  (),       ValidateNone())
REGVAR_DEF(OdInt16,             DRAGMODE,       (2),                  (),       ValidateNone())
REGVAR_DEF(OdInt16,             OSMODE,         (37),                 (),       ValidateNone())
REGVAR_DEF(OdInt16,             PICKSTYLE,      (1),                  (),       ValidateNone())
// Range (0.1 - 1.0) emulates ACAD's lw setting slider                          
REGVAR_DEF(double,              LWDISPSCALE,    (0.55),               (),       ValidateNone())
REGVAR_DEF(OdDb::LineWeight,    LWDEFAULT,      (OdDb::kLnWt025),     (),       ValidateNone())
REGVAR_DEF(OdString,            FONTALT,        ("simplex.shx"),      (),       ValidateNone())
REGVAR_DEF(OdInt16,             PLINETYPE,      (2),                  (),       ValidateNone())
REGVAR_DEF(bool,                SAVEROUNDTRIP,  (true),               (),       ValidateNone())
REGVAR_DEF(OdDb::ProxyImage,    PROXYSHOW,      (OdDb::kProxyShow),   (),       ValidateRange(OdDb::kProxyNotShow, OdDb::kProxyBoundingBox))
REGVAR_DEF(bool,                TEXTFILL,       (true),               (),       ValidateNone())

REGVAR_DEF(OdUInt32,            PREVIEW_WIDTH,  (180),                (),       ValidateNone())
REGVAR_DEF(OdUInt32,            PREVIEW_HEIGHT, (85),                 (),       ValidateNone())

// New in Red Deer                                                              
REGVAR_DEF(OdInt16,             GRIPHOVER,      (3),                  (),       ValidateNone())
REGVAR_DEF(OdInt16,             GRIPOBJLIMIT,   (100),                (),       ValidateNone())
REGVAR_DEF(OdInt16,             GRIPTIPS,       (1),                  (),       ValidateNone())
REGVAR_DEF(OdInt16,             HPASSOC,        (1),                  (),       ValidateNone())
REGVAR_DEF(OdString,            LOCALROOTPREFIX,(""),                 (),       ValidateNone())
REGVAR_DEF(OdInt16,             LOGFILEMODE,    (0),                  (),       ValidateNone())

REGVAR_DEF(OdUInt32,            MAXHATCHDENSITY,(1000000),            (),       ValidateNone())
REGVAR_DEF(OdInt16,             FIELDDISPLAY,   (1),                  (),       ValidateNone())

// Deviation for saving curves (ellipse, spline) to R12
REGVAR_DEF(double,              R12SaveDeviation, (0.),               (),       ValidateNone())
// Number of segments between spline control points or on PI/2 elliptic arc.
REGVAR_DEF(OdInt16,             R12SaveAccuracy,  (8),                (),       ValidateNone())

#ifdef DOUNDEF_REGVAR_DEF
#undef REGVAR_DEF
#undef DOUNDEF_REGVAR_DEF
#endif



