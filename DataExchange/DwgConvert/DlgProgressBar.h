#pragma once

#include "resource.h"
#include "TextProgressCtrl.h"
// CDlgProgressBar 对话框

class CDlgProgressBar : public CDialog
{
    DECLARE_DYNAMIC(CDlgProgressBar)

public:
    CDlgProgressBar(CWnd* pParent = NULL);   // 标准构造函数
    virtual ~CDlgProgressBar();

// 对话框数据
    enum { IDD = IDD_DLGPROGRESSBAR };

protected:
    virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

    DECLARE_MESSAGE_MAP()

public:

    BOOL CreateDlg(CWnd *pParentWnd = NULL);
    BOOL Show();
    BOOL Hide();
    BOOL Close();

public:
    // 进度条
    //CProgressCtrl m_progressBar;
    CTextProgressCtrl m_progressBar;
};
