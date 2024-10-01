using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;  // Das Hauptmen�-Panel
    public GameObject settingsPanel;  // Das Einstellungs-Panel

    // Funktion, um das Spiel zu starten
    public void PlayGame()
    {
        Debug.Log("Play button clicked!");
        SceneManager.LoadScene("GameScene"); // Name deiner Spielszene
    }

    // Funktion, um das Einstellungs-Panel zu �ffnen und das Hauptmen� zu schlie�en
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);   // Aktiviert das Einstellungs-Panel
        mainMenuPanel.SetActive(false);  // Deaktiviert das Hauptmen�-Panel
    }

    // Funktion, um zur Startseite zur�ckzukehren (Einstellungs-Panel deaktivieren, Hauptmen� aktivieren)
    public void BackToMainMenu()
    {
        settingsPanel.SetActive(false);  // Deaktiviert das Einstellungs-Panel
        mainMenuPanel.SetActive(true);   // Aktiviert das Hauptmen�-Panel
    }

    // Funktion, um das Spiel zu beenden
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
}