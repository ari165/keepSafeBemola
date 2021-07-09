using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator transition;
    // Start is called before the first frame update
    public void LoadScene(String scene)
    {
        StartCoroutine(LoadLevel(scene));
    }
    
    IEnumerator LoadLevel(String scene)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(scene);
    }
}
