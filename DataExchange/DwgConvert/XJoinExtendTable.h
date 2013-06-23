//////////////////////////////////////////////////////////////////////////

#include "afxcoll.h"

#ifndef AFX_XJOINEXTENDTABLE_H____INCLUDED_
#define AFX_XJOINEXTENDTABLE_H____INCLUDED_

class AFX_CLASS_EXPORT XJoinExtendTable  
{
public:
	XJoinExtendTable();
	virtual ~XJoinExtendTable();

public:
	void AddExtendFieldsValue(CString sLayerName);
	CMapStringToString* m_mapRegAppName;
	ITable* m_ipExtendTable;
	ITable* m_ipConfigTable;
	ITable* m_ipTargetTable;
	CStringList* m_pLogList;

	CTextProgressCtrl* m_pProgressCtrl;

	// CAD中的入库字段的所有注册应用名
	CStringList* m_lstRegApps;

private:
	void WriteLog(CString sLog);
	//新旧字段名称对照
	CMapStringToString m_sMapOldNameToNewName;
	//新增加字段名称对别名
	CMapStringToString m_sMapNameToAlias;

};

#endif // !defined(AFX_XJOINTEXTENDTABLE_H__08A51BCA_6699_46BE_86BF_8343E1CF191C__INCLUDED_)
