using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Skyline.Core.Helper;

namespace Skyline.Core.UI
{
    public partial class FrmPolylineSymbol : Form
    {
        private UCThematicUniqueValue fatherform;
        public FrmPolylineSymbol(UCThematicUniqueValue Parent)
        {
            fatherform = Parent;
            InitializeComponent();
        }

        private void FrmPolylineSymbol_Load(object sender, EventArgs e)
        {
            try
            {
                switch (this.fatherform.CurrentSymbol.CurrentPolylineSymbol.PolylineType)
                {
                    case "Solidline":
                        this.imageComboBoxPolylineType.SelectedIndex = 0;
                        break;
                    case "Dottedline":
                        this.imageComboBoxPolylineType.SelectedIndex = 1;
                        break;
                    case "Dottedline2":
                        this.imageComboBoxPolylineType.SelectedIndex = 2;
                        break;
                    case "Dottedline3":
                        this.imageComboBoxPolylineType.SelectedIndex = 3;
                        break;
                    case "Dottedline4":
                        this.imageComboBoxPolylineType.SelectedIndex = 4;
                        break;
                    case "Dottedline5":
                        this.imageComboBoxPolylineType.SelectedIndex = 5;
                        break;
                    case "Dottedline6":
                        this.imageComboBoxPolylineType.SelectedIndex = 6;
                        break;
                    case "Dottedline7":
                        this.imageComboBoxPolylineType.SelectedIndex = 7;
                        break;
                    case "Dottedline8":
                        this.imageComboBoxPolylineType.SelectedIndex = 8;
                        break;
                    default:
                        break;
                }
                this.spinEditPolylineWidth.Text = this.fatherform.CurrentSymbol.CurrentPolylineSymbol.PolylineWidth.ToString();
                this.colorEditLineBackColor.Color = this.fatherform.CurrentSymbol.CurrentPolylineSymbol.PolylineBackColor;
                this.spinEditLineBackOpacity.Text = this.fatherform.CurrentSymbol.CurrentPolylineSymbol.PolylineBackOpacity.ToString();
            }
            catch
            {
                MessageBox.Show("窗体加载失败！");
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                PolylineSymbol pPolylineSymbol = new PolylineSymbol();
                #region
                switch (this.imageComboBoxPolylineType.SelectedIndex)
                {
                    case 0:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Solidline.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Solidline.png");
                        }
                        pPolylineSymbol.PolylineType = "Solidline";
                        break;
                    case 1:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline";
                        break;
                    case 2:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline2.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline2.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline2";
                        break;
                    case 3:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline3.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline3.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline3";
                        break;
                    case 4:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline4.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline4.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline4";
                        break;
                    case 5:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline5.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline5.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline5";
                        break;
                    case 6:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline6.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline6.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline6";
                        break;
                    case 7:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline7.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline7.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline7";
                        break;
                    case 8:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline8.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Dottedline8.png");
                        }
                        pPolylineSymbol.PolylineType = "Dottedline8";
                        break;
                    default:
                        break;
                }
                #endregion
                //若datagrid中有加载数据，修改grid中显示图片
                if (this.fatherform.CurrentThemeType == 1)
                {
                    if (this.fatherform.SimpleThemeGridView.RowCount > 0)
                        ((DataGridViewImageColumn)this.fatherform.SimpleThemeGridView.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + pPolylineSymbol.PolylineType + "55.png");

                }
                else
                {
                    if (this.fatherform.CurrentThemeType == 2)
                    {
                        if (this.fatherform.BreakThemeGridView.RowCount == 1)
                        {
                            this.fatherform.BreakThemeGridView.Rows[0].Cells[0].Value = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + pPolylineSymbol.PolylineType + "55.png");
                        }
                        else
                        {
                            #region
                            int ClassNum = this.fatherform.BreakThemeGridView.RowCount;
                            double Scalestep = 2.0 / (ClassNum - 1);
                            Image pImage = null;
                            pImage = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + pPolylineSymbol.PolylineType + "55.png");

                            for (int i = 0; i < ClassNum; i++)
                            {
                                this.fatherform.BreakThemeGridView[0, i].Value = ImageHelper.KiResizeImage(pImage, (int)(pImage.Width * (1 + Scalestep * i)), (int)(pImage.Height * (1 + Scalestep * i)));
                            }
                            #endregion
                        }
                    }
                }
                pPolylineSymbol.PolylineWidth = Convert.ToDouble(this.spinEditPolylineWidth.Text);
                pPolylineSymbol.PolylineBackColor = this.colorEditLineBackColor.Color;
                pPolylineSymbol.PolylineBackOpacity = Convert.ToInt32(this.spinEditLineBackOpacity.Text);
                this.fatherform.CurrentSymbol.CurrentPolylineSymbol = pPolylineSymbol;
                this.Close();
            }
            catch
            {
                MessageBox.Show("线符号化失败！");
            }

        }

        private void FrmPolylineSymbol_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.fatherform.CurrentThemeType == 1)
            {
                this.fatherform.myPolylineSymbol = null;
            }
            else
            {
                if (this.fatherform.CurrentThemeType == 2)
                    this.fatherform.myPolylineBreakSymbol = null;
            }
        }

    }
}
