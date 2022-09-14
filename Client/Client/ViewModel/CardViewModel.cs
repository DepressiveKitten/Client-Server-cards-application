using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Client.Core;
using Client.Model;

namespace Client.ViewModel
{
    public class CardViewModel : ViewModelBase
    {
        private readonly Card card;

        public BitmapSource BitmapImage => card?.BitmapImage;

        public string Name => card?.Name;

        public int Id => card.Id;

        public CardViewModel(Card card)
        {
            this.card = card;
        }

        public void SetData(CardViewModel model)
        {
            card.Id = model.Id;
            card.Name = model.Name;
            card.BitmapImage = model.BitmapImage;
        }
    }
}
