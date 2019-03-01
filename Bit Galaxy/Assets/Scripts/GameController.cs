using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    private int difficultLevel;
    private float timeBetweenSpawns;
    private int score;
    private int planetsSaved;

    private bool musicOn;
    private bool soundsOn;

    //Time variables
    private float timePlanet;
  	private float escalaDeTiempo = -1;

  	public Text timeText;
  	private float tiempoDelFrameConTimeScale;
  	private float tiempoAMostrarEnSegundos;
  	private float escalaDeTiempoAlPausar, escalaDeTiempoInicial;
  	private bool isPaused = false;

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

      timePlanet = 11;
      escalaDeTiempoInicial = escalaDeTiempo;
      tiempoAMostrarEnSegundos = timePlanet;
      ActualizarReloj(timePlanet);
    }

    void Update() {
      ProgressionFunction();
      tiempoDelFrameConTimeScale = Time.deltaTime * escalaDeTiempo;
      tiempoAMostrarEnSegundos += tiempoDelFrameConTimeScale;
      ActualizarReloj(tiempoAMostrarEnSegundos);
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

    //Time functions
    void ActualizarReloj(float tiempoEnSegundos) {
  		int segundos = 0;
  		string TextoDelReloj;

  		if (tiempoEnSegundos < 0) {
  			//Change Planet
  		}

  		segundos = (int)tiempoEnSegundos % 60;
  		TextoDelReloj = segundos.ToString();

  		timeText.text = TextoDelReloj;
  	}

    public void Pausar() {
  		if (!isPaused) {
  			isPaused = true;
  			escalaDeTiempoAlPausar = escalaDeTiempo;
  			escalaDeTiempo = 0;
  		}
  	}

  	public void Continuar() {
  		if (isPaused) {
  			isPaused = false;
  			escalaDeTiempo = escalaDeTiempoAlPausar;
  		}
  	}

  	public void Reiniciar() {
  		isPaused = false;
  		escalaDeTiempo = escalaDeTiempoInicial;
  		tiempoAMostrarEnSegundos = timePlanet;
  		ActualizarReloj(tiempoAMostrarEnSegundos);
  	}

    public void GameOver() {

    }

}//class















//space
