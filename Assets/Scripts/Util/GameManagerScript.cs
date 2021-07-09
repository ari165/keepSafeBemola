using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject pauseScreen;

    void Start()
    {
        // v-sync 1:1
        QualitySettings.vSyncCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // pause screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Time.timeScale != 0 ? 0.0f : 1.0f;
            pauseScreen.SetActive(!pauseScreen.activeSelf);
        }
    }
}
