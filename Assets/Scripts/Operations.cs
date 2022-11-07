using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Operations : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuObj;

    [SerializeField]
    private GameObject Crosshair;

    [SerializeField]
    private bool EnableOperations = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!EnableOperations)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Statics.SetPaused(!Statics.IsPaused());
            MenuObj.SetActive(Statics.IsPaused());
            Crosshair.SetActive(!Statics.IsPaused());

            Cursor.lockState = Statics.IsPaused() ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = Statics.IsPaused();
        }
    }

    public void ResumeGame()
    {
        Statics.SetPaused(false);
        MenuObj.SetActive(false);
        Crosshair.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
