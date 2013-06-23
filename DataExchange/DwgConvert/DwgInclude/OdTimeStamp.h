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



#ifndef _ODTIME_INCLUDED_
#define _ODTIME_INCLUDED_

#include "DD_PackPush.h"

/** Description:
    This class represents TimeStamp objects in an OdDbDatabase instance.

    Library:
    Db

    {group:Other_Classes}
*/
class FIRSTDLL_EXPORT OdTimeStamp
{
public:
  enum InitialValue
  {
    kInitZero          = 1,  // Midnight, January 1, 1970, UTC.
    kInitLocalTime     = 2,  // Workstation date in local time.
    kInitUniversalTime = 3   // Workstation date in Universal (Greenwich Mean) Time.
  };

  /**
    Arguments:
    init (I) Initial value.
    
    Remarks:
    The default constructor initializes the Julian date and time to midnight, January 1, 1970,  UTC.
    
    init will be one of the following:
    
    @table
    Name                 Value    Description
    kInitZero            1        Midnight, January 1, 1970, UTC.
    kInitLocalTime       2        Workstation date in local time.
    kInitUniversalTime   3        Workstation date in Universal (Greenwich Mean) Time.
  */ 
  OdTimeStamp();
  OdTimeStamp(
    InitialValue init);

	/** Description:
	  Returns the date of this TimeStamp object.
	  Arguments:
	  month (O) Receives the *month*.
	  day (O) Receives the *day*.
	  year (O) Receives the *year*.
  */
  void getDate(
    short& month, 
    short& day, 
    short& year) const;

	/** Description:
	  Sets the date of this TimeStamp object.
	  Arguments:
	  month (I) Month.
	  day (I) Day.
	  year (I) Year.
  */
  void setDate(
    short month, 
    short day, 
    short year);

	/** Description:
	  Returns the *month* of this TimeStamp object.
  */
  short month() const;

	/** Description:
	  Sets the *month* of this TimeStamp object.
	  Arguments:
	  month (I) Month.
  */
  void setMonth(
    short month);

	/** Description:
	  Returns the *day* of this TimeStamp object.
  */
  short day() const;

	/** Description:
	  Sets the *day* of this TimeStamp object.
	  Arguments:
	  day (I) Day.
  */
  void setDay(
    short day);

	/** Description:
	  Returns the *year* of this TimeStamp object.
  */
  short year() const;

	/** Description:
	  Sets the *year* of this TimeStamp object.
	  Arguments:
	  year (I) Year.
  */
  void setYear(
    short year);

	/** Description:
	  Returns the time of this TimeStamp object.
	  Arguments:
	  hour (O) Receives the *hour*.
	  minute (O) Receives the *minute*.
	  second (O) Receives the *second*.
	  millisecond (O) Receives the *millisecond*.
  */
  void getTime(
    short& hour, 
    short& minute, 
    short& second, 
    short& millisecond) const;

	/** Description:
	  Sets the time of this TimeStamp object.
	  Arguments:
	  hour (I) Hour.
	  minute (I) Minute.
	  second (I) Second.
	  millisecond (I) Millisecond.
  */
  void setTime(
    short hour, 
    short minute, 
    short second, 
    short millisecond);

	/** Description:
	  Returns the *hour* of this TimeStamp object.
  */
  short hour() const;

	/** Description:
	  Sets the *hour* of this TimeStamp object.
	  Arguments:
	  hour (I) Hour.
  */
  void setHour(
    short hour);

	/** Description:
	  Returns the *minute* of this TimeStamp object.
  */
  short minute() const;

	/** Description:
	  Sets the *minute* of this TimeStamp object.
	  Arguments:
	  minute (I) Minute.
  */
  void setMinute(
    short minute);

	/** Description:
	  Returns the *second* of this TimeStamp object.
  */
  short second() const;

	/** Description:
	  Sets the *second* of this TimeStamp object.
	  Arguments:
	  second (I) Second.
  */
  void setSecond(short second);

	/** Description:
	  Returns the *millisecond* of this TimeStamp object.
  */
  short millisecond() const;

	/** Description:
	  Sets the *millisecond* of this TimeStamp object.
	  Arguments:
	  millisecond (I) Millisecond.
  */
  void setMillisecond(
    short millisecond);

	/** Description:
	  Sets this TimeStamp object to Midnight, January 1, 1970, UTC.
  */
  void setToZero();

  /** Description:
	  Sets this TimeStamp object to the workstation date in local time.
  */
  void getLocalTime();

  /** Description:
	  Sets this TimeStamp object to the workstation date in Universal (Greenwich Mean) Time.
	*/
  void getUniversalTime();

	/** Description:
	  Converts this TimeStamp object from local time to Universal (Greenwich Mean) Time.
  */
  void localToUniversal();

	/** Description:
	  Converts this TimeStamp object from Universal (Greenwich Mean) Time to local time.
  */
  void universalToLocal();

	/** Description:
	  Returns the Julian *day* of this TimeStamp object.
  */
  OdUInt32 julianDay() const;

	/** Description:
	  Sets the Julian *day* of this TimeStamp object.
	  
	  Arguments:
	  julianDay (I) Julian *day*.
  */
  void setJulianDay(
    OdUInt32 julianDay);

	/** Description:
	  Returns the milliseconds past midnight for this TimeStamp object.
  */
  OdUInt32 msecsPastMidnight() const;

	/** Description:
	  Sets the milliseconds past midnight for this TimeStamp object.
	  
	  Arguments:
	  msecsPastMidnight (I) Milliseconds past midnight.
  */
  void setMsecsPastMidnight(
    OdUInt32 msecsPastMidnight);

	/** Description: 
	  Sets the Julian Date for this TimeStamp object.

	  Arguments:
	  julianDay (I) Julian *day*.
	  msecsPastMidnight (I) Milliseconds past midnight.
  */
  void setJulianDate(
    OdUInt32 julianDay, 
    OdUInt32 msecsPastMidnight);

	/** Description: 
	  Returns the fraction of the Julian date of this TimeStamp object.

	  Remarks:
	  The Julian fraction is the fraction of the *day* since midnight.
  */
  double julianFraction() const;

	/** Description: 
	  Sets the fraction of the Julian date of this TimeStamp object.
	  Arguments:
	  julianFraction (I) Julian fraction.
	  Remarks:
	  The Julian fraction is the fraction of the *day* since midnight.
  */
  void setJulianFraction(
    double julianFraction);

  bool operator==(
    const OdTimeStamp& tStamp) const;

  bool operator!=(
    const OdTimeStamp& tStamp) const
  {
    return !(operator==(tStamp));
  }

  bool operator>(
    const OdTimeStamp& tStamp) const;

  bool operator>=(
    const OdTimeStamp& tStamp) const
  {
    return ! operator<(tStamp);
  }

  bool operator<(
    const OdTimeStamp& tStamp) const;

  bool operator<=(
    const OdTimeStamp& tStamp) const
  {
    return ! operator>(tStamp);
  }

  const OdTimeStamp operator+(
    const OdTimeStamp &tStamp) const
  {
    return OdTimeStamp(*this) += tStamp;
  }

  const OdTimeStamp operator-(
    const OdTimeStamp &tStamp) const
  {
    return OdTimeStamp(*this) -= tStamp;
  }

  const OdTimeStamp& operator+=(
    const OdTimeStamp &tStamp);

  const OdTimeStamp& operator-=(
    const OdTimeStamp &tStamp);

  /** Description:
      Adds the specified TimeStamp object to this TimeStamp object, and returns the sum.
      
      Arguments:
      tStamp (I) TimeStamp.
  */
  const OdTimeStamp& add(
    const OdTimeStamp &tStamp)
  {
    return operator+=(tStamp);
  }

  /** Description:
      Subtracts the specified TimeStamp object from this TimeStamp object, and returns the difference.
      
      Arguments:
      tStamp (I) TimeStamp.
  */
  const OdTimeStamp& subtract(
    const OdTimeStamp &tStamp)
  {
    return operator-=(tStamp);
  }

  /** Description:
    Returns this TimeStamp object as standard formatted *string*.
    
    Arguments:
    string (O) Receives the formatted time *string*.
    
    Remarks:
    The returned *string* is always 24 characters in length, and is in the form
    
                  Tue Oct 11 08:06:22 2005
  */
  void ctime(
    OdString& timeString) const;

  /** Description:
    Returns this TimeStamp object as a user-formatted *string*.
    
    Arguments:
    string (O) Receives the formatted time *string*.
    format (I) Format string.
    
    Remarks:
    The formatting codes for the *format* *string* are as follows; the # suppresses leading zeros:

    @table
    Code          Description
    %a            Short weekday 
    %A            Full weekday 
    %b            Short *month* 
    %B            Full *month* 
    %c            Short date and time for current locale
    %#c           Long date and time for current locale 
    %d            Day of *month* (01 - 31) 
    %#d           Day of *month* (1 - 31) 
    %H            Hour in 24 *hour* *format* (00 - 23) 
    %#H           Hour in 24 *hour* *format* (0 - 23) 
    %I            Hour in 12 *hour* *format* (01 - 12) 
    %#I           Hour in 12 *hour* *format* (1 - 12) 
    %j            Day of *year* (001 - 366) 
    %#j           Day of *year* (1 - 366) 
    %m            Month (01 - 12) 
    %#m           Month (1 - 12) 
    %M            Minute (00 - 59) 
    %#M           Minute (0 - 59) 
    %p            AM/PM indicator for current local
    %S            Second (00 - 59) 
    %#S           Second (0 - 59) 
    %U            Week of the *year*, first *day* of week is Sunday (00 - 53) 
    %#U           Week of the *year*, first *day* of week is Sunday (0 - 53) 
    %w            Weekday, Sunday is 0, (0 - 6) 
    %#w           Weekday, Sunday is 0, (0 - 6) 
    %W            Week of the *year*, first *day* of week is Monday (00 - 53) 
    %#W           Week of the *year*, first *day* of week is Monday (0 - 53) 
    %x            Date for the current locale 
    %X            Time for the current locale 
    %y            Year without century (00 - 99) 
    %#y           Year without century (0 - 99) 
    %Y            Year with century  
    %Y            Year with century, no leading zeros 
    %z            Time-zone name  
    %Z            Time zone abbreviation
  */
  void strftime(
    const char *format, 
    OdString &strfTime) const;

  /** Description:
    Returns this TimeStamp object as long.
    
    Remarks:
    packedValue returns the number of seconds elapsed since midnight, January 1, 1970, Universal (Greenwich Mean) Time.
    
    If this TimeStamp object contains a date before midnight, January 1, 1970,  UTC, 
    or after 3:14:07 on January 19, 2038, UT, packedValue will return –1.
  */
  long packedValue() const;
private:
  OdUInt32 m_julianDay;
  OdUInt32 m_msec;
};
#include "DD_PackPop.h"

#endif // _ODTIME_INCLUDED_


