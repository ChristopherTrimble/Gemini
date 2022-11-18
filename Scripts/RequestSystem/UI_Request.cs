using TMPro;
using UnityEngine;

public class UI_Request : MonoBehaviour
{
    [SerializeField] private SO_NPCRequest requestsSO;
    [SerializeField] private TextMeshProUGUI requestInfo;

    public void SetSO(SO_NPCRequest request)
    {
        requestsSO = request;

        string info = requestsSO.NPCName + " wants ";
        for (int i = 0; i < requestsSO.resourcesRequested.Length; i++)
        {
            string type = requestsSO.resourcesRequested[i] == 0 ? "wood" : "stone";
            info += requestsSO.amountsRequested[i].ToString() + " " + type + " ";
        }
        info += "for " + requestsSO.requestReason;

        requestInfo.text = info;
    }

    public void DestroyThisUI()
    {
        Destroy(this.transform.gameObject);
    }
}
