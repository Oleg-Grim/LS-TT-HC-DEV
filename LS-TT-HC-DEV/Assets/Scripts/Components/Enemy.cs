using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Client
{
    internal struct Enemy
    {
        public float maxHealth;
        public float currentHealth;
        public GameObject avatar;
        public Vector3 position;
        public Vector3 target;
        public float speed;
        public EcsEntity spawnerRef;

        public Text healthText;
        public Slider healthSlider;
    }
}