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



#ifndef OD_RTEXT_H
#define OD_RTEXT_H

#include "DD_PackPush.h"

#include "DbEntity.h"
class OdDbEntityImpl;

/** Description:
    Represents an RText entity in an OdDbDatabase.

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT RText : public OdDbEntity
{
public:
  ODRX_DECLARE_MEMBERS(RText);

  OdGeVector3d normal() const;
  void         setNormal(const OdGeVector3d &Extrusion);

  OdGePoint3d getPoint() const;
  void        setPoint(const OdGePoint3d &Point);

  double getRotAngle() const;
  void   setRotAngle(double RotAngle);

  double getHeight() const;
  void   setHeight(double Height);

  bool isStringExpression() const;
  void setToExpression(bool isExpression);

  bool enabledMTextSequences() const;
  void enableMTextSequences(bool Enable);

  OdString getStringContents() const;
  void     setStringContents(const OdString &str);

  OdDbObjectId textStyleId() const;
  OdString textStyleName() const;
  void setTextStyle(OdDbObjectId id);
  void setTextStyle(const OdChar * name);

  virtual OdResult dwgInFields(OdDbDwgFiler* pFiler);
  virtual void dwgOutFields(OdDbDwgFiler* pFiler) const;

  virtual OdResult dxfInFields(OdDbDxfFiler* pFiler);
  virtual void dxfOutFields(OdDbDxfFiler* pFiler) const;

  virtual bool worldDraw(OdGiWorldDraw* ) const;

  virtual OdResult transformBy(const OdGeMatrix3d& xform);

  void subClose();

protected:
  RText();
  RText(OdDbEntityImpl* pImpl);
};

typedef OdSmartPtr<RText> OdRTextPtr;

#include "DD_PackPop.h"

#endif  // OD_RTEXT_H


