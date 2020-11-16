using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    float jump = 5.0f;
    bool onPlane = false;
    int deathPressed;
    bool death = false;
    int health = 10;
    

    public GameObject ParentCube;
    public Rigidbody playerRb;
    public Animator PlayerAnim;
    public GameObject HealthText;

    // Start is called before the first frame update
    void Start()
    {
        //Only use for Private Decalration of on top
        //PlayerAnim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        GamePlay();
    }
    private void OnCollisionEnter(Collision collision) //OnCollision On Plane For Single Jump
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            onPlane = true;
        }
    }
    private void DeathPressed() //Display Health Text
    {
        deathPressed++;
        health--;
        HealthText.GetComponent<Text>().text = ("Health: " + health);
    }
    private void GamePlay() //Gameplay will happen only if death is false
    {
        if (!death)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                PlayerAnim.SetBool("isMove", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                PlayerAnim.SetBool("isMove", true);
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                PlayerAnim.SetBool("isMove", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, -90, 0);
                PlayerAnim.SetBool("isMove", true);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                PlayerAnim.SetBool("isMove", true);
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                PlayerAnim.SetBool("isMove", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && onPlane)
            {
                playerRb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                PlayerAnim.SetTrigger("trigFlip");
                onPlane = false;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                DeathPressed();
            }

            if (Input.GetKeyDown(KeyCode.K) && deathPressed == 10)
            {
                PlayerAnim.SetTrigger("trigDeath");
                death = true;
            }
        }
    }
}
