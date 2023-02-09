using KursayinCalculator.Pages;
using Microsoft.Extensions.Logging;

namespace KursayinCalculator;

public partial class MainPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time ha ha";
        else
            CounterBtn.Text = $"Clicked {count} times lol";
        
        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void KeypadBtn_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new KeypadPage());
    }
}