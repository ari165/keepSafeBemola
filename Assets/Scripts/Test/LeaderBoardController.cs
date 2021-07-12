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
    
    private string player_name;
    
    void Start()
    {
        player_name = PlayerPrefs.GetString("name");

        Init_sdk();
        //UpdateLeaderBoard();
        if (player_name != "" && player_name != " ")
        {
            SubmitScore();
            LootLockerSDKManager.GetByListOfMembers(new []{"test"}, ID, (response) =>
            {
                if (response.success)
                {
                    LootLockerLeaderboardMember score = response.members[0];
                    player_stats.text = (score.rank + ". " + score.member_id + "        " + score.score);
                    Debug.Log(response.members[0].rank);
                }
            });
        }

    }

    public static void Init_sdk()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("connected");
            }
            else
            {
                Debug.Log("failed to connect");
            }
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
        LootLockerSDKManager.SubmitScore(player_name, data.highScore, ID, (response) =>
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

    public void UpdateLeaderBoard()
    {
        LootLockerSDKManager.GetScoreList(ID, score_texts.Length, (response) =>
        {
            Debug.Log("?");
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    score_texts[i].text = (scores[i].rank + ". " + scores[i].member_id + "        " + scores[i].score);
                }
                
            }
            else
            {
                Debug.Log("something went wrong");
            }
        });
    }
}
