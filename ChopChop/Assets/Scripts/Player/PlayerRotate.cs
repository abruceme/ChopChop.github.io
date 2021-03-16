using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float moveSpeed = 5f;
    // public float turnSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //StartCoroutine(RotateMe(Vector3.up * -100, .1f));
            transform.Rotate(Vector3.up, -90);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //StartCoroutine(RotateMe(Vector3.up * 100, .1f));
            transform.Rotate(Vector3.up, 90);
        }
    }

    
    IEnumerator RotateMe(Vector3 byAngles, float inTime) 
     {    var fromAngle = transform.rotation;
         var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
         for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
             transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
             yield return null;
         }
     }
}
