using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;

    //private Coroutine ByeRoutine;

    void Start()
    {
        //InvokeRepeating(nameof(RandomSpawn), 0, 2f);
        //StartCoroutine(Hello());
        //ByeRoutine = StartCoroutine(Bye());   
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        //if (Time.time > 3)
        //{
        //    //StopAllCoroutines();
        //    StopCoroutine(ByeRoutine); //these are pretty self explanatory
        //}
    }

    void RandomSpawn()
    {
        var index = Random.Range(0, spawnPoints.Length);
        var spawnPoint = spawnPoints[index];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            RandomSpawn();
            yield return new WaitForSeconds(3);
        }
    }

    //IEnumerator Hello(float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    Debug.Log("Hello" + Time.frameCount); //1st frame
    //    yield return null; //waits one frame
    //    Debug.Log("Hello" + Time.frameCount); //2nd frame
    //}

    //IEnumerator Bye()
    //{
    //    while (true)
    //    {
    //        Debug.Log("Bye" + Time.frameCount + "" + Time.time);
    //        yield return new WaitForSeconds(1);

    //        yield return Hello(4); //this is how you call coroutines in coroutines, though this will wait for the coroutine to finish first
    //        //StartCoroutine(Hello()) this one wont wait for Hello() to finish first
    //        yield return new WaitForSeconds(1);
    //        //yield return null; //loops but skips one frame, frame count increments infinitely

    //        if (Time.time > 5)
    //        {
    //            yield break; //stop coroutine within itself //normal break; is for getting out of loops
    //        }
    //    }
    //}
}
