// Script for having a typewriter effect for UI - Version 2
// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)
// Want to get creative? Try a Unicode leading character(https://unicode-table.com/en/blocks/block-elements/)
// Copy Paste from page into Inspector

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
	public class TypeWriter : MonoBehaviour
	{
		private SO_VoidEvent closeMenuEvent;
		private TextMeshProUGUI currentTMP;
		private string writer;

		public bool isBusy;

		[SerializeField] private float delayBeforeStart;
		[SerializeField] private float timeBtwChars = 0.1f;
		[SerializeField] private string leadingChar = "";
		[SerializeField] private bool leadingCharBeforeDelay;

		[SerializeField] private AudioClip[] writingSounds;
		[SerializeField] private AudioSource audioSource;

		private bool isTyping;

		// Use this for initialization
		void Awake()
		{
			closeMenuEvent = Resources.Load<SO_VoidEvent>("VoidEvents/DialogueMenuClose");
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(1) && GetComponent<Canvas>().enabled)
			{
				if (!isTyping)
				{
					closeMenuEvent.OnEventCall();
					audioSource.Stop();
				}
			
				StopAllCoroutines();
				currentTMP.text = writer;
				audioSource.Stop();
				isTyping = false;
				isBusy = false;
			}
		}

		public void StartTypewriter(string text, TextMeshProUGUI tmpProText)
		{
			isBusy = true;
			StopAllCoroutines();
			currentTMP = tmpProText;

			if (tmpProText != null)
			{
				writer = text;
			
				tmpProText.text = "";

				StartCoroutine(nameof(TypeWriterTMP));
			}
		}

		private void OnDisable()
		{
			StopAllCoroutines();
		}

		IEnumerator TypeWriterTMP()
		{
			isTyping = true;
			currentTMP.text = leadingCharBeforeDelay ? leadingChar : "";

			yield return new WaitForSeconds(delayBeforeStart);

			audioSource.PlayOneShot(writingSounds[Random.Range(0, writingSounds.Length)]);
        
			foreach (char c in writer)
			{
				if (currentTMP.text.Length > 0)
				{
					currentTMP.text = currentTMP.text.Substring(0, currentTMP.text.Length - leadingChar.Length);
				}
				currentTMP.text += c;
				currentTMP.text += leadingChar;
				yield return new WaitForSeconds(timeBtwChars);
			}
		
			audioSource.Stop();

			if (leadingChar != "")
			{
				currentTMP.text = currentTMP.text.Substring(0, currentTMP.text.Length - leadingChar.Length);
			}

			isTyping = false;
			isBusy = false;
		}

		private void OnDestroy()
		{
			closeMenuEvent.OnEventCall = null;
		}
	}
}