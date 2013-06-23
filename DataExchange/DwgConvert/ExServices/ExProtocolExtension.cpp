// ExProtocolExtension.cpp: implementation of the ExProtocolExtension class.
//
//////////////////////////////////////////////////////////////////////

#include "OdaCommon.h"
#include "ExProtocolExtension.h"
#include "RxObjectImpl.h"

#include "Db2dPolyline.h"
#include "Db3dPolyline.h"
#include "Db3dPolylineVertex.h"
#include "DbPolyFaceMesh.h"
#include "DbPolyFaceMeshVertex.h"
#include "DbFaceRecord.h"
#include "DbPolygonMesh.h"
#include "DbPolygonMeshVertex.h"
#include "DbBlockReference.h"
#include "DbBlockTableRecord.h"
#include "DbAttribute.h"
#include "DbMInsertBlock.h"
#include "DbSpline.h"
#include "DbEllipse.h"
#include "DbSolid.h"
#include "DbTrace.h"
#include "DbHatch.h"
#include "DbCircle.h"
#include "Db3dSolid.h"
#include "DbRegion.h"
#include "DbMText.h"
#include "DbMline.h"
#include "DbRasterImage.h"
#include "DbOle2Frame.h"
#include "Ge/GeKnotVector.h"
#include "DbProxyEntity.h"
#include "GiWorldDrawDumper.h"
#include "StaticRxObject.h"
#include "DbSpatialFilter.h"
#include "DbIndex.h"
#include "DbPolyline.h"
#include "DbArcAlignedText.h"
#include "Ge/GeCircArc3d.h"
#include "Ge/GeCurve2d.h"
#include "Ge/GeEllipArc2d.h"
#include "Ge/GeNurbCurve2d.h"
#include "OdFileBuf.h"
#include "GeometryFromProxy.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

ODRX_NO_CONS_DEFINE_MEMBERS(OdDbEntity_Dumper, OdRxObject)

ExProtocolExtension::ExProtocolExtension()
{

}

ExProtocolExtension::~ExProtocolExtension()
{
  if(m_pDumpers)
    uninitialize();
}

// dumps common data (for all entities)
void dumpCommonData(OdDbEntity* pEnt, STD(ostream) & os)
{
  OdDbHandle    hTmp;
  char          buff[20];

  hTmp = pEnt->getDbHandle();
  hTmp.getIntoAsciiBuffer(buff);
  os << "    " << pEnt->isA()->name() << ", " << buff << STD(endl);
}

// this method is called for all entities for which there 
// aren't peculiar approach yet
void OdDbEntity_Dumper::dump(OdDbEntity* pEnt, STD(ostream) & os) const
{
  dumpCommonData(pEnt, os);
  
  os << "      " << "Entity graphics data:" << STD(endl);
  
  // Dump the graphics data of "unknown" entity
  // graphics for proxy entity are retrieved by the same way
  OdGiContextDumper ctx(pEnt->database());
  OdGiWorldDrawDumper wd(os);
  wd.setContext(&ctx);
  pEnt->worldDraw(&wd);
} // end OdDbEntity_Dumper::dump

class OdDb2dPolyline_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDb2dPolylinePtr pPoly = pEnt;
    OdDbObjectIteratorPtr pIter = pPoly->vertexIterator();
	
    for (; !pIter->done(); pIter->step())
    {
      OdDb2dVertexPtr pVertex = pIter->entity();
      if (pVertex.get())
      {
        os << "      " << pVertex->isA()->name() << STD(endl);
      }
    }
  }
}; // end class OdDb2dPolyline_Dumper


class OdDb3dPolyline_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDb3dPolylinePtr pPoly = pEnt;
    OdDbObjectIteratorPtr pIter = pPoly->vertexIterator();
    for (; !pIter->done(); pIter->step())
    {
      OdDb3dPolylineVertexPtr pVertex = pIter->entity();
      if (pVertex.get())
      {
        os << "      " << pVertex->isA()->name() << STD(endl);
      }
    }
  }
};


class OdDbPolyFaceMesh_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbPolyFaceMeshPtr pPoly = pEnt;
    int nVerts = pPoly->numVertices();
    int nFaces = pPoly->numFaces();
        os << "      " << "N Verts: " << nVerts << "\tN Faces: " << nFaces << STD(endl);


    OdDbObjectIteratorPtr pIter = pPoly->vertexIterator();
    for (; !pIter->done(); pIter->step())
    {
      OdDbPolyFaceMeshVertexPtr pVertex = pIter->entity()->queryX(OdDbPolyFaceMeshVertex::desc());
      if (!pVertex.isNull())
      {
        OdGePoint3d pos(pVertex->position());
        os << "      " << pVertex->isA()->name() << "\t\t" << pos.x << ",\t" << pos.y << ",\t" << pos.z <<STD(endl);
      }
      else
      {
        OdDbFaceRecordPtr pFace = pIter->entity()->queryX(OdDbFaceRecord::desc());
        if (!pFace.isNull())
        {
          os << "      " << pFace->isA()->name() << "\t\tVertex indices:";
          for (int i = 0; i < 4; ++i)
          {
            os << "\t" << pFace->getVertexAt(i);
          }
          os << STD(endl);
        }
        else
        { // Unknown entity type
          os << "      " << pVertex->isA()->name() << STD(endl);
        }
      }
    }
  }
};


class OdDbPolygonMesh_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbPolygonMeshPtr pPoly = pEnt;
    OdDbObjectIteratorPtr pIter = pPoly->vertexIterator();
    for (; !pIter->done(); pIter->step())
    {
      OdDbPolygonMeshVertexPtr pVertex = pIter->entity();
      if (pVertex.get())
      {
        os << "      " << pVertex->isA()->name() << STD(endl);
      }
    }
  }
};


class OdDbBlockReference_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbBlockReferencePtr pBlkRef = pEnt;

    OdDbObjectIteratorPtr pIter = pBlkRef->attributeIterator();
    for (; !pIter->done(); pIter->step())
    {
      OdDbAttributePtr pAttr = pIter->entity();
      if (!pAttr.isNull())
      {
        os << "      " << pAttr->isA()->name() << STD(endl);
      }
    }
    OdDbSpatialFilterPtr pFilt = OdDbIndexFilterManager::getFilter(pBlkRef,
      OdDbSpatialFilter::desc(), OdDb::kForRead);
    if(pFilt.get())
    {
      os << "      " << "Associated spatial filter: " << STD(endl);
      OdGePoint2dArray pts;
      OdGeVector3d normal;
      double elevation, frontClip, backClip;
      bool   enabled;
      pFilt->getDefinition(pts, normal, elevation, frontClip, backClip, enabled);
      os << "      " << "Points: " << STD(endl);
      for(OdGePoint2dArray::iterator pPoint = pts.begin(); pPoint != pts.end(); ++pPoint)
      {
        os << "      " << "(" << pPoint->x << ", "<< pPoint->y << ")" << STD(endl);
      }
      os << "      " << "Normal: (" << normal.x << ", " << normal.y << ", " << normal.z << ")" << STD(endl);
      os << "      " << "elevation: " << elevation << STD(endl);
      os << "      " << "frontClip: " << frontClip << STD(endl);
      os << "      " << "backClip: " << backClip << STD(endl);
      os << "      " << "enabled: " << enabled << STD(endl);
    }
  }
};


class OdDbMInsertBlock_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbMInsertBlockPtr pPoly = pEnt;
    OdDbObjectIteratorPtr pIter = pPoly->attributeIterator();
    for (; !pIter->done(); pIter->step())
    {
      OdDbAttributePtr pAttr = pIter->entity();
      if (!pAttr.isNull())
      {
        os << "      " << pAttr->isA()->name() << STD(endl);
      }                               
    }
  }
};


class OdDbSpline_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbSplinePtr pSpline = pEnt;

    int degree;
    bool rational, closed, periodic;
    OdGePoint3dArray ctrlPts;
    OdGeDoubleArray weights;
    OdGeKnotVector  knots;
    double tol;

    pSpline->getNurbsData(degree, rational, closed, periodic, ctrlPts, knots, weights, tol);
    os << "      " << "Degree: " << degree << ", Closed: " << closed << STD(endl);
    unsigned i;
    os << "      " << "Control Points: " << STD(endl);
    for (i = 0; i < ctrlPts.size(); i++)
    {
      os << "        " << ctrlPts[i].x << ", " << ctrlPts[i].y << ", " << ctrlPts[i].z << STD(endl);
    }

    os << "      " << "Knots: " << STD(endl);
    for (i = 0; i < (unsigned)knots.length(); i++)
    {
      os << "        " << knots[i] << STD(endl);
    }

    if (rational)
    {
      os << "      " << "Weights: " << STD(endl);
      for (i = 0; i < weights.size(); i++)
      {
        os << "        " << weights[i] << STD(endl);
      }
    }
  }
};


class OdDbEllipse_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbEllipsePtr pEll = pEnt;
    OdGePoint3d cen,cen1;
    OdGeVector3d normal_vector,major;
    double sang,eang,ratio;
    
    os << "      " << "Center: " << pEll->center().x << ", " << pEll->center().y << ", " << pEll->center().z << STD(endl);
    os << "      " << "Start Param: " << pEll->paramAtAngle(pEll->startAngle()) << STD(endl);
    os << "      " << "End Param: " << pEll->paramAtAngle(pEll->endAngle()) << STD(endl);
    os << "      " << "Radius Ratio: " << pEll->radiusRatio() << STD(endl);
    os << "      " << "Major Axis: " << pEll->majorAxis().x << ", " << pEll->majorAxis().y << ", " << pEll->majorAxis().z << STD(endl);
    os << "      " << "Normal: " << pEll->normal().x << ", " << pEll->normal().y << ", " << pEll->normal().z << STD(endl);
    
    pEll->get(cen1,normal_vector,major,ratio,sang,eang); 
    

  }
};


class OdDbSolid_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);
    OdDbSolidPtr pSolid = pEnt;
    OdGePoint3d pt;
    pSolid->getPointAt(0, pt);
    os << "      " << "Point 0: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
    pSolid->getPointAt(1, pt);
    os << "      " << "Point 1: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
    pSolid->getPointAt(2, pt);
    os << "      " << "Point 2: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
    pSolid->getPointAt(3, pt);
    os << "      " << "Point 3: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
  }
};


class OdDbTrace_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);
    OdDbTracePtr pTrace = pEnt;
    OdGePoint3d pt;
    pTrace->getPointAt(0, pt);
    os << "      " << "Point 0: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
    pTrace->getPointAt(1, pt);
    os << "      " << "Point 1: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
    pTrace->getPointAt(2, pt);
    os << "      " << "Point 2: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
    pTrace->getPointAt(3, pt);
    os << "      " << "Point 3: " << pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
  }
};


class OdDb3dSolid_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    OdDb3dSolidPtr   pSolid;

    dumpCommonData(pEnt, os);
    pSolid = pEnt;

    os << "      " << " it is 3D Solid object " << STD(endl);
  }
};


class OdDbProxyEntity_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    // this will dump proxy entity graphics
    OdDbEntity_Dumper::dump(pEnt, os);

    // If the entity is of type AcAdPart, then dump the 
    // associated SAT file.
    OdDbProxyEntityPtr pProxy(pEnt);
    OdString satString;
    if (pProxy->originalClassName() == "AcAdPart" &&
        odGetSatFromProxy(pProxy, satString))
    {
      os << "      " << "AD_PART SAT file: " 
        << STD(endl) << satString.c_str();
    }      
  }
}; // end OdDbProxyEntity_Dumper


class OdDbPolyline_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbPolylinePtr pPoly = pEnt;
    for (unsigned int i = 0; i < pPoly->numVerts(); i++)
    { 
      OdGePoint3d pt;
      pPoly->getPointAt(i, pt);
      os << "      " << "Point: " << 
        pt.x << ", " << pt.y << ", " << pt.z << STD(endl);
      double bulge = pPoly->getBulgeAt(i);
      if (pPoly->segType(i) == OdDbPolyline::kArc)
      {
        os << "      " << "Bulge: " << bulge << STD(endl);
        if (i < (pPoly->numVerts() - 1) || pPoly->isClosed())
        {
          OdGeCircArc3d arc;
          pPoly->getArcSegAt(i, arc);
          OdGePoint3d start, end;
          start = arc.startPoint();
          end = arc.endPoint();
          os << "      " << "Arc Start: " << start.x << ", " << 
            start.y << ", " << start.z << STD(endl);
          os << "      " << "Arc End: " << end.x << ", " << 
            end.y << ", " << end.z << STD(endl);
        }
      }

    }
  }
}; // end class OdDbPolyline_Dumper


class OdDbHatch_Dumper : public OdDbEntity_Dumper
{
private:
  static void dumpPolylineType( int loopIndex , OdDbHatchPtr &pHatch, STD(ostream) & os ){
    OdGePoint2dArray vertices;
    OdGeDoubleArray bulges;
    
    pHatch->getLoopAt (loopIndex, 
      vertices, 
      bulges);
    
    bool hasbulges = vertices.size () == bulges.size () ? true: false ;
    OdGeDoubleArray::const_iterator blgs = bulges.begin ();
    double bulge;
    
    for (OdGePoint2dArray::const_iterator verts = vertices.begin(); vertices.end () != verts; ++verts) {
      
      if (hasbulges)
      {
        bulge = *blgs;
        blgs++;
      }else
        bulge = 0.0;
      os << "      " << (*verts).x << "," << (*verts).y << "," <<  0.0 <<  4.0 * atan (bulge) << STD(endl);
    }
  }

  static void dumpEllipticedge( OdGeEllipArc2d* pEllipArc , STD(ostream) & os ) {
    
    if (pEllipArc == NULL)
      return ;
    os << "      Center: " <<  pEllipArc->center().x << "," << pEllipArc->center().y << STD(endl);
    os << "      MinorRadius: " <<  pEllipArc->minorRadius() << STD(endl);
    os << "      MajorRadius: " <<  pEllipArc->majorRadius() << STD(endl);
    os << "      MinorAxis: " <<  pEllipArc->minorAxis().x << "," << pEllipArc->minorAxis().y << STD(endl);
    os << "      MajorAxis: " <<  pEllipArc->majorAxis().x << "," << pEllipArc->majorAxis().y << STD(endl);
    os << "      StartAng: " <<  pEllipArc->startAng() << STD(endl);
    os << "      EndAng: " <<  pEllipArc->endAng()     << STD(endl);
    os << "      Startpoint: " <<  pEllipArc->startPoint().x << "," <<  pEllipArc->startPoint().y << STD(endl);
    os << "      Endpoint: " <<  pEllipArc->endPoint().x << "," << pEllipArc->endPoint().y << STD(endl);
    os << "      Direction: " <<  (( pEllipArc->isClockWise() ) ? "ClockWise" : "CounterClockWise") << STD(endl);
    
    OdGePoint2dArray    pts;
    pEllipArc->getSamplePoints (pEllipArc->startAng (), pEllipArc->endAng (), OdaPI / 30 , pts);
    os << "      SamplePoint Data: " << STD(endl);
    for( unsigned int i  = 0 ; i < pts.length() ; i++ ){
      os << "POINT " << pts[i].x << "," << pts[i].y << STD(endl);
      //os << "         " << pts[i].x << "," << pts[i].y << STD(endl);
    }
  } 

  static void dumpEdgesType( int loopIndex , OdDbHatchPtr &pHatch , STD(ostream) & os ){
    
    EdgeArray edges;
    
    pHatch->getLoopAt (loopIndex, edges);
    
    for (EdgeArray::const_iterator edge = edges.begin (); edge != edges.end (); ++edge) {
      OdGeCurve2d* pEdge = *edge;
      os << "     Edge";
      switch (pEdge->type ()) {
      case OdGe::kLineSeg2d:
        {
          OdGeLineSeg2d* pLineSeg = (OdGeLineSeg2d*)pEdge;
          os << " LineSeg2d";
          //dumpLineSeg2d( pLineSeg , os );
        }
        break;
        
      case OdGe::kCircArc2d:
        {
                        OdGeCircArc2d* pCircArc = (OdGeCircArc2d*)pEdge;
            os << " CircArc2d";
            //dumpCircArc( pCircArc , os );
        }
        break;
        
      case OdGe::kEllipArc2d:
        {
          OdGeEllipArc2d* pEllipArc = (OdGeEllipArc2d*)pEdge;
          os << " EllipseArc2d";
                dumpEllipticedge( pEllipArc , os );
        }
        break;
        
      case OdGe::kNurbCurve2d:
        {
          OdGeNurbCurve2d* pNurbCurve = (OdGeNurbCurve2d*)pEdge;
          os << " NurbCurve2d";
          //dumpNurbCurve2d( pNurbCurve , os );
          break;
        }
        break;
      }
      os << STD(endl);
    }
  }

public:
  
  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);
    
    OdDbHatchPtr pHatch = pEnt;/* OdDbHatch */
    if( !pHatch->numLoops() ){
      os << "      " << "Hatch has no loops" << STD(endl);
      return;
    }
    for(int i = 0 ; i < pHatch->numLoops() ; i++ )
    {
      os << "      " << "Loop " << i << " is";
      OdInt32 loopType = pHatch->loopTypeAt( i );
      if( loopType & OdDbHatch::kExternal )
        os << " External";
      if( loopType & OdDbHatch::kDerived )
        os << " Derived";
      if( loopType & OdDbHatch::kTextbox )
        os << " Textbox";
      if( loopType & OdDbHatch::kOutermost )
        os << " Outermost";
      if( loopType & OdDbHatch::kNotClosed )
        os << " NotClosed";
      if( loopType & OdDbHatch::kSelfIntersecting )
        os << " SelfIntersecting";
      if( loopType & OdDbHatch::kTextIsland )
        os << " TextIsland";
      if( loopType & OdDbHatch::kPolyline ){
        dumpPolylineType( i , pHatch, os );
        os << " Polyline" << STD(endl);
      }else{
        os << STD(endl);
        dumpEdgesType( i , pHatch , os );
      }
    }
  }
private:
};



class OdDbCircle_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbCirclePtr pCircle = pEnt;/*OdDbCircle*/
    os << "AcDbCircle:" << STD(endl);
    os << "     Center: " << pCircle->center().x << "," << pCircle->center().y << "," << pCircle->center().z << STD(endl);
    os << "     Radius: " << pCircle->radius() << STD(endl);
    os << "     Thickness: " << pCircle->thickness() << STD(endl); 
    os << "     Normal: " << pCircle->normal().x << "," << pCircle->normal().y << "," << pCircle->normal().z << STD(endl);
  }
}; // end class OdDbCircle_Dumper


class OdDbRegion_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);
    OdDbRegionPtr pRegion = pEnt;

    OdWrFileBuf ow("RegionAcisOut.acis");
    pRegion->acisOut( &ow , kAfTypeASCII );

  }
}; // end class OdDbCircle_Dumper


class OdDbMText_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);
    OdDbMTextPtr pMText = pEnt;
    os << "AcDbMText:" << STD(endl);
    OdGePoint3dArray array;
    pMText->getBoundingPoints( array );
    os << "   Bounding Data:" << STD(endl);
    for(unsigned int i = 0 ; i < array.size() ; i++)
      os << "         " << array[i].x << "," << array[i].y << "," << array[i].z << STD(endl);
  }
}; // end class OdDbMtext_Dumper


class OdDbText_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);
    OdDbTextPtr pText = pEnt;//OdDbEntity OdDbText
    os << "AcDbText:" << STD(endl);
    OdGePoint3dArray array;
    pText->getBoundingPoints( array );
    os << " Normal Vector: " << pText->normal().x << "," << pText->normal().y << "," << pText->normal().z << STD(endl);

    os << "   Bounding Data:" << STD(endl);
    os << "LINE" << STD(endl);
    for(unsigned int i = 0 ; i < array.size() ; i++){
      OdGePoint3d pt = array[i];

      os << pt.x << "," << pt.y << "," << pt.z << STD(endl);

    }
  }
}; // end class OdDbMtext_Dumper


class OdDbMline_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbMlinePtr pMline = pEnt; // OdDbMline
  
    int n = pMline->numVertices();
    for (int i = 0; i < n; i++)
    {
      OdMLSegmentArray p;
      pMline->getParametersAt(i, p);
      os << "    Segment: " << i << ", " << p.size() << " parameters:" << STD(endl);
	  // pmk  '<' : signed/unsigned mismatch
      for (int j = 0; j < (int) p.size(); j++)
      {
        int k;
        os << "      41: ";
        // pmk  '<' : signed/unsigned mismatch
        for (k = 0; k < (int) p[j].m_SegParams.size(); k++)
        {
          os << p[j].m_SegParams[k] << ", ";
        }
        os << STD(endl);
        os << "      42: ";
        // pmk  '<' : signed/unsigned mismatch
        for (k = 0; k < (int) p[j].m_AreaFillParams.size(); k++)
        {
          os << p[j].m_AreaFillParams[k] << ", ";
        }
        os << STD(endl);
      }
    }
  }
}; 


class OdDbRasterImage_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbRasterImagePtr pImage = pEnt;  
    OdGePoint3d origin;
    OdGeVector3d u,v,u2,v2;
    pImage->getOrientation (origin,u,v);
    u.normalize();
    v.normalize();
    int i = 4;//break;
  }
}; 


class OdDbArcAlignedText_Dumper : public OdDbEntity_Dumper
{
public:

  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);

    OdDbArcAlignedTextPtr pAAT = pEnt;  
  }
}; 

class OdDbOle2Frame_Dumper : public OdDbEntity_Dumper
{
public:
  void dump(OdDbEntity* pEnt, STD(ostream) & os) const
  {
    dumpCommonData(pEnt, os);
    OdDbOle2FramePtr pOle = pEnt;
    
    int type = pOle->getType();
    
    OdString str = pOle->getUserType();
    
    os << "\n\t ===== OLE2FRAME entity data =====\n";
    os << "\n\t type = "<<type << "\n\t user type string\'"<<str.c_str()<<"\'";
    os << "\n\t =================================\n";
      

  }
}; // end class OdDbOle2Frame_Dumper


class Dumpers
{
  OdStaticRxObject<OdDbEntity_Dumper>         m_entityDumper;
  OdStaticRxObject<OdDbRegion_Dumper>         m_regionDumper;
  OdStaticRxObject<OdDbPolyline_Dumper>       m_polylineDumper;
  OdStaticRxObject<OdDb2dPolyline_Dumper>     m_2dPolylineDumper;
  OdStaticRxObject<OdDb3dPolyline_Dumper>     m_3dPolylineDumper;
  OdStaticRxObject<OdDbPolyFaceMesh_Dumper>   m_polyFaceMeshDumper;
  OdStaticRxObject<OdDbPolygonMesh_Dumper>    m_polygonMesh;
  OdStaticRxObject<OdDbBlockReference_Dumper> m_blockReference;
  OdStaticRxObject<OdDbMInsertBlock_Dumper>   m_mInsertBlock;
  OdStaticRxObject<OdDbSpline_Dumper>         m_splineDumper;
  OdStaticRxObject<OdDbEllipse_Dumper>        m_ellipseDumper;
  OdStaticRxObject<OdDbSolid_Dumper>          m_solidDumper;
  OdStaticRxObject<OdDbTrace_Dumper>          m_traceDumper;
  OdStaticRxObject<OdDb3dSolid_Dumper>        m_3DSolidDumper;
  OdStaticRxObject<OdDbProxyEntity_Dumper>    m_proxyEntityDumper;
  OdStaticRxObject<OdDbHatch_Dumper>          m_hatchDumper;
  OdStaticRxObject<OdDbCircle_Dumper>         m_circleDumper;
  OdStaticRxObject<OdDbMText_Dumper>          m_mTextDumper;
  OdStaticRxObject<OdDbText_Dumper>           m_textDumper;
  OdStaticRxObject<OdDbMline_Dumper>          m_mlineDumper;
  OdStaticRxObject<OdDbRasterImage_Dumper>    m_imageDumper;
  OdStaticRxObject<OdDbArcAlignedText_Dumper> m_arcAlignedTextDumper;
  OdStaticRxObject<OdDbOle2Frame_Dumper>      m_ole2FrameDumper;

public:
  void addXs()
  {
    OdDbEntity::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_entityDumper);
    OdDbRegion::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_regionDumper);
    OdDbPolyline::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_polylineDumper);
    OdDb2dPolyline::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_2dPolylineDumper);
    OdDb3dPolyline::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_3dPolylineDumper);
    OdDbPolyFaceMesh::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_polyFaceMeshDumper);
    OdDbPolygonMesh::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_polygonMesh);
    OdDbBlockReference::desc()->addX(
    OdDbEntity_Dumper::desc(), &m_blockReference);
    OdDbMInsertBlock::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_mInsertBlock);
    OdDbSpline::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_splineDumper);
    OdDbEllipse::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_ellipseDumper);
    OdDbSolid::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_solidDumper);
    OdDbTrace::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_traceDumper);
    OdDb3dSolid::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_3DSolidDumper);
    OdDbProxyEntity::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_proxyEntityDumper);
    OdDbHatch::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_hatchDumper);
    OdDbCircle::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_circleDumper);
    OdDbMText::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_mTextDumper);
    OdDbText::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_textDumper);
    OdDbMline::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_mlineDumper);
    OdDbRasterImage::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_imageDumper);
    OdDbArcAlignedText::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_arcAlignedTextDumper);
    OdDbOle2Frame::desc()->addX(
      OdDbEntity_Dumper::desc(), &m_ole2FrameDumper);
  } // end addXs

  void delXs()
  {
    OdDbEntity::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbRegion::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbPolyline::desc()->delX(OdDbEntity_Dumper::desc());
    OdDb2dPolyline::desc()->delX(OdDbEntity_Dumper::desc());
    OdDb3dPolyline::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbPolyFaceMesh::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbPolygonMesh::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbBlockReference::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbMInsertBlock::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbSpline::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbEllipse::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbSolid::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbTrace::desc()->delX(OdDbEntity_Dumper::desc());
    OdDb3dSolid::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbProxyEntity::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbHatch::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbCircle::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbMText::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbText::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbMline::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbRasterImage::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbArcAlignedText::desc()->delX(OdDbEntity_Dumper::desc());
    OdDbOle2Frame::desc()->delX(OdDbEntity_Dumper::desc());
  }
};

void ExProtocolExtension::initialize()
{
  // Register OdDbEntity_Dumper with DWGdirect
  OdDbEntity_Dumper::rxInit();
  m_pDumpers = new Dumpers;
  m_pDumpers->addXs();
  
}//  end ExProtocolExtension::initialize()

void ExProtocolExtension::uninitialize()
{
  m_pDumpers->delXs();
  OdDbEntity_Dumper::rxUninit();
  delete m_pDumpers;
  m_pDumpers = 0;
}



