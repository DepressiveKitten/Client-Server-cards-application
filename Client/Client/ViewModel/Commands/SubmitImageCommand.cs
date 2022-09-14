using Client.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Client.ViewModel.Commands
{
    class SubmitImageCommand : CommandBase
    {

        private CardCreationViewModel CardCreationViewModel { get; }

        public SubmitImageCommand(CardCreationViewModel cardCreationViewModel)
        {
            CardCreationViewModel = cardCreationViewModel;
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            dialog.FilterIndex = 1;
            if(dialog.ShowDialog() is true)
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(dialog.FileName);
                image.EndInit();
                CardCreationViewModel.Image = image;
            }
        }
    }
}
