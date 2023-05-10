// Author: Christopher Trimble

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private SO_ConversationEvent dialogueStart;
    [SerializeField] private SO_VoidEvent dialogueEnd;
    [SerializeField] private SO_VoidEvent openMenu;
    [SerializeField] private SO_VoidEvent closeMenu;
    [SerializeField] private GameObject abilityIndication, abilityBackground;
    private GameObject imageObject, imageBackground;
    private bool isPaused, inDialogue;
    void Start()
    {
        dialogueStart = Resources.Load<SO_ConversationEvent>("ConversationEvents/StartConversation");
        dialogueEnd = Resources.Load<SO_VoidEvent>("VoidEvents/EndConversation");
        dialogueStart.OnEventCall += OpenDialogue;
        dialogueEnd.OnEventCall += CloseDialogue;
        
        openMenu = Resources.Load<SO_VoidEvent>("Events/OpenMenu");
        openMenu.OnEventCall += OpenMenu;
        
        closeMenu = Resources.Load<SO_VoidEvent>("Events/CloseMenu");
        isPaused = false;
        inDialogue = false;

        imageObject = GameObject.FindGameObjectWithTag("AbilityNumber");
        imageBackground = GameObject.FindGameObjectWithTag("AbilityBackground");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPaused && !inDialogue)
            openMenu.EventCall();
        else if(Input.GetKeyDown(KeyCode.P) && isPaused)
            CloseMenu();
    }

    public void OpenDialogue(Conversation conversation)
    {
        inDialogue = true;
    }

    public void CloseDialogue()
    {
        inDialogue = false;
    }
    
    public void OpenMenu()
    {
        abilityIndication.SetActive(false);
        abilityBackground.SetActive(false);
        menu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void CloseMenu()
    {
        abilityIndication.SetActive(true);
        abilityBackground.SetActive(true);
        menu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState  = CursorLockMode.Locked;
        isPaused = false;
        closeMenu.EventCall();
    }

    public void QuitGame()
    {
        menu.SetActive(false);
        SceneManager.LoadScene("Scene_MainMenu");
    }

    private void OnDestroy()
    {
        openMenu.OnEventCall = null;
        closeMenu.OnEventCall = null;
        dialogueStart.OnEventCall = null;
        dialogueEnd.OnEventCall = null;
    }
}
