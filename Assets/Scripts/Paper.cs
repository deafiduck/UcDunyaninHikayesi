using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject paperPanel;
    public GameObject realBook;
    private float theDistance;

    private void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;

        if(theDistance <= 1.5)
        {

            if (Input.GetKey(KeyCode.G))
            {
               
                paperPanel.SetActive(true);
                realBook.SetActive(false);
            }
        }
        else
        {
         
            paperPanel.SetActive(false);
            realBook.SetActive(true);
        }

        Debug.Log("Distance: " + theDistance);
    }
   

}
