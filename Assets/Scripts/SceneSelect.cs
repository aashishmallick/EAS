using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour {

	public void TerrainSceneS(){
		SceneManager.LoadScene("TerrainScene");
	}

	public void WarzoneSceneS(){
		SceneManager.LoadScene("WarScene");
	}

	public void WastedCityS(){
		SceneManager.LoadScene("destroyed_city");
	}
}
