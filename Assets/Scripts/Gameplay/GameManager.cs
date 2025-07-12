using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static int level = 0;
    private InputAction interact;
    private InputAction interactPos;

    PersonData toFind;
    void Awake()
    {
        interact = InputSystem.actions.FindAction("Interact");
        interactPos = InputSystem.actions.FindAction("InteractPos");
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

    }
}
