using UnityEngine;

public class PeopleSpawnerManager : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private Transform spawnParent;
    private float spawnDelay = 2.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnDelay <= 0.0f)
        {
            createPerson();
            spawnDelay = 2.0f;
        }
        else
        {
            spawnDelay -= Time.deltaTime;
        }
    }

    public void createPerson()
    {
        int spawnPos = Random.Range(0, positions.Length);
        Vector3 spawnPosToUse = new Vector3(positions[spawnPos].position.x, positions[spawnPos].position.y, 0.0f);
        GameObject newPer = GameObject.Instantiate(personPrefab, spawnPosToUse, Quaternion.identity, spawnParent);
        Person perFuncs = newPer.GetComponent<Person>();
        perFuncs.rollData();
        perFuncs.setDir(spawnPos);
    }
}
