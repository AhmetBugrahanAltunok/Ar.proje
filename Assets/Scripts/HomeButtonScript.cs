using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonScript : MonoBehaviour
{
    public GameObject confirmationPanel; // Onay paneli referansý

    // Home butonuna basýldýðýnda çalýþacak
    public void OnHomeButtonPressed()
    {
        confirmationPanel.SetActive(true); // Onay panelini göster
    }

    // "Evet" butonuna basýldýðýnda çalýþacak
    public void ConfirmReturnToHome()
    {
        SceneManager.LoadScene("GýrýsEkraný"); // Ana sayfaya dön
    }

    // "Hayýr" butonuna basýldýðýnda çalýþacak
    public void CancelReturnToHome()
    {
        confirmationPanel.SetActive(false); // Onay panelini kapat
    }
}
