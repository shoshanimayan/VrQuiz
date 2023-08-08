using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core;
namespace Signals
{


	public class StateChangeSignal
	{
		public State ToState;
	}

	public class StateChangedSignal
	{
		public State ToState;
	}


	public class SetQuestionNumberSignal
	{
		public int QuestionNumber;
	}

	public class SetQuestionTimeSignal
	{
		public float QuestionTime;
	}

	public class SetTopScoreSignal
	{
		public int Score;
	}
	
}
