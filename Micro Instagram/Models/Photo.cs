using System.ComponentModel; // Required for INotifyPropertyChanged
using System.Runtime.CompilerServices; // Required for CallerMemberName

namespace Micro_Instagram.Models
{
    public class Photo : INotifyPropertyChanged
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged();
                }
            }
        }

        private string url;
        public string Url
        {
            get => url;
            set
            {
                if (url != value)
                {
                    url = value;
                    OnPropertyChanged();
                    ThumbnailUrl = value;
                    OnPropertyChanged(nameof(ThumbnailUrl)); 
                }
            }
        }

        private string thumbnailUrl;
        public string ThumbnailUrl
        {
            get => thumbnailUrl;
            set
            {
                if (thumbnailUrl != value)
                {
                    thumbnailUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
