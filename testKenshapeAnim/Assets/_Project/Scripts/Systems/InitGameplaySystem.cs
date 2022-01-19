using Leopotam.Ecs;

namespace BombGame
{
    internal class InitGameplaySystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly GameData _data = null;
        private readonly Configuration _config = null;
        private readonly References _refs = null;
        
        public void Init()
        {
            var control = _world.NewEntity();
            control.Get<Control>();
            _refs._uiCore.Control = control;
            
            for (int i = 0; i < _refs.SpawnPoints.Length; i++)
            {
                var spawnerEntity = _world.NewEntity();
                ref var spawner = ref spawnerEntity.Get<Spawner>();

                spawner.Timer = i * 3;
                spawner.Position = _refs.SpawnPoints[i].position;
            }
            
            _data.GameplayTimer = _config.LevelTimerDefault;
            _data.GameState = State.MainMenu;
        }
    }
}