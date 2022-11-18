using System;
using System.Collections;
using PlayerScripts;
using UnityEngine;

namespace AI
{
    public class RangedAI : MonoBehaviour
    {
        private bool isPlayerSighted;
        public float sightRadius = 5f;
        private SphereCollider sightTrigger;
        public float shootProjectileCooldown;
        private bool isOnCooldown;
        public GameObject projectile;
        public float projectileSpeed;
        private GameObject target;

        private void Awake()
        {
            sightTrigger = GetComponent<SphereCollider>();
            sightTrigger.radius = sightRadius;
        }

        private void Update()
        {
            if (isOnCooldown)
                return;
            
            if (isPlayerSighted)
            {
                StartCoroutine(ShootProjectileTimer());
            }
            else if (!isPlayerSighted)
            {
                StopCoroutine(ShootProjectileTimer());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            isPlayerSighted = true;
            target = other.gameObject;
            Debug.Log("PLAYER SEEN");
            sightTrigger.radius = sightTrigger.radius * 1.5f;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            isPlayerSighted = false;
            target = null;
            Debug.Log("PLAYER LOST");
            sightTrigger.radius = sightTrigger.radius / 1.5f;
        }

        private IEnumerator ShootProjectileTimer()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(shootProjectileCooldown);

            if (target != null)
            {
                var shotProjectile = Instantiate(projectile, transform.GetChild(0).position, Quaternion.identity);
                var shotDirection = target.transform.GetChild(2).position - this.transform.position;
                shotDirection = shotDirection.normalized;
                shotProjectile.GetComponent<Rigidbody>().AddForce(shotDirection * projectileSpeed, ForceMode.Impulse);
            }
            
            isOnCooldown = false;
        }
    }
}
