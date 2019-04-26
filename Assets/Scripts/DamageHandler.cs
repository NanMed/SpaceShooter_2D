using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {

	public int health = 1;

	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;

	//public Sprite original;
	//public Sprite colored;

	SpriteRenderer spriteRend;
	//SpriteRenderer spriteChange;

	void Start() {
		correctLayer = gameObject.layer;

		// NOTE!  This only get the renderer on the parent object.
		// In other words, it doesn't work for children. I.E. "enemy01"
		spriteRend = GetComponent<SpriteRenderer>();
		//spriteChange = GetComponent<SpriteRenderer>();

		if(spriteRend == null) {
			spriteRend = transform.GetComponentInChildren<SpriteRenderer>();

			if(spriteRend==null) {
				Debug.LogError("Object '"+gameObject.name+"' has no sprite renderer.");
			}
		}
	}

	void OnTriggerEnter2D() {
		health--;

		if(invulnPeriod > 0) {
			invulnTimer = invulnPeriod;
			gameObject.layer = 10;
		}
	}

	void Update() {

		if(invulnTimer > 0) {
			invulnTimer -= Time.deltaTime;

			if(invulnTimer <= 0) {
				gameObject.layer = correctLayer;
				if(spriteRend != null) {
					spriteRend.enabled = true;
				}
			}
			else {
				if(spriteRend != null) {
					spriteRend.enabled = !spriteRend.enabled;
				}
			}
		}

		if(health <= 0) {
			Die();	
		}

		/*if(health <= 0) {
			StartCoroutine(ChangeColor(original));
		} else {
			StartCoroutine(ChangeColor(colored));
		}*/
	}

	void Die() {
		Destroy(gameObject);
	}

	/*public IEnumerator ChangeColor(Sprite sprite){
		spriteRend.sprite = colored;
		yield return new WaitForSeconds(1.0f);
		spriteRend.sprite = original;
	}*/

}
