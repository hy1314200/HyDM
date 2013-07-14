using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraExplorerX;
using ESRI.ArcGIS.SystemUI;
using System.Windows.Forms;

namespace ThreeDimenDataManage.Utility
{
    public class TEHookHelper
    {

        public TEHookHelper(ITerraExplorer51 teExplorer, ISGWorld61 sgWorld)
        {
            this.TerrainExplorer = teExplorer;
            this.SGWorld = sgWorld;


            // 绑定事件到ESRI的ITool
            _ISGWorld61Events_Event sgWorldEvent = this.SGWorld as _ISGWorld61Events_Event;
            sgWorldEvent.OnRButtonDown += new _ISGWorld61Events_OnRButtonDownEventHandler(sgWorldEvent_OnRButtonDown);
            sgWorldEvent.OnLButtonDown += new _ISGWorld61Events_OnLButtonDownEventHandler(sgWorldEvent_OnLButtonDown);
            sgWorldEvent.OnMButtonDown += new _ISGWorld61Events_OnMButtonDownEventHandler(sgWorldEvent_OnMButtonDown);

            sgWorldEvent.OnLButtonUp += new _ISGWorld61Events_OnLButtonUpEventHandler(sgWorldEvent_OnLButtonUp);
            sgWorldEvent.OnRButtonUp += new _ISGWorld61Events_OnRButtonUpEventHandler(sgWorldEvent_OnRButtonUp);
            sgWorldEvent.OnMButtonUp += new _ISGWorld61Events_OnMButtonUpEventHandler(sgWorldEvent_OnMButtonUp);

            sgWorldEvent.OnLButtonDblClk += new _ISGWorld61Events_OnLButtonDblClkEventHandler(sgWorldEvent_OnLButtonDblClk);
            sgWorldEvent.OnRButtonDblClk += new _ISGWorld61Events_OnRButtonDblClkEventHandler(sgWorldEvent_OnRButtonDblClk);
            sgWorldEvent.OnMButtonDblClk += new _ISGWorld61Events_OnMButtonDblClkEventHandler(sgWorldEvent_OnMButtonDblClk);

        }

        private bool OnMouseDoubleClick(MouseButtons mouseButton,int shift,int x,int y)
        {
            if (this.TETool != null)
                this.TETool.OnMouseUp((int)System.Windows.Forms.MouseButtons.Right, shift, x,y);

            return true;
        }
        bool sgWorldEvent_OnMButtonDblClk(int Flags, int X, int Y)
        {
            return OnMouseDoubleClick(MouseButtons.Middle, Flags, X, Y);
        }
        bool sgWorldEvent_OnRButtonDblClk(int Flags, int X, int Y)
        {
            return OnMouseDoubleClick(MouseButtons.Right, Flags, X, Y);
        }
        bool sgWorldEvent_OnLButtonDblClk(int Flags, int X, int Y)
        {
            return OnMouseDoubleClick(MouseButtons.Left, Flags, X, Y);
        }

        private bool OnMouseUp(MouseButtons mouseButton,int shift,int x,int y)
        {
            if (this.TETool != null)
                this.TETool.OnMouseUp((int)System.Windows.Forms.MouseButtons.Right, shift, x,y);

            return true;
        }
        bool sgWorldEvent_OnMButtonUp(int Flags, int X, int Y)
        {
            return OnMouseUp(MouseButtons.Middle, Flags, X, Y);
        }
        bool sgWorldEvent_OnRButtonUp(int Flags, int X, int Y)
        {
            return OnMouseUp(MouseButtons.Right, Flags, X, Y);
        }
        bool sgWorldEvent_OnLButtonUp(int Flags, int X, int Y)
        {
            return OnMouseUp(MouseButtons.Left, Flags, X, Y);
        }

        private bool OnMouseDown(MouseButtons mouseButton,int shift,int x,int y)
        {
            if (this.TETool != null)
                this.TETool.OnMouseDown((int)System.Windows.Forms.MouseButtons.Right, shift, x,y);

            return true;
        }
        bool sgWorldEvent_OnMButtonDown(int Flags, int X, int Y)
        {
            return OnMouseDown(MouseButtons.Middle, Flags, X, Y);
        }
        bool sgWorldEvent_OnRButtonDown(int Flags, int X, int Y)
        {
            return OnMouseDown(MouseButtons.Right, Flags, X, Y);
        }
        bool sgWorldEvent_OnLButtonDown(int Flags, int X, int Y)
        {
            return OnMouseDown(MouseButtons.Left, Flags, X, Y);
        }

        public ITerraExplorer51 TerrainExplorer { get; private set;}

        public ISGWorld61 SGWorld { get; private set; }

        public ITool TETool { get; set; }
    }
}
