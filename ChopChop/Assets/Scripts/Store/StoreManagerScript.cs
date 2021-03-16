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

    public WeaponCollision weaponCollision;

    public bool boughtPowerPotion = false;

    // Start is called before the first frame update
    void Start()
    {
        goldText.text = "Golds:" + golds.ToString();

        //item id
        storeItems[1, 1] = 1;       //health potion id
        storeItems[1, 2] = 2;       //add attack boost id

        //item price
        storeItems[2, 1] = 30;      //health potion price
        storeItems[2, 2] = 50;      //add attack boost price

    }

    public void setBoughtPowerPotionState(bool newState){
        boughtPowerPotion = newState;
    }

    public void activatePowerPotion(){
        boughtPowerPotion = false;
        powerPotionButton.GetComponent<Button>().interactable = true;
    }

    // Update is called once per frame
    public void BuyBoosts()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("StoreEvent").GetComponent<EventSystem>().currentSelectedGameObject;
        // Debug.Log("Current Gold-----------------" + Score.getGold());

        int currentGold = Score.getGold();
        if(currentGold >= storeItems[2, buttonRef.GetComponent<BuyPotion>().boostID]){
            Score.useGold(storeItems[2, buttonRef.GetComponent<BuyPotion>().boostID]);
            // Debug.Log("Money Left ------------  " + Score.getGold());
            goldText.text = "Golds:" + golds.ToString();

            int boostID = buttonRef.GetComponent<BuyPotion>().boostID;
            if(boostID == 1){
                health.addHealth(healthPotionval);
                Debug.Log("Health After Drinking Potion------------  " + health.getCurrentHealth());
            }else if(boostID == 2){
                // if(!boughtPowerPotion){
                    boughtPowerPotion = true;
                    player.addAttack();
                    // Debug.Log("Current Left Arm Attack value ------------  " + GameObject.Find("mixamorig:LeftArm").GetComponent<WeaponCollision>().getFirstDamage());
                    // Debug.Log("Current Right Arm Attack value ------------  " + GameObject.Find("mixamorig:RightArm").GetComponent<WeaponCollision>().getFirstDamage());
                    Debug.Log("Bought Power Potion------------  ");        
                    powerPotionButton.GetComponent<Button>().interactable = false;
                // }
                // else{
                //     boughtPowerPotion = false;
                //     Debug.Log("Power Potion Activated------------  ");  
                //     powerPotionButton.GetComponent<Button>().interactable = true;
                // }
                
            }
        }
    }

    public void addPlayerAttack(){

    }
}
