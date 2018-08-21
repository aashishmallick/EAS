using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PauseMenu : MonoBehaviour {

	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResumeGame(){
		GameManager.instance.PauseM.enabled = false;
		Time.timeScale = 1.0f;
		GameManager.instance.isPressed = false;
		}

	public void MainMenu(){
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("GameMenu");
		
	}

	public void QuitGame(){
		Application.Quit();
	}
	
}
