#ifndef __ODGIMODELTOVIEWPROC_H__
#define __ODGIMODELTOVIEWPROC_H__

#include "Gi/GiConveyorNode.h"
#include "Ge/GeDoubleArray.h"

class OdGeMatrix3d;
class OdGiDeviation;

/** Description:

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT ODRX_ABSTRACT OdGiModelToViewProc : public OdRxObject
{
protected:
  OdGiModelToViewProc();
public:
  ODRX_DECLARE_MEMBERS(OdGiModelToViewProc);

  virtual void setDrawContext( OdGiConveyorContext* pDrawCtx ) = 0;

  virtual OdGiConveyorInput& modelInput() = 0;
  virtual OdGiConveyorInput& eyeInput() = 0;

  virtual OdGiConveyorOutput& output() = 0;

  virtual void setEyeToOutputTransform(const OdGeMatrix3d& xMat) = 0;
  virtual const OdGeMatrix3d& eyeToOutputTransform() const = 0;

  virtual void setView(
    const OdGePoint3d& target,
    const OdGeVector3d& xVector,
    const OdGeVector3d& upVector,
    const OdGeVector3d& eyeVector) = 0;
  virtual void setWorldToEyeTransform(const OdGeMatrix3d& xMat) = 0;

  virtual const OdGeMatrix3d& worldToEyeTransform() const = 0;
  virtual const OdGeMatrix3d& eyeToWorldTransform() const = 0;

  virtual const OdGeMatrix3d& modelToEyeTransform() const = 0;
  virtual const OdGeMatrix3d& eyeToModelTransform() const = 0;
  
  virtual void pushModelTransform(const OdGeMatrix3d& xMat) = 0;
  virtual void popModelTransform() = 0;
  virtual const OdGeMatrix3d& modelToWorldTransform() const = 0;
  virtual const OdGeMatrix3d& worldToModelTransform() const = 0;

  virtual void pushClipBoundary(OdGiClipBoundary* pBoundary) = 0;
  virtual void popClipBoundary() = 0;

  virtual void setWorldDeviation(const OdGiDeviation& worldDev) = 0;
  virtual void setWorldDeviation(const OdGeDoubleArray& deviations) = 0;

  virtual const OdGiDeviation& worldDeviation() const = 0;
  virtual const OdGiDeviation& modelDeviation() const = 0;
  virtual const OdGiDeviation& eyeDeviation() const = 0;
};


typedef OdSmartPtr<OdGiModelToViewProc> OdGiModelToViewProcPtr;

#endif //#ifndef __ODGIMODELTOVIEWPROC_H__

