using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
        
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -7.77f)
        {
            Debug.Log("WOOHOO CHANGING SIDES");
            Vector3 newPosition = new Vector3(7.78f, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        else if (transform.position.x >= 7.78f)
        {
           
            Vector3 newPosition = new Vector3(-7.77f, transform.position.y, transform.position.z);
            transform.position = newPosition;
           
        }

        Debug.Log("Change side x");

        if (transform.position.y <= -3.84f)
        {
           
            Vector3 newPosition = new Vector3(transform.position.x, 3.8f, transform.position.z);
            transform.position = newPosition;
        }

        else if (transform.position.y >= 3.8f)
        {
          
            Vector3 newPosition = new Vector3(transform.position.x, -3.84f, transform.position.z);
            transform.position = newPosition;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enter Collision , in bullet");
        
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter Trigger , in bullet");
        if (collision.gameObject.tag == "Asteroid")
        {
            if (collision.gameObject.GetComponent<Asteroid>().getSize() == 1 && !collision.gameObject.GetComponent<Asteroid>().IsShot())
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Asteroid>().GetComponent<AudioSource>().Play();
                Debug.Log("Sound Played");
                collision.gameObject.GetComponent<Asteroid>().playAnimation();
                Debug.Log("Animation");

                Player[] player = FindObjectsOfType<Player>();
                foreach (Player p in player)
                {
                    p.addPoints(2);
                    p.changePointsText();
                }

                collision.gameObject.GetComponent<Asteroid>().SetShot(true);
                Destroy(collision.gameObject, 3);

            }

            if (collision.gameObject.GetComponent<Asteroid>().getSize() == 2)
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Asteroid>().GetComponent<AudioSource>().Play();
                //PLAY MEDIUM ANIMATION
                collision.gameObject.GetComponent<Asteroid>().playMediumAnimation();

                Player[] player = FindObjectsOfType<Player>();
                foreach (Player p in player)
                {
                    p.createSmallAsteroids(collision);
                    p.addPoints(5);
                    p.changePointsText();
                }

                collision.gameObject.GetComponent<Asteroid>().SetShot(true);
                Destroy(collision.gameObject, 3);
            }

            if (collision.gameObject.GetComponent<Asteroid>().getSize() == 3)
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Asteroid>().GetComponent<AudioSource>().Play();
                //PLAY BIG ANIMATION
                collision.gameObject.GetComponent<Asteroid>().playBigAnimation();

                Player[] player = FindObjectsOfType<Player>();
                foreach (Player p in player)
                {
                    p.createMediumAsteroids(collision);
                    p.addPoints(5);
                    p.changePointsText();
                }

                collision.gameObject.GetComponent<Asteroid>().SetShot(true);
                Destroy(collision.gameObject, 3);
            }

        }
    }
}
