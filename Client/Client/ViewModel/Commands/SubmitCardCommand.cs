using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardHttpClient;
using Client.Core;
using Client.Model;
using Client.Stores;
using AutoMapper;
using System.Windows;

namespace Client.ViewModel.Commands
{
    public class SubmitCardCommand : CommandBase
    {
        private NavigationStore NavigationStore { get; }

        private Func<ViewModelBase> CreateViewModel { get; }

        private CardCreationViewModel CardCreationViewModel { get; }

        private ICardHttpClient CardHttpClient { get; }

        private IMapper Mapper { get; }

        public SubmitCardCommand(CardCreationViewModel cardCreationViewModel, NavigationStore navigationStore, Func<ViewModelBase> createViewModel, ICardHttpClient cardHttpClient, IMapper mapper)
        {
            CardCreationViewModel = cardCreationViewModel;
            NavigationStore = navigationStore;
            CreateViewModel = createViewModel;
            CardHttpClient = cardHttpClient;
            Mapper = mapper;

            CardCreationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }


        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(CardCreationViewModel.CardName) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public async override void Execute(object parameter)
        {
            Card card = new Card()
            {
                BitmapImage = CardCreationViewModel.Image,
                Name = CardCreationViewModel.CardName,
            };

            Task<bool> isSucced;

            if (CardCreationViewModel.Id is null)
            {
                isSucced = CardHttpClient.CreateCardAsync(Mapper.Map<Card,HttpCard>(card));
            }
            else
            {
                isSucced = CardHttpClient.UpdateCardAsync((int)CardCreationViewModel.Id,Mapper.Map<Card, HttpCard>(card));
            }

            NavigationStore.CurrentViewModel = CreateViewModel();

            _ = isSucced.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show(task.Exception.InnerException.Message);
                }

                MessageBox.Show(task.Result ? "New Card was added" : "Failed to add new card");
            });       
        }
    }
}