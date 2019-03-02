using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    public GameObject[] Meteorite = new GameObject[5];
    private int spawnIndex;
    private int meteoriteIndex;

    private float time;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
      time = GameController.Instance.GetTime();
      StartMeteorites();
    }

    // Update is called once per frame
    void Update()
    {
      time = GameController.Instance.GetTime();
    }

    IEnumerator SpawnMeteorite(float _time) {
      yield return new WaitForSeconds(_time);
      spawnIndex = Random.Range(0, transform.childCount);
      meteoriteIndex = Random.Range(0, Meteorite.Length);
      Instantiate(Meteorite[meteoriteIndex], transform.GetChild(spawnIndex).position, Quaternion.identity);
      coroutine = SpawnMeteorite(time);
      StartCoroutine(coroutine);
    }

    public void StopMeteorites() {
      Debug.Log("Meteorites stopped");
      StopCoroutine(coroutine);
    }

    public void StartMeteorites() {
      Debug.Log("Meteorites start");
      StartCoroutine(SpawnMeteorite(0f));
    }

}//class
