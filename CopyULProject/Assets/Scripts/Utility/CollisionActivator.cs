using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EY.Utility.Collision
{
    [RequireComponent(typeof(BoxCollider))]
    public class CollisionActivator : MonoBehaviour
    {
        public Action OnEnter { get; set; }
        public Action OnExit { get; set; }
        private const string Player = "Player";

        void OnTriggerExit(Collider other)
        {
            if(other.CompareTag(Player)) OnExit?.Invoke();    
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Player))
            {
                Debug.Log("Enter");
                OnEnter?.Invoke();
            }
        }
    }
}

