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

#ifndef __ODDBMATERIAL_H__
#define __ODDBMATERIAL_H__

#include "DD_PackPush.h"

#include "Gi/GiMaterial.h"
#include "DbObject.h"

/** Description:
  This class represents Material properties for shaded entities.

  {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbMaterial : public OdDbObject
{
public:
  ODDB_DECLARE_MEMBERS(OdDbMaterial);

  OdDbMaterial();
  
  /** Description:
    Sets the *name* of this Material object.
    
    Arguments:
    name (I) Name.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual OdResult setName(
    const OdString& name);
  /** Description:
    Returns the *name* of this Material object.
  */
  virtual OdString name() const;


  /** Description:
    Sets the *description* of this Material object.
    
    Arguments:
    description (I) Description.
    
    Remarks:
    Returns eOk if successful, or an appropriate error code if not.
  */
  virtual void setDescription(
    const OdString& description);
  /** Description:
    Returns the *description* of this Material object.
  */
  virtual OdString description() const;


  /** Description:
    Sets the *ambient* color component of this Material object.
    
    Arguments:
    ambientColor (I) Ambient color.

    Remarks:    
    The *ambient* component component is most apparent when there is no direct illumination on the entity.
  */
  virtual void setAmbient(
    const OdGiMaterialColor& ambientColor);
    
  /** Description:
    Returns the *ambient* color component of this Material object.
    
    Arguments:
    ambientColor (O) Receives the *ambient* color.

    Remarks:    
    The *ambient* component component is most apparent when there is no direct illumination on the entity.
  */
  virtual void ambient(
    OdGiMaterialColor& ambientColor) const;

  /** Description:
    Sets the *diffuse* component of this Material object.
    
    Arguments:
    diffuseColor (I) Diffuse color.
    diffuseMap (I) Diffuse map.

    Remarks:    
    The *diffuse* component is most apparent when there is direct illumination on the entity.
  */
  virtual void setDiffuse(
    const OdGiMaterialColor& diffuseColor, 
    const OdGiMaterialMap& diffuseMap);
    
  /** Description:
    Sets the *diffuse* component of this Material object.
    
    Arguments:
    diffuseColor (O) Receives the *diffuse* color.
    diffuseMap (O) Receives the *diffuse* map.

    Remarks:    
    The *diffuse* component is most apparent when there is direct illumination on the entity.
  */
  virtual void diffuse(
    OdGiMaterialColor& diffuseColor, 
    OdGiMaterialMap& diffuseMap) const;


  /** Description:
    Sets the *specular* component of this Material object.
    
    Arguments:
    specularColor (I) Specular color.
    specularMap (I) Specular map.
    glossFactor (I) Gloss factor.
    
    Remarks:    
    The *specular* component is viewpoint dependent, and most apparent when the entity is highlighted.
  */
  virtual void setSpecular(
    const OdGiMaterialColor& specularColor, 
    const OdGiMaterialMap& specularMap, 
    double glossFactor);

  /** Description:
    Returns the *specular* component of this Material object.
    
    Arguments:
    specularColor (O) Receives the *specular* color.
    specularMap (O) Receives the *specular* map.
    glossFactor (O) Receives the gloss factor.
    
    Remarks:    
    The *specular* component is viewpoint dependent, and most apparent when the entity is highlighted.
  */
  virtual void specular(
    OdGiMaterialColor& specularColor, 
    OdGiMaterialMap& specularMap, 
    double& glossFactor) const;

  /** Description:
    Sets the *reflection* component of this Material object.
    
    Arguments:
    reflectionMap (I) Reflection map.
    
    Remarks:    
    The *reflection* component creates a mirror finish on the entity.
  */
  virtual void setReflection(
    const OdGiMaterialMap& reflectionMap);

  /** Description:
    Returns the *reflection* component of this Material object.
    
    Arguments:
    reflectionMap (O) Receives the *reflection* map.
    
    Remarks:    
    The *reflection* component creates a mirror finish on the entity .
  */
  virtual void reflection(
    OdGiMaterialMap& reflectionMap) const;


  /** Description:
    Sets the *opacity* component of this Material object.
    
    Arguments:
    opacityPercentage (I) Opacity percentage.
    opacityMap (I) Opacity map.
  */
  virtual void setOpacity(
    double opacityPercentage, 
    const OdGiMaterialMap& opacityMap);

  /** Description:
    Returns the *opacity* component of this Material object.
    
    Arguments:
    opacityPercentage (O) Receives the *opacity* percentage.
    opacityMap (O) Receives the *opacity* map.
  */
  virtual void opacity(
    double& opacityPercentage, 
    OdGiMaterialMap& opacityMap) const;

  /** Description:
    Sets the *bump* component of this Material object.
    
    Arguments:
    bumpMap (I) Bump map.
  */
  virtual void setBump(
    const OdGiMaterialMap& bumpMap);

  /** Description:
    Returns the *bump* component of this Material object.
    
    Arguments:
    bumpMap (O) Receives the *bump* map.
  */
  virtual void bump(
    OdGiMaterialMap& map) const;

  /** Description:
    Sets the *refraction* component of this Material object.
    
    Arguments:
    refractionIndex (I) Refraction index.
    refractionMap (I) Refraction map.
  */
  virtual void setRefraction(
    double refractionIndex, 
    const OdGiMaterialMap& refractionMap);

  /** Description:
    Returns the *refraction* component of this Material object.
    
    Arguments:
    refractionIndex (O) Receives the *refraction* index.
    refractionMap (O) Receives the *refraction* map.
  */
  virtual void refraction(
    double& refractionIndex, 
    OdGiMaterialMap& refractionMap) const;

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);
    
  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;
    
  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);
    
  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;

  /*
  virtual OdGiDrawable* drawable();
  virtual void setGsNode(OdGsNode* pNode);
  virtual OdGsNode* gsNode() const;
  */

  virtual OdUInt32 setAttributes(
    OdGiDrawableTraits* pTraits) const;
};
/** Description:
    This template class is a specialization of the OdSmartPtr class for OdDbMaterial object pointers.
*/
typedef OdSmartPtr<OdDbMaterial> OdDbMaterialPtr;

#include "DD_PackPop.h"

#endif
