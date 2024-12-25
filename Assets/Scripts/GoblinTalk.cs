using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoblinTalk : MonoBehaviour
{
    Animator anim;
    Transform target; //ana karakterin pozisyonu
    public Text dialogueText;  // UI Text referans�

    [System.Serializable]
    public class Dialogue
    {
        public string text;
        public float duration;
    }

    public List<Dialogue> dialogues;  // Diyalog listesini tan�mla
    private bool isTalking = false;  // Coroutine'in �al��t���n� belirten bayrak
    private bool dialogueCompleted = false;  // Diyaloglar�n tamamland���n� belirten bayrak

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueText.text = "";  // Ba�lang��ta bo� metin
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
            StartCoroutine(ShowDialogues());  // Diyaloglar� g�steren Coroutine ba�lat
        }
    }

    private void GoblinIdle()
    {
        anim.SetBool("Talking", false);
        dialogueText.text = "";  // Konu�ma metnini temizle
        isTalking = false;  // Diyalog g�sterme i�lemini durdur
    }

    private IEnumerator ShowDialogues()
    {
        isTalking = true;  // Coroutine'in �al��t���n� belirle
        foreach (Dialogue dialogue in dialogues)
        {
            dialogueText.text = dialogue.text;
            yield return new WaitForSeconds(dialogue.duration);
        }
        dialogueText.text = "";  // T�m diyaloglar g�sterildikten sonra metni temizle
        isTalking = false;  // Coroutine'in bitti�ini belirle
        dialogueCompleted = true;  // Diyaloglar�n tamamland���n��belirle
����}
}