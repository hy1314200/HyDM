
#ifndef __OD_LINE_WT_INDEX__
#define __OD_LINE_WT_INDEX__

inline int lineWeightIndex(OdDb::LineWeight lw)
{
  switch(lw)
  {
  case OdDb::kLnWt000:
    return 0;
  case OdDb::kLnWt005:
    return 1;
  case OdDb::kLnWt009:
    return 2;
  case OdDb::kLnWt013:
    return 3;
  case OdDb::kLnWt015:
    return 4;
  case OdDb::kLnWt018:
    return 5;
  case OdDb::kLnWt020:
    return 6;
  case OdDb::kLnWt025:
    return 7;
  case OdDb::kLnWt030:
    return 8;
  case OdDb::kLnWt035:
    return 9;
  case OdDb::kLnWt040:
    return 10;
  case OdDb::kLnWt050:
    return 11;
  case OdDb::kLnWt053:
    return 12;
  case OdDb::kLnWt060:
    return 13;
  case OdDb::kLnWt070:
    return 14;
  case OdDb::kLnWt080:
    return 15;
  case OdDb::kLnWt090:
    return 16;
  case OdDb::kLnWt100:
    return 17;
  case OdDb::kLnWt106:
    return 18;
  case OdDb::kLnWt120:
    return 19;
  case OdDb::kLnWt140:
    return 20;
  case OdDb::kLnWt158:
    return 21;
  case OdDb::kLnWt200:
    return 22;
  case OdDb::kLnWt211:
    return 23;
  }
  return 0;
}

#endif // __OD_LINE_WT_INDEX__
