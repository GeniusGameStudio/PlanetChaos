using UnityEngine;
using System.Collections;
using Cinemachine;
using TurnBaseUtil;

// Classe responsável pelo código que controla a nossa bala
public class BulletController : MonoBehaviour {

	private Rigidbody2D rb; 
	// Ref ao Rigidbody2D da nossa bala
	public Transform bulletSpriteTransform;

	public Transform PlayerTransform { get; set; }

	private Vector2 windForce;
	// Ref ao transform do GameObject Sprite que está dentro do GameObject Bullet
	private bool updateAngle = true; 
	// bool que diz se devemos ou não atualizar a rotação do GameObject Sprite baseado na traj. da bala
    // Esse bool serve para dizer que após a bala colidir com algum outro corpo, a rotação 
	//da bala não deve ser mais atualizada baseando-se na trajetória
	public GameObject bulletSmoke;
	// Ref ao gameObject BulletSmoke, que contém o sistema de particula que faz o rastro da bala
	public CircleCollider2D destructionCircle;
	public static GroundController groundController;

	// Use this for initialization
	void Start () {
		GameManager.Instance.vCam.Follow = gameObject.transform;
		rb = GetComponent<Rigidbody2D>();
		//rb.velocity = new Vector2(5f, 10f);
		windForce = new Vector2(Random.Range(-5f, 5f) * 50, 0);
		Debug.Log("winforce:" + windForce);
		rb.AddForce(windForce);
	}
	
	// Update is called once per frame
	void Update () {
		if( updateAngle ){
			Vector2 dir = new Vector2(rb.velocity.x, rb.velocity.y);
			// Determinaçao do angulo do vetor velocidade
            dir.Normalize();			
			float angle = Mathf.Asin (dir.y)*Mathf.Rad2Deg;
			if( dir.x < 0f ){
				angle = 180 - angle;
			}
			
			//Debug.Log("angle = " + angle);

			// Atualizanndo a rotaçao de Sprite ( GameObject que contem o Sprite Render de nossa bala ) 
			//de acordo com o angulo da trajetoria
			bulletSpriteTransform.localEulerAngles = new Vector3(0f, 0f, angle+45f);

		}

		if(transform.position.y < -8f)
        {
			DestroySelf();
		}
	}

	private void DestroySelf()
    {
		Destroy(gameObject);

		//三秒后切换摄像头，为了给特效时间
		GameManager.Instance.baseTimer.SetTimer(3f, () => { GameManager.Instance.TurnBaseController.EndTurn(); GameManager.Instance.TurnBaseController.StartTurn(); });
		
	}

	void OnCollisionEnter2D( Collision2D coll ){
		// Quando a bala colide com outro corpo que nao seja o Player ela 
		//não mais atualiza a rotação baseado na trajetória
        // e o efeito de partícula do rastro da bala é desativado
		if( coll.collider.tag == "Ground" ){
			updateAngle = false;
			bulletSmoke.SetActive(false);
			groundController.DestroyGround( destructionCircle );
			DestroySelf();
		}
	}
}
