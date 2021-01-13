using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPanel : MonoBehaviour
{
	public int index;

	public void SelectMatch()
	{
		PilotSelectionManager.Instance.SelectMatch(index);
	}
}
