using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> targets;
    
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private GameObject titleScreen;

    private int score;
    public bool isGameActive;
    private float spawnRate = 1.0f;

    private bool playedGameOverSound = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;

        FindObjectOfType<AudioManager>().Stop("Background");
        if(playedGameOverSound == false)
        {
            FindObjectOfType<AudioManager>().Play("GameOver");
            playedGameOverSound = true;
        }        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        playedGameOverSound = false;
        spawnRate /= difficulty;
        
        FindObjectOfType<AudioManager>().Play("Background");

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.SetActive(false);
    }
}
