using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InfectPlayer : MonoBehaviour
{
    GameObject[] players = new GameObject[8];

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        int rand = Random.Range(0, players.Length);
        players[rand].GetComponent<PlayerController>().Infection = true;
        Debug.Log("Subject 0 infected");
    }
}
