//Author: Lauren Davis
using UnityEngine;
using System.Collections.Generic;

namespace RequestSystem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/New SO_RequestDatabase")]
    public class SO_RequestDatabase : ScriptableObject
    {
        private List<string> requestNames;
        [SerializeField] private List<SO_NPCRequest> requests;

        public void SetRequestStages(List<string> names, List<bool> requestStage)
        {
            SetupDatabase();
            for (int i = 0; i < requestNames.Count; i++)
            {
                SO_NPCRequest request = Resources.Load<SO_NPCRequest>("NPCRequests/" + requestNames[i]);
                request.requestStage = names.Contains(request.requestName) ? requestStage[names.IndexOf(request.requestName)] : false;
            }
        }
        
        private void SetupDatabase()
        {
            requestNames = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                requestNames.Add(requests[i].requestName);
        }
    }
}