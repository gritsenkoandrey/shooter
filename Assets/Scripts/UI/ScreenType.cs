namespace UI
{
    [System.Serializable]
    public enum ScreenType : byte
    {
        Game = 0,
        Win  = 1,
        Lose = 2,
        
        None = byte.MaxValue
    }
}