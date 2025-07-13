using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int level = 0;
    private int point;
    private int howPPl;
    private int howOgu;
    private int howTapp;
    private int howBam;

    private InputAction interact;
    private InputAction interactPos;
    [SerializeField] private Sprite currentPass;
    private bool currentPassCat;
    [SerializeField] private Sprite[] possibles;
    [SerializeField] private PeopleSpawnerManager peopleSpawnerManager;
    [SerializeField] private Image uiSpawn;
    [SerializeField] private Animator passRect;
    [SerializeField] private RectTransform passRectTrans;
    [SerializeField] private GameObject lose;
    [SerializeField] private TextMeshProUGUI losePpl;
    [SerializeField] private TextMeshProUGUI loseOgu;
    [SerializeField] private TextMeshProUGUI loseTap;
    [SerializeField] private TextMeshProUGUI loseBam;
    [SerializeField] private TextMeshProUGUI loseScore;

    private bool gameRunning = false;
    [SerializeField] private int curMin;
    [SerializeField] private int curSpawn;
    private float time = 10;
    [SerializeField] Slider timeSlider;
    void Awake()
    {
        interact = InputSystem.actions.FindAction("Interact");
        interactPos = InputSystem.actions.FindAction("InteractPos");
        startLevel();
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning == true)
        {
            time -= Time.deltaTime;
            setTimeSlide();
            if (time <= 0  && curMin > 0)
            {
                peopleSpawnerManager.sendDestroyAll();
                
                lose.SetActive(true);
                gameRunning = false;
            }
            if (curMin == 0)
            {
                passRectMoveOUT();
                peopleSpawnerManager.sendAllOff();
                gameRunning = false;
            }
        }
        else if (gameRunning == false)
        {
            if (peopleSpawnerManager.checkAllGone() <= 0)
            {
                if (lose.activeInHierarchy == true)
                {
                    return;
                }
                else
                {
                    addLevel();
                    resetPass();
                    startLevel();
                }
            }
        }

        if (interact.WasPressedThisFrame() == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(interactPos.ReadValue<Vector2>()), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Person"))
                {
                    Person tempCheck = hit.collider.GetComponent<Person>();
                    if (tempCheck.perData.clothes == currentPass || tempCheck.perData.hat == currentPass)
                    {
                        Debug.Log("Correct");
                        point += 100;
                        curMin -= 1;
                        tempCheck.startBoard();
                    }
                    else
                    {
                        Debug.Log("Wrong");
                    }
                }
                else if (hit.collider.CompareTag("Ogu"))
                {

                }
                else if (hit.collider.CompareTag("Tappy"))
                {

                }
                else if (hit.collider.CompareTag("Bam"))
                {

                }
            }
        }
    }

    private void rollToFind()
    {
        int toFind = Random.Range(11, possibles.Length);
        currentPass = possibles[toFind];
        uiSpawn.sprite = currentPass;
        if (toFind < 11)
        {
            currentPassCat = true;
        }
        else
        {
            currentPassCat = false;
        }

        Debug.Log(currentPass);
    }

    private void startLevel()
    {
        level++;
        rollToFind();
        curMin = PeopleSpawnerManager.minreq;
        curSpawn = PeopleSpawnerManager.spawnMax;
        gameRunning = true;
        peopleSpawnerManager.startSpawn(currentPassCat, currentPass);
        passRectMoveIN();
    }

    private void addLevel()
    {
        PeopleSpawnerManager.minreq += 2;
        PeopleSpawnerManager.spawnMax += 3;
    }

    private void setTimeSlide()
    {
        timeSlider.value = time;
    }

    private void passRectMoveIN()
    {
        passRect.SetTrigger("toMove");
    }
    private void passRectMoveOUT()
    {
        passRect.SetTrigger("toMove");
    }
    private void resetPass()
    {
        passRectTrans.position = new Vector3(-1000, 0, 0);
        passRect.SetTrigger("toMove");
    }

    private void updateScores()
    {
        loseBam.text = howBam.ToString();
        loseOgu.text = howOgu.ToString();   
        losePpl.text = howPPl.ToString();
        loseTap.text = howTapp.ToString();
        loseScore.text = point.ToString();
    }

}
