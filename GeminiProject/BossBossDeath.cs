using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class BossBossDeath : MonoBehaviour
{
    private bool done = true;
    private int direction = 1;
    private bool buttonClicked = false;
    [SerializeField] private Image image;

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
