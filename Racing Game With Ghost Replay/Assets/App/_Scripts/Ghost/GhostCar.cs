using RacingGhost.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RacingGhost.RaceElements.Ghost
{
    public class GhostCar : MonoBehaviour
    {

        #region Links&Properties

        [Header("Body Parts")]
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _frWheel;
        [SerializeField] private Transform _flWheel;
        [SerializeField] private Transform _rrWheel;
        [SerializeField] private Transform _rlWheel;

        private Vector3 _startPosition;

        private List<float> _timeStamps = new List<float>();
        private List<GhostPositionData> _ghostPositionData = new List<GhostPositionData>();

        // Current timestamp
        private float _timeStamp;

        // Indexes for interpolation between two consecutive timestamps.
        private int _index1;
        private int _index2;

        #endregion

        #region Unity Methods

        void Update()
        {            
            _timeStamp += Time.unscaledDeltaTime;

            GetIndex();
            SetPositions();
        }

        #endregion

        #region Custom Methods

        // Sets initial values for ghost car and starts simulation (Replay of ghost)
        public void StartSimulating(Vector3 startPosition, List<float> timeStamps, List<GhostPositionData> ghostPositionData)
        {
            _timeStamp = 0;
            _startPosition = startPosition;

            _timeStamps.Clear();
            _timeStamps = new List<float>(timeStamps);

            _ghostPositionData = new List<GhostPositionData>(ghostPositionData);

            transform.position = _startPosition;
            gameObject.SetActive(true);
        }

        // Getting indexes from timeStamps list
        private void GetIndex()
        {
            for (int i = 0; i < _timeStamps.Count - 2; i++)
            {
                // If current timeStamp exists in saved timeStamps list, then indexes are equal to current timeStamp
                if (_timeStamps[i] == _timeStamp)
                {
                    _index1 = i;
                    _index2 = i;
                    return;
                }
                // If current timeStamp does not exist in the saved timeStamps list, then index1 is equal to the first
                // element in the list that is less than current timeStamp and index2 is equal to the first element in the 
                // list that is greater than current timeStamp
                else if (_timeStamps[i] < _timeStamp && _timeStamp < _timeStamps[i + 1])
                {
                    _index1 = i;
                    _index2 = i + 1;
                    return;
                }
            }

            // After the loop ends set indexes to the last position and rotation values.
            _index1 = _timeStamps.Count - 1;
            _index2 = _timeStamps.Count - 1;

            gameObject.SetActive(false);
        }

        private void SetPositions()
        {
            // if current timeStamp exists in saved timeStamps list
            if (_index1 == _index2)
            {

                // Simply setting car part positions and rotations to their corresponding values
                // at the current timeStamp

                transform.position = _ghostPositionData[_index1].ParentPositionData.Position;
                transform.eulerAngles = _ghostPositionData[_index1].ParentPositionData.Rotation;

                _body.position = _ghostPositionData[_index1].BodyPositionData.Position;
                _body.eulerAngles = _ghostPositionData[_index1].BodyPositionData.Rotation;

                _frWheel.position = _ghostPositionData[_index1].FrWheelPositionData.Position;
                _frWheel.eulerAngles = _ghostPositionData[_index1].FrWheelPositionData.Rotation;

                _flWheel.position = _ghostPositionData[_index1].FlWheelPositionData.Position;
                _flWheel.eulerAngles = _ghostPositionData[_index1].FlWheelPositionData.Rotation;

                _rrWheel.position = _ghostPositionData[_index1].RrWheelPositionData.Position;
                _rrWheel.eulerAngles = _ghostPositionData[_index1].RrWheelPositionData.Rotation;

                _rlWheel.position = _ghostPositionData[_index1].RlWheelPositionData.Position;
                _rlWheel.eulerAngles = _ghostPositionData[_index1].RrWheelPositionData.Rotation;
            }
            else
            {
                // Defines where current _timeStamp is located between _timeStamps[index1] and _timeStamps[index2].
                // Division is normalizing the elapsed time within the interval to a value between 0.0 and 1.0:
                float interpolationFactor = (_timeStamp - _timeStamps[_index1]) / (_timeStamps[_index2] -  _timeStamps[_index1]);

                // Then all car part positions and rotation are interpolated between
                // _ghostPositionData[_index1] and _ghostPositionData[_index2].

                transform.position = Vector3.Lerp(
                    _ghostPositionData[_index1].ParentPositionData.Position,
                    _ghostPositionData[_index2].ParentPositionData.Position,
                    interpolationFactor);

                transform.eulerAngles = Vector3.Lerp(
                    _ghostPositionData[_index1].ParentPositionData.Rotation,
                    _ghostPositionData[_index2].ParentPositionData.Rotation,
                    interpolationFactor);

                _body.position = Vector3.Lerp(
                    _ghostPositionData[_index1].BodyPositionData.Position,
                    _ghostPositionData[_index2].BodyPositionData.Position,
                    interpolationFactor);

                _body.eulerAngles = Vector3.Lerp(
                    _ghostPositionData[_index1].BodyPositionData.Rotation,
                    _ghostPositionData[_index2].BodyPositionData.Rotation,
                    interpolationFactor);

                _frWheel.position = Vector3.Lerp(
                    _ghostPositionData[_index1].FrWheelPositionData.Position,
                    _ghostPositionData[_index2].FrWheelPositionData.Position,
                    interpolationFactor);

                _frWheel.eulerAngles = Vector3.Lerp(
                    _ghostPositionData[_index1].FrWheelPositionData.Rotation,
                    _ghostPositionData[_index2].FrWheelPositionData.Rotation,
                    interpolationFactor);

                _flWheel.position = Vector3.Lerp(
                    _ghostPositionData[_index1].FlWheelPositionData.Position,
                    _ghostPositionData[_index2].FlWheelPositionData.Position,
                    interpolationFactor);

                _flWheel.eulerAngles = Vector3.Lerp(
                    _ghostPositionData[_index1].FlWheelPositionData.Rotation,
                    _ghostPositionData[_index2].FlWheelPositionData.Rotation,
                    interpolationFactor);

                _rrWheel.position = Vector3.Lerp(
                    _ghostPositionData[_index1].RrWheelPositionData.Position,
                    _ghostPositionData[_index2].RrWheelPositionData.Position,
                    interpolationFactor);

                _rrWheel.eulerAngles = Vector3.Lerp(
                    _ghostPositionData[_index1].RrWheelPositionData.Rotation,
                    _ghostPositionData[_index2].RrWheelPositionData.Rotation,
                    interpolationFactor);

                _rlWheel.position = Vector3.Lerp(
                    _ghostPositionData[_index1].RlWheelPositionData.Position,
                    _ghostPositionData[_index2].RlWheelPositionData.Position,
                    interpolationFactor);

                _rlWheel.eulerAngles = Vector3.Lerp(
                    _ghostPositionData[_index1].RlWheelPositionData.Rotation,
                    _ghostPositionData[_index2].RlWheelPositionData.Rotation,
                    interpolationFactor);
            }
        }

        #endregion

    }
}

