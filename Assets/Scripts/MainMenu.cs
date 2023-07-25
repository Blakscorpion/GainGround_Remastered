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
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

}