
#ifndef __OD_VIEWPORT_DEFS__
#define __OD_VIEWPORT_DEFS__

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum OrthographicView
  {
    kNonOrthoView = 0,
    kTopView      = 1,
    kBottomView   = 2,
    kFrontView    = 3,
    kBackView     = 4,
    kLeftView     = 5,
    kRightView    = 6
  };

  enum RenderMode
  {
    k2DOptimized,
    kWireframe,
    kHiddenLine,
    kFlatShaded,
    kGouraudShaded,
    kFlatShadedWithWireframe,
    kGouraudShadedWithWireframe
  };
}

#endif // __OD_VIEWPORT_DEFS__
