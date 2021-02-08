using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToShipSelectionButton : MonoBehaviour
{
    public void GoToShipSelection()
    {
        SceneManager.LoadScene("ShipSelectionScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("OpponentScene");
    }
}
