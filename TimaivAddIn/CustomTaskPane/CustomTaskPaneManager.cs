﻿using System.Collections.Generic;
using System;
using System.Linq;
using static TimaivAddIn.Constants;
using Office = Microsoft.Office.Core;

namespace TimaivAddIn.CustomTaskPane
{
    class CustomTaskPaneManager
    {
        #region Lazy Singleton
        private CustomTaskPaneManager instance;
        internal CustomTaskPaneManager GetInstance() => instance ?? (instance = new CustomTaskPaneManager());
        #endregion

        #region Consturctor
        private CustomTaskPaneManager() { }
        #endregion

        #region Private Members
        private readonly List<PaneWrapper> wrappers = new List<PaneWrapper>();
        #endregion

        #region Methods
        public void InitPane(object _window)
        {
            if (_window == null) throw new ArgumentNullException();

            if (GetPane(_window) == null)
            {
                var pane = Globals.ThisAddIn.CustomTaskPanes.Add(new CustomTaskPaneForm(),
                                                                 APP_NAME);
                pane.Width = PANE_INITIAL_WIDTH;
                pane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                pane.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;

                PaneWrapper wrapper = new PaneWrapper(_window, pane);
            }
        }

        public PaneWrapper GetPane(object _window)
        {
            if (_window == null) throw new ArgumentNullException();

            return wrappers.FirstOrDefault(i => i.Window == _window);
        }

        public void ShowPane(object _window)
        {
            if (_window == null) throw new ArgumentNullException();

            if (GetPane(_window) is PaneWrapper wrapper)
            {
                ShowPane(wrapper);
            }
        }

        public void ShowPane(PaneWrapper _wrapper)
        {
            if (_wrapper == null) throw new ArgumentNullException();

            if (!_wrapper.Pane.Visible)
                _wrapper.Pane.Visible = true;
        }

        public void HidePane(object _window)
        {
            if (_window == null) throw new ArgumentNullException();

            if (GetPane(_window) is PaneWrapper wrapper)
            {
                HidePane(wrapper);
            }
        }

        public void HidePane(PaneWrapper _wrapper)
        {
            if (_wrapper == null) throw new ArgumentNullException();

            if (_wrapper.Pane.Visible)
                _wrapper.Pane.Visible = false;
        }

        public void RemovePane(object _window)
        {
            if (_window == null) throw new ArgumentNullException();

            if (GetPane(_window) is PaneWrapper wrapper)
            {
                RemovePane(wrapper);
            }
        }

        public void RemovePane(PaneWrapper _wrapper)
        {
            if (_wrapper == null) throw new ArgumentNullException();

            wrappers.Remove(_wrapper);
        }
        #endregion
    }
}