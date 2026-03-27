using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;

    private InputAction moveAction;
    private InputAction smashAction;
    private InputAction breakAction;

    [SerializeField] Transform focalPoint;
    public bool HasPowerUp;
    private Coroutine powerUpRoutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        smashAction = InputSystem.actions.FindAction("Smash");
        breakAction = InputSystem.actions.FindAction("Break");
    }

    // Update is called once per frame
    void Update()
    {
        var move = moveAction.ReadValue<Vector2>();
        rb.AddForce(move.y * speed * focalPoint.forward);
        if (breakAction.IsPressed())
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            HasPowerUp = true;
            Destroy(other.gameObject);
            if (powerUpRoutine != null)
            {
                StopCoroutine(powerUpRoutine); //stopping the coroutine b4 starting it again to avoid it from running the same coroutine at the same time
            }
            powerUpRoutine = StartCoroutine(PowerUpCooldown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (HasPowerUp & collision.gameObject.CompareTag("Enemy"))
        {
            //var v = collision.rigidbody.linearVelocity; //get enemy direction, wow!
            //v.Normalize();

            var dir = collision.rigidbody.position - transform.position; //this is better since linearVelocity can get kinda fucky e.g. giving you lower than desired values
            //first val here is the end of direction, second val is the start of direction
            dir.Normalize();
            collision.rigidbody.AddForce(dir * 10, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(5f);
        HasPowerUp = false;
    }
}
