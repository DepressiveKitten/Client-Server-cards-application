using Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Stores
{
    public interface INavigationStore
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
