using UnityEngine;
using System.Collections;
using Cinemachine;
using TurnBaseUtil;

public class BulletController : MonoBehaviour {

	private Rigidbody2D rb; 

	public Transform bulletSpriteTransform;

	public Transform PlayerTransform { get; set; }

	private bool updateAngle = true; 

	public GameObject bulletSmoke;

	public CircleCollider2D destructionCircle;
	public static GroundController groundController;

	private SpriteRenderer sprite;

	private int currentTurn;

	public GameObject explosionEffectPrefab;

	private bool isDestroyed;

	private GameObject explosionEffectObject;

	// Use this for initialization
	void Start () {
		currentTurn = GameManager.Instance.TurnBaseController.GetCurrentTurnTeamIndex();
		GameManager.Instance.vCam.Follow = gameObject.transform;
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		//rb.velocity = new Vector2(5f, 10f);
		
		Debug.Log("winforce:" + GameManager.Instance.TurnBaseController.TurnProperties.WindForce);
		rb.AddForce(GameManager.Instance.TurnBaseController.TurnProperties.WindForce);
	}
	
	void Update () {
		if( updateAngle ){
			Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y);
            dir.Normalize();			
			float angle = Mathf.Asin (dir.y)*Mathf.Rad2Deg;
			if( dir.x < 0f ){
				angle = 180 - angle;
			}
			
			bulletSpriteTransform.localEulerAngles = new Vector3(0f, 0f, angle+45f);

		}

		if(transform.position.y < -8f || transform.position.x < -18f || transform.position.x > 18f)
        {
			if(!isDestroyed)
				DestroySelf(false);
		}
	}

	private void DestroySelf(bool isDelay)
    {
		isDestroyed = true;
		if (isDelay)
		{
			sprite.enabled = false;
			rb.Sleep();
			//两秒后切换摄像头，为了给特效时间
			StartCoroutine(GameManager.Instance.DelayFuc(() => { Destroy(gameObject); if (currentTurn == GameManager.Instance.TurnBaseController.GetCurrentTurnTeamIndex()) { GameManager.Instance.TurnBaseController.EndTurn(); GameManager.Instance.TurnBaseController.StartTurn(); } }, 3f));
		}
		else
		{
			if (currentTurn == GameManager.Instance.TurnBaseController.GetCurrentTurnTeamIndex())
			{
				GameManager.Instance.TurnBaseController.EndTurn(); GameManager.Instance.TurnBaseController.StartTurn();
			}
			Destroy(gameObject);
		}

	}

	void OnCollisionEnter2D( Collision2D coll ){
		if( coll.collider.tag == "Ground" && !isDestroyed){
			updateAngle = false;
			bulletSmoke.SetActive(false);
			groundController.DestroyGround( destructionCircle );
            if (!isDestroyed)
            {
				explosionEffectObject = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
				Invoke("RemoveEffectTrigger", 0.2f);
				DestroySelf(true);
			}
		}
	}

	void RemoveEffectTrigger()
    {
		explosionEffectObject.GetComponent<CircleCollider2D>().enabled = false;
	}
}
