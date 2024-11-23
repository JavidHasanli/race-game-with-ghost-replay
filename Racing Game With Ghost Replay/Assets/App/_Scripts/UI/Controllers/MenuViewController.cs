using RacingGhost.UI.Views;
using System;
using UnityEngine;

namespace RacingGhost.UI.Controllers
{
    public class MenuViewController : MonoBehaviour
    {
        public static event Action OnPlayerStartedRaceEvent;

        #region Links&Properties

        [Header("View")]
        [SerializeField] private MenuView _view;

        #endregion

        #region Unity Methods

        void OnEnable()
        {
            
        }

        void OnDisable()
        {
            
        }

        #endregion

        #region Custom Methods

        public void StartGame()
        {
            OnPlayerStartedRaceEvent?.Invoke();
            _view.gameObject.SetActive(false);
        }

        #endregion

        #region Event Handlers

        #endregion
    }
}