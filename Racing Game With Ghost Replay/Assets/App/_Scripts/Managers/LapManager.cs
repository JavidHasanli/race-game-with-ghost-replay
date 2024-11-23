using RacingGhost.RaceElements;
using RacingGhost.ScriptableObjects;
using RacingGhost.UI.Controllers;
using System;
using UnityEngine;

namespace RacingGhost.Managers
{
    public class LapManager : MonoBehaviour
    {
        public static event Action<int> OnLapCompletedEvent;
        public static event Action OnRaceEndedEvent;

        #region Links&Properties

        private int _currentLap = 1;
        private int _targetLaps;
        private bool _finishLineWasCrossed = false;

        #endregion

        #region Unity Methods       

        void Awake()
        {
            Initialize();
        }

        void OnEnable()
        {
            GameManager.OnGameInitializedEvent += OnGameInitializedEventHandler;
            Car.OnFinishLineCrossedEvent += OnFinishLineCrossedEventHandler;
            GamePlayViewController.OnRaceRestartedEvent += OnRaceRestartedEventHandler;
        }

        void OnDisable()
        {
            GameManager.OnGameInitializedEvent -= OnGameInitializedEventHandler;
            Car.OnFinishLineCrossedEvent -= OnFinishLineCrossedEventHandler;
            GamePlayViewController.OnRaceRestartedEvent -= OnRaceRestartedEventHandler;
        }

        #endregion

        #region Custom Methods

        private void Initialize()
        {
            _finishLineWasCrossed = false;
            _currentLap = 1;
        }

        #endregion

        #region Event Handlers

        private void OnGameInitializedEventHandler(GameSettings settings)
        {
            _targetLaps = settings.TotalLaps;
        }

        private void OnFinishLineCrossedEventHandler()
        {
            if (!_finishLineWasCrossed)
            {
                _finishLineWasCrossed = true;
                return;
            }

            _currentLap++;

            if (_currentLap > _targetLaps)
            {
                Debug.Log("Race Ended");
                OnRaceEndedEvent?.Invoke(); // Notify listeners that race has ended
            }
            else
            {
                OnLapCompletedEvent?.Invoke(_currentLap); // Notify listeners that player completed a lap
            }
        }

        private void OnRaceRestartedEventHandler()
        {
            Initialize();
        }

        #endregion
    }
}

