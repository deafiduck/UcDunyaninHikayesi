using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject bookPanel;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bookPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            bookPanel.SetActive(false);
        }
    }

  
}

