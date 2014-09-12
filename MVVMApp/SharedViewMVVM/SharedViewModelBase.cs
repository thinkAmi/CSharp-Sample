using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.TeamFoundation.MVVM;    // ViewModelBase
using System.Collections.ObjectModel;   // ObservableCollection

namespace SharedViewMVVM
{
    public class SharedViewModelBase : ViewModelBase
    {
        public ObservableCollection<Slip> SlipSource { get; set; }

        // 継承先のクラスで実装
        public virtual string WindowTitle { get { throw new System.NotImplementedException(); } }
        public virtual string QuantityTitle { get { throw new System.NotImplementedException(); } }
    }
}
