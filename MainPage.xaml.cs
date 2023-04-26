

using System.Diagnostics;

namespace FirstMarkupApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		Title = nameof(MainPage);

		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		if (sender is Button button) {


			var page = button.Text.Split().LastOrDefault();
			if (page == "Code") {
				await Navigation.PushAsync(new MainPageCode());
			} else if (page == "Markup") { 
				await Navigation.PushAsync(new MainPageMarkup());
			}



		}

		
    }
}

