#ifndef _DbTextIterator_h_Included_
#define _DbTextIterator_h_Included_

#include "DbExport.h"
#include "OdCodePage.h"
#include "Gi/GiTextStyle.h"

class TOOLKIT_EXPORT OdDbTextIterator;
typedef OdSmartPtr<OdDbTextIterator> OdDbTextIteratorPtr;

#include "DD_PackPush.h"

/** Description:
    Class that can be used to parse the text received in a client override of
    OdGiConveyorGeometry::textProc.

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbTextIterator : public OdRxObject
{
public:
  /** Description:
      Creates an OdDbTextIterator object for the passed in data.

      Arguments:
      str (I) Text string to be parsed.
      nLen (I) Length of str in bytes.
      bRaw (I) If true, special characters in the form of %%c will be treated as 
               raw text, i.e. they will be displayed as %%c rather than as the indicated
               special character.
      codepage (I) Codepage of the passed in text.
      pStyle (I) OdGiTextStyle object that defines the style to be used for the passed in text.

      Return Value:
      Smart pointer to a new OdDbTextIterator object.
  */
  static OdDbTextIteratorPtr createObject(const OdChar* str, 
                                          int nLen, 
                                          bool bRaw, 
                                          OdCodePageId codepage, 
                                          const OdGiTextStyle* pStyle);

  /** Description:
      Retrieves the next character from the string associated with this OdDbTextIterator object.

      Remarks:
      The returned character will be a Unicode character in all cases, except for when the 
      bInBigFont flag is set in the currProperties() value.  In this case, the returned character
      will be MBCS (corresponding to a \M+NXXXX character in the original string).
  */
  virtual OdUInt16 nextChar() = 0;

  /** Description:
      Returns the properties for the last character returned by the nextChar function.
  */
	virtual const OdCharacterProperties& currProperties() const = 0;

  /** Description:
      Returns a pointer to the current position of this iterator in the string.
  */
	virtual const OdChar* currPos() const = 0;
};

#include "DD_PackPop.h"

#endif
