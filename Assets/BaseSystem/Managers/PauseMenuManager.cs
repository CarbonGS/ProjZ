using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static bool IsRestarting = false;
    public GameObject pauseMenuUI;
    public GameObject fpUI;
    public bool isPaused = false;
    public MainMenuManager mmm;
    private Gun gun;

    /// <summary>
    /// Called once before the first execution of Update after the MonoBehaviour is created.
    /// Initializes the gun reference and hides the pause menu UI.
    /// </summary>
    void Start()
    {
        gun = FindFirstObjectByType<Gun>();
        pauseMenuUI.SetActive(false);
    }

    /// <summary>
    /// Called once per frame. Handles pause input and ensures gun reference is valid.
    /// </summary>
    void Update()
    {
        if (gun == null)
            gun = FindFirstObjectByType<Gun>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Resumes the game by toggling the pause state.
    /// </summary>
    public void ResumeGame()
    {
        TogglePause();
    }

    /// <summary>
    /// Restarts the current game scene, skipping the main menu and resetting stats.
    /// </summary>
    public void RestartGame()
    {
        IsRestarting = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Toggles the pause state of the game, showing or hiding the pause menu and adjusting the time scale accordingly.
    /// </summary>
    private void TogglePause()
    {
        fpUI.SetActive(isPaused);
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        // Activate or deactivate game objects based on pause state
        if (isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (gun != null)
                gun.SetPauseState(true);
            pauseMenuUI.SetActive(true);
            if (mmm != null && mmm.objectsToDeactivate != null)
            {
                foreach (var obj in mmm.objectsToDeactivate)
                {
                    if (obj != null && obj.name != "FirstPersonController")
                    {
                        bool shouldBeActive = !isPaused;
                        if (obj.activeSelf != shouldBeActive)
                            obj.SetActive(shouldBeActive);
                    }
                }
            }
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (gun != null)
                gun.SetPauseState(false);
            pauseMenuUI.SetActive(false);
            if (mmm != null && mmm.objectsToDeactivate != null)
            {
                foreach (var obj in mmm.objectsToDeactivate)
                {
                    if (obj != null && obj.name != "FirstPersonController")
                    {
                        bool shouldBeActive = !isPaused;
                        if (obj.activeSelf != shouldBeActive)
                            obj.SetActive(shouldBeActive);
                    }
                }
            }
        }
    }
}
