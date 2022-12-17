using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmBehaviour : MonoBehaviour
{
    [Range(0, 100)]
    public int speed = 10;
    [Range(0, 100)]
    public int horizontalSpeed = 10;

    Vector3 startPos, endPos, movement;

    GameManager Manager;
    private ObjectPooler objectPooler;

    private void Awake()
    {
        Manager = FindObjectOfType<GameManager>();
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    void FixedUpdate()
    {
        if (Manager.getIsRuning())
        {
            GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);

            if (Manager.CanMove)
            {
                if (Input.GetMouseButtonDown(0))
                {

                    startPos = Input.mousePosition;

                }
                else if (Input.GetMouseButton(0))
                {
                    endPos = Input.mousePosition;
                    movement = startPos - endPos;

                    if (movement.x < 0 && transform.position.z > -4)
                    {
                        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, -Mathf.Abs(movement.x) * Time.deltaTime);
                    }
                    else if (movement.x > 0 && transform.position.z < 4)
                    {
                        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, Mathf.Abs(movement.x) * Time.deltaTime);
                    }

                }
            }
           
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        AdjustSwarm();
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
