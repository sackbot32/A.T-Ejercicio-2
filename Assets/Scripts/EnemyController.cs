using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyType
{
    Crab,
    Octopus,
    Jumper
}

public class EnemyController : MonoBehaviour
{
    //EnemyType
    [SerializeField] 
    private EnemyType enemyType;
    //Components
    private Transform groundDetect;
    private SpriteRenderer sprite;
    //Ground check 
    private LayerMask layer;
    private float rayLenght = 1.2f;
    //Movement
    [SerializeField] 
    private Vector3 direcction;
    [SerializeField]
    private float speed;
    private bool flipped;
    //Create a float to use to make a sine wave
    private float currentSine;

    // Start is called before the first frame update
    void Start()
    {
        //Give it value and start corruoutine only if jumper
        if(enemyType == EnemyType.Jumper)
        {
            print("Sine on");
            currentSine = 270f;
            StartCoroutine(increaseSine(1,0.1f));
        }
        sprite = GetComponentInChildren<SpriteRenderer>();
        groundDetect = transform.GetChild(1);
        layer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }
    /// <summary>
    /// Detect if enemy touch the floor
    /// </summary>
    /// <returns></returns>
    private bool GroundDetection()
    {
        Debug.DrawRay(groundDetect.position, Vector2.down*rayLenght, Color.magenta, 0.1f);
        return Physics2D.Raycast(groundDetect.position, Vector2.down,rayLenght, layer);
    }
    /// <summary>
    /// Recieves length and direcction and returns if it has detected something
    /// </summary>
    /// <param name="lenght">
    /// Lenght of raycast
    /// </param>
    /// <param name="dir">
    /// Direction of ray
    /// </param>
    /// <returns>if it has hit something</returns>
    private bool GeneralDetection(float lenght, Vector2 dir)
    {
        Debug.DrawRay(groundDetect.position, dir * lenght, Color.magenta, 0.1f);
        return Physics2D.Raycast(groundDetect.position, dir, lenght, layer);
    }

    
    /// <summary>
    /// Move enemy in an exact direction set before at the speed set depending of what type of enemy is it
    /// </summary>
    private void EnemyMove()
    {
        switch (enemyType)
        {
            case EnemyType.Crab:
                if (!GeneralDetection(rayLenght,Vector2.down) || GeneralDetection(rayLenght/2, direcction))
                {
                    direcction = -direcction;
                    sprite.flipX = !sprite.flipX;
                    groundDetect.localPosition = new Vector3(-groundDetect.localPosition.x, groundDetect.localPosition.y, groundDetect.localPosition.z);
                }
            break;
            case EnemyType.Octopus:
                if (GeneralDetection(rayLenght, Vector2.down) || GeneralDetection(rayLenght, Vector2.up))
                {
                    direcction = -direcction;
                    groundDetect.localPosition = new Vector3(-groundDetect.localPosition.x, groundDetect.localPosition.y, groundDetect.localPosition.z);
                }
                break;
            case EnemyType.Jumper:
                if(GeneralDetection(rayLenght, new Vector2(direcction.x,0) ))
                {
                    direcction = new Vector3(-direcction.x, direcction.y,0);
                }
                
                //Use the sine current number to get the point of the wave
                float sine = Mathf.Sin(currentSine);
                direcction = new Vector3(direcction.x,sine);
                break;
        }
        
        transform.position += direcction * speed * Time.deltaTime;
    }
    /// <summary>
    /// Increases the currentSine variable to create a loop for a Mathf.Sin method between 270 and 630
    /// </summary>
    /// <param name="increase">How much it increases</param>
    /// <param name="time">how much time between increases</param>
    /// <returns></returns>
    private IEnumerator increaseSine(float increase,float time)
    {
        while (true)
        {
            currentSine += increase;
            //When it reaches 630 or above it should reset because its an equal number and to avoid going too high
            if (currentSine >= 630)
            {
                currentSine = 270;
            }
            yield return new WaitForSeconds(time);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            PlayerHealth playerH = collision.gameObject.GetComponent<PlayerHealth>();
            if(playerH != null)
            {
                playerH.TakeDamage(1);
            }
        }
    }
}
