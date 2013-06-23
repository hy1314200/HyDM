
#ifndef __OD_GI_UTILS__
#define __OD_GI_UTILS__

template <class T3dPtIter>
void odgiSquareValues(OdInt32 nPoints, T3dPtIter pPoints, OdGeVector3d& n1, OdGeVector3d& n2)
{
  OdGeVector3d res;
  nPoints -= 2;
  n1 = n2 = OdGeVector3d::kIdentity;
  for(T3dPtIter pPt1 = pPoints + 1, pPt2 = pPoints + 2; nPoints-- > 0; ++pPt1, ++pPt2)
  {
    res = (*pPt2 - *pPoints).crossProduct(*pPt1 - *pPoints);
    if(res.dotProduct(n1) >= 0.)
      n1 += res;
    else
      n2 += res;
  }
}

template <class T3dPtIter>
OdGeVector3d odgiFaceNormal(OdInt32 nPoints, T3dPtIter pPoints)
{
  OdGeVector3d n1, n2;
  odgiSquareValues(nPoints, pPoints, n1, n2);
  n1 += n2;
  OdGe::ErrorCondition f;
  n1.normalize(OdGeContext::gZeroTol, f);
  if(f!=OdGe::kOk)
    return n2.normalize(OdGeContext::gZeroTol, f);
  return n1;
}


#endif // __OD_GI_UTILS__
