

using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;



namespace FirstMarkupApp
{
    public class MainPageMarkup : ContentPage
    {
        private enum Row { MerchantCategorySelection, ListView }

        private enum CellColumn { Image, ArticleDetails, Amount }

        private enum CellRow { Header, Center, Footer }


        public class Article { 

            public string? OrderNumber { get; set; }

            public string? Name { get; set; }

            public double PriceEuros { get; set; }
            
            public string? PackagintUnits { get; set; }

        }

        public class ListPosition {

            public int Ordinal { get; set; }

            public Article? Article { get; set; }

            public double Amount { get; set; }

        }

        public IList<string> Merchants { get; set; } = new List<string>();

        public IList<string> Categories { get; set; } = new List<string>();

        public IList<ListPosition> Items { get; set; } = new List<ListPosition>();

        public MainPageMarkup()
        {
            Merchants.Add("EDEKA");
            Merchants.Add("Metro");
            Merchants.Add("Merchant 3");
            Merchants.Add("Merchant 4");
            Merchants.Add("Merchant 5");

            Categories.Add("Backwaren");
            Categories.Add("Milchprodukte");
            Categories.Add("Category 3");
            Categories.Add("Category 4");
            Categories.Add("Category 5");

            Items.Add(
                new ListPosition() { 
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
                new ListPosition() { 
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
                new ListPosition() {
                        Ordinal = 3,
                        Article = new Article() {
                            OrderNumber = "1234567890",
                            Name = "Butter",
                            PriceEuros = 2.49,
                            PackagintUnits = "1 Piece, 250g"
                        },
                        Amount = 3
                    });
            Items.Add(
                new ListPosition() {
                    Ordinal = 4,
                    Article = new Article() {
                        OrderNumber = "1234567890",
                        Name = "Butter",
                        PriceEuros = 2.49,
                        PackagintUnits = "1 Piece, 250g"
                    },
                    Amount = 0
                })
            ;

        }

        class ListPositionDataTemplate : DataTemplate {

            public ListPositionDataTemplate() : base(() => Build()) { }

            static View Build() =>
                new Grid {
                    ColumnSpacing = 10,

                    ColumnDefinitions = Columns.Define(
                        (CellColumn.Image, Auto),
                        (CellColumn.ArticleDetails, Star),
                        (CellColumn.Amount, Auto)
                    ),

                    RowDefinitions = Rows.Define(
                        (CellRow.Header, Auto),
                        (CellRow.Center, Star),
                        (CellRow.Footer, Auto)
                    ),

                    Children = {
                            // Image
                            new BoxView()
                                .Column(CellColumn.Image)
                                .RowSpan(3)
                                .BackgroundColor(Colors.Yellow)
                                .Center()
                                .Size(50)
                            ,

                            // Header
                            new Label()
                                .Column(CellColumn.ArticleDetails)
                                .Row(CellRow.Header)
                                .Bind(Label.TextProperty,
                                    binding1: new Binding(nameof(ListPosition.Ordinal)),
                                    binding2: new Binding(nameof(ListPosition.Article)),
                                    convert: ((int o, Article a) values) => {
                                        if (values.o == 0 || values.a == null) {
                                            return string.Empty;
                                        }
                                        return string.Join(" | ", new string[] { values.o.ToString(), values.a.OrderNumber });
                                        }
                                )
                                .Font(bold : false, size : 10)
                            ,

                            // Article Name
                            new Label()
                                .Column (CellColumn.ArticleDetails)
                                .Row(CellRow.Center)
                                .Bind(Label.TextProperty,
                                    static(ListPosition p) => p.Article.Name,
                                    convert: (string? text) => text?.ToUpperInvariant() ?? "")
                                .Font(bold: true, size: 16)
                            ,

                            // Footer
                            new Label()
                                .Column(CellColumn.ArticleDetails)
                                .Row(CellRow.Footer)
                                .Bind(Label.TextProperty, static(ListPosition p) => p.Article.PackagintUnits)
                                .Font(bold : false, size : 10)
                                .Start()
                            ,

                            new Label()
                                .Column(CellColumn.Amount)
                                .Row(CellRow.Footer)
                                .Bind(Label.TextProperty, static(ListPosition p) => p.Article.PriceEuros)
                                .Font(bold : false, size : 10)
                                .Center()
                            ,

                            // ShoppingList/Order/ShoppingCart "Amount" button
                            new Button()
                                .Column(CellColumn.Amount)
                                .Row(CellRow.Header).RowSpan(2)
                                .Bind(IsVisibleProperty, 
                                    static (ListPosition p) => p.Amount,
                                    convert: (double value) => value > 0.0)
                                .Bind(Button.TextProperty, nameof(ListPosition.Amount))
                                .BackgroundColor(Colors.LightGray)
                                .Center()
                            ,

                        }
                }.Padding(0, 10);

        };

        class ListPositionCellView : ViewCell {

            public ListPositionCellView()
            {
                View = new VerticalStackLayout() { 
                    Children = { new Label().Text("Some text") } 
                };
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();

                if (BindingContext != null) {
                    ;
                }
            }

        }

        void Build() => Content = new Grid {

                RowDefinitions = Rows.Define(
                    (Row.MerchantCategorySelection, Auto),
                    (Row.ListView, Stars(1))
                ),

                Children =
                {
                    new VerticalStackLayout() {
                        Children = {
                            new Picker() {
                                ItemsSource = (System.Collections.IList)Merchants
                            },
                            new Picker() {
                                ItemsSource = (System.Collections.IList)Categories
                            }
                        }
                    }.Row(Row.MerchantCategorySelection),

                    new CollectionView()
                        .Row(Row.ListView)
                        .ItemsSource(Items)
                        .ItemTemplate(new ListPositionDataTemplate())
                        //.ItemTemplate(new DataTemplate(typeof(ListPositionCellView)))

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
