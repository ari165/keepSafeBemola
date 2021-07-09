using UnityEngine;
using UnityEngine.UI;

public class LostUIController : MonoBehaviour
{
    public Text score;
    public Text highScore;
    public Text coin;
    
    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        StatsData data = SaveSystem.LoadStats();
        // update ui
        score.text = PlayerPrefs.GetFloat("score").ToString();

        if (data != null)
        {
            highScore.text = data.highScore.ToString();
            coin.text = data.coins.ToString();
        }
    }

}
