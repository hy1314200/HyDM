
#ifndef __ODGISHELLFACEITERATOR__
#define __ODGISHELLFACEITERATOR__

#include "DD_PackPush.h"


/** Description:
    
    {group:OdGi_Classes} 
*/
class OdGiShellFaceIterator
{
  const OdGePoint3d* _points;
  const OdInt32* _faceList;
public:
  inline OdGiShellFaceIterator()
    : _points(0)
    , _faceList(0)
  {
  }

  inline OdGiShellFaceIterator(const OdGePoint3d* points, const OdInt32* faceList)
  {
    set(points, faceList);
  }

  inline void set(const OdGePoint3d* points, const OdInt32* faceList)
  {
    _points = points;
    _faceList = faceList;
  }

  inline const OdInt32* faceListPos() { return _faceList; }

  inline const OdGePoint3d& operator *() const
  {
    return _points[*_faceList];
  }
  inline operator const OdGePoint3d*() const
  {
    return _points + (*_faceList);
  }
  inline const OdGePoint3d* operator ->() const
  {
    return _points + *_faceList;
  }
  inline OdGiShellFaceIterator& operator ++()
  {
    ++_faceList;
    return *this;
  }
  inline OdGiShellFaceIterator operator ++(int)
  {
    OdGiShellFaceIterator res(*this);
    ++_faceList;
    return res;
  }
  inline OdGiShellFaceIterator& operator --()
  {
    --_faceList;
    return *this;
  }
  inline OdGiShellFaceIterator operator --(int)
  {
    OdGiShellFaceIterator res(*this);
    --_faceList;
    return res;
  }
  inline OdGiShellFaceIterator& operator += (const int n)
  {
    _faceList+=n;
    return *this;
  }
  inline OdGiShellFaceIterator& operator -= (const int n)
  {
    _faceList-=n;
    return *this;
  }
  inline OdGiShellFaceIterator operator + (const int n) const
  {
    OdGiShellFaceIterator res(*this);
    res+=n;
    return res;
  }
  inline OdGiShellFaceIterator operator - (const int n) const
  {
    OdGiShellFaceIterator res(*this);
    res-=n;
    return res;
  }
  inline bool operator < (const OdGiShellFaceIterator& op) const
  {
    return _faceList < op._faceList;
  }
  inline bool operator < (const OdInt32* op) const
  {
    return _faceList < op;
  }
};

#include "DD_PackPop.h"

#endif // __ODGISHELLFACEITERATOR__
