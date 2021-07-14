using System;
using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderBoardController : MonoBehaviour
{
    public InputField MemberID;
    public int ID;
    public Text[] score_texts;
    public Text player_stats;
    public static bool init;
    
    private string player_name;
    
    void Start()
    {
        player_name = PlayerPrefs.GetString("name");

        Init_sdk();
        UpdateLeaderBoard();
        CheckPlayerStats();
        SubmitScore(100, "test");
    }

    public static void Init_sdk()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("connected");
                init = true;
            }
            else
            {
                Debug.Log("failed to connect");
            }
        });
    }

    public void CheckPlayerStats()
    {
        player_name = PlayerPrefs.GetString("name");
        if (player_name == "" || player_name == " ") return;
        SubmitScore();
        LootLockerSDKManager.GetByListOfMembers(new []{player_name}, ID, (response) =>
        {
            if (!response.success) return;
            LootLockerLeaderboardMember score = response.members[0];
            player_stats.text = (score.rank + ". " + score.member_id + "        " + score.score);
            Debug.Log(response.members[0].rank);
        });
    }
    
    public void SubmitScore(int score, String p_name)
    {
        LootLockerSDKManager.SubmitScore(p_name, score, ID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("connected");
                UpdateLeaderBoard();
            }
            else
            {
                Debug.Log("failed to connect");
            }
        });
    }
    
    public void SubmitScore()
    {
        StatsData data = SaveSystem.LoadStats();
        SubmitScore(data.highScore, PlayerPrefs.GetString("name"));
    }

    public void UpdateLeaderBoard()
    {
        if (!init)
            Init_sdk();
        LootLockerSDKManager.GetScoreList(ID, score_texts.Length, (response) =>
        {

            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    Debug.Log(scores[i].rank + ". " + scores[i].member_id + "        " + scores[i].score);
                    score_texts[i].text = " " + scores[i].rank + ". " + scores[i].member_id + "        " + scores[i].score;
                }
                
            }
            else
            {
                Debug.Log("something went wrong");
            }
        });
    }

    public void SaveName()
    {
        if (MemberID.text == "") return;
        PlayerPrefs.SetString("name", MemberID.text);
        print(PlayerPrefs.GetString("name"));
        SubmitScore();
    }
}
