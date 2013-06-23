#ifndef _OD_MODELERGEOMETRY_INCLUDED_
#define _OD_MODELERGEOMETRY_INCLUDED_

#include "RxObject.h"
#include "ModelerDefs.h"
#include "DbEntity.h"

class OdStreamBuf;
class OdBrBrep;
class OdGiWorldDraw;
class OdGiViewportDraw;
class OdDbRegion;

#include "DD_PackPush.h"


/** Description:
    Provides conversion services for ACIS SAT and SAB data.

    Remarks:
    This class is used by DWGdirect to convert ACIS data to/from various
    versions of SAT/SAB.  For example, if a version 2004 DWG file is saved
    as R15 DXF, the SAB data in the 2004 file will need to be converted
    to version 700 SAT so that it will be valid within the R15 DXF file.

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdModelerGeometry : public OdRxObject
{
protected:
  OdModelerGeometry() {}
public:  
  ODRX_DECLARE_MEMBERS(OdModelerGeometry);

  // in/out functionality

  /** Description:
      Reads all ACIS data from the passed in stream, until the end of file
      condition is encountered.  

      Remarks:
      The implementation of this function is responsible for determining
      the type (SAT/SAB) and version of the passed in data.

      Arguments:
      pStreamIn Stream object from which to read the ACIS data.
      typeVer Set to version of sat, if not NULL. ( pStreamIn can be NULL )
      bStandardSaveFlag Variable which controls the saving and restoring 
                        of use count data in the save file (used by Autodesk).

      Return value : Normaly it return eOk. Return eInvalidInput If pStreamIn containes multibody sat.
  */
  virtual OdResult in(OdStreamBuf* pStreamIn, AfTypeVer *typeVer = NULL, bool bStandardSaveFlag = true) = 0;
  

  /** Description:
      Writes the ACIS data contained in this object to the passed in 
      data stream, in the specified format.

      Remarks:
      The implementation of this function is responsible for converting
      the contained ACIS data to the specified type and format.  If 
      kAfTypeVerAny is specified, then any type/version of data may be 
      written.

      Arguments:
      pStreamOut Stream object to which the ACIS data contained in this object
                 is written.
      typeVer Type and version of ACIS data to write.
      bStandardSaveFlag Variable which controls the saving and restoring 
                        of use count data in the save file (used by Autodesk).
  */
  virtual OdResult out(OdStreamBuf* pStreamOut, AfTypeVer typeVer, bool bStandardSaveFlag = true) const = 0;

  /** Description:
      Populates the passed in OdBrBrep object with the ACIS B-Rep data contained
      in this object.
  */

  virtual bool brep(OdBrBrep& brep) const = 0;

  // Draw functionality
  // kOrderedEdges - Geometry must be returned as closed loops. 
  // Viewport clipping use it.
  enum
  {
    kNothing      = 0,
    kIsolines     = 1,
    kEdges        = 2,
    kShells       = 4,
    kOrderedEdges = 8
  };

  virtual bool setFACETRES(double) = 0;

  virtual bool worldDraw(OdGiWorldDraw* pWd, OdUInt32 geomType) = 0;
  virtual bool drawSilhouettes(OdGiViewportDraw* pVd) = 0;
  virtual bool explode(OdDbEntityPtrArray& /*entitySet*/) const = 0;
  
  virtual bool getTransformation(OdGeMatrix3d& m) = 0;
  virtual void transformBy( const OdGeMatrix3d& xform ) = 0;
  
  virtual void createBox( double xLen, double yLen, double zLen ) = 0;
  virtual void createFrustum( double height, double xRadius, double yRadius, double topXRadius ) = 0;
  virtual void createSphere( double radius ) = 0;
  virtual void createTorus( double majorRadius, double minorRadius ) = 0;
  virtual void createWedge( double xLen, double yLen, double zLen ) = 0;

  virtual bool extrude(const OdDbRegion* region, double height, double taper) = 0;
  virtual bool revolve(const OdDbRegion* region, const OdGePoint3d& axisPoint, const OdGeVector3d& axisDir, double angleOfRevolution) = 0;

  // Remove color attributes
  virtual void ClearColorAttributes() = 0;
  // Return true if face\edge has truecolor-adesk-attrib.
  virtual bool hasTrueColorAttributes() const = 0;

  virtual OdResult getPlane(OdGePlane& regionPlane) const = 0;
};

typedef OdSmartPtr<OdModelerGeometry> OdModelerGeometryPtr;

#include "DD_PackPop.h"

#endif // _OD_MODELERGEOMETRY_INCLUDED_
