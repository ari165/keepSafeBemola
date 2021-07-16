using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour
{

    public SceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger.gameObject.SetActive(true);
    }

    public void Resume()
    {
        GameObject.Find("PauseScreen").SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GoToMainMenu()
    {
        sceneChanger.LoadScene("StartMenu");
        Time.timeScale = 1.0f;
    }
    public void GoToTutorialP1() {
        sceneChanger.LoadScene("tutorial p1");
        Time.timeScale = 1.0f;
    }
    public void GoToTutorialP2() {
        sceneChanger.LoadScene("tutorial p2");
        Time.timeScale = 1.0f;
    }
    public void GoToTutorialP3() {
        sceneChanger.LoadScene("tutorial p3");
        Time.timeScale = 1.0f;
    }

    
    public void Restart()
    {
        sceneChanger.LoadScene("main");
        Time.timeScale = 1.0f;
        
    }

    public void GoToAbout()
    {
        sceneChanger.LoadScene("about");
        Time.timeScale = 1.0f;
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void GoToShop()
    {
        sceneChanger.LoadScene("Shop");
    }

    public void SetDifficulty()
    {
        EventSystem eventSystem = EventSystem.current;
        Text text = eventSystem.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>();
        int dif = PlayerPrefs.GetInt("difficulty");
        dif++;
        dif = dif > 4 ? 0 : dif;
        PlayerPrefs.SetInt("difficulty", dif);
        text.text = dif switch
        {
            0 => "very easy",
            1 => "easy",
            2 => "normal",
            3 => "hard",
            4 => "very hard",
            _ => text.text
        };
    }

    public void BuyPlayer()
    {
        AudioSource success = GameObject.Find("Audio_success").GetComponent<AudioSource>();
        AudioSource fail = GameObject.Find("Audio_fail").GetComponent<AudioSource>();
        
        // get the current event system, if there is no event system, somethings wrong
        EventSystem currentEventSystem = EventSystem.current;
        if(currentEventSystem == null) { return; }
        
        // get the clicked button's name and convert it to int by removing all the letters, after that, we have the id 
        String btnName = currentEventSystem.currentSelectedGameObject.name;
        String id = btnName.Trim(new char[] {'P', 'l', 'a', 'y', 'e', 'r', '_'});
        
        // get the cost of the item by using the text component
        int cost = Convert.ToInt32(currentEventSystem.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
        
        // load coins data from the custom made save system
        StatsData data = SaveSystem.LoadStats();

        if (data != null)
        {
            // load the array that has all the owned players
            OwnedData owned = SaveSystem.LoadOwned();
            // if owned is null, it means that there is no save file, so add the default skin and the selected one (if there is enough money)
            if (owned == null)
            {
                if (data.coins < cost)
                {
                    SaveSystem.SaveOwned(new int[] {0}, 0, new Color32(0, 0, 0, 255));
                    fail.Play();
                }
                else
                {
                    // save everything to a custom binary file
                    SaveSystem.SaveStats(data.coins - cost, data.highScore);
                    SaveSystem.SaveOwned(new int[]{Convert.ToInt32(id), 0}, Convert.ToInt32(id), new Color32(0, 0, 0, 255));
                    success.Play();
                }
            }

            else
            {       
                Color32 color = new Color32(Convert.ToByte(owned.customColor[0]), 
                    Convert.ToByte(owned.customColor[1]),
                    Convert.ToByte(owned.customColor[2]), 255);
                // check to see if the player already has the item
                bool hasIt = owned.ownedPlayers.Any(val => val == Convert.ToInt32(id));
                
                // if player has it, equip it and use the old array because nothing was bought
                if (hasIt)
                {
                    SaveSystem.SaveOwned(owned.ownedPlayers, Convert.ToInt32(id), color);
                }
                
                // if player doesnt have it, buy it if you can
                else
                {
                    if (data.coins < cost)
                    {
                        fail.Play();
                    }
                    else
                    {
                        SaveSystem.SaveStats(data.coins - cost, data.highScore);
                        SaveSystem.SaveOwned(Util.Merge(owned.ownedPlayers, new int[]{Convert.ToInt32(id)}), Convert.ToInt32(id), color);
                        success.Play();
                    }
                }
            }
            // update ui
            currentEventSystem.GetComponent<ShopUI>().updateUI();
        }
    }

    public void Customize()
    {
        sceneChanger.LoadScene("Customize");
        Time.timeScale = 1.0f;
    }
}
