
#ifndef __OD_TEXT_DEFS__
#define __OD_TEXT_DEFS__

/** Description: 
  
    {group:DD_Namespaces}
*/
namespace OdDb
{
  enum TextHorzMode
  {
    kTextLeft   = 0,  // TH_LEFT,
    kTextCenter = 1,  // TH_CENT,
    kTextRight  = 2,  // TH_RIGHT,
    kTextAlign  = 3,  // THV_ALIGN,
    kTextMid    = 4,  // THV_MID,
    kTextFit    = 5
  }; // THV_FIT

  enum TextVertMode
  {
    kTextBase    = 0,  // TV_BASE,
    kTextBottom  = 1,  // TV_BOT,
    kTextVertMid = 2,  // TV_MID,
    kTextTop     = 3
  }; // TV_TOP
}

#endif // __OD_TEXT_DEFS__
