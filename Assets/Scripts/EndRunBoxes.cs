using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunBoxes : MonoBehaviour
{
    private GameManager Manager;

    void Awake()
    {
        Manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArrowSwarm"))
        {
            if (Manager.getArrowCount() - (Manager.getEnemyDurability() + Manager.getLevel()) > 0 && Manager.NumberOfBoxes > 0)
            {
                Manager.setArrowCount(Manager.getArrowCount() - (Manager.getEnemyDurability() + Manager.getLevel()));
                Manager.setGoldCount(Manager.getGoldCount() + (Manager.goldRatio * Manager.getLevel()));
            }
            else
            {
                Manager.EndRun();
            }
            Manager.NumberOfBoxes -= 1;
        }

        if (Manager.NumberOfBoxes == 0)
        {
            Manager.setLevel(Manager.getLevel() + 1);
            Manager.EndRun();
        }
       
     
    }
    private void OnTriggerExit(Collider other)
    {
        
        Debug.Log(Manager.NumberOfBoxes + this.gameObject.name);
    }
}
