using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIndicator : MonoBehaviour
{
    private SO_ConversationEvent conversationStart;
    private SO_VoidEvent conversationEnd;
    private Image iconImage;

    private void Awake()
    {
        conversationStart = Resources.Load<SO_ConversationEvent>("ConversationEvents/startConversation");
        conversationStart.OnEventCall += HideIcon;
        conversationEnd = Resources.Load<SO_VoidEvent>("VoidEvents/EndConversation");
        conversationEnd.OnEventCall += ShowIcon;

        iconImage = GetComponent<Image>();
    }

    private void ShowIcon()
    {
        iconImage.enabled = true;
    }

    private void HideIcon(Conversation arg0)
    {
        iconImage.enabled = false;
    }

    private void OnDestroy()
    {
        conversationEnd.OnEventCall = null;
        conversationStart.OnEventCall = null;
    }
}
