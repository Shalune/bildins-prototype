using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {

	public GameObject selectionHighlight;

	GridPiece selection = null;
	GridPiece._pieceType buildType = GridPiece._pieceType.NUMELEMENTS;
	private float timer = 0f;
	private float inputDelay = 0.5f;

	private static Controls monolith;

	public static Controls Instance(){
		if (monolith == null) {
			monolith = new Controls ();
		}

		return monolith;
	}

	void Awake(){
		if (monolith == null) {
			monolith = this;
		} else {
			Destroy (this);
		}
	}

	void Update(){
		if (Input.GetMouseButtonDown(1)){
			Deselect ();
		}
		KeyBoardControls ();
		timer += Time.deltaTime;
	}

	private bool AllowNewInput(){
		if (timer > inputDelay) {
			timer = 0f;
			return true;
		}
		return false;
	}

	public void Clicked(ref GameObject place){
		if (!Buildable()) {
			Selection (place.GetComponent<GridPiece>());
		} else if (AllowNewInput()) {
			BuildAt (ref place);
		}
	}

	private void Selection(GridPiece place){
		selection = place;
		FourxUI.Instance ().UpdateUI (place.GetTooltip ());
		selectionHighlight.SetActive (true);
		selectionHighlight.transform.position = (Vector3)selection.gameObject.transform.position + Vector3.back;
	}

	private void BuildAt(ref GameObject place){
		Map.Instance ().ReplacePieceWith (ref place, buildType);
		GameManager.Instance().EndTurn();
	}

	private void KeyBoardControls(){
		if (Input.GetButton ("Submit") && AllowNewInput()) {
			GameManager.Instance ().EndTurn ();
		}

		if (Input.GetButton ("Commercial")) {
			buildType = GridPiece._pieceType.COMMERCE;
		}

		if (Input.GetButton ("Industrial")) {
			buildType = GridPiece._pieceType.INDUSTRY;
		}

		// deselect build
		if (Input.GetButton ("Cancel")) {
			buildType = GridPiece._pieceType.NUMELEMENTS;
		}
	}

	private void Deselect(){
		selection = null;
		selectionHighlight.SetActive (false);
		FourxUI.Instance ().ClearTooltip ();
	}

	private bool Buildable(){
		if (buildType == GridPiece._pieceType.NUMELEMENTS) {
			return false;
		}
		return true;
	}
}
