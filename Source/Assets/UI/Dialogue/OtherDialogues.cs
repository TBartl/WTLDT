using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OtherDialogues : MonoBehaviour {

	public static OtherDialogues OD;

	[TextArea(3, 10)]
	public string[] alternative_dialogues;

	// Use this for initialization
	void Start () {
		OD = this;
	}
		


	public void ChangeDialogue () {
		int index = Random.Range (0, alternative_dialogues.Length);
		GameObject dialogueGO = GameObject.FindGameObjectWithTag ("DialogueBox");
		dialogueGO.GetComponent<Text>().text = alternative_dialogues [index];
	}
}
