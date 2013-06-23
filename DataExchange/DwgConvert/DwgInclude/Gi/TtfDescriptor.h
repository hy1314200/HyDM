#ifndef _OdTtfDescriptor_h_Included_
#define _OdTtfDescriptor_h_Included_

#include "Gi.h"
#include "OdString.h"

/** Description:

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdTtfDescriptor
{
  // Members

	OdUInt32        m_nFlags;
  OdString        m_sFontFile;
	OdString        m_Typeface;

public:
  // Constructor

  OdTtfDescriptor() : m_nFlags(0) 
  {
  }

  OdTtfDescriptor(const OdChar* szTypeface, bool bold, bool italic, int charset, int pitchAndFamily)
    : m_Typeface(szTypeface)
  {
    setTtfFlags(bold, italic, charset, pitchAndFamily);
  }

  // Accessors /transformers

  OdString fileName() const     { return m_sFontFile; }
  OdString typeface() const     { return m_Typeface;  }
  OdUInt32 getTtfFlags() const  { return m_nFlags;    }

  void clearFileName()
  { 
    m_sFontFile.empty();  
  }
  void clearTypeface()
  { 
    m_Typeface.empty();  
  }
  void addTypeface(OdChar ch)
  { 
    m_Typeface += ch;
  }

	void getTtfFlags(bool& bBold, bool& bItalic, int& nCharset, int& nPitchAndFamily) const
  {
	  bBold = isBold();
	  bItalic = isItalic();
	  nCharset = charSet();
	  nPitchAndFamily = pitchAndFamily();
  }

  void setFileName(const OdString& filename)  { m_sFontFile = filename; }
  void setTypeFace(const OdString& typeface)  { m_Typeface = typeface;  }
  void setTtfFlags(OdUInt32 nFlags)           { m_nFlags = nFlags;      }

  void setTtfFlags(bool bold, bool italic, int charset, int pitchAndFamily)
  {
    setBold(bold);
    setItalic(italic);
    setCharSet(charset);
    setPitchAndFamily(pitchAndFamily);
  }

  void setBold(bool val)          { SETBIT(m_nFlags, 0x02000000, val);  }
  void setItalic(bool val)        { SETBIT(m_nFlags, 0x01000000, val);  }
  void setCharSet(int val)        { m_nFlags = (m_nFlags & 0xFFFF00FF | ((val << 8) & 0x0000FF00)); }
  void setPitchAndFamily(int val) { m_nFlags = (m_nFlags & 0xFFFFFF00 | (val & 0x000000FF));  }

  bool isBold() const             { return GETBIT(m_nFlags, 0x02000000);  }
  bool isItalic() const           { return GETBIT(m_nFlags, 0x01000000);  }
  OdUInt16 charSet() const        { return OdUInt16((m_nFlags & 0x0000ff00) >> 8); }
  int pitchAndFamily() const      { return (m_nFlags & 0x000000ff); }

  // PitchAndFamily 
  //  Specifies the pitch and family of the font. The two low-order bits specify the pitch of the font.
  //  Bits 4 through 7 of the member specify the font family.
  int getPitch() const            { return (pitchAndFamily() & 0x00000003); }
  int getFamily() const           { return (pitchAndFamily() & 0x000000f0); }


};

#endif // _OdTtfDescriptor_h_Included_
