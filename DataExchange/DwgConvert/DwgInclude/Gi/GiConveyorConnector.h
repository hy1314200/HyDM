#ifndef __ODGICONVEYORCONNECTOR__
#define __ODGICONVEYORCONNECTOR__

#include "GiConveyorNode.h"
#include "OdArray.h"

#include "DD_PackPush.h"


/** Description:
    
    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiConveyorConnector : public OdGiConveyorInput, public OdGiConveyorOutput
{
  typedef OdArray<OdGiConveyorOutput*, OdMemoryAllocator<OdGiConveyorOutput*> > NodeArray;

  NodeArray             m_sources;
  OdGiConveyorGeometry* m_pDestGeom;
public:
  OdGiConveyorConnector();

  void addSourceNode(OdGiConveyorOutput& sourceNode);
  void removeSourceNode(OdGiConveyorOutput& sourceNode);
  void setDestGeometry(OdGiConveyorGeometry& destGeometry);
  OdGiConveyorGeometry& destGeometry() const;
  OdGiConveyorGeometry& geometry();
};

inline OdGiConveyorGeometry& OdGiConveyorConnector::geometry() { return *m_pDestGeom; }

#include "DD_PackPop.h"


#endif // __ODGICONVEYORCONNECTOR__
