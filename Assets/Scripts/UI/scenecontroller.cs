using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenecontroller : MonoBehaviour
{
    public void to_about() {
        SceneManager.LoadScene("about");
    }
}
