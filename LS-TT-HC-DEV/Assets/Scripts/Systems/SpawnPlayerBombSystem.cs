using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class SpawnPlayerBombSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private Configuration _config;

        private float cooldown = 0.5f;
        private float baseCooldown = 0.5f;

        public void Run()
        {
            if (cooldown < 0)
            {
                if (_config.gameState == Configuration.GameState.gameplay)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        var cam = _config.worldCam;
                        var ray = cam.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;

                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            var bombPosition = new Vector3(hitInfo.point.x, 5, hitInfo.point.z);

                            var bomb = _world.NewEntity();
                            bomb.Get<ActiveBomb>();
                            bomb.Get<Bomb>().position = bombPosition;

                            var tmp = GameObject.Instantiate(_config.playerBomb, bombPosition, Quaternion.identity);
                            bomb.Get<Bomb>().avatar = tmp;
                            bomb.Get<Bomb>().speed = 10;

                            cooldown = baseCooldown;
                        }
                    }
                }
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}