using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;

    public GameObject helathPotion;
    public GameObject powerPotion;

    private bool paused = false;
    public void TogglePause()
    {
        if (!paused)
        {
            GamePause();
        }
        else
        {
            GameResume();
        }
    }
    private void GamePause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        helathPotion.SetActive(true);
        powerPotion.SetActive(true);
        paused = true;

    }
    private void GameResume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        helathPotion.SetActive(false);
        powerPotion.SetActive(false);
        paused = false;

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
