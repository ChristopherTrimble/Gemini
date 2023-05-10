//Author: Christopher Trimble
using TMPro;
using UnityEngine;

public class UI_Request : MonoBehaviour
{
    [SerializeField] private SO_NPCRequest requestsSO;
    [SerializeField] private TextMeshProUGUI requestInfo;

    public void SetSO(SO_NPCRequest request)
    {
        requestsSO = request;

        string info = requestsSO.requestReason + " " + requestsSO.NPCName + " needs <br>";
        for (int i = 0; i < requestsSO.request.Length; i++)
        {
            info += requestsSO.request[i].amounts + " " + requestsSO.request[i].resources.ToString() + " ";
        }

        requestInfo.text = info;
    }

    public void SetFinished()
    {
        requestInfo.text = "<s>" + requestInfo.text + "</s>";
    }
}
