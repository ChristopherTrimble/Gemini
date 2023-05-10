using System;
using System.Collections;
using TMPro;
using UI;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    private Canvas dialogueUiCanvas;
    private SO_ConversationEvent startConversation;
    private SO_VoidEvent dialogueStateTransition;
    private SO_VoidEvent endConversation;
    [SerializeField] private DialogueEventListeners dialogueListener;
    
    private SO_DialogueManager dialogueManager;
    private Conversation currentConversation;

    [SerializeField] private TextMeshProUGUI mainTMP;
    [SerializeField] private TextMeshProUGUI option1TMP;
    [SerializeField] private TextMeshProUGUI option2TMP;
    [SerializeField] private TextMeshProUGUI speakerTMP;
    [SerializeField] private GameObject option1Background;
    [SerializeField] private GameObject option2Background;
    
    
    
    [SerializeField] private TypeWriter typeWriter;

    private int optionChoice = -1;

    private bool isOption1Valid;
    private bool isOption2Valid;

    private void Awake()
    {
        startConversation = Resources.Load<SO_ConversationEvent>("ConversationEvents/StartConversation");
        endConversation = Resources.Load<SO_VoidEvent>("VoidEvents/EndConversation");
        dialogueStateTransition = Resources.Load<SO_VoidEvent>("VoidEvents/DialogueStateTransition");
        startConversation.OnEventCall += OnConversationStart;

        dialogueManager = Resources.Load<SO_DialogueManager>("SO_DialogueManager");

        dialogueUiCanvas = GetComponent<Canvas>();
    }

    private void OnDisable()
    {
        endConversation.EventCall();
    }

    private void OnConversationStart(Conversation conversation)
    {   
        dialogueStateTransition.EventCall();
        currentConversation = conversation;
        dialogueUiCanvas.enabled = true;

        //Need to find desired conversation ID
        var speaker = currentConversation.dialogues[0].speaker;
        var conversationId = dialogueManager.GrabConversationID(speaker);
        
        //Transition into Dialogue State
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
         //need to block movement
         //need to block light abilities

         StartCoroutine(RunDialogue(currentConversation, conversationId));
    }

    private IEnumerator RunDialogue(Conversation conversation, int conversationId)
    {
        //Blanks out all text fields at beginning of conversation
        mainTMP.text = "";
        option1TMP.text = "";
        option2TMP.text = "";
        speakerTMP.text = "";
        option1Background.SetActive(false);
        option2Background.SetActive(false);
        
        //Get valid conversation
        var currentDialogue = conversation.GetDialogue(conversationId);

        speakerTMP.text = currentDialogue.speaker; //Writes speaker name
        
        //Check for event
        if (currentDialogue.hasEvent)
        {
            var isSuccess = dialogueListener.RequestListener(currentDialogue.eventString);

            foreach (var option in currentDialogue.options)
            {
                option.isValid = option.conditions.Contains(isSuccess);
            }
        }
        
        //Display first dialogue
        while (typeWriter.isBusy)
        {
            yield return null;
        }
        
        var text = currentDialogue.text;
        typeWriter.StartTypewriter(text, mainTMP);
        
        //check if terminal
        while (typeWriter.isBusy)
        {
            yield return null;
        }
        
        if (currentDialogue.isTerminal)
        {
            var exit = false;
            
            while (!exit)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    exit = true;
                }

                yield return null;
            }
            
            Debug.Log("Terminal");
            dialogueUiCanvas.enabled = false;
            dialogueManager.SetConversationID(currentDialogue.speaker,
                !currentDialogue.isRepeatable ? currentDialogue.nextValidId : currentDialogue.id);
            //Click to exit
            
            endConversation.EventCall();
            dialogueStateTransition.EventCall();
            yield break;
        }
        
        //check for options
        if (currentDialogue.hasOptions)
        {
            int index = 0;
            
            foreach (var option in currentDialogue.options)
            {
                if (option is { isValid: true })
                {
                    while (typeWriter.isBusy)
                    {
                        yield return null;
                    }

                    if (index == 0)
                    {
                        option1Background.SetActive(true);
                        typeWriter.StartTypewriter(option.text, option1TMP);
                    }

                    if (index == 1)
                    {
                        option2Background.SetActive(true);
                        typeWriter.StartTypewriter(option.text, option2TMP);
                    }
                }
                index++;
            }
        }
        
        //Get options choice
        while (optionChoice == -1)
        {
            yield return null;
        }
        
        //Go to that dialogue ID
        var nextDialogue = currentDialogue.options[optionChoice].nextId;
        //repeat
        if (nextDialogue == -1)
        {
            var exit = false;
            
            while (!exit)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    exit = true;
                }

                yield return null;
            }
            
            dialogueUiCanvas.enabled = false;
            optionChoice = -1;
        }
        else
        {
            optionChoice = -1;
            StartCoroutine(RunDialogue(currentConversation, nextDialogue));
        }
    }

    public void SetOptionChoice(int choice)
    {
        optionChoice = choice;
    }

    private void OnDestroy()
    {
        startConversation.OnEventCall = null;
        endConversation.OnEventCall = null;
        dialogueStateTransition.OnEventCall = null;
    }
}
