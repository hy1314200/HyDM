using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Hy.Dictionary.Operate
{
    public class CommandTestDictionary:Define.BaseCommand
    {
        public CommandTestDictionary()
        {
            this.m_Caption = "字典测试";
            this.m_Category = "字典";
        }
        public override void OnClick()
        {
            IList<DictItem> li= Hy.Dictionary.DictHelper.GetAll();
        }
    }
}
