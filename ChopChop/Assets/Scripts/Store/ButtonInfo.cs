using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonInfo : MonoBehaviour
{
    public int boostID;
    public Text priceText;
    public GameObject StoreManager;

    // Update is called once per frame
    void Update()
    {
        priceText.text = "Price: $" + StoreManager.GetComponent<StoreManagerScript>().storeItems[2, boostID].ToString();
        
    }
}
