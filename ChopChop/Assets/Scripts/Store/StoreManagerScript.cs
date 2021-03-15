using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using scoring;
public class StoreManagerScript : MonoBehaviour
{
    public int[,] storeItems = new int[3,3];
    public float golds = Score.getGold();
    public Text goldText;

    public int healthPotionval = 30;
    public int powerPotionval = 15;
    public Health health;
    public PlayerController player;

    public GameObject powerPotionButton;

    // Start is called before the first frame update
    void Start()
    {
        goldText.text = "Golds:" + golds.ToString();

        //item id
        storeItems[1, 1] = 1;       //health potion id
        storeItems[1, 2] = 2;       //add attack boost id

        //item price
        storeItems[2, 1] = 30;      //health potion price
        storeItems[2, 2] = 150;      //add attack boost price

    }

    // Update is called once per frame
    public void BuyBoosts()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("StoreEvent").GetComponent<EventSystem>().currentSelectedGameObject;
        Debug.Log("Current Gold-----------------" + Score.getGold());

        int currentGold = Score.getGold();
        if(currentGold >= storeItems[2, buttonRef.GetComponent<BuyPotion>().boostID]){
            Score.useGold(storeItems[2, buttonRef.GetComponent<BuyPotion>().boostID]);
            // currentGold -= storeItems[2, buttonRef.GetComponent<ButtonInfo>().boostID];
            Debug.Log("Money Left ------------  " + Score.getGold());
            goldText.text = "Golds:" + golds.ToString();

            int boostID = buttonRef.GetComponent<BuyPotion>().boostID;
            if(boostID == 1){
                health.addHealth(healthPotionval);
                Debug.Log("Health After Drinking ------------  " + health.getCurrentHealth());
            }else if(boostID == 2){
                player.addAttack(powerPotionval);
                powerPotionButton.GetComponent<Button>().interactable = false;
            }
        }
    }
}
