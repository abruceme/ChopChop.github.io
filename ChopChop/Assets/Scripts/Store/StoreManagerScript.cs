using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StoreManagerScript : MonoBehaviour
{
    public int[,] storeItems = new int[3,3];
    public float points;
    public Text pointsText;

    // Start is called before the first frame update
    void Start()
    {
        pointsText.text = "Points:" + points.ToString();

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
        // Debug.Log("TESTTESTTTTTTTTTTTTTTTTTTTT");
        Debug.Log("yes cannnn buy " + storeItems[2, buttonRef.GetComponent<ButtonInfo>().boostID]);
        if(points >= storeItems[2, buttonRef.GetComponent<ButtonInfo>().boostID]){
            points -= storeItems[2, buttonRef.GetComponent<ButtonInfo>().boostID];
            Debug.Log(points);
            pointsText.text = "Points:" + points.ToString();
        }
    }
}
