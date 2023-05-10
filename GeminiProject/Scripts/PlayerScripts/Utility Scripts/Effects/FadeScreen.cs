using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    private float fadeSpeed;
    private string fadeMsg;
    private bool showMsg = false;
    public TextMeshProUGUI fadeMsgObject;
    public GameObject continueButton;
    private bool isBusy;

    private SO_VoidEvent respawnPlayer;
    private SO_VoidEvent transitionToFadeCanvas;
    [SerializeField] private GraphicRaycaster fadeRaycaster;
    
    
    private void Awake()
    {
        respawnPlayer = Resources.Load<SO_VoidEvent>("Events/RespawnPlayer");
        transitionToFadeCanvas = Resources.Load<SO_VoidEvent>("Events/TransitionToFadeCanvas");
    }

    public void FadeOut(float speed)
    {
        if(isBusy)
            return;
        fadeRaycaster.enabled = true;
        isBusy = true;
        Debug.Log("Fade Out");
        transitionToFadeCanvas.EventCall();
        fadeSpeed = speed;
        StartCoroutine(FadeOutTimer());
    }
    
    public void FadeOut(float speed, string msg)
    {
        if(isBusy)
            return;

        fadeRaycaster.enabled = true;
        isBusy = true;
        Debug.Log("Fade Out With Message");
        transitionToFadeCanvas.EventCall();
        fadeSpeed = speed;
        showMsg = true;
        fadeMsg = msg;
        StartCoroutine(FadeOutTimer());
        Cursor.visible = true;
    }

    public void FadeIn()
    {
        fadeRaycaster.enabled = false;
        isBusy = false;
        Debug.Log("Fade In");
        transitionToFadeCanvas.EventCall();
        Cursor.visible = false;
        fadeSpeed = -fadeSpeed;
        StartCoroutine(FadeInTimer());
    }

    private IEnumerator FadeOutTimer()
    {
        fadeMsgObject.gameObject.SetActive(false);
        continueButton.SetActive(false);
        
        var color = new Color(0, 0, 0, 0);
        while (color.a < 1f)
        {
            color = gameObject.GetComponent<Image>().color;
            color.a += 1f * fadeSpeed * Time.deltaTime;
        
            gameObject.GetComponent<Image>().color = color;
            yield return null;
        }

        if (showMsg)
        {
            fadeMsgObject.gameObject.SetActive(true);
            fadeMsgObject.text = fadeMsg;
        }

        showMsg = false;
        
        respawnPlayer.EventCall();
        Cursor.lockState = CursorLockMode.None;
        continueButton.SetActive(true);
        Debug.Log("FADE DONE");
    }
    
    private IEnumerator FadeInTimer()
    {
        fadeMsgObject.gameObject.SetActive(false);
        continueButton.SetActive(false);
        
        var color = new Color(0, 0, 0, 255);
        while (color.a != 0f)
        {
            if (color.a < 0)
            {
                color = new Color(0, 0, 0, 0);
                gameObject.GetComponent<Image>().color = color;
                break;
            }

            color = gameObject.GetComponent<Image>().color;
            color.a += 1f * fadeSpeed * Time.deltaTime;
        
            gameObject.GetComponent<Image>().color = color;
            yield return null;
        }

        if (showMsg)
        {
            fadeMsgObject.gameObject.SetActive(true);
            fadeMsgObject.text = fadeMsg;
        }

        showMsg = false;
        
        Cursor.lockState = CursorLockMode.Confined;
        continueButton.SetActive(false);
        Debug.Log("FADE DONE");
    }
}
