using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameoverText;
    public float restartFlashInterval;

    private bool gameover;
    private bool restart;
    private int score;
    private float timeToToggleText;
    private string rText = "Press 'R' to Restart";

    void Start()
    {
        gameover = false;
        restart = false;
        restartText.text = "";
        gameoverText.text = "";
        score = 0;
        timeToToggleText = restartFlashInterval;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameover)
            {
                ToggleRestartText();
                restart = true;
                break;
            }
        }
    }

    void Update()
    {
        if(restart)
        {
            timeToToggleText -= Time.deltaTime;
            if (timeToToggleText <= 0)
            {
                ToggleRestartText();
                timeToToggleText = restartFlashInterval;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
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
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameoverText.text = "Game Over";
        gameover = true;
    }

    void ToggleRestartText()
    {
        if (restartText.text == "")
        {
            restartText.text = rText;
        } else
        {
            restartText.text = "";
        }
    }
}
