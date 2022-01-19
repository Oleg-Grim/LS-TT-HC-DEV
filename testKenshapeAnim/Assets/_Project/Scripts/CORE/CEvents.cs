using System;

public static class CEvents
{
    public static event Action<string> OnTimerChanged;
    public static void FireTimerChanged(string text)
    {
        OnTimerChanged?.Invoke(text);
    }

    public static event Action OnEnemyKilled;
    public static void FireEnemyKilled()
    {
        OnEnemyKilled?.Invoke();    
    }
    
    public static event Action<int> OnLoseGame;
    public static void FireLoseGame(int level)
    {
        OnLoseGame?.Invoke(level);
    }
    
    public static event Action<int> OnWinGame;
    public static void FireWinGame(int level)
    {
        OnWinGame?.Invoke(level);
    }
}