using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotAttribute : MonoBehaviour
{
	public string symbol;
	public float value = 0;
	AttributePoint[] points;

	void Awake()
	{
		points = GetComponentsInChildren<AttributePoint>();
	}

	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < points.Length; i++) {
			points[i].gameObject.GetComponent<Image>().enabled = value >= (i + 1f) / 10f;
		}

		Text symbolText = GetComponentInChildren<Text>();
		symbolText.text = symbol;
	}

	public void SetValue(float newValue) {
		value = newValue;

		for (int i = 0; i < points.Length; i++) {
			points[i].gameObject.GetComponent<Image>().enabled = value >= (i + 1f) / 10f;
		}

	}
}
