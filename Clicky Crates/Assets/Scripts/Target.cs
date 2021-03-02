using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private float minSpeed = 12, maxSpeed = 16;
    private float torqueRange = 10;
    private float spawnX = 2.0f, spawnY = -3;
    public int pointValue;

    private GameManager gameManager;
    [SerializeField]
    private ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = SpawnPosition();

        //Getting a reference to the Game Manager object and then accessing the GameManager script
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            if(gameObject.CompareTag("Bad"))
            {
                FindObjectOfType<AudioManager>().Play("Negative");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Positive");
            }    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    
    float RandomTorque()
    {
        return Random.Range(-torqueRange, torqueRange);
    }

    Vector3 SpawnPosition()
    {
        return new Vector3(Random.Range(-spawnX, spawnX), spawnY);
    }
}
