using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    private int difficultLevel;
    private float timeBetweenSpawns;
    private int score;
    private int planetsSaved;

    private bool musicOn;
    private bool soundsOn;

    void Awake() {
      if(Instance == null) {
        Instance = this;
        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }

    void Start() {
      difficultLevel = 1;
      score = 0;
      planetsSaved = 0;
      timeBetweenSpawns = 2f;
    }

    void Update() {
      ProgressionFunction();
    }

    void ProgressionFunction() {
      if(score >= 100) {
        difficultLevel = 2;
      } else if(score >= 250) {
        difficultLevel = 3;
      }

      switch(difficultLevel) {
        case 1:
          timeBetweenSpawns = 2f;
        break;

        case 2:
          timeBetweenSpawns = 1.75f;
        break;

        case 3:
          timeBetweenSpawns = 1f;
        break;
      }
    }

    public float GetTime() {
      return timeBetweenSpawns;
    }

    public int GetScore() {
      return score;
    }

    public void SetScore(int _score) {
      score += _score;
    }

    public int GetPlanetsSaved() {
      return planetsSaved;
    }

    public void SetPlanetsSaved(int _planets) {
      planetsSaved += _planets;
    }

    public void GameOver() {

    }

}//class















//space
