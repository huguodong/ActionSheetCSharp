using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActionSheetCSharp
{
    public interface IActionSheetListener
    {
        void OnDismiss(ActionSheet actionSheet);
    }
}
