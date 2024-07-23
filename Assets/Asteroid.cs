using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int size;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isShot = false;

    public bool IsShot()
    {
        return isShot;
    }

    public void SetShot(bool value)
    {
        isShot = value;
    }

    void Start()
    {
        if (size == 1)
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator.SetBool("collideWithBullet", false);
        }
        if (size == 2)
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator.SetBool("collide", false);
        }
        if (size == 3)
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator.SetBool("collideWith", false);
        }
        
    }

    public int getSize()
    {
        return size;
    } 

    public void playAnimation()
    {
        animator.SetBool("collideWithBullet", true);
        
    }

    public void playBigAnimation()
    {
        animator.SetBool("collideWith", true);

    }

    public void playMediumAnimation()
    {
        animator.SetBool("collide", true);

    }

    public void stopAnimation()
    {
        animator.SetBool("collideWithBullet", false);
    }

    public void destroyAsteroid(Asteroid a)
    {
        Destroy(a);
    }

    // Update is called once per frame
    void Update()
    {
        //FIX THE TRANSFORM FOR THE ASTEROIDS
        if (transform.position.x <= -7.77f)
        {
            Debug.Log("WOOHOO CHANGING SIDES");
            Vector3 newPosition = new Vector3(7.78f, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }

        if (transform.position.x >= 7.78f)
        {
            Debug.Log(getSize() + "is here");
            Vector3 newPosition = new Vector3(-7.77f, transform.position.y, transform.position.z);
            transform.position = newPosition;
            Debug.Log(getSize() + " Position: " + transform.position);
        }

        Debug.Log("Change side x");

        if (transform.position.y <= -3.84f)
        {
            Debug.Log(getSize() + "is here" + "Y");
            Vector3 newPosition = new Vector3(transform.position.x, 3.8f, transform.position.z);
            transform.position = newPosition;
        }

        if (transform.position.y >= 3.8f)
        {
            Debug.Log(getSize() + "is here" + "Y");
            Vector3 newPosition = new Vector3(transform.position.x, -3.84f, transform.position.z);
            transform.position = newPosition;
        }

        Debug.Log("Change side y");
    }

}
