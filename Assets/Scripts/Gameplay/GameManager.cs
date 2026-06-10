using System.Collections;
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

    private bool canRoll = false;

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
    [SerializeField] private TextMeshProUGUI remain;

    private bool gameRunning = false;
    [SerializeField] private int curMin;
    [SerializeField] private int curSpawn;
    private float time = 15;
    [SerializeField] Slider timeSlider;
    void Awake()
    {
        interact = InputSystem.actions.FindAction("Interact");
        interactPos = InputSystem.actions.FindAction("InteractPos");
        FindAnyObjectByType<SoundManager>().StopBGM();
        FindAnyObjectByType<SoundManager>().StopSfx();
        FindAnyObjectByType<SoundManager>().PlayBGM("game");
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
            updateRemain();
            setTimeSlide();
            if (time <= 0  && curMin > 0)
            {
                peopleSpawnerManager.sendDestroyAll();
                updateScores();
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
                    Debug.Log("testPass");
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
                        FindAnyObjectByType<SoundManager>().PlaySFX("tap");
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

    private void rollBuds()
    {
        float willSpawn = Random.Range(0.0f, 1.0f);
        if (willSpawn > 0.7f)
        {
            int whichBud = Random.Range(0, 3);
            //if ()
            //{
                
            //}
        }
        else
        {
            return;
        }
    }
    private void startLevel()
    {
        passRectMoveIN();
        StartCoroutine(delayShow());
        level++;
        time = 15;
        rollToFind();
        curMin = PeopleSpawnerManager.minreq;
        curSpawn = PeopleSpawnerManager.spawnMax;
        gameRunning = true;
        peopleSpawnerManager.startSpawn(currentPassCat, currentPass);
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

    private void updateRemain()
    {
        remain.text = curMin.ToString();
    }

    IEnumerator delayShow()
    {
        yield return new WaitForSeconds(1);
        passRectMoveIN();
    }
}
