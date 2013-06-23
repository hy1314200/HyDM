// RasterModule.h - interface of module, performing different operations on raster images

#ifndef __OD_RASTER_MODULE__
#define __OD_RASTER_MODULE__

#include "RxModule.h"

class OdGiRasterImage;
typedef OdSmartPtr<OdGiRasterImage> OdGiRasterImagePtr;

#include "DD_PackPush.h"

/** Description:

    {group:OdRx_Classes} 
*/
class TOOLKIT_EXPORT OdRxRasterServices : public OdRxModule
{
public:
  ODRX_DECLARE_MEMBERS(OdRxRasterServices);
  /** Description:
      Loads raster image. Returned pointer will be passed to OdGiViewportGeometry::rasterImageDc()
  */
  virtual OdGiRasterImagePtr loadRasterImage(const OdString &fileName) = 0;
  virtual OdGiRasterImagePtr loadRasterImage(OdStreamBuf *pBuf) = 0;

  /** Description:
      Save raster image to file
      file type is recognized by extension 
      transparent_color is index in palette of input image ( if output format 
      supports transparency )
  */
  virtual bool saveRasterImage( const OdGiRasterImage*, 
    const OdChar* path, int transparent_color = -1 ) = 0;
};

typedef OdSmartPtr<OdRxRasterServices> OdRxRasterServicesPtr;

#define RX_RASTER_SERVICES_APPNAME "RxRasterServices"

#include "DD_PackPop.h"

#endif // __OD_RASTER_MODULE__
