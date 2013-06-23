#include "StdAfx.h"
#include "LogRecorder.h"
#include <stdio.h>
#include <io.h> 

/********************************************************************
简要描述 : 生成日志文件
输入参数 :
返 回 值 :
//
//
修改日志 :
*********************************************************************/
CLogRecorder::CLogRecorder(CString sLogFileName)
{
	if(_access(sLogFileName, 0) == -1)
	{
		m_bOpen = m_file.Open(sLogFileName, CFile::modeCreate | CFile::modeWrite | CFile::typeText);
	}
	else
	{
		m_bOpen = m_file.Open(sLogFileName, CFile::modeWrite | CFile::typeText);
		m_file.SeekToEnd();
	}
}
/********************************************************************
简要描述 : 写日志
输入参数 :
返 回 值 :
//
//
修改日志 :
*********************************************************************/
void CLogRecorder::WriteLog(CString sLog)
{
	if (m_bOpen && !sLog.IsEmpty())
	{
		CTime dtCur = CTime::GetCurrentTime();
		CString sLogTime = dtCur.Format("%Y/%m/%d %H:%M:%S");
		sLog = sLogTime + "-" + sLog+"\r\n";
		m_file.WriteString(sLog);
	}
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 关闭日志文件
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
void CLogRecorder::CloseFile()
{
	if (m_bOpen)
	{
		m_bOpen = FALSE;
		m_file.Close();
	}
	
}

//////////////////////////////////////////////////////////////////////////
//简要描述 : 关闭日志文件
//输入参数 :
//返 回 值 :
//修改日志 :
//////////////////////////////////////////////////////////////////////////
CLogRecorder::~CLogRecorder(void)
{
	if (m_bOpen)
	{
		m_file.Close();
	}
}
