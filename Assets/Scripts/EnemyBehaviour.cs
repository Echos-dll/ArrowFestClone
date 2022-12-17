using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    GameManager Manager;
    ObjectPooler objectPooler;
    
    // Start is called before the first frame update
    void Start()
    {
        Manager = FindObjectOfType<GameManager>();
        objectPooler = ObjectPooler.PoolInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ArrowSwarm"))
        {
            if ((Manager.getArrowCount() - Manager.getEnemyDurability()) > 0)
            {
                Manager.setArrowCount(Manager.getArrowCount() - Manager.getEnemyDurability());
                Manager.setGoldCount(Manager.getGoldCount() + Manager.goldRatio);
                AdjustSwarm();
                
            }
            else
            {
                Manager.EndRun();
            }
           
        }
    }

    private void AdjustSwarm()
    {
        if (objectPooler.getActiveArrowCount() > Manager.getArrowCount())
        {
            int number = objectPooler.getActiveArrowCount() - Manager.getArrowCount();
            objectPooler.BackToPool(number);
        }
        if (objectPooler.getActiveArrowCount() < Manager.getArrowCount())
        {
            int number = Manager.getArrowCount() - objectPooler.getActiveArrowCount();
            objectPooler.SpawnFromPool(number);
        }
    }
}
