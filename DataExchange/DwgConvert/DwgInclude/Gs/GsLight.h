#ifndef _OdGsLight_h_Included_
#define _OdGsLight_h_Included_

#include "RxObject.h"

class OdGePoint3d;
class OdGeVector3d;

#include "DD_PackPush.h"

/** Description:

    {group:OdGs_Classes} 
*/
class FIRSTDLL_EXPORT OdGsLight : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdGsLight);

  enum LightType
  {
    kPointLight,
    kDistantLight,
    kSpotlight
  };

  enum LightAttenuation
  {
    kAttenNone,
    kAttenInverseLinear,
    kAttenInverseSquare
  };

  /** Description:
      The type of the light.
  */
  virtual LightType type() const = 0;
  /** Description:
      The position and target of the light.
  */
  virtual OdGePoint3d position() const = 0;
  virtual OdGePoint3d target() const = 0;
  virtual OdGeVector3d direction() const = 0;
  /** Description:
      The color of the light.
  */
  virtual ODCOLORREF color() const = 0;
  virtual double intensity() const = 0;
  virtual LightAttenuation attenuation() const = 0;
};

typedef OdSmartPtr<OdGsLight> OdGsLightPtr;

#include "DD_PackPop.h"

#endif // _OdGsLight_h_Included_
