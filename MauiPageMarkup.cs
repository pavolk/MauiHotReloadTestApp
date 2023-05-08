

using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
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

        private enum Row { TextEntry, ListView }
        private enum Column { Description, Input }




        private enum CellColumn { Image, ArticleDetails, Amount }

        private enum ArticleDetailsRow { Header, Center, Footer }


        public class Article { 

            public string OrderNumber { get; set; }

            public string Name { get; set; }
            public double PriceEuros { get; set; }
            public string PackagintUnits { get; set; }

        }

        public class Position {

            public int Ordinal { get; set; }

            public Article Article { get; set; }

            public double Amount { get; set; }


        }

        public ObservableCollection<Position> Items { get; set; } = new ObservableCollection<Position>();

        public MainPageMarkup()
        {
            Items.Add(
                new Position() { 
                            Ordinal = 1, 
                            Article = new Article() { 
                                OrderNumber = "1234567890",
                                Name = "Milch", 
                                PriceEuros = 1.19,
                                PackagintUnits = "Tetrapack, 1,5l"
                            },
                            Amount = 10
                     });
            Items.Add(
                new Position() { 
                            Ordinal = 2, 
                            Article = new Article() { 
                                OrderNumber = "1234567890",
                                Name = "Brot", 
                                PriceEuros = 3.05,
                                PackagintUnits = "1 Piece, 1,5kg"
                            },
                            Amount = 2
                    });
            Items.Add(
                    new Position() { 
                            Ordinal = 3, 
                            Article = new Article() { 
                                OrderNumber = "1234567890",
                                Name = "Butter", 
                                PriceEuros = 2.49,
                                PackagintUnits = "1 Piece, 250g"
                            },
                            Amount = 3
                    });

        }


        void Build() => Content = new Grid {

                RowDefinitions = Rows.Define(
                    (Row.TextEntry, 36),
                    (Row.ListView, Stars(1))
                ),

                ColumnDefinitions = Columns.Define(
                    (Column.Description, Star),
                    (Column.Input, Stars(2))
                ),

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
                        .Bind(Entry.TextProperty, "Name", BindingMode.TwoWay),

                    new CollectionView()
                        .Row(Row.ListView)
                        .Column(0).ColumnSpan(2)
                        .ItemsSource(Items)
                        .ItemTemplate(new DataTemplate(() =>
                                new Grid {

                                    ColumnSpacing = 10,

                                    ColumnDefinitions = Columns.Define(
                                        (CellColumn.Image, Auto),
                                        (CellColumn.ArticleDetails, Star),
                                        (CellColumn.Amount, Auto)
                                    ),

                                    Children = {
                                        new BoxView()
                                            .Column(CellColumn.Image)
                                            .BackgroundColor(Colors.Yellow)
                                            .Center()
                                            .Size(50)
                                        ,

                                        new Grid() {

                                            RowDefinitions = Rows.Define(
                                                (ArticleDetailsRow.Header, Auto),
                                                (ArticleDetailsRow.Center, Star),
                                                (ArticleDetailsRow.Footer, Auto)
                                            ),

                                            Children = {

                                                new Label()
                                                    .Row(ArticleDetailsRow.Header)
                                                    .Bind(Label.TextProperty,
                                                        binding1: new Binding(nameof(Position.Ordinal)),
                                                        binding2: new Binding(nameof(Position.Article)),
                                                        convert: ((int o, Article a) values) => {
                                                            if (values.o == 0 || values.a == null) {
                                                                return string.Empty;
                                                            }
                                                            return string.Join(" | ", new string[] { values.o.ToString(), values.a.OrderNumber });
                                                         }
                                                    )
                                                    .Font(bold : false, size : 10)
                                                ,

                                                new Label()
                                                    .Bind(Label.TextProperty, 
                                                        static(Position p) => p.Article.Name,
                                                        convert: (string? text) => text?.ToUpperInvariant() ?? "")
                                                    .Font(bold: true, size: 16)
                                                    .Row(ArticleDetailsRow.Center)
                                                ,

                                                new Grid() {
                                                    ColumnDefinitions = Columns.Define(Star, Star),
                                                    Children = {
                                                        new Label()
                                                            .Column(0)
                                                            .Bind(Label.TextProperty, static(Position p) => p.Article.PackagintUnits)
                                                            .Font(bold : false, size : 10)
                                                            .Start()
                                                        ,
                                                        new Label()
                                                            .Column(1)
                                                            .Bind(Label.TextProperty, static(Position p) => p.Article.PriceEuros)
                                                            .Font(bold : false, size : 10)
                                                            .End()
                                                    }
                                                }.Row(ArticleDetailsRow.Footer)

                                            }

                                        }.Column(CellColumn.ArticleDetails),

                                        new Button()
                                            .IsVisible(true)
                                            .Column(CellColumn.Amount)
                                            .Bind(Button.TextProperty, nameof(Position.Amount))
                                            .BackgroundColor(Colors.LightGray)
                                            .Center()
                                        ,
                                        
                                    }
                                }.Padding(0, 10)
                            )
                        )
                        

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
