using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb;
    SpriteRenderer sr;
    public float fallLimitY = -6f;


    public float bounceForce = 7f;

    public int activeColorCount = 2;

    void Update()
    {
        if (GameManager.Instance.IsGameOver())
            return;

        if (transform.position.y < fallLimitY)
        {
            Debug.Log("MISSED GROUND");
            GameManager.Instance.GameOver();
        }
    }

    public enum BallColor
    {
        Red,
        Blue,
        Green,
        Yellow
    }

    public BallColor currentColor;

    Color[] visualColors =
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow
    };

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        Bounce();
        ChangeColor();
    }

    void Bounce()
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }


    void ChangeColor()
    {
        int index = Random.Range(0, activeColorCount);
        sr.color = visualColors[index];
        currentColor = (BallColor)index;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.IsGameOver())
            return;

        
        if (rb.linearVelocity.y > 0)
            return;

        
        TileColor[] tiles = collision.collider.GetComponentsInParent<TileColor>();
        if (tiles.Length == 0)
            return;

        bool mismatchFound = false;

        foreach (TileColor tile in tiles)
        {
            
            ContactPoint2D contact = collision.contacts[0];
            if (contact.normal.y < 0.5f)
                continue;

            if ((int)currentColor != (int)tile.tileColor)
            {
                mismatchFound = true;
                break;
            }
        }

        if (mismatchFound)
        {
            Debug.Log("MISMATCH");
            GameManager.Instance.GameOver();
        }
        else
        {
            Debug.Log("MATCH");
            GameManager.Instance.AddScore(10);
            Bounce();
            ChangeColor();
        }
    }


}
