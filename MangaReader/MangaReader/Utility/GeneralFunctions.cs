using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace MangaReader.Utility {
    public class GeneralFunctions {
        public static Image ConstructImage(string path) {
            var image = new Image();

            var uri = new Uri(path, UriKind.Absolute);
            var bitmap = new BitmapImage(uri);
            image.Source = bitmap;

            return image;
        }
    }
}
