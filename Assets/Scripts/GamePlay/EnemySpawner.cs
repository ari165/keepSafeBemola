using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] ObjectsToSpawn;

    public float radius;
    public float radiusOffset;

    public float waitTime;
    public float waitTimeOffset;
    
    public float t;
    private float tl;
    
    public float enemySpeed;
    private float bm;
    private float bl;
    
    public float currentTime;

    private float x, y;

    // Start is called before the first frame update
    void Start()
    {
        tl = PlayerPrefs.GetInt("difficulty") switch
        {
            // 1.08 impossible, 1.09 hard, 1.1 normal, 1.11 easy, 1.12 very easy
            0 => 1.16f,
            1 => 1.14f,
            2 => 1.13f,
            3 => 1.08f,
            4 => 1.07f,
            _ => tl
        };
        
        bl = PlayerPrefs.GetInt("difficulty") switch
        {
            0 => 30f,
            1 => 25f,
            2 => 20f,
            3 => 15f,
            4 => 10f,
            _ => bl
        };
        
        StartCoroutine(spawner());
    }

    private IEnumerator spawner()
    {
        t = 0.1f;
        currentTime = waitTime;
        bm = 0.005f;
        while (true)
        {
            if (t > 0.0001)
                t /= tl;
            // increase enemy speed in each spawn cycle
            enemySpeed += bm /= 1.01f;
            
            // get the current wait time based on wait time and a small amount of randomization, the t variable is the value that gets
            // increased over time and makes spawning faster
            currentTime = (currentTime - t);
            if (currentTime < 0)
            {
                currentTime = 0.01f;
            }
            // wait until the next spawn cycle
            yield return new WaitForSeconds(currentTime + Random.Range(-waitTimeOffset, waitTimeOffset));
            
            // get the spawn location
            float angle = Random.value * Mathf.PI * 2;
            x = Mathf.Cos(angle) * (radius + Random.Range(-radiusOffset, radiusOffset));
            y = Mathf.Sin(angle) * (radius + Random.Range(-radiusOffset, radiusOffset));
            
            // spawn enemy
            spawnEnemy(new Vector3(x, y, 0), enemySpeed / bl);
        }
    }
    
    private void spawnEnemy(Vector3 pos, float speed)
    {
        // chance of spawning the faster type
        float chance = Random.Range(0, 100);
        if (chance > 15)
        {
            GameObject go = Instantiate(ObjectsToSpawn[0], pos, Quaternion.identity);
            go.GetComponent<EnemyController>().speed = speed;
            //go.GetComponent<EnemyController>().enabled = false;
        }
        else
        {
            GameObject go = Instantiate(ObjectsToSpawn[1], pos, Quaternion.identity);
            go.GetComponent<EnemyController>().speed = speed * 1.3f;
            //go.GetComponent<EnemyController>().enabled = false;

        }
    }
}
