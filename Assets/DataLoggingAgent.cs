using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoggingAgent : MonoBehaviour
{
    public GameObject[] Players;
    public List<Vector2> TransformTracking = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        Players = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        Players = GameObject.FindGameObjectsWithTag("Player");

        StartCoroutine(Tracking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Tracking()
    {
        while (true)
        {
            foreach (GameObject player in Players)
            {
                TransformTracking.Add(player.transform.position);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
