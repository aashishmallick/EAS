using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;


public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	
	[SerializeField] GameObject player;
	[SerializeField] GameObject[] spawnPoints;
	[SerializeField] GameObject[] powerUpSpawns;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject soldier;
	[SerializeField] GameObject arrow;
	[SerializeField] GameObject ranger;
	[SerializeField] GameObject healthPowerUp;
	[SerializeField] GameObject speedPowerUp;
	[SerializeField] Text levelText;
	[SerializeField] Text diedEnemyCount;
	[SerializeField] Text endGameText;
	[SerializeField] int finalLevel=10;
	[SerializeField] int maxPowerUps = 10;	

	[SerializeField] Canvas pauseM;
	[SerializeField] Button resumeBtn;

	[SerializeField] public Texture2D cursorTexture;
	
	 
	private int diedEnemyC = 0;

	public bool isPressed = false;


	
	private bool gameOver = false;
	[SerializeField]
	private int currentLevel;
	private float generatedSpawnTime = 1;
	private float currentSpawnTime = 0;
	private float powerUpSpawnTime = 30;
	private float currentPowerUpSpawnTime = 0;
	private GameObject newEnemy;
	private int powerups= 0;
	private GameObject newPowerup;

	private List<EnemyHealth> enemies = new List<EnemyHealth>();
	private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

	public void RegisterEnemy(EnemyHealth enemy){
		enemies.Add(enemy);
	}

	public void KilledEnemy(EnemyHealth enemy){
		killedEnemies.Add(enemy);
		diedEnemyCount.text = "Enemies Killed : " + (++diedEnemyC);

	}

	public void RegisterPowerUp(){
		powerups++;
	}

	public Canvas PauseM{
		get{
			return pauseM;
		}
	}
	public bool IsPressed{
		get{
			return isPressed;
		}
		set{
			isPressed = value;
		}
	}

	public Button ResumeBtn{
		get{
			return resumeBtn;
		}

	}
	public GameObject Arrow{
		get{
			return arrow;
		}
	}
	public GameObject Player{
		get{
			return player;
		}
	}
	public bool GameOver{
		get{
			return gameOver;
		}
	}


	void Awake(){
		if(instance == null){
			instance = this;
		} else if(instance != this){
			Destroy(gameObject);
		}
	}
	
	// Use this for initialization
	void Start () {
		pauseM.GetComponentInParent<Canvas>().enabled = false;
		endGameText.GetComponent<Text>().enabled = false;
		StartCoroutine(spawn());
		StartCoroutine(powerUpSpawn());
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
		currentSpawnTime += Time.deltaTime;
		currentPowerUpSpawnTime += Time.deltaTime;
			
	}

	public void PlayerHit (int currentHP){
		if(currentHP > 0){
			gameOver = false;
		}
		else{
			
			gameOver = true;
			StartCoroutine(endGame("Defeat"));
			}
	}

	IEnumerator spawn(){
		if(currentSpawnTime > generatedSpawnTime){
			currentSpawnTime = 0;
			if(enemies.Count < currentLevel){
				int randomNumber = Random.Range(0, spawnPoints.Length -1);
				
				GameObject spawnLocation = spawnPoints[randomNumber];
				int randomEnemy = Random.Range(0, 3);
				if(randomEnemy == 0){
					newEnemy = Instantiate(soldier) as GameObject;
					newEnemy.SetActive(true);
				} else if (randomEnemy == 1){
					newEnemy = Instantiate(ranger) as GameObject;
					newEnemy.SetActive(true);
				} else if(randomEnemy == 2){
					newEnemy = Instantiate(tanker) as GameObject;
					newEnemy.SetActive(true);
				}
				newEnemy.transform.position = spawnLocation.transform.position;
			}

			if(killedEnemies.Count == currentLevel && currentLevel != finalLevel){
				enemies.Clear();
				killedEnemies.Clear();
				yield return new WaitForSeconds(3);
				currentLevel++;
				levelText.text = "Wave " + currentLevel;
				
			}
			if(killedEnemies.Count == finalLevel){
				StartCoroutine(endGame("Victory!"));
			}
		
		}
		yield return null;
		StartCoroutine(spawn()); 
	}

	IEnumerator powerUpSpawn(){
		if(currentPowerUpSpawnTime > powerUpSpawnTime){
			currentPowerUpSpawnTime = 0;
			if(powerups < maxPowerUps){
				int randomNumber = Random.Range(0, powerUpSpawns.Length -1);
				GameObject spawnLocation = powerUpSpawns [randomNumber];
				int randomPowerUp = Random.Range(0,2);
				if(randomPowerUp == 0){
					newPowerup = Instantiate(healthPowerUp) as GameObject;
				} else if (randomPowerUp == 1){
					newPowerup = Instantiate(speedPowerUp) as GameObject;
				}

				newPowerup.transform.position = spawnLocation.transform.position;
			}
		}
		yield return null;
		StartCoroutine(powerUpSpawn());
	}

	IEnumerator endGame(string outcome){
		endGameText.text = outcome;
		endGameText.GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(10f);
		SceneManager.LoadScene("GameMenu");
	}


}










