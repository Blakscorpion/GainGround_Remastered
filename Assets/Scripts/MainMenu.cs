using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene in the hierarchy order of the Scene Manager (File --> Build Settings)
        Invoke("LoadNextScene", 1.7f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    // Clean the PlayerPref to avoid starting with the heroes of the previous session
        PlayerPrefs.DeleteAll();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

}