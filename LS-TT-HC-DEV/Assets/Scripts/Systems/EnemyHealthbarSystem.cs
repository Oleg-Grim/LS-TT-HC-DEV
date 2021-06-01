using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    internal class EnemyHealthbarSystem : IEcsRunSystem
    {
        EcsFilter<Enemy> _filter;
        public void Run()
        {
            if(!_filter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    var enemy = _filter.GetEntity(index);
                    var tmp = enemy.Get<Enemy>();
                    var tmpSlider = tmp.healthSlider = tmp.avatar.GetComponentInChildren<Slider>();
                    var tmpText = tmp.healthText = tmp.avatar.GetComponentInChildren<Text>();

                    tmpSlider.maxValue = tmp.maxHealth;
                    tmpSlider.minValue = 0;
                    tmpSlider.value = tmp.currentHealth;

                    string currentHealth = Mathf.CeilToInt(tmp.currentHealth).ToString("##");
                    string maxHealth = tmp.maxHealth.ToString("##");
                    string showHealth = currentHealth + "/" + maxHealth;
                    tmpText.text = showHealth;
                }
            }
        }
    }
}