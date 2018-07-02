using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using static TimaivAddIn.Utils.ResourceUtils;

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

        public void OnClick(Office.IRibbonControl control)
        {
            Globals.ThisAddIn.RibbonManager.OnClick(control.Id);
        }
        #endregion

        #region Methods
        public void Invalidate() => ribbon.Invalidate();
        #endregion
    }
}