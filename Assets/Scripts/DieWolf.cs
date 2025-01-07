using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DieWolf : MonoBehaviour
{
   
    bool healing;
   public GameObject healingEffect;
    GameObject player;
    public GameObject healText;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void FixedUpdate()
    {
        CheckDistance();
    }
    void CheckDistance()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 5)
        {
            healText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                healText.SetActive(false);
                healingEffect.SetActive(true);
                StartCoroutine(HideHealingEffect()); 
            }
        }
        else
        {
            healText.SetActive(false);
        }
    }

    IEnumerator HideHealingEffect()
    {
        yield return new WaitForSeconds(4f); // 2 saniye bekle
        healingEffect.SetActive(false); // Healing efektini gizle
    }

}
