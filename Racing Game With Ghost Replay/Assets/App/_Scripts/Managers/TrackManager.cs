using RacingGhost.RaceElements;
using RacingGhost.UI.Controllers;
using UnityEngine;

namespace RacingGhost.Managers
{
    public class TrackManager : MonoBehaviour
    {
        #region Links&Properties

        [SerializeField] private GameObject _finishLineTrigger;
        [SerializeField] private GameObject _finishLineBarrier1;
        [SerializeField] private GameObject _finishLineBarrier2;
        [SerializeField] private GameObject _finishLineActivator;

        #endregion

        #region Unity Methods

        void Awake()
        {
            Initialize();
        }

        void OnEnable()
        {
            Car.OnFinishLineCrossedEvent += OnFinishLineCrossedEventHandler;
            Car.OnFinishLineActivatorTriggeredEvent += OnFinishLineActivatorTriggeredEventHandler;
            GamePlayViewController.OnRaceRestartedEvent += OnRaceRestartedEventHandler;
        }

        void OnDisable()
        {
            Car.OnFinishLineCrossedEvent -= OnFinishLineCrossedEventHandler;
            Car.OnFinishLineActivatorTriggeredEvent -= OnFinishLineActivatorTriggeredEventHandler;
            GamePlayViewController.OnRaceRestartedEvent -= OnRaceRestartedEventHandler;
        }

        #endregion

        #region Custom Methods

        private void Initialize()
        {
            _finishLineTrigger.SetActive(true);
            _finishLineBarrier1.SetActive(true);
            _finishLineBarrier2.SetActive(false);
            _finishLineActivator.SetActive(true);
        }

        #endregion

        #region Event Handlers

        private void OnFinishLineCrossedEventHandler()
        {
            _finishLineTrigger.SetActive(false);
            _finishLineBarrier1.SetActive(true);
            _finishLineBarrier2.SetActive(false);
            _finishLineActivator.SetActive(true);
        }

        private void OnFinishLineActivatorTriggeredEventHandler()
        {
            _finishLineTrigger.SetActive(true);
            _finishLineBarrier1.gameObject.SetActive(false);
            _finishLineBarrier2.SetActive(true);
            _finishLineActivator.gameObject.SetActive(false);
        }

        private void OnRaceRestartedEventHandler()
        {
            Initialize();
        }

        #endregion

    }
}

