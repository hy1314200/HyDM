#ifndef __ODGICONVEYORENTRYPOINT__
#define __ODGICONVEYORENTRYPOINT__


#include "GiEmptyGeometry.h"

#include "DD_PackPush.h"

/** Description:
    
    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiConveyorEntryPoint : public OdGiConveyorOutput
{
protected:
  OdGiConveyorGeometry* m_pGeometry;
public:
  OdGiConveyorEntryPoint();
  OdGiConveyorEntryPoint(OdGiConveyorGeometry& geom);

  OdGiConveyorGeometry& geometry();

  void setDestGeometry(OdGiConveyorGeometry& destGeometry);
  OdGiConveyorGeometry& destGeometry() const;
};


inline OdGiConveyorGeometry& OdGiConveyorEntryPoint::geometry()
{
  return *m_pGeometry;
}


#include "DD_PackPop.h"

#endif // __ODGICONVEYORENTRYPOINT__
