namespace ConsoleApp2;

/// <summary>
/// Provides functionality to generate random DateTime values within a specified range.
/// </summary>
public class RandomDateTime
{
    private DateTime _start;
    private Random _random;
    private int _range;

    public RandomDateTime()
    {
        _start = new DateTime(2000, 1, 1);
        _random = new Random();
        _range = (DateTime.Today - _start).Days;
    }

    public DateTime Next() 
        => _start
            .AddDays(_random.Next(_range))
            .AddHours(_random.Next(0, 24))
            .AddMinutes(_random.Next(0, 60))
            .AddSeconds(_random.Next(0, 60));
}