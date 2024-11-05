using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]  private Text coffeesText;
    [SerializeField] Text recordText;
    [SerializeField] public Text scoreText;
    private void Start()
    {
        int LastRunScore = PlayerPrefs.GetInt("LastRunScore");
        int scoreText = PlayerPrefs.GetInt("recordScore");
        if (LastRunScore > scoreText)
        {
            scoreText = LastRunScore;
            PlayerPrefs.SetInt("recordScore", scoreText);
            recordText.text = scoreText.ToString();
        }
        else
        {
            recordText.text = scoreText.ToString();
        }
        int coffees = PlayerPrefs.GetInt("coffes");
        coffeesText.text = coffees.ToString();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    public void History()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
