using System.Windows;
using System.Windows.Controls;

namespace Werwolf.CodeInputField;

public partial class CodeInputView
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(CodeInputViewModel), typeof(CodeInputView),
        new PropertyMetadata(default(CodeInputViewModel)));

    public CodeInputView()
    {
        InitializeComponent();
    }

    private void TextChanged(object sender, TextChangedEventArgs e)
    {
        var text = InputTextBox.Text;
        var inputText = text.Replace(" ", "");
        if (inputText == "" || inputText.Contains(" ") || inputText.Length != 6)
        {
            ViewModel.JoinButtonIsEnabled = false;
        }
        else
        {
            ViewModel.JoinButtonIsEnabled = true;
        }
    }

    private void SaveCode(object sender, RoutedEventArgs e)
    {
        ViewModel.RoomCode = InputTextBox.Text;
    }

    public CodeInputViewModel ViewModel
    {
        get => (CodeInputViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
}