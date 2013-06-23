#ifndef __ODGIGEOMETRYSIMPLIFIER_H__
#define __ODGIGEOMETRYSIMPLIFIER_H__


#include "Gi/GiExport.h"
#include "Gi/GiConveyorGeometry.h"
#include "Ge/GeDoubleArray.h"
#include "Ge/GePoint2dArray.h"
#include "Ge/GePoint3dArray.h"
#include "Gs/Gs.h"
class OdGiDeviation;


#include "DD_PackPush.h"

/** Description:
    Class that provides tessellation functionality, or the breaking up of complex 
    entities into sets of simpler entities.  An OdGiGeometrySimplifier instance
    is used to simplify geometry produced by the DWGdirect vectorization
    framework.

    {group:OdGi_Classes} 
*/
class ODGI_EXPORT OdGiGeometrySimplifier : public OdGiConveyorGeometry
{
  // Old version do not override !!!
  // Overide shellFaceOut() or triangleOut() instead
  virtual int polygonWithHolesOut(OdUInt32, const OdGePoint3d*, OdUInt32 , OdUInt32*);
protected:
  void drawTypedArc(OdGiArcType arcType, const OdGePoint3d& center,
    OdGePoint3dArray& points, const OdGeVector3d* pNormal, const OdGeVector3d* pExtrusion);
private:
  bool setFaceTraits(const OdGiFaceData* pFaceData, int faceIndex);
  bool setEdgeTraits(const OdGiEdgeData* pEdgeData, int edgeIndex);

  OdGiConveyorContext*  m_pDrawCtx;
  OdGiSubEntityTraits*  m_pTraits;
  bool                  m_bProcessingText;

  OdInt32               m_nVertexCount;
  const OdGePoint3d*    m_pVertexList;
  const OdGiVertexData* m_pVertexData;
  OdGePoint2dArray      m_loopsPnts;
  OdGePoint3dArray      m_points3d;
protected:
  const OdGiDeviation*  m_pDeviation;
  OdGeDoubleArray       m_deviations;
  OdGsView::RenderMode  m_renderMode;

  double deviation(const OdGiDeviationType deviationType, const OdGePoint3d& point) const;

  bool fillMode();
  bool fillMode(bool& bDrawEdges);
  bool processingText() const { return m_bProcessingText; }
  OdGiContext& giCtx() const { return const_cast<OdGiContext&>(m_pDrawCtx->giContext()); }


public:
  OdGiGeometrySimplifier();

  /** Description:
      Specifies the maximum deviation allowed during the 
      tessellation process.  This value sets the limit for the maximum difference 
      between the actual curve or surface, and the tessellated curve or surface.

      Arguments:
        deviations (I) Array of deviation values (in isotropic world space) 
          for the deviation types defined by the OdGiDeviationType enum.  
          Each OdGiDeviationType value is used as an index into this array.
  */
  void setDeviation(const OdGeDoubleArray& deviations);

  /** Arguments:
        pDeviation (I) Deviation to be used for anisotropic space (perspective view).
  */
  void setDeviation(const OdGiDeviation* pDeviation);

  /** Description
      Sets the render mode for this object.
       
      See Also:
        OdGsView::RenderMode
  */
  OdGsView::RenderMode renderMode() { return m_renderMode; }

  /* Description:
     Sets the OdGiConveyorContext for this object.  This must be set before 
     calling any of the tessellation functions.
  */
  void setDrawContext(OdGiConveyorContext* pDrawCtx);

  /* Description:
     Returns the OdGiConveyorContext for this object.  
  */
  OdGiConveyorContext* drawContext( ) { return m_pDrawCtx; }

  /* Description:
     Returns the OdGiConveyorContext for this object.  
  */
  const OdGiConveyorContext* drawContext( ) const { return m_pDrawCtx; }

  /** Description:
      This function is called by the OdGiGeometrySimplifier class to 
      pass polyline data generated during tessellation, to a client application.
      Clients should override this function.

      Arguments:
        nbPoints (I) Number of points in the polyline.
        pVertexList (I) Pointer to an array of vertices that make up the polyline.
  */
  virtual void polylineOut(OdInt32 nbPoints, const OdGePoint3d* pVertexList);

  /** Description:
      This function is called by the OdGiGeometrySimplifier class to 
      pass polygon data generated during tessellation, to a client application.
      Clients should override this function.

      Arguments:
        nbPoints (I) Number of points in the polygon.
        pVertexList (I) Pointer to an array of vertices that make up the polygon.
        pNormal (I) Normal vector for the polygon.
  */
  virtual void polygonOut(OdInt32 nbPoints, 
    const OdGePoint3d* pVertexList, 
    const OdGeVector3d* pNormal = 0);

  /** Description:
      Called by the default implementations of meshProc and shellProc, to 
      set vertex data that will be used in the tessellation of these objects.

      Arguments:
        nVertexCount (I) Number of vertices in the mesh or shell.
        pVertexList (I) Pointer to an array of vertices that make up the mesh or shell.
        pVertexData (I) Additional vertex attributes (normals, colors, etc.). 
           
      See Also:
        OdGiVertexData
  */
  void setVertexData(OdInt32 nVertexCount,
    const OdGePoint3d* pVertexList,
    const OdGiVertexData* pVertexData = 0);

  /** Description:
      Returns the vertex count set in the most recent call to setVertexData.
  */
  OdInt32 vertexDataCount() const;

  /** Description:
      Returns the vertex list set in the most recent call to setVertexData.
  */
  const OdGePoint3d* vertexDataList() const;

  /** Description:
      Returns the vertex attribute data set in the most recent call to setVertexData.
  */
  const OdGiVertexData* vertexData() const;

  /** Remarks:
      Client applications have the option of overriding this function to process the polyline
      data themselves.  If the function is not overridden, the default implementation of 
      this function processes the polyline, taking into account the following:

        o The pXform transformation (if any) that must be applied to the polyline.
        o Extrusion of the polyline (if it has a non-zero thickness).
        o Creating the closing segment if the polyline is closed.
        o Start and end widths for each segment, or constant width, if applicable.
        o Arc segments (if bulge values are present).

      In the default implementation of this function, line segments with a non-zero width 
      will be converted into calls to shellProc, and segments with no width will generate
      calls to polylineProc.  Arc segments with non-zero width will be converted into 
      calls to polygonProc, and arc segments with no width will generate calls to 
      polylineProc.      

      OdGiGeometrySimplifier::plineProc is only called for polylines with a continuous
      linetype. 
  */
  virtual void plineProc(const OdGiPolyline& lwBuf,
    const OdGeMatrix3d* pXform = 0,
    OdUInt32 fromIndex = 0,
    OdUInt32 numSegs = 0);
  
  /** Remarks:
      Client applications have the option of overriding this function to process the polyline
      data themselves.  If the function is not overridden, the default implementation
      of this function processes the polyline, calling polylineOut if the data is not 
      extruded, or meshProc if an extrusion value is present.

      OdGiGeometrySimplifier::polylineProc is only called for polylines with a continuous
      linetype. 
  */
  virtual void polylineProc(OdInt32 nbPoints, 
    const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = 0, 
    const OdGeVector3d* pExtrusion = 0,
    OdInt32 lBaseSubEntMarker = -1);

  /** Remarks:
      Client applications have the option of overriding this function to process the polygon
      data themselves.  If the function is not overridden, the default implementation
      of this function processes the polygon, calling polygonOut if the data is not 
      extruded, or shellProc if an extrusion value is present.
  */
  virtual void polygonProc(OdInt32 nbPoints, 
    const OdGePoint3d* pVertexList,
    const OdGeVector3d* pNormal = 0, 
    const OdGeVector3d* pExtrusion = 0);

  /**  Remarks:
       This function should not be overridden by clients. 
  */
  virtual void xlineProc(const OdGePoint3d&, const OdGePoint3d&);

  /**  Remarks:
       This function should not be overridden by clients. 
  */
  virtual void rayProc(const OdGePoint3d&, const OdGePoint3d&);

  /** Remarks:
      Client applications have the option of overriding this function to process the mesh
      data themselves.  If the function is not overridden, the default implementation
      of this function processes the mesh into a set of facets.  More specifically,
      the default meshProc function calls setVertexData, and the calls either
      generateMeshFaces for filled facets, or generateMeshWires for a wireframe.
  */
  virtual void meshProc(OdInt32 rows,
    OdInt32 columns,
    const OdGePoint3d* pVertexList,
    const OdGiEdgeData* pEdgeData = 0,
    const OdGiFaceData* pFaceData = 0,
    const OdGiVertexData* pVertexData = 0);

      /** Description:
          This function is called from the default implementation of meshProc
          to tessellate a wireframe mesh. Vertex data needed for this function
          can be obtained by calling vertexDataList and vertexData.
          Client applications have the option of overriding this function to process the mesh
          data themselves.  If the function is not overridden, the default implementation
          of this function processes the mesh into a set of facets.  More specifically,
          the default generateMeshWires function processes mesh attributes, and calls
          polylineOut with the tessellated mesh data.

          Arguments:
            rows (I) Number of rows in the mesh.
            columns (I) Number of columns in the mesh.
            pEdgeData (I) Pointer to additional edge data (color, layer, etc.).
            pFaceData (I) Pointer to additional face data (color, layer, etc.).
          
          See Also:
            OdGiEdgeData
            OdGiFaceData
      */
      virtual void generateMeshWires(OdInt32 rows, 
        OdInt32 columns,
        const OdGiEdgeData* pEdgeData,
        const OdGiFaceData* pFaceData);

      /** Description:
          This function is called from the default implementation of meshProc
          to tessellate a filled mesh. Vertex data needed for this function
          can be obtained by calling vertexDataList and vertexData.
          Client applications have the option of overriding this function to process the mesh
          data themselves.  If the function is not overridden, the default implementation
          of this function processes the mesh into a set of facets.  More specifically,
          the default generateMeshFaces function processes mesh attributes, and calls
          triangleOut function with the tessellated mesh data.

          Arguments:
            rows (I) Number of rows in the mesh.
            columns (I) Number of columns in the mesh.
            pEdgeData (I) Pointer to additional edge data (color, layer, etc.).
            pFaceData (I) Pointer to additional face data (color, layer, etc.).
          
          See Also:
            OdGiEdgeData
            OdGiFaceData
      */
      virtual void generateMeshFaces(OdInt32 rows, OdInt32 columns,
                                     const OdGiFaceData* pFaceData);

  /** Remarks:
      Client applications have the option of overriding this function to process the shell
      data themselves.  If the function is not overridden, the default implementation
      of this function processes the shell into a set of facets.  More specifically,
      the default shellProc function calls setVertexData, and the calls either
      generateShellFaces for filled facets, or generateShellWires for a wireframe.
  */
  virtual void shellProc(OdInt32 nbVertex,
                 const OdGePoint3d* pVertexList,
                 OdInt32 faceListSize,
                 const OdInt32* pFaceList,
                 const OdGiEdgeData* pEdgeData = 0,
                 const OdGiFaceData* pFaceData = 0,
                 const OdGiVertexData* pVertexData = 0);

      /** Description:
          This function is called from the default implementation of shellProc
          to tessellate a wireframe shell. Vertex data needed for this function
          can be obtained by calling vertexDataList and vertexData.
          Client applications have the option of overriding this function to process the shell
          data themselves.  If the function is not overridden, the default implementation
          of this function processes the shell into a set of facets.  More specifically,
          the default generateShellWires function processes shell attributes, and calls
          polylineOut with the tessellated shell data.

          Arguments:
            faceListSize (I) Number of entries in pFaceList.
            pFaceList (I) List of numbers that define the faces in the shell.  See
              Vectorization Primitives for more details.
            pEdgeData (I) Pointer to additional edge data (color, layer, etc.).
            pFaceData (I) Pointer to additional face data (color, layer, etc.).
          
          See Also:
            OdGiEdgeData
            OdGiFaceData
      */
      virtual void generateShellWires(OdInt32 faceListSize,
                                      const OdInt32* pFaceList,
                                      const OdGiEdgeData* pEdgeData = 0,
                                      const OdGiFaceData* pFaceData = 0);
      
      /** Description:
          This function is called from the default implementation of shellProc
          to tessellate a filled wireframe shell. Vertex data needed for this function
          can be obtained by calling vertexDataList and vertexData.
          Client applications have the option of overriding this function to process the shell
          data themselves.  If the function is not overridden, the default implementation
          of this function processes the shell into a set of facets.  More specifically,
          the default generateShellFaces function processes shell attributes, and calls
          shellFaceOut with the tessellated shell data.

          Arguments:
            faceListSize (I) Number of entries in pFaceList.
            pFaceList (I) List of numbers that define the faces in the shell.  See
              Vectorization Primitives for more details.
            pEdgeData (I) Pointer to additional edge data (color, layer, etc.).
            pFaceData (I) Pointer to additional face data (color, layer, etc.).
          
          See Also:
            OdGiEdgeData
            OdGiFaceData
      */
      virtual void generateShellFaces(OdInt32 faceListSize,
                                      const OdInt32* pFaceList,
                                      const OdGiEdgeData* pEdgeData = 0,
                                      const OdGiFaceData* pFaceData = 0);

          /** Description:
              This function is called from the default implementation of generateShellFaces,
              to process a single face in a shell along with the holes in that face.
              Vertex data needed for this function can be obtained by calling 
              vertexDataList and vertexData.
              Client applications have the option of overriding this function to process the face
              data themselves.  If the function is not overridden, the default implementation
              of this function processes the face information calls
              triangleOut with the resulting triangle data.

              Arguments:
                faceListSize (I) Number of entries in pFaceList.
                pFaceList (I) List of numbers that define the face itself, and the holes in this face.  The face will be the first sequence in pFaceList, followed optionally by one or more holes. See Vectorization Primitives for more details.
                pEdgeData (I) Pointer to additional edge data (color, layer, etc.).
                pFaceData (I) Pointer to additional face data (color, layer, etc.).
          
              See Also:
                OdGiEdgeData
                OdGiFaceData
          */
          virtual void shellFaceOut(OdInt32 faceListSize,
                                    const OdInt32* pFaceList,
                                    const OdGeVector3d* pNormal);

              /** Description:
                  This function is called from the default implementations of shellFaceOut
                  and generateMeshFaces.  Vertex data needed for this function
                  can be obtained by calling vertexDataList and vertexData.
                  Client applications have the option of overriding this function to process 
                  the triangle data themselves.  If the function is not overridden, 
                  the default implementation of this function processes the color 
                  attributes for the triangle, and calls polygonOut.

                  Arguments:
                    p3Vertices (I) List of numbers that define the vertices in the triangle.
                    pNormal (I) Normal vector for this triangle.
          
              */
              virtual void triangleOut(const OdInt32* p3Vertices,
                                       const OdGeVector3d* pNormal);


  /** Description:
      Auxiliary function that can be used to break a shell up into a set of faces, 
      each with a maximum number of edges.
      Vertex data needed for this function is obtained by calling 
      vertexDataList and vertexData (so the caller of this function is responsible for 
      setting this data).  Face data is passed to the facetOut function, which 
      can be overridden by client applications to capture the tessellated data.

      Arguments:
        faceListSize (I) Number of entries in pFaceList.
        pFaceList (I) List of numbers that define the faces in the shell.  See Vectorization Primitives for more details.
        pFaceData (I) Pointer to additional face data (color, layer, etc.).
        nMaxFacetSize (I) Maximum number of edges in the tessellated faces produced
          by this function.
      
      See Also:
        OdGiFaceData
  */
  virtual void generateShellFacets(OdInt32 faceListSize,
    const OdInt32* pFaceList,
    const OdGiFaceData* pFaceData = 0,
    OdInt32 nMaxFacetSize = 3);
  
      /** Description:
          This function is called from the default implementations of generateShellFacets.  
          Vertex data needed for this function is obtained by calling 
          vertexDataList and vertexData.
          Client applications have the option of overriding this function to process 
          the face data themselves.  If the function is not overridden, 
          the default implementation of this function calls polygonOut with the face data.

          Arguments:
            pFaceList (I) List of numbers that define the vertices in the triangle.  The first
              entry will be the "count", followed by "count" indices into the original 
              vertex list.  See Vectorization Primitives for more details.
            pEdgeIndices (I) Currently not used.
            pNormal (I) Normal vector for this face.
  
      */
      virtual void facetOut(const OdInt32* pFaceList,
        const OdInt32* pEdgeIndices,
        const OdGeVector3d* pNormal);

  /** Remarks:
      Client applications have the option of overriding this function to process the circle
      data themselves.  If the function is not overridden, the default implementation
      tessellates the passed in circle using the current kOdGiMaxDevForCircle deviation,
      and calls polylineProc with the resulting data.
  */
  virtual void circleProc(const OdGePoint3d& center,
    double radius, 
    const OdGeVector3d& normal,
    const OdGeVector3d* pExtrusion = 0);

  virtual void circleProc(const OdGePoint3d& start,
    const OdGePoint3d& point, 
    const OdGePoint3d& end,
    const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      Client applications have the option of overriding this function to process the circular arc
      data themselves.  If the function is not overridden, the default implementation
      tessellates the passed in circular arc using the current kOdGiMaxDevForCircle deviation,
      and calls polylineProc with the resulting data (for kOdGiArcSimple type arcs).  For 
      kOdGiArcSector and kOdGiArcChord arc types, polygonOut is called.
  */
  virtual void circularArcProc(const OdGePoint3d& center,
    double radius,
    const OdGeVector3d& normal,
    const OdGeVector3d& startVector,
    double sweepAngle,
    OdGiArcType arcType = kOdGiArcSimple,
    const OdGeVector3d* pExtrusion = 0);

  virtual void circularArcProc(const OdGePoint3d& start,
    const OdGePoint3d& point,
    const OdGePoint3d& end,
    OdGiArcType arcType = kOdGiArcSimple,
    const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      Client applications have the option of overriding this function to process the text
      data themselves.  If the function is not overridden, the default implementation
      tessellates the passed in text string, by calling the textProc function
      on the associated OdGiContext object.  TrueType text will result in calls to 
      shellProc, and SHX text will get sent to polylineProc and polygonProc.

  */
  virtual void textProc(const OdGePoint3d& position,
    const OdGeVector3d& u, 
    const OdGeVector3d& v,
    const OdChar* msg, 
    OdInt32 length, 
    bool raw,
    const OdGiTextStyle* pTextStyle,
    const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      Client applications have the option of overriding this function to process the shape
      data themselves.  If the function is not overridden, the default implementation
      tessellates the passed in shape, by calling the shapeProc function
      on the associated OdGiContext object.  The resulting geometry 
      will get sent to polylineProc and polygonProc.
  */
  virtual void shapeProc(const OdGePoint3d& position,
    const OdGeVector3d& u, 
    const OdGeVector3d& v,
    int shapeNo, 
    const OdGiTextStyle* pStyle,
    const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      Client applications have the option of overriding this function to process the NURBS
      data themselves.  If the function is not overridden, the default implementation
      tessellates the passed in NURBS curve using the current kOdGiMaxDevForCurve deviation,
      and calls polylineProc with the resulting data.
  */
  virtual void nurbsProc(const OdGeNurbCurve3d& nurbs);

  /** Remarks:
      Client applications have the option of overriding this function to process the elliptical arc
      data themselves.  If the function is not overridden, the default implementation
      tessellates the passed in elliptical arc using the current kOdGiMaxDevForCurve deviation,
      and calls polylineProc with the resulting data.
  */
  virtual void ellipArcProc(
    const OdGeEllipArc3d& ellipArc,
    const OdGePoint3d* pEndPointsOverrides = 0,
    OdGiArcType arcType = kOdGiArcSimple,
    const OdGeVector3d* pExtrusion = 0);

  /** Remarks:
      The default implementation of this function is a no-op.
  */
  virtual void rasterImageProc(const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiRasterImage* pImg, 
    const OdGePoint2d* uvBoundary, 
    OdUInt32 numBoundPts,
    bool transparency = false,
    double brightness = 50.0,
    double contrast = 50.0,
    double fade = 0.0);

  /** Remarks:
      The default implementation of this function is a no-op.
  */
  virtual void metafileProc(const OdGePoint3d& origin,
    const OdGeVector3d& u,
    const OdGeVector3d& v,
    const OdGiMetafile* pMetafile,
    bool bDcAligned = true,        
    bool bAllowClipping = false); 
};



inline void
OdGiGeometrySimplifier::setVertexData(OdInt32 nVertexCount,
                                      const OdGePoint3d* pVertexList,
                                      const OdGiVertexData* pVertexData)
{
  m_nVertexCount      = nVertexCount;
  m_pVertexList       = pVertexList;
  m_pVertexData       = pVertexData;
}

inline OdInt32
OdGiGeometrySimplifier::vertexDataCount() const
{
  return m_nVertexCount;
}

inline const OdGePoint3d*
OdGiGeometrySimplifier::vertexDataList() const
{
  return m_pVertexList;
}

inline const OdGiVertexData*
OdGiGeometrySimplifier::vertexData() const
{
  return m_pVertexData;
}

#include "DD_PackPop.h"

#endif //#ifndef __ODGIGEOMETRYSIMPLIFIER_H__

