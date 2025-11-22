using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject[] objectsToDeactivate; // Assign all non-menu GameObjects here in Inspector
    public GameObject mmCam;

    /// <summary>
    /// Initializes the main menu, activates the menu UI, and deactivates all specified game objects.
    /// Skips the menu if PauseMenuManager.IsRestarting is true.
    /// </summary>
    void Start()
    {
        if (PauseMenuManager.IsRestarting)
        {
            menuUI.SetActive(false);
            foreach (var obj in objectsToDeactivate)
            {
                if (obj != null)
                    obj.SetActive(true);
            }
            if (mmCam != null) mmCam.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PauseMenuManager.IsRestarting = false; // Reset flag
            return;
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuUI.SetActive(true);
        foreach (var obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

    /// <summary>
    /// Starts the game by hiding the menu UI, activating all specified game objects, and deactivating the main menu camera.
    /// </summary>
    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuUI.SetActive(false);
        foreach (var obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }
        if (mmCam != null) mmCam.SetActive(false);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Called once per frame. Currently not used.
    /// </summary>
    void Update()
    {
        
    }
}
