using UnityEngine;

namespace BombGame
{
    [System.Serializable]
    internal class GameData
    {
        public Vector3 MinTargetPosition;
        public Vector3 MaxTargetPosition;

        public float GameplayTimer;
        public State GameState;

        public bool SpawnEnemies;

        public float BombTimer;

        public int LevelMoney;

    }

    public enum State
    {
        MainMenu,
        Gameplay,
        Finish,
        Shop
    }

}