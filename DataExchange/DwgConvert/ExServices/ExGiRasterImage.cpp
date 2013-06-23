#include "OdaCommon.h"
#include "RxObjectImpl.h"
#include "../ExServices/ExGiRasterImage.h"
#include "OdRound.h"

// for ExRasterModule
#include "OdPlatformStreamer.h"
#include "OdString.h"

// ExGiRasterImage implementation
#include "DbSystemServices.h" // (?)
#include "RxDynamicModule.h"

#if defined(_MSC_VER)
#pragma warning ( disable : 4100 ) //  unreferenced formal parameter
#endif

OdExGiRasterImage::OdExGiRasterImage(OdUInt32 width, OdUInt32 height, OdUInt8 bitCount)
  : m_header(width, height, bitCount)
{
}

OdExGiRasterImage::OdExGiRasterImage()
  : m_header(0, 0, 0)
{
}

// Gets the color(RGB) in specified point of raster image
void OdExGiRasterImage::getColorAt(OdUInt32 x, OdUInt32 y, OdUInt8& blue, OdUInt8& green, OdUInt8& red) const
{
/* OdUInt32 width  = width();
  OdUInt32 height = height();*/
  int lineSize = scanLineSize();
  ODA_ASSERT(x < m_header.m_width);
  ODA_ASSERT(y < m_header.m_height);
  const OdUInt8* pIm = m_bits.asArrayPtr();
  const OdUInt8* ptr = pIm + y * lineSize + x * m_header.m_bitPerPixel / 8;
  // const OdUInt8* palPtr = bmpInfo.m_palette.asArrayPtr();
  if(m_header.m_bitPerPixel == 24)
  {
    red   = ptr[2];
    green = ptr[1];
    blue  = ptr[0];
    // return ;//OdGePoint3d(ptr[2], ptr[1], ptr[0]);
  }
  else 
  {
    OdUInt32 index = 
      (
      ptr[0] >> 
      (
      ((8 / m_header.m_bitPerPixel) - 1 - (x % (8 / m_header.m_bitPerPixel)))
      * m_header.m_bitPerPixel
      )
      ) 
      & 
      ((1 << m_header.m_bitPerPixel) - 1);
      /* index *= 4; // Hmm...
    return OdGePoint3d(palPtr[index + 2], palPtr[index + 1], palPtr[index + 0]);*/
    getPalColorAt(index, blue, green, red);
  }
}

// overrides
OdUInt32 OdExGiRasterImage::pixelWidth() const
{
  return m_header.m_width;
}

OdUInt32 OdExGiRasterImage::pixelHeight() const
{
  return m_header.m_height;
}

int OdExGiRasterImage::colorDepth() const
{
  return m_header.m_bitPerPixel;
}

OdGiRasterImage::Units OdExGiRasterImage::defaultResolution(double& xPelsPerUnit, double& yPelsPerUnit) const
{
  xPelsPerUnit = m_header.m_xPelsPerUnit;
  yPelsPerUnit = m_header.m_yPelsPerUnit;
  return m_header.m_resUnits;
}

// applying Brightness, Contrast, Fade to the pixel
inline void applyBrightness(OdUInt8 oldB, OdUInt8 oldG, OdUInt8 oldR, 
                            OdUInt8& newB, OdUInt8& newG, OdUInt8& newR,
                            double brightness)
{
  if(brightness < 50.0)
  {
    ODA_ASSERT(brightness >= 0.0);
    newB = (OdUInt8)OdRound((double)oldB * (brightness / 50.0));
    newG = (OdUInt8)OdRound((double)oldG * (brightness / 50.0));
    newR = (OdUInt8)OdRound((double)oldR * (brightness / 50.0));
  }
  else if(brightness > 50.0)
  {
    ODA_ASSERT(brightness <=100.0);
    newB = (OdUInt8)(oldB + (OdUInt8)OdRound((double)(255 - oldB) * ((brightness - 50.0) / 50.0)));
    newG = (OdUInt8)(oldG + (OdUInt8)OdRound((double)(255 - oldG) * ((brightness - 50.0) / 50.0)));
    newR = (OdUInt8)(oldR + (OdUInt8)OdRound((double)(255 - oldR) * ((brightness - 50.0) / 50.0)));
  }
  else
  {
    newB = oldB;
    newG = oldG;
    newR = oldR;
  }
}

inline void applyContrast(OdUInt8 oldB, OdUInt8 oldG, OdUInt8 oldR, 
                          OdUInt8& newB, OdUInt8& newG, OdUInt8& newR,
                          double contrast)
{
  if(contrast < 50.0)
  {
    ODA_ASSERT(contrast >= 0.0);
    newB = (OdUInt8)(oldB - (OdUInt8)OdRound(((double)oldB - 127.0) * ((50.0 - contrast) / 50.0)));
    newG = (OdUInt8)(oldG - (OdUInt8)OdRound(((double)oldG - 127.0) * ((50.0 - contrast) / 50.0)));
    newR = (OdUInt8)(oldR - (OdUInt8)OdRound(((double)oldR - 127.0) * ((50.0 - contrast) / 50.0)));
  }
  else if(contrast > 50.0)
  {
    ODA_ASSERT(contrast <= 100.0);
    double nm, nm1, nm2;
    OdUInt8 minComp, maxComp;
    if((oldB <= oldG) && (oldB <= oldR))
    {
      minComp = oldB;
    }
    else if((oldG <= oldB) && (oldG <= oldR))
    {
      minComp = oldG;
    }
    else
    {
      minComp = oldR;
    }
    
    if((oldB >= oldG) && (oldB >= oldR))
    {
      maxComp = oldB;
    }
    else if((oldG >= oldB) && (oldG >= oldR))
    {
      maxComp = oldG;
    }
    else
    {
      maxComp = oldR;
    }
    
    nm1 = (double)minComp / (127.0 - (double)minComp); // min
    nm2 = (255.0 - (double)maxComp) / ((double)maxComp - 127.0); // max
    
    if((minComp < 127) && (maxComp > 127))
    {
      nm = odmin(nm1, nm2);
    }
    else if(minComp < 127)
    {
      nm = nm1;
    }
    else if(maxComp > 127)
    {
      nm = nm2;
    }
    else
    {
      nm = 1.0;
    }
    newB = (OdUInt8)(oldB + (OdUInt8)OdRound(((double)oldB - 127.0) * (((double)contrast - 50.0) / 50.0) * nm));
    newG = (OdUInt8)(oldG + (OdUInt8)OdRound(((double)oldG - 127.0) * (((double)contrast - 50.0) / 50.0) * nm));
    newR = (OdUInt8)(oldR + (OdUInt8)OdRound(((double)oldR - 127.0) * (((double)contrast - 50.0) / 50.0) * nm));
  }
  else
  {
    newB = oldB;
    newG = oldG;
    newR = oldR;
  }
}

inline void applyFade(OdUInt8 oldB, OdUInt8 oldG, OdUInt8 oldR, 
                      OdUInt8& newB, OdUInt8& newG, OdUInt8& newR,
                      double fade)
{
  ODA_ASSERT((fade >= 0.0) && (fade <= 100.0));
  newB = (OdUInt8)OdRound((double)oldB * ((100.0 - fade) / 100.0));
  newG = (OdUInt8)OdRound((double)oldG * ((100.0 - fade) / 100.0));
  newR = (OdUInt8)OdRound((double)oldR * ((100.0 - fade) / 100.0));
}

void OdExGiRasterImage::applyBCF(double brightness, double contrast, double fade)
{
  if(m_header.m_bitPerPixel == 24)
  {
    // applying it to each pixel
    OdUInt32 byteWidth = scanLineSize();
    OdUInt32 i, j;
    OdBinaryData& scanLines = bits();
    for(i = 0; i < m_header.m_height; i ++)
    {
      for(j = 0; j < m_header.m_width; j ++)
      {
        OdUInt8 B1 = scanLines[i*byteWidth + j*3];
        OdUInt8 G1 = scanLines[i*byteWidth + j*3 + 1];
        OdUInt8 R1 = scanLines[i*byteWidth + j*3 + 2];
        OdUInt8 B2, G2, R2;
        applyBrightness(B1, G1, R1, B2, G2, R2, brightness);
        applyContrast(B2, G2, R2, B1, G1, R1, contrast);
        applyFade(B1, G1, R1, scanLines[i*byteWidth + j*3], 
          scanLines[i*byteWidth + j*3 + 1], 
          scanLines[i*byteWidth + j*3 + 2], fade);
      }
    }
  }
  else
  {
    // applying it to palette
    OdUInt32 i;
    for(i = 0; i < getPalNumColors(); i ++)
    {
      OdUInt8 B1, G1, R1;
      OdUInt8 B2, G2, R2;
      getPalColorAt(i, B1, G1, R1);
      applyBrightness(B1, G1, R1, B2, G2, R2, brightness);
      applyContrast(B2, G2, R2, B1, G1, R1, contrast);
      applyFade(B1, G1, R1, B2, G2, R2, fade);
      setPalColorAt(i, B2, G2, R2);
    }
  }
}

void OdExGiRasterImage::copyFrom(const OdRxObject* pOtherObj)
{
  OdExGiRasterImage* pFrom = (OdExGiRasterImage*)pOtherObj;
  ODA_ASSERT(pFrom);
  m_header  = pFrom->m_header;
  m_bits    = pFrom->m_bits;
  m_palette = pFrom->m_palette;
}

/** Returns bitmap info header
*/
/*
void OdExGiRasterImage::getBitmapInfoHeader(BITMAPINFOHEADER& bmih) const
{
  bmih.biSize          = 40;
  bmih.biBitCount      = m_header.m_bitPerPixel;
  bmih.biCompression   = 0;
  bmih.biHeight        = m_header.m_height;
  bmih.biWidth         = m_header.m_width;
  bmih.biPlanes        = 1;
  bmih.biSizeImage     = 0;
  bmih.biClrImportant  = 0;
  bmih.biClrUsed       = 0;
  bmih.biXPelsPerMeter = m_header.m_resUnits == kNone ? 0 : (long)m_header.m_xPelsPerUnit;
  bmih.biYPelsPerMeter = m_header.m_resUnits == kNone ? 0 : (long)m_header.m_yPelsPerUnit;
}*/

// MKU 05.04.2003
//  ---  OdGiRasterImage interface  ---

// Returns the number of colors in the palette of the image
OdUInt32 OdExGiRasterImage::numColors() const
{
  return getPalNumColors();
}

// Returns the color by index from the palette of the image
// Returns the color by index from the palette of the image
ODCOLORREF OdExGiRasterImage::color(OdUInt32 nIndex) const
{
  OdUInt8 blue, green, red, alpha;
  getPalColorAt(nIndex, blue, green, red, &alpha);
  return ODRGBA(red, green, blue, alpha);
}

// Returns the number of accessible bytes in the scanline.
//  Note: each scanline is padded so that they end on a byte boundary.
/*
OdUInt32 OdExGiRasterImage::getScanLineWidth() const
{
  OdUInt32  width; 
  OdUInt32  height;
  size(width, height);
  
  OdUInt32 round = 0;
  if (nBitsOnByte > colorDepth())
  {
    round = nBitsOnByte / colorDepth() - 1;
  }
  
  return ((width * colorDepth() + round) / nBitsOnByte);
}*/

// Returns pointer on the scanline of the image by index without any stuff.
//  Note: The number of accessible scanlines is equal to value 'height' (see size() call).
//        The number of accessible bytes  in the scanline is equal to getScanLineWidth() returning value.
//        The scanline gotten by an index 0 is the first.
//        The scanline gotten by an index (height - 1) is the last.
/*
const OdUInt8* OdExGiRasterImage::getScanLine(OdUInt32 index) const
{
  OdUInt32  widthScanLine; 
  OdUInt32  height;
  
  size(widthScanLine, height);
  
  widthScanLine = getScanLineWidth();
  
  ODA_ASSERT(index < height);
  
  OdUInt32 numBytes(0);
  const OdUInt8* pData = getScanLines(numBytes);
  
  ODA_ASSERT(pData);
  ODA_ASSERT((numBytes / height) >= widthScanLine);
  
  OdUInt32 stuff = (numBytes / height) - widthScanLine;
  
  return (pData + ((height - index - 1) * (widthScanLine + stuff)));
}*/

OdUInt32 OdExGiRasterImage::scanLineSize() const
{
  return OdGiRasterImage::calcScanLineSize(m_header.m_width, m_header.m_bitPerPixel);
}

void OdExGiRasterImage::scanLines(OdUInt8* pBytes, OdUInt32 index, OdUInt32 numLines) const
{
  OdUInt32 scLSz(scanLineSize());
  memcpy(pBytes, m_bits.asArrayPtr() + index*scLSz, numLines*scLSz);
}

/** Description:
    applying palette transformation to bitanal images.
    If image isn't bitonal - does nothing
*/
void OdExGiRasterImage::applyBitonalPaletteTransform(ODCOLORREF traitsColor, ODCOLORREF bgColor)
{
  if(m_palette.numColors() != 2)
    return ; // do nothing

  int darkColor, lightColor;
  
  OdUInt8 red1, blue1, green1, red2, blue2, green2;
  m_palette.colorAt(0, blue1, green1, red1);
  m_palette.colorAt(1, blue2, green2, red2);

  if(30*red1 + 59*green1 + 11*blue1 > 30*red2 + 59*green2 + 11*blue2)
  {
    lightColor = 0;
    darkColor  = 1;
  }
  else
  {
    lightColor = 1;
    darkColor  = 0;
  }

  m_palette.setColorAt(lightColor, ODGETBLUE(bgColor), ODGETGREEN(bgColor), ODGETRED(bgColor));
  m_palette.setColorAt(darkColor, ODGETBLUE(traitsColor), ODGETGREEN(traitsColor), ODGETRED(traitsColor));
}

/** Description:
    Returns pointer to image's pixels in bmp format.
    Note that it is optional to have implementation of this function
    (to prevent dummy copying of pixels data). You can return NULL in your
    implementation if it is inconvenient in some case - any functionality
    uses this interface must take it into account.
*/
const OdUInt8* OdExGiRasterImage::scanLines() const
{
  return m_bits.asArrayPtr();
}


// ExRasterModule implementation

/** Returns the class, loading the raster images from different formats
*/

#ifdef RASTER_JPEG6B

#undef FAR

extern "C"
{
#include "jpeglib.h"
}

/*
 * <setjmp.h> is used for the optional error recovery mechanism.
 */

#include <setjmp.h>

struct my_error_mgr 
{
  struct jpeg_error_mgr pub;	/* "public" fields */

  jmp_buf setjmp_buffer;	/* for return to caller */
};

typedef struct my_error_mgr * my_error_ptr;

static void my_error_exit (j_common_ptr cinfo)
{
  /* cinfo->err really points to a my_error_mgr struct, so coerce pointer */
  my_error_ptr myerr = (my_error_ptr) cinfo->err;

  /* Always display the message. */
  /* We could postpone this until after returning, if we chose. */
  (*cinfo->err->output_message) (cinfo);

  /* Return control to the setjmp point */
  longjmp(myerr->setjmp_buffer, 1);
}

static OdGiRasterImagePtr loadJpegImage(OdString fileName)
{
  OdExGiRasterImagePtr pExRastIm = OdRxObjectImpl<OdExGiRasterImage>::createObject();
  
  OdUInt32 i, j;
  /* This struct contains the JPEG decompression parameters and pointers to
  * working space (which is allocated as needed by the JPEG library).
  */
  struct jpeg_decompress_struct cinfo;
  struct my_error_mgr jerr;
  /* Step 1: allocate and initialize JPEG decompression object */
  
  // ...
  
  /* Now we can initialize the JPEG decompression object. */
  /* We set up the normal JPEG error routines, then override error_exit. */
  cinfo.err = jpeg_std_error(&jerr.pub);
  jerr.pub.error_exit = my_error_exit;
  /* Establish the setjmp return context for my_error_exit to use. */
  if (setjmp(jerr.setjmp_buffer))
  {
  /* If we get here, the JPEG code has signaled an error.
  * We need to clean up the JPEG object, close the input file, and return.
    */
    jpeg_destroy_decompress(&cinfo);
    return OdGiRasterImagePtr();
  }
  jpeg_create_decompress(&cinfo);
  
  /* Step 2: specify data source (eg, a file) */
  
  FILE *infile = fopen(fileName, "rb");
  if (NULL != infile)
  {
    jpeg_stdio_src(&cinfo, infile);
    /* Step 3: read file parameters with jpeg_read_header() */
  
    (void) jpeg_read_header(&cinfo, TRUE);
    /* Step 4: set parameters for decompression */
  
    /* setting output color space to RGB
    */
    cinfo.out_color_space = JCS_RGB;
  
    /* Step 5: Start decompressor */
  
    (void) jpeg_start_decompress(&cinfo);
  
    pExRastIm->setMetrics(cinfo.output_width, cinfo.output_height, 24);
  
    /* Step 6: while (scan lines remain to be read) */
    /*           jpeg_read_scanlines(...); */
    int bmpLineSize = pExRastIm->scanLineSize();//(header.m_header.m_width * 3/*number of components*/ + 3) & ~3;
  
    //scanLines.resize(cinfo.output_height * bmpLineSize);
    pExRastIm->bits().resize(cinfo.output_height * bmpLineSize);
    i = 0;
    while (cinfo.output_scanline < cinfo.output_height)
    {
      OdUInt8* ptr = /*scanLines.asArrayPtr()*/pExRastIm->bits().asArrayPtr()
        + (cinfo.output_height - i - 1) * 
        bmpLineSize;
      jpeg_read_scanlines(&cinfo, &ptr, 1);
      for(j = 0; j < cinfo.output_width; j ++)
      {
        OdUInt8 tmp;
        tmp = ptr[0];
        ptr[0] = ptr[2];
        ptr[2] = tmp;
        ptr += 3;
      }
      i ++;
    }
  
    /* Step 7: Finish decompression */
  
    jpeg_finish_decompress(&cinfo);
  
    /* At this point you may want to check to see whether any corrupt-data
    * warnings occurred (test whether jerr.pub.num_warnings is nonzero).
    */
    fclose(infile);
  }
  /* Step 8: Release JPEG decompression object */

  /* This is an important step since it will release a good deal of memory. */
  jpeg_destroy_decompress(&cinfo);

  /* And we're done! */
  return pExRastIm;
}

#elif defined(RASTER_SNOW_BOUND)

#include "WINDOWS.H" // windows-specific
#pragma comment(lib, "SNBD7W9S.LIB" )
#include "../../../ThirdParty/SnowBound/IMGLIB.H"

static int raster_to_dib(char far* buffer, 
                         void far* private_data,
                         int ypos,
                         int bytes)
{
  OdUInt32 width, height;
  width  = ((OdExGiRasterImage*)private_data)->pixelWidth();
  height = ((OdExGiRasterImage*)private_data)->pixelHeight();
  OdUInt8* sl = ((OdExGiRasterImage*)private_data)->bits().asArrayPtr();
  memcpy(sl + (height - ypos - 1) * bytes, buffer, bytes);
  return 0;
}

static int set_header(LPBITMAPINFOHEADER lpHeader, 
                      void far* private_data)
{
  ((OdExGiRasterImage*)private_data)->setMetrics(lpHeader->biWidth, lpHeader->biHeight, 
    (OdUInt8)(lpHeader->biBitCount));

  // Resizing...
  ((OdExGiRasterImage*)private_data)->bits().resize(lpHeader->biSizeImage);

  // Loading palette...
  if(lpHeader->biBitCount != 24)
  {
    ((OdExGiRasterImage*)private_data)->palette().data().resize(1 << (lpHeader->biBitCount + 2));
    memcpy(((OdExGiRasterImage*)private_data)->palette().data().asArrayPtr(),
      ((OdUInt8*)lpHeader) + sizeof(BITMAPINFOHEADER), 
      1 << (lpHeader->biBitCount + 2));
  }
  return 0;
}

#endif


OdGiRasterImagePtr ExRasterModule::loadRasterImage(OdStreamBuf *pBuf)
{
  OdExGiRasterImagePtr pExRastIm = OdRxObjectImpl<OdExGiRasterImage>::createObject();

  OdExGiRasterImage& bitmap = (*pExRastIm);
  
  OdUInt32 size;
  
  OdUInt32 startPos = pBuf->tell();
  OdInt16 type = OdPlatformStreamer::rdInt16(*pBuf); // type
  if(type != 19778) // BMP
  {
    // scanLines.clear();
    bitmap.bits().clear();
    return OdGiRasterImagePtr();
  }
  size = OdPlatformStreamer::rdInt32(*pBuf);
  OdPlatformStreamer::rdInt32(*pBuf); // reserved
  OdUInt32 scanLinesPos = OdPlatformStreamer::rdInt32(*pBuf); // offBits
  
  // This is a header(OdGiBitmapInfoHeader or OdGiBitmapCoreHeader) position
  //OdUInt32 headerPos = pFileBuf->tell();
  OdUInt32 headerLen = OdPlatformStreamer::rdInt32(*pBuf);
  OdUInt32 compr(0L);
  OdUInt32 width, height;
  OdUInt8 colDepth;
  if(headerLen == 40)
  {
    // OdGiBitmapInfoHeader used
    // header.m_header.m_size = headerLen;
    width  = OdPlatformStreamer::rdInt32(*pBuf);
    height = OdPlatformStreamer::rdInt32(*pBuf);
    //header.m_header.m_planes =
    OdPlatformStreamer::rdInt16(*pBuf);
    colDepth = (OdUInt8)OdPlatformStreamer::rdInt16(*pBuf);
    compr = OdPlatformStreamer::rdInt32(*pBuf);
    //header.m_header.m_sizeImage =
    OdPlatformStreamer::rdInt32(*pBuf);
    double xPelsPerMeter = OdPlatformStreamer::rdInt32(*pBuf);
    double yPelsPerMeter = OdPlatformStreamer::rdInt32(*pBuf);
    bitmap.setDefaultResolution(OdGiRasterImage::kMeter, xPelsPerMeter, yPelsPerMeter);

    //header.m_header.m_clrUsed =
    OdPlatformStreamer::rdInt32(*pBuf);
    //header.m_header.m_clrImportant =
    OdPlatformStreamer::rdInt32(*pBuf);
    bitmap.setMetrics(width, height, colDepth);
  }
  else
  {
    // OdGiBitmapCoreHeader used
    ODA_ASSERT(headerLen == 12);
    // header.m_header.m_size = headerLen;
    width = (OdUInt32)OdPlatformStreamer::rdInt16(*pBuf);
    height = (OdUInt32)OdPlatformStreamer::rdInt16(*pBuf);
    /*
    header.m_header.m_planes =
    */
    OdPlatformStreamer::rdInt16(*pBuf);
    colDepth = (OdUInt8)OdPlatformStreamer::rdInt16(*pBuf);
    /*
    header.m_header.m_compression = 0L; // No compression
    header.m_header.m_sizeImage = 0L; // default
    header.m_header.m_xPelsPerMeter = 0L; // default
    header.m_header.m_yPelsPerMeter = 0L; // default
    header.m_header.m_clrUsed = 0L; // default
    header.m_header.m_clrImportant = 0L; // default
    */
    bitmap.setMetrics(width, height, colDepth);
  }
  if(compr != 0L)
  {
    bitmap.bits().clear();
    return OdGiRasterImagePtr(); // such bitmaps is not supported yet
  }
  // palette
  if(scanLinesPos!=0) // if zero -- there is no palette (cr1475, GU)
  {
    OdUInt32 paletteSize = scanLinesPos - (pBuf->tell() - startPos);
    bitmap.palette().data().resize(paletteSize);
    if(paletteSize)
      pBuf->getBytes(bitmap.palette().data().asArrayPtr(), paletteSize);
  }
  OdUInt32 scanLinesSize = bitmap.scanLineSize() * height;
  bitmap.bits().resize(scanLinesSize);
  pBuf->getBytes(bitmap.bits().asArrayPtr(), scanLinesSize);

  return pExRastIm; 
}

OdGiRasterImagePtr ExRasterModule::loadRasterImage(const OdString &fileName)
{
#ifdef RASTER_JPEG6B
  
  {
    OdString right3 = fileName.right(3);
    OdString right4 = fileName.right(4);
    right3.makeLower();
    right4.makeLower();

    if((right3 == "jpg") || (right4 == "jpeg"))
    {
      return loadJpegImage(fileName);
    }
    else if((right3 == "bmp") || (right3 == "dib") || (right3 == "rle"))
    {
      OdStreamBufPtr pFileBuf;
      try
      {
        pFileBuf = odSystemServices()->createFile(fileName);
      }
      catch (...)
      {
        return OdGiRasterImagePtr();
      }
      return loadRasterImage(pFileBuf);
    }
    else
    {
      return OdGiRasterImagePtr();
    }
  }

#elif defined(RASTER_SNOW_BOUND)

  {
    OdExGiRasterImagePtr pExRastIm = OdRxObjectImpl<OdExGiRasterImage>::createObject();
    HFILE fd = _lopen( fileName, OF_READ );
    if(fd!=HFILE_ERROR)
    {
      int imghandle = IMGLOW_decompress_bitmap( fd, 0, 0, raster_to_dib, 
        pExRastIm, set_header );
      _lclose( fd );
      // int imghandle = IMG_decompress_bitmap((char*)name.c_str());
      if(imghandle > 0)
      {
        IMG_delete_bitmap(imghandle);
      }
      return pExRastIm;
    }
    return OdGiRasterImagePtr(); // File not found, or format is not supported
  }

#else

  OdString right3 = fileName.right(3);
  right3.makeLower();
  if((right3 == "bmp") || (right3 == "dib") || (right3 == "rle"))
  {
    OdStreamBufPtr pFileBuf;
    try
    {
      pFileBuf = odSystemServices()->createFile(fileName);
    }
    catch (...)
    {
      return OdGiRasterImagePtr();
    }
    return loadRasterImage(pFileBuf);
  }
  else
  {
    return OdGiRasterImagePtr();
  }

#endif
}

#ifdef RASTER_SNOW_BOUND
static int getSBRasterType( const OdString& s )
{
  // return IMGLOW_get_filetype( const_cast<char*>( s.c_str() ) );
  OdString ext = s.right(4);
  ext.makeLower();
  if ( ext == ".tif" || ext == "tiff" )
    return TIFF_UNCOMPRESSED;
  else if ( ext == ".bmp" )
    return BMP_UNCOMPRESSED;
  else if ( ext == ".gif" )
    return GIF;
  else if ( ext == ".jpg" || ext == "jpeg" )
    return JPEG;
  else if ( ext == ".png" )
    return PNG;
  else return -1;
}

int get_dib_data( char FAR *private_data, char FAR *dst_ptr, int ypos, int rast_size )
{
  const OdExGiRasterImage* img = (const OdExGiRasterImage*)private_data;
  const OdUInt8* sl = img->getBits().asArrayPtr() + ypos*rast_size;
    // (?)assuming rast_size is scan line size
  std::copy( sl, sl + rast_size, dst_ptr );
  return rast_size;
}

#endif
bool ExRasterModule::saveRasterImage( const OdGiRasterImage* img, const OdChar* path, int )
{
#if defined(RASTER_JPEG6B)
  // TODO: save as JPEG or BMP
  return false;
#elif defined(RASTER_SNOW_BOUND)
  int type = getSBRasterType( path );
  if ( type < 0 ) return false; // unknown extension
  int fd = _lcreat( path, OF_WRITE );
  OdBinaryData headerMemCont;
  headerMemCont.resize( sizeof(BITMAPINFOHEADER) + 4 * img->numColors() );
  BITMAPINFO* pHeader = (BITMAPINFO*)headerMemCont.getPtr();
  // img->getBitmapInfoHeader( pHeader->bmiHeader );
  pHeader->bmiHeader.biBitCount     = img->colorDepth();
  pHeader->bmiHeader.biClrImportant = 0;
  pHeader->bmiHeader.biClrUsed      = 0;
  pHeader->bmiHeader.biCompression  = 0;
  pHeader->bmiHeader.biHeight       = img->pixelHeight();
  pHeader->bmiHeader.biPlanes       = 1;
  pHeader->bmiHeader.biSize         = 40;
  pHeader->bmiHeader.biSizeImage    = 0;
  pHeader->bmiHeader.biWidth        = img->pixelWidth();
  double xPelsPerMeter, yPelsPerMeter;
  img->defaultResolution(xPelsPerMeter, yPelsPerMeter);
  pHeader->bmiHeader.biXPelsPerMeter = (long)xPelsPerMeter;
  pHeader->bmiHeader.biYPelsPerMeter = (long)yPelsPerMeter;

  /*OdUInt32 pal_size = 0;
  memcpy( pHeader->bmiColors, img->getPaletteData( pal_size ), img->numColors()*4);*/
  img->paletteData((OdUInt8*)pHeader->bmiColors);
  int retval = IMGLOW_save_bitmap( fd, &pHeader->bmiHeader, type, get_dib_data, (void*)img );
  _lclose( fd );
  if ( retval < 0 ) 
  {
    ODA_TRACE1( "IMGLOW_save_bitmap() failed, error code = %d\n", retval );
  }
  return retval >= 0;
#else
  // TODO: save as bmp
  return false;
#endif
}

/** Description:
    The user override of this function should register any custom objects defined in the 
    custom application, using the OdRxObject::rxInit function.  It should also register
    custom commands defined in the module.
*/
void ExRasterModule::initApp()
{
  ODA_TRACE0("ExRasterModule::initApp() called\n");
}

/** Description:
    The user override of this function should unregister any custom objects defined in the
    custom application, using the OdRxObject::rxUninit function.  It should also
    remove any custom commands that were registered in the initApp function.
*/
void ExRasterModule::uninitApp()
{
}

ODRX_DEFINE_DYNAMIC_MODULE(ExRasterModule);
