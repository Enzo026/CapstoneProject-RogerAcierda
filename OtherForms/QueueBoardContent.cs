using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Flowershop_Thesis.OtherForms
{
    public partial class QueueBoardContent : UserControl
    {
        public QueueBoardContent()
        {
            InitializeComponent();
        }
        #region UpdateStatus
        private string name;

        [Category("QueueCard")]
        public string Name
        {
            get { return name; }
            set { name = value; NameLbl.Text = value; }
        }
        #endregion

    }
}
