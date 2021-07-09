using UnityEngine;
using UnityEngine.UI;

public class DifficultyChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int dif;
        dif = PlayerPrefs.GetInt("difficulty");
        transform.GetChild(0).GetComponent<Text>().text = dif switch
        {
            0 => "very easy",
            1 => "easy",
            2 => "normal",
            3 => "hard",
            4 => "very hard",
            _ => transform.GetChild(0).GetComponent<Text>().text
        };
    }
}
