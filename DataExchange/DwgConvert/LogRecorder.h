#pragma once


class CLogRecorder
{
private:
	CStdioFile m_file;
	BOOL m_bOpen;
public:

	void WriteLog(CString sLog);

	void CloseFile();

	CLogRecorder(CString sLogFileName);
	~CLogRecorder(void);
};
