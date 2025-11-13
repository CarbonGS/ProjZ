using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject[] objectsToDeactivate; // Assign all non-menu GameObjects here in Inspector
    public GameObject mmCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        menuUI.SetActive(true);
        foreach (var obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }

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

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
