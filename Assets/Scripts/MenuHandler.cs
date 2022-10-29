using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Texture2D crosshair;
    public AudioClip MenuClickSound;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadOption()
    {
        SceneManager.LoadScene(4);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayMenuClickSound()
    {        
        gameObject.GetComponent<AudioSource>().PlayOneShot(MenuClickSound);
    }
}