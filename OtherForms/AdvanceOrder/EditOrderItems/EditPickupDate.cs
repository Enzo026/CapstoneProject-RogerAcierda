using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flowershop_Thesis.OtherForms.AdvanceOrder.EditOrderItems
{
    public partial class EditPickupDate : UserControl
    {
        public EditPickupDate()
        {
            InitializeComponent();
            setdate();
            
        }
        public void setdate()
        {
            
            DateTime today = DateTime.Today;
            DateTime mindate  = today.AddDays(1);

            dateTimePicker1.MinDate = mindate;
        }
    }
}
