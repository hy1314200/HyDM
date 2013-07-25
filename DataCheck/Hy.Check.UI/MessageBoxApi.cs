using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Hy.Common.UI;
using Hy.Check.Utility;

namespace Hy.Check.UI
{
    public class MessageBoxApi
    {
        /// <summary>
        /// Shows the finished message box.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void ShowFinishedMessageBox(string text)
        {
            ShowMessageBox(text, COMMONCONST.MESSAGEBOX_HINT, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Shows the waring message box.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void ShowWaringMessageBox(string text)
        {
            ShowMessageBox(text, COMMONCONST.MESSAGEBOX_WARING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Shows the error message box.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void ShowErrorMessageBox(string text)
        {
            ShowMessageBox(text, COMMONCONST.MESSAGEBOX_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows the question message box.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static DialogResult ShowQuestionMessageBox(string text)
        {
            return XtraMessageBox.Show(text, COMMONCONST.MESSAGEBOX_WARING, MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
        }

        private static void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            XtraMessageBox.Show(text, caption, buttons, icon);
        }

    }
}
