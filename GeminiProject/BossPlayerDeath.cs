using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class BossPlayerDeath : MonoBehaviour
{
    private bool done = true;
    private int direction = 1;
    private bool buttonClicked = false;
    [SerializeField] private Image image;
    private GameObject player;
    private GameObject canvas;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (done)
            StartCoroutine(PulseateAlpha());
    }

    private IEnumerator PulseateAlpha()
    {
        done = false;
        for (int i = 0; i < 65; i++)
        {
            Color color = image.color;
            color.a += direction * 0.00392157f;
            image.color = color;
            yield return new WaitForSecondsRealtime(0.005f);
        }
        direction = -direction;
        done = true;
    }
    
    public void BackToTownClick()
    {
        if (buttonClicked) return;
        SceneManager.LoadSceneAsync("Town");
        buttonClicked = true;
    }

    public void RetryBossClick()
    {
        if (buttonClicked) return;
        SceneManager.LoadSceneAsync("Boss");
        buttonClicked = true;
    }
}
