using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    float moveSpeedTemp;
    Vector2 velocity, knockbackVelocity, dodgeVelocity;
    public Rigidbody2D rigidbody2d;
    private float animTimer = 0f;
    public float animSpeed = 0.3f;
    public float animBobHeight = 0.2f;
    bool moving = false;
    bool isHitByEnemy = false;
    bool isDodging = false;
    private float hitByEnemyAnimTimer = 0f;
    public int health = 3;
    Vector3 knockbackDir;
    private float dodgeTimer = 0f;
    public float dodgeDuration = 0.2f;
    public float dodgeSpeed = 30.0f;
    Vector2 direction;
    Vector2 dodgeDirection;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            return;
        }
        var weapons = GetComponentsInChildren<AbsWeaponController>();

        if (weapons.Length > 0)
        {
            foreach (var weapon in weapons)
            {
                if (GameManager.Instance.equippedWeapon == weapon.GetItemId())
                {
                    weapon.gameObject.SetActive(true);
                    GetComponent<MainWeaponController>().equippedWeapon = weapon;
                    //break;
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitByEnemy) rigidbody2d.velocity = knockbackVelocity;
        else if (isDodging) rigidbody2d.velocity = dodgeVelocity;
        else rigidbody2d.velocity = velocity;
        MoveAnim();
        if (isHitByEnemy) HitByEnemyAnim();
        if (isDodging) DodgeAnim();
        // GameManager.Instance.dungeonController.checkRoom(transform.position);
    }

    public void SetMovement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        velocity = direction * moveSpeed;
    }

    public void Dodge(InputAction.CallbackContext context)
    {
        if (context.performed && !isDodging && direction != Vector2.zero)
        {
            dodgeDirection = direction;
            moveSpeedTemp = moveSpeed;
            isDodging = true;
            dodgeVelocity = dodgeDirection * moveSpeed;
        };
    }

    public void DodgeAnim()
    {
        if (dodgeTimer < dodgeDuration)
        {
            dodgeVelocity = dodgeDirection * Mathf.Lerp(moveSpeedTemp * 4, dodgeSpeed, Mathf.Sin(Mathf.PI * dodgeTimer / dodgeDuration));
            {
                transform.localRotation = Quaternion.Slerp(
                    Quaternion.Euler(0, 0, 350),
                    Quaternion.Euler(0, 0, 10),
                    Mathf.Sin(Mathf.PI * dodgeTimer / dodgeDuration));
            }
            dodgeTimer += Time.deltaTime;
        }
        else
        {
            transform.localRotation = Quaternion.identity;
            moveSpeed = moveSpeedTemp;
            dodgeTimer = 0f;
            isDodging = false;
        }
    }

    public void ascendStaircase()
    {
        // GameManager.Instance.dungeonController.nextFloor();
        // transform.position = GameManager.Instance.dungeonController.getFloorStart();
    }

    public void DungeonButton()
    {
        Debug.Log("test!");
    }

    private bool isMoving()
    {
        return (velocity.x != 0 || velocity.y != 0);
    }

    private void MoveAnim()
    {
        if (isMoving())
        {
            animTimer += animSpeed;
            transform.localScale = new Vector3(0.75F, (float)(0.75 - animBobHeight + Math.Abs(animBobHeight * Math.Sin(animTimer / 10))), 0.0F);
        }
    }

    public void HitByEnemy(Vector3 knockbackDir, int damage)
    {
        if (!isHitByEnemy)
        {
            isHitByEnemy = true;
            health -= damage;
            this.knockbackDir = knockbackDir;
        }
    }
    public void HitByEnemyAnim()
    {
        knockbackVelocity = Vector2.Lerp(knockbackDir, Vector2.zero, (float)Math.Sin(hitByEnemyAnimTimer * Math.PI));
        hitByEnemyAnimTimer += Time.deltaTime;
        if (hitByEnemyAnimTimer > 0.2) { isHitByEnemy = false; hitByEnemyAnimTimer = 0; };
    }
}
