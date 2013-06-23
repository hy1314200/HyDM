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



#ifndef _ODDBDIMSTYLETABLERECORD_INCLUDED
#define _ODDBDIMSTYLETABLERECORD_INCLUDED

#include "DD_PackPush.h"

#include "DbSymbolTableRecord.h"

/** Description: 
    This namespace contains utility functions for DimensionStyle processing.

    {group:DD_Namespaces}
*/
namespace OdDmUtil
{
  /** Description:
    Returns the R14 DIMFIT value corresponding to the the specified DIMATFIT and DIMTMOVE values.

    Arguments:
    dimatfit (I) DIMATFIT value.
    dimtmove (I) DIMTMOVE value.
    
    Remarks:
    The complete mapping is as follows:
    
    @table
    dimtmove    dimafit   dimfit
    0           0         0
    0           1         1
    0           2         2
    0           3         3
    1           0         4
    1           1         4
    1           2         4
    1           3         4
    2           0         5
    2           1         5
    2           2         5
    2           3         5
    
    All other input combinations return 3.
  */
  int TOOLKIT_EXPORT dimfit(
    int dimatfit, 
    int dimtmove);
  
  /** Description:
    Returns the R14 DIMUNIT value corresponding to the specified DIMLUNIT and DIMFRAC values.

    Arguments:
    dimlunit (I) DIMLUNIT value.
    dimfrac (I) DIMFRAC value.    

    Remarks:
    The complete mapping is as follows:
    
    @table
    dimfrac   dimlunit  dimunit
    0         1         1 
    0         2         2
    0         3         3
    0         4         4
    0         5         5
    0         6         8
    1         1         1 
    1         2         2
    1         3         3
    1         4         4
    1         5         5
    1         6         8
    2         1         1 
    2         2         2
    2         3         3
    2         4         6
    2         5         7
    2         6         8
    
    All other input combinations return 2.
  */
  int TOOLKIT_EXPORT dimunit(
    int dimlunit, 
    int dimfrac);
  
  /** Description:
    Returns the R15 DIMATFIT value corresponding to the specified R14 DIMFIT value.

    Arguments:
    dimfit (I) DIMFIT value.
    
    Remarks:
    The complete mapping is as follows:
    
    @table
    dimfit  dimatfit
    0       0
    1       1
    2       2
    3       3
    4       3
    5       3
    other   3
    
  */
  int TOOLKIT_EXPORT dimatfit(
    int dimfit);
  
  /** Description:
    Returns the R15 DIMTMOVE value corresponding to the specified R14 DIMFIT value.
    
    Arguments:
    dimfit (I) DIMFIT value.
    
    Remarks:
    The complete mapping is as follows:
    
    @table
    dimfit  dimtmove
    0       0
    1       0
    2       0
    3       0
    4       1
    5       2
    other   0
  */
  int TOOLKIT_EXPORT dimtmove(
    int dimfit);
  
  /** Description:
    Returns the R15 DIMLUNIT value corresponding to the specified R14 DUMUNIT value.
    
    Arguments:
    dimunit (I) DIMUNIT value.
    
    Remarks:
    The complete mapping is as follows:
    
    @table
    dimunit    dimlunit
    1          1
    2          2
    3          3
    4          4
    5          5
    6          4
    7          5
    8          6
    other      2
  */
  int TOOLKIT_EXPORT dimlunit(
    int dimunit);
  
  /** Description:
    Returns the R15 DIMFRAC value corresponding to the specified R14 DUMUNIT value.
    
    Arguments:
    dimunit (I) DIMUNIT value.
    
    Remarks:
    The complete mapping is as follows:
    
    @table
    dimunit    dimfrac
    1          0
    2          0
    3          0
    4          0
    5          0
    6          2
    7          2
    8          0
    other      0
  */
  int TOOLKIT_EXPORT dimfrac(
    int dimunit);
    
  /** Description:
    Returns the local name of the specified dimension arrowhead.
    
    blockId (I) Object ID of the dimension arrowhead.
  */
  OdString TOOLKIT_EXPORT arrowName(
    OdDbObjectId blockId);
  
  /** Description:
    Returns true if and only if the specified arrowhead *name* is that of a built-in arrowhead.

    Arguments:
    arrowheadName (I) Arrowhead *name*.

    Remarks:
    The built-in arrowheads are as follows:
    
    @table
    Name
    ArchTick
    BoxBlank
    BoxFilled
    Closed
    ClosedBlank
    DatumBlank
    DatumFilled
    Dot
    DotBlank
    DotSmall
    Integral
    None
    Oblique
    Open
    Origin
    Origin2
    Small
  */
  bool TOOLKIT_EXPORT isBuiltInArrow(
    const OdChar* arrowheadName);
  
  /** Description:
    Returns true if and only if the specified arrowhead *name* is that of a built-in arrowhead 
    that is treated as having zero length.  
      
    Arguments:
    arrowheadName (I) Arrowhead *name*.

    Remarks:
    The built-in zero-length arrowheads are as follows:

    @table
    Name
    ArchTick
    DotSmall
    Integral
    None
    Oblique
    Small
  */
  bool TOOLKIT_EXPORT isZeroLengthArrow(
    const OdChar* arrowheadName);
  
  /** Description:
    Returns the object ID of the arrowhead block with the specified arrowhead *name*.

    Arguments:
    arrowheadName (I) Arrowhead *name*.
    pDb (I) Database containing the arrowhead.
  */
  OdDbObjectId TOOLKIT_EXPORT findArrowId(
    const OdChar* arrowheadName, 
    OdDbDatabase* pDb);
  
  /** Description:
    Returns the object ID of the arrowhead block with the specified arrowhead *name*, creating
    this block if necessary.

    Arguments:
    arrowheadName (I) Arrowhead *name*.
    pDb (I) Database containing the arrowhead.
  */
  OdDbObjectId TOOLKIT_EXPORT getArrowId(
    const OdChar* arrowheadName, 
    OdDbDatabase* pDb);

  /*
       OdString globalArrowName(const OdChar* pName);  
       OdString globalArrowName(OdDbObjectId blockId);
       OdString arrowName(const OdChar* pName);
  */

}

/** Description:

    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum DimArrowFlags
  {
    kFirstArrow  = 0, // First arrowhead
    kSecondArrow = 1  // Second arrowhead
  };
}

/** Description:
    This class represents DimensionStyle records in the OdDbDimStyleTable in an OdDbDatabase instance.

    Library:
    Db

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDimStyleTableRecord: public OdDbSymbolTableRecord
{
public:
  ODDB_DECLARE_MEMBERS(OdDbDimStyleTableRecord);
  
  OdDbDimStyleTableRecord();
    
#define VAR_DEF(a, b, dxf, c, d, r1, r2) \
/** Returns the dim##b value of this object. */ \
a dim##b() const; \
/** Sets the dim##b value of this object. */ \
void setDim##b(a);
#include "DimVarDefs.h"
#undef  VAR_DEF
  
  /** Description:
    Sets the DIMBLK value for this DimensionStyle. 
    
    Arguments:
    dimblk (I) New value.  
  */
  void setDimblk(
    const OdChar* newValue);

  /** Description:
    Sets the DIMBLK1 value for this DimensionStyle. 

    Arguments:
    dimblk1 (I) New value.  
  */
  void setDimblk1(
    const OdChar* dimblk1);

  /** Description:
    Sets the DIMBLK2 value for this DimensionStyle. 

    Arguments:
    dimblk2 (I) New value.  
  */
  void setDimblk2(
  const OdChar* dimblk2);

  /** Description:
    Sets the DIMLDRBLK value for this DimensionStyle.

    Arguments:
    dimldrblk (I) New value.  
  */
  void setDimldrblk(
    const OdChar* dimldrblk);
  
  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);
  
  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;
  
  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);
  
  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
  
  virtual OdResult dxfInFields_R12(
    OdDbDxfFiler* pFiler);

  virtual void dxfOutFields_R12(
    OdDbDxfFiler* pFiler) const;
  
  virtual void getClassID(
    void** pClsid) const;

  /** Description:
    Returns the object ID of the arrowhead block for the specified arrowhead type.

    Arguments:
    arrowType (I) Arrow type.
    
    Remarks:
    arrowType will be one of the following:
    
    @table
    Name           Value    Description    
    kFirstArrow    0        First arrowhead
    kSecondArrow   1        Second arrowhead
  */
  OdDbObjectId arrowId(
    OdDb::DimArrowFlags arrowType) const;

  /*
  OdString dimpost() const;
  OdString dimapost() const;
  OdString dimblk() const;
  OdString dimblk1() const;
  OdString dimblk2() const;
  
  int dimfit() const;
  int dimunit() const;
    
  void setDimfit(int fit);
  void setDimunit(int unit);
  bool isModifiedForRecompute() const;
  */

};

/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbDimStyleTableRecord object pointers.
*/
typedef OdSmartPtr<OdDbDimStyleTableRecord> OdDbDimStyleTableRecordPtr;

#include "DD_PackPop.h"

#endif // _ODDBDIMSTYLETABLERECORD_INCLUDED


