using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour/*, IPointerClickHandler */{



	//public float clicktime = 0.5f;

	[Header("_agent")]
	public float speedMultiplier = 2.0f;
	public float moveSpeed = 15f;

	// private variable
	private float _velocity;
	private float maxSpeed;


	// store reference to component on gameobject
	NavMeshAgent _agent;
	Camera _mainCam;
	Animator _animator;
	//float clicked;

	void Awake() {
		_agent = GetComponent<NavMeshAgent>();
		_mainCam = Camera.main;
		_animator = GetComponent<Animator>();
	}

	void Start () {
		maxSpeed = moveSpeed * speedMultiplier;
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
    	Debug.Log("one clicked");
        if (data.clickCount == 2)
        {
        	Debug.Log("double clicked");
			_agent.speed *= speedMultiplier; 
			_animator.SetFloat("SpeedMultiplier", speedMultiplier);
			_animator.SetFloat("SwimSpeed", _agent.speed);
        }
    }

	void OneClickMove ()
	{
		if (Input.GetMouseButtonDown (0)) {
			_animator.SetFloat ("SpeedMultiplier", 1);
			_animator.SetFloat ("SwimSpeed", _agent.speed);
			_agent.speed = moveSpeed;
			Ray ray = _mainCam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				// MOVE OUR AGENT
				_agent.SetDestination (hit.point);
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		OneClickMove ();

		_animator.SetFloat("_velocity", _velocity);
	}
}
