using Ashsvp;
using RacingGhost.Managers;
using RacingGhost.UI.Controllers;
using System;
using UnityEngine;

namespace RacingGhost.RaceElements
{
    public class Car : MonoBehaviour
    {
        #region Links&Properties

        public static event Action OnFinishLineCrossedEvent;
        public static event Action OnFinishLineActivatorTriggeredEvent;

        private SimcadeVehicleController _vehicleController;

        private Vector3 _initialPosition;

        #endregion

        #region Unity Methods

        void Awake()
        {
            _vehicleController = GetComponent<SimcadeVehicleController>();
            _initialPosition = transform.position;

            _vehicleController.EnableCarMovement(false);
        }

        void OnEnable()
        {
            GameManager.OnCountdownEndEvent += OnCountdownEndEventHandler;
            LapManager.OnRaceEndedEvent += OnRaceEndedEventHandler;
            GamePlayViewController.OnRaceRestartedEvent += OnRaceRestartedEventHandler;
        }

        void OnDisable()
        {
            GameManager.OnCountdownEndEvent -= OnCountdownEndEventHandler;
            LapManager.OnRaceEndedEvent -= OnRaceEndedEventHandler;
            GamePlayViewController.OnRaceRestartedEvent -= OnRaceRestartedEventHandler;
        }

        #endregion

        #region Event Handlers

        private void OnCountdownEndEventHandler()
        {
            _vehicleController.EnableCarMovement(true);
        }

        private void OnRaceEndedEventHandler()
        {
            _vehicleController.EnableCarMovement(false);
        }

        private void OnRaceRestartedEventHandler()
        {
            _vehicleController.EnableCarMovement(false);
            _vehicleController.ResetCarPosition(_initialPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FinishLine finishLine))
            {
                OnFinishLineCrossedEvent?.Invoke();
            }

            if (other.TryGetComponent(out FinishLineActivator finishLineActivator))
            {
                OnFinishLineActivatorTriggeredEvent?.Invoke();
            }
        }

        #endregion
    }
}

