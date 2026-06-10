using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private InputAction interact;
    private float time = 5.0f;
    private bool hasCLicekd = false;


    private void Awake()
    {
        
    }
    private void Start()
    {
        interact = InputSystem.actions.FindAction("Interact");
        FindAnyObjectByType<SoundManager>().StopBGM();
        FindAnyObjectByType<SoundManager>().StopSfx();
        FindAnyObjectByType<SoundManager>().PlayBGM("mainMenu");
        Debug.Log("Playing");
        FindAnyObjectByType<SoundManager>().PlayBGM("mainMenu");
        PeopleSpawnerManager.minreq = 3;
        PeopleSpawnerManager.spawnMax = 10;
    }

    private void Update()
    {
        if (interact.WasPressedThisFrame() == true && hasCLicekd == false)
        {
            hasCLicekd = true;
            transition.SetTrigger("toChange");
            FindAnyObjectByType<SoundManager>().PlaySFX("tap");
        }

        if (hasCLicekd == true)
        {
            if (time <= 0)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }
}
