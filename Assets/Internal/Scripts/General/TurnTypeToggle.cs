using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

namespace General
{
	public class TurnTypeToggle: MonoBehaviour
	{


        ///  INSPECTOR VARIABLES       ///
        [SerializeField] private ActionBasedSnapTurnProvider _snapTurn;
        [SerializeField] private ActionBasedContinuousTurnProvider _continuousTurn;
        [SerializeField] private TextMeshProUGUI _toggleText;
        ///  PRIVATE VARIABLES         ///
        private int _turnType;

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
            _turnType = (_turnType == 0 ? 1 : 0);
            SetTurnType(_turnType);
            SetToggleText(_turnType);
        }
		///  IMPLEMENTATION            ///

	}
}
