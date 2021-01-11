using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotContent : MonoBehaviour
{
	public GameObject pilotScrollPreviewPrefab;

    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < MasterManager.Instance.Pilots.Length; i++) {
			GameObject scrollPreviewObject = Instantiate(pilotScrollPreviewPrefab, transform);
			PilotScrollPreview pilotScrollPreview = scrollPreviewObject.GetComponent<PilotScrollPreview>();
			pilotScrollPreview.SetPilot(MasterManager.Instance.Pilots[i], i);
		}
    }
}
