using Client.Core;
using Client.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel.Commands
{
    class EditNavigationCommand : CommandBase
    {
        private NavigationStore NavigationStore { get; }
        private Func<ViewModelBase> CreateViewModel { get; }

        CardViewModel SelectedCard { get; }

        public EditNavigationCommand(NavigationStore navigationStore, Func<ViewModelBase> createViewModel, CardViewModel selectedCard)
        {
            NavigationStore = navigationStore;
            CreateViewModel = createViewModel;
            SelectedCard = selectedCard;
        }

        public override void Execute(object parameter)
        {
            if(SelectedCard?.Id < 0)
            {
                MessageBox.Show("No items are chosen");
                return;
            }
            NavigationStore.CurrentViewModel = CreateViewModel();
            ((CardCreationViewModel)NavigationStore.CurrentViewModel).Id = SelectedCard.Id;
            ((CardCreationViewModel)NavigationStore.CurrentViewModel).Image = SelectedCard.BitmapImage;
            ((CardCreationViewModel)NavigationStore.CurrentViewModel).CardName = SelectedCard.Name;
        }
    }
}
