#ifndef AFX_DWGENTITYREADER_H____INCLUDED_
#define AFX_DWGENTITYREADER_H____INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#define STL_USING_IOSTREAM
#include "OdaSTL.h"
#define  STD(a)  std:: a

class OdRxObject;
class OdDbEntity;
class Dumpers;


struct DwgPoint
{
	double x;
	double y;
	double z;
};

class OdDbEntity_Dumper : public OdRxObject
{
public:
    ODRX_DECLARE_MEMBERS(OdDbEntity_Dumper);

    virtual IGeometry* dump(OdDbEntity* pEnt);
    //读dwg实体通用信息
    void dumpCommonData(OdDbEntity* pEnt);

protected:
	void Bspline(int n, int t, DwgPoint* control, DwgPoint* output, int num_output);
	void ComputeIntervals(int* u, int n, int t);
	void ComputePoint(int* u, int n, int t, double v, DwgPoint* control, DwgPoint* output);
	double Blend(int k, int t, int* u, double v);

};
class ExProtocolExtension
{
    Dumpers* m_pDumpers;
public:
    ExProtocolExtension();
    virtual ~ExProtocolExtension();
    void initialize();
    void uninitialize();
};
#endif
