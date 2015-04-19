using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControls : MonoBehaviour {
    public float maxHealth = 100f;
    public float maxHunger = 100f;
    public float Health = 100f;
    public float Hunger = 0f;

	public float baseSpeed          = 0.1f;
	public float jumpAcceleration   = 400f;
    private float runAcceleration   = 2f;
    //public int attackDamage         = 10;
    //public float attackCooldown     = 0.2f;
    private float groundRadius      = 0.3f;

    public Transform floorCheckCollider;
    public LayerMask whatIsFloor;
    public Slider HealthBar;
    public Slider HungerBar;
    public Collider2D attackCollider; 

	private Rigidbody2D rigidBody2D;
    private bool isOnFloor = false;
    private bool isLookingRight = true;
    private bool isRunning = false;

	private float attackTimer;
	private Animator animator;    

    //Стоимость действий
   	public  float normalHungerRaiseRate    = 0.2f;
   	private float currentHungerRaiseRate;
    private float jumpCost  = 10f;     //За прыжок
    private float runCost   = 0.1f;    //Увеличивается каждые пять кадров, поэтому значение небольшое
    private float regenCost = 0.8f;    //За единицу здоровья
    private float regenRate = 0.001f;   

    //////////////////СОБЫТИЯ ДВИЖКА//////////////////////////

	// Use this for initialization
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        HealthBar.maxValue = maxHealth;
        HungerBar.maxValue = maxHunger;
        HealthBar.value = maxHealth;
        HungerBar.value = maxHunger;
        currentHungerRaiseRate = normalHungerRaiseRate;
        
        StartCoroutine(hungerRaiser());
    }

	void Update () 
	{
        if (!Global.isPaused())
        {
            HUD();
        }
	}

    void FixedUpdate() 
    {
        if (!Global.isPaused())
        {
            //print(Time.fixedDeltaTime);
            floorCheck();
            Attack();
            Movement();
            Regeneration();
        }
    }

    //////////////////ПЕРЕДВИЖЕНИЕ////////////////////////////
    void floorCheck() 
    {
        isOnFloor = Physics2D.OverlapCircle(floorCheckCollider.position, groundRadius, whatIsFloor); 
    }

	void Movement ()
	{
        float forwardMovement = Input.GetAxis("Horizontal") ;
        Walking(forwardMovement);
        Jump();
        TurnAnimation(forwardMovement); 
	}

    void TurnAnimation(float forwardMovement)
    {
        if ((forwardMovement > 0 && !isLookingRight) || (forwardMovement < 0 && isLookingRight)) {
            isLookingRight = !isLookingRight;
            Vector3 turnVector = transform.localScale;
            turnVector.x *= -1;
            transform.localScale = turnVector;
        }
    }

    void Jump() 
    {
        if (CheckHunger(jumpCost) && Input.GetButtonDown("Jump") && isOnFloor)
        {
            rigidBody2D.AddForce(new Vector2(0f, jumpAcceleration));
            IncHunger(jumpCost);
        }
    }

    void Walking(float forwardMovement) {
        isRunning = (isOnFloor && forwardMovement != 0 && Input.GetButton("Run") && CheckHunger(runCost));
        
        if (isOnFloor)//Ходим только по полу
        {
            float speed = forwardMovement * baseSpeed * ((isRunning) ? runAcceleration : 1);
            rigidBody2D.velocity = new Vector2(speed, rigidBody2D.velocity.y);
        }

        if (isRunning)
        {
            IncHunger(runCost);
        }
        //animator.SetBool("walking", speed != 0);
    }

    void Attack()
    {
        /*	if(attackTimer < 0)
                attackTimer = 0;

		
            if(attackTimer == 0)
            {
                bool attacking = Input.GetButtonDown("Attack1");
                if(attacking)	
                {
                    attackCollider.enabled = true;
                    attackTimer = attackCooldown;			
                }
            }
            else if(attackTimer > 0)
            {
                attackCollider.enabled = false;
                attackTimer -= Time.deltaTime;
            }*/
    }

    //////////////////ГОЛОД И ЗДОРОВЬЕ////////////////////////
    void HUD()
    {
        HealthBar.value = Health;
        HungerBar.value = Hunger;
    }

    IEnumerator hungerRaiser()
    {
        //print("ололо");
        while (!Global.isPaused())
        {
            IncHunger(currentHungerRaiseRate);
            yield return new WaitForSeconds(1);
        }
    }

    bool CheckHunger(float Cost) {
        return (Hunger + Cost <= maxHunger);
    }

    void IncParam(ref float Param, float maxParam, float Increment)
    {
        float incResult = Param + Increment;

        if (incResult > maxParam)
        {
            incResult = maxParam;
        }
        else if (incResult < 0)
        {
            incResult = 0;
        }

        Param = incResult;
    }

    void IncHunger(float Cost)
    {
        IncParam(ref Hunger, maxHunger, Cost);
    }

    void IncHealth(float HealthPoint)
    {
        IncParam(ref Health, maxHealth, HealthPoint);
    }


    void Regeneration()
    {
        if (Health < maxHealth && Hunger < maxHunger)
        {
            float regenPoint = (maxHunger - Hunger) * regenRate; // Вот где-то тут може получиться немного халявной регенерации на остатках шкалы голода. Думаю, це нестрашно
            IncHealth(regenPoint);
            IncHunger(regenPoint*regenCost);
        }
    }
}
