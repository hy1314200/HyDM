using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

namespace Hy.Check.UI.Forms
{
    public partial class FrmLayerTransparency : XtraForm
    {
        public ILayer m_pLayer;
        private IActiveView m_pActiveView;
        public short nDefaultValue;

        public FrmLayerTransparency()
        {
            InitializeComponent();
            trackBarLayerTransparency.Properties.Maximum = 100;
            trackBarLayerTransparency.Properties.Minimum = 0;
        }

        public void InitForm(ILayer pLayer,IActiveView pActiveView)
        {
            m_pLayer = pLayer;
            m_pActiveView = pActiveView;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //nDefaultValue = Convert.ToInt16(this.txtLayerTransparency.Text);
            ILayerEffects plyrEffects = m_pLayer as ILayerEffects;
            plyrEffects.Transparency = Convert.ToInt16(this.txtLayerTransparency.Text);
            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.txtLayerTransparency.Text = nDefaultValue.ToString();
            trackBarLayerTransparency.Value = nDefaultValue;

            ILayerEffects plyrEffects = m_pLayer as ILayerEffects;
            plyrEffects.Transparency = nDefaultValue;
            m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        private void trackBarLayerTransparency_ValueChanged(object sender, EventArgs e)
        {
            this.txtLayerTransparency.Text = trackBarLayerTransparency.Value.ToString();
            //ILayerEffects plyrEffects = m_pLayer as ILayerEffects;
            //plyrEffects.Transparency = Convert.ToInt16(this.txtLayerTransparency.Text);
            //m_pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            
        }

        private void frmLayerTransparency_Load(object sender, EventArgs e)
        {
            ILayerEffects plyrEffects = m_pLayer as ILayerEffects;
            nDefaultValue = plyrEffects.Transparency;
            this.txtLayerTransparency.Text = nDefaultValue.ToString();
            trackBarLayerTransparency.Value = nDefaultValue;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}