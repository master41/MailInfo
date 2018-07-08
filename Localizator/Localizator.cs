using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localizator
{
    public class Lozalizator
    {
        #region Lazy Singleton
        private static Lozalizator instance;
        internal static Lozalizator GetInstance() => instance ?? (instance = new Lozalizator());
        #endregion
    }
}
