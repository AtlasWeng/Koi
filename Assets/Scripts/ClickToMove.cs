using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour/*, IPointerClickHandler */{



	//public float clicktime = 0.5f;

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

	PlayerShooting playershooting;

	// store reference to component on gameobject
	NavMeshAgent _agent;
	Animator _animator;

	//float clicked;

	void Awake() {
		_agent = GetComponent<NavMeshAgent>();
		_animator = GetComponent<Animator>();
		playershooting = GetComponentInChildren <PlayerShooting> ();
	}

	void Start () {
		maxSpeed = normalSpeed * maxSpeedMultiplier;
		Debug.Log ("Max speed is: " + maxSpeed);
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
		if (Time.time > nextShoot) {
			nextShoot = Time.time + attackRate;

			transform.LookAt (target);

			Vector3 dirToShoot = target.transform.position - transform.position;
			//Debug.Log ("Bubble direction is: " +  dirToShoot.normalized);

			playershooting.Shoot (dirToShoot);
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
	}
}
