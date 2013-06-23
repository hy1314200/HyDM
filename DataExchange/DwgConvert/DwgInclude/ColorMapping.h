#ifndef COLORMAPPING_DEFINED
#define COLORMAPPING_DEFINED

#include "OdPlatform.h"

//DD:EXPORT_ON

// Background is dark if  Red < 128 && Green < 128 && Blue < 128
// Background is light if  Red > 127 || Green > 127 || Blue > 127
inline bool odcmIsBackgroundLight(ODCOLORREF backgroung)
{
  return (ODGETRED(backgroung) >= 128 ||
					ODGETGREEN(backgroung) >= 128 ||
					ODGETBLUE(backgroung) >= 128);
}

// Returns palette for dark background
FIRSTDLL_EXPORT const ODCOLORREF * odcmAcadDarkPalette();

// Returns palette for light background
FIRSTDLL_EXPORT const ODCOLORREF * odcmAcadLightPalette();

// Returns palette for given background
FIRSTDLL_EXPORT const ODCOLORREF * odcmAcadPalette(ODCOLORREF backgr);


// Returns RGB for given ACI (AutoCAD color index)
// pPalette - palette to perform mapping
FIRSTDLL_EXPORT ODCOLORREF odcmLookupRGB(int idx, const ODCOLORREF* pPalette);

// Returns ACI (AutoCAD color index) for given RGB
// pPalette - palette to perform mapping
FIRSTDLL_EXPORT int odcmLookupACI(ODCOLORREF rgb, const ODCOLORREF* pPalette);

//DD:EXPORT_OFF

#endif	// COLORMAPPING_DEFINED
