using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject levelPopup; // Assign the popup panel here in inspector

    // Called when "Play" is clicked
    public void StartGame()
    {
        // SceneManager.LoadScene("Level 1"); 
        SceneManager.LoadScene("Level 2 IndustrialZone"); // Added for testing
    }

    // Called when "Levels" is clicked
    public void ShowLevelPopup()
    {
        levelPopup.SetActive(true);
    }

    // Called when "Close" is clicked in the popup
    public void HideLevelPopup()
    {
        levelPopup.SetActive(false);
    }

    public void LoadLevel1()
    {
        // SceneManager.LoadScene("Level 1"); // Testin Lab
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2 IndustrialZone"); // Industrial Zone
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3"); // Nature Zone
    }

    public void LoadLevel4()
    {
         SceneManager.LoadScene("Level 4 Sticky Zone"); // Sticky Zone
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene("Level 5 Scifi"); // Sci-Fi Zone
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
