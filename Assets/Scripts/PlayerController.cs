using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public bool keyPressed = false;
	[SerializeField]
	private float moveSpeed = 10.0f;
	[SerializeField]
	private LayerMask layerMask;

	private AudioSource[] aSource;
	private AudioSource pAttack;
	private AudioSource spinAttackS;
	private AudioSource stabSound;
	
	

	private CharacterController characterController;
	private Vector3 currentLookTarget = Vector3.zero;
	private Animator anim;
	private BoxCollider[] swordColliders;
	private GameObject fireTrail;
	private ParticleSystem fireTrailParticles;



	// Use this for initialization
	void Start() {
		aSource = GetComponents<AudioSource>();
		pAttack = aSource[1];
		spinAttackS = aSource[2];
		stabSound = aSource[3];
		fireTrail = GameObject.FindWithTag("Fire") as GameObject;
		fireTrail.SetActive(false);
		fireTrailParticles = GetComponent<ParticleSystem>();
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		swordColliders = GetComponentsInChildren<BoxCollider>();

	}
	
	// Update is called once per frame
	void Update() {

		if(!GameManager.instance.GameOver){
			if(Input.GetKeyDown(KeyCode.Escape)){
				if(!(GameManager.instance.isPressed)){
				GameManager.instance.PauseM.enabled = true;
				GameManager.instance.isPressed = true;
				Time.timeScale = 0.0f;
				}
				else{
	
					GameManager.instance.PauseM.enabled = false;
					GameManager.instance.isPressed = false;
					Time.timeScale = 1.0f;
				}
			}
		}
		if (!GameManager.instance.GameOver && (!GameManager.instance.isPressed)){
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.SimpleMove(moveDirection * moveSpeed);


			if (moveDirection == Vector3.zero){
                anim.SetBool("IsWalking", false);
            } else{
                anim.SetBool("IsWalking", true);
            }  
			if (Input.GetMouseButtonDown(0)){
                anim.Play("DoubleChop");
				pAttack.PlayOneShot(pAttack.clip);
				pAttack.PlayDelayed(0.5f);
								
            } 
			if (Input.GetMouseButtonDown(1)) {
                anim.Play("SpinAttack");
				spinAttackS.PlayOneShot(spinAttackS.clip);
			}
			if(Input.GetKeyDown(KeyCode.C)){
				anim.Play("Chop");
				pAttack.PlayDelayed(0.3f);
			}
			if(Input.GetKeyDown(KeyCode.V)){
				anim.Play("Stab");
				stabSound.PlayOneShot(stabSound.clip);
			}
			if(Input.GetKeyDown(KeyCode.E)){
				//anim.Play("Block");
				//Vector3 dodge = new Vector3(0, 0, -Input.GetAxis("Vertical"));
           // characterController.SimpleMove(-dodge * moveSpeed*10);
			}
			if(Input.GetKeyDown(KeyCode.Escape)){
				if(!(GameManager.instance.isPressed)){
				GameManager.instance.PauseM.enabled = true;
				GameManager.instance.isPressed = true;
				Time.timeScale = 0.0f;
				}
				else{
	
					GameManager.instance.PauseM.enabled = false;
					GameManager.instance.isPressed = false;
					Time.timeScale = 1.0f;
				}
			}
        }
    }

	void FixedUpdate(){
		if(!GameManager.instance.GameOver){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);

		if(Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore)){
			if(hit.point != currentLookTarget){
				currentLookTarget = hit.point;
			}
			Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime*10f);
			}
		}
	}

	public void BeginAttack(){
		foreach(var weapon in swordColliders){
			weapon.enabled = true;
		}
	}

	public void EndAttack(){
		foreach(var weapon in swordColliders){
			weapon.enabled = false;
		}
	}

	public void SpeedPowerUp(){
		StartCoroutine(fireTrailRoutine());
	}

	IEnumerator fireTrailRoutine(){
		fireTrail.SetActive(false);
		moveSpeed += 6;
		yield return new WaitForSeconds(10f);
		moveSpeed -= 6;
		fireTrailParticles = fireTrail.GetComponent<ParticleSystem>();
		var em = fireTrailParticles.emission;
		em.enabled = false;
		yield return new WaitForSeconds(3f);
		em.enabled = true;
		fireTrail.SetActive(false);
		fireTrailParticles.Clear();
	}
}







