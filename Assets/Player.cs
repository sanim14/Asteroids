using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField] Flame rearFlame;
    [SerializeField] Flame rightWingFlame;
    [SerializeField] Flame leftWingFlame;
    [SerializeField] GameObject bigAsteroid;
    [SerializeField] GameObject midAsteroid;
    [SerializeField] GameObject smallAsteroid;
    [SerializeField] Text livesText;
    [SerializeField] Text gameOverText;
    [SerializeField] Text pointText;
    bool gameOver = false;
    float asteroidCoolDown = 0f;
    

    int lives = 3;
    float bigAsteroidTimer = 15f;
    float midAsteroidTimer = 10f;
    float smallAsteroidTimer = 5f;
    int points = 0;

    [SerializeField] GameObject bullet;
    float rotateAmount = 90f;
    float speed;

    bool isBlinking = false;
    float blinkInterval = 0.1f;
    float timeSinceLastBlink = 0f;
    int blinkCount = 0;
    int maxBlinkCount = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rearFlame.gameObject.SetActive(false);
        rightWingFlame.gameObject.SetActive(false);
        leftWingFlame.gameObject.SetActive(false);
    }

   public void createMediumAsteroids(Collider2D collision)
    {
        Vector2 asteroidVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

        Vector2 perpendicularVelocity = new Vector2(-asteroidVelocity.y, asteroidVelocity.x);

        GameObject b = Instantiate(midAsteroid, collision.gameObject.transform.position + collision.gameObject.transform.up * 0.5f, collision.gameObject.transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = asteroidVelocity + perpendicularVelocity * 0.5f;

        GameObject c = Instantiate(midAsteroid, collision.gameObject.transform.position - collision.gameObject.transform.up * 0.5f, collision.gameObject.transform.rotation);
        c.GetComponent<Rigidbody2D>().velocity = asteroidVelocity - perpendicularVelocity * 0.5f;
    }

    public void createSmallAsteroids(Collider2D collision)
    {
        Vector2 asteroidVelocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

        Vector2 perpendicularVelocity = new Vector2(-asteroidVelocity.y, asteroidVelocity.x);

        GameObject b = Instantiate(smallAsteroid, collision.gameObject.transform.position + collision.gameObject.transform.up * 0.5f, collision.gameObject.transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = asteroidVelocity + perpendicularVelocity * 0.5f;

        GameObject c = Instantiate(smallAsteroid, collision.gameObject.transform.position - collision.gameObject.transform.up * 0.5f, collision.gameObject.transform.rotation);
        c.GetComponent<Rigidbody2D>().velocity = asteroidVelocity - perpendicularVelocity * 0.5f;
    }

    public void changePointsText()
    {
        pointText.text = "Points: " + points;
    }

    public void addPoints(int num)
    {
        points += num;
    }

    public void resetGame()
    {
        //WORKS
        GetComponent<SpriteRenderer>().enabled = true;
        isBlinking = false;
        blinkInterval = 0.1f;
        timeSinceLastBlink = 0f;
        blinkCount = 0;
        maxBlinkCount = 20;


        points = 0;
        gameOver = false;
        lives = 3;
        Vector3 position = new Vector3(1.73f, -1.16f, 0);
        transform.position = position;
        rearFlame.gameObject.SetActive(false);
        rightWingFlame.gameObject.SetActive(false);
        leftWingFlame.gameObject.SetActive(false);
        Asteroid[] asteroid = FindObjectsOfType<Asteroid>();
        foreach (Asteroid a in asteroid)
        {
            Destroy(a.gameObject);
        }
        pointText.text = "Points: 0";
        gameOverText.text = " ";
        livesText.text = "Lives: | | |";
        asteroidCoolDown = 0f;
        bigAsteroidTimer = 15f;
        midAsteroidTimer = 10f;
        smallAsteroidTimer = 5f;
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKey(KeyCode.R))
        {
            resetGame();
        }

        if (gameOver)
        {
            return;
        }

        if (asteroidCoolDown != 0f)
        {
            asteroidCoolDown -= Time.deltaTime;
        }

        if (isBlinking)
        {
            timeSinceLastBlink += Time.deltaTime;

            if (timeSinceLastBlink >= blinkInterval)
            {
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
                timeSinceLastBlink = 0f;
                blinkCount++;

                if (blinkCount >= maxBlinkCount)
                {
                    isBlinking = false;
                    GetComponent<SpriteRenderer>().enabled = true;
                    blinkCount = 0;
                }
            }
        }


        if (transform.position.x <= -7.77f)
        {
            Vector3 newPosition = new Vector3(7.78f, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        else if (transform.position.x >= 7.78f)
        {
            Vector3 newPosition = new Vector3(-7.77f, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }


        if (transform.position.y <= -3.84f)
        {

            Vector3 newPosition = new Vector3(transform.position.x, 3.8f, transform.position.z);
            transform.position = newPosition;
            Debug.Log("Player Position: " + newPosition);
            Debug.Log("Player Position: " + transform.position);
        }

        else if (transform.position.y >= 3.8f)
        {
            //Debug.Log("YAYYY PLAYER SWITCHED SIDES Y");
            Vector3 newPosition = new Vector3(transform.position.x, -3.84f, transform.position.z);
            transform.position = newPosition;
        }



        //if the y position of the asteroid is greater than the range than move it to the opposite side 
        if (bigAsteroidTimer <= 0f)
        {
            Vector3 position = new Vector3(Random.Range(-7.77f, 7.78f), Random.Range(-3.84f, 3.8f), 0);
            GameObject b = Instantiate(bigAsteroid, position, Quaternion.identity);
            b.GetComponent<Rigidbody2D>().velocity = b.transform.right * .5f;

            bigAsteroidTimer = 15f;
        }
        else
        {
            bigAsteroidTimer -= Time.deltaTime;
        }

        if (midAsteroidTimer <= 0f)
        {
            Vector3 position = new Vector3(Random.Range(-7.77f, 7.78f), Random.Range(-3.84f, 3.8f), 0);
            GameObject b = Instantiate(midAsteroid, position, Quaternion.identity);
            b.GetComponent<Rigidbody2D>().velocity = b.transform.up * .5f;

            midAsteroidTimer = 10f;
        }
        else
        {
            midAsteroidTimer -= Time.deltaTime;
        }

        if (smallAsteroidTimer <= 0f)
        {
            Vector3 position = new Vector3(Random.Range(-7.77f, 7.78f), Random.Range(-3.84f, 3.8f), 0);
            GameObject b = Instantiate(smallAsteroid, position, Quaternion.identity);
            b.GetComponent<Rigidbody2D>().velocity = b.transform.right * .5f;

            smallAsteroidTimer = 5f;
        }
        else
        {
            smallAsteroidTimer -= Time.deltaTime;
        }


        rearFlame.gameObject.SetActive(false);
        rightWingFlame.gameObject.SetActive(false);
        leftWingFlame.gameObject.SetActive(false);

        speed = 5f;
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            showLeftWingFlame();
            showRightWingFlame();
            dir += Vector2.right * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            showRearFlame();
            dir -= Vector2.right * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.rotation += rotateAmount * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.rotation -= rotateAmount * Time.deltaTime;
        }

        dir = Quaternion.Euler(0, 0, rb.rotation) * dir;

        rb.velocity += dir * Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = Instantiate(bullet, transform.position + transform.right * 1.5f, transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = b.transform.right * .5f;
        }
    }

    public void showRearFlame()
    {
        rearFlame.gameObject.SetActive(true);

        Vector3 offset = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * new Vector3(-1f, 0f, 0);
        rearFlame.transform.position = transform.position + offset;
        rearFlame.transform.rotation = transform.rotation;
    }

    public void showRightWingFlame()
    {
        rightWingFlame.gameObject.SetActive(true);

        Vector3 offset = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * new Vector3(0.8f, -.45f, 0);
        rightWingFlame.transform.position = transform.position + offset;
        rightWingFlame.transform.rotation = transform.rotation;
    }

    public void showLeftWingFlame()
    {
        leftWingFlame.gameObject.SetActive(true);

        Vector3 offset = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * new Vector3(0.8f, 0.40f, 0);
        leftWingFlame.transform.position = transform.position + offset;
        leftWingFlame.transform.rotation = transform.rotation;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (asteroidCoolDown <= 0f)
        {
            //Play animation for rocket
            Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
            foreach (Asteroid asteroid in asteroids)
            {
                if (asteroid.gameObject.Equals(other.gameObject))
                {
                    if (!isBlinking)
                    {
                        isBlinking = true;
                        lives--;
                    }

                    if (lives == 2)
                    {
                        livesText.text = "Lives: | |";
                        asteroidCoolDown = 2f;
                    }
                    else if (lives == 1)
                    {
                        livesText.text = "Lives: |";
                        asteroidCoolDown = 2f;
                    }
                    else
                    {
                        livesText.text = "Lives: ";
                        gameOverText.text = "You lose";
                        gameOver = true;
                    }
                }
            }
        }
    }
}