using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;  // Das Hauptmenü-Panel
    public GameObject settingsPanel;  // Das Einstellungs-Panel

    // Funktion, um das Spiel zu starten
    public void PlayGame()
    {
        Debug.Log("Play button clicked!");
        SceneManager.LoadScene("SampleScene"); // Name deiner Spielszene
    }

    // Funktion, um das Einstellungs-Panel zu öffnen und das Hauptmenü zu schließen
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);   // Aktiviert das Einstellungs-Panel
        mainMenuPanel.SetActive(false);  // Deaktiviert das Hauptmenü-Panel
    }

    // Funktion, um zur Startseite zurückzukehren (Einstellungs-Panel deaktivieren, Hauptmenü aktivieren)
    public void BackToMainMenu()
    {
        settingsPanel.SetActive(false);  // Deaktiviert das Einstellungs-Panel
        mainMenuPanel.SetActive(true);   // Aktiviert das Hauptmenü-Panel
    }

    // Funktion, um das Spiel zu beenden
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
}