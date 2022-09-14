using CardHttpClient;
using Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.ViewModel.Commands
{
    class DeleteCommand:CommandBase
    {
        CardViewModel SelectedCard { get; }

        private ICardHttpClient CardHttpClient { get; }

        public DeleteCommand(CardViewModel selectedCard, ICardHttpClient cardHttpClient)
        {
            CardHttpClient = cardHttpClient;
            SelectedCard = selectedCard;
        }

        public override void Execute(object parameter)
        {
            if (SelectedCard?.Id < 0)
            {
                MessageBox.Show("No items are chosen");
                return;
            }

            var isSucced = CardHttpClient.DeleteCardAsync(SelectedCard.Id);

            _ = isSucced.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MessageBox.Show(task.Exception.InnerException.Message);
                }

                MessageBox.Show(task.Result ? "Card was deleted" : "Failed to delete card");
            });
        }
    }
}
