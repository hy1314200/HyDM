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
// programs incorporating this software must include the following statment 
// with their copyright notices:
//
// DWGdirect © 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////



#ifndef OD_GE_KNOT_VECTOR
#define OD_GE_KNOT_VECTOR /* {Secret} */

#include "GeDoubleArray.h"
#include "GeIntArray.h"

class OdGeInterval;

#include "DD_PackPush.h"

/**
    Description:
    This class represents an ordered series of monotonically increasing doubles used by spline entities.
    
 
    Library: Ge

    {group:OdGe_Classes} 
*/
class GE_TOOLKIT_EXPORT OdGeKnotVector
{
public:
  /** Arguments:
    tol (I) Knot equality *tolerance*.
    size (I) Length of vector.
    plusMult (I) Multiplicity increment for each *knot*.
    source (I) Object to be cloned.

  */
  OdGeKnotVector (
    double tol = 1.e-9) : m_Tolerance (tol) {}

  // OdGeKnotVector (int size, int growSize, double tol = 1.e-9);
  
  OdGeKnotVector (
    int size, 
    const double source[], 
    double tol = 1.e-9)
    : m_Tolerance (tol)
  {
    m_Data.resize (size);
    for (int i = 0; i < size; i++)
    {
      m_Data[i] = source[i];
    }
  }
    
  OdGeKnotVector (
    int plusMult, 
    const OdGeKnotVector& source);
    
  OdGeKnotVector (
    const OdGeKnotVector& source)
    : m_Data (source.m_Data)
    , m_Tolerance (source.m_Tolerance)
  {}
    
  OdGeKnotVector (
    const OdGeDoubleArray& source, 
    double tol = 1.e-9)
      : m_Data (source)
      , m_Tolerance (tol)
  {}

  // ~OdGeKnotVector ();

  OdGeKnotVector& operator = (
      const OdGeKnotVector& knotVector)
  {
    m_Data = knotVector.m_Data;
    m_Tolerance = knotVector.m_Tolerance;
    return *this;
  }

  OdGeKnotVector& operator = (
      const OdGeDoubleArray& dblArray)
  {
    m_Data = dblArray;
    return *this;
  }

  /**
    Arguments:
    i (I) Index of knot.
    
    Remarks:
    Returns or references the ith knot of the knot vector.
  */
  double& operator [] (
    int i)
  { ODA_ASSERT (isValid (i)); return m_Data[i]; }
  
  double operator [] (
    int i) const
  { ODA_ASSERT (isValid (i)); return m_Data[i]; }

   
  /**
    Description:
    Returns true if and only if knotVector is identical to this one.
    
    Arguments:
    knotVector (I) Knot vector. 
  */
  bool isEqualTo (
    const OdGeKnotVector& knotVector) const;

  /**
    Description:
    Returns the first *knot* value of this vector.
  */
  double startParam () const { return m_Data.first ();}

  /**
    Description:
    Returns the last *knot* value of this vector.
  */
  double endParam () const { return m_Data.last ();}
  
  /**
    Description:
    Returns *knot* multiplicity (repetitions) of the *knot* value at the specified index.
    
    Arguments:
    idx (I) Index of the *knot* to be queried.
    
    Remarks:
    If consecutive *knots* are within the *knot* equality *tolerance*,
    the *knots* are considered identical, and their multiplicities combined.
  */
  int multiplicityAt (
    int idx) const;
    
  /**
    Description:
    Returns the number of intervals between distinct *knots*. 

    Remarks:
    Consecutive *knots* are considered distinct if and only if
    they are not within the *knot* equality *tolerance*.
  */
  int numIntervals () const;
  
  /**
    Description:
    Returns the *knot* *interval*, and the index of the *knot* *interval*, containing the point specified by param. 

    Arguments:
    order (I) The *order* of the spline.
    param (I) Parameter to specify a point on the vector.
    interval (O) Receives the *interval* containing the point specified by param.
    Remarks:
    param must lie between the *knot* values indexed by order -1 and length() - order, where order is the *order* of the spline. 
  */
  int getInterval (
    int order, 
    double param, 
    OdGeInterval& interval) const;

  /**
    Description:
    Returns the number of distinct *knots*. 
    
    Arguments:
    knots (I) Array of *knots*.
    multiplicity (O) Receives an array of multiplicities (repetitions) of each *knot*.

    Remarks:
    If consecutive *knots* are within the *knot* equality *tolerance*,
    the *knots* are considered identical, and their multiplicities combined.
  */    
  void getDistinctKnots (
    OdGeDoubleArray& knots, 
    OdGeIntArray *multiplicity = NULL) const;

  /**
    Description:
    Returns true if an only if the specified parameter is
    between the first and last *knots*.
    
    Arguments:
    param (I) Parameter to be tested. 
  */
  bool contains (
    double param) const
  {
    int last = m_Data.size () - 1;
    return (last < 0) ? false : (param >= m_Data[0] - m_Tolerance && param <= m_Data[last] + m_Tolerance);
  }

  /**
    Description:
    Returns true if and only if knot is a member of this vector within the *knot* equality *tolerance*.
  */
  bool isOn (
    double knot) const;

  /**
    Description:
    Reverses the *order* of this vector, and returns a reference to this vector.
  */
  OdGeKnotVector& reverse ();

  /**
    Description:
    Removes the specified *knot* from this vector, and returns a reference to this vector.
    
    Arguments:
    idx (I) Index of the *knot* to be removed.
  */
  OdGeKnotVector& removeAt (
    int);

  /**
    Description:
    Removes the specified range of *knots* from this vector, and returns a reference to this vector.
    
    Arguments:
    startIndex (I) Index of the first *knot* to be removed.
    endIndex (I) Index of the last *knot* to be removed.
  */
  OdGeKnotVector& removeSubVector (
    int startIndex, 
    int endIndex);

  /**
    Description:
    Inserts the specified *knot* the specified number of times at the specified index, and returns a reference to 
    this vector.
    
    Arguments:
    idx (I) Index of the *knot* to be inserted.
    knot (I) Value to be inserted
    multiplicity (I) Number ot times to *insert* the *knot*.
  */
  OdGeKnotVector& insertAt (
    int idx, 
    double knot, 
    int multiplicity = 1);

  /**
    Description:
    Inserts a knot in the appropriate *knot* interval as specified
    by param, and returns a reference to this vector.
    
    Arguments:
    param (I) Parameter to specify a point on the vector.
    
    Remarks:
    If the specified point is within the *knot* equality *tolerance* of another *knot*,
    said knot's multiplicity is incremented.
  */  
  OdGeKnotVector& insert (
    double param);

  /**
    Description:
    Appends a vector, or single *knot*, to this vector, and returns a reference to this vector, or the value of the single *knot*. 
   
    Arguments:
    knot (I) New last *knot* value.
    tail (I) Knot vector to be appended.
    knotRatio (I) Knot ratio.
    
    Remarks:
    If knotRatio > 0, append performs a linear transformations on this vector and on tail,
    such that the ratio of their lengths is equal to knotRatio, and that tail immediately follows
    this vector. tail is modified by this operation.
  */
  int append (
    double knot)
  {
    m_Data.append (knot);
    return m_Data.size ();
  }

  OdGeKnotVector& append (
    OdGeKnotVector& tail, 
    double knotRatio = 0.0);

  /**
    Description:
    
    Splits this vector at the point corresponding to param.
    
    Arguments:
    param (I) Parameter to specify a point on the vector.
    pKnotHead (O) Receives the head portion of the *split*.
    multLast (I) Multiplicity of the last *knot* in the head portion.
    pKnotTail (O) Receives the tail portion of the *split*.
    multFirst (I) Multiplicity of the first knot in the tail portion.
  */
  int split (
    double param,
    OdGeKnotVector* pKnotHead,
    int multLast,
    OdGeKnotVector* pKnotTail,
    int multFirst) const;

  /**
    Description:
    Transforms this vector such that the first *knot* has a value
    of lower, and the last *knot* has a value of *upper*, and 
    returns a reference to this vector. 
  
    Arguments:
    lower (I) New *lower* knot.
    upper (I) New *upper* knot.
  */
  OdGeKnotVector& setRange (
    double lower, 
    double upper);

  /**
    Description:
    Returns the *knot* equality *tolerance*.
  */
  double tolerance () const { return m_Tolerance; }
  
  /**
    Description:
    Sets the *knot* equality *tolerance* for this vector, 
    and returns a reference to this vector.

    Arguments:
     tol (I) Knot equality *tolerance*.
  */
  OdGeKnotVector& setTolerance (
    double tol) 
  { m_Tolerance = tol; return *this;}


  /**
    Description:
    Returns the *length* of this vector.
  */
  int length () const { return m_Data.size ();}

  /**
    Description:
    Returns true if and only if length() == 0.
  */
  bool isEmpty () const { return length () == 0;}
  
  /**
    Description:
    Returns the logical *length* of this vector.
    
    Remarks:
    The logical *length* is the number of elements in the array returned
    by asArrayPtr() and getPtr().
  */  
  int logicalLength () const { return m_Data.size ();}

  /**
    Description:
    Sets the logical *length* of this vector, 
    and returns a reference to this vector.
    
    Arguments:
    size (I) Logical *length* of vector.
    
    Remarks:
    The logical *length* is the number of elements in the array returned
    by asArrayPtr() and getPtr().
  */  
  OdGeKnotVector& setLogicalLength (
    int size)
  {
    m_Data.resize (size);
    return *this;
  }
    
  // int physicalLength () const;
  // OdGeKnotVector& setPhysicalLength (int);

  // int growLength () const;
  // OdGeKnotVector& setGrowLength (int);

  /**
    Description:
    Returns a pointer to this vector as an array of doubles.
    
    Remarks:
    The number of elements in this array is returned by
    logicalLength(), and *set* by setLogicalLength().
  */
  const double* getPtr () const { return m_Data.asArrayPtr (); }

  /**
    Description:
    Returns a pointer to this vector as an array of doubles.
    
    Remarks:
    The number of elements in this array is returned by
    logicalLength(), and *set* by setLogicalLength().
  */
  const double* asArrayPtr () const { return m_Data.asArrayPtr (); }
  double* asArrayPtr () { return m_Data.asArrayPtr (); }


  /**
    Description:
    Sets the parameters for this vector according to the arguments, 
    and returns a reference to this vector.

    Arguments:
    tol (I) Knot equality *tolerance*.
    size (I) Length of vector.
  */
  OdGeKnotVector& set (
    int size, 
    const double source[], 
    double tol = 1.e-9)
    {
      m_Data.assign (source, source + size);
      m_Tolerance = tol;
      return *this;
    }


protected:
  OdGeDoubleArray m_Data;
  double m_Tolerance;
  
  /**
    Returns true if and only if idx < length()
    
    Arguments:
    idx (I) Index to be queried.
  */
   bool isValid (
    OdUInt32 idx) const
    { return idx < m_Data.size (); }
};



#include "DD_PackPop.h"

#endif // OD_GE_KNOT_VECTOR

