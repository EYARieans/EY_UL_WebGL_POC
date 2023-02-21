using EY.Managers.Sound;
using UnityEngine;
using EY.Managers.Collider;
using System;

namespace EY.Managers.Game
{
    public class GameManager : MonoBehaviour, IDisposable
    {
        [SerializeField] private LevelManager levelManager;

        private static bool created = false;

        //void Awake()
        //{
        //    if (!created)
        //    {
        //        DontDestroyOnLoad(this.gameObject);
        //        created = true;
        //    }
        //}

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DebugPrint(string msg)
        {
            Debug.Log(msg);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

