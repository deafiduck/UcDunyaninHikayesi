using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class ErlikHan : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText; // Soruyu g�sterecek Text
    public Button optionButton1; // �lk se�enek butonu
    public Button optionButton2; // �kinci se�enek butonu
    public TextMeshProUGUI optionText1; // �lk se�enek metni
    public TextMeshProUGUI optionText2; // �kinci se�enek metni

    [Header("Questions")]
    public string[] questions; // Soru listesi
    public string[] option1Texts; // �lk se�enekler
    public string[] option2Texts; // �kinci se�enekler
    public int[] correctAnswers; // Do�ru cevaplar�n index'leri (0 veya 1)

    private int currentQuestionIndex = 0; // Hangi soruday�z

    [Header("External Condition")]
    public GameObject erlikHan; // GoblinTalk objesinin referans�
    private GoblinTalk goblinTalk; // GoblinTalk scripti

    private void Start()
    {
        // GoblinTalk scriptine referans al
        goblinTalk = erlikHan.GetComponent<GoblinTalk>();

        // �lk ba�ta sorular� gizle
        questionText.gameObject.SetActive(false);
        optionButton1.gameObject.SetActive(false);
        optionButton2.gameObject.SetActive(false);

        // Butonlara t�klama eventi ekle
        optionButton1.onClick.AddListener(() => CheckAnswer(0)); // �lk se�ene�i kontrol et
        optionButton2.onClick.AddListener(() => CheckAnswer(1)); // �kinci se�ene�i kontrol et

        // Dialogue tamamlanana kadar sorular� g�stermemek i�in bekleyin
        StartCoroutine(WaitForDialogueCompletion());
    }

    // IEnumerator kullanarak dialog tamamlanana kadar bekleniyor
    private IEnumerator WaitForDialogueCompletion()
    {
        // dialogueCompleted true olana kadar bekleyin
        while (!goblinTalk.dialogueCompleted)
        {
            yield return null; // Bir frame bekle
        }

        // Ko�ul sa�land���nda sorular� g�ster
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        // Sorular� ilk defa g�sterdi�imizde UI'yi aktif hale getir
        questionText.gameObject.SetActive(true);
        optionButton1.gameObject.SetActive(true);
        optionButton2.gameObject.SetActive(true);

        if (currentQuestionIndex < questions.Length)
        {
            // Soruyu ve se�enekleri g�ncelle
            questionText.text = questions[currentQuestionIndex];
            optionText1.text = option1Texts[currentQuestionIndex];
            optionText2.text = option2Texts[currentQuestionIndex];
        }
        else
        {
            // T�m sorular bitti�inde mesaj g�ster
            questionText.text = "T�m sorular� tamamlad�n�z!";
            optionButton1.gameObject.SetActive(false);
            optionButton2.gameObject.SetActive(false);
        }
    }

    private void CheckAnswer(int selectedOption)
    {
        // Se�ilen yan�t do�ru mu?
        if (selectedOption == correctAnswers[currentQuestionIndex])
        {
            Debug.Log("Do�ru Cevap!");
        }
        else
        {
            Debug.Log("Yanl�� Cevap!");
        }

        // Bir sonraki soruya ge�
        currentQuestionIndex++;
        ShowQuestion();
    }
}
