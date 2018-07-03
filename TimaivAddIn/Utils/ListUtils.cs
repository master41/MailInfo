using MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TimaivAddIn.Utils
{
    static class ListUtils
    {
        internal static ViewModelBase GetPage<T>(this ObservableCollection<ViewModelBase> _list) where T : ViewModelBase
        {            
            return FirstOfType<T>(_list);
        }

        internal static T FirstOfType<T>(this IEnumerable<object> _list) where T : class
        {
            foreach (var vm in _list)
            {
                if (vm.GetType() == typeof(T))
                    return vm as T;
            }

            return null;
        }
    }
}
