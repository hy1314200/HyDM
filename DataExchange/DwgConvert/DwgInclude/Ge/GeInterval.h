///////////////////////////////////////////////////////////////////////////////
// Copyright © 2002, Open Design Alliance Inc. "Open Design") 
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
// DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////

#ifndef OD_GE_INTERVAL_H
#define OD_GE_INTERVAL_H /* {Secret} */
#include "DD_PackPush.h"
/**
    Description:
    This class represents a finite, infinite, or semi-infinite
    interval as the real axis.
  
    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeInterval
{
public:
  /**
    Arguments:
    lower (I) Lower *bound*.
    upper (I) Upper *bound*.
    tol (I) Boundary *tolerance*.
    boundedBelow (I) Determines if *bound* specifies a *lower* or an *upper* *bound*.
    bound (I) Specifies a *lower* *bound* if boundedBelow is true, or an *upper*
              *bound* if it is false.     

    Remarks:
    With no arguments other than tol, creates an unbounded interval.
  */
  OdGeInterval (
    double tol = 1.e-12)
    : m_Tol (tol)
    , m_UpperParam (0.0)
    , m_LowerParam (0.0)
    , m_bBoundedAbove (false)
    , m_bBoundedBelow (false)
    { }

  // OdGeInterval (const OdGeInterval& source);

  OdGeInterval (
    double lower,
    double upper,
    double tol = 1.e-12)
  : m_Tol (tol)
  , m_UpperParam (upper)
  , m_LowerParam (lower)
  , m_bBoundedAbove (true)
  , m_bBoundedBelow (true)
  { }
  
  OdGeInterval (
    bool boundedBelow,
    double bound,
    double tol = 1.e-12);

  ~OdGeInterval ()
  {  }  

  // OdGeInterval& operator = (const OdGeInterval& interval);

  /** 
    Description:
    Returns the *lower* *bound* of this interval.
    
    Remarks:
    This value is meaningful if and only if the interval has a *lower* *bound*.
  */
  double lowerBound () const { return m_LowerParam; }

  /** 
    Description:
    Returns the *upper* *bound* of this interval.
    
    Remarks:
    This value is meaningful if and only if the interval has an *upper* *bound*.
  */
  double upperBound () const { return m_UpperParam; }

  /**
    Description:
    Returns a point on this interval.
    
    Remarks:
    @table
    Bounded Above   Bounded Below          Returns
    Yes             ---                    Upper Bound
    No              Yes                    Lower Bound
    No              No                     0.0
  */  
  double element () const;

  /** 
    Description:
    Returns the bounds of this interval.
    
    Arguments:
    lower (O) Receives the Lower *bound*.
    upper (O) Receives the Upper *bound*.
  */
  void getBounds (
    double& lower,
    double& upper) const
  { lower = m_LowerParam; upper = m_UpperParam; }
    
  /** 
    Description:
    Returns the *length* of this interval.
    
    Remarks:
    Returns -1.0 if this interval is unbounded above or below.
  */
  double length () const
  { return (isBounded () ? (m_UpperParam - m_LowerParam) : -1.0); }  
  
  /**
    Description:
    Returns the boundary *tolerance* for this interval.
  */
  double tolerance () const { return m_Tol; }

  /**
    Description:
    Sets the parameters for this interval according to the arguments, and returns a reference to this interval.

    Arguments:
    lower (I) Lower *bound*.
    upper (I) Upper *bound*.
    boundedBelow (I) Determines if bound specifies a *lower* or an *upper* *bound*.
    bound (I) Specifies a *lower* *bound* if boundedBelow is true, or an *upper*
              *bound* if it is false.
              
    Remarks:
    If called with no parameters, unbounds this interval.     
  */
  OdGeInterval& set (
    double lower,
    double upper)
  { setLower (lower); return setUpper (upper); }
  
  OdGeInterval& set (
    bool boundedBelow,
    double bound);
    
  OdGeInterval& set ()
  { m_LowerParam = m_UpperParam = 0.0; m_bBoundedBelow = m_bBoundedAbove = false; return *this; }

  /**
    Description:
    Sets the *upper* *bound* for this interval, and returns a reference to this interval.

    Arguments:
    upper (I) Upper *bound*.
  */
  OdGeInterval& setUpper (
    double upper)
  { m_UpperParam = upper; m_bBoundedAbove = true; return *this; }
    
  /**
    Description:
    Sets the *lower* *bound* for this interval, and returns a reference to this interval.

    Arguments:
    *lower* (I) Lower *bound*.
  */
  OdGeInterval& setLower (
    double lower)
  { m_LowerParam = lower; m_bBoundedBelow = true; return *this; }    

  /**
    Description:
    Sets the boundary *tolerance* for this interval, and returns a reference to this interval.

    Arguments:
    tol (I) Boundary *tolerance*.
  */
  OdGeInterval& setTolerance (
    double tol)
  { m_Tol = tol; return *this; }


  /**
    Description:
    Returns the smallest interval containing both this interval and otherInterval.

    Arguments:
    otherInterval (I) The interval to be merged with this one.
    result (O) Receives the merged interval.
  */
  void getMerge (
    const OdGeInterval& otherInterval,
    OdGeInterval& result) const;
    
  /**
    Description:
    Returns the number intervals resulting from subtracting otherInterval from this one, and the intervals.

    Arguments:
    otherInterval (I) The interval to be subtracted from this one.
    lInterval (O) Receives the Left (or only) interval.
    rInterval (O) Receives the right interval.
    
    Remarks:
    @table
    return value   Results
    0              Empty result
    1              Single interval in lInterval
    2              Left in lInterval, Right in rInterval.
  */
  int subtract (
    const OdGeInterval& otherInterval,
    OdGeInterval& lInterval,
    OdGeInterval& rInterval) const;

  /**
    Description:
    Returns true if and only otherInterval insersects with this one, and the 
    interval of intersection.

    Arguments:
    otherInterval (I) The interval to be intersected.
    result (O) Receives the intersected interval.
  */
  bool intersectWith (
    const OdGeInterval& otherInterval,
    OdGeInterval& result) const;
    
  /**
    Description:
    Returns true if and only if this interval is bounded above and below.
  */
  bool isBounded () const
  { return (m_bBoundedAbove && m_bBoundedBelow); }
    
  /**
    Description:
    Returns true if and only if this interval is bounded above.
  */
  bool isBoundedAbove () const { return m_bBoundedAbove; }
    
  /**
    Description:
    Returns true if and only if this interval is bounded below.
  */
  bool isBoundedBelow () const { return m_bBoundedBelow; }

  /**
    Description:
    Returns true if and only if this interval is unbounded above or below.
  */
  bool isUnBounded () const
  { return (!m_bBoundedAbove && !m_bBoundedBelow); }
  
  /**
    Description:
    Returns true if and only if this interval is bounded, and the *upper* and *lower* bound are equal within tol.
  */
  bool isSingleton () const;

  /**
    Description:
    Returns true if and only if this interval does not intersect otherInterval within tol.
    
    Arguments:
    otherInterval (I) The interval to be tested.
  */
  bool isDisjoint (
    const OdGeInterval& otherInterval) const;

  /**
    Description:
    Returns true if and only if this interval *contains* value or otherInterval within tol.
    
    Arguments:
    otherInterval (I) The interval to be tested.
    value (I) The *value* to be tested.
  */
  bool contains (
    const OdGeInterval& otherInterval) const;
    
  bool contains (
    double value) const
  {
    return ( (!m_bBoundedBelow || m_LowerParam - m_Tol < value)
      && (!m_bBoundedAbove || m_UpperParam + m_Tol > value));
  }

  /**
    Description:
    Returns true if and only if this interval is bounded above, otherInterval is bounded below, 
    and the *upper* bound of this interval is equal to the *lower* bound of otherInterval within tol.
    
    Arguments:
    otherInterval (I) The interval to be tested.
  */
  bool isContinuousAtUpper (
    const OdGeInterval& otherInterval) const;

  /**
    Description:
    Returns true if and only if this interval is bounded above, otherInterval is bounded below, 
    neither is singleton, they intersect, otherInterval does not contain this one, and the *upper* bound of this interval is 
    contained in otherInterval, and the *lower* bound of otherInterval is contained within this one. If all these conditions are met,
    also returns the intersection of the intervals.
    
    Arguments:
    otherInterval (I) The interval to be tested.
    overlap (O) Receives the *overlap* of the intervals.
  */
  bool isOverlapAtUpper (
    const OdGeInterval& otherInterval,
    OdGeInterval& overlap) const;

  bool operator == (
    const OdGeInterval& otherInterval) const;
  bool operator != (
    const OdGeInterval& otherInterval) const;
    
  /**
    Description:
    Returns true if and only if
    1. Both this interval and otherInterval are unbounded above or bounded above with their *upper* bounds equal within tol.
    2. This interval is bounded above, and the *upper* bound is equal to value within tol. 

    Arguments:
    otherInterval (I) The interval to be tested.
    value (I) The *value* to be tested.
  */
  bool isEqualAtUpper (
    const OdGeInterval& otherInterval) const;
  bool isEqualAtUpper (
    double value) const;
    
  /**
    Description:
    Returns true if and only if
    1. Both this interval and otherInterval are unbounded below or bounded below with their *lower* bounds equal within tol.
    2. This interval is bounded below, and the *lower* bound is equal to value within tol. 

    Arguments:
    otherInterval (I) The interval to be tested.
    value (I) The *value* to be tested.
  */
  bool isEqualAtLower (
    const OdGeInterval& otherInterval) const;
  bool isEqualAtLower (
    double value) const;

  /**
    Description:
    Returns true if and only if there is a positive integer N such that
    
            value + N * period
            
    is on this interval, and returns that *value*.         
    
    Arguments:
    period (I) Period.
    value (I/O) Value.
  */
  bool isPeriodicallyOn (
    double period,
    double& value);

  friend 
    bool operator > (
    double value,
    const OdGeInterval& interval);
  bool operator > (
    double value) const;
  bool operator > (
    const OdGeInterval& otherInterval) const;
  friend 
    bool operator >= (
    double value,
    const OdGeInterval& interval);
  bool operator >= (
    double value) const;
  bool operator >= (
    const OdGeInterval& otherInterval) const;
  friend 
    bool operator < (
    double value,
    const OdGeInterval& interval);
  bool operator < (
    double value) const;
  bool operator < (
    const OdGeInterval& otherInterval) const;
  friend 
    bool operator <= (
    double value,
    const OdGeInterval& interval);
  bool operator <= (
    double value) const;
  bool operator <= (
    const OdGeInterval& otherInterval) const;

private:
  double m_LowerParam;
  double m_UpperParam;
  double m_Tol;
  bool m_bBoundedAbove;
  bool m_bBoundedBelow;
};

#include "DD_PackPop.h"
#endif // OD_GE_INTERVAL_H


