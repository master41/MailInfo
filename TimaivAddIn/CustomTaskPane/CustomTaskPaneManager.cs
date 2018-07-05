using System.Collections.Generic;
using System;
using System.Linq;
using static TimaivAddIn.Constants;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using Tools = Microsoft.Office.Tools;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using MVVM;
using static TimaivAddIn.Utils.DebugUtils;
using TimaivAddIn.Interfaces;
using TimaivAddIn.UserControls;
using TimaivAddIn.ViewModels.ViewModelAbout;
using TimaivAddIn.ViewModels.ViewModelSettings;

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
        private readonly List<PaneWrapper> panes = new List<PaneWrapper>();
        #endregion

        #region Property
        private Tools.CustomTaskPaneCollection CustomTaskPanes => Globals.ThisAddIn.CustomTaskPanes;
        #endregion

        #region Methods
        private PaneWrapper CreatePane(object _window, int _id)
        {
            try
            {
                var pane = CustomTaskPanes.Add(new CustomTaskPaneForm(), APP_NAME, _window);
                pane.Width = PANE_INITIAL_WIDTH;
                pane.DockPosition = Office.MsoCTPDockPosition.msoCTPDockPositionRight;
                pane.DockPositionRestrict = Office.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNoChange;
                return new PaneWrapper(_window, null, pane, id);
            }
            catch (COMException) { }
            catch (ObjectDisposedException) { }

            return null;
        }

        public PaneWrapper InitPane<T>(object _window,
                                       bool _createNew,
                                       out bool _isNew,
                                       Action<ViewModelBase> _callback = null,
                                       int _id = 0) where T : UserControl, new()
        {
            if (_window == null) throw new ArgumentNullException();


            bool isPaneIsNew = false;
            PaneWrapper wrapper = GetPane(_window);

            if (!_createNew && wrapper != null && (wrapper.Pane.Control as CustomTaskPaneForm).ElementHost.Child.GetType() == typeof(T) && wrapper.Id == _id)
            {
                _isNew = false;
                return wrapper;
            }
            else
            {
                wrapper = CreatePane(_window, _id);
                isPaneIsNew = true;
            }

            var paneWrapperCached = _createNew ? null : panes.FirstOrDefault(i => (i.Pane.Control as CustomTaskPaneForm).ElementHost.Child.GetType() == typeof(T) && i.Id == _id);

            if (paneWrapperCached != null)
            {
                wrapper.ViewModel = paneWrapperCached.ViewModel;
                _isNew = false;
            }
            else
            {
                _isNew = true;
                wrapper.ViewModel = CreateViewModel<T>();
                _callback?.Invoke(wrapper.ViewModel);
            }

            UserControl uc = new T()
            {
                DataContext = wrapper.ViewModel
            };

            (wrapper.Pane.Control as CustomTaskPaneForm).ElementHost.Child = uc;

            if (isPaneIsNew)
                panes.Add(wrapper);

            return wrapper;
        }

        private ViewModelBase CreateViewModel<T>() where T : UserControl
        {
            Type type = typeof(T);
            ViewModelBase vm = null;

            if (type == typeof(UserControlAbout))
            {
                vm = new ViewModelAbout();
            }
            else if (type == typeof(UserControlSettings))
            {
                vm = new ViewModelSettings();
            }
            else Stop();

            return vm;
        }

        public void InitPane<T>(object _window) where T : UserControl, new()
        {
            InitPane<T>(_window, false, out bool _);
        }

        public PaneWrapper GetPane(object _window)
        {
            if (_window == null) throw new ArgumentNullException();

            return panes.FirstOrDefault(i => i.Window == _window);
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

            panes.Remove(_wrapper);
        }

        private void LocalizePanes()
        {
            foreach (var wrapper in panes)
            {
                if (wrapper.ViewModel is ILocalizable localizePane) localizePane.Localize();
            }
        }
        #endregion
    }
}
