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
            else if (modeIndex == 3)
            {
                ClockLabel.Text = ConvertToWords(DateTime.Now);
            }
            else if (modeIndex == 4)
            {
                ClockLabel.Text = ConvertToHex(DateTime.Now);
            }
            else if (modeIndex == 5)
            {
                ClockLabel.Text = ConvertToUnixTimestamp(DateTime.Now);
            }
            else if (modeIndex == 6)
            {
                ClockLabel.Text = ConvertToOctal(DateTime.Now);
            }
            else
            {
                ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            }
        });
    }

    private void OnSwitchModeClicked(object sender, EventArgs e)
    {
        modeIndex = (modeIndex + 1) % 7;

        ModeButton.Text = modeIndex switch
        {
            0 => "Switch to Binary",
            1 => "Switch to Roman Numerals",
            2 => "Switch to Words",
            3 => "Switch to Hexadecimal",
            4 => "Switch to Unix Timestamp",
            5 => "Switch to Octal",
            6 => "Switch to Normal",
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

    private string ConvertToWords(DateTime time)
    {
        int hour12 = time.Hour % 12 == 0 ? 12 : time.Hour % 12;

        string period = time.Hour < 12 ? "AM" : "PM";

        string hour = NumberToWords(hour12);

        string minute = time.Minute switch
        {
            0 => "o'clock",
            < 10 => "oh " + NumberToWords(time.Minute),
            _ => NumberToWords(time.Minute)
        };

        string second = time.Second switch
        {
            0 => "zero seconds",
            1 => "one second",
            _ => NumberToWords(time.Second) + " seconds"
        };

        return $"{hour} {minute} and {second} {period}";
    }

    private string ConvertToHex(DateTime time)
    {
        string hours = time.Hour.ToString("X2");
        string minutes = time.Minute.ToString("X2");
        string seconds = time.Second.ToString("X2");

        return $"{hours}:{minutes}:{seconds}";
    }

    private string ConvertToUnixTimestamp(DateTime time)
    {
        DateTimeOffset dto = new DateTimeOffset(time);
        return dto.ToUnixTimeSeconds().ToString();
    }
    private string ConvertToOctal(DateTime time)
    {
        string hours = Convert.ToString(time.Hour, 8).PadLeft(2, '0');
        string minutes = Convert.ToString(time.Minute, 8).PadLeft(2, '0');
        string seconds = Convert.ToString(time.Second, 8).PadLeft(2, '0');

        return $"{hours}:{minutes}:{seconds}";
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

    private string NumberToWords(int number)
    {
        string[] numbers =
        {
            "zero","one","two","three","four","five","six","seven","eight","nine",
            "ten","eleven","twelve","thirteen","fourteen","fifteen","sixteen","seventeen","eighteen","nineteen"
        };

        string[] tens =
        {
            "","","twenty","thirty","forty","fifty"
        };

        if (number < 20) return numbers[number];
        if (number < 60)
        {
            string ten = tens[number / 10];
            string ones = number % 10 == 0 ? "" : " " + numbers[number % 10];
            return ten + ones;
        }
        return number.ToString();
    }
}
