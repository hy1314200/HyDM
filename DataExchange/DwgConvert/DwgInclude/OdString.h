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
// programs incorporating this software must include the following statement 
// with their copyright notices:
//
//      DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////




#ifndef __ODSTRING_H__
#define __ODSTRING_H__

class OdWString;

#define ODA_CDECL 
#define ODA_PASCAL 

#include <stdarg.h>
#include <stdio.h>

#include "OdPlatform.h"

#define odaIsValidString(lpch)    (true)

#include "DD_PackPush.h"

#ifdef sgi
#include <string>
using namespace std;  // va_XXXX items are in std namespace for newest SGI compiler
#endif

/** Description:
    Raw data used by OdString.

    {group:Structs}
*/
struct OdStringData
{            
   /**  reference count */
  long nRefs;      
   /** length of data (including terminator) */
  int nDataLength;  
   /** length of allocation */
  int nAllocLength;       

   /** OdChar* to managed data
   */
  OdChar* data()           
    { return (OdChar*)(this+1); }
};

/** Description:
  {group:Structs}
*/      
struct OdEmptyStringData
{
  OdStringData  kStrData;
  OdChar        kChar;
};

/** Description:
    Represents a DWGdirect string.

    Remarks:
    OdString objects use zero-based indices to access string elements.

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdString
{
public:
  /** Constructor. */
  OdString();

  /** Copy Constructor.
   */
  OdString(const OdString& stringSrc);

  /** Constructor. Initialize this string to a sequence of nRepeat instances of the 
  character ch.
   */
  OdString(OdChar ch, int nRepeat);

  /** Constructor.  This OdString will receive a copy of the character string referenced
  by lpsz.
   */
  OdString(const char* lpsz);

  /** Constructor. This OdString will receive a copy of the first nLength characters of
  the string referenced by lpch.
   */
  OdString(const char* lpch, int nLength);

  /** Constructor.  This OdString will receive a copy of the character string referenced
  by lpsz.
   */
  OdString(const unsigned char* psz);
  /** Constructor. Converts wide char string to multibyte.
   */
  OdString( const OdWString& );

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
  OdChar getAt(int nIndex) const;

  /** Returns the single character at index nIndex of the string.  No range checking is done.
   */
  OdChar operator[](int nIndex) const;

  /** Sets the single character at index nIndex of the string.  No range checking is done.
   */
  void setAt(int nIndex, OdChar ch);

  /** Returns a const pointer to the underlying char array referenced by this OdString.
   */
  const OdChar* c_str() const;

  /** Conversion operator returns a const pointer to the underlying char array 
  referenced by this OdString.
   */
  operator const OdChar*() const;

  /** Assignment operator.  This OdString is assigned the value of stringSrc.
   */
  const OdString& operator=(const OdString& stringSrc);

  /** Assignment operator.  This OdString is assigned the value of a single character, ch.
   */
  const OdString& operator=(OdChar ch);
  
  /** Assignment operator.  This OdString is assigned the value of lpsz.
   */
  const OdString& operator=(const char* lpsz);

  /** Assignment operator.  This OdString is assigned the value of psz.
   */
  const OdString& operator=(const unsigned char* psz);

  /** Description:
      String concatenatation operator.

      Remarks:
      This OdString is assigned the value of string concatenated onto this OdString.

      Return Value:
      The modified OdString.
   */
  const OdString& operator+=(const OdString& string);

  /** Description:
      String concatenatation operator.

      Remarks:
      This OdString is assigned the value of ch concatenated onto this OdString.

      Return Value:
      The modified OdString.
   */
  const OdString& operator+=(OdChar ch);

  /** Description:
      String concatenatation operator.

      Remarks:
      This OdString is assigned the value of lpsz concatenated onto this OdString.

      Return Value:
      The modified OdString.
   */
  const OdString& operator+=(const OdChar* lpsz);

  friend FIRSTDLL_EXPORT OdString operator+(const OdString& string1, const OdString& string2);
  friend FIRSTDLL_EXPORT OdString operator+(const OdString& string, OdChar ch);
  friend FIRSTDLL_EXPORT OdString operator+(OdChar ch, const OdString& string);

#ifdef _UNICODE
  friend FIRSTDLL_EXPORT OdString operator+(const OdString& string, char ch);
  friend FIRSTDLL_EXPORT OdString operator+(char ch, const OdString& string);
#endif
  friend FIRSTDLL_EXPORT OdString operator+(const OdString& string, const OdChar* lpsz);
  friend FIRSTDLL_EXPORT OdString operator+(const OdChar* lpsz, const OdString& string);

  /** Case sensitive string comparison.  Returns < 0 if this OdString is < lpsz, 
  0 if the 2 strings are identical (case sensitive), or > 0 if this 
  OdString is > lpsz.
   */
  int compare(const OdChar* lpsz) const;

  /** Case insensitive string comparison.  Returns < 0 if this OdString is < lpsz, 
  0 if the 2 strings are identical (case insensitive), or > 0 if this 
  OdString is > lpsz.
   */
  int iCompare(const OdChar* lpsz) const;

  /** Returns the nCount characters starting at index nFirst.
   */
  OdString mid(int nFirst, int nCount) const;

  /** Returns all characters starting at index nFirst.
   */
  OdString mid(int nFirst) const;

  /** Returns the first nCount characters in this OdString.
   */
  OdString left(int nCount) const;

  /** Returns the last nCount characters in this OdString.
   */
  OdString right(int nCount) const;

  /** Starting from the beginning of this OdString, returns a substring made up of 
  the characters in this OdString that are also contained in lpszCharSet.  The substring
  terminates when the first character in OdString that is not also in lpszCharSet is
  encountered (this character is not appended to the substring).  An empty string
  is returned if the first character in this OdString is not present in lpszCharSet.
   */
  OdString spanIncluding(const OdChar* lpszCharSet) const;

  /** Starting from the beginning of this OdString, returns a substring made up of 
  the characters in this OdString that NOT contained in lpszCharSet.  The substring
  terminates when the first character in OdString that IS also in lpszCharSet is
  encountered (this character is not appended to the substring).  An empty string
  is returned if the first character in this OdString is present in lpszCharSet.
   */
  OdString spanExcluding(const OdChar* lpszCharSet) const;
  
  /** Converts this string to upper case.
   */
  OdString& makeUpper();

  /** Converts this string to lower case.
   */
  OdString& makeLower();

  /** Reverses this string right to left.
   */
  OdString& makeReverse();


  /** Removes all whitespace from the right side or end of this string.
   */
  OdString& trimRight();

  /** Removes all whitespace from the left side or beginning of this string.
   */
  OdString& trimLeft();

  /** Removes all consecutive occurrences of chTarget from the end of this string.
   */
  OdString& trimRight(OdChar chTarget);

  /** Removes all consecutive occurrences of characters that are present in 
  lpszTargets, from the end of this string.
   */
  OdString& trimRight(const OdChar* lpszTargets);

  /** Removes all consecutive occurrences of chTarget from the beginning of this string.
   */
  OdString& trimLeft(OdChar chTarget);

  /** Removes all consecutive occurrences of characters that are present in 
  lpszTargets, from the beginning of this string.
   */
  OdString& trimLeft(const OdChar* lpszTargets);


  /** Description:
      Replaces all occurrences of chOld in this OdString, with chNew.

      Return Value:
      Number of characters replaced.
   */
  int replace(OdChar chOld, OdChar chNew);

  /** Description:
      Replaces all occurrences of the substring lpszOld in this OdString, with lpszNew.

      Remarks:
      If lpszNew is the null string, all occurrences of lpszOld are deleted.

      Return Value:
      Number of substrings replaced.
   */
  int replace(const OdChar* lpszOld, const OdChar* lpszNew);

  /** Description:
      Removes all occurrences of chRemove from this OdString.

      Return Value:
      Number of characters removed.
   */
  int remove(OdChar chRemove);

  /** Description:
      Inserts character ch into this string at index nIndex.

      Remarks:
      If nIndex is beyond the current length, the string is lengthened to
      accommodate the insertion.

      Return Value:
      The new length of this string.
   */
  int insert(int nIndex, OdChar ch);

  /** Description:
      Inserts string pstr into this string at index nIndex.

      Remarks:
      If nIndex is beyond the current length, the string is lengthened
      to accommodate the insertion.

      Return Value:
      The new length of this string.
   */
  int insert(int nIndex, const OdChar* pstr);

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
  int find(OdChar ch) const;

  /** Description:
      Finds the first occurrence of the specified character in this string, 
      starting from the end.

      Return Value:
      Index of last occurrence of ch in this string, otherwise -1 if ch is
      not found.
   */
  int reverseFind(OdChar ch) const;

  /** Description:
      Finds the first occurrence of the specified character in this string, 
      starting from index nStart.

      Return Value:
      Index of first occurrence of ch on or after nStart, 
      otherwise -1 if ch is not found.
   */
  int find(OdChar ch, int nStart) const;

  /** Description:
      Finds the first occurrence of the any of the characters specified in 
      lpszCharSet in this string,  starting from the beginning.

      Return Value:
      Index of first occurrence of one of the specified characters, 
      otherwise -1 if none of the characers is found.
   */
  int findOneOf(const OdChar* lpszCharSet) const;

  /** Description:
      Finds the first occurrence of the string lpszSub in this OdString.

      Return Value:
      The index of the first occurrence of lpszSub, otherwise
      -1 if not found.
   */
  int find(const OdChar* lpszSub) const;

  /** Description:
      Finds the first occurrence of the string lpszSub in this OdString,
      starting from index nStart.


      Return Value:
      The index of the first occurrence of lpszSub starting from
      nStart, otherwise -1 if not found.
   */
  int find(const OdChar* lpszSub, int nStart) const;

  /** Assigns a value to this string using the printf-style format string and 
  arguments.
   */
  OdString& ODA_CDECL format(const OdChar* lpszFormat, ...);

  /** Creates a formatted string with a variable list of arguments in a fashion
   similar to vsprintf() . 
   */
  OdString& formatV(const OdChar* lpszFormat, va_list argList); /* va_list is not portable. */
                                                                
#ifndef _UNICODE
  /** Converts all the characters in this OdString object from the ANSI 
   character set to the OEM character set. 
   */
  void ansiToOem();
  /** Converts all the characters in this OdString object from the OEM 
   character set to the ANSI character set.
   */
  void oemToAnsi();
#endif

  /** Description:
      Returns a pointer to a modifiable C style character array, with minimum
      length of nMinBufLength.

      Remarks:
      releaseBuffer should be called when the pointer returned by getBuffer is no longer needed.
      The pointer returned by this function is not valid after releaseBuffer is called.
   */
  OdChar* getBuffer(int nMinBufLength);

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
  OdChar* getBufferSetLength(int nNewLength);

  /** Releases any extra memory allocated by this OdString that is currently unused.
   */
  void freeExtra();

  /** Turns reference counting back on for this OdString.
   */
  OdChar* lockBuffer();

  /** Turns reference counting off for this OdString.
   */
  void unlockBuffer();

/*   disable wide char functionalities
  *  @@@@@#######*******&&&&&&&
   Constructor.  This OdString will receive a copy of the wide character string referenced
   by lpsz.
  OdString(const OdCharW* lpsz);

  Constructor. This OdString will receive a copy of the first nLength wide characters of
  the string referenced by lpch.
  OdString(const OdCharW* lpch, int nLength);

 * ######%%%%%%%@@@@@@@@*******&&&&&&&&

   Assignment operator.  This OdString is assigned the value of lpsz.
  const OdString& operator=(const OdCharW* lpsz);

*/
#ifdef NOT_IMPLEMENTED
  /* Case sensitive string compare returning zero if the strings are identical, 
   < 0 if this CString object is less than lpsz, or >0 if this OdChar* object is greater than lpsz.
   */
  int collate(const OdChar* lpsz) const;

  int iCollate(const OdChar* lpsz) const;

  OdString& ODA_CDECL format(unsigned int nFormatID, ...);
#endif


public:
  /** Destructor.
   */
  ~OdString();

  /** Returns the number of bytes allocated for the character data in this OdString.
   */
  int getAllocLength() const;

protected:
  static OdEmptyStringData kEmptyData;

   /** pointer to ref counted string data 
   */
  OdChar* m_pchData;   

  OdStringData* getData() const;


  void init();


  void allocCopy(OdString& dest, int nCopyLen, int nCopyIndex, int nExtraLen) const;


  void allocBuffer(int nLen);


  void assignCopy(int nSrcLen, const OdChar* lpszSrcData);


  void concatCopy(int nSrc1Len, const OdChar* lpszSrc1Data, int nSrc2Len, const OdChar* lpszSrc2Data);


  void concatInPlace(int nSrcLen, const OdChar* lpszSrcData);

 
  void copyBeforeWrite();


  void allocBeforeWrite(int nLen);


  void release();


  static void ODA_PASCAL release(OdStringData* pData);


  static int ODA_PASCAL safeStrlen(const OdChar* lpsz);


  static void freeData(OdStringData* pData);
};

inline OdStringData* OdString::getData() const
  { ODA_ASSERT(m_pchData != NULL); return ((OdStringData*)m_pchData)-1; }

inline OdString::OdString()
  { init(); }

inline OdString::OdString(const unsigned char* lpsz)
  { init(); *this = (const char*)lpsz; }

inline const OdString& OdString::operator=(const unsigned char* lpsz)
  { *this = (const char*)lpsz; return *this; }

#if defined(_UNICODE) && !defined(_WIN32_WCE)
inline const OdString& OdString::operator+=(char ch)
  { *this += (OdChar)ch; return *this; }

inline const OdString& OdString::operator=(char ch)
  { *this = (OdChar)ch; return *this; }

inline OdString operator+(const OdString& string, char ch)
  { return string + (OdChar)ch; }

inline OdString operator+(char ch, const OdString& string)
  { return (OdChar)ch + string; }
#endif

inline int OdString::getLength() const
  { return getData()->nDataLength; }

inline int OdString::getAllocLength() const
  { return getData()->nAllocLength; }

inline bool OdString::isEmpty() const
  { return getData()->nDataLength == 0; }

inline OdString::operator const OdChar*() const { return m_pchData; }

inline const OdChar* OdString::c_str() const { return m_pchData; }

inline int ODA_PASCAL OdString::safeStrlen(const OdChar* lpsz)
  { return (lpsz == NULL) ? (int)0 : (int)odStrLen(lpsz); }

inline int OdString::compare(const OdChar* lpsz) const
  { ODA_ASSERT(odaIsValidString(lpsz)); return odStrCmp(m_pchData, lpsz); }    /* MBCS/Unicode aware */

inline int OdString::iCompare(const OdChar* lpsz) const
  { ODA_ASSERT(odaIsValidString(lpsz)); return odStrICmp(m_pchData, lpsz); }   

inline OdChar OdString::getAt(int nIndex) const
{
  ODA_ASSERT(nIndex >= 0);
  ODA_ASSERT(nIndex < getData()->nDataLength);
  return m_pchData[nIndex];
}

inline OdChar OdString::operator[](int nIndex) const
{
  ODA_ASSERT(nIndex >= 0);
  ODA_ASSERT(nIndex < getData()->nDataLength);
  return m_pchData[nIndex];
}
inline bool operator==(const OdString& s1, const OdString& s2)
  { return s1.compare(s2) == 0; }

inline bool operator==(const OdString& s1, const OdChar* s2)
  { return s1.compare(s2) == 0; }

inline bool operator==(const OdChar* s1, const OdString& s2)
  { return s2.compare(s1) == 0; }

inline bool operator!=(const OdString& s1, const OdString& s2)
  { return s1.compare(s2) != 0; }

inline bool operator!=(const OdString& s1, const OdChar* s2)
  { return s1.compare(s2) != 0; }

inline bool operator!=(const OdChar* s1, const OdString& s2)
  { return s2.compare(s1) != 0; }

inline bool operator<(const OdString& s1, const OdString& s2)
  { return s1.compare(s2) < 0; }

inline bool operator<(const OdString& s1, const OdChar* s2)
  { return s1.compare(s2) < 0; }

inline bool operator<(const OdChar* s1, const OdString& s2)
  { return s2.compare(s1) > 0; }

inline bool operator>(const OdString& s1, const OdString& s2)
  { return s1.compare(s2) > 0; }

inline bool operator>(const OdString& s1, const OdChar* s2)
  { return s1.compare(s2) > 0; }

inline bool operator>(const OdChar* s1, const OdString& s2)
  { return s2.compare(s1) < 0; }

inline bool operator<=(const OdString& s1, const OdString& s2)
  { return s1.compare(s2) <= 0; }

inline bool operator<=(const OdString& s1, const OdChar* s2)
  { return s1.compare(s2) <= 0; }

inline bool operator<=(const OdChar* s1, const OdString& s2)
  { return s2.compare(s1) >= 0; }

inline bool operator>=(const OdString& s1, const OdString& s2)
  { return s1.compare(s2) >= 0; }

inline bool operator>=(const OdString& s1, const OdChar* s2)
  { return s1.compare(s2) >= 0; }

inline bool operator>=(const OdChar* s1, const OdString& s2)
  { return s2.compare(s1) <= 0; }

#include "DD_PackPop.h"

#endif // __ODSTRING_H__



