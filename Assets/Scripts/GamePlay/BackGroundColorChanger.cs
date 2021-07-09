using System.Collections;
using UnityEngine;

public class BackGroundColorChanger : MonoBehaviour
{
    //TODO: FIX THIS SHIT
    /* ghabli ro pack kardam to jadidesho inja benevis bruh*/
    
    public Camera cam;
    public float LerpTime;
    public float WaitTime;
    
    private Color[] colors = new Color[25];
    private float t = 0;
    private int len;
    private int index = 0;
    private bool change;
    
    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            colors[i] = new Color(
                Random.Range(0.1f, 0.8f), 
                Random.Range(0.1f, 0.8f), 
                Random.Range(0.1f, 0.8f)
            );
        }

        //StartCoroutine(ChangeColor());
    }
    
    void Update()
    {

    }

    private IEnumerator ChangeColor()
    {
        // change index every x seconds
        while (true)
        {
            index++;
            index = index >= colors.Length ? 0 : index;
            yield return new WaitForSeconds(WaitTime);
        }
    }
}
