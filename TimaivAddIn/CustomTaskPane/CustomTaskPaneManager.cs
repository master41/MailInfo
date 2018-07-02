using System.Collections.Generic;
using System;
using System.Linq;
using static TimaivAddIn.Constants;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using Tools = Microsoft.Office.Tools;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using MVVM;
using static TimaivAddIn.Utils.DebugUtils;
using TimaivAddIn.Interfaces;

namespace TimaivAddIn.CustomTaskPane
{
    class CustomTaskPaneManager
    {
        #region Lazy Singleton
        private static CustomTaskPaneManager instance;
        internal static CustomTaskPaneManager GetInstance() => instance ?? (instance = new CustomTaskPaneManager());
        #endregion

        #region Consturctor
        private CustomTaskPaneManager() { }
        #endregion

        #region Private Members
        private readonly List<PaneWrapper> wrappers = new List<PaneWrapper>();
        #endregion

        #region Property
        private Tools.CustomTaskPaneCollection CustomTaskPanes => Globals.ThisAddIn.CustomTaskPanes;
        #endregion

        #region Methods
        private PaneWrapper CreatePane(object _window)
        {
            try
            {
                var pane = CustomTaskPanes.Add(new CustomTaskPaneForm(), APP_NAME, _window);
                pane.Width = PANE_INITIAL_WIDTH;
                pane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                pane.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
                return new PaneWrapper(_window, null, pane);
            }
            catch (COMException) { }
            catch (ObjectDisposedException) { }

            return null;
        }

        public void InitPane<T>(object _window,
                                bool _createNew,
                                out bool _isNew,
                                Action<ViewModelBase> _callback = null) where T : UserControl
        {
            if (_window == null) throw new ArgumentNullException();

            _isNew = false;

            if (_window is Outlook.Inspector || _window is Outlook.Explorer)
            {
                ViewModelBase viewModel = null;
                PaneWrapper wrapper = GetPane(_window);

                if (wrapper == null || _createNew)
                {
                    wrapper = CreatePane(_window);
                    wrapper.ViewModel = GetViewModel<T>();
                    _isNew = true;
                    _callback?.Invoke(viewModel);
                }

                ShowPane(wrapper);
            }
            else Stop();
        }

        private ViewModelBase GetViewModel<T>() where T : UserControl
        {
            return null;
        }

        public void InitPane<T>(object _window) where T : UserControl
        {
            InitPane<T>(_window, false, out bool _);
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

        private void LocalizePanes()
        {
            foreach (var wrapper in wrappers)
            {
                if (wrapper.ViewModel is ILocalizable localizePane) localizePane.Localize();
            }
        }
        #endregion
    }
}
