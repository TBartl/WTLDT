using UnityEngine;
using System.Collections;

public class IntroSequence : MonoBehaviour {
	void Start()
	{
		StartCoroutine(RunSequence());
	}

	public GameObject lookSimbaText;
	public GameObject canYouControl;
	public GameObject butShadows;
	public GameObject haresay;
	public GameObject longLive;
	public GameObject letThisBeALesson;
	public GameObject illKillEveryLion;
	public GameObject evenYouSimba;

	IEnumerator RunSequence ()
	{
		Game_Manager.S.levelEnding = true;
		while (PlayerMovement.S == null)
			yield return null;

		PlayerMovement.S.gameObject.GetComponent<MeshRenderer>().enabled = false;
		yield return null;
		PlayerMovement.S.gameObject.GetComponent<MeshRenderer>().enabled = false;


		ControlledMovement bunnyMovement = GameObject.Find("Player Intro 2(Clone)").GetComponent<ControlledMovement>();
		ControlledMovement kingMovement = GameObject.Find("KingIntro(Clone)").GetComponent<ControlledMovement>();
		ControlledMovement simbaMovement = GameObject.Find("SimbaIntro(Clone)").GetComponent<ControlledMovement>();

		yield return new WaitForSeconds(2.5f);
		yield return null;

		lookSimbaText.SetActive(false);
		butShadows.SetActive(true);
		yield return new WaitForSeconds(2.5f);

		butShadows.SetActive(false);
		kingMovement.RotateActor(180);
		simbaMovement.RotateActor(180);
		bunnyMovement.MoveActor(new IntVector2(6, 2));
		canYouControl.SetActive(true);
		yield return new WaitForSeconds(2f);

		canYouControl.SetActive(false);
		bunnyMovement.MoveActor(new IntVector2(6, 1));
		haresay.SetActive(true);
		yield return new WaitForSeconds(1.5f);

		haresay.SetActive(false);
		longLive.SetActive(true);
		yield return new WaitForSeconds(1.5f);


		bunnyMovement.MoveActor(new IntVector2(6, 0));
		simbaMovement.RotateActor(270);
		longLive.SetActive(false);		
		yield return new WaitForSeconds(1f);

		simbaMovement.MoveActor(new IntVector2(8, 0));
		yield return new WaitForSeconds(.2f);
		simbaMovement.MoveActor(new IntVector2(9, 0));
		yield return new WaitForSeconds(.2f);
		simbaMovement.MoveActor(new IntVector2(10, 0));
		yield return new WaitForSeconds(.2f);
		simbaMovement.MoveActor(new IntVector2(11, 0));
		yield return new WaitForSeconds(.2f);
		simbaMovement.MoveActor(new IntVector2(12, 0));
		yield return new WaitForSeconds(.2f);
		simbaMovement.RotateActor(270);
		yield return new WaitForSeconds(.2f);

		letThisBeALesson.SetActive(true);
		yield return new WaitForSeconds(3.5f);
		letThisBeALesson.SetActive(false);
		yield return new WaitForSeconds(.2f);

		bunnyMovement.RotateActor(90);
		evenYouSimba.SetActive(true);
		yield return new WaitForSeconds(1f);
		evenYouSimba.SetActive(false);

		bunnyMovement.MoveActor(new IntVector2(7, 0));
		yield return new WaitForSeconds(.2f);
		bunnyMovement.MoveActor(new IntVector2(8, 0));
		yield return new WaitForSeconds(.2f);
		bunnyMovement.MoveActor(new IntVector2(9, 0));
		yield return new WaitForSeconds(.2f);
		bunnyMovement.MoveActor(new IntVector2(10, 0));
		yield return new WaitForSeconds(.2f);
		bunnyMovement.MoveActor(new IntVector2(11, 0));
		yield return new WaitForSeconds(.2f);
		bunnyMovement.MoveActor(new IntVector2(12, 0));
		yield return new WaitForSeconds(.2f);

		Game_Manager.S.WinLevel();

	}
}
