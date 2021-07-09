using UnityEngine;

[System.Serializable]
public class OwnedData
{
    public int[] ownedPlayers;
    public int currentPlayer;
    public int[] customColor;

    public OwnedData(int[] ownedIDs,int currentID, Color32 color)
    {
        ownedPlayers = ownedIDs;
        currentPlayer = currentID;
        customColor = new int[3]
        {
            color.r,
            color.g,
            color.b
        };
    }
}
