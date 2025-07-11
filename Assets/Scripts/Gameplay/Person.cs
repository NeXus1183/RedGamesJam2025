using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField] private PersonData perData;
    [SerializeField] private SpriteRenderer selfSprite;

    private Vector3 startPos;
    private Transform self;
    private bool dirrection;
    private float angle;
    private float moveDelay = 1.5f;
    private bool farEnough = false;


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
        if (dir == 0)
        {
            dirrection = true;
        }
        else
        {
            dirrection= false;
        }
        angle = Random.Range(-0.15f, 0.5f);
    }

    private void move()
    {
        if (dirrection == true)
        {
            self.Translate(1 * Time.deltaTime, angle * Time.deltaTime, 0);
        }
        else
        {
            self.Translate(-1 * Time.deltaTime, angle * Time.deltaTime, 0);
        }

        float distance = Vector3.Distance(startPos, transform.position);
        if (distance > 50)
        {
            farEnough = true;
        }
    }
}


