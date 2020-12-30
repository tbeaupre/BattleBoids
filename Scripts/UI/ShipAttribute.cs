using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipAttribute : MonoBehaviour
{
	public string symbol;
	public float value;

	// Start is called before the first frame update
	void Start()
	{
		value = Random.value;
		ShipAttributePoint[] points = GetComponentsInChildren<ShipAttributePoint>();
		Debug.Log(points.Length);

		for (int i = 0; i < points.Length; i++) {
			points[i].gameObject.SetActive(value >= (i + 1f) / 10f);
		}

		Text symbolText = GetComponentInChildren<Text>();
		symbolText.text = symbol;
	}
}
