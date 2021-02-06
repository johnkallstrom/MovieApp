namespace MovieApp.Web.Models
{
    public class SelectOption
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public SelectOption()
        {
        }

        public SelectOption(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }
}
