using InputSettings;
using PlayerScripts;
using UnityEngine;

//Author: Lauren Davis

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerStats>().Respawn();
        }
    }
}
