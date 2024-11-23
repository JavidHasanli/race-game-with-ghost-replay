using RacingGhost.RaceElements.Ghost;
using RacingGhost.Ghost.Data;
using RacingGhost.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using RacingGhost.Data;
using RacingGhost.RaceElements;
using RacingGhost.UI.Controllers;

namespace RacingGhost.Managers
{
    public class CarRecordManager : MonoBehaviour
    {

        #region Links&Properties

        [Header("Links")]
        [SerializeField] private Transform _carToTrack; // Parent gameobject of the tracked car

        [Header("Body Parts To Record")] // Body parts of the tracked car
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _frWheel;
        [SerializeField] private Transform _flWheel;
        [SerializeField] private Transform _rrWheel;
        [SerializeField] private Transform _rlWheel;

        [Header("Ghost Car")]
        [SerializeField] private GhostCar _ghostCar;

        [Header("Properties")]
        [SerializeField] private bool _recordEnabled; // Whether replay recording enabled or not (through the inspector)
        [SerializeField] private int _recordFrequency = 60; // Record N times per second (higher is better)

        private float _timer;
        private float _timeStamp;

        private List<float> _timeStamps = new List<float>();
        private List<GhostPositionData> _ghostPositionData = new List<GhostPositionData>();

        private int _lapToSpawnGhost;
        private int _currentLap = 0;
        private Vector3 _ghostStartPosition;
        private bool _record; // Enable by checking conditions

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
            LapManager.OnLapCompletedEvent += OnLapCompletedEventHandler;
            GamePlayViewController.OnRaceRestartedEvent += OnRaceRestartedEventHandler;
        }

        void OnDisable()
        {
            GameManager.OnGameInitializedEvent -= OnGameInitializedEventHandler;
            Car.OnFinishLineCrossedEvent -= OnFinishLineCrossedEventHandler;
            LapManager.OnLapCompletedEvent -= OnLapCompletedEventHandler;
            GamePlayViewController.OnRaceRestartedEvent -= OnRaceRestartedEventHandler;
        }

        void Update()
        {   
            if (!_recordEnabled || !_record)
                return;

            _timer += Time.unscaledDeltaTime;
            _timeStamp += Time.unscaledDeltaTime;

            if (_timer >= 1 / _recordFrequency)
            {
                _timeStamps.Add(_timeStamp);
                _ghostPositionData.Add(
                    new GhostPositionData(
                        new CarPartPositionData(_carToTrack.transform.position, _carToTrack.transform.rotation.eulerAngles),
                        new CarPartPositionData(_body.transform.position, _body.transform.rotation.eulerAngles),
                        new CarPartPositionData(_frWheel.transform.position, _frWheel.transform.rotation.eulerAngles),
                        new CarPartPositionData(_flWheel.transform.position, _flWheel.transform.rotation.eulerAngles),
                        new CarPartPositionData(_rrWheel.transform.position, _rrWheel.transform.rotation.eulerAngles),
                        new CarPartPositionData(_rlWheel.transform.position, _rlWheel.transform.rotation.eulerAngles)));             

                _timer = 0;
            }
        }

        #endregion

        #region Custom Methods

        private void SpawnGhost()
        {
            _ghostCar.StartSimulating(_ghostStartPosition, _timeStamps, _ghostPositionData);
        }

        // Setting up initial state for recording manager
        private void Initialize()
        {
            _timer = 0;
            _timeStamp = 0;
            _timeStamps.Clear();
            _ghostPositionData.Clear();
            _record = false;
            _currentLap = 0;
        }

        #endregion

        #region Event Handlers

        private void OnGameInitializedEventHandler(GameSettings settings)
        {
            _lapToSpawnGhost = settings.GhostSpawnLap;
        }

        // Handles logic when finish line was crossed when player started the race
        private void OnFinishLineCrossedEventHandler()
        {
            if (_currentLap < 1)
            {
                _ghostStartPosition = _carToTrack.transform.position;
                _record = true;
            }
        }

        // Handles logic for every lap completed by the player
        private void OnLapCompletedEventHandler(int currentLap)
        {
            _currentLap = currentLap;

            // Checks if the current lap is the lap when ghost should spawn and stops further recording
            if (_lapToSpawnGhost == _currentLap && _recordEnabled) 
            {
                _record = false;
                SpawnGhost();
            }
        }

        private void OnRaceRestartedEventHandler()
        {
            Initialize();
        }

        #endregion

    }
}

