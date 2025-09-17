using System.Timers;

namespace ChaosClock;

public partial class MainPage : ContentPage
{
    private readonly System.Timers.Timer timer;
    private int modeIndex = 0;

    public MainPage()
    {
        InitializeComponent();

        timer = new System.Timers.Timer(1000);
        timer.Elapsed += UpdateClock;
        timer.Start();
    }

    private void UpdateClock(object? sender, ElapsedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (modeIndex == 1)
            {
                ClockLabel.Text = ConvertToBinary(DateTime.Now);
            }
            else if (modeIndex == 2)
            {
                ClockLabel.Text = ConvertToRomanNumerals(DateTime.Now);
            }
            else
            {
                ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        });
    }

    private void OnSwitchModeClicked(object sender, EventArgs e)
    {
        modeIndex = (modeIndex + 1) % 3;

        ModeButton.Text = modeIndex switch
        {
            0 => "Switch to Binary",
            1 => "Switch to Roman Numerals",
            2 => "Switch to Normal",
            _ => "Switch Mode"
        };
    }

    private string ConvertToBinary(DateTime time)
    {
        string hours = Convert.ToString(time.Hour, 2).PadLeft(6, '0');
        string minutes = Convert.ToString(time.Minute, 2).PadLeft(6, '0');
        string seconds = Convert.ToString(time.Second, 2).PadLeft(6, '0');

        return $"{hours}:{minutes}:{seconds}";
    }

    private string ConvertToRomanNumerals(DateTime time)
    {
        return $"{ToRoman(time.Hour)}:{ToRoman(time.Minute)}:{ToRoman(time.Second)}";
    }

    private string ToRoman(int number)
    {
        if (number == 0) return "N";

        var map = new Dictionary<int, string>
        {
            {1000, "M"}, {900, "CM"}, {500, "D"}, {400, "CD"},
            {100, "C"}, {90, "XC"}, {50, "L"}, {40, "XL"},
            {10, "X"}, {9, "IX"}, {5, "V"}, {4, "IV"},
            {1, "I"}
        };

        var roman = "";
        foreach (var kvp in map)
        {
            while (number >= kvp.Key)
            {
                roman += kvp.Value;
                number -= kvp.Key;
            }
        }
        return roman;
    }
}
