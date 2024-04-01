using Micro_Instagram.Models;
using Micro_Instagram.Services;
using Micro_Instagram.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Micro_Instagram
{
    public partial class MainPage : ContentPage
    {
        private ApiService apiService = new ApiService();
        private ObservableCollection<Photo> photos = new ObservableCollection<Photo>();

        public MainPage()
        {
            InitializeComponent();
            photosCollectionView.ItemsSource = photos;
            NavigationPage.SetHasNavigationBar(this, false);
           
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (photos.Count == 0)
            {
                await LoadItems();
            }
            MessagingCenter.Subscribe<DetailPage, int>(this, "DeletePhoto", (page, photoId) =>
            {
                var photoToDelete = photos.FirstOrDefault(photo => photo.Id == photoId);
                if (photoToDelete != null)
                {
                    Device.BeginInvokeOnMainThread(() => photos.Remove(photoToDelete));
                    DisplayAlert("Photo Deleted", "The photo has been deleted.", "OK");
                }
                
            });

            MessagingCenter.Subscribe<DetailPage, Photo>(this, "UpdatePhoto", (page, updatedPhoto) =>
            {
                var photoToUpdate = photos.FirstOrDefault(photo => photo.Id == updatedPhoto.Id);
                if (photoToUpdate != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        photoToUpdate.Title = updatedPhoto.Title;
                        photoToUpdate.Url = updatedPhoto.Url;

                        
                        photos[photos.IndexOf(photoToUpdate)] = photoToUpdate;
                        DisplayAlert("Photo Saved", "The photo has been saved.", "OK");
                    });
                  
                }
     
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<DetailPage, int>(this, "DeletePhoto");
            MessagingCenter.Unsubscribe<DetailPage, Photo>(this, "UpdatePhoto");
        }

        private async Task LoadItems()
        {
            var photos = await apiService.GetPhotosAsync();
            Device.BeginInvokeOnMainThread(() =>
            {
                this.photos.Clear();
                foreach (var photo in photos)
                {
                    this.photos.Add(photo);
                }
            });
        }


        private void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = e.CurrentSelection.FirstOrDefault() as Photo;
            if (selectedItem != null)
            {
                Navigation.PushAsync(new DetailPage(selectedItem));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
