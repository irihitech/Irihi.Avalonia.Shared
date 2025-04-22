using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Layout;
using Xunit;

namespace Irihi.Avalonia.Shared.HeadlessTest;

public class EnumMixerExtensionTests
{
    [AvaloniaFact]
    public void EnumMixerExtension_ProvideValue_ReturnsEnumValues()
    {
        var view = new EnumMixerExtensionView();
        var window = new Window { Content = view };
        window.Show();

        Assert.Equal(CustomEnum.Option2, view.comboBox.SelectedItem);
        Assert.Equal(HorizontalAlignment.Center, view.listBox.SelectedItem);
        Assert.Empty(view.listBox2.Items);
    }
}

public enum CustomEnum
{
    Option1,
    Option2,
    Option3
}