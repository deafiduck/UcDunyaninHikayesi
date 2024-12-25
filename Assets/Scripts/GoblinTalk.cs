using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinTalk : MonoBehaviour
{
    Animator anim;
    Transform target; //ana karakterin pozisyonu
    public Text dialogueText;  // UI Text referansý

    [System.Serializable]
    public class Dialogue
    {
        public string text;
        public float duration;
    }

    public List<Dialogue> dialogues;  // Diyalog listesini tanýmla
    private bool isTalking = false;  // Coroutine'in çalýþtýðýný belirten bayrak
    private bool dialogueCompleted = false;  // Diyaloglarýn tamamlandýðýný belirten bayrak

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueText.text = "";  // Baþlangýçta boþ metin
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= 5)
        {
            if (!isTalking && !dialogueCompleted)
            {
                GoblingTalking();
            }
        }
        else
        {
            GoblinIdle();
        }
        Debug.Log("Distance" + distance);
    }

    private void GoblingTalking()
    {
        anim.SetBool("Talking", true);
        if (!isTalking && !dialogueCompleted)
        {
            StartCoroutine(ShowDialogues());  // Diyaloglarý gösteren Coroutine baþlat
        }
    }

    private void GoblinIdle()
    {
        anim.SetBool("Talking", false);
        dialogueText.text = "";  // Konuþma metnini temizle
        isTalking = false;  // Diyalog gösterme iþlemini durdur
    }

    private IEnumerator ShowDialogues()
    {
        isTalking = true;  // Coroutine'in çalýþtýðýný belirle
        foreach (Dialogue dialogue in dialogues)
        {
            dialogueText.text = dialogue.text;
            yield return new WaitForSeconds(dialogue.duration);
        }
        dialogueText.text = "";  // Tüm diyaloglar gösterildikten sonra metni temizle
        isTalking = false;  // Coroutine'in bittiðini belirle
        dialogueCompleted = true;  // Diyaloglarýn tamamlandýðýný belirle
    }
}