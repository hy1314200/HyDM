// ODA typedefs
#ifndef _GICONTEXTFORDBDATABASE_INCLUDED_
#define _GICONTEXTFORDBDATABASE_INCLUDED_

#include "Gi/GiCommonDraw.h"
#include "Gi/GiSubEntityTraitsData.h"
#include "DbDatabase.h"
#include "Gi/GiLinetyper.h"

#include "StaticRxObject.h"
#include "DbDatabaseReactor.h"
#include "Ps/PlotStyles.h"

#include "DD_PackPush.h"

struct OdGsClientViewInfo;

/** Description:

    {group:OdGi_Classes} 
*/
class TOOLKIT_EXPORT OdGiDefaultContext : public OdGiContext
{
private:
  class DatabaseHolder : public OdStaticRxObject<OdDbDatabaseReactor>
  {
  public:
    DatabaseHolder();
    ~DatabaseHolder();

    OdDbDatabase* m_pDb;

    void setDatabase(OdDbDatabase * pDb);

    void goodbye(const OdDbDatabase* pDb);
  }
  m_DbHolder;

public:
  OdGiDefaultContext();
  ~OdGiDefaultContext();

  OdDbDatabase* database() const;
  void setDatabase(OdDbDatabase* pDb);

  OdGiDrawablePtr openDrawable(OdDbStub* id);

  OdUInt32 numberOfIsolines() const;
  double commonLinetypeScale() const;

  OdDb::LineWeight defaultLineWeight() const;

  void getDefaultTextStyle(OdGiTextStyle& style);

  void drawText(OdGiConveyorGeometry* pDest,
    const OdGePoint3d& position,
    const OdGeVector3d& u, const OdGeVector3d& v,
    const OdChar* msg, OdInt32 nLength, bool raw,
    const OdGiTextStyle* pStyle,
    const OdGeVector3d* pExtrusion);

  void drawShape(OdGiConveyorGeometry* pDest,
    const OdGePoint3d& position,
    const OdGeVector3d& u, const OdGeVector3d& v,
    int shapeNo, const OdGiTextStyle* pStyle,
    const OdGeVector3d* pExtrusion);

  virtual void fillGsClientViewInfo(const OdDbObjectId& vpId, OdGsClientViewInfo& viewInfo);

  DD_USING(OdGiContext::drawText);
  DD_USING(OdGiContext::drawShape);
};

/** Description:

    {group:OdGi_Classes} 
*/
class TOOLKIT_EXPORT OdGiContextForDbDatabase : public OdGiDefaultContext
{
protected:
  OdUInt32              m_flags;
  ODCOLORREF            m_paletteBackground;
  OdPsPlotStyleTablePtr m_pPlotStyleTable;

  OdGiContextForDbDatabase();
public:

  ODRX_DECLARE_MEMBERS(OdGiContextForDbDatabase);

  void setFlags(OdUInt32 flags) { m_flags = flags; }

 	OdUInt32 flags() const { return m_flags; }

  void drawShape(OdGiCommonDraw* pDraw, OdGePoint3d& pos, int shapeNo, const OdGiTextStyle* pStyle);

  void drawText(OdGiCommonDraw* pDraw, OdGePoint3d& pos,
	  const OdChar* msg, OdInt32 nLength,
    const OdGiTextStyle* pStyle, OdUInt32 flags = 0);

  void drawText(OdGiCommonDraw* pDraw, OdGePoint3d& pos,
    double height, double width, double oblique, const OdChar* msg);

  DD_USING(OdGiDefaultContext::drawText);
  DD_USING(OdGiDefaultContext::drawShape);

  unsigned int circleZoomPercent(OdDbStub* vpObjectId) const;

  bool useGsModel() const;
  void enableGsModel(bool bEnable);

  bool isPlotGeneration() const;
  void setPlotGeneration(bool bPlotGeneration) { SETBIT(m_flags, kOdGiPlotGeneration, bPlotGeneration); }

  bool fillTtf() const;

  ODCOLORREF paletteBackground() const { return m_paletteBackground; }
  void setPaletteBackground(ODCOLORREF paletteBackground) { m_paletteBackground = paletteBackground; }

  void textExtentsBox(const OdGiTextStyle& style, const OdChar* pStr, int nStrLen,
    OdUInt32 flags, OdGePoint3d& min, OdGePoint3d& max, OdGePoint3d* pEndPos = 0);

  void shapeExtentsBox(const OdGiTextStyle& style, int shapeNo, 
    OdGePoint3d& min, OdGePoint3d& max);

  void loadPlotStyleTable(OdStreamBuf* pPstFile);

  virtual PStyleType plotStyleType() const;

  virtual void plotStyle(int nPenNumber, OdPsPlotStyleData& data) const;

  virtual void plotStyle(OdDbStub* psNameId, OdPsPlotStyleData& data) const;
};

typedef OdSmartPtr<OdGiContextForDbDatabase> OdGiContextForDbDatabasePtr;


#include "DD_PackPop.h"

#endif //_GICONTEXTFORDBDATABASE_INCLUDED_

