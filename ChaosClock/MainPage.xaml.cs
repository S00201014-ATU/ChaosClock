using System.Timers;

namespace ChaosClock;

public partial class MainPage : ContentPage
{
    private readonly System.Timers.Timer timer;
    private bool binaryMode = false;

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
            ClockLabel.Text = binaryMode
            ? ConvertToBinary(DateTime.Now)
            : DateTime.Now.ToString("HH:mm:ss");
        });
    }

    private void onToggleBinaryClicked(object? sender, EventArgs e)
    {
        binaryMode = !binaryMode;
        ToggleButton.Text = binaryMode ? "Show Normal" : "Show Binary";
    }

    private string ConvertToBinary(DateTime time)
    {
        string hours = Convert.ToString(time.Hour, 2).PadLeft(6, '0');
        string minutes = Convert.ToString(time.Minute, 2).PadLeft(6, '0');
        string seconds = Convert.ToString(time.Second, 2).PadLeft(6, '0');

        return $"{hours}:{minutes}:{seconds}";
    }
}