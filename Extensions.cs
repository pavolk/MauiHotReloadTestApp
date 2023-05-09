using System;

namespace FirstMarkupApp.Extensions;

public static class PickerExtensions
{
    public static Picker ItemSource<T>(this Picker picker, IEnumerable<T> enumerable)
    {
        picker.ItemsSource = enumerable.ToList();
        return picker;
    }

}
