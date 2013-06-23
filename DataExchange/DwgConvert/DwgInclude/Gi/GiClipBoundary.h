#ifndef _GI_CLIP_BOUNDARY_H_ICLUDED_
#define _GI_CLIP_BOUNDARY_H_ICLUDED_

#include "DD_PackPush.h"

#include "RxObject.h"
#include "Ge/GePoint2dArray.h"
#include "Ge/GeVector3d.h"
#include "Ge/GeMatrix3d.h"


/** Description:
    Defines a clip boundary, which can be used, for example, to clip the contents of a block
    insertion (xclip functionality).

    {group:OdGi_Classes} 
*/
struct OdGiClipBoundary
{
  /** Description:
      Normal vector that defines the plane in which the clip boundary lies.
  */
  OdGeVector3d          m_vNormal;               

  /** Description:
      Target point of the clip boundary.
  */
  OdGePoint3d           m_ptPoint;

  /** Description:
      Points that define the clip boundary.
  */
  OdGePoint2dArray      m_Points;

  /** Description:
      Transformation from model to clip space.
  */
  OdGeMatrix3d          m_xToClipSpace;          

  /** Description:
      Transformation from block space to world space.
  */
  OdGeMatrix3d          m_xInverseBlockRefXForm;

  /** Description:
      Distance from the target point at which the front Z clipping plane is defined
      (used only if m_bClippingFront is true).
  */
  double                m_dFrontClipZ;

  /** Description:
      Distance from the target point at which the back Z clipping plane is defined
      (used only if m_bClippingBack is true).
  */
  double                m_dBackClipZ;

  /** Description:
      If true, Z clipping is applied at the m_dFrontClipZ distance from the target point.
  */
  bool                  m_bClippingFront;

  /** Description:
      If true, the clipping boundary itself is drawn, otherwise it is not drawn.
  */
  bool                  m_bDrawBoundary;

  /** Description:
      If true, Z clipping is applied at the m_dBackClipZ distance from the target point.
  */
  bool                  m_bClippingBack;
};

#include "DD_PackPop.h"

#endif
