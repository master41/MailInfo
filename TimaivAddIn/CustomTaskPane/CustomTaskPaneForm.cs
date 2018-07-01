using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace TimaivAddIn.CustomTaskPane
{
    public partial class CustomTaskPaneForm : UserControl
    {
        #region Constructor
        public CustomTaskPaneForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        internal ElementHost ElementHost => elementHost1;
        #endregion
    }
}
