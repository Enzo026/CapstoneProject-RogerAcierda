﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.Restocking
{
    public partial class DropdownRestockItem : Form
    {
        public DropdownRestockItem()
        {
            InitializeComponent();
        }

        private void DropdownRestockItem_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}