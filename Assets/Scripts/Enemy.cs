using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;
    private Coroutine stunRoutine;
    private bool routineRan;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerController>().HasStun)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize(); //get only direction via turning var to 0 or 1;
            rb.AddForce(dir * speed);
        }
    }

    IEnumerator Stun()
    {
        while (player.GetComponent<PlayerController>().HasStun)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            yield return new WaitForSeconds(5f);
            player.GetComponent<PlayerController>().HasStun = false;
            Debug.Log("Done!" + Time.time);
        }
    }
}
