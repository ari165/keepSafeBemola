
[System.Serializable]
public class StatsData
{
    public int coins;
    public int highScore;
    
    public StatsData(int coin, int hScore)
    {
        coins = coin;
        highScore = hScore;
    }
}
