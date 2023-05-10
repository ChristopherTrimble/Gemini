using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

//Author: Lauren Davis

public class MazeNode : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] GameObject floor;

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false); 
    }

    public void ActivateArrowTrap(int wallToActivate)
    {
        walls[wallToActivate].gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DeactivateArrowTrap(int wallToActivate)
    {
        walls[wallToActivate].gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ActivateSpikeTrap()
    {
        floor.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void DeactivateSpikeTrap()
    {
        floor.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
