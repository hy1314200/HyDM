// dllmain.h : 模块类的声明。

class CHyDwgConvertModule : public ATL::CAtlDllModuleT< CHyDwgConvertModule >
{
public :
	DECLARE_LIBID(LIBID_HyDwgConvert)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_HYDWGCONVERT, "{F0AF6BB6-674B-45C9-B291-B281B09DAC5F}")
};

extern class CHyDwgConvertModule _AtlModule;
