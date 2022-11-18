using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Interfaces;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace AI
{
    public class EnemyFollowAI : MonoBehaviour
    {
        private bool isPlayerInChaseRadius;
        private GameObject target;
        private Rigidbody rb;
        private bool isLungeOnCooldown;
        public float chaseSpeed;
        public float lungeTimer;
        public float lungeForce;


        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!isPlayerInChaseRadius) return;
            
            ChasePlayer();

            if (!isLungeOnCooldown)
            {
                StartCoroutine(Lunge());
            }
        }

        private IEnumerator Lunge()
        {
            isLungeOnCooldown = true;
            yield return new WaitForSeconds(lungeTimer);
            
            var lungeDirection = target.transform.position - this.transform.position;
            lungeDirection = lungeDirection.normalized;
            rb.AddForce(lungeDirection * lungeForce, ForceMode.Impulse);
            Debug.Log("LUNGE");

            isLungeOnCooldown = false;
        }

        private void ChasePlayer()
        {
            var direction = target.transform.position - this.transform.position;
            direction = direction.normalized;
            transform.Translate(direction * (chaseSpeed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GetComponent<SphereCollider>().radius = GetComponent<SphereCollider>().radius * 1.5f;
                target = other.gameObject;
                isPlayerInChaseRadius = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GetComponent<SphereCollider>().radius = GetComponent<SphereCollider>().radius / 1.5f;
                isPlayerInChaseRadius = false;
                target = null;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<IDamageable>().Damage(10f);
            }
        }
    }
}
