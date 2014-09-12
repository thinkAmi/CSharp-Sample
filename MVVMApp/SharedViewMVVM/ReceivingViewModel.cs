using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedViewMVVM
{
    public class ReceivingViewModel : SharedViewModelBase
    {
        public override string WindowTitle { get { return "入庫画面"; } }
        public override string QuantityTitle { get { return "入庫数"; } }
    }
}
