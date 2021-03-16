using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pauseMenu;

    public GameObject healthPotion;
    public GameObject powerPotion;

    private bool paused = false;
    public PlayerController player;
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
        // Debug.Log("Current prcatice Mode ? ------" + player.practiceMode);
        if (!player.practiceMode)
        {
            GameObject.Find("Canvas").transform.Find("HealthPotionText").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("PowerPotionText").gameObject.SetActive(true);
            healthPotion.SetActive(true);
            powerPotion.SetActive(true);
        }
        paused = true;

    }
    private void GameResume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        if (!player.practiceMode)
        {
             GameObject.Find("Canvas").transform.Find("HealthPotionText").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("PowerPotionText").gameObject.SetActive(false);
            healthPotion.SetActive(false);
            powerPotion.SetActive(false);
        }
        paused = false;

    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("StartMenuScene");
    }
}
