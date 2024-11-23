using RacingGhost.Ghost.Data;
using System;

namespace RacingGhost.Data
{
    [Serializable]
    public class GhostPositionData
    {
        private CarPartPositionData _parentPositionData;
        private CarPartPositionData _bodyPositionData;
        private CarPartPositionData _frWheelPositionData;
        private CarPartPositionData _flWheelPositionData;
        private CarPartPositionData _rrWheelPositionData;
        private CarPartPositionData _rlWheelPositionData;

        public CarPartPositionData ParentPositionData => _parentPositionData;
        public CarPartPositionData BodyPositionData => _bodyPositionData;
        public CarPartPositionData FrWheelPositionData => _frWheelPositionData;
        public CarPartPositionData FlWheelPositionData => _flWheelPositionData;
        public CarPartPositionData RrWheelPositionData => _rrWheelPositionData;
        public CarPartPositionData RlWheelPositionData => _rlWheelPositionData;

        public GhostPositionData(
            CarPartPositionData parentPositionData,
            CarPartPositionData bodyPositionData,
            CarPartPositionData frWheelPositionData,
            CarPartPositionData flWheelPositionData,
            CarPartPositionData rrWheelPositionData,
            CarPartPositionData rlWheelPositionData)
        {
            _parentPositionData = parentPositionData;
            _bodyPositionData = bodyPositionData;
            _frWheelPositionData = frWheelPositionData;
            _flWheelPositionData = flWheelPositionData;
            _rrWheelPositionData = rrWheelPositionData;
            _rlWheelPositionData = rlWheelPositionData;
        }
    }
}

