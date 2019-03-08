using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    private int difficultLevel;
    private float timeBetweenSpawns;
    private int score;
    private int highScore;
    private int planetsSaved;

    private bool musicOn;
    private bool soundsOn;

    //Time variables
    private float timePlanet;
  	private float escalaDeTiempo = -1;

  	public Text timeText;
    public Text scoreText;
    public Text planetsSavedText;
  	private float tiempoDelFrameConTimeScale;
  	private float tiempoAMostrarEnSegundos;
  	private float escalaDeTiempoAlPausar, escalaDeTiempoInicial;
  	private bool isPaused = false;

    public MeteoriteManager meteoriteSystem;
    public PlayerScript player;
    private PlanetScript currentPlanet;
    public GameObject teletransportEffect;
    public Transform playerPos;
    public GameObject planetPrefab;
    public Transform rightPos;
    public StarsScript starsBack_01;
    public StarsScript starsBack_02;

    private AudioSource audioTransport;

    void Awake() {
      if(Instance == null) {
        Instance = this;
      }
      audioTransport = GetComponent<AudioSource>();
    }

    void Start() {
      difficultLevel = 1;
      score = 0;
      planetsSaved = 0;
      timeBetweenSpawns = 2f;

      timePlanet = 10;
      escalaDeTiempoInicial = escalaDeTiempo;
      tiempoAMostrarEnSegundos = timePlanet;
      ActualizarReloj(timePlanet);
      Pausar();
      StartCoroutine(StartGame(2f));
    }

    void Update() {
      ProgressionFunction();
      tiempoDelFrameConTimeScale = Time.deltaTime * escalaDeTiempo;
      tiempoAMostrarEnSegundos += tiempoDelFrameConTimeScale;
      ActualizarReloj(tiempoAMostrarEnSegundos);
      scoreText.text = "Score: " + score.ToString();

      if(score > Singleton.GetInstance().HighScore) {
        Singleton.GetInstance().HighScore = score;
      }
      Singleton.GetInstance().TotalPlanets = planetsSaved;
    }

    IEnumerator StartGame(float timer) {
      yield return new WaitForSeconds(timer);
      audioTransport.Play();
      Instantiate(teletransportEffect, transform.position, Quaternion.identity);
      player.Teletransport_In(playerPos.position);
      Continuar();
    }

    void ProgressionFunction() {
      if(score >= 50 && score <= 99) {
        difficultLevel = 2;
      } else if(score >= 100 && score <= 199) {
        difficultLevel = 3;
      } else if(score >= 200 && score <= 299) {
        difficultLevel = 4;
      } else if(score >= 300 && score <= 399) {
        difficultLevel = 5;
      } else if(score >= 400 && score <= 499) {
        difficultLevel = 6;
      } else if(score >= 500 && score <= 599) {
        difficultLevel = 7;
      }


      switch(difficultLevel) {
        case 1:
          timeBetweenSpawns = 2f;
        break;

        case 2:
          timeBetweenSpawns = 1.75f;
        break;

        case 3:
          timeBetweenSpawns = 1.5f;
        break;

        case 4:
          timeBetweenSpawns = 1.25f;
        break;

        case 5:
          timeBetweenSpawns = 1f;
        break;

        case 6:
          timeBetweenSpawns = 0.75f;
        break;

        case 7:
          timeBetweenSpawns = 0.5f;
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

    void ChangePlanet() {
      StartCoroutine(PlanetToDestroy(0.4f));
      planetsSaved += 1;
      planetsSavedText.text = "Planets: " + planetsSaved.ToString();
      meteoriteSystem.StopMeteorites();
      audioTransport.Play();
      Instantiate(teletransportEffect, transform.position, player.gameObject.transform.rotation);
      player.Teletransport_Out();
      Pausar();
    }

    IEnumerator PlanetToDestroy(float timer) {
      yield return new WaitForSeconds(timer);
      Instantiate(planetPrefab, rightPos.position, transform.rotation);
      currentPlanet.planetState = 2;
      if(starsBack_01.starsState == 2) {
        starsBack_01.starsState = 1;
      } else if(starsBack_01.starsState == 3) {
        starsBack_01.starsState = 2;
      }
      if(starsBack_02.starsState == 2) {
        starsBack_02.starsState = 1;
      } else if(starsBack_02.starsState == 3) {
        starsBack_02.starsState = 2;
      }
      StartCoroutine(GameContinue(1.2f));
    }

    IEnumerator GameContinue(float timer) {
      yield return new WaitForSeconds(timer);
      audioTransport.Play();
      Instantiate(teletransportEffect, transform.position, Quaternion.identity);
      player.Teletransport_In(playerPos.position);
      Continuar();
      meteoriteSystem.StartMeteorites();
    }

    void OnTriggerEnter2D(Collider2D target) {
      if(target.gameObject.CompareTag("Planet")) {
        currentPlanet = target.gameObject.GetComponent<PlanetScript>();
      }
    }

    //Time functions
    void ActualizarReloj(float tiempoEnSegundos) {
  		int segundos = 0;
  		string TextoDelReloj;

  		if (tiempoEnSegundos < 0) {
  			//Change Planet
        Reiniciar();
        ChangePlanet();
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
      StartCoroutine(LoadScene("GameOver"));
    }

    IEnumerator LoadScene(string sceneName) {
      yield return new WaitForSeconds(0.1f);
      SceneManager.LoadScene(sceneName);
    }

}//class















//space
