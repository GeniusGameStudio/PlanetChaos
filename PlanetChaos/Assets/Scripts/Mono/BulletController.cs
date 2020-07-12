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

	private int currentPlayerCount;

	public GameObject explosionEffectPrefab;

	private bool isDestroyed;

	private GameObject explosionEffectObject;

	private AudioSource SFX;
	public AudioClip boomSFX;

	// Use this for initialization
	void Start () {
		currentPlayerCount = GameManager.Instance.TurnBaseController.GetAllTeamPlayerCount();
		//GameManager.Instance.vCam.m_Lens.OrthographicSize = 5;
		GameManager.Instance.vCam.Follow = gameObject.transform;
		rb = GetComponent<Rigidbody2D>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		SFX = GetComponent<AudioSource>();
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

		if(transform.position.y < -9f || transform.position.x < -18f || transform.position.x > 18f)
        {
			if(!isDestroyed)
				DestroySelf(false, 0.5f);
		}
	}

	private void DestroySelf(bool isDelay, float duration = 2f)
    {
		isDestroyed = true;
		if (isDelay)
		{
			sprite.enabled = false;
			rb.velocity = Vector2.zero;
			rb.isKinematic = true;
			
		}
		//duration秒后切换摄像头，为了给特效时间
		StartCoroutine(GameManager.Instance.DelayFuc(() => {
			Destroy(gameObject);
			if (currentPlayerCount == GameManager.Instance.TurnBaseController.GetAllTeamPlayerCount())
			{
				GameManager.Instance.TurnBaseController.EndTurn();
				GameManager.Instance.TurnBaseController.StartTurn();
			}
		}, duration));
		
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
				SFX.PlayOneShot(boomSFX);
			}
		}
	}

	void RemoveEffectTrigger()
    {
		explosionEffectObject.GetComponent<CircleCollider2D>().enabled = false;
	}
}
