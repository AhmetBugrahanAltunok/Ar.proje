using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GirisEkraniManager : MonoBehaviour
{
    public Button baslaButton;
    public Button bilgiButton;
    public Button cikisButton;
    public GameObject bilgiPanel;
    public Button kapatButton;

    void Start()
    {
        // Butonlara týklama olaylarýný atama
        baslaButton.onClick.AddListener(Basla);
        bilgiButton.onClick.AddListener(BilgiGoster);
        cikisButton.onClick.AddListener(Cikis);
        kapatButton.onClick.AddListener(BilgiKapat);

        // Bilgi paneli baþlangýçta kapalý
        bilgiPanel.SetActive(false);
    }

    // Oyunu baþlatma fonksiyonu
    public void Basla()
    {
        SceneManager.LoadScene("SampleScene"); // SampleScene sahnesine geçiþ
    }

    // Bilgi panelini açma fonksiyonu
    public void BilgiGoster()
    {
        bilgiPanel.SetActive(true);
    }

    // Bilgi panelini kapatma fonksiyonu
    public void BilgiKapat()
    {
        bilgiPanel.SetActive(false);
    }

    // Oyundan çýkýþ fonksiyonu
    public void Cikis()
    {
        Application.Quit();
        Debug.Log("Oyun kapatýldý.");
    }
}
