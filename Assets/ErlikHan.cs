using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class ErlikHan : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI questionText; // Soruyu gösterecek Text
    public Button optionButton1; // Ýlk seçenek butonu
    public Button optionButton2; // Ýkinci seçenek butonu
    public TextMeshProUGUI optionText1; // Ýlk seçenek metni
    public TextMeshProUGUI optionText2; // Ýkinci seçenek metni

    [Header("Questions")]
    public string[] questions; // Soru listesi
    public string[] option1Texts; // Ýlk seçenekler
    public string[] option2Texts; // Ýkinci seçenekler
    public int[] correctAnswers; // Doðru cevaplarýn index'leri (0 veya 1)

    private int currentQuestionIndex = 0; // Hangi sorudayýz

    [Header("External Condition")]
    public GameObject erlikHan; // GoblinTalk objesinin referansý
    private GoblinTalk goblinTalk; // GoblinTalk scripti

    private void Start()
    {
        // GoblinTalk scriptine referans al
        goblinTalk = erlikHan.GetComponent<GoblinTalk>();

        // Ýlk baþta sorularý gizle
        questionText.gameObject.SetActive(false);
        optionButton1.gameObject.SetActive(false);
        optionButton2.gameObject.SetActive(false);

        // Butonlara týklama eventi ekle
        optionButton1.onClick.AddListener(() => CheckAnswer(0)); // Ýlk seçeneði kontrol et
        optionButton2.onClick.AddListener(() => CheckAnswer(1)); // Ýkinci seçeneði kontrol et

        // Dialogue tamamlanana kadar sorularý göstermemek için bekleyin
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

        // Koþul saðlandýðýnda sorularý göster
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        // Sorularý ilk defa gösterdiðimizde UI'yi aktif hale getir
        questionText.gameObject.SetActive(true);
        optionButton1.gameObject.SetActive(true);
        optionButton2.gameObject.SetActive(true);

        if (currentQuestionIndex < questions.Length)
        {
            // Soruyu ve seçenekleri güncelle
            questionText.text = questions[currentQuestionIndex];
            optionText1.text = option1Texts[currentQuestionIndex];
            optionText2.text = option2Texts[currentQuestionIndex];
        }
        else
        {
            // Tüm sorular bittiðinde mesaj göster
            questionText.text = "Tüm sorularý tamamladýnýz!";
            optionButton1.gameObject.SetActive(false);
            optionButton2.gameObject.SetActive(false);
        }
    }

    private void CheckAnswer(int selectedOption)
    {
        // Seçilen yanýt doðru mu?
        if (selectedOption == correctAnswers[currentQuestionIndex])
        {
            Debug.Log("Doðru Cevap!");
        }
        else
        {
            Debug.Log("Yanlýþ Cevap!");
        }

        // Bir sonraki soruya geç
        currentQuestionIndex++;
        ShowQuestion();
    }
}
