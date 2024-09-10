namespace AISmartPasteDemo
{
    public class FeedbackFormViewModel
    {
        /// <summary>
        /// Gets or sets the feedback form model.
        /// </summary>
        public FeedbackForm FeedbackForm { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackFormViewModel"/> class.
        /// </summary>
        public FeedbackFormViewModel()
        {
            this.FeedbackForm = new FeedbackForm();
        }
    }
}