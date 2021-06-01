using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class UI_Controller : MonoBehaviour
    {
        public GameObject[] gamescreens;
        public GameObject activeUI;

        public Text timerText;
        public Text levelText;
        public EcsEntity continueButton;

        public void TurnUI(int index)
        {
            for (int i = 0; i < gamescreens.Length; i++)
            {
                gamescreens[i].gameObject.SetActive(false);
            }
                activeUI = gamescreens[index];
                activeUI.SetActive(true);
        }

        public void Continue()
        {
            continueButton.Get<WinState>();
        }

        public void Reset()
        {
            continueButton.Get<LoseState>();
        }
    }
}