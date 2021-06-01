using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    [System.Serializable]
    public class Configuration
    {
        [Header("Refereces")]
        public BoxCollider worldBox;
        public Camera worldCam;
        public GameObject enemyPrefab;
        public GameObject playerBomb;
        public GameObject spawerFX;
        public GameObject bombFX;

        [Header("Initial Settings")]
        public float gameDefaultTimer = 25;
        public int spawnersDefaultCount = 3;
        public int diffDefaultCounter = 3;
        public int difficultyDefaultSpawners = 0;
        public float bombRadius = 2;
        public float bombDamage;
        public float enemyHealthMultiplier = 1;
        public float enemyDefaultHealth = 10;

        [HideInInspector] public enum GameState {mainMenu, gameplay, loseScreen, WinScreen };
        [HideInInspector] public EcsEntity[] ecsEntities;
        
        [HideInInspector] public int levelDefaultCount = 1;
        [HideInInspector] public int spawnersCount = 3;
        [HideInInspector] public Vector3 worldMaxBounds;
        [HideInInspector] public Vector3 worldMinBounds;
        [HideInInspector] public float worldHeight;
        [HideInInspector] public float worldWidth;
        [HideInInspector] public Vector3 worldUnit;
        [HideInInspector] public float spawnLine;
        [HideInInspector] public float loseLine;
        [HideInInspector] public float spawnerHorizotal;
        [HideInInspector] public float spawnerHorizotalOffset;
        [HideInInspector] public int difficultySpawners = 0;
        [HideInInspector] public float gameTimer = 25;
        [HideInInspector] public GameState gameState;
        [HideInInspector] public int levelCount = 1;
        [HideInInspector] public int diffCounter = 3;
        [HideInInspector] public bool resetGame;
    }
}