using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour/*, IPointerClickHandler */{



	//public float clicktime = 0.5f;
	[Header ("Koi")]
	public int magazine = 3;
	public float sinkSpeed = 2.5f;
	public int bullet;

	[Header("Agent")]
	public float normalSpeed = 15f;

	[Header ("Attack")]
	[Range (0f, 1f)]
	public float attackRate;

	// private variable
	private float maxSpeed;
	private float maxSpeedMultiplier = 2.0f;
	private float currentSpeedMultiplier = 1f;
	private float nextShoot;

	private Transform target;

	private bool doubleClick = false;
	private bool enemyClicked;
	private bool swimming;
	private bool isDead = false;
	private bool isSinking = false;

	PlayerShooting playershooting;

	// store reference to component on gameobject
	NavMeshAgent _agent;
	Animator _animator;
	CapsuleCollider _capsuleCollider;

	//float clicked;

	void Awake() {
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponent<Animator>();
		playershooting = GetComponentInChildren <PlayerShooting> ();
	}

	void Start () {
		maxSpeed = normalSpeed * maxSpeedMultiplier;
		//Debug.Log ("Max speed is: " + maxSpeed);
	}

//	bool DoubleClick ()
//	{
//		if (Input.GetMouseButtonDown (0)) {
//			clicked++;
//		}
//
//		if (clicked >= 2) {
//			return true;
//		}
//		return false;
//	}

    //public void OnPointerClick(PointerEventData data)
	public void OnPointerClick(BaseEventData basedata)
    {
		PointerEventData data = (PointerEventData) basedata;	
    	//Debug.Log("one clicked");
        if (data.clickCount == 2)
        {
        	//Debug.Log("double clicked");
			_agent.speed *= maxSpeedMultiplier; 
			doubleClick = true;
        }
    }

	void OneClickMove ()
	{
		if (Input.GetMouseButtonDown (0)) {
			//_animator.SetFloat ("SpeedMultiplier", 1);
			doubleClick = false;
			_agent.speed = normalSpeed;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.CompareTag ("Enemy")) {
					target = hit.transform;
					enemyClicked = true;
				} else {
					swimming = true;
					enemyClicked = false;
					_agent.SetDestination (hit.point); // move our agent
				}
			}

			if (enemyClicked) {
				ShootBubble ();
			}
		}
	}

	private void ShootBubble ()
	{
		if (target == null) {
			return;
		}

		transform.LookAt (target);

		if (Time.time > nextShoot && magazine > 0) {
			nextShoot = Time.time + attackRate;

			Vector3 dirToShoot = target.transform.position - transform.position;
			//Debug.Log ("Bubble direction is: " +  dirToShoot.normalized);

			magazine -= 1;
			Debug.Log ("there are " + magazine + "left");
			playershooting.Shoot (dirToShoot);
		}
	}

	public void AddBubble (int extraBubble) {
		magazine += extraBubble;
		if (magazine > 3)
			magazine = 3;
	}

	void Death () {
		isDead = true;

		//_capsuleCollider.isTrigger = true;

		_animator.SetTrigger ("_dead");

		Debug.Log("player dead!");
	}

	public void StartSinking () {
		GetComponent <Rigidbody> ().isKinematic = true;

		isSinking = true;

		Destroy(gameObject, 2f);
	}

	void OnTriggerEnter (Collider collider)
	{
		Debug.Log("Enter something");

		if (collider.CompareTag ("Enemy")) {
			Death();
		}
	}

	// Update is called once per frame
	void Update ()
	{
		OneClickMove ();

		if (doubleClick) {
			if (currentSpeedMultiplier < maxSpeedMultiplier) {
				currentSpeedMultiplier += Time.deltaTime;
			} 
		} else {
			currentSpeedMultiplier = 1;
		}

		// update animation conditions
		_animator.SetFloat("_speedMultiplier", currentSpeedMultiplier);
		_animator.SetFloat("_velocity", _agent.velocity.magnitude / maxSpeed);

		// let player start to sinking if he dead
		if (isSinking) {
			transform.Translate (Vector3.down * sinkSpeed * Time.deltaTime);
		}
	}
}
