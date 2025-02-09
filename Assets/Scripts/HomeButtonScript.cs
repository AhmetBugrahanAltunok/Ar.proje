using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonScript : MonoBehaviour
{
    public GameObject confirmationPanel; // Onay paneli referans�

    // Home butonuna bas�ld���nda �al��acak
    public void OnHomeButtonPressed()
    {
        confirmationPanel.SetActive(true); // Onay panelini g�ster
    }

    // "Evet" butonuna bas�ld���nda �al��acak
    public void ConfirmReturnToHome()
    {
        SceneManager.LoadScene("G�r�sEkran�"); // Ana sayfaya d�n
    }

    // "Hay�r" butonuna bas�ld���nda �al��acak
    public void CancelReturnToHome()
    {
        confirmationPanel.SetActive(false); // Onay panelini kapat
    }
}
