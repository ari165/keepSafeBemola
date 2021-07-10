using UnityEngine.UI;
using LootLocker.Requests;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    public InputField MemberID, Score;
    public int ID;
    public Text[] score_texts;
    void Start()
    {
        init_sdk();
    }

    public void init_sdk()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
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

    public void SubmitFromField()
    {
        SubmitScore(int.Parse(Score.text));
    }
    public void SubmitScore(int score)
    {
        LootLockerSDKManager.SubmitScore(MemberID.text, score, ID, (response) =>
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
        LootLockerSDKManager.GetScoreList(ID, 6, (response) =>
        {
            Debug.Log("?");
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    Debug.Log(scores[i].rank);
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
