namespace AISmartPasteDemo
{
    using System.ComponentModel;

    public class FeedbackForm : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private string email = string.Empty;
        private string productName = string.Empty;
        private string productVersion = string.Empty;
        private string rating = string.Empty;
        private string comments = string.Empty;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invokes the property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get => this.name;
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        public string Email
        {
            get => this.email;
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    this.OnPropertyChanged("Email");
                }
            }
        }

        public string ProductName
        {
            get => this.productName;
            set
            {
                if (this.productName != value)
                {
                    this.productName = value;
                    this.OnPropertyChanged("ProductName");
                }
            }
        }

        public string ProductVersion
        {
            get => this.productVersion;
            set
            {
                if (this.productVersion != value)
                {
                    this.productVersion = value;
                    this.OnPropertyChanged("ProductVersion");
                }
            }
        }

        public string Rating
        {
            get => this.rating;
            set
            {
                if (this.rating != value)
                {
                    this.rating = value;
                    this.OnPropertyChanged("Rating");
                }
            }
        }

        public string Comments
        {
            get => this.comments;
            set
            {
                if (this.comments != value)
                {
                    this.comments = value;
                    this.OnPropertyChanged("Comments");
                }
            }
        }
    }
}