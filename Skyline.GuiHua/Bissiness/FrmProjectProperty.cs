using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace Skyline.GuiHua.Bussiness
{
    public partial class FrmProjectProperty : DevExpress.XtraEditors.XtraForm
    {
        public FrmProjectProperty()
        {
            InitializeComponent();
        }

        public ProjectInfo Project
        {
            set
            {
                if (value == null)
                    return;

                //txtName.Text = value.Name;
                //txtType.Text = value.Type;
                //txtEnterprise.Text = value.Enterprise;
                //txtAddress.Text = value.Address;
                this.ucProjectBaseInfo.Project = value;

                this.tabCtrlSchema.TabPages.Clear();
                this.tabCtrlSchema.TabPages.Add(this.tpBaseInfo);
                if (value.Schemas != null)
                {
                    foreach (SchemaInfo schemaInfo in value.Schemas)
                    {
                        UCSchemaProperty ucSchemaProperty = new UCSchemaProperty();
                        ucSchemaProperty.Schema = schemaInfo;
                        XtraTabPage tpSchema= this.tabCtrlSchema.TabPages.Add();
                        tpSchema.Text = schemaInfo.Name;
                        tpSchema.Controls.Add( ucSchemaProperty);
                        ucSchemaProperty.Dock = DockStyle.Fill;
                    }
                }
            }
        }

    }
}