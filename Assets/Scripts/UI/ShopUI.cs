using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Text coin;
    public Image[] stats;
    public Button custom;

    private StatsData data;
    private OwnedData owned;
    // Start is called before the first frame update
    private void Start()
    {
        updateUI();
    }

    public void updateUI()
    {
        data = SaveSystem.LoadStats();
        owned = SaveSystem.LoadOwned();
        
        if (data != null)
        {
            coin.text = data.coins.ToString();
        }


        if (owned == null)
        {
            SaveSystem.SaveOwned(new int[] {0}, 0, new Color32(0, 0, 0, 255));
            owned = SaveSystem.LoadOwned();
        }

        for (int i = 0; i < stats.Length; i++)
        {   
            bool hasIt = false;
            foreach (var t in owned.ownedPlayers)
            {
                if (i == t)
                {
                    stats[i].color = Color.green;
                    hasIt = true;
                    if (i == 14)
                    {
                        custom.gameObject.SetActive(true);
                    }
                }
                
                if (i == owned.currentPlayer)
                {
                    stats[i].color = Color.yellow;
                    hasIt = true;
                }
            }

            if (!hasIt)
            {
                stats[i].color = new Color32(248, 92, 92, 255);
            }
        }
    }
}
