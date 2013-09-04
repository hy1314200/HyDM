// dllmain.h : 模块类的声明。

class CTestModule : public ATL::CAtlDllModuleT< CTestModule >
{
public :
	DECLARE_LIBID(LIBID_TestLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_TEST, "{1133FD08-F412-452D-93E9-D12D633C47AB}")
};

extern class CTestModule _AtlModule;
