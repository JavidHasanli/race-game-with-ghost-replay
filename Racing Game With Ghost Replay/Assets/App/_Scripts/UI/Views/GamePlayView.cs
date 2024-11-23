using TMPro;
using UnityEngine;

namespace RacingGhost.UI.Views
{
    public class GamePlayView : MonoBehaviour
    {
        #region Links&Properties

        [Header("Links")]
        [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private TMP_Text _lapText;
        [SerializeField] private TMP_Text _raceEndedText;
        [SerializeField] private GameObject _restartButton;

        #endregion

        #region Custom Methods

        public void EnableCountdownText(bool value)
        {
            _countdownText.gameObject.SetActive(value);
        }

        public void UpdateCountdownText (string text)
        {
            _countdownText.text = text;
        }

        public void EnableLapText(bool value)
        {
            _lapText.gameObject.SetActive(value);
        }

        public void UpdateLapText(int currentLap, int targetLap)
        {
            _lapText.text = $"Lap: {currentLap}/{targetLap}";
        }

        public void EnableEndRaceElements(bool value) 
        {
            _raceEndedText.gameObject.SetActive(value);
            _restartButton.SetActive(value);
        }

        #endregion
    }
}