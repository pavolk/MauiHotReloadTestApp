namespace FirstMarkupApp
{
    public class MainPageCode : ContentPage
    {
        public MainPageCode()
        {
            Title = nameof(MainPageCode);

            Content = new VerticalStackLayout {
                Children =
                {
                    new Label
                    {
                        Text = "Some Label",
                        TextColor = Colors.Purple,
                        HorizontalOptions = LayoutOptions.Center,
  
                    },

                    new Label
                    {
                        Text = "Welcome to .NET MAUI!!!",
                        TextColor = Colors.Purple,
                        HorizontalOptions = LayoutOptions.Center,

                    },




                }
            };
        }
    }
}
