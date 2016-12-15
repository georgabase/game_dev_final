using UnityEngine;
using System.Collections;

public class EnemyMovenemt : MonoBehaviour
{
	public LayerMask enemyMask;
	public float speed = 1;
	Rigidbody2D myBody;
	Transform myTrans;
	float myWidth, myHeight;
	public bool facingRight = false;

	void Start ()
	{
		myTrans = this.transform;
		myBody = this.GetComponent<Rigidbody2D>();
		SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
		myWidth = mySprite.bounds.extents.x;
		myHeight = mySprite.bounds.extents.y;
	}

	void FixedUpdate ()
	{
		
		//Use this position to cast the isGrounded/isBlocked lines from
		Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;

		//Check to see if there's ground in front of us before moving forward
		//NOTE: Unity 4.6 and below use "- Vector2.up" instead of "+ Vector2.down"
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
		bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

		//Check to see if there's a wall in front of us before moving forward
		Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f);
		bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .05f, enemyMask);

		//If theres no ground, turn around. Or if I hit a wall, turn around
		if(!isGrounded || isBlocked)
		{
			Vector3 currRot = myTrans.eulerAngles;
			currRot.y += 180;
			myTrans.eulerAngles = currRot;

			// Switch the way the player is labelled as facing.
			facingRight = !facingRight;

			}

		//Always move forward
		Vector2 myVel = myBody.velocity;
		myVel.x = -myTrans.right.x * speed;
		myBody.velocity = myVel;
	}
}