using Client.Core;
using Client.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel.Commands
{
    class NavigateCommand : CommandBase
    {
        private NavigationStore NavigationStore { get; }
        private Func<ViewModelBase> CreateViewModel { get; }

        public NavigateCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            NavigationStore = navigationStore;
            CreateViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            NavigationStore.CurrentViewModel = CreateViewModel();
        }
    }
}
