#ifndef _ODDBERRORINVALIDSYSVAR_INCLUDED_
#define _ODDBERRORINVALIDSYSVAR_INCLUDED_

/** Description:

    {group:Error_Classes}
*/
class TOOLKIT_EXPORT OdError_InvalidSysvarIntValue : public OdError
{
public:
  OdError_InvalidSysvarIntValue(const OdString& name, int value, int limmin, int limmax);
  int limmin() const;
  int limmax() const;
  OdString name() const;
};

/** Description:

    {group:Error_Classes}
*/
class TOOLKIT_EXPORT OdError_UnknownSysvar : public OdError
{
public:
  OdError_UnknownSysvar(const OdString& name);
  OdString name() const;
};

#endif
