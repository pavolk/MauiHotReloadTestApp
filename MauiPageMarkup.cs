

using CommunityToolkit.Maui.Markup;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

using FirstMarkupApp.Models;
using static FirstMarkupApp.Extensions.PickerExtensions;

namespace FirstMarkupApp
{

    // TODO: 
    // * incremental loading
    // * pull-to-refresh
    // * grouping (article -> cagetory or article -> manufacturer)
    // * add shell-support
    //   * navigation: deep linking
    //   * search-handler


    public partial class MainPageMarkup : ContentPage
    {
        private enum Row { MerchantCategorySelection, ListView }

        private enum CellColumn { Image, ArticleDetails, Amount }

        private enum CellRow { Header, Center, Footer }


        public IEnumerable<string> Merchants
        {
            get => new List<string>() {
                "EDEKA",
                "Metro",
                "Merchang 3",
                "Merchang 4",
                "Merchang 5"
            };
        }

        public IEnumerable<string> Categories 
        { 
            get => new List<string>() {
                "Backwaren",
                "Milchprodukte",
                "Category 3",
                "Category 4",
                "Category 5"
            };
        }


        public IEnumerable<ListPosition> Items { get => GetPositions(); }

        IEnumerable<ListPosition> GetPositions() 
        {
            yield return new ListPosition() {
                Ordinal = 1,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Milch",
                    PriceEuros = 1.19,
                    PackagintUnits = "Tetrapack, 1,5l"
                },
                Amount = 10
            };
            yield return new ListPosition() {
                Ordinal = 2,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Brot",
                    PriceEuros = 3.05,
                    PackagintUnits = "1 Piece, 1,5kg"
                },
                Amount = 2
            };
            yield return new ListPosition() {
                Ordinal = 3,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 3
            };
            yield return new ListPosition() {
                Ordinal = 4,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 0
            };
            yield return new ListPosition() {
                Ordinal = 1,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Milch",
                    PriceEuros = 1.19,
                    PackagintUnits = "Tetrapack, 1,5l"
                },
                Amount = 10
            };
            yield return new ListPosition() {
                Ordinal = 2,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Brot",
                    PriceEuros = 3.05,
                    PackagintUnits = "1 Piece, 1,5kg"
                },
                Amount = 2
            };
            yield return new ListPosition() {
                Ordinal = 3,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 3
            };
            yield return new ListPosition() {
                Ordinal = 4,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 0
            };
            yield return new ListPosition() {
                Ordinal = 1,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Milch",
                    PriceEuros = 1.19,
                    PackagintUnits = "Tetrapack, 1,5l"
                },
                Amount = 10
            };
            yield return new ListPosition() {
                Ordinal = 2,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Brot",
                    PriceEuros = 3.05,
                    PackagintUnits = "1 Piece, 1,5kg"
                },
                Amount = 2
            };
            yield return new ListPosition() {
                Ordinal = 3,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 3
            };
            yield return new ListPosition() {
                Ordinal = 4,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 0
            };
            yield return new ListPosition() {
                Ordinal = 1,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Milch",
                    PriceEuros = 1.19,
                    PackagintUnits = "Tetrapack, 1,5l"
                },
                Amount = 10
            };
            yield return new ListPosition() {
                Ordinal = 2,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Brot",
                    PriceEuros = 3.05,
                    PackagintUnits = "1 Piece, 1,5kg"
                },
                Amount = 2
            };
            yield return new ListPosition() {
                Ordinal = 3,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 3
            };
            yield return new ListPosition() {
                Ordinal = 4,
                Article = new Article() {
                    OrderNumber = "1234567890",
                    Name = "Butter",
                    PriceEuros = 2.49,
                    PackagintUnits = "1 Piece, 250g"
                },
                Amount = 0
            };
        }

        public MainPageMarkup()
        {
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
                            new Picker().ItemSource(Merchants),
                            new Picker().ItemSource(Categories)
                        }
                    }.Row(Row.MerchantCategorySelection)
                    ,

                    new CollectionView()
                        .Row(Row.ListView)
                        .ItemsSource(Items)
                        .ItemTemplate(new ListPositionDataTemplate())
                    ,
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
