#ifndef ODDBMATCHPROPERTIES_H
#define ODDBMATCHPROPERTIES_H

#include "RxObject.h"

class OdDbEntity;

/** Description:

    {group:OdDb_Classes}
*/
class OdDbMatchProperties	: public OdRxObject
{
public:
  enum Flags
  {
    kColorFlag          = 0x001,
    kLayerFlag          = 0x002,
    kLtypeFlag          = 0x004,
    kThicknessFlag      = 0x008,
    kLtscaleFlag        = 0x010,
    kTextFlag           = 0x020,
    kDimensionFlag      = 0x040,
    kHatchFlag          = 0x080,
    kLweightFlag        = 0x100,
    kPlotstylenameFlag  = 0x200,
    kSetAllFlagsOn      = 0x3FF
  };

	ODRX_DECLARE_MEMBERS(OdDbMatchProperties);

	virtual void copyProperties(OdDbEntity* pSrc, OdDbEntity* pDest, unsigned int flag) const;
};

#endif // ODDBMATCHPROPERTIES_H


