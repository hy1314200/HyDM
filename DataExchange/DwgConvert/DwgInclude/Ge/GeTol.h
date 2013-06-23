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



#ifndef OD_GETOL_H
#define OD_GETOL_H /* {Secret} */


#include "GeExport.h"

#include "DD_PackPush.h"

/**
    Description:
    
    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeTol
{
public:
  OdGeTol(double t = 1.e-10) : m_pointTol(t), m_vectorTol(t) {}
  OdGeTol(double t1, double t2) : m_pointTol(t1) , m_vectorTol(t2){}

  double equalPoint() const { return m_pointTol; }
  double equalVector() const { return m_vectorTol;}

  void setEqualPoint(double val) { m_pointTol = val; }
  void setEqualVector(double val) { m_vectorTol = val; }

private:
  double m_vectorTol;
  double m_pointTol;
};

#include "DD_PackPop.h"

#endif


