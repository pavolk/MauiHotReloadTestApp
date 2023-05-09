

using CommunityToolkit.Maui.Markup;
using FirstMarkupApp.Models;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;



namespace FirstMarkupApp;

public partial class MainPageMarkup
{
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
}

