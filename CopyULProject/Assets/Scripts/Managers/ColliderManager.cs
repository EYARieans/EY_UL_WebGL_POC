using EY.Utility.Collision;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EY.Managers.Collider
{
    public class ColliderManager : MonoBehaviour
    {
        public Action OnMainGateEntry { get; set; }
        public Action OnMainGateExit { get; set; }
        public Action OnLaboratoryEntry { get; set; }
        public Action OnLaboratoryExit { get; set; }
        public Action OnTerraceEntry { get; set; }
        public Action OnTerraceExit { get; set; }

        [SerializeField] private CollisionActivator MainGateCollider;
        [SerializeField] private CollisionActivator LaboratoryCollider;
        [SerializeField] private CollisionActivator TerraceCollider;

        // Start is called before the first frame update
        void Start()
        {
            MainGateCollider.OnEnter += OnMainGateEntry;
            LaboratoryCollider.OnEnter += OnLaboratoryEntry;
            TerraceCollider.OnEnter += OnTerraceEntry;
        }
    }
}

