using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Skyline.Core.Helper
{
	public class CSharpAPIsClass
	{
        private delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        //[DllImport("user32.dll")]
        //private static extern IntPtr FindWindowW(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        private static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

         [DllImport("user32.dll")]

       [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);



        [StructLayout(LayoutKind.Sequential)]

        public struct RECT

       {

            public int Left;                             //最左坐标

            public int Top;                             //最上坐标

            public int Right;                         //最右坐标
                                              
            public int Bottom;                        //最下坐标

       }

        public static RECT getRect(IntPtr awin)
        {



            RECT rc = new RECT();

            GetWindowRect(awin, ref rc);

            return rc;

        }

        public static Array getWinWH(IntPtr awin)
       {

       

            RECT rc = new RECT();

            GetWindowRect(awin, ref rc);

            int width = rc.Right - rc.Left;                        //窗口的宽度

            int height = rc.Bottom - rc.Top;                   //窗口的高度

            int x = rc.Left;

            int y = rc.Top;

            int[] res={width,height};

            return res;

        }


        public struct WindowInfo
        {
            public IntPtr hWnd;
            public string szWindowName;
            public string szClassName;
        }

        public WindowInfo[] GetAllDesktopWindows()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();

            //enum all desktop windows
            EnumWindows(delegate(IntPtr hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd
                wnd.hWnd = hWnd;
                //get window name
                GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class
                GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();
                //add it into list
                wndList.Add(wnd);
                return true;
            }, 0);

            return wndList.ToArray();
        }
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;                                                          //常量，隐藏
        private const int SW_SHOWNORMAL = 1;                                                    //常量，显示，标准状态
        private const int SW_SHOWMINIMIZED = 2;                                                 //常量，显示，最小化
        private const int SW_SHOWMAXIMIZED = 3;                                                 //常量，显示，最大化
        private const int SW_SHOWNOACTIVATE = 4;                                                //常量，显示，不激活
        private const int SW_RESTORE = 9;                                                       //常量，显示，回复原状
        private const int SW_SHOWDEFAULT = 10;                                                  //常量，显示，默认


        public  void ToChange(IntPtr p, bool isboolean)
        {
            if (isboolean)
            {
                ShowWindowAsync(p, SW_SHOWNORMAL);
            }
            else
            {
                ShowWindowAsync(p, SW_HIDE);
            }
        }   

	}
}
