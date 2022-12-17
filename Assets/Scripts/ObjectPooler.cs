using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler: MonoBehaviour
{
    [SerializeField]
    [Range(0,300)]
    private int MaxArrowCount;

    private GameObject ArrowPrefab;
    private GameObject ArrowSwarm;

    private GameManager Manager;

    //private int currentIndex;

    private List<GameObject> ArrowList = new List<GameObject>();
    private List<GameObject> ActiveArrowList = new List<GameObject>();

    public static ObjectPooler PoolInstance;

    private void Awake()
    {
        PoolInstance = this;

        Manager = FindObjectOfType<GameManager>();
        ArrowPrefab = Manager.getArrowPrefab();
        ArrowSwarm = Manager.getArrowSwarm();
    }

    private void Start()
    {   
        for (int i = 0; i < MaxArrowCount; i++)
        {
            GameObject obj = Instantiate(ArrowPrefab);
            obj.transform.parent = ArrowSwarm.transform;
            ArrowList.Add(obj);
            obj.SetActive(false);
            
        }
    }

    public void SpawnFromPool (int amount)
    {
        if (amount > MaxArrowCount)
        {
            amount = MaxArrowCount;
        }

        for (int i = 0; i < amount; i++)
        {
            if (ActiveArrowList.Count != MaxArrowCount)
            {
                GameObject obj = ArrowList[ActiveArrowList.Count];
                obj.SetActive(true);
                ActiveArrowList.Add(obj);
            }
            else
            {
                break;
            }
            
        }
        
    }

    public void BackToPool (int amount)
    {
        int check = 0;
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = ActiveArrowList[ActiveArrowList.Count-1];
            
            obj.SetActive(false);
            ActiveArrowList.Remove(obj);
            check++;
        }
    }

    public int getActiveArrowCount()
    {
        return ActiveArrowList.Count + 1;
    }

    public List<GameObject> getActiveArrowList()
    {
        return ArrowList;
    }


}
