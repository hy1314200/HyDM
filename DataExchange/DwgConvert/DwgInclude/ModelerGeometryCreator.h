#ifndef _OD_MODELERGEOMETRYCREATOR_INCLUDED_
#define _OD_MODELERGEOMETRYCREATOR_INCLUDED_

#include "RxObject.h"
#include "ModelerGeometry.h"
#include "OdArray.h"

class OdStreamBuf;

#include "DD_PackPush.h"

/** Description:
    Utility class used to load and save SAT/SAB files, and to perform miscellaneous
    other ACIS-related operations.
    
    Remarks:
    The functions in this class can do the following: 
    
    o Create OdModelerGeometry instances from a specified input stream.
    o Save out SAT/SAB data from an OdModelerGeometry instance.
    o Create a region entity from a set of curves.

    {group:Other_Classes}
*/
class TOOLKIT_EXPORT OdModelerGeometryCreator : public OdRxObject
{
protected:
  OdModelerGeometryCreator() {}
public:  
  ODRX_DECLARE_MEMBERS(OdModelerGeometryCreator);

  /** Description: 
      Creates an instance (or instances) of OdModelerGeometry, from the passed in 
      data source (which should be an SAT or SAB file).
      
      Arguments:
        out (O) Array of OdModelerGeometryPtr, created from the contents of the specified input file.
        pStreamIn (I) Stream object from which to read the ACIS data, in SAT/SAB format.
        bStandardSaveFlag (I) Variable which controls the saving and restoring 
          of use count data in the save file (used by Autodesk).

      Remarks:
      Since OdModelerGeometry does not support multi-body SAT files, it breaks such SAT 
      files into a set of single body OdModelerGeometry objects (hence the use of an array
      of OdModelerGeometryPtr as an output argument).

      Return Value:
      eOk if the operation was successful, otherwise and appropriate error value.
  */
  virtual OdResult createModeler(OdArray<OdModelerGeometryPtr> &out, 
    OdStreamBuf* pStreamIn, 
    bool bStandardSaveFlag = true) = 0;

  /** Description: 
      Creates a single SAT/SAB file from the set of specified input objects.
      
      Arguments:
        mg_in (I) Array of OdModelerGeometryPtr, representing a set of ACIS solids.
        pStreamOut (O) Stream object to which the ACIS data is written.
        typeVer(I) Type and version of ACIS data to write.
        bStandardSaveFlag (I) Variable which controls the saving and restoring of use count data in the save file (used by Autodesk).
        
      Return Value:
      eOk if the operation was successful, otherwise and appropriate error value.
  */
  virtual OdResult createSat(const OdArray<OdModelerGeometryPtr> &mg_in, 
    OdStreamBuf* pStreamOut, 
    AfTypeVer typeVer, 
    bool bStandardSaveFlag = true) = 0;

  /** Arguments:
        ent_in (I) Array of OdDbEntityPtr (must be OdDb3dSolid, OdDbBody, or OdDbRegion).
      
      Remarks:
      Color attributes are also added to the SAT/SAB data.
  */
  virtual OdResult createSat(const OdDbEntityPtrArray &ent_in, 
    OdStreamBuf* pStreamOut, 
    AfTypeVer typeVer, 
    bool bStandardSaveFlag = true) = 0;

  /** Description: 
      Creates an ACIS region (represented by an OdModelerGeometry instance), from 
      a passed in set of curves.
      
      Arguments:
      curves (I) An array of curves, containing pointers to 
                 OdGeLineSeg3d, OdGeCircArc3d, OdGeEllipArc3d, or OdGeNurbCurve3d. 
                 The curves must be ordered by start/end points (the end point of 
                 each curve must be equal to start point of the next curve, and 
                 the last end point should be equal to the first start point). 
                 All curves must be in the same plane.
      region (O) Newly created ModelerGeometry instance, that contains a region corresponding
                 to the passed in curves.

      Remarks:
      Appropriate color attributes are added to the region.
  */
  virtual OdResult createRegionFromCurves(const OdArray<OdGeCurve3d*,OdMemoryAllocator<OdGeCurve3d*> > &curves, 
    OdModelerGeometryPtr &region) = 0;
};

typedef OdSmartPtr<OdModelerGeometryCreator> OdModelerGeometryCreatorPtr;

#include "DD_PackPop.h"

#endif // _OD_MODELERGEOMETRYCREATOR_INCLUDED_
