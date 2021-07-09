using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    public float speed;
    
    public BaseController Base;
    public Text scoreText;

    public ParticleSystem destroyPS;
    public bool RandomColor;
    public bool RandomSize;
    
    public float[] sizeRange = new float[2];
    
    private float size;
    
    void OnValidate()
    {
        if (sizeRange.Length != 2)
        {
            Debug.LogWarning("Don't change the 'sizeRange' field's array size!");
            Array.Resize(ref sizeRange, 2);
        }
    }
    void Start()
    {
        if (RandomColor)
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

        // find gameObjects
        Base = GameObject.Find("Base").GetComponent<BaseController>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        target = Base.gameObject.transform;
        
        // start the movement function
        //StartCoroutine(LerpPosition(target.position, 5));

        // set rotation
        Vector2 dir = target.position - transform.position; 
        transform.right = dir;

        if (!RandomSize) return;
        size = Random.Range(sizeRange[0], sizeRange[1]);
        transform.localScale = new Vector3(size, size, size);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime * 100;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            if (Time.timeScale != 0)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += 0.01f + speed;
            }
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // score system
            Base.score += 1;
            scoreText.text = Base.score.ToString();
            
            // particle system
            GameObject p = Instantiate(destroyPS, transform.position, transform.rotation).gameObject;
            Renderer pr = p.GetComponent<Renderer>();
            pr.material.color = GetComponent<MeshRenderer>().material.color;

            Destroy(p, 1);
            
        }
        // after everything is done, destroy self
        Destroy(gameObject);
    }
}
