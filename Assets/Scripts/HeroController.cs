using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController	: MonoBehaviour {

	public float maxSpeed = 10f;

	// Jumping variables requirement
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 50;

	// crouch var
	bool crouch = false;

	// moving var
	bool facingRight = true;
	Rigidbody2D rb2d;
	Transform tfm;
	Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		tfm = GetComponent<Transform> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// jump function
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("ground", grounded);
		anim.SetFloat ("vSpeed", rb2d.velocity.y);

		// move function
		float move = Input.GetAxis ("Horizontal");	
		anim.SetFloat ("speed", Mathf.Abs (move));
		rb2d.velocity = new Vector2 (move * maxSpeed, rb2d.velocity.y);
		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();		
			
	}

	void Update() {
		if (grounded) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				anim.SetBool ("ground", false);
				anim.SetBool ("crouch", false);
				rb2d.AddForce (new Vector2 (0, jumpForce));
			} else if (Input.GetKeyDown (KeyCode.S)) {
				anim.SetBool ("crouch", true);
			}
		}
	}

	void Flip() {		
		facingRight = !facingRight;
		Vector3 theScale = tfm.localScale;
		theScale.x *= -1;
		tfm.localScale = theScale;
	}
}
