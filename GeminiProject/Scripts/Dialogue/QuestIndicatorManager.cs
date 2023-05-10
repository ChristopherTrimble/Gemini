using System.Collections.Generic;
using UnityEngine;

public class QuestIndicatorManager : MonoBehaviour
{
    public static QuestIndicatorManager instance;
    [SerializeField] private GameObject[] npcIndicators;
    [SerializeField] private string[] npcNames;
    private Dictionary<string, GameObject> indicators;

    private void Awake()
    {
        SingletonInitialization();
        IndicatorDictionaryInitialization();
    }

    private void IndicatorDictionaryInitialization()
    {
        indicators = new Dictionary<string, GameObject>();

        for (int i = 0; i < npcNames.Length; i++)
        {
            indicators.Add(npcNames[i], npcIndicators[i]);
        }
    }

    private void SingletonInitialization()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public void ActivateIndicator(string npcName)
    {
        indicators[npcName].SetActive(true);
    }
    
    public void DeactivateIndicator(string npcName)
    {
        indicators[npcName].SetActive(false);
    }
}
