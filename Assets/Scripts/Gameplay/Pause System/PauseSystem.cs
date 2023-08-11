using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

namespace GT
{
    public class PauseSystem : MonoBehaviour
    {
        [SerializeField] private bool startPaused = false;

        public static Action<PauseStates> OnPauseStateChange;
        public static bool Paused { get; private set; }

        private static bool isGameRunning = true;

        private void Awake()
        {
            InputManager.OnPausePress += PauseControl;
        }
        private void OnDestroy()
        {
            InputManager.OnPausePress -= PauseControl;
        }

        private void Start()
        {
            isGameRunning = true;
            Paused = !startPaused;
            PauseControl();
        }

        public static void PauseControl()
        {
            if (isGameRunning)
            {
                Paused = !Paused;
                Time.timeScale = Paused ? 0 : 1;
                PauseStates actualState = Paused ? PauseStates.Paused : PauseStates.Resumed;
                Debug.Log("Envio estado de pausa: " + Paused);
                OnPauseStateChange?.Invoke(actualState);
            }
        }

    }
}