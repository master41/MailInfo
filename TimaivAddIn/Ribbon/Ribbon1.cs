using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using static TimaivAddIn.Utils.ResourceUtils;
using System.Drawing;

namespace TimaivAddIn
{
    [ComVisible(true)]
    public class Ribbon1 : Office.IRibbonExtensibility
    {
        #region Private Members
        private Office.IRibbonUI ribbon;
        #endregion

        #region Constructor
        public Ribbon1()
        {
        }
        #endregion

        #region IRibbonExtensibility Members
        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("TimaivAddIn.Ribbon.Ribbon1.xml");
        }
        #endregion

        #region Ribbon Callbacks
        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public void OnAction(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.RibbonManager.OnClick(control.Id);
        }

        public string GetLabel(Office.IRibbonControl control)
        {
            return "fuck";
        }

        public Bitmap GetImage(Office.IRibbonControl control)
        {
            return null;
        }

        public string GetScreenTip(Office.IRibbonControl control)
        {
            return null;
        }

        public string GetSuperTip(Office.IRibbonControl control)
        {
            return null;
        }
        #endregion

        #region Methods
        public void Invalidate() => ribbon.Invalidate();
        #endregion
    }
}