using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Lauren Davis
//Reference: https://youtu.be/gQCWt_01jDQ

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] public GameObject projectile;
    [SerializeField] public float moveSpeed;
    [SerializeField] public Transform spawnLocation;
    [SerializeField] private Vector3 offsetLocation = new Vector3(0, 0, 5f);
    [SerializeField] public Quaternion spawnRotation;

    [SerializeField] public float spawnTime = .5f;
    [SerializeField] public float timeSinceSpawned = 7f;

    [SerializeField] public Detection detectionZone;
    private SO_SoundManager soundManager;

    private void Start()
    {
        soundManager = Resources.Load<SO_SoundManager>("SO_SoundManager");
        moveSpeed = GetComponent<ProjectileArrow>().moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (detectionZone.detectedObjs.Count > 0)
        {
            timeSinceSpawned += Time.deltaTime;
            if (timeSinceSpawned >= spawnTime)
            {
                Instantiate(projectile, spawnLocation.position, spawnRotation);
                Instantiate(projectile, spawnLocation.position - offsetLocation, spawnRotation);
                Instantiate(projectile, spawnLocation.position + offsetLocation, spawnRotation);

                soundManager.PlayOnGameObject(this, SoundType.Arrows, gameObject, false);

                timeSinceSpawned = 0;
            }
        }
        else
        {
            timeSinceSpawned = 15f;
        }
    }
}
