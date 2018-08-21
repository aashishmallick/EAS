using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	[SerializeField] GameObject hero;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject soldier;
	[SerializeField] GameObject ranger;

	[SerializeField] Image backGround;
	[SerializeField] Image aboutGame;

	[SerializeField] public Texture2D cursorTexture;

	private Animator heroAnim;
	private Animator tankerAnim;
	private Animator soldierAnim;
	private Animator rangerAnim;
	
	void Awake(){
		Assert.IsNotNull(hero);
		Assert.IsNotNull(tanker);
		Assert.IsNotNull(soldier);
		Assert.IsNotNull(ranger);
	}
	
	// Use this for initialization
	void Start () {
		backGround.GetComponent<Image>().enabled = false;
		aboutGame.GetComponent<Image>().enabled = false;
		heroAnim = hero.GetComponent<Animator>();
		tankerAnim = tanker.GetComponent<Animator>();
		soldierAnim = soldier.GetComponent<Animator>();
		rangerAnim = ranger.GetComponent<Animator>();
		StartCoroutine(showcase());
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
		if(Input.GetKeyDown(KeyCode.Escape)){
			backGround.enabled = false;
			aboutGame.enabled = false;
		}
	
	}

	IEnumerator showcase(){
		yield return new WaitForSeconds(1f);
		heroAnim.Play("SpinAttack");
		yield return new WaitForSeconds(1f);
		tankerAnim.Play("Attack");
		yield return new WaitForSeconds(1f);
		soldierAnim.Play("Attack");
		yield return new WaitForSeconds(1f);
		rangerAnim.Play("Attack");

		yield return new WaitForSeconds(1f);
		StartCoroutine(showcase());
	}

	public void Battle(){
		SceneManager.LoadScene("SceneMenu");

	}

	public void AboutGamePopUp(){
		backGround.enabled = true;
		aboutGame.enabled = true;
	}

	public void Quit(){
		Application.Quit();
	}
}
