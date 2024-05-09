using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    public GameObject Setting_panel;
    public Slider Sensitivity_slider;
    public TextMeshProUGUI Slider_count;

    // Start is called before the first frame update
    public void Awake()
    {
        Sensitivity_slider.value = PlayerPrefs.GetFloat("Sensitivity", 1);
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public static void Quit()
    {
        Application.Quit();
    }

    public void Setting_Desplay_T()
    {
        Setting_panel.SetActive(true);
    }

    public void Setting_Desplay_F()
    {
        Setting_panel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        Slider_count.text = Sensitivity_slider.value.ToString();
        Player.SidewaysSpeed(Sensitivity_slider.value);
    }
}