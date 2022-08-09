using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the next scene in the hierarchy order of the Scene Manager (File --> Build Settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Clean the PlayerPref to avoid starting with the heroes of the previous session
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}