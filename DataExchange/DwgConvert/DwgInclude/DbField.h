///////////////////////////////////////////////////////////////////////////////
// Copyright c 2002, Open Design Alliance Inc. ("Open Design") 
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
//      DWGdirect c 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////

#ifndef OD_DBFIELD_H
#define OD_DBFIELD_H

#include "DD_PackPush.h"

#include "DbObject.h"
#include "DbFieldValue.h"

class OdDbField;

/** Description:
    This template class is a specialization of the OdArray class for OdDbField object pointers.
*/
typedef OdArray<OdDbField*> OdDbFieldArray;


/** Description:
  {group:Structs}
*/      
typedef struct OdFd
{
  // Enum for acdbEvaluateFields
  
  enum EvalFields
  {
    kEvalRecursive      = 0x01    // Recursively evaluate complex objects
  };
  
} OdFd;

class OdDbField;
typedef OdSmartPtr<OdDbField> OdDbFieldPtr;

//*************************************************************************
// OdDbField
//*************************************************************************

/** Description:
    This class represents Field objects in an OdDbDatabase instance.
    
    Remarks:
    Field objects store the Field expression and the evaluated result.
    
    Fields can be evaluated by an evaluator to any one of the following data types: Long,         
    Double, String, Date, Point, 3dPoint, ObjectId, Buffer, and Resbuf       

    {group:OdDb_Classes}
*/
class TOOLKIT_EXPORT OdDbField : public OdDbObject
{
public:
  enum State
  {
    kInitialized        = 0x01,       // Field has been initialized by the evaluator.
    kCompiled           = 0x02,       // Field has beene compiled.
    kModified           = 0x04,       // Field code has been *modified*, but has yet to be evaluated.
    kEvaluated          = 0x08,       // Field has been evaluated.
    kHasCache           = 0x10,       // Field has evaluated cache.
    kHasFormattedString = 0x20        // For DWGdirect internal use only. 
  };
  
  enum EvalOption
  {
    kDisable            = 0,          // Disable evaluation of field.
    kOnOpen             = 0x01,       // Evaluate on drawing open.
    kOnSave             = 0x02,       // Evaluate on drawing save.
    kOnPlot             = 0x04,       // Evaluate on drawing plot.
    kOnEtransmit        = 0x08,       // Evaluate on drawing etransmit.
    kOnRegen            = 0x10,       // Evaluate on drawing regen.
    kOnDemand           = 0x20,       // Evaluate on demand.
    kAutomatic          = (kOnOpen | kOnSave | kOnPlot |  kOnEtransmit | kOnRegen | kOnDemand) // Evaluate automatically.
  };
  
  enum EvalContext
  {
    kOpen               = 0x01,       // Field being evaluated on drawing open.
    kSave               = 0x02,       // Field being evaluated on drawing save.
    kPlot               = 0x04,       // Field being evaluated on drawing plot.
    kEtransmit          = 0x08,       // Field being evaluated on drawing etransmit.
    kRegen              = 0x10,       // Field being evaluated on drawing regen.
    kDemand             = 0x20,       // Field being evaluated on demand.
    kPreview            = 0x40        // Field being evaluated for preview.
  };
  
  enum EvalStatus
  {
    kNotYetEvaluated    = 0x01,       // Yet to be evaluated.
    kSuccess            = 0x02,       // Evaluated successfully.
    kEvaluatorNotFound  = 0x04,       // Evaluator not found.
    kSyntaxError        = 0x08,       // Field code syntax error.
    kInvalidCode        = 0x10,       // Invalid field code.
    kInvalidContext     = 0x20,       // Invalid context to evaluate field.
    kOtherError         = 0x40        // Other evaluation error.
  };
  
  enum FieldCodeFlag
  {
    kFieldCode          = 0x01,       // Get raw field code. Ignored by setFieldCode().
    kEvaluatedText      = 0x02,       // Get evaluated text. Ignored by setFieldCode().
    kEvaluatedChildren  = 0x04,       // Get field code with evaluated text for Child fields. Ignored by setFieldCode().
    kObjectReference    = 0x08,       // Get embedded text as references to Child fields. Ignored by setFieldCode().
    kAddMarkers         = 0x10,       // Get embedded text as field codes enclosed in field markers. Ignored by setFieldCode().
    kEscapeBackslash    = 0x20,       // Convert single backslashes to double backslashes. Ignored by setFieldCode().
    kStripOptions       = 0x40,       // Strip the standard options from field code. Ignored by setFieldCode().
    kPreserveFields     = 0x80,       // For DWGdirect internal use only. Ignored by getFieldCode().
    kTextField          = 0x100       // Treat the field as text with embedded fields. Ignored by getFieldCode().
  };
  
  enum FilingOption
  {
    kSkipFilingResult   = 0x01        // Don't file field value.
  };
  
public:
  ODDB_DECLARE_MEMBERS(OdDbField);
  
  OdDbField();

  // OdDbField(const OdString& fieldCode, bool bTextField = false);
  // ~OdDbField(void);
  
  /** Description:
    Sets this Field object as a property of the specified object, and
    adds it to the *database*.
    
    Arguments:
    pOwner (I) Pointer to the Owner object.
    propertyName (I) Name of the property.
    
    Remarks:
    The specified object must be *database* residient.
  */
  OdResult setInObject(
    OdDbObject* pOwner, 
    const OdString& propertyName);
  
  /** Description:
    Adds this Field object and its Child objects to the specified *database*.
    
    Arguments:
    pDb (I) Pointer to the *database* in which to post.
  */
  OdResult postInDatabase(
    OdDbDatabase* pDb);
  
  /** Description:
    Returns the *state* of this Field object.
    
    Remarks:
    state will return a combination of the following: 
    
    @table
    Name                   Value    Description
    kInitialized           0x01     Field has been initialized by the evaluator.
    kCompiled              0x02     Field has beene compiled.
    kModified              0x04     Field code has been *modified*m but has yet to be evaluated.
    kEvaluated             0x08     Field has been evaluated.
    kHasCache              0x10     Field has evaluated cache.
    kHasFormattedString    0x20     For DWGdirect internal use only. 
  */
  OdDbField::State state() const;
  
  /** Description:
    Returns the evaluation status of this Field object.
    
    Remarks:
    evaluationStatus will return one of the following: 
    
    @table
    Name                   Value    Description
    kNotYetEvaluated       0x01     Yet to be evaluated.
    kSuccess               0x02     Evaluated successfully.
    kEvaluatorNotFound     0x04     Evaluator not found.
    kSyntaxError           0x08     Field code syntax error.
    kInvalidCode           0x10     Invalid field code.
    kInvalidContext        0x20     Invalid context to evaluate field.
    kOtherError            0x40     Other evaluation error.
  */
  OdDbField::EvalStatus evaluationStatus() const; 
                                          // OdUInt32* errCode = NULL, 
                                         // OdString* errMsg = NULL) const;

  /** Description:
    Returns the evaluation option of this Field object.
    
    Remarks:
    evaluationOption will a combination of the following: 
    
    @table
    Name                   Value    Description
    kDisable               0        Disable evaluation of field.
    kOnOpen                0x01     Evaluate on drawing open.
    kOnSave                0x02     Evaluate on drawing save.
    kOnPlot                0x04     Evaluate on drawing plot.
    kOnEtransmit           0x08     Evaluate on drawing etransmit.
    kOnRegen               0x10     Evaluate on drawing regen.
    kOnDemand              0x20     Evaluate on demand.
    kAutomatic             0x3F     Evaluate automatically.
  */
  OdDbField::EvalOption evaluationOption() const;
  
  /** Description:
    Sets the evaluation option for this Field object.
    
    Arguments:
    evaluationOption (I) Evaluation option.
    
    Remarks:
    evaluationOption will a combination of the following: 
    
    @table
    Name                   Value    Description
    kDisable               0        Disable evaluation of field.
    kOnOpen                0x01     Evaluate on drawing open.
    kOnSave                0x02     Evaluate on drawing save.
    kOnPlot                0x04     Evaluate on drawing plot.
    kOnEtransmit           0x08     Evaluate on drawing etransmit.
    kOnRegen               0x10     Evaluate on drawing regen.
    kOnDemand              0x20     Evaluate on demand.
    kAutomatic             0x3F     Evaluate automatically.
  */
  OdResult setEvaluationOption(
    OdDbField::EvalOption evaluationOption);
    
  /** Description:
    Returns the filing option of this Field object.
    
    Remarks:
    filingOption will return one of the following: 
    
    @table
    Name                   Value    Description
    kSkipFilingResult      0x01     Don't file field value.
  */
  OdDbField::FilingOption filingOption() const;
  
  /** Description:
    Sets the filing option for this Field object.
    
    Remarks:
    filingOption will be one of the following: 
    
    @table
    Name                   Value    Description
    kSkipFilingResult      0x01     Don't file field value.
  */
  OdResult setFilingOption(
    OdDbField::FilingOption filingOption);
  
  /** Description:
    Returns the ID of the evaluator for this Field object.
    
    Remarks:
    Returns an empty string if no evaluator has been set for this Field object, or
    the evaluator cannot be found.
  */
  OdString evaluatorId() const;

  /** Description:
    Sets the ID of the evaluator for this Field object.
    Arguments:
    evaluatorId (I) Evaluator ID.
  */
  OdResult setEvaluatorId(
    const OdString& evaluatorId);
  
  /** Description:
    Returns true if and only if this Field object is text with Child fields.
  */
  bool isTextField() const;

  // OdResult convertToTextField (void);

  // OdString getFieldCode     (OdDbField::FieldCodeFlag nFlag, OdArray<OdDbField*>* pChildFields = NULL, 
  //                           OdDb::OpenMode mode = OdDb::kForRead) const;
  
  /** Description:
    Returns the field code of this Field object in the specfied form..
    
    Arguments:
    flags (I) Field code *flags(.
    
    Remarks:
    flags can be one or more of the following values:
    
    @table
    Name                   Value    Description
    kFieldCode             0x01     Get raw field code. 
    kEvaluatedText         0x02     Get evaluated text.
    kEvaluatedChildren     0x04     Get field code with evaluated text for Child fields.
    kObjectReference       0x08     Get field code as object reference.
    kAddMarkers            0x10     Include markers around field codes.
    kEscapeBackslash       0x20     Convert single backslashes to double backslashes.
    kStripOptions          0x40     Strip the standard options from field code.
    kPreserveFields        0x80,    For DWGdirect internal use only. Ignored.
    kTextField             0x100    Treat the field as text with embedded fields. Ignored.
  */
  OdString getFieldCode(
    OdDbField::FieldCodeFlag flags);

  //                      OdArray<OdDbField*>* pChildFields = NULL, 
  //                      OdDb::OpenMode mode = OdDb::kForRead) const;


  /** Description:
    Sets the field code of this Field object in the specfied form..
    
    Arguments:
    fieldCode (I) Field code.
    flags (I) Field code *flags*.
    pChildFields (I) Pointer to an array of Child fields.
    
    Remarks:
    Child fields are the embedded fields of text fields, and 
    nested fields of non-text fields.
    
    flags can be one or more of the following values:
    
    @table
    Name                   Value    Description
    kFieldCode             0x01     Get raw field code. Ignored.
    kEvaluatedText         0x02     Get evaluated text. Ignored. 
    kEvaluatedChildren     0x04     Get field code with evaluated text for Child fields. Ignored.
    kObjectReference       0x08     Get field code as object reference. Ignored.
    kAddMarkers            0x10     Include markers around field codes. Ignored.
    kEscapeBackslash       0x20     Convert single backslashes to double backslashes. Ignored.
    kStripOptions          0x40     Strip the standard options from field code. Ignored.
    kPreserveFields        0x80,    For DWGdirect internal use only.
    kTextField             0x100    Treat the field as text with embedded fields.
  */
  OdResult setFieldCode(
    OdString& fieldCode, 
    OdDbField::FieldCodeFlag flags = (OdDbField::FieldCodeFlag) 0,
    OdDbFieldArray* pChildFields = NULL);
  
  /** Description:
    Returns the number of Child fields in this Field object.
  */
  OdUInt32  childCount() const;
  
  /** Description:
    Returns the specified Child field in this Field object, and opens it in the specified mode.
    
    Arguments:
    childIndex (I) Child index.
    openMode (I)  Mode in which to the Child field.
  */
  OdDbFieldPtr getChild(
    OdUInt32 childIndex, 
    OdDb::OpenMode openMode);

  /** Description:
    Returns the output *format* for this Field object.
  */
  OdString getFormat() const;

  /** Description:
    Sets the output *format* for this Field object.
    
    Arguments:
    format (I) Output *format*.
    
    Note:
    Returns eOk if successful, or an appropriate error code if not.
  */
  OdResult setFormat(
    const OdString& format);
          
  // OdResult evaluate    (int nContext, OdDbDatabase* pDb, 
  //                      int* pNumFound     = NULL, int* pNumEvaluated = NULL);
  
  
  /** Description:
    Returns the data type of this Field object in the specfied form..
    
    Remarks:
    Returns kUnknown if the field has yet to be evaluated.

    dataType will return one of the following values:
    
    @table
    Name              Value
    kUnknown          0
    kLong             0x01
    kDouble           0x02
    kString           0x04
    kDate             0x08
    kPoint            0x10
    k3dPoint          0x20
    kObjectId         0x40
    kBuffer           0x80
    kResbuf           0x100
    
  */
  OdFieldValue::DataType dataType() const;

  /** Description:
    Returns the field evaluation string using the output *format* for this Field object.
    
    Arguments:
    pValue (O) Receives a pointer to the evaluation string.
    
    Remarks:
    If returning an OdString, returns an empty string if not successful.
    
    If returning an OdResult, returns eOk if successful, or an appropriate error code if not.

    Note:
    The string pointed to by pValue must be freed by the caller.
  */            
  OdString getValue() const;
  OdResult getValue(
    OdFieldValuePtr& pValue) const;
            
  /*
  bool  hasHyperlink            (void) const;
  OdResult getHyperlink(char** pszName,
                        char** pszDescription, 
                        char** pszSubLocation,
                        char** pszDisplayString) const;
  OdResult setHyperlink(const char* pszName, 
                        const char* pszDescription, 
                        const char* pszSubLocation = NULL);
  OdResult removeHyperlink(void);
  */
  
  /** Description:
    Returns the specified data of this Field object.
    
    Arguments:
    key (I) Key.
  */
  OdFieldValuePtr getData(
    const OdString& key) const;
  
  /** Description:
    Sets the specified data for this Field object.
    
    Arguments:
    key (I) Key.
    pData (I) Pointer to the object containing the data. 
  */
  OdResult setData(
    const OdString& key, 
    const OdFieldValuePtr& pData);

public:
  // Base class overrides

  virtual OdResult dwgInFields(
    OdDbDwgFiler* pFiler);
  virtual void dwgOutFields(
    OdDbDwgFiler* pFiler) const;
  virtual OdResult dxfInFields(
    OdDbDxfFiler* pFiler);
  virtual void dxfOutFields(
    OdDbDxfFiler* pFiler) const;
  
  // virtual OdResult subClose();
};

#include "DD_PackPop.h"
#endif // OD_DBFIELD_H
