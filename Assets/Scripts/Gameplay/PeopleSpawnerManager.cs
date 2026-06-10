using UnityEngine;

public class PeopleSpawnerManager : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    [SerializeField] private Transform[] extraPos;
    [SerializeField] private Transform[] oguPos;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject[] buds;

    private float spawnDelay = 0.5f;
    public static int minreq = 3;
    public static int spawnMax = 10;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createPerson(int layer)
    {
        int spawnPos = Random.Range(0, positions.Length);
        Vector3 spawnPosToUse = new Vector3(positions[spawnPos].position.x, positions[spawnPos].position.y, 0.0f);
        GameObject newPer = GameObject.Instantiate(personPrefab, spawnPosToUse, Quaternion.identity, spawnParent);
        for (int i = 0; i < newPer.transform.childCount; i++)
        {
            newPer.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder += layer;
        }
        Person perFuncs = newPer.GetComponent<Person>();
        perFuncs.rollData();
        perFuncs.setDir(spawnPos);
        if (GameManager.level == 2)
        {
            perFuncs.setSpeed(1.0f, 2.0f);
        }
        else if(GameManager.level == 4)
        {
            perFuncs.setSpeed(1.25f, 2.5f);
        }
        else if (GameManager.level >= 6)
        {
            perFuncs.setSpeed(1.5f, 3.0f);
        }
        else
        {
            perFuncs.setSpeed(2.0f, 3.0f);
        }
    }

    private void createPersonPass(int layer, Sprite toSPawn, bool type)
    {
        int spawnPos = Random.Range(0, positions.Length);
        Vector3 spawnPosToUse = new Vector3(positions[spawnPos].position.x, positions[spawnPos].position.y, 0.0f);
        GameObject newPer = GameObject.Instantiate(personPrefab, spawnPosToUse, Quaternion.identity, spawnParent);
        for (int i = 0; i < newPer.transform.childCount; i++)
        {
            newPer.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder += layer;
        }
        Person perFuncs = newPer.GetComponent<Person>();
        perFuncs.rollDataReq(type, toSPawn);
        perFuncs.setDir(spawnPos);
        if (GameManager.level == 2)
        {
            perFuncs.setSpeed(1.0f, 2.0f);
        }
        else if (GameManager.level == 4)
        {
            perFuncs.setSpeed(1.25f, 2.5f);
        }
        else if (GameManager.level >= 6)
        {
            perFuncs.setSpeed(1.5f, 3.0f);
        }
        else
        {
            perFuncs.setSpeed(2.0f, 3.0f);
        }
    }
    private void createBud(int layer, int toSPawn)
    {
        int spawnPos = Random.Range(0, positions.Length);
        Vector3 spawnPosToUse = new Vector3(positions[spawnPos].position.x, positions[spawnPos].position.y, 0.0f);
        GameObject newPer = GameObject.Instantiate(personPrefab, spawnPosToUse, Quaternion.identity, spawnParent);
        for (int i = 0; i < newPer.transform.childCount; i++)
        {
            newPer.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder += layer;
        }
        Person perFuncs = newPer.GetComponent<Person>();
        if (GameManager.level == 2)
        {
            perFuncs.setSpeed(1.0f, 2.0f);
        }
        else if (GameManager.level == 4)
        {
            perFuncs.setSpeed(1.25f, 2.5f);
        }
        else if (GameManager.level >= 6)
        {
            perFuncs.setSpeed(1.5f, 3.0f);
        }
        else
        {
            perFuncs.setSpeed(2.0f, 3.0f);
        }
    }
    public void startSpawn(bool type, Sprite item)
    {
        for (int i = 0; i < spawnMax; i++)
        {
            if (i < minreq)
            {
                createPersonPass(i, item, type);
            }
            else
            {
                createPerson(i);
            }
        }
    }

    public void sendAllOff()
    {
        for (int i = 0; i < spawnParent.childCount; i++)
        {
            Person temp = spawnParent.GetChild(i).GetComponent<Person>();
            temp.beginMoveAway();
        }
    }

    public void sendDestroyAll()
    {
        for (int i = 0; i < spawnParent.childCount; i++)
        {
            Destroy(spawnParent.GetChild(i).gameObject);
        }
    }

    public int checkAllGone()
    {
        int curRemains = spawnParent.childCount;
        return curRemains;
    }
}
