using UnityEngine;
using UnityEngine.Android;

public class Person : MonoBehaviour
{
    [SerializeField] private PersonData perData;
    [SerializeField] private Renderer selfSprite;
    [SerializeField] private BoxCollider2D selfColldier;
    [SerializeField] private Transform hatTrans;
    [SerializeField] private Transform headTrans;

    private Vector3 startPos;
    private Transform self;
    private bool dirrection;
    private float angle;
    private float moveDelay = 0.25f;
    private bool farEnough = false;
    private float speed;
    private float mult = 1.0f;
    private bool hairlCur = false;

    [SerializeField] private SpriteRenderer hat;
    [SerializeField] private SpriteRenderer clothes;
    [SerializeField] private SpriteRenderer luggage;
    [SerializeField] private SpriteRenderer person;

    [SerializeField] private Sprite[] posHat;
    [SerializeField] private Sprite[] posClothes;
    [SerializeField] private Sprite[] posLuggage;
    [SerializeField] private Sprite[] posPer;

    private void Awake()
    {
        startPos = transform.position;
        self = transform;
    }
    private void Update()
    {
        if (selfSprite.isVisible == false && farEnough == true)
        {
            Destroy(gameObject);
        }

        if (moveDelay <= 0)
        {
            move();
        }
        else
        {
            moveDelay -= Time.deltaTime;
        }
    }

    public void rollData()
    {
        int toRandFeat;
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                toRandFeat = Random.Range(0, posPer.Length);
                person.sprite = posPer[toRandFeat];
                if (toRandFeat == 0)
                {
                    hairlCur = true;
                    headTrans.localPosition = new Vector3(headTrans.localPosition.x, 0.086f, headTrans.localPosition.z);
                }
                else
                {
                    hairlCur = false;
                }
            }
            if (i == 1)
            {
                toRandFeat = Random.Range(0, posHat.Length);
                hat.sprite = posHat[toRandFeat];
                if (hairlCur == true)
                {
                    if (toRandFeat == 0)
                    {
                        hatTrans.localPosition = new Vector3(-0.001f, 0.246f, hatTrans.localPosition.z);
                    }
                    else if (toRandFeat > 0 && toRandFeat < 5)
                    {
                        hatTrans.localPosition = new Vector3(-0.054f, 0.28f, hatTrans.localPosition.z);
                    }
                    else if (toRandFeat > 4 && toRandFeat < 8)
                    {
                        hatTrans.localPosition = new Vector3(0.003f, 0.271f, hatTrans.localPosition.z);
                    }
                    else if (toRandFeat >=8)
                    {
                        hatTrans.localPosition = new Vector3(-0.005f, 0.34f, hatTrans.localPosition.z);
                    }
                }
                else
                {
                    if (toRandFeat == 0)
                    {
                        hatTrans.localPosition = new Vector3(0.001f, 0.235f, hatTrans.localPosition.z);
                    }
                    else if (toRandFeat > 0 && toRandFeat < 5)
                    {
                        hatTrans.localPosition = new Vector3(-0.049f, 0.28f, hatTrans.localPosition.z);
                    }
                    else if (toRandFeat > 4 && toRandFeat < 8)
                    {
                        hatTrans.localPosition = new Vector3(0.003f, 0.271f, hatTrans.localPosition.z);
                    }
                    else if (toRandFeat >= 8)
                    {
                        hatTrans.localPosition = new Vector3(-0.005f, 0.354f, hatTrans.localPosition.z);
                    }
                }
            }
            else if (i == 2)
            {
                toRandFeat = Random.Range(0, posClothes.Length);
                clothes.sprite = posClothes[toRandFeat];
            }
            //else if (i == 3)
            //{
            //    toRandFeat = Random.Range(0, posLuggage.Length);
            //    luggage.sprite = posLuggage[toRandFeat];
            //}
        }
    }

    public void setDir(int dir)
    {
        if (dir < 3)
        {
            dirrection = true;
        }
        else
        {
            dirrection = false;
        }

        if (GameManager.level == 1)
        {
            mult = 1.0f;
        }
        else if (GameManager.level == 2)
        {
            mult = 1.2f;
        }
        else if (GameManager.level == 3)
        {
            mult = 1.4f;
        }
        else
        {
            mult = 0.5f;
        }
    }

    private void move()
    {
        if (dirrection == true)
        {
            self.Translate((speed * mult) * Time.deltaTime, (angle * mult) * Time.deltaTime, 0);
        }
        else
        {
            self.Translate(-(speed * mult) * Time.deltaTime, (angle * mult) * Time.deltaTime, 0);
        }

        if (farEnough == false)
        {
            float distance = Vector3.Distance(startPos, transform.position);
            if (distance > 5)
            {
                farEnough = true;
                selfColldier.enabled = true;
            }
        }
    }

    public void setSpeed(float toSetLower, float toSetUpper)
    {
        float speedUse = Random.Range(toSetLower, toSetUpper);
        float angleUse = Random.Range(-0.75f, 0.75f);
        angle = angleUse;
        speed = speedUse;
    }

    public void reverseMove(bool which)
    {
        Debug.Log("test");
        if (which == true)
        {
            speed = -speed;
        }
        else if (which == false)
        {
            angle = -angle;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BoundaryHori"))
        {
            reverseMove(true);
        }
        else if(collision.CompareTag("BoundaryVerrt"))
        {
            reverseMove(false);
        }
        Debug.Log("AAAAAAAAAA");
    }
}


