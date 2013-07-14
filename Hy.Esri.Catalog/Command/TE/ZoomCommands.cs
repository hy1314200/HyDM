using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;

namespace ThreeDimenDataManage.Command.TE
{
    public class CommandViewGlobe:TECommand
    {
        public override void OnClick()
        {
            (m_TEHelper.TerrainExplorer as IMenu).Invoke((int)enumTECommandType.Globe);
        }
        public override bool Enabled
        {
            get
            {
                return !string.IsNullOrEmpty(m_TEHelper.SGWorld.Project.Name);
            }
        }
    }

    public class CommandViewState:TECommand
    {
        public override void OnClick()
        {
            (m_TEHelper.TerrainExplorer as IMenu).Invoke((int)enumTECommandType.State);
        }
        public override bool Enabled
        {
            get
            {
                return !string.IsNullOrEmpty(m_TEHelper.SGWorld.Project.Name);
            }
        }
    }
    public class CommandViewCity:TECommand
    {
        public override void OnClick()
        {
            (m_TEHelper.TerrainExplorer as IMenu).Invoke((int)enumTECommandType.City);
        }
        public override bool Enabled
        {
            get
            {
                return !string.IsNullOrEmpty(m_TEHelper.SGWorld.Project.Name);
            }
        }
    }

    public class CommandViewStreet:TECommand
    {
        public override void OnClick()
        {
            (m_TEHelper.TerrainExplorer as IMenu).Invoke((int)enumTECommandType.Street);
        }
        public override bool Enabled
        {
            get
            {
                return !string.IsNullOrEmpty(m_TEHelper.SGWorld.Project.Name);
            }
        }
    }
    
    public class CommandViewHouse:TECommand
    {
        public override void OnClick()
        {
            (m_TEHelper.TerrainExplorer as IMenu).Invoke((int)enumTECommandType.House);
        }
        public override bool Enabled
        {
            get
            {
                return !string.IsNullOrEmpty(m_TEHelper.SGWorld.Project.Name);
            }
        }
    }
}
