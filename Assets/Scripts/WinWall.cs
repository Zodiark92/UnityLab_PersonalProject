using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWall : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.ShowVictory();
    }

}
