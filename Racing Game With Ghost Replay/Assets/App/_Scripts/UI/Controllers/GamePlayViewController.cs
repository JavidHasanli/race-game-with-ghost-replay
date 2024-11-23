using RacingGhost.Managers;
using RacingGhost.RaceElements;
using RacingGhost.ScriptableObjects;
using RacingGhost.UI.Views;
using System;
using System.Collections;
using UnityEngine;

namespace RacingGhost.UI.Controllers
{
    public class GamePlayViewController : MonoBehaviour
    {
        #region Links&Properties

        public static event Action OnRaceRestartedEvent;

        [Header("View")]
        [SerializeField] private GamePlayView _view;

        [Header("Properties")]
        [Tooltip("Disables countdown text after given time when countdown ends")]
        [SerializeField] private float _countdownTextLifetime = 2f;

        private int _targetLaps;

        #endregion

        #region Unity Methods

        void Awake()
        {
            _view.EnableCountdownText(false);
            _view.EnableLapText(false);
            _view.EnableEndRaceElements(false);
        }

        void OnEnable()
        {
            GameManager.OnGameInitializedEvent += OnGameInitializedEventHandler;
            GameManager.OnCountdownTickEvent += OnCountdownTickEventHandler;
            GameManager.OnCountdownEndEvent += OnCountdownEndEventHandler;
            MenuViewController.OnPlayerStartedRaceEvent += OnPlayerStartedRaceEventHandler;
            LapManager.OnLapCompletedEvent += OnLapCompletedEventHandler;
            LapManager.OnRaceEndedEvent += OnRaceEndedEventHandler;
        }

        void OnDisable()
        {
            GameManager.OnGameInitializedEvent -= OnGameInitializedEventHandler;
            GameManager.OnCountdownTickEvent -= OnCountdownTickEventHandler;
            GameManager.OnCountdownEndEvent -= OnCountdownEndEventHandler;
            MenuViewController.OnPlayerStartedRaceEvent -= OnPlayerStartedRaceEventHandler;
            LapManager.OnLapCompletedEvent -= OnLapCompletedEventHandler;
            LapManager.OnRaceEndedEvent -= OnRaceEndedEventHandler;
        }

        #endregion

        #region Custom Methods

        public void RestartRace()
        {
            OnRaceRestartedEvent?.Invoke();
            _view.EnableEndRaceElements(false);
            _view.EnableLapText(false);
            _view.UpdateLapText(1, _targetLaps);
            _view.EnableCountdownText(true);
        }

        // Disables countdown text after given time
        private IEnumerator DisableCountdownTextCoroutine()
        {
            yield return new WaitForSeconds(_countdownTextLifetime);
            _view.EnableCountdownText(false);
            _view.UpdateLapText(1, _targetLaps);
            _view.EnableLapText(true);
        }

        #endregion

        #region Event Handlers

        private void OnGameInitializedEventHandler(GameSettings settings)
        {
            _targetLaps = settings.TotalLaps;
            _view.UpdateLapText(1, _targetLaps);
        }

        private void OnCountdownTickEventHandler(int remainingTime)
        {
            _view.UpdateCountdownText(remainingTime.ToString());
        }

        private void OnCountdownEndEventHandler()
        {
            _view.UpdateCountdownText("GO!");
            StartCoroutine(DisableCountdownTextCoroutine());
            _view.EnableLapText(true);
        }  

        private void OnPlayerStartedRaceEventHandler()
        {
            _view.gameObject.SetActive(true);
            _view.EnableCountdownText(true);
        }

        private void OnLapCompletedEventHandler(int currentLap)
        {
            _view.UpdateLapText(currentLap, _targetLaps);
        }

        private void OnRaceEndedEventHandler()
        {
            _view.EnableEndRaceElements(true);
            _view.EnableLapText(false);
        }

        #endregion
    }
}

