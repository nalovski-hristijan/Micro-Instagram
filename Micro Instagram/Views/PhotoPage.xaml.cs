using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Micro_Instagram.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoPage : ContentPage
    {
        public PhotoPage(string photoUrl)
        {
            InitializeComponent();
            fullSizePhoto.Source = photoUrl; // Display the photo using its URL
        }
    }
}