using Microsoft.Extensions.Logging;

namespace KursayinCalculator.ViewModels;

public class TestViewModel
{
    public TestViewModel(Logger<MainPage> logger)
    {
        Logger = logger;
    }
    public Logger<MainPage> Logger { get; set; }
    public string Hello { get; set; }
}