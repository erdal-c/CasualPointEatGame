using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerContoler : MonoBehaviour
{
    float playerHealth = 100f;
    public float playerSpeed = 1000f;
    float dashPower = 500f;
    float dashCoolDown = 2f;

    Vector2 mousePos;
    Rigidbody playerRB;

    public int dotPoint;
    public int TotalPoint;

    bool isDash;
    float dashTime;

    [SerializeField] LayerMask dotLayer;
    Collider[] dotColliderArray;

    public AudioSource EatSound;
    public AudioSource DashSound;
    public AudioSource HitSound;

    public float PlayerSpeedProperty { get { return playerSpeed; } 
                                       set { playerSpeed = value;} }

    public float DashSpeedProperty { get { return dashPower; }
                                     set { dashPower = value;} }

    public float DashTimeProperty { get { return dashCoolDown; } 
                                    set { dashCoolDown = value;} }
    public float PlayerHealthProperty
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    void Start()
    {
        playerRB= GetComponent<Rigidbody>();
    }

    void Update()
    {
        MouseCheck();
        if (Input.GetMouseButtonDown(0) && dashTime <= Time.timeSinceLevelLoad && Time.timeScale > 0)
        {
            isDash= true;
            dashTime = Time.timeSinceLevelLoad + 0.4f;
            DashSound.Play();
        }
        MapPointMove();
    }

    private void FixedUpdate()
    {
        playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, 5f);
        if(mousePos.magnitude > 50f) 
        {
            playerRB.AddForce(mousePos.normalized * playerSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            playerRB.velocity = Vector2.Lerp(playerRB.velocity, Vector2.zero, 0.1f);
        }

        if (isDash)
        {
            Dash();
        }
    }

    void MouseCheck()
    {
        mousePos = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0f);               
    }
    void Dash()
    {
        if (isDash && dashTime>Time.timeSinceLevelLoad) 
        {
            dashTime-= Time.fixedDeltaTime;
            playerRB.AddForce(mousePos.normalized * dashPower, ForceMode.Acceleration);
            
        }
        else if (dashTime <= Time.timeSinceLevelLoad)
        {
            isDash = false;
            dashTime += dashCoolDown;
        }
    }
    void DotEat()
    {
        dotPoint++;
        TotalPoint++;
    }
    public int ReturnDot()
    {
        return dotPoint;
    }

    public void ResetDot()
    {
        dotPoint= 0;
    }

    void MapPointMove()
    {
        dotColliderArray = Physics.OverlapSphere(transform.position, 12f, dotLayer);
        MiniMap.instance.MapPointUploader(dotColliderArray, transform);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Dot"))
        {
            ObjectPool.ýnstance.AddQueue("dotpool", collision.gameObject);
            DotEat();
            EatSound.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            playerHealth -= 20;
            UIManager.Instance.HealthText(playerHealth);
            HitSound.Play();
            if(playerHealth <= 0)
            {
                UIManager.Instance.GameOverMenu();
            }
        }
    }
}
