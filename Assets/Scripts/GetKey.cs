using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    public float detectionRange = 1.5f; // Anahtarý alma mesafesi
    public bool keyTaken; // Anahtar alýndý mý alýnmadý mý
    public GameObject key;
    public GameObject keyImage;
    public GameObject pointLight;
    public GameObject getKeyText; // E'ye bas yazýsý

    private Transform player; // Oyuncunun pozisyonu

    void Start()
    {
        keyTaken = false;
        getKeyText.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= detectionRange)
        {
            if (!keyTaken) // Anahtar alýnmadýysa çalýþsýn
            {
                getKeyText.SetActive(true);
            }

            if (Input.GetKey(KeyCode.E) && !keyTaken)
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
