using UnityEngine;

public class PeopleSpawnerManager : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private Transform spawnParent;
    private float spawnDelay = 0.5f;
    private float spawnMax = 10;
    void Start()
    {
        for (int i = 0; i < spawnMax; i++)
        {
            createPerson(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (spawnDelay <= 0.0f && spawnParent.childCount != spawnMax)
        //{
        //    createPerson();
        //    spawnDelay = 0.5f;
        //}
        //else
        //{
        //    spawnDelay -= Time.deltaTime;
        //}
    }

    public void createPerson(int layer)
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
        if (GameManager.level == 1)
        {
            perFuncs.setSpeed(1.0f, 2.0f);
        }
        else if(GameManager.level == 2)
        {
            perFuncs.setSpeed(1.25f, 2.5f);
        }
        else if (GameManager.level == 3)
        {
            perFuncs.setSpeed(1.5f, 3.0f);
        }
        else
        {
            perFuncs.setSpeed(2.0f, 3.0f);
        }
    }
}
