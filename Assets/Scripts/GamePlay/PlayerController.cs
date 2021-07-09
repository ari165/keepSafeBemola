using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    private AudioSource enemyHitSound;
    private Transform thisTransform;
    private Camera cam;

    public Vector3 screenPos;
    public Vector3 worldPos;

    private void Start()
    {
        // set the variables
        enemyHitSound = GetComponent<AudioSource>();
        thisTransform = GetComponent<Transform>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    private void FixedUpdate()
    {
        // get the touch/mouse input and convert it to world position
        screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        worldPos = cam.ScreenToWorldPoint(screenPos);
        
        if (-0.25 < worldPos.x && worldPos.x < 0.25 && worldPos.y > -0.25 && worldPos.y < 0.25)
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && !hit.collider.gameObject.CompareTag("Player"))
            {
                return;
            }
        }
        // move player smoothly to the position
        thisTransform.position = Vector3.Lerp(thisTransform.position, worldPos, 0.01f * speed);
    }
    

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if collided with enemy
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("BEnemy"))
        {
            // play sound with a random pitch
            enemyHitSound.pitch = Random.Range(0.6f, 1.4f);
            enemyHitSound.Play();
        }
    }
    

}
