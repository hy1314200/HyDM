#ifndef _OdFontServices_h_Included_
#define _OdFontServices_h_Included_

#include "RxObject.h"
#include "Gi/GiTextStyle.h"
#include "Gi/GiExport.h"

class OdDbDatabase;

/** Description:

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdFontServices : public OdRxObject
{
public:
  virtual void loadStyleRec(OdGiTextStyle& style, OdDbDatabase* pDb) const = 0;
  virtual OdString getFontFilePath(OdGiTextStyle& style, OdDbDatabase* pDb) const = 0;
  virtual OdString getBigFontFilePath(OdGiTextStyle& style, OdDbDatabase* pDb) const = 0;
  virtual OdFontPtr defaultFont() const = 0;
};

#define ODDB_FONT_SERVICES "OdDbFontServices"

typedef OdSmartPtr<OdFontServices> OdFontServicesPtr;

#endif
