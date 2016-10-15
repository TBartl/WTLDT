using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour {
	void Start()
	{
		StartCoroutine(RunSequence());
	}

	public GameObject lookSimbaText;
	public GameObject butShadows;
	public GameObject longLive;
	public GameObject letThisBeALesson;
	public GameObject evenYouSimba;

	IEnumerator RunSequence ()
	{
		Game_Manager.S.levelEnding = true;
		while (PlayerMovement.S == null)
			yield return null;
		PlayerMovement.S.gameObject.GetComponent<MeshRenderer>().enabled = false;
		yield return null;
		PlayerMovement.S.gameObject.GetComponent<MeshRenderer>().enabled = false;
		for (float f = 0; f < 2.5f; f += Time.deltaTime)
		{
			if (Input.GetKeyDown(KeyCode.W))
				break;
			yield return null;
		}
		yield return null;
		Destroy(lookSimbaText);
		butShadows.SetActive(true);
		for (float f = 0; f < 2.5f; f += Time.deltaTime)
		{
			if (Input.GetKeyDown(KeyCode.W))
				break;
			yield return null;
		}

		ControlledMovement bunnyMovement = GameObject.Find("Player Intro 2(Clone)").GetComponent<ControlledMovement>();
		ControlledMovement kingMovement = GameObject.Find("KingIntro(Clone)").GetComponent<ControlledMovement>();
		ControlledMovement simbaMovement = GameObject.Find("SimbaIntro(Clone)").GetComponent<ControlledMovement>();

		Destroy(butShadows);
		kingMovement.RotateActor(0);
		simbaMovement.RotateActor(0);
		bunnyMovement.MoveActor(new IntVector2(6, 2));
		for (float f = 0; f < .8f; f += Time.deltaTime)
			yield return null;
		bunnyMovement.MoveActor(new IntVector2(6, 1));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		longLive.SetActive(true);

		for (float f = 0; f < 2f; f += Time.deltaTime)
			yield return null;
		bunnyMovement.MoveActor(new IntVector2(6, 0));
		simbaMovement.RotateActor(270);
		longLive.SetActive(false);

		for (float f = 0; f < 1f; f += Time.deltaTime)
			yield return null;

		simbaMovement.MoveActor(new IntVector2(8, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		simbaMovement.MoveActor(new IntVector2(9, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		simbaMovement.MoveActor(new IntVector2(10, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		simbaMovement.MoveActor(new IntVector2(11, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		simbaMovement.MoveActor(new IntVector2(12, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		simbaMovement.RotateActor(270);
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;

		letThisBeALesson.SetActive(true);
		for (float f = 0; f < 3.5f; f += Time.deltaTime)
			yield return null;
		letThisBeALesson.SetActive(false);
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;

		bunnyMovement.RotateActor(90);
		evenYouSimba.SetActive(true);
		for (float f = 0; f < 1f; f += Time.deltaTime)
			yield return null;
		evenYouSimba.SetActive(false);

		bunnyMovement.MoveActor(new IntVector2(7, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		bunnyMovement.MoveActor(new IntVector2(8, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		bunnyMovement.MoveActor(new IntVector2(9, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		bunnyMovement.MoveActor(new IntVector2(10, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		bunnyMovement.MoveActor(new IntVector2(11, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;
		bunnyMovement.MoveActor(new IntVector2(12, 0));
		for (float f = 0; f < .2f; f += Time.deltaTime)
			yield return null;

		Game_Manager.S.WinLevel();

	}
}
