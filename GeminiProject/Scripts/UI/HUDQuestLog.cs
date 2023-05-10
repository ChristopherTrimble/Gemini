using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class HUDQuestLog : MonoBehaviour
    {
        private Canvas questLogCanvas;

        [SerializeField] private float fadeTimer;

        private float countingTimer;

        private SO_VoidEvent showQuestLog;
        

        private void Awake()
        {
            countingTimer = fadeTimer;
            
            questLogCanvas = GetComponent<Canvas>();

            showQuestLog = Resources.Load<SO_VoidEvent>("VoidEvents/ShowQuestLog");
            showQuestLog.OnEventCall += ShowQuestLog;
        }

        private void Update()
        {
            questLogCanvas.enabled = countingTimer > 0;

            countingTimer = countingTimer -= Time.deltaTime;
            //Debug.Log(countingTimer);
        }

        private void ShowQuestLog()
        {
            countingTimer = fadeTimer;
        }

        private IEnumerator QuestHudFade()
        {
            
        
        
            yield return null;
        }

        private void OnDestroy()
        {
            showQuestLog.OnEventCall = null;
        }
    }
}
