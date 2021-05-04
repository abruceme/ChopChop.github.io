using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialContinue : MonoBehaviour
{
    private Transform tutorialText;
    private Transform nextButton;
    private Transform tutorialPanel;
    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GameObject.Find("Canvas").transform.Find("TutorialText");
        nextButton = GameObject.Find("Canvas").transform.Find("TutorialNextButton");
        tutorialPanel = GameObject.Find("Canvas").transform.Find("TutorialPanel");
    }

    public void ContinueTutorial()
    {
        tutorialText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        tutorialPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}
