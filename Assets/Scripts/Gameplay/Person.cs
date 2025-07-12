using UnityEngine;
using UnityEngine.Android;

public class Person : MonoBehaviour
{
    [SerializeField] private PersonData perData;
    [SerializeField] private Renderer selfSprite;
    [SerializeField] private BoxCollider2D selfColldier;

    private Vector3 startPos;
    private Transform self;
    private bool dirrection;
    private float angle;
    private float moveDelay = 0.25f;
    private bool farEnough = false;
    private float speed;
    private float mult = 1.0f;

    [SerializeField] private SpriteRenderer hat;
    [SerializeField] private SpriteRenderer clothes;
    [SerializeField] private SpriteRenderer luggage;
    [SerializeField] private SpriteRenderer person;

    [SerializeField] private Sprite[] posHat;
    [SerializeField] private Sprite[] posClothes;
    [SerializeField] private Sprite[] posLuggage;
    [SerializeField] private Sprite[] posPer;


    [SerializeField] private Color[] randCol;

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
            toRandFeat = Random.Range(0, randCol.Length);
            if (i == 0)
            {
                //toRandFeat = Random.Range(0, posHat.Length);
                //hat.sprite = posHat[toRandFeat];
                hat.color = randCol[toRandFeat];
            }
            else if (i == 1) 
            {
                //toRandFeat = Random.Range(0, posClothes.Length);
                //clothes.sprite = posClothes[toRandFeat];
                clothes.color = randCol[toRandFeat];
            }
            else if (i == 2)
            {
                //toRandFeat = Random.Range(0, posLuggage.Length);
                //luggage.sprite = posLuggage[toRandFeat];
                luggage.color = randCol[toRandFeat];
            }
            else if (i == 3)
            {
                //toRandFeat = Random.Range(0, posPer.Length);
                //person.sprite = posPer[toRandFeat];
                person.color = randCol[toRandFeat];
            }
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


