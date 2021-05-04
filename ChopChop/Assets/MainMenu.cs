using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {

        SceneManager.LoadSceneAsync("EndlessMode");

    }
    public void PracticeGame()
    {
        SceneManager.LoadSceneAsync("Practice");
    }
    
    public void TutorialGame()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public  void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
