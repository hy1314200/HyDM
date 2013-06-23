///////////////////////////////////////////////////////////////////////////////
// Copyright © 2002, Open Design Alliance Inc. ("Open Design") 
// 
// This software is owned by Open Design, and may only be incorporated into 
// application programs owned by members of Open Design subject to a signed 
// Membership Agreement and Supplemental Software License Agreement with 
// Open Design. The structure and organization of this Software are the valuable 
// trade secrets of Open Design and its suppliers. The Software is also protected 
// by copyright law and international treaty provisions. You agree not to 
// modify, adapt, translate, reverse engineer, decompile, disassemble or 
// otherwise attempt to discover the source code of the Software. Application 
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



//:> OdPolyPolygon2d.h: interface for the OdPolyPolygon2d class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_OdPolyPolygon2d_H__0D83A6B9_F93A_4801_AB9B_47B6648AE23B__INCLUDED_)
#define AFX_OdPolyPolygon2d_H__0D83A6B9_F93A_4801_AB9B_47B6648AE23B__INCLUDED_

#include "DD_PackPush.h"

#if defined(_MSC_VER)
#pragma warning (push)
#pragma warning ( disable : 4512 ) // assignment operator could not be generated
#endif

#include "RxObject.h"
#include "OdArray.h"
#include "Ge/GePoint2d.h"
#include "Int32Array.h"
#include "Ge/GePoint2dArray.h"

/** Description:

    {group:Other_Classes}
*/
class OdPolyPolygon2d : public OdRxObject
{
	OdGePoint2dArray  m_Points;
	OdInt32Array      m_Counts;
public:
	//ODRX_DECLARE_MEMBERS(OdPolyPolygon2d);

	OdPolyPolygon2d() {}
	virtual ~OdPolyPolygon2d() {}

	void newContour() { m_Counts.append(0); }
	void addToCurrentContour(OdGePoint2d& p) { m_Points.append(p); m_Counts[m_Counts.size() - 1]++; }

	OdGePoint2dArray& points() { return m_Points; }
	const OdGePoint2dArray& points() const { return m_Points; }
	OdInt32Array& counts() { return m_Counts; }
	const OdInt32Array& counts() const { return m_Counts; }


};

#if defined(_MSC_VER)
#pragma warning (pop)
#endif

#include "DD_PackPop.h"

#endif // !defined(AFX_OdPolyPolygon2d_H__0D83A6B9_F93A_4801_AB9B_47B6648AE23B__INCLUDED_)


