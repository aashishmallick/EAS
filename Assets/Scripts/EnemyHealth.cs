using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {


	[SerializeField] private int startingHealth = 20;
	[SerializeField] private float timeSinceLastHit = 0.5f;
	[SerializeField] private float dissappearSpeed = 2f;

	private AudioSource audioE;
	private float timer = 0f;
	private Animator anim;
	private UnityEngine.AI.NavMeshAgent nav;
	private bool isAlive;
	private Rigidbody rigidBody;
	private CapsuleCollider capsuleCollider;
	private bool dissappearEnemy = false;
	private int currentHealth;
	private ParticleSystem blood;


	public bool IsAlive{
		get{
			return isAlive;
		}
	}
	// Use this for initialization
	void Start () {
		GameManager.instance.RegisterEnemy(this);
		anim = GetComponent<Animator>();
		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		rigidBody = GetComponent<Rigidbody>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		audioE = GetComponent<AudioSource>();
		isAlive = true;
		currentHealth = startingHealth;
		blood = GetComponentInChildren<ParticleSystem>();
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(dissappearEnemy){
			transform.Translate(-Vector3.up * dissappearSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other){
		if(timer >= timeSinceLastHit && !GameManager.instance.GameOver){
			if(other.tag == "PlayerWeapon"){
				takeHit();
				timer = 0f;
			}
		}
	}
	public void takeHit(){
		if(currentHealth > 0){
			audioE.PlayOneShot(audioE.clip);
			anim.Play("Hurt");
			currentHealth -= 10;
			blood.Play();
		}

		if(currentHealth <=0){
			isAlive = false;
			KillEnemy();
		}
	}

	void KillEnemy(){
		GameManager.instance.KilledEnemy(this);
		capsuleCollider.enabled = false;
		nav.enabled = false;
		anim.SetTrigger("EnemyDie");
		rigidBody.isKinematic = true;
		blood.Play();
		StartCoroutine(removeEnemy());
	}

	IEnumerator removeEnemy(){
		//wait for seconds after enemy dies
		yield return new WaitForSeconds(4f);
		//start to sink the enemy
		dissappearEnemy = true;
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	} 
}

