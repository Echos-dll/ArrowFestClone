using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunGate : MonoBehaviour
{
    private GameManager Manager;

    // Start is called before the first frame update
    void Awake()
    {
        Manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArrowSwarm"))
        {
            Manager.setKillPhase(true); 
        }
    }
}
