using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text ArrowCountText;
    public Text GoldCountText;
    public GameObject Shop;
    public Button ButtonStart;

    [SerializeField]
    GameObject ArrowPrefab;

    [SerializeField]
    GameObject ArrowSwarm;

    [SerializeField] private GameObject RedGate;
    [SerializeField] private GameObject BlueGate;
    [SerializeField] private GameObject EnemyPrefab;
                    

    int ArrowCount = 1;
    int StartArrowCount = 1;
    int GoldCount;
    int Level = 1;

    int EnemyDurability = 3;

    int GoldRatio = 3;
    int numberOfBoxes = 20;
    int ArrowShopCost = 100;
    int CoinShopCost = 100;

    private bool gameCanStart = true;
    bool KillPhase = false;
    bool isRuning = false;
    bool canMove = true;
    private bool gameCreated = false;
    Vector3 endPosition;
    ObjectPooler objectPooler;

    private static List<GameObject> createdItems = new List<GameObject>();

    void Awake()
    {
        objectPooler = FindObjectOfType<ObjectPooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Start Screen

        //Adjust arrow positions
        float posZ = 0;
        int rowCount = 0;
        int goUp = 0;
        float posY = ArrowPrefab.transform.position.y;

        var ArrowList = objectPooler.getActiveArrowList();
        foreach (var arrow in ArrowList)
        {
            if (rowCount % 11 == 10)
            {
                posY += 0.2f;
                posZ = 0;
                rowCount = 0;
                goUp += 1;
            }

            if (rowCount % 2 == 0)
            {
                if (goUp >= 1)
                {
                    if (posZ == 0)
                    {
                        endPosition = new Vector3(ArrowPrefab.transform.position.x, posY, ArrowPrefab.transform.position.z - posZ);
                        arrow.transform.position = endPosition;
                        rowCount++;
                    }
                    else
                    {
                        endPosition = new Vector3(ArrowPrefab.transform.position.x, posY, ArrowPrefab.transform.position.z - posZ);
                        arrow.transform.position = endPosition;
                        rowCount++;
                    }

                    posZ += 0.1f;

                }
                else
                {
                    posZ += 0.1f;
                    endPosition = new Vector3(ArrowPrefab.transform.position.x, posY, ArrowPrefab.transform.position.z - posZ);
                    arrow.transform.position = endPosition;
                    rowCount++;
                }

            }
            else
            {
                endPosition = new Vector3(ArrowPrefab.transform.position.x, posY, ArrowPrefab.transform.position.z + posZ);
                arrow.transform.position = endPosition;
                rowCount++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ArrowCountText.text = ArrowCount.ToString();
        GoldCountText.text = GoldCount.ToString();
        
        if (isRuning)
        {
            Shop.SetActive(false);
            if (!gameCreated)
            {
                CreateLevel();
            }
            if (KillPhase)
            {
                //Start end run sequence
                //Stop control
                canMove = false;
                //Move camera

                if (ArrowCount == 0)
                {
                    isRuning = false;
                    //end run score
                }

                gameCanStart = false;
                ClearLevel();
            }
        }
        else
        {
            ClearLevel();
            Shop.SetActive(true);
        }
    }

    private void ClearLevel()
    {
        for (int i = 0; i < createdItems.Count; i++)
        {
            var toDestroy = createdItems[i];
            createdItems.Remove(createdItems[i]);
            Destroy(toDestroy);
        }

        gameCreated = false;
        gameCanStart = true;
    }

    private void CreateLevel()
    {
        int random1;
        float random2;
        Vector3 newPosition = new Vector3(0,1,0);
        for (int i = 0; i < 12; i++)
        {
            random1 = Random.Range(0, 3);
            random2 = Random.Range(-1f, 1f);
            newPosition.z = random2;
            if (random1 == 0)
            {
                // spawn red
                var spawnObj = Instantiate(BlueGate);
                spawnObj.transform.position = newPosition;
                createdItems.Add(spawnObj);
                newPosition.x += 5;
            }else if (random1 == 1)
            {
                // spawn blue
                var spawnObj = Instantiate(RedGate);
                spawnObj.transform.position = newPosition;
                createdItems.Add(spawnObj);
                newPosition.x += 5;
            }else if (random1 == 2)
            {
                // spawn enemy
                var spawnObj = Instantiate(EnemyPrefab);
                spawnObj.transform.position = newPosition;
                createdItems.Add(spawnObj);
                newPosition.x += 5;
            }
        }
        gameCreated = true;
        
    }

    public void ButtonArrow()
    {
        if (ArrowShopCost < GoldCount)
        {
            Debug.Log("Button Arrow");
            StartArrowCount += 1;
            GoldCount -= ArrowShopCost;
            ArrowCount = startArrowCount;
            ArrowShopCost += 100;
        }
    }

    public void ButtonCoin()
    {
        if (CoinShopCost < GoldCount)
        {
            Debug.Log("Button Coin");
            GoldRatio += 1;
            GoldCount -= CoinShopCost;
            CoinShopCost += 100;
        }
    }

    public void EndRun()
    {
        KillPhase = false;
        isRuning = false;
        ArrowSwarm.transform.position = new Vector3(-2, 2, 0);
        ArrowCount = startArrowCount;
        numberOfBoxes = 20;
        ClearLevel();
    }

    #region Getter & Setter
    public int getArrowCount()
    {
        return ArrowCount;
    }

    public void setArrowCount(int ArrowCount)
    {
        this.ArrowCount = ArrowCount;
    }

    public GameObject getArrowPrefab()
    {
        return ArrowPrefab;
    }

    public GameObject getArrowSwarm()
    {
        return ArrowSwarm;
    }

    public int getGoldCount()
    {
        return GoldCount;
    }

    public void setGoldCount(int newValue)
    {
        GoldCount = newValue;
    }

    public int getLevel()
    {
        return Level;
    }

    public void setLevel(int newLevel)
    {
        Level = newLevel;
    }

    public bool getKillPhase()
    {
        return KillPhase;
    }

    public void setKillPhase(bool newState)
    {
        KillPhase = newState;
    }

    public bool getIsRuning()
    {
        return isRuning;
    }

    public void setIsRuning(bool newState)
    {
        isRuning = newState;
    }

    public void setEnemyDurability(int newValue)
    {
        EnemyDurability = newValue;
    }

    public int getEnemyDurability()
    {
        return EnemyDurability;
    }

    public int goldRatio { get => GoldRatio; set => GoldRatio = value; }
    public int startArrowCount { get => StartArrowCount; set => StartArrowCount = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public int NumberOfBoxes { get => numberOfBoxes; set => numberOfBoxes = value; }

    public bool GameCanStart
    {
        get => gameCanStart;
        set => gameCanStart = value;
    }

    #endregion
}
