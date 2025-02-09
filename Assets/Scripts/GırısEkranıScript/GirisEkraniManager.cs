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
        // Butonlara t�klama olaylar�n� atama
        baslaButton.onClick.AddListener(Basla);
        bilgiButton.onClick.AddListener(BilgiGoster);
        cikisButton.onClick.AddListener(Cikis);
        kapatButton.onClick.AddListener(BilgiKapat);

        // Bilgi paneli ba�lang��ta kapal�
        bilgiPanel.SetActive(false);
    }

    // Oyunu ba�latma fonksiyonu
    public void Basla()
    {
        SceneManager.LoadScene("SampleScene"); // SampleScene sahnesine ge�i�
    }

    // Bilgi panelini a�ma fonksiyonu
    public void BilgiGoster()
    {
        bilgiPanel.SetActive(true);
    }

    // Bilgi panelini kapatma fonksiyonu
    public void BilgiKapat()
    {
        bilgiPanel.SetActive(false);
    }

    // Oyundan ��k�� fonksiyonu
    public void Cikis()
    {
        Application.Quit();
        Debug.Log("Oyun kapat�ld�.");
    }
}
