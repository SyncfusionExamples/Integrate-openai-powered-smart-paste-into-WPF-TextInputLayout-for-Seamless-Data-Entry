namespace AISmartPasteDemo
{
    using System.Text.RegularExpressions;
    using System.Windows;
    using Newtonsoft.Json;
    using Syncfusion.SfSkinManager;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string clipboardText;
        SemanticKernelService semanticKernelService = new SemanticKernelService();
        readonly string mailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
            @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public MainWindow()
        {
            InitializeComponent();
            SfSkinManager.SetTheme(this, new Theme() { ThemeName = "Material3Light" });
        }

        private async void OnSmartPasteButtonClicked(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                this.clipboardText = Clipboard.GetText();
            }

            if (string.IsNullOrEmpty(this.clipboardText))
            {
                MessageBox.Show("No text copied to clipboard. Please copy the text and try again", "Information", MessageBoxButton.OK);
                return;
            }

            string dataFormJsonData = JsonConvert.SerializeObject(this.feedbackFormViewModel.FeedbackForm);
            string prompt = $"Merge the copied data into the DataForm field content, ensuring that the copied text matches suitable field names. Here are the details:" +
            $"\n\nCopied data: {this.clipboardText}," +
            $"\nDataForm Field Name: {dataFormJsonData}," +
            $"\nProvide the resultant DataForm directly." +
            $"\n\nConditions to follow:" +
            $"\n1. Do not use the copied text directly as the field name; merge appropriately." +
            $"\n2. Ignore case sensitivity when comparing copied text and field names." +
            $"\n3. Final output must be Json format" +
            $"\n4. No need any explanation or comments in the output" +
            $"\n Please provide the valid JSON object without any additional formatting characters like backticks or newlines";
            string finalResponse = await this.semanticKernelService.GetResponseFromGPT(prompt);
            this.ProcessSmartPasteData(finalResponse);
        }

        private void OnSubmitButtonClicked(object sender, RoutedEventArgs e)
        {
            if (this.ValidateAllFields())
            {
                MessageBox.Show("Feedback form submitted successfully", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Please enter the required details", "Error", MessageBoxButton.OK);
            }
        }

        private bool ValidateAllFields()
        {
            bool isValidForm = true;
            FeedbackForm feedbackForm = this.feedbackFormViewModel.FeedbackForm;

            this.nameTextInputLayout.HasError = false;
            this.emailTextInputLayout.HasError = false;
            this.productNameTextInputLayout.HasError = false;
            this.prodectVersionTextInputLayout.HasError = false;
            this.ratingTextInputLayout.HasError = false;
            this.commentsTextInputLayout.HasError = false;

            //// Name Validation.
            if (string.IsNullOrEmpty(feedbackForm.Name))
            {
                isValidForm = false;
                this.nameTextInputLayout.HasError = true;
                this.nameTextInputLayout.ErrorText = "Enter your name";
            }
            else if (feedbackForm.Name.Length > 20)
            {
                isValidForm = false;
                this.nameTextInputLayout.HasError = true;
                this.nameTextInputLayout.ErrorText = "Name cannot exceed 20 characters";
            }

            //// Email Validation.
            if (string.IsNullOrEmpty(feedbackForm.Email))
            {
                isValidForm = false;
                this.emailTextInputLayout.HasError = true;
                this.emailTextInputLayout.ErrorText = "Enter your email";
            }
            else if (!Regex.IsMatch(this.feedbackFormViewModel.FeedbackForm.Email, this.mailPattern))
            {
                isValidForm = false;
                this.emailTextInputLayout.HasError = true;
                this.emailTextInputLayout.ErrorText = "Please enter a valid email address.";
            }

            //// Product Name Validation.
            if (string.IsNullOrEmpty(feedbackForm.ProductName))
            {
                isValidForm = false;
                this.productNameTextInputLayout.HasError = true;
                this.productNameTextInputLayout.ErrorText = "Enter product name";
            }
            else if (feedbackForm.Name.Length > 20)
            {
                isValidForm = false;
                this.productNameTextInputLayout.HasError = true;
                this.productNameTextInputLayout.ErrorText = "Product name cannot exceed 20 characters";
            }

            //// Product Version Validation.
            if (string.IsNullOrEmpty(feedbackForm.ProductVersion))
            {
                isValidForm = false;
                this.prodectVersionTextInputLayout.HasError = true;
                this.prodectVersionTextInputLayout.ErrorText = "Enter product version";
            }

            //// Rating Validation.
            if (string.IsNullOrEmpty(feedbackForm.Rating))
            {
                isValidForm = false;
                this.ratingTextInputLayout.HasError = true;
                this.ratingTextInputLayout.ErrorText = "Enter rating";
            }
            else if (!double.TryParse(feedbackForm.Rating, out double rating))
            {
                isValidForm = false;
                this.ratingTextInputLayout.HasError = true;
                this.ratingTextInputLayout.ErrorText = "Please enter a valid rating";
            }
            else if (rating < 1 || rating > 5)
            {
                isValidForm = false;
                this.ratingTextInputLayout.HasError = true;
                this.ratingTextInputLayout.ErrorText = "Rating should be between 1 and 5";
            }

            //// Comments Validation.
            if (string.IsNullOrEmpty(feedbackForm.Comments))
            {
                isValidForm = false;
                this.commentsTextInputLayout.HasError = true;
                this.commentsTextInputLayout.ErrorText = "Enter your comments";
            }

            return isValidForm;
        }

        private void ProcessSmartPasteData(string response)
        {
            //// Deserialize the JSON string to a Dictionary
            var openAIJsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);

            //// Create lists to hold field names and values
            var filedNames = new List<string>();
            var fieldValues = new List<string>();

            foreach (var data in openAIJsonData)
            {
                filedNames.Add(data.Key);
                fieldValues.Add(data.Value?.ToString() ?? string.Empty);
            }

            this.feedbackFormViewModel.FeedbackForm.Name = fieldValues[0];
            this.feedbackFormViewModel.FeedbackForm.Email = fieldValues[1];
            this.feedbackFormViewModel.FeedbackForm.ProductName = fieldValues[2];
            this.feedbackFormViewModel.FeedbackForm.ProductVersion = fieldValues[3];
            this.feedbackFormViewModel.FeedbackForm.Rating = fieldValues[4];
            this.feedbackFormViewModel.FeedbackForm.Comments = fieldValues[5];
        }
    }
}