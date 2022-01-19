using UnityEngine;

namespace BombGame
{
    [System.Serializable]
    public class Configuration
    {
        public int CurrentLevel;
        
        [Header("Enemy Settings")]
        public float LevelTimerDefault;
        public float EnemyHealth;
        public float SpawnTimerDefault;
        
        [Header("Player Settings")]
        public float BombTimerDefault;
        public float BombFallSpeed;
        public float BombDamage;
        public float BombRadius;
        public int PlayerMoney;

        public Configuration(Configuration data)
        {
            CurrentLevel = data.CurrentLevel;
            LevelTimerDefault = 30;

            EnemyHealth = data.EnemyHealth;
            SpawnTimerDefault = data.SpawnTimerDefault;
            BombTimerDefault = data.BombTimerDefault;
            BombFallSpeed = data.BombFallSpeed;
            BombDamage = data.BombDamage;
            BombRadius = data.BombRadius;
        }

        public Configuration()
        {
            CurrentLevel = 1;
            LevelTimerDefault = 30;

            EnemyHealth = 1;
            SpawnTimerDefault = 10;
            BombTimerDefault = 1;
            BombFallSpeed = 7.5f;
            BombDamage = 5;
            BombRadius = 1.5f;
        }
    }
}