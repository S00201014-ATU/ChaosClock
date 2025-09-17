using System.Timers;

namespace ChaosClock;

public partial class MainPage : ContentPage
{
    private readonly System.Timers.Timer timer;

    public MainPage()
    {
        InitializeComponent();

        timer = new System.Timers.Timer(1000);
        timer.Elapsed += UpdateClock;
        timer.Start();
    }

    private void UpdateClock(object? sender, System.Timers.ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        });
    }
}