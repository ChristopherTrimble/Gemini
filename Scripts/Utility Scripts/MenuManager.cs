using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private SO_VoidEvent openMenu;
    [SerializeField] private SO_VoidEvent closeMenu;
    void Start()
    {
        openMenu = Resources.Load<SO_VoidEvent>("Events/OpenMenu");
        openMenu.OnEventCall += OpenMenu;
        
        closeMenu = Resources.Load<SO_VoidEvent>("Events/CloseMenu");
    }

    private void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        closeMenu.EventCall();
    }

    public void QuitGame()
    {
        menu.SetActive(false);
        SceneManager.LoadScene("Scene_MainMenu");
    }
}
