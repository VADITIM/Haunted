using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider brightnessSlider;
    public Slider volumeSlider;
    public Slider mouseSpeedSlider;
    public TMP_Dropdown languageDropdown;
    public Toggle nightModeToggle;
    public Button backButton;

    // Referenz zum Licht (f�r Helligkeitseinstellung)
    public Light directionalLight;
    public AudioSource gameAudioSource; // AudioSource f�r die Lautst�rke

    private void Start()
    {
        // Helligkeit und Lautst�rke auf Standardwerte setzen
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1.0f); // 1.0 als Standardwert
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f); // 0.5 als Standardwert
        mouseSpeedSlider.value = PlayerPrefs.GetFloat("MouseSpeed", 1.0f); // 1.0 als Standardwert
        nightModeToggle.isOn = PlayerPrefs.GetInt("NightMode", 0) == 1;

        // Sprache auf Deutsch setzen
        languageDropdown.value = PlayerPrefs.GetInt("Language", 0); // 0 f�r Deutsch

        // Event Listener hinzuf�gen
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        mouseSpeedSlider.onValueChanged.AddListener(SetMouseSpeed);
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
        nightModeToggle.onValueChanged.AddListener(ToggleNightMode);
        backButton.onClick.AddListener(BackToMainMenu);

        // Starte mit aktuellen Einstellungen
        ApplySettings();
    }

    private void SetBrightness(float value)
    {
        PlayerPrefs.SetFloat("Brightness", value);
        if (directionalLight != null)
        {
            directionalLight.intensity = Mathf.Lerp(0.5f, 2.0f, value); // Beispiel: Anpassen der Lichtintensit�t
        }
    }

    private void SetVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        if (gameAudioSource != null)
        {
            gameAudioSource.volume = value; // Setzt die Lautst�rke
        }
    }

    private void SetMouseSpeed(float value)
    {
        PlayerPrefs.SetFloat("MouseSpeed", value);
        // Hier kannst du die Mausgeschwindigkeit �ndern, wenn du eine Maus-Skriptsteuerung hast
        // Beispiel: PlayerController.mouseSensitivity = Mathf.Lerp(minSpeed, maxSpeed, value);
    }

    private void ChangeLanguage(int index)
    {
        PlayerPrefs.SetInt("Language", index);
        // Logik f�r Sprach�nderung im Spiel
        if (index == 0)
        {
            // Deutsch
            // Setze die TextMeshPro-Komponenten auf Deutsch
        }
        else if (index == 1)
        {
            // Englisch
            // Setze die TextMeshPro-Komponenten auf Englisch
        }
    }

    private void ToggleNightMode(bool isOn)
    {
        PlayerPrefs.SetInt("NightMode", isOn ? 1 : 0);
        // Logik f�r Nachtmodus (z.B. Umstellen von UI-Farben, Beleuchtung etc.)
    }

    private void BackToMainMenu()
    {
        settingsPanel.SetActive(false);
        // Zeige das Hauptmen� an
    }

    // Wendet alle gespeicherten Einstellungen an
    private void ApplySettings()
    {
        // Helligkeit anwenden
        SetBrightness(PlayerPrefs.GetFloat("Brightness", 1.0f));
        // Lautst�rke anwenden
        SetVolume(PlayerPrefs.GetFloat("Volume", 0.5f));
        // Mausgeschwindigkeit anwenden (wenn Maussteuerung vorhanden ist)
        SetMouseSpeed(PlayerPrefs.GetFloat("MouseSpeed", 1.0f));
        // Sprache anwenden
        ChangeLanguage(PlayerPrefs.GetInt("Language", 0));
        // Nachtmodus anwenden
        ToggleNightMode(PlayerPrefs.GetInt("NightMode", 0) == 1);
    }
}