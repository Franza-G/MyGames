using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicCliptwo;
    public AudioClip musicClipthree;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    [SerializeField] public Text timerText;
    [SerializeField] public float mainTimer;
    private float Timer;
    private bool canCount = true;
    private bool doOnce = false;

    

    void Start ()
    {
        Timer = mainTimer;
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";       
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    void Update()
    {
        if(Timer >= 0.0f && canCount)
        {
            Timer -= Time.deltaTime;
            timerText.text = Timer.ToString("F");
        }

        else if(Timer <= 0.0f && !doOnce)
        {            
            canCount = false;
            doOnce = true;
            GameOver();
            timerText.text = "0.00";
            Timer = 0.0f;
            
        }


        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene("Main");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'G' to Restart";
                restart = true;                
                break;
            }
        }
        
    }


    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if (score >= 300)
        {
            gameOverText.text = "You win! Game created by Franza Gregoire";
            musicSource.Stop();
            musicSource.clip = musicCliptwo;
            musicSource.Play();
            gameOver = true;
            restart = true;
            

        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game over! Game created by Franza Gregoire";
        musicSource.Stop();
        musicSource.clip = musicClipthree;
        musicSource.Play();
        gameOver = true;

        timerText.text = "0.00";
        Timer = 0.0f;

        Destroy(GameObject.FindWithTag("Player"));
    }
        
}
