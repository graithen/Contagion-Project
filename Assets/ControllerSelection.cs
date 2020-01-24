using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSelection : MonoBehaviour
{
    public GameObject Player;
    public GameObject Observer;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("PlayerType") == 0)
            Player.transform.parent = null;
        if (PlayerPrefs.GetInt("PlayerType") == 1)
            Observer.transform.parent = null;

        Destroy(this.gameObject);
    }
}
