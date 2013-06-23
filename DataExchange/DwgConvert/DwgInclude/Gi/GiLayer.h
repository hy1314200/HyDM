#ifndef _ODGILAYER_INCLUDED_
#define _ODGILAYER_INCLUDED_


class OdDbStub;

#include "OdaDefs.h"
#include "CmColor.h"
#include "OdString.h"

/** Description:

    {group:OdGi_Classes} 
*/
class OdGiLayer
{
	enum
	{
		kByBlock     = 1,
		kFrozen      = 2,
		kOff         = 4,
		kPlottable   = 8
	};

  OdUInt32          m_flags;
  OdDb::LineWeight  m_lineweight;
  OdCmEntityColor   m_color;
  OdDbStub*         m_linetypeId;
  OdString          m_plotStyleName;
public:
  OdGiLayer()
    : m_flags(0)
    , m_lineweight(OdDb::kLnWtByLwDefault)
    , m_color(OdCmEntityColor::kForeground)
    , m_linetypeId(0)
  { }

  bool isByBlock() const                          { return !GETBIT(m_flags, kByBlock); }
	void setByBlock(bool value)                     { SETBIT(m_flags, kByBlock, !value); }

  bool isFrozen() const                           { return GETBIT(m_flags, kFrozen); }
  void setIsFrozen(bool value)                    { SETBIT(m_flags, kFrozen, value); }

  bool isOff() const                              { return GETBIT(m_flags, kOff); }
  void setIsOff(bool value)                       { SETBIT(m_flags, kOff, value); }

  const OdCmEntityColor& color() const            { return m_color; }
  void setColor(const OdCmEntityColor& cl)        { m_color = cl; }

  OdDb::LineWeight lineweight() const             { return m_lineweight; }
  void setLineweight(OdDb::LineWeight lw)         { m_lineweight = lw; }

  OdDbStub* linetype() const                      { return m_linetypeId; }
  void setLinetype(OdDbStub* id)                  { m_linetypeId = id; }

  bool isPlottable() const                        { return GETBIT(m_flags, kPlottable); }
	void setPlottable(bool value)                   { SETBIT(m_flags, kPlottable, value); }

  OdString plotStyleName() const                  { return m_plotStyleName; }
  void setPlotStyleName(const OdString& newName)  { m_plotStyleName = newName; }
};


#endif //_ODGILAYER_INCLUDED_

