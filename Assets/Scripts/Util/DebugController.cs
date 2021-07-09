using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
/*
 * a very flexible command system based in a tutorial
 */
public class DebugController : MonoBehaviour
{
    public bool showConsole;
    public bool showHelp;
    public GUIStyle consoleStyle;

    private string input;

    public static DebugCommand STOP_GAME;
    public static DebugCommand START_GAME;
    public static DebugCommand GOD_MODE;
    public static DebugCommand<int> SET_COINS;
    public static DebugCommand HELP;
    public static DebugCommand<int> SET_HEALTH;
    public static DebugCommand RESET_OWNED;
    public static DebugCommand RESET_STATS;
    public static DebugCommand UPDATE_UI;
    public static DebugCommand<int> ADD_COINS;

    public List<object> commandList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            showConsole = !showConsole;
        }
    }

    private void Awake()
    {
        // commands
        STOP_GAME = new DebugCommand("stop_game", "sets the timeScale to 0", "stop_game", () =>
        {
            Time.timeScale = 0.0f;
        });
        
        START_GAME = new DebugCommand("start_game", "sets the timeScale to 1", "start_game", () =>
        {
            Time.timeScale = 1.0f;
        });
        
        GOD_MODE = new DebugCommand("godmode", "makes the base invincible", "godmode", () =>
        {
            if (CheckScene("main"))
            {
                GameObject.Find("Base").GetComponent<BaseController>().godMode = true;
            }
        });
        
        SET_COINS = new DebugCommand<int>("set_coins", "sets the amount of gold", "set_coins", (x) =>
        {
            SaveSystem.SaveStats(x, SaveSystem.LoadStats().highScore);
            Update_ui();
        });
        
        HELP = new DebugCommand("help", "Shows a list of commands", "help", () =>
        {
            showHelp = !showHelp;
        });
        
        SET_HEALTH = new DebugCommand<int>("set_health", "sets the base health", "set_coins", (x) =>
        {
            if (CheckScene("main"))
            {
                GameObject.Find("Base").GetComponent<BaseController>().Health = x;
            }
        });

        RESET_OWNED = new DebugCommand("reset_owned", "resets the owned save file", "reset_owned", () =>
        {
            SaveSystem.SaveOwned(new int[]{0}, 0, new Color32(0, 0, 0, 255));
            Update_ui();
        });
        
        RESET_STATS = new DebugCommand("reset_stats", "resets the stats save file", "reset_stats", () =>
        {
            SaveSystem.SaveStats(0, 0);
            Update_ui();
        });
        
        UPDATE_UI = new DebugCommand("update_ui", "updates the ui", "update_ui", Update_ui);

        ADD_COINS = new DebugCommand<int>("add_coins", "adds coins", "add_coins", (x) =>
        {
            SaveSystem.SaveStats(SaveSystem.LoadStats().coins + x, SaveSystem.LoadStats().highScore);
            Update_ui();
        });
        

        commandList = new List<object>
        {
            STOP_GAME,
            START_GAME,
            GOD_MODE,
            SET_COINS, 
            HELP,
            SET_HEALTH,
            RESET_OWNED,
            RESET_STATS,
            UPDATE_UI,
            ADD_COINS
        };
    }

    private Vector2 scroll;
    private void OnGUI()
    {
        if (!showConsole) return;
        
        if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
        {
            if (input == "")
            {
                showConsole = false;
            }
            HandleInput();
            input = "";
        }
        float y = 0;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 110), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 35 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 100), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}";

                Rect labelRect = new Rect(5, 30 * i, viewport.width - 100, 30);

                GUI.Label(labelRect, label, consoleStyle);
            }
            
            GUI.EndScrollView();

            y += 110;
        }
        
        GUI.Box(new Rect(0, y, Screen.width, 50), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 40, 40), input, consoleStyle);

    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');
        
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.CommandId))
            {
                print($"executed <{input}> command");
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                } 
                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }
        }
    }

    private static bool CheckScene(string name)
    {
        String scene = SceneManager.GetActiveScene().name;
        return name == scene;
    }

    private void Update_ui()
    {
        if (CheckScene("Shop"))
        {
            EventSystem.current.GetComponent<ShopUI>().updateUI();
        }
        else if (CheckScene("Lost"))
        {
            EventSystem.current.GetComponent<LostUIController>().UpdateUI();
        }
    }
}
