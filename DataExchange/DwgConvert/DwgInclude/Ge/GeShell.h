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



#ifndef _ODGESHELL_INCLUDED_
#define _ODGESHELL_INCLUDED_ /* {Secret} */

#include "Int32Array.h"
#include "CmEntityColorArray.h"

/**
     
    Library: Ge

    {group:OdGe_Classes} 
*/
class OdGeShell
{
  int                   m_nFaceStartIndex;
public:
  OdGeShell() : m_nFaceStartIndex(-1) {}

  OdGePoint3dArray      vertices;             
  OdInt32Array          faces;                

  OdArray<OdUInt16>     edgeColors;           
  OdCmEntityColorArray  edgeTrueColors;       
  OdArray<OdDbObjectId> edgeLayers;             
  OdArray<OdDbObjectId> edgeLinetypes;        
  OdInt32Array          edgeSelectionMarkers; 
  OdArray<OdUInt8>      edgeVisibilities;       

  OdArray<OdUInt16>     faceColors;           
  OdCmEntityColorArray  faceTrueColors;       
  OdArray<OdDbObjectId> faceLayers;           
  OdGeVector3dArray     faceNormals;            
  OdInt32Array          faceSelectionMarkers; 
  OdArray<OdUInt8>      faceVisibilities;     

  /**
    To be determined.
  */
  void addVertex(const OdGePoint3d& vertex)
  {
    vertices.push_back(vertex);
  }

  /**
    To be determined.
  */
  void endFace()
  {
    if(m_nFaceStartIndex>=0)
    {
      faces[m_nFaceStartIndex] *= (faces.size()-m_nFaceStartIndex-1);
      m_nFaceStartIndex = -1;
    }
  }

  /**
    To be determined.
  */
  void startFace(bool bHole = false)
  {
    ODA_ASSERT(m_nFaceStartIndex==-1); // endFace() wasn't called.
    m_nFaceStartIndex = faces.size();
    faces.push_back(bHole ? -1 : 1);
  }

  /**
    To be determined.
  */
  void addFaceVertex(int nVertexIndex, int visible = 1)
  {
    visible = nVertexIndex < 0 ? 0 : visible;
    nVertexIndex = visible ? nVertexIndex : -nVertexIndex;
    faces.push_back(--nVertexIndex);
    edgeVisibilities.push_back(OdUInt8(visible));
  }
  
  /**
    To be determined.
  */
  void draw(OdGiGeometry& geometry)
  {
    ODA_ASSERT(m_nFaceStartIndex<0 || faces[m_nFaceStartIndex]>2); // endFace() wasn't called.

    OdGiEdgeData edgeData;
    if (!edgeColors.isEmpty()) edgeData.setColors(edgeColors.getPtr());
    if (!edgeTrueColors.isEmpty()) edgeData.setTrueColors(edgeTrueColors.getPtr());
    if (!edgeLayers.isEmpty()) edgeData.setLayers((OdDbStub**)edgeLayers.getPtr());
    if (!edgeLinetypes.isEmpty()) edgeData.setLinetypes((OdDbStub**)edgeLinetypes.getPtr());
    if (!edgeSelectionMarkers.isEmpty()) edgeData.setSelectionMarkers(edgeSelectionMarkers.getPtr());
    if (!edgeVisibilities.isEmpty()) edgeData.setVisibility(edgeVisibilities.getPtr());

    OdGiFaceData faceData;
    if (!faceColors.isEmpty()) faceData.setColors(faceColors.getPtr());
    if (!faceTrueColors.isEmpty()) faceData.setTrueColors(faceTrueColors.getPtr());
    if (!faceLayers.isEmpty()) faceData.setLayers((OdDbStub**)faceLayers.getPtr());
    if (!faceNormals.isEmpty()) faceData.setNormals(faceNormals.getPtr());
    if (!faceSelectionMarkers.isEmpty()) faceData.setSelectionMarkers(faceSelectionMarkers.getPtr());
    if (!faceVisibilities.isEmpty()) faceData.setVisibility(faceVisibilities.getPtr());

    geometry.shell(vertices.size(), vertices.getPtr(), faces.size(), faces.getPtr(), &edgeData, &faceData);
  }
};

#endif // _ODGESHELL_INCLUDED_


