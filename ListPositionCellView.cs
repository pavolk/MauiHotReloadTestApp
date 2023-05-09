

using CommunityToolkit.Maui.Markup;


namespace FirstMarkupApp;

/*
public class ListPositionCellView : ViewCell 
{
    public ListPositionCellView()
    {
        View = new VerticalStackLayout() {
            Children = {
                new HorizontalStackLayout() {
                    Children = {
                        new Label().Text("Some Text").Center()
                    }
                }.Fill().End()
            }
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
*/

public class ListPositionCellView : TextCell 
{
    public ListPositionCellView() 
    {
        Text = "Some text";
        Detail = "Some detail";
    }
}