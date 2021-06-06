using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NG.ServiceWorker.AppViews.ListUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListItemCell : ViewCell
    {
        public ListItemCell()
        {
            InitializeComponent();
        }
    }
}
