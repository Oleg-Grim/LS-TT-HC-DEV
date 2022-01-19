using UnityEngine;
using UnityEngine.UI;

namespace BombGame
{
    [System.Serializable]
    internal class References
    {
        public Camera Cam;
        public UICore _uiCore;
        
        public GameObject EnemyPrefab;
        public GameObject BombPrefab;
        public GameObject BombFX;

        public Transform[] SpawnPoints;

    }
}