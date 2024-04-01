using Micro_Instagram.Models;
using Micro_Instagram.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Micro_Instagram.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        private Photo photo;
        private ApiService apiService = new ApiService();
        private bool isInEditMode = false;

        public DetailPage(Photo photo)
        {
            InitializeComponent();
            this.photo = photo;
            BindPhotoDetails();
        }

        private void BindPhotoDetails()
        {
            photoImage.Source = photo.Url;
            titleLabel.Text = photo.Title;
            titleInput.Text = photo.Title;
        }

        private void OnPhotoTapped(object sender, EventArgs e)
        {
           
            Navigation.PushAsync(new PhotoPage(photo.Url));
        }

        private void EditButtonTapped(object sender, System.EventArgs e)
        {
            isInEditMode = !isInEditMode;

            if (isInEditMode)
            {
              
                titleLabel.IsVisible = false;
                titleInput.IsVisible = true;
                saveButton.IsVisible = true;
                editPhoto.IsVisible = true;
            }
            else
            {
                
                titleInput.Text = photo.Title; 
                titleLabel.IsVisible = true;
                titleInput.IsVisible = false;
                saveButton.IsVisible = false;
                editPhoto.IsVisible = false;
            }
        }

        private async void SaveButtonTapped(object sender, System.EventArgs e)
        {
            photo.Title = titleInput.Text;
            titleLabel.Text = photo.Title;

            var updateSuccess = await apiService.UpdatePhotoAsync(photo);
            if (updateSuccess)
            {
                await Navigation.PopAsync();
                MessagingCenter.Send<DetailPage, Photo>(this, "UpdatePhoto", photo);
            }
            else
            {
                await DisplayAlert("Error", "Failed to update the photo.", "OK");
            }

            titleLabel.IsVisible = true;
            titleInput.IsVisible = false;
            saveButton.IsVisible = false;
            editPhoto.IsVisible = false;
        }



        private async void DeleteButtonTapped(object sender, System.EventArgs e)
        {
            var confirm = await DisplayAlert("Delete Photo", "Are you sure you want to delete this photo?", "Yes", "No");
            if (confirm)
            {
                var isSuccess = await apiService.DeletePhotoAsync(photo.Id);
                if (isSuccess)
                {
                    await Navigation.PopAsync();
                    MessagingCenter.Send<DetailPage, int>(this, "DeletePhoto", photo.Id);
                } 
                else
                {
                    await DisplayAlert("Error", "Failed to delete the photo.", "OK");
                }
            }
        }

        private async void OnEditPhotoTapped(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    photoImage.Source = ImageSource.FromStream(() => stream);
                    this.photo.Url = photo.FullPath;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }


    }
}
