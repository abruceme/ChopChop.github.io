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

    // Update is called once per frame
    public void BuyBoosts()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("StoreEvent").GetComponent<EventSystem>().currentSelectedGameObject;
        Debug.Log("Current Gold-----------------" + Score.getGold());

        int currentGold = Score.getGold();
        if(currentGold >= storeItems[2, buttonRef.GetComponent<ButtonInfo>().boostID]){
            Score.useGold(storeItems[2, buttonRef.GetComponent<ButtonInfo>().boostID]);
            // currentGold -= storeItems[2, buttonRef.GetComponent<ButtonInfo>().boostID];
            Debug.Log("Money Left ------------  " + Score.getGold());
            goldText.text = "Golds:" + golds.ToString();
        }
    }
}
