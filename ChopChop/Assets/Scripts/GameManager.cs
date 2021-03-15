using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using scoring;
public class GameManager : MonoBehaviour
{
    public GameObject reviveButton;
    public int currentgolds;

    public int revivePrice = 100;
     public Text goldText;
     public Health health;

    public GameObject pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        // Time.timeScale = 1f;
    }

    // Update is called once per frame
    public void GameOver()
    {
        reviveButton.SetActive(true);
    }

    public void OnReviveBtnPressed(){
        currentgolds = Score.getGold();
        Debug.Log("NOW ------------ " + currentgolds);
        if(currentgolds >= revivePrice){
            currentgolds -= revivePrice;
            Score.useGold(revivePrice);
            Score.setGold(currentgolds);
            goldText.text = "Golds:" + currentgolds.ToString();
            Debug.Log("LEft -------------- " + currentgolds);
            GameObject.Find("Canvas").transform.Find("Restart").gameObject.SetActive(false);
            reviveButton.SetActive(false);
            health.SetPlayerHealth(100);
            Time.timeScale = 1f;
            pauseButton.SetActive(true);
        }else{
            Time.timeScale = 0f;
            pauseButton.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Restart").gameObject.SetActive(true);
        }
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // Time.timeScale = 1f;
    }
}
