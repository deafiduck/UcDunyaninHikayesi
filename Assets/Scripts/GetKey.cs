using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    public float theDistance; //player'ýn anahtara olan uzaklýðý 
    public bool keyTaken; //anahtar alýndý mý alýnmadý mý
    public GameObject key;
    public GameObject keyImage;
    public GameObject pointLight;
    public GameObject getKeyText; //E'ye bas yazýsý
    void Start()
    {
        keyTaken = false;
        getKeyText.SetActive(false);
    }

    
    void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;

        //Debug.Log("Key distance: " + theDistance);
        if (theDistance <= 1.5f)
        {
            if (!keyTaken) //anahtar alýnmadýysa çalýþsýn 
            {
                getKeyText.SetActive(true); 
            }
           
            if (Input.GetKey(KeyCode.E))
            {
                KeyTaken();
            }
            
        }
        else
        {
            getKeyText.SetActive(false);
        }


        if (keyTaken)
        {
            keyImage.SetActive(true);
        }
        else
        {
            keyImage.SetActive(false);
        }
    }

    private void KeyTaken()
    {
        keyTaken = true;
        StartCoroutine(KeyTakenText());
        key.GetComponent<MeshRenderer>().enabled = false;
        pointLight.SetActive(false);
     
    }

    IEnumerator KeyTakenText()
    {
        yield return new WaitForSeconds(2f);
        getKeyText.SetActive(false);
    }

}
