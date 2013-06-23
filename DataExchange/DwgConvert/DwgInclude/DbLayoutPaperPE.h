#ifndef _DbLayoutPaperPE_h_Included_
#define _DbLayoutPaperPE_h_Included_

#include "SmartPtr.h"

class OdDbLayout;
class OdGePoint3d;

/** Description:
    Protocol extension class that can be registered on the OdDbLayout class, 
    to provide a vectorization client the opportunity to customize the drawing
    of the background paper during paperspace vectorization.
    
    Library:
    Db

    {group:OdDb_Classes} 
*/
class TOOLKIT_EXPORT OdDbLayoutPaperPE : public OdRxObject
{
public:
  ODRX_DECLARE_MEMBERS(OdDbLayoutPaperPE);

  /** Description:
      This fuction is called during vectorization to draw the paper.  

      Arguments:
      pThis (I) The layout that is being vectorized.
      pWd (I/O) Draw object that can be used by this function to set the 
        necessary attributes and create the necessary geometry.
      points (I) Contains the 4 corners of the paper.

      Return Value:
      true if the paper was successfully drawn, otherwise false, in which case
      the layout will do a default rendering of the paper.
  */
  virtual bool drawPaper(const OdDbLayout* pThis, OdGiWorldDraw* pWd, OdGePoint3d* points) = 0;

  /** Description:
      This fuction is called during vectorization to draw the paper border.

      Arguments:
      pThis (I) The layout that is being vectorized.
      pWd (I/O) Draw object that can be used by this function to set the 
        necessary attributes and create the necessary geometry.
      points (I) Contains the 4 corners of the paper.

      Return Value:
      true if the border was successfully drawn, otherwise false, in which case
      the layout will do a default rendering of the border.
  */
  virtual bool drawBorder(const OdDbLayout* pThis, OdGiWorldDraw* pWd, OdGePoint3d* points) = 0;

  /** Description:
      This fuction is called during vectorization to draw the paper margins.

      Arguments:
      pThis (I) The layout that is being vectorized.
      pWd (I/O) Draw object that can be used by this function to set the 
        necessary attributes and create the necessary geometry.
      points (I) Contains the 4 margin points.

      Return Value:
      true if the margins were successfully drawn, otherwise false, in which case
      the layout will do a default rendering of the margins.
  */
  virtual bool drawMargins(const OdDbLayout* pThis, OdGiWorldDraw* pWd, OdGePoint3d* points) = 0;
};

typedef OdSmartPtr<OdDbLayoutPaperPE> OdDbLayoutPaperPEPtr;

#endif //_DbLayoutPaperPE_h_Included_
