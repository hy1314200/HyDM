/*/////////////////////   Esri_AoInterface.h   /////////////////////////////////////////////
//
//	简    介：	该文件包含了所有的ArcGis Engine 提供的接口文件	 
//	应用环境:	该文件只为各种接口类提供声明作用。
//  历    史：	修改时间1，修改人，修改原因
//
/*/////////////////////////////////////////////////////////////////////////////////////
#ifndef _ESRI_AOINTERFACE_H__
#define _ESRI_AOINTERFACE_H__

#pragma   warning(disable:4099)
#pragma   warning(disable:4146)
#pragma   warning(disable:4786)

/*
#include"Ao_Importtlh\esriSystem.tlh"
#include"Ao_Importtlh\esriSystemUI.tlh"
#include"Ao_Importtlh\esriGeometry.tlh"
#include"Ao_Importtlh\esriDisplay.tlh"
#include"Ao_Importtlh\esriServer.tlh"
#include"Ao_Importtlh\esriOutput.tlh"
#include"Ao_Importtlh\esriGeoDatabase.tlh"
#include"Ao_Importtlh\esriDataSourcesFile.tlh"
#include"Ao_Importtlh\esriDataSourcesGDB.tlh"
#include"Ao_Importtlh\esriDataSourcesOleDB.tlh"
#include"Ao_Importtlh\esriDataSourcesRaster.tlh"
#include"Ao_Importtlh\esriGeoAnalyst.tlh"
#include"Ao_Importtlh\esriGeoDatabaseDistributed.tlh"
#include"Ao_Importtlh\esriGeoStatisticalAnalyst.tlh"
#include"Ao_Importtlh\esriGISClient.tlh"
#include"Ao_Importtlh\esri3DAnalyst.tlh"
#include"Ao_Importtlh\esriGlobeCore.tlh"
#include"Ao_Importtlh\esriLocation.tlh"
#include"Ao_Importtlh\esriNetworkAnalysis.tlh"
#include"Ao_Importtlh\GlobeControl.tlh"
#include"Ao_Importtlh\esriSpatialAnalyst.tlh"
#include"Ao_Importtlh\esriSystemUtility.tlh"
#include"Ao_Importtlh\esriFramework.tlh"
//#include"Ao_Importtlh\esriTrackingAnalyst.tlh"
#include"Ao_Importtlh\esriControlCommands.tlh"
#include"Ao_Importtlh\esriCarto.tlh"
#include"Ao_Importtlh\MapControl.tlh"
#include"Ao_Importtlh\PageLayoutControl.tlh"
#include"Ao_Importtlh\ReaderControl.tlh"
#include"Ao_Importtlh\SceneControl.tlh"
#include"Ao_Importtlh\TocControl.tlh"
#include"Ao_Importtlh\ToolbarControl.tlh"
*/

//#include"Ao_Importtlh\esriSystem.tlh"
//#include"Ao_Importtlh\esriGeometry.tlh"

#import "C:\Program Files (x86)\ArcGIS\Desktop10.0\com\esriSystem.olb" raw_interfaces_only raw_native_types no_namespace named_guids, rename("IObjectConstruct", "IEsriObjectConstruct")rename("OLE_COLOR","Esri_Color") rename("OLE_HANDLE","Esri_Handle")
#import "C:\Program Files (x86)\ArcGIS\Desktop10.0\com\esriGeometry.olb" raw_interfaces_only raw_native_types no_namespace named_guids, rename("ISegment", "IEsriSegment") 

#include"Ao_Importtlh\esriGeodatabase.tlh"
#include"Ao_Importtlh\esriSystemUI.tlh"
#include"Ao_Importtlh\esriDisplay.tlh"
//#include"Ao_Importtlh\esriServer.tlh"
//#include"Ao_Importtlh\esriOutput.tlh"
#include"Ao_Importtlh\esriCarto.tlh"
//#include"Ao_Importtlh\esriGeoDatabase.tlh"
//#include"Ao_Importtlh\esriDataSourcesFile.tlh"
//#include"Ao_Importtlh\esriDataSourcesGDB.tlh"
//#include"Ao_Importtlh\esriDataSourcesOleDB.tlh"
//#include"Ao_Importtlh\esriDataSourcesRaster.tlh"
//#include"Ao_Importtlh\esriGeoAnalyst.tlh"
//#include"Ao_Importtlh\esriGeoDatabaseDistributed.tlh"
//#include"Ao_Importtlh\esriGeoStatisticalAnalyst.tlh"
//#include"Ao_Importtlh\esriGISClient.tlh"
//#include"Ao_Importtlh\esri3DAnalyst.tlh"
//#include"Ao_Importtlh\esriGlobeCore.tlh"
#include"Ao_Importtlh\esriLocation.tlh"
//#include"Ao_Importtlh\esriNetworkAnalysis.tlh"
//#include"Ao_Importtlh\esriGlobeCore.tlh"
#include"Ao_Importtlh\esriSpatialAnalyst.tlh"
#include"Ao_Importtlh\esriSystemUtility.tlh"
#include"Ao_Importtlh\esriControls.tlh"
#include"Ao_Importtlh\esriFramework.tlh"


#endif //_ESRI_AOINTERFACE_H_