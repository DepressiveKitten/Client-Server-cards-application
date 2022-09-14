using AutoMapper;
using CardHttpClient;
using Client.Core;
using Client.Model;
using Client.Stores;
using Client.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client.ViewModel
{
    public class CardListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<CardViewModel> cards;

        public ObservableCollection<CardViewModel> Cards => cards;

        private CardViewModel selectedItem;

        public CardViewModel SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem.SetData(value);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public CardListingViewModel(NavigationStore navigationStore, Func<CardCreationViewModel> createCardCreationViewModel, ICardHttpClient cardHttpClient, IMapper mapper)
        {
            selectedItem = new CardViewModel(new Card() {Id = -1 });

            cards = new ObservableCollection<CardViewModel>();

            DeleteCommand = new DeleteCommand(selectedItem, cardHttpClient);

            CreateCommand = new NavigateCommand(navigationStore, createCardCreationViewModel);

            RefreshCommand = new RefreshCardsCommand(this, cardHttpClient, mapper);

            EditCommand = new EditNavigationCommand(navigationStore, createCardCreationViewModel, selectedItem);

            RefreshCommand.Execute(null);
        }
    }
}
