using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    private int value;
    private int process;

    ObjectPooler objectPooler;
    GameManager Manager;

    //<summary>
    //
    //  0 = plus
    //  1 = minus
    //  2 = multiply
    //  3 = divide
    //
    //</summary>
    private void Start()
    {
        objectPooler = ObjectPooler.PoolInstance;
        Manager = FindObjectOfType<GameManager>();

        process = UnityEngine.Random.Range(0, 4);
        value = UnityEngine.Random.Range(1, 30);

    }

    private void Update()
    {
        if (Manager.getIsRuning() == false)
        {
            AdjustSwarm();
        }
    }

    //<summary>
    //
    //  Adjust number of arrows in this method
    //
    //</summary>
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("helo");
        if (other.CompareTag("ArrowSwarm"))
        {
            switch (process)
            {
                case 0:
                    Manager.setArrowCount(Manager.getArrowCount() + value);
                    AdjustSwarm();
                    break;

                case 1:
                    if(Manager.getArrowCount() - value < 1)
                    {
                        Manager.setArrowCount(1);
                    }
                    else
                    {
                        Manager.setArrowCount(Manager.getArrowCount() - value);
                    }
                    AdjustSwarm();
                    break;

                case 2:
                    Manager.setArrowCount(Manager.getArrowCount() * value);
                    AdjustSwarm();
                    break;

                case 3:
                    if (Manager.getArrowCount() / value < 1)
                    {
                        Manager.setArrowCount(1);
                    }
                    else
                    {
                        Manager.setArrowCount(Manager.getArrowCount() / value);
                    }
                    AdjustSwarm();
                    break;
            }
        }
        else
        {
            Debug.Log("Tag Error");
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
