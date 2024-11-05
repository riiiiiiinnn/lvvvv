using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private CapsuleCollider col;
    private Animator anim;
    private Score score;
    private Vector3 dir;
    public AudioSource AudioS;
    public AudioSource CoffeesAu;
    public AudioSource CoffeesAuDIO;
    public AudioSource CoffeesAuDIOSSSS;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private int coffees;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject scoreText;
    [SerializeField] public Text coffeesText;
    [SerializeField] private Score scoreScript;

    private bool isSliding;
    private bool isImmortal;

    private int lineToMove = 1;
    public float lineDistance = 3; 
    private float maxSpeed = 110;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        col = GetComponent<CapsuleCollider>();
        score = scoreText.GetComponent<Score>();
        score.scoreMultiplier = 1;
        Time.timeScale = 1;
       
        StartCoroutine(SpeedIncrease());
        isImmortal = false;
        
    }

    private void Update()
    {
        if (swipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
            anim.SetTrigger("right");
            AudioS.Play();
        }

        if (swipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
            anim.SetTrigger("left");
            AudioS.Play();
        }

        if (swipeController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
            anim.SetTrigger("jump");
            AudioS.Play();

        }

        if (swipeController.swipeDown)
        {
            StartCoroutine(Slide());
            anim.SetTrigger("down");
            AudioS.Play();
        }


        if (controller.isGrounded && !isSliding)
            anim.SetBool("run", true);
        else
            anim.SetBool("run", false);

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

        

    }

    private void Jump()
    {
        dir.y = jumpForce;
        anim.SetTrigger("isJumping");
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            if (isImmortal)
                Destroy(hit.gameObject);
            else
            {
                Panel.SetActive(true);
                CoffeesAuDIOSSSS.Play();
                int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
                PlayerPrefs.SetInt("lastRunScore", lastRunScore);
                Time.timeScale = 0;
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coffee")
        {
            coffees++;
            CoffeesAuDIO.Play();
            coffeesText.text = coffees.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BonusStar")
        {
            CoffeesAu.Play();
            StartCoroutine(StarBonus());
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "BonusShield")
        {
            CoffeesAu.Play();
            StartCoroutine(ShieldBonus());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(1);
        if (speed < maxSpeed)
        {
            speed += 1;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator Slide()
    {
        col.center = new Vector3(0, -0.4f, 5f);
        col.height = 2;
        isSliding = true;
        anim.SetBool("isRunning", false);
        anim.SetTrigger("isSliding");

        yield return new WaitForSeconds(1);

        col.center = new Vector3(0, 0.108f, 0);
        col.height = 4.420422f;
        isSliding = false;
    }

    private IEnumerator StarBonus()
    {
        score.scoreMultiplier = 2;

        yield return new WaitForSeconds(5);

        score.scoreMultiplier = 1;
    }

    private IEnumerator ShieldBonus()
    {
        isImmortal = true;

        yield return new WaitForSeconds(5);

        isImmortal = false;
    }
}