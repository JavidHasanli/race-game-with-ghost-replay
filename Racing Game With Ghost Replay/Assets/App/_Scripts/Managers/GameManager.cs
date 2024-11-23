using RacingGhost.ScriptableObjects;
using RacingGhost.UI.Controllers;
using System;
using System.Collections;
using UnityEngine;

namespace RacingGhost.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Links&Properties

        public static event Action<GameSettings> OnGameInitializedEvent;
        public static event Action<int> OnCountdownTickEvent;
        public static event Action OnCountdownEndEvent;

        [SerializeField] private GameSettings _gameSettings;

        #endregion

        #region Unity Methods

        void Start()
        {
            OnGameInitializedEvent?.Invoke(_gameSettings); // Notify listeners that game has been initialized
        }

        void OnEnable()
        {
            MenuViewController.OnPlayerStartedRaceEvent += OnPlayerStartedRaceEventHandler;
            GamePlayViewController.OnRaceRestartedEvent += OnRaceRestartedEventHandler;
        }

        void OnDisable()
        {
            MenuViewController.OnPlayerStartedRaceEvent -= OnPlayerStartedRaceEventHandler;
            GamePlayViewController.OnRaceRestartedEvent -= OnRaceRestartedEventHandler;
        }

        #endregion

        #region Custom Methods

        // Starts coroutine for countdown to avoid doing it in Update.
        public void StartCountdown(int duration)
        {
            StartCoroutine(CountdownCoroutine(duration));
        }

        private IEnumerator CountdownCoroutine(int duration)
        {
            int remainingTime = duration;

            while (remainingTime > 0)
            {
                OnCountdownTickEvent?.Invoke(remainingTime); // Notify listeners of the current time
                yield return new WaitForSeconds(1f);
                remainingTime--;
            }

            OnCountdownEndEvent?.Invoke(); // Notify listeners that the countdown is complete
        }


        #endregion

        #region Event Handlers

        private void OnPlayerStartedRaceEventHandler()
        {
            StartCountdown(_gameSettings.CountdownDuration);
        }

        private void OnRaceRestartedEventHandler()
        {
            StartCountdown(_gameSettings.CountdownDuration);
        }

        #endregion
    }
}

