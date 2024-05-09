using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public GameObject Player;
    public GameObject MainCamera;
    public GameObject Game_over;
    public Transform player_t;
    public GameObject instantiatedObject;
    public float countdownTime;
    public GameObject Score_text;
    public GameObject timerTextGO;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText_GO;
    public TextMeshProUGUI High_Score_GO;
    public int score = 0;
    public int High_Score = 0;
    public UnityEngine.UI.Toggle Pouse_Play;
    public GameObject left1;
    public GameObject right1;

    private void Awake()
    {
        GM = this;
        Time.timeScale = 1f;
        High_Score = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(StartCountdown());
        Pouse_Play.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    private void Update()
    {
        if (instantiatedObject != null)
            player_t = instantiatedObject.transform;
    }

    private IEnumerator StartCountdown()
    {
        Score_text.SetActive(false);
        float currentTime = countdownTime;
        while (currentTime > 0)
        {
            if (currentTime != 1)
            {
                timerText.text = (currentTime - 1).ToString();
                FindObjectOfType<AudioManager>().Play_Sound("Timer_Beep");
            }
            else
            {
                timerText.text = "GO.!";
                FindObjectOfType<AudioManager>().Play_Sound("Timer_Beep3");
            }

            yield return new WaitForSecondsRealtime(1f);
            currentTime--;
        }

        timerTextGO.SetActive(false);
        Score_text.SetActive(true);
        left1.SetActive(true);
        right1.SetActive(true);
        instantiatedObject = Instantiate(Player, new Vector3(0f, 3.5f, 0f), Quaternion.identity);

        FindObjectOfType<AudioManager>().Play_Sound("BG");
    }

    public void Score(int s)
    {
        score += s;
        scoreText.text = score.ToString();
    }

    public void Game_Over()
    {
        Game_over.SetActive(true);
        scoreText_GO.text = "Score: " + score.ToString();
        if (score > High_Score)
        {
            High_Score = score;
            PlayerPrefs.SetInt("HighScore", High_Score);
        }
        High_Score_GO.text = "High Score: " + High_Score.ToString();
    }

    public void Quit()
    {
        SceneLoader.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}