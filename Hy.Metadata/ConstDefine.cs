using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Metadata
{
    internal class ConstDefine
    {
        const string StandardTableName = "T_MetaStandard";
        const string ColumnsTableName = "T_MetaColumns";
        const string MetaMappingDictTypeName = "MetaMapping";

        internal const string Type_Key_String = "StringTypeKey";
        internal const string Type_Key_Int = "IntTypeKey";
        internal const string Type_Key_Decimal = "DecimalTypeKey";
        internal const string Type_Key_DateTime = "DateTimeKey";
        internal const string Type_Key_Binary = "BinaryKey";

        internal const string Pagination_KeyName = "PaginationSQL";
    }
}
