
#ifndef __ODGISHELLFACEWITHHOLESITERATOR__
#define __ODGISHELLFACEWITHHOLESITERATOR__

#include "DD_PackPush.h"

/** Description:
    
    {group:OdGi_Classes} 
*/
class OdGiShellFaceWithHolesIterator
{
  const OdGePoint3d* _points;
  const OdInt32* _faceList;
  OdInt32 _loopSize;
public:
  OdInt32Array loopCounts;

  OdGiShellFaceWithHolesIterator()
    : _points(points)
    , _faceList(faceList)
    , _loopSize(0)
  {
  }

  OdGiShellFaceWithHolesIterator(const OdGePoint3d* points, const OdInt32* faceList)
    : _points(points)
    , _faceList(faceList)
    , _loopSize(0)
  {
    loopCounts.push_back(_loopSize = *faceList);
  }

  const OdGePoint3d& operator *() const
  {
    return _points[*_faceList];
  }
  const OdGePoint3d* operator ->() const
  {
    return _points + *_faceList;
  }
  OdGiShellFaceIterator& operator ++()
  {
    if(_loopSize--)
      loopCounts.push_back(_loopSize = *faceList);
    ++_faceList;
    return *this;
  }
  OdGiShellFaceIterator operator ++(int)
  {
    OdGiShellFaceIterator res(*this);
    ++(*this);
    return res;
  }
};

#include "DD_PackPop.h"

#endif // __ODGISHELLFACEWITHHOLESITERATOR__
