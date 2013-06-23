#ifndef _OdWString_h_Included_
#define _OdWString_h_Included_

#include <stdarg.h>
#include <stdio.h>

#include "OdPlatform.h"

#define odaIsValidString(lpch)    (true)

#include "DD_PackPush.h"

#ifdef sgi
#include <string>
/** Description: 
  
    {group:DD_Namespaces}
*/
using namespace std;  // va_XXXX items are in std namespace for newest SGI compiler
#endif

typedef wchar_t OdCharW;
class OdString;

/** Description:
    Raw data used by OdWString.

    {group:Structs}
*/
struct OdWStringData
{            
   /**  reference count */
  long nRefs;      
   /** length of data (including terminator) */
  int nDataLength;  
   /** length of allocation */
  int nAllocLength;       

   /** OdCharW* to managed data
   */
  OdCharW* data()           
    { return (OdCharW*)(this+1); }
};

/** Description:
  {group:Structs}
*/      
struct OdEmptyWStringData
{
  OdWStringData  kStrData;
  OdCharW        kChar;
};

/** Description:
    Represents a DWGdirect string.

    Remarks:
    OdWString objects use zero-based indices to access string elements.

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdWString
{
public:
  /** Constructor. */
  OdWString();

  /** Copy Constructor.
   */
  OdWString(const OdWString& stringSrc);

  /** Constructor. Initialize this string to a sequence of nRepeat instances of the 
  character ch.
   */
  OdWString(OdCharW ch, int nRepeat);

  /** Constructor.  This OdWString will receive a copy of the character string referenced
  by lpsz.
   */
  OdWString(const OdCharW* lpsz);

  /** Constructor. This OdWString will receive a copy of the first nLength characters of
  the string referenced by lpch.
   */
  OdWString(const OdCharW* lpch, int nLength);
  
  /** Constructor. Converts multibyte string to wide-char.
   */
  OdWString( const OdString& );

  /** Returns the number of characters in this string.
   */
  int getLength() const;

  /** Returns true if this string is empty, otherwise false.
   */
  bool isEmpty() const;

  /** Clears the contents of this string, setting its contents to the empty string (length 0).
   */
  void empty();

  /** Returns the single character at index nIndex of the string.  No range checking is done.
   */
  OdCharW getAt(int nIndex) const;

  /** Returns the single character at index nIndex of the string.  No range checking is done.
   */
  OdCharW operator[](int nIndex) const;

  /** Sets the single character at index nIndex of the string.  No range checking is done.
   */
  void setAt(int nIndex, OdCharW ch);

  /** Returns a const pointer to the underlying OdCharW array referenced by this OdWString.
   */
  const OdCharW* c_str() const;

  /** Conversion operator returns a const pointer to the underlying OdCharW array 
  referenced by this OdWString.
   */
  operator const OdCharW*() const;

  /** Assignment operator.  This OdWString is assigned the value of stringSrc.
   */
  const OdWString& operator=(const OdWString& stringSrc);

  /** Assignment operator.  This OdWString is assigned the value of a single character, ch.
   */
  const OdWString& operator=(OdCharW ch);
  
  /** Assignment operator.  This OdWString is assigned the value of lpsz.
   */
  const OdWString& operator=(const OdCharW* lpsz);

  /** Description:
      String concatenatation operator.

      Remarks:
      This OdWString is assigned the value of string concatenated onto this OdWString.

      Return Value:
      The modified OdWString.
   */
  const OdWString& operator+=(const OdWString& string);

  /** Description:
      String concatenatation operator.

      Remarks:
      This OdWString is assigned the value of ch concatenated onto this OdWString.

      Return Value:
      The modified OdWString.
   */
  const OdWString& operator+=(OdCharW ch);

  /** Description:
      String concatenatation operator.

      Remarks:
      This OdWString is assigned the value of lpsz concatenated onto this OdWString.

      Return Value:
      The modified OdWString.
   */
  const OdWString& operator+=(const OdCharW* lpsz);

  friend FIRSTDLL_EXPORT OdWString operator+(const OdWString& string1, const OdWString& string2);
  friend FIRSTDLL_EXPORT OdWString operator+(const OdWString& string, OdCharW ch);
  friend FIRSTDLL_EXPORT OdWString operator+(OdCharW ch, const OdWString& string);

#ifdef _UNICODE
  friend FIRSTDLL_EXPORT OdWString operator+(const OdWString& string, OdCharW ch);
  friend FIRSTDLL_EXPORT OdWString operator+(OdCharW ch, const OdWString& string);
#endif
  friend FIRSTDLL_EXPORT OdWString operator+(const OdWString& string, const OdCharW* lpsz);
  friend FIRSTDLL_EXPORT OdWString operator+(const OdCharW* lpsz, const OdWString& string);

  /** Case sensitive string comparison.  Returns < 0 if this OdWString is < lpsz, 
  0 if the 2 strings are identical (case sensitive), or > 0 if this 
  OdWString is > lpsz.
   */
  int compare(const OdCharW* lpsz) const;

  /** Case insensitive string comparison.  Returns < 0 if this OdWString is < lpsz, 
  0 if the 2 strings are identical (case insensitive), or > 0 if this 
  OdWString is > lpsz.
   */
  //int iCompare(const OdCharW* lpsz) const;

  /** Returns the nCount characters starting at index nFirst.
   */
  OdWString mid(int nFirst, int nCount) const;

  /** Returns all characters starting at index nFirst.
   */
  OdWString mid(int nFirst) const;

  /** Returns the first nCount characters in this OdWString.
   */
  OdWString left(int nCount) const;

  /** Returns the last nCount characters in this OdWString.
   */
  OdWString right(int nCount) const;

  /** Starting from the beginning of this OdWString, returns a substring made up of 
  the characters in this OdWString that are also contained in lpszCharSet.  The substring
  terminates when the first character in OdWString that is not also in lpszCharSet is
  encountered (this character is not appended to the substring).  An empty string
  is returned if the first character in this OdWString is not present in lpszCharSet.
   */
  OdWString spanIncluding(const OdCharW* lpszCharSet) const;

  /** Starting from the beginning of this OdWString, returns a substring made up of 
  the characters in this OdWString that NOT contained in lpszCharSet.  The substring
  terminates when the first character in OdWString that IS also in lpszCharSet is
  encountered (this character is not appended to the substring).  An empty string
  is returned if the first character in this OdWString is present in lpszCharSet.
   */
  OdWString spanExcluding(const OdCharW* lpszCharSet) const;
  
  /** Converts this string to upper case.
   */
  OdWString& makeUpper();

  /** Converts this string to lower case.
   */
  OdWString& makeLower();

  /** Reverses this string right to left.
   */
  OdWString& makeReverse();


  /** Removes all whitespace from the right side or end of this string.
   */
  OdWString& trimRight();

  /** Removes all whitespace from the left side or beginning of this string.
   */
  OdWString& trimLeft();

  /** Removes all consecutive occurrences of chTarget from the end of this string.
   */
  OdWString& trimRight(OdCharW chTarget);

  /** Removes all consecutive occurrences of characters that are present in 
  lpszTargets, from the end of this string.
   */
  OdWString& trimRight(const OdCharW* lpszTargets);

  /** Removes all consecutive occurrences of chTarget from the beginning of this string.
   */
  OdWString& trimLeft(OdCharW chTarget);

  /** Removes all consecutive occurrences of characters that are present in 
  lpszTargets, from the beginning of this string.
   */
  OdWString& trimLeft(const OdCharW* lpszTargets);


  /** Description:
      Replaces all occurrences of chOld in this OdWString, with chNew.

      Return Value:
      Number of characters replaced.
   */
  int replace(OdCharW chOld, OdCharW chNew);

  /** Description:
      Replaces all occurrences of the substring lpszOld in this OdWString, with lpszNew.

      Remarks:
      If lpszNew is the null string, all occurrences of lpszOld are deleted.

      Return Value:
      Number of substrings replaced.
   */
  int replace(const OdCharW* lpszOld, const OdCharW* lpszNew);

  /** Description:
      Removes all occurrences of chRemove from this OdWString.

      Return Value:
      Number of characters removed.
   */
  int remove(OdCharW chRemove);

  /** Description:
      Inserts character ch into this string at index nIndex.

      Remarks:
      If nIndex is beyond the current length, the string is lengthened to
      accommodate the insertion.

      Return Value:
      The new length of this string.
   */
  int insert(int nIndex, OdCharW ch);

  /** Description:
      Inserts string pstr into this string at index nIndex.

      Remarks:
      If nIndex is beyond the current length, the string is lengthened
      to accommodate the insertion.

      Return Value:
      The new length of this string.
   */
  int insert(int nIndex, const OdCharW* pstr);

  /** Description:
      Deletes nCount characters from this string, starting at index nIndex.

      Return Value:
      The new length of this string.
   */
  int deleteChars(int nIndex, int nCount = 1);


  /** Description:
      Finds the first occurrence of the specified character in this string, 
      starting from the beginning.

      Return Value:
      Index of first occurrence of ch in this string, otherwise -1 if ch is
      not found.
   */
  int find(OdCharW ch) const;

  /** Description:
      Finds the first occurrence of the specified character in this string, 
      starting from the end.

      Return Value:
      Index of last occurrence of ch in this string, otherwise -1 if ch is
      not found.
   */
  int reverseFind(OdCharW ch) const;

  /** Description:
      Finds the first occurrence of the specified character in this string, 
      starting from index nStart.

      Return Value:
      Index of first occurrence of ch on or after nStart, 
      otherwise -1 if ch is not found.
   */
  int find(OdCharW ch, int nStart) const;

  /** Description:
      Finds the first occurrence of the any of the characters specified in 
      lpszCharSet in this string,  starting from the beginning.

      Return Value:
      Index of first occurrence of one of the specified characters, 
      otherwise -1 if none of the characers is found.
   */
  int findOneOf(const OdCharW* lpszCharSet) const;

  /** Description:
      Finds the first occurrence of the string lpszSub in this OdWString.

      Return Value:
      The index of the first occurrence of lpszSub, otherwise
      -1 if not found.
   */
  int find(const OdCharW* lpszSub) const;

  /** Description:
      Finds the first occurrence of the string lpszSub in this OdWString,
      starting from index nStart.


      Return Value:
      The index of the first occurrence of lpszSub starting from
      nStart, otherwise -1 if not found.
   */
  int find(const OdCharW* lpszSub, int nStart) const;

  /** Assigns a value to this string using the printf-style format string and 
  arguments.
   */
  OdWString& format(const OdCharW* lpszFormat, ...);

  /** Creates a formatted string with a variable list of arguments in a fashion
   similar to vsprintf() . 
   */
  OdWString& formatV(const OdCharW* lpszFormat, va_list argList); /* va_list is not portable. */
                                                                
  /** Description:
      Returns a pointer to a modifiable C style character array, with minimum
      length of nMinBufLength.

      Remarks:
      releaseBuffer should be called when the pointer returned by getBuffer is no longer needed.
      The pointer returned by this function is not valid after releaseBuffer is called.
   */
  OdCharW* getBuffer(int nMinBufLength);

  /** Description:
      Releases a buffer obtained by getBuffer, setting the length to nNewLength.
   */
  void releaseBuffer(int nNewLength = -1);

  /** Description:
      Returns a pointer to a modifiable C style character array, with an exact
      length of nNewLength.

      Remarks:
      releaseBuffer should be called when the pointer returned by getBuffer is no longer needed.
      The pointer returned by this function is not valid after releaseBuffer is called.
   */
  OdCharW* getBufferSetLength(int nNewLength);

  /** Releases any extra memory allocated by this OdWString that is currently unused.
   */
  void freeExtra();

  /** Turns reference counting back on for this OdWString.
   */
  OdCharW* lockBuffer();

  /** Turns reference counting off for this OdWString.
   */
  void unlockBuffer();

/*   disable wide OdCharW functionalities
  *  @@@@@#######*******&&&&&&&
   Constructor.  This OdWString will receive a copy of the wide character string referenced
   by lpsz.
  OdWString(const OdCharW* lpsz);

  Constructor. This OdWString will receive a copy of the first nLength wide characters of
  the string referenced by lpch.
  OdWString(const OdCharW* lpch, int nLength);

 * ######%%%%%%%@@@@@@@@*******&&&&&&&&

   Assignment operator.  This OdWString is assigned the value of lpsz.
  const OdWString& operator=(const OdCharW* lpsz);

*/
#ifdef NOT_IMPLEMENTED
  /* Case sensitive string compare returning zero if the strings are identical, 
   < 0 if this CString object is less than lpsz, or >0 if this OdCharW* object is greater than lpsz.
   */
  int collate(const OdCharW* lpsz) const;

  int iCollate(const OdCharW* lpsz) const;

  OdWString&  format(unsigned int nFormatID, ...);
#endif


public:
  /** Destructor.
   */
  ~OdWString();

  /** Returns the number of bytes allocated for the character data in this OdWString.
   */
  int getAllocLength() const;

protected:
  static OdEmptyWStringData kEmptyData;

   /** pointer to ref counted string data 
   */
  OdCharW* m_pchData;   

  OdWStringData* getData() const;


  void init();


  void allocCopy(OdWString& dest, int nCopyLen, int nCopyIndex, int nExtraLen) const;


  void allocBuffer(int nLen);


  void assignCopy(int nSrcLen, const OdCharW* lpszSrcData);


  void concatCopy(int nSrc1Len, const OdCharW* lpszSrc1Data, int nSrc2Len, const OdCharW* lpszSrc2Data);


  void concatInPlace(int nSrcLen, const OdCharW* lpszSrcData);

 
  void copyBeforeWrite();


  void allocBeforeWrite(int nLen);


  void release();


  static void release(OdWStringData* pData);


  static int safeStrlen(const OdCharW* lpsz);


  static void freeData(OdWStringData* pData);
};

inline OdWStringData* OdWString::getData() const
  { ODA_ASSERT(m_pchData != NULL); return ((OdWStringData*)m_pchData)-1; }

inline OdWString::OdWString()
  { init(); }

#if defined(_UNICODE) && !defined(_WIN32_WCE)
inline const OdWString& OdWString::operator+=(OdCharW ch)
  { *this += (OdCharW)ch; return *this; }

inline const OdWString& OdWString::operator=(OdCharW ch)
  { *this = (OdCharW)ch; return *this; }

inline OdWString operator+(const OdWString& string, OdCharW ch)
  { return string + (OdCharW)ch; }

inline OdWString operator+(OdCharW ch, const OdWString& string)
  { return (OdCharW)ch + string; }
#endif

inline int OdWString::getLength() const
  { return getData()->nDataLength; }

inline int OdWString::getAllocLength() const
  { return getData()->nAllocLength; }

inline bool OdWString::isEmpty() const
  { return getData()->nDataLength == 0; }

inline OdWString::operator const OdCharW*() const { return m_pchData; }

inline const OdCharW* OdWString::c_str() const { return m_pchData; }

inline int OdWString::safeStrlen(const OdCharW* lpsz)
{
  if ( !lpsz ) return 0;
  const OdCharW* p = lpsz;
  while ( *p++ )
  {}
  return int(p - lpsz - 1);
}

inline OdCharW OdWString::getAt(int nIndex) const
{
  ODA_ASSERT(nIndex >= 0);
  ODA_ASSERT(nIndex < getData()->nDataLength);
  return m_pchData[nIndex];
}

inline OdCharW OdWString::operator[](int nIndex) const
{
  ODA_ASSERT(nIndex >= 0);
  ODA_ASSERT(nIndex < getData()->nDataLength);
  return m_pchData[nIndex];
}
inline bool operator==(const OdWString& s1, const OdWString& s2)
  { return s1.compare(s2) == 0; }

inline bool operator==(const OdWString& s1, const OdCharW* s2)
  { return s1.compare(s2) == 0; }

inline bool operator==(const OdCharW* s1, const OdWString& s2)
  { return s2.compare(s1) == 0; }

inline bool operator!=(const OdWString& s1, const OdWString& s2)
  { return s1.compare(s2) != 0; }

inline bool operator!=(const OdWString& s1, const OdCharW* s2)
  { return s1.compare(s2) != 0; }

inline bool operator!=(const OdCharW* s1, const OdWString& s2)
  { return s2.compare(s1) != 0; }

inline bool operator<(const OdWString& s1, const OdWString& s2)
  { return s1.compare(s2) < 0; }

inline bool operator<(const OdWString& s1, const OdCharW* s2)
  { return s1.compare(s2) < 0; }

inline bool operator<(const OdCharW* s1, const OdWString& s2)
  { return s2.compare(s1) > 0; }

inline bool operator>(const OdWString& s1, const OdWString& s2)
  { return s1.compare(s2) > 0; }

inline bool operator>(const OdWString& s1, const OdCharW* s2)
  { return s1.compare(s2) > 0; }

inline bool operator>(const OdCharW* s1, const OdWString& s2)
  { return s2.compare(s1) < 0; }

inline bool operator<=(const OdWString& s1, const OdWString& s2)
  { return s1.compare(s2) <= 0; }

inline bool operator<=(const OdWString& s1, const OdCharW* s2)
  { return s1.compare(s2) <= 0; }

inline bool operator<=(const OdCharW* s1, const OdWString& s2)
  { return s2.compare(s1) >= 0; }

inline bool operator>=(const OdWString& s1, const OdWString& s2)
  { return s1.compare(s2) >= 0; }

inline bool operator>=(const OdWString& s1, const OdCharW* s2)
  { return s1.compare(s2) >= 0; }

inline bool operator>=(const OdCharW* s1, const OdWString& s2)
  { return s2.compare(s1) <= 0; }

#include "DD_PackPop.h"

typedef OdArray<OdWString> OdWStringArray;

#endif //_OdWString_h_Included_
