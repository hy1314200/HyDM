#pragma once



#include "DlgProgressBar.h"
#include "LogRecorder.h"


//////////////////////////////////////////////////////////////////////////

#define  PI  3.14159265358979323846
const double g_dAngleParam = 180 / PI;



class SharedUsed
{
public:
    SharedUsed(void);
    ~SharedUsed(void);
};

//从IFeatureClass中取得空间参考
IGeometryDef* API_GetGeometryDef(IFeatureClass* pFeatureClass);

//根据查询条件把源要素类转化到目标工作空间(MDB)
void API_ConvertFeatureClass(IFeatureClass* pSrcFtCls,
                             IQueryFilterPtr pQueryFilter,
                             IWorkspace* pTargetWorkspace,
                             LPSTR sTargetDatasetName,
                             LPSTR sTargetFeatureClassName,
                             IFieldsPtr pTargetFields = NULL);

ISpatialReference* API_GetSpatialReference(IFeatureClass* pFeatureClass);

CString GetLogPath();