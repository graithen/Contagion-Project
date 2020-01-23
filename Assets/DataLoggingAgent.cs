using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoggingAgent : MonoBehaviour
{
    public GameObject[] Players;
    public List<Vector2>[] PlayerTransforms;

    public int TotalPlayers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitiateDataSets());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InitiateDataSets()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Initiating datasets!");

        Players = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        Players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in Players)
            TotalPlayers++;

        PlayerTransforms = new List<Vector2>[Players.Length];
        
        for(int i = 0; i < Players.Length; i++)
        {
            PlayerTransforms[i] = new List<Vector2>();
        }
        StartCoroutine(Tracking());
    }

    IEnumerator Tracking()
    {
        Debug.Log("Starting tracking!");
        while (true)
        {
            int iterator = 0;
            for (int i = 0; i < Players.Length; i++)
            {
                Vector2 position = Players[i].transform.position;
                Debug.Log(position);
                PlayerTransforms[i].Add(position);
                Debug.Log("Player " + i + " coords: " + PlayerTransforms[i][iterator]);
                iterator++;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
