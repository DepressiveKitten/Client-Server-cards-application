using AutoMapper;
using CardHttpClient;
using CardJsonDeserialization;
using Client.Model;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<HttpCard,JsonCard>();
            this.CreateMap<JsonCard, HttpCard>();
            this.CreateMap<HttpCard, Card>()
                .ForMember(dest => dest.BitmapImage,
                opt => opt.MapFrom(src => Base64StringToBitmap(src.Image)));
            this.CreateMap<Card, HttpCard>()
                .ForMember(dest => dest.Image,
                opt => opt.MapFrom(src => Convert.ToBase64String(GetBytesFromBitmapSource(src.BitmapImage))));
        }

        static byte[] GetBytesFromBitmapSource(BitmapSource bmp)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        public static BitmapSource Base64StringToBitmap(string source)
        {
            if(string.IsNullOrEmpty(source))
            {
                return null;
            }
            using (var ms = new System.IO.MemoryStream(Convert.FromBase64String(source)))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
