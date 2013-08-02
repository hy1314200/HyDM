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
    public partial class FrmPointSymbol : Form
    {
        private UCThematicUniqueValue fatherform;
        public FrmPointSymbol(UCThematicUniqueValue Parent)
        {
            fatherform = Parent;
            InitializeComponent();
        }

        private void FrmPointSymbol_Load(object sender, EventArgs e)
        {
            try
            {
                switch (this.fatherform.CurrentSymbol.CurrentPointSymbol.PointType)
                {
                    case "Circle":
                        this.imageComboPointType.SelectedIndex = 0;
                        break;
                    case "Triangle":
                        this.imageComboPointType.SelectedIndex = 1;
                        break;
                    case "Rectangle":
                        this.imageComboPointType.SelectedIndex = 2;
                        break;
                    case "Pentagon":
                        this.imageComboPointType.SelectedIndex = 3;
                        break;
                    case "Hexagon":
                        this.imageComboPointType.SelectedIndex = 4;
                        break;
                    case "Arrow":
                        this.imageComboPointType.SelectedIndex = 5;
                        break;
                    default:
                        break;
                }
                this.spinEditPointSize.Text = this.fatherform.CurrentSymbol.CurrentPointSymbol.PointSize.ToString();
            }
            catch
            {
                MessageBox.Show("窗口初始化失败！");
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            try
            {
                PointSymbol pPointSymbol = new PointSymbol();
                // 根据选择的图形修改按钮上相应图形
                #region
                switch (this.imageComboPointType.SelectedIndex)
                {
                    case 0:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Circleblue.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Circleblue.png");
                        }
                        pPointSymbol.PointType = "Circle";
                        break;
                    case 1:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Triangleblue.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Triangleblue.png");
                        }
                        pPointSymbol.PointType = "Triangle";
                        break;
                    case 2:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Rectangleblue.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Rectangleblue.png");
                        }
                        pPointSymbol.PointType = "Rectangle";
                        break;
                    case 3:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Pentagonblue.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Pentagonblue.png");
                        }
                        pPointSymbol.PointType = "Pentagon";
                        break;
                    case 4:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Hexagonblue.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Hexagonblue.png");
                        }
                        pPointSymbol.PointType = "Hexagon";
                        break;
                    case 5:
                        if (this.fatherform.CurrentThemeType == 1)
                            this.fatherform.BtnSymbolType.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Arrowblue.png");
                        else
                        {
                            if (this.fatherform.CurrentThemeType == 2)
                                this.fatherform.BtnSymbolType2.Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\Arrowblue.png");
                        }
                        pPointSymbol.PointType = "Arrow";
                        break;
                    default:
                        break;
                }
                #endregion

                //若datagrid中有加载数据，修改grid中显示图片
                #region
                if (this.fatherform.CurrentThemeType == 1)
                {
                    if (this.fatherform.SimpleThemeGridView.RowCount > 0)
                        ((DataGridViewImageColumn)this.fatherform.SimpleThemeGridView.Columns[0]).Image = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + pPointSymbol.PointType + "55b.png");
                }
                else
                {
                    if (this.fatherform.CurrentThemeType == 2)
                    {
                        if (this.fatherform.BreakThemeGridView.RowCount == 1)
                        {
                            this.fatherform.BreakThemeGridView[0, 0].Value = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + pPointSymbol.PointType + "55b.png");
                        }
                        else
                        {
                            #region
                            int ClassNum = this.fatherform.BreakThemeGridView.RowCount;
                            double Scalestep = 2.0 / (ClassNum - 1);
                            Image pImage = null;
                            pImage = Image.FromFile(Application.StartupPath + @"\SymbolImage\" + pPointSymbol.PointType + "55b.png");
                            for (int i = 0; i < ClassNum; i++)
                            {
                                this.fatherform.BreakThemeGridView[0, i].Value = ImageHelper.KiResizeImage(pImage, (int)(pImage.Width * (1 + Scalestep * i)), (int)(pImage.Height * (1 + Scalestep * i)));
                            }
                            #endregion
                        }
                    }

                }
                #endregion
                pPointSymbol.PointSize = Convert.ToDouble(this.spinEditPointSize.Text);
                this.fatherform.CurrentSymbol.CurrentPointSymbol = pPointSymbol;
                this.Close();
            }
            catch
            {
                MessageBox.Show("点符号化失败！");
            }
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPointSymbol_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.fatherform.CurrentThemeType == 1)
                {
                    this.fatherform.myPointSymbol = null;

                }
                else
                {
                    if (this.fatherform.CurrentThemeType == 2)
                        this.fatherform.myPointBreakSymbol = null;
                }
            }
            catch
            {
                MessageBox.Show("关闭遇到问题！");
            }
           
        }
    }
}
