using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Skyline.GuiHua.Bussiness
{
    public partial class UCSchemaProperty : UserControl
    {
        public UCSchemaProperty()
        {
            InitializeComponent();
        }

        public SchemaInfo Schema
        {
            set
            {
                if (value == null)
                    return;

                txtBuildingArea.Text = value.BuildingArea.ToString();
                txtName.Text = value.Name;
                txtRoadArea.Text = value.RoadArea.ToString();
                txtType.Text = value.Type;
                txtVegArea.Text = value.VegetationArea.ToString();
            }
        }
    }
}
