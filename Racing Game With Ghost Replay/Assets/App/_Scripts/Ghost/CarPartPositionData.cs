using System;
using UnityEngine;

namespace RacingGhost.Ghost.Data
{
    [Serializable]
    public class CarPartPositionData
    {
        [SerializeField] private Vector3 _position;
        [SerializeField] private Vector3 _rotation;

        public Vector3 Position => _position;
        public Vector3 Rotation => _rotation;

        public CarPartPositionData(Vector3 position, Vector3 rotation)
        {
            _position = position;
            _rotation = rotation;
        }
    }
}

