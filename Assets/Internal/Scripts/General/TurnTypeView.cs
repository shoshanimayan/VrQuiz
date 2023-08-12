using UnityEngine;
using Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

namespace General
{
	public class TurnTypeView: MonoBehaviour,IView
	{


        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private ActionBasedSnapTurnProvider _snapTurn;
        [SerializeField] private ActionBasedContinuousTurnProvider _continuousTurn;
        [SerializeField] private TextMeshProUGUI _toggleText;
        ///  PRIVATE VARIABLES         ///
        private int _turnType;
        private TurnTypeMediator _mediator;
        ///  PRIVATE METHODS           ///

        private void SetToggleText(int TurnType)
        {
            switch (TurnType)
            {
                case 0:
                    _toggleText.text = "use continuous turn";
                    break;
                case 1:
                    _toggleText.text = "use snap turn";
                    break;
            }
        }

        private void SetTurnType(int TurnType)
        {
            PlayerPrefs.SetInt("turn", TurnType);

            if (TurnType == 0)
            {
                _snapTurn.leftHandSnapTurnAction.action.Enable();
                _snapTurn.rightHandSnapTurnAction.action.Enable();
                _continuousTurn.leftHandTurnAction.action.Disable();
                _continuousTurn.rightHandTurnAction.action.Disable();
            }
            else if (TurnType == 1)
            {
                _snapTurn.leftHandSnapTurnAction.action.Disable();
                _snapTurn.rightHandSnapTurnAction.action.Disable();
                _continuousTurn.leftHandTurnAction.action.Enable();
                _continuousTurn.rightHandTurnAction.action.Enable();
            }
        }
        private void Awake()
        {
            if (PlayerPrefs.HasKey("turn"))
            {
                _turnType = PlayerPrefs.GetInt("turn");
                SetTurnType(_turnType);
                SetToggleText(_turnType);

            }
            else
            {
                _turnType = 0;
                PlayerPrefs.SetInt("turn", 0);
                _snapTurn.leftHandSnapTurnAction.action.Enable();
                _snapTurn.rightHandSnapTurnAction.action.Enable();
                _continuousTurn.leftHandTurnAction.action.Disable();
                _continuousTurn.rightHandTurnAction.action.Disable();
                SetToggleText(_turnType);

            }
        }
        ///  LISTNER METHODS           ///

        ///  PUBLIC API                ///
        public void ChangeTurnType()
        {
            _mediator?.PlayClickAudio();
            _turnType = (_turnType == 0 ? 1 : 0);
            SetTurnType(_turnType);
            SetToggleText(_turnType);
        }

        public void Initialize(TurnTypeMediator mediator)
        {
            _mediator = mediator;
        }
        ///  IMPLEMENTATION            ///

    }
}
