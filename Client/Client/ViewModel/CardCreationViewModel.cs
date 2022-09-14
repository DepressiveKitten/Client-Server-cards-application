using AutoMapper;
using CardHttpClient;
using Client.Core;
using Client.Model;
using Client.Stores;
using Client.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Client.ViewModel
{
    public class CardCreationViewModel : ViewModelBase
    {
        private string cardName;
        public string CardName
        {
            get
            {
                return cardName;
            }
            set
            {
                cardName = value;
                OnPropertyChanged(nameof(cardName));
            }
        }

        private BitmapSource image;
        public BitmapSource Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public int? Id { get; set; }

        public ICommand SubmitCommand { get; }

        public ICommand CancelCommand { get; }

        public ICommand SubmitImageCommand { get; }

        public CardCreationViewModel(NavigationStore navigationStore, Func<CardListingViewModel> createCardListingViewModel, ICardHttpClient cardHttpClient, IMapper mapper)
        {
            SubmitCommand = new SubmitCardCommand(this, navigationStore, createCardListingViewModel, cardHttpClient, mapper);

            CancelCommand = new NavigateCommand(navigationStore, createCardListingViewModel);

            SubmitImageCommand = new SubmitImageCommand(this);
        }
    }
}
