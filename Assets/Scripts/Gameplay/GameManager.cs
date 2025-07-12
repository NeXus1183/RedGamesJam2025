using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static int level = 3;
    private InputAction interact;
    private InputAction interactPos;
    [SerializeField] private Sprite currentPass;
    private bool currentPassCat;    
    [SerializeField] private Sprite[] possibles;
    [SerializeField] private PeopleSpawnerManager peopleSpawnerManager;


    void Awake()
    {
        interact = InputSystem.actions.FindAction("Interact");
        interactPos = InputSystem.actions.FindAction("InteractPos");
        rollToFind();
        peopleSpawnerManager.startSpawn(currentPassCat, currentPass);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interact.WasPressedThisFrame() == true) 
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(interactPos.ReadValue<Vector2>()), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Person"))
                {
                    Debug.Log("Person hit");
                    hit.collider.gameObject.tag = "Untagged";
                }
            }
        }
    }

    private void rollToFind()
    {
        int toFind = Random.Range(0, possibles.Length);
        currentPass = possibles[toFind];
        if (toFind < 11)
        {
            currentPassCat = true;
        }
        else
        {
            currentPassCat = false;
        }
    }
}
