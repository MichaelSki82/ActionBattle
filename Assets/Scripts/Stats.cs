using System;
using Unity.VisualScripting;

public static class Stats
{
    public static event Action ShowVictoryWin;

    public static int HurdLevel { get; set; } = 1;
    private static int _score = 0;
    private static bool _scoreWinPoints;

    

    public static int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            if (_score >= 100 * HurdLevel)
            {
               
               ShowVictoryWin?.Invoke();
              
                // HurdLevel++;
                // _score = 0;
            }
        }
    }

    public static void ResetAllStats()
    {
        HurdLevel = 1;
        _score = 0;
    }
}