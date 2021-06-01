using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class WorldSetUpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        private Configuration _config;


        public void Init()
        {
            _config.worldMaxBounds = _config.worldBox.bounds.max;
            _config.worldMinBounds = _config.worldBox.bounds.min;
            _config.spawnersCount = _config.spawnersDefaultCount + _config.difficultySpawners;
            _config.worldHeight = _config.worldMaxBounds.z - _config.worldMinBounds.z;
            _config.worldWidth = _config.worldMaxBounds.x - _config.worldMinBounds.x;

            _config.worldUnit = new Vector3(_config.worldWidth / 10, 0, _config.worldHeight / 10);
            _config.spawnLine = _config.worldMaxBounds.z - _config.worldUnit.z;
            _config.loseLine = _config.worldMinBounds.z + _config.worldUnit.z * 2;
            _config.spawnerHorizotalOffset = _config.worldWidth / (_config.spawnersCount + 1);

            float lastSpawnerHorizontalPosition = _config.worldMinBounds.x + _config.spawnerHorizotalOffset;

            _config.ecsEntities = new EcsEntity[_config.spawnersCount];

            for (int i = 0; i < _config.spawnersCount; i++)
            {
                _config.ecsEntities[i] = _world.NewEntity();

                Vector3 nextSpawnerPosition = new Vector3(lastSpawnerHorizontalPosition, 0.35f, _config.spawnLine);

                _config.ecsEntities[i].Get<Position>().value = nextSpawnerPosition;
                _config.ecsEntities[i].Get<Spawner>();
                _config.ecsEntities[i].Get<SpawnerFX>().spawnerFX = _config.spawerFX;
                lastSpawnerHorizontalPosition = nextSpawnerPosition.x + _config.spawnerHorizotalOffset;
            }

            _config.resetGame = false;
        }

        public void Run()
        {
            if(_config.resetGame)
            {
                Init();
            }
        }
    }
}