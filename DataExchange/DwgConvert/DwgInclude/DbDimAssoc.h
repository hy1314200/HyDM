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



#ifndef OD_DBDIMASSOC_H
#define OD_DBDIMASSOC_H

#include "DD_PackPush.h"

#include "DbSubentId.h"
#include "DbHandle.h"

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbXrefFullSubentPath : public OdDbFullSubentPath
{
public:
  OdHandleArray& xrefObjHandles();

  void dwgIn(OdDbDwgFiler* pFiler);
  void dwgOut(OdDbDwgFiler* pFiler);
  void dxfOut(OdDbDxfFiler* pFiler, OdInt nGrCode = 0);
private:
  OdHandleArray m_XrefObjHandles;
};

class OdDbOsnapPointRef;
typedef OdSmartPtr<OdDbOsnapPointRef> OdDbOsnapPointRefPtr;

/** Description:

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbOsnapPointRef : public OdRxObject
{
public:
  OdDbOsnapPointRef();

  ODRX_DECLARE_MEMBERS(OdDbOsnapPointRef);

  OdDb::OsnapMode osnapMode() const;
  void setOsnapMode(OdDb::OsnapMode val);

  OdDbXrefFullSubentPath& mainEntity();
  OdDbXrefFullSubentPath& intersectEntity();

  double nearOsnap() const;
  void setNearOsnap(double val);

  OdGePoint3d& osnapPoint();

  OdDbOsnapPointRef* lastPointRef();
  void setLastPointRef(OdDbOsnapPointRefPtr pOsnapPointRef);

  void dwgIn(OdDbDwgFiler* pFiler);
  void dwgOut(OdDbDwgFiler* pFiler);
  void dxfOut(OdDbDxfFiler* pFiler);
private:
  OdDb::OsnapMode        m_OsnapMode;
  OdDbXrefFullSubentPath m_MainEntity;
  OdDbXrefFullSubentPath m_IntersectEntity;
  double                 m_dNearOsnap;
  OdGePoint3d            m_OsnapPoint;
  OdDbOsnapPointRefPtr   m_pLastPointRef;
};

/** Represents an associative dimension in OdDbDatabase.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbDimAssoc : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbDimAssoc);

  /** Description:
      Constructor (no arguments).
  */
  OdDbDimAssoc();

  enum RotatedType
  {
    kParallel       = 0,
    kPerpendicular  = 1
  };

  enum Associativity
  {
    kFirstPoint,
    kSecondPoint,
    kThirdPoint,
    kFourthPoint,
    kPointAmount
  };

  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);

  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);

  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;

  /** Description:
      Gets the Object ID of the dimension associated with the object (DXF 330).
  */
  OdDbObjectId dimension() const;

  /** Description:
      Sets the Object ID of the dimension associated with the object (DXF 330).
  */
  void setDimension(const OdDbObjectId& dimId);

  /** Description:
      Gets the rotated dimension type (DXF 71).
  */
  RotatedType rotatedDimType() const;

  /** Description:
      Sets the rotated dimension type (DXF 71).
  */
  void setRotatedDimType(RotatedType rotated);

  /** Description:
      Gets the associativity flag (DXF 90).
  */
  OdInt32  associativityFlag() const;

  /** Description:
      Gets the trans space Flag (DXF 70).
  */
  bool transSpace() const;

  /** Description:
      Sets the trans space flag (DXF 70).
  */
  void setTransSpace(bool flag);

  /** Description:
      Gets a specified OdDbOsnapPointRef from this object.

      Arguments:
      index (I) Index of OdDbOsnapPointRef object to get.

      Return Value:
      OdDbOsnapPointRefPtr.
  */
  OdDbOsnapPointRefPtr osnapPointRef(Associativity index) const;

  /** Description:
      Sets a specified OdDbOsnapPointRef for this object.

      Arguments:
      index (I) Index of OdDbOsnapPointRef object to set.
      pOsnapPointRef (I) The OdDbOsnapPointRefPtr values to set.
  */
  void setOsnapPointRef(Associativity index, OdDbOsnapPointRefPtr pOsnapPointRef);

  void modifiedGraphics(const OdDbObject* pObj);
};

typedef OdSmartPtr<OdDbDimAssoc> OdDbDimAssocPtr;

//
// Inlines
//
inline
OdHandleArray& OdDbXrefFullSubentPath::xrefObjHandles()
{ 
  return m_XrefObjHandles; 
}

inline
OdDb::OsnapMode OdDbOsnapPointRef::osnapMode() const
{
  return m_OsnapMode;
}

inline
void OdDbOsnapPointRef::setOsnapMode(OdDb::OsnapMode val)
{
  m_OsnapMode = val;
}

inline
OdDbXrefFullSubentPath& OdDbOsnapPointRef::mainEntity()
{
  return m_MainEntity;
}

inline
OdDbXrefFullSubentPath& OdDbOsnapPointRef::intersectEntity()
{
  return m_IntersectEntity;
}

inline
double OdDbOsnapPointRef::nearOsnap() const
{
  return m_dNearOsnap;
}

inline
void OdDbOsnapPointRef::setNearOsnap(double val)
{
  m_dNearOsnap = val;
}

inline
OdGePoint3d& OdDbOsnapPointRef::osnapPoint()
{
  return m_OsnapPoint;
}

inline
OdDbOsnapPointRef* OdDbOsnapPointRef::lastPointRef()
{
  return m_pLastPointRef;
}

inline
void OdDbOsnapPointRef::setLastPointRef(OdDbOsnapPointRefPtr pOsnapPointRef)
{
  m_pLastPointRef = pOsnapPointRef;
}

#include "DD_PackPop.h"

#endif // OD_DBDIMASSOC_H


