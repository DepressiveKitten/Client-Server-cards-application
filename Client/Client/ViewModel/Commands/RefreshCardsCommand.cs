using AutoMapper;
using CardHttpClient;
using Client.Core;
using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel.Commands
{
    public class RefreshCardsCommand : CommandBase
    {
        private CardListingViewModel ListingViewModel { get; }

        private ICardHttpClient CardHttpClient { get; }

        private IMapper Mapper { get; }

        public RefreshCardsCommand(CardListingViewModel listingViewModel, ICardHttpClient cardHttpClient, IMapper mapper)
        {
            ListingViewModel = listingViewModel;
            CardHttpClient = cardHttpClient;
            Mapper = mapper;
        }

        public override void Execute(object parameter)
        {
            Task<IEnumerable<HttpCard>> cards = CardHttpClient.GetCardsAsync();

            var uiContext = SynchronizationContext.Current;

            _ = cards.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show(task.Exception.InnerException.Message);
                }
                App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    ListingViewModel.Cards.Clear();
                });

                foreach (var card in cards.Result)
                {
                    CardViewModel cardModel = new CardViewModel(Mapper.Map<HttpCard, Card>(card));
                    cardModel.BitmapImage?.Freeze();
                    App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        ListingViewModel.Cards.Add(cardModel);
                    });
                }
            }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show(task.Exception.InnerException.Message);
                }
            });
        }
    }
}
