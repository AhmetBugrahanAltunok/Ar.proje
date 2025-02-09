using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;  // Sahne yönetimi için eklendi

public class UIManager : MonoBehaviour
{
    public TMP_InputField toplamGezegenInput;
    public TMP_InputField yasamBelirtisiInput;
    public TMP_InputField yasamGezegenInput;
    public TMP_InputField sonucInput;
    public TMP_InputField sonucInput2;
    public TMP_InputField finalSonucInput;
    public TMP_Text resultText;
    public TMP_Text resultText1;
    public TMP_Text resultText2;
    public TMP_Text finalCalculationText;

    public GameObject step1Panel;
    public GameObject step3Panel;
    public GameObject step2Panel;
    public GameObject resultPanel1;
    public GameObject resultPanel2;
    public GameObject finalResultPanel;
    public GameObject gameOverPanel;

    private int toplamGezegenSayisi;
    private int yasamBelirtisiGezegenSayisi;
    private int yasamGezegenSayisi;

    private int userToplamGezegenSayisi;
    private int userYasamBelirtisiSayisi;
    private int userYasamGezegenSayisi;

    private float zekaCarpani = 0.8f;
    private float teknolojiCarpani = 0.625f;

    public GezegenSpawner gezegenSpawner;

    void Start()
    {
        toplamGezegenSayisi = gezegenSpawner.GetToplamGezegenSayisi();
        yasamBelirtisiGezegenSayisi = gezegenSpawner.GetYasamBelirtisiGezegenSayisi();
        yasamGezegenSayisi = gezegenSpawner.GetYasamGezegenSayisi();
    }

    public void CheckTotalPlanets()
    {
        string inputText = toplamGezegenInput.text.Trim();

        if (string.IsNullOrEmpty(inputText))
        {
            resultText.text = "Lütfen bir sayý girin!";
            return;
        }

        if (int.TryParse(inputText, out userToplamGezegenSayisi) && userToplamGezegenSayisi > 0)
        {
            if (userToplamGezegenSayisi == toplamGezegenSayisi)
            {
                step1Panel.SetActive(false);
                step3Panel.SetActive(true);
                resultText.text = "";
            }
            else
            {
                resultText.text = "Yanlýþ! Tekrar deneyin.";
            }
        }
        else
        {
            resultText.text = "Lütfen pozitif bir tam sayý girin!";
        }
    }

    public void CheckYasamBelirtisiGezegen()
    {
        string belirtisiInputText = yasamBelirtisiInput.text.Trim();

        if (string.IsNullOrEmpty(belirtisiInputText))
        {
            resultText.text = "Lütfen yaþam belirtisi gezegen sayýsýný girin!";
            return;
        }

        if (int.TryParse(belirtisiInputText, out userYasamBelirtisiSayisi))
        {
            if (userYasamBelirtisiSayisi == yasamBelirtisiGezegenSayisi)
            {
                step3Panel.SetActive(false);
                resultPanel1.SetActive(true);
                ShowCalculationPrompt();
            }
            else
            {
                resultText.text = "Yanlýþ! Tekrar deneyin.";
            }
        }
        else
        {
            resultText.text = "Lütfen geçerli bir sayý girin!";
        }
    }

    public void ShowCalculationPrompt()
    {
        resultText1.text = $"{userYasamBelirtisiSayisi} / {userToplamGezegenSayisi} = ?\nLütfen sonucu girin.";
    }

    public void CheckUserResult()
    {
        string userResultInput = sonucInput.text.Trim().Replace(',', '.');
        Debug.Log($"Kullanýcý Girdisi (düzenlenmiþ): {userResultInput}");

        if (string.IsNullOrEmpty(userResultInput))
        {
            resultText1.text = "Lütfen sonucu girin!";
            return;
        }

        CultureInfo culture = CultureInfo.InvariantCulture;

        if (float.TryParse(userResultInput, NumberStyles.Any, culture, out float userResult))
        {
            float correctResult = (float)userYasamBelirtisiSayisi / userToplamGezegenSayisi;

            correctResult = Mathf.Round(correctResult * 100f) / 100f;
            userResult = Mathf.Round(userResult * 100f) / 100f;

            Debug.Log($"Doðru Sonuç (yuvarlanmýþ): {correctResult}, Kullanýcý Girdisi (yuvarlanmýþ): {userResult}");

            if (Mathf.Abs(userResult - correctResult) < 0.02f)
            {
                resultText1.text = "Tebrikler! Doðru sonucu girdiniz.";
                resultPanel1.SetActive(false);
                step2Panel.SetActive(true);
            }
            else
            {
                resultText1.text = "Yanlýþ! Tekrar deneyin.";
            }
        }
        else
        {
            resultText1.text = "Geçerli bir sayý girin!";
        }
    }

    public void CheckYasamGezegen()
    {
        string yasamGezegenInputText = yasamGezegenInput.text.Trim();

        if (string.IsNullOrEmpty(yasamGezegenInputText))
        {
            resultText.text = "Lütfen yaþam olan gezegen sayýsýný girin!";
            return;
        }

        if (int.TryParse(yasamGezegenInputText, out userYasamGezegenSayisi))
        {
            if (userYasamGezegenSayisi == yasamGezegenSayisi)
            {
                step2Panel.SetActive(false);
                resultPanel2.SetActive(true);
                ShowSecondCalculationPrompt();
            }
            else
            {
                resultText.text = "Yanlýþ! Tekrar deneyin.";
            }
        }
        else
        {
            resultText.text = "Lütfen geçerli bir sayý girin!";
        }
    }

    public void ShowSecondCalculationPrompt()
    {
        resultText2.text = $"{userYasamGezegenSayisi} / {userYasamBelirtisiSayisi} = ?\nLütfen sonucu girin.";
    }

    public void CheckSecondUserResult()
    {
        string userResultInput2 = sonucInput2.text.Trim().Replace(',', '.');
        Debug.Log($"Kullanýcý Ýkinci Girdisi (düzenlenmiþ): {userResultInput2}");

        if (string.IsNullOrEmpty(userResultInput2))
        {
            resultText2.text = "Lütfen sonucu girin!";
            return;
        }

        CultureInfo culture = CultureInfo.InvariantCulture;

        if (float.TryParse(userResultInput2, NumberStyles.Any, culture, out float userResult2))
        {
            float correctResult2 = (float)userYasamGezegenSayisi / userYasamBelirtisiSayisi;

            correctResult2 = Mathf.Round(correctResult2 * 100f) / 100f;
            userResult2 = Mathf.Round(userResult2 * 100f) / 100f;

            Debug.Log($"Doðru Ýkinci Sonuç (yuvarlanmýþ): {correctResult2}, Kullanýcý Ýkinci Girdisi (yuvarlanmýþ): {userResult2}");

            if (Mathf.Abs(userResult2 - correctResult2) < 0.02f)
            {
                resultText2.text = "Tebrikler! Doðru sonucu girdiniz.";
                resultPanel2.SetActive(false);
                ShowFinalCalculationPrompt();
            }
            else
            {
                resultText2.text = "Yanlýþ! Tekrar deneyin.";
            }
        }
        else
        {
            resultText2.text = "Geçerli bir sayý girin!";
        }
    }

    public void ShowFinalCalculationPrompt()
    {
        finalResultPanel.SetActive(true);
        finalCalculationText.text = $"Tüm Gezegenler: {userToplamGezegenSayisi}\nYaþam Ýhtimali: {(float)userYasamBelirtisiSayisi / userToplamGezegenSayisi:F2}\nGerçek Yaþam: {(float)userYasamGezegenSayisi / userYasamBelirtisiSayisi:F2}\nZeka Çarpaný: {zekaCarpani}\nTeknoloji Çarpaný: {teknolojiCarpani}";
    }

    public void CheckFinalResult()
    {
        string userFinalResultInput = finalSonucInput.text.Trim().Replace(',', '.');
        Debug.Log($"Kullanýcý Final Girdisi (düzenlenmiþ): {userFinalResultInput}");

        if (string.IsNullOrEmpty(userFinalResultInput))
        {
            finalCalculationText.text += "\nLütfen sonucu girin!";
            return;
        }

        CultureInfo culture = CultureInfo.InvariantCulture;

        if (float.TryParse(userFinalResultInput, NumberStyles.Any, culture, out float userFinalResult))
        {
            float yasamIhtimali = (float)userYasamBelirtisiSayisi / userToplamGezegenSayisi;
            float gercekYasam = (float)userYasamGezegenSayisi / userYasamBelirtisiSayisi;

            float correctFinalResult = userToplamGezegenSayisi * yasamIhtimali * gercekYasam * zekaCarpani * teknolojiCarpani;

            correctFinalResult = Mathf.Round(correctFinalResult * 100f) / 100f;
            userFinalResult = Mathf.Round(userFinalResult * 100f) / 100f;

            Debug.Log($"Doðru Final Sonuç (yuvarlanmýþ): {correctFinalResult}, Kullanýcý Final Girdisi (yuvarlanmýþ): {userFinalResult}");

            if (Mathf.Abs(userFinalResult - correctFinalResult) < 0.02f)
            {
                finalCalculationText.text += "\nTebrikler! Tüm hesaplamalarý baþarýyla tamamladýnýz.";
                finalResultPanel.SetActive(false);
                gameOverPanel.SetActive(true);
            }
            else
            {
                finalCalculationText.text += "\nYanlýþ! Tekrar deneyin.";
            }
        }
        else
        {
            finalCalculationText.text += "\nGeçerli bir sayý girin!";
        }
    }

    // Oyunu yeniden baþlatma veya ana menüye dönme fonksiyonu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("GýrýsEkraný");
    }
}
