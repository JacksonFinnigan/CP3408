using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	public GameObject[] objects;

	// Use this for initialization
	void Start () {
		Idle();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Attack(){
		for (int i = 0; i < objects.Length; i++) {
			Animator animator = objects [i].GetComponent<Animator> ();
			animator.SetTrigger("attack1");
		}
	}

	public void Run(){
		for (int i = 0; i < objects.Length; i++) {
			Animator animator = objects [i].GetComponent<Animator> ();
			animator.SetTrigger("run");
		}
	}

	public void Hurt(){
		for (int i = 0; i < objects.Length; i++) {
			Animator animator = objects [i].GetComponent<Animator> ();
			if (0 == Random.Range (0, 2)) {
				animator.SetTrigger ("hurt1");
			} else {
				animator.SetTrigger ("hurt2");
			}
		}
	}

	public void Dead(){
		for (int i = 0; i < objects.Length; i++) {
			Animator animator = objects [i].GetComponent<Animator> ();
			animator.SetTrigger("dead");
		}
	}

	public void Idle(){
		for (int i = 0; i < objects.Length; i++) {
			Animator animator = objects [i].GetComponent<Animator> ();
			animator.SetTrigger("idle");
		}
	}

}
