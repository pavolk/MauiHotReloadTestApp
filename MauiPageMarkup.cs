

using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;



namespace FirstMarkupApp
{

    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name;


    }

    public class MainPageMarkup : ContentPage
    {
        private readonly MainViewModel ViewModel = new();

        private enum Row { TextEntry }
        private enum Column { Description, Input }

        public MainPageMarkup()
        { }

        void Build() => Content = new Grid {

                RowDefinitions = Rows.Define((Row.TextEntry, 36)),

                ColumnDefinitions = Columns.Define(
                    (Column.Description, Star),
                    (Column.Input, Stars(2))),

                Children =
                {
                    new Label()
                        .Row(Row.TextEntry)
                        .Column(Column.Description)
                        .Text("Customer Name:")
                        .CenterVertical(),


                    new Entry()
                        .Row(Row.TextEntry)
                        .Column(Column.Input)
                        .Placeholder("Enter name")
                        .FontSize(15)
                        .TextColor(Colors.Black)
                        .Bind(Entry.TextProperty, "Name", BindingMode.TwoWay)
                }

            }.Padding(10, 10);

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            Build();

#if DEBUG
            HotReloadService.UpdateApplicationEvent += ReloadUI;
#endif
        }

        private void ReloadUI(Type[] obj)
        {
            MainThread.BeginInvokeOnMainThread(() => {
                Build();
            });
        }
    }

}
