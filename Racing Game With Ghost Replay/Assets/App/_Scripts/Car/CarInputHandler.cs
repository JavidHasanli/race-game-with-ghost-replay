using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RacingGhost.RaceElements 
{
    /// <summary>
    /// Car Input System
    /// </summary>
    public class CarInputHandler : MonoBehaviour
    {
        #region Links&Properties
        
        [Header("Input Action Asset")]
        [SerializeReference] private InputActionAsset _carControls;

        [Header("Action Map Name References")]
        [SerializeField] private string _actionMapName = "CarControls";

        [Header("Action Name References")]
        [SerializeField] private string _accelerate = "Accelerate";
        [SerializeField] private string _decelerate = "Decelerate";
        [SerializeField] private string _turnRight = "TurnRight";
        [SerializeField] private string _turnLeft = "TurnLeft";
        [SerializeField] private string _brake = "Brake";

        private InputAction _accelerateAction;
        private InputAction _decelerateAction;
        private InputAction _turnRightAction;
        private InputAction _turnLeftAction;
        private InputAction _brakeAction;

        public bool AccelerateTriggered { get; private set; }
        public bool DecelerateTriggered { get; private set; }
        public bool TurnRightTriggered { get; private set; }
        public bool TurnLeftTriggered { get; private set; }
        public bool BrakeTriggered { get; private set; }

        #endregion

        #region Unity Methods

        void Awake()
        {
            _accelerateAction = _carControls.FindActionMap(_actionMapName).FindAction(_accelerate);
            _decelerateAction = _carControls.FindActionMap(_actionMapName).FindAction(_decelerate);
            _turnRightAction = _carControls.FindActionMap(_actionMapName).FindAction(_turnRight);
            _turnLeftAction = _carControls.FindActionMap(_actionMapName).FindAction(_turnLeft);
            _brakeAction = _carControls.FindActionMap(_actionMapName).FindAction(_brake);
        }

        void OnEnable()
        {
            _accelerateAction.Enable();
            _decelerateAction.Enable();
            _turnRightAction.Enable();
            _turnLeftAction.Enable();
            _brakeAction.Enable();

            RegisterInputActions();
        }

        void OnDisable()
        {
            _accelerateAction.Disable();
            _decelerateAction.Disable();
            _turnRightAction.Disable();
            _turnLeftAction.Disable();
            _brakeAction.Disable();

            UnregisterInputActions();
        }

        #endregion

        #region Custom Methods

        private void RegisterInputActions()
        {
            _accelerateAction.performed += OnAccelerateActionPerformedHandler;
            _accelerateAction.canceled += OnAccelerateActionCanceledHandler;

            _decelerateAction.performed += OnDecelerateActionPerformedHandler;
            _decelerateAction.canceled += OnDecelerateActionCanceledHandler;

            _turnRightAction.performed += OnTurnRightActionPerformedHandler;
            _turnRightAction.canceled += OnTurnRightActionCanceledHandler;

            _turnLeftAction.performed += OnTurnLeftActionPerformedHandler;
            _turnLeftAction.canceled += OnTurnLeftActionCanceledHandler;

            _brakeAction.performed += OnBrakeActionPerformedHandler;
            _brakeAction.canceled += OnBrakeActionCanceledHandler;
        }

        private void UnregisterInputActions()
        {
            _accelerateAction.performed -= OnAccelerateActionPerformedHandler;
            _accelerateAction.canceled -= OnAccelerateActionCanceledHandler;

            _decelerateAction.performed -= OnDecelerateActionPerformedHandler;
            _decelerateAction.canceled -= OnDecelerateActionCanceledHandler;

            _turnRightAction.performed -= OnTurnRightActionPerformedHandler;
            _turnRightAction.canceled -= OnTurnRightActionCanceledHandler;

            _turnLeftAction.performed -= OnTurnLeftActionPerformedHandler;
            _turnLeftAction.canceled -= OnTurnLeftActionCanceledHandler;

            _brakeAction.performed -= OnBrakeActionPerformedHandler;
            _brakeAction.canceled -= OnBrakeActionCanceledHandler;
        }

        #endregion

        #region Event Handlers

        private void OnAccelerateActionPerformedHandler(InputAction.CallbackContext context)
        {
            AccelerateTriggered = true;
        }

        private void OnAccelerateActionCanceledHandler(InputAction.CallbackContext context)
        {
            AccelerateTriggered = false;
        }

        private void OnDecelerateActionPerformedHandler(InputAction.CallbackContext context)
        {
            DecelerateTriggered = true;
        }

        private void OnDecelerateActionCanceledHandler(InputAction.CallbackContext context)
        {
            DecelerateTriggered = false;
        }

        private void OnTurnRightActionPerformedHandler(InputAction.CallbackContext context)
        {
            TurnRightTriggered = true;
        }

        private void OnTurnRightActionCanceledHandler(InputAction.CallbackContext context)
        {
            TurnRightTriggered = false;
        }

        private void OnTurnLeftActionPerformedHandler(InputAction.CallbackContext context)
        {
            TurnLeftTriggered = true;
        }

        private void OnTurnLeftActionCanceledHandler(InputAction.CallbackContext context)
        {
            TurnLeftTriggered = false;
        }

        private void OnBrakeActionPerformedHandler(InputAction.CallbackContext context)
        {
            BrakeTriggered = true;
        }

        private void OnBrakeActionCanceledHandler(InputAction.CallbackContext context)
        {
            BrakeTriggered = false;
        }

        #endregion
    }
}

