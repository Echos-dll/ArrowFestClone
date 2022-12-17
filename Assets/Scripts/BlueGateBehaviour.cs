using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueGateBehaviour : MonoBehaviour
{
    private int value;
    private int process;

    ObjectPooler objectPooler;
    GameManager Manager;

    private TextMesh gateText;
    private void Start()
    {
        objectPooler = ObjectPooler.PoolInstance;
        Manager = FindObjectOfType<GameManager>();

        process = Random.Range(0, 2);
        value = Random.Range(1, 30);
        
        gateText = gameObject.transform.GetComponentInChildren<TextMesh>();
        
        switch (process)
        {
            case 0:
                gateText.text = "+" + value;
                break;

            case 1:
                gateText.text = "*" + value;
                break;
        }
        
    }

    private void Update()
    {
        if (Manager.getIsRuning() == false)
        {
            AdjustSwarm();
        }
    }
    
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
                    Manager.setArrowCount(Manager.getArrowCount() * value);
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
