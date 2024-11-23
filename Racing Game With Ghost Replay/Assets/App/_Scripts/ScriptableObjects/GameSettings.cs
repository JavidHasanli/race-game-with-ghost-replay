using UnityEngine;

namespace RacingGhost.ScriptableObjects
{
    /// <summary>
    /// Used to setup general game settings.
    /// </summary>
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private int _totalLaps = 2; // Max laps players needs to complete to end the race
        [SerializeField] private int _ghostSpawnLap = 2; // Lap at which player's ghost spawns
        [SerializeField] private int _countdownDuration = 3; // Countdown for race start

        public int TotalLaps => _totalLaps;
        public int GhostSpawnLap => _ghostSpawnLap;
        public int CountdownDuration => _countdownDuration;
    }
}

