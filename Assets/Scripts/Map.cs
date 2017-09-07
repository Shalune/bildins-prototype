using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public float gridPieceSize = 1f;
	public float gridYScaleFactor = 0.85f;
	public Vector2 gridUpBoundCorner;
	public Vector2 gridLowBoundCorner;

	private int reduceCultureRadius = 2;
	private int cultureSeparation = 3;

	GridPiece._pieceType[,] gridTypes;
	GameObject[,] grid;
	public GameObject gridEmptyGround;

	private static Map monolith;
	private bool gridPlaced = false;


	public static Map Instance(){
		if (monolith == null) {
			Debug.Log ("why the fuck");
			monolith = new Map ();
		}

		return monolith;
	}

	void Awake(){
		monolith = this;
		int x = (int)Mathf.Floor((gridUpBoundCorner.x - gridLowBoundCorner.x)/gridPieceSize);
		int y = (int)Mathf.Floor((gridUpBoundCorner.y - gridLowBoundCorner.y)/(gridPieceSize*gridYScaleFactor));

		gridTypes = new GridPiece._pieceType[y,x];
		grid = new GameObject [y,x];
		InitGrid ();
	}

	void InitGrid(){
		List<Vector2> cities = new List<Vector2> ();
		List<Vector2> cultures = new List<Vector2> ();
		List<Vector2> lakes = new List<Vector2> ();
		List<Vector2> forests = new List<Vector2> ();

		int numCities = Random.Range (3, 5);
		int numCultures = Random.Range (2, 3);
		int numLakes = Random.Range (1, 3);
		int numForests = Random.Range (6, 9 - numLakes);

		int max = Mathf.Max (Mathf.Max (numCities, numCultures), Mathf.Max (numLakes, numForests));
		for (int i = 0; i < max; i++) {
			if (i < numCities)
				cities.Add (ChooseSpot());
			if (i < numCultures)
				cultures.Add (ChooseSpot());
			if (i < numLakes)
				lakes.Add (ChooseSpot());
			if (i < numForests)
				forests.Add (ChooseSpot());
		}

		foreach (Vector2 v in forests) {
			CreateNature (v, GridPiece._pieceType.RESOURCE);
		}
		foreach (Vector2 v in lakes) {
			CreateNature (v, GridPiece._pieceType.WATER);
		}
		foreach (Vector2 v in cities) {
			CreateCity (v);
		}
		foreach (Vector2 v in cultures) {
			CreateCulture (v);
		}

		for (int i = 0; i < grid.GetLength (0); i++) {
			for (int j = 0; j < grid.GetLength (1); j++) {
				gridTypes [i, j] = gridTypes[i,j];
			}
		}
	}

	private Vector2 ChooseSpot(){
		return new Vector2 (Random.Range (0, grid.GetLength (1)), Random.Range (0, grid.GetLength (0)));
	}

	private void CreateCity(Vector2 v){
		gridTypes [(int)v.y, (int)v.x] = GridPiece._pieceType.RESIDENCE;
	}

	private void CreateCulture(Vector2 v){
		gridTypes [(int)v.y, (int)v.x] = GridPiece._pieceType.CULTURE;
	}

	private void CreateNature(Vector2 v, GridPiece._pieceType type){
		int startY = (int)v.y - 1;
		int endY = (int)v.y + 1;
	
		for (int i = startY; i < endY+1; i++) {
			int startX = (int)v.x - 1;
			int endX = (int)v.x + 1;


			int dist = Mathf.Abs (i - (int)v.y);
			if (dist % 2 == 1 && i % 2 == 0) {
				startX += Mathf.FloorToInt ((float)dist/2f);
				endX -= Mathf.CeilToInt ((float)dist/2f);
			} else {
				startX += Mathf.CeilToInt ((float)dist/2f);
				endX -= Mathf.FloorToInt ((float)dist/2f);
			}

			for (int j = startX; j < endX+1; j++) {
				if(ValidGridSpot(i, j)){
					gridTypes [i, j] = type;
				}
			}
		}
	}


	void Update(){
		if (!gridPlaced) {
			PlaceGrid ();
		}
	}

	private void PlaceGrid(){

		for (int i = 0; i < grid.GetLength (0); i++) {
			for (int j = 0; j < grid.GetLength (1); j++) {

				float x = gridLowBoundCorner.x + (j * gridPieceSize);
				float y = gridLowBoundCorner.y + (i * gridPieceSize * gridYScaleFactor);

				if (i % 2 == 0) {
					x += gridPieceSize * 0.5f;
				}

				PlacePiece(ref grid[i, j], gridTypes[i, j], new Vector3 (x,y,0));
				//grid [i, j].transform.position = new Vector3 (x,y,0);
			}
		}

		gridPlaced = true;
	}

	public void PlacePiece(ref GameObject place, GridPiece._pieceType newType, Vector3 newPos){
		GameObject pieceTemplate = null;
		if (!PieceLibrary.GridPieceOfType (newType, ref pieceTemplate)) {
			Debug.Log ("Attempted to place invalid gridPiece type: " + newType.ToString ());
			return;
		}

		place = GameObject.Instantiate (pieceTemplate, newPos, Quaternion.identity);
	}

	public void ReplacePieceWith(ref GameObject place, GridPiece._pieceType newType){
		GameObject pieceTemplate = null;
		if (!PieceLibrary.GridPieceOfType (newType, ref pieceTemplate)) {
			Debug.Log ("Attempted to replace using invalid gridPiece type: " + newType.ToString ());
			return;
		}

		GridPiece[] currentGrid = place.GetComponents<GridPiece> ();
		foreach (GridPiece gp in currentGrid) {
			Destroy (gp);
		}
		place.GetComponent<Renderer> ().material = pieceTemplate.GetComponent<Renderer> ().sharedMaterial;
		place.AddComponent (pieceTemplate.GetComponent<GridPiece> ().GetType());

		// reduce culture in radius
		if (newType != GridPiece._pieceType.RESIDENCE) {
			Debug.Log ("SETBACK " + pieceTemplate.GetComponent<GridPiece> ().GetPieceType().ToString());
			ReduceCultureGrowth (place);
		}
	}

	private void ReduceCultureGrowth(GameObject place){
		int x = 0, y = 0;
		// find place in grid
		for (int i = 0; i < grid.GetLength (0); i++) {
			for (int j = 0; j < grid.GetLength (1); j++) {
				if (grid [i, j] == place) {
					x = j;
					y = i;
					break;
					break;
				}
			}
		}

		// adjust all nearby culture
		int startY = y - reduceCultureRadius;
		int endY = y + reduceCultureRadius;

		for (int i = startY; i < endY+1; i++) {
			int startX = x - reduceCultureRadius;
			int endX = x + reduceCultureRadius;


			int dist = Mathf.Abs (i - y);
			if (dist % 2 == 1 && i % 2 == 0) {
				startX += Mathf.FloorToInt ((float)dist/2f);
				endX -= Mathf.CeilToInt ((float)dist/2f);
			} else {
				startX += Mathf.CeilToInt ((float)dist/2f);
				endX -= Mathf.FloorToInt ((float)dist/2f);
			}

			for (int j = startX; j < endX+1; j++) {
				if(ValidGridSpot(i, j)){
					GridPiece piece = grid [i, j].GetComponent<GridPiece> ();
					piece.SetBackAge ();
					/*
					 * BOTCHED - can lead to recursive loop replacewith <-> reduceculture
					 * 
					// order important here, must setback age regardless of piece type
					if (piece.SetBackAge () && piece.GetPieceType() == GridPiece._pieceType.CULTURE) {
						ReplacePieceWith(ref grid[i, j], GridPiece._pieceType.RESIDENCE);
					}
					*/
				}
			}
		}
	}

	private int CheckNearbyGrid(GridPiece._pieceType searchFor, int inI, int inJ, int searchSize){
		if (searchSize <= 0) {
			return 0;
		}

		int result = 0;
		int startY = inI - searchSize;
		int endY = inI + searchSize;

		for (int i = startY; i < endY+1; i++) {
			int startX = inJ - searchSize;
			int endX = inJ + searchSize;


			int dist = Mathf.Abs (i - inI);
			if (dist % 2 == 1 && i % 2 == 0) {
				startX += Mathf.FloorToInt ((float)dist/2f);
				endX -= Mathf.CeilToInt ((float)dist/2f);
			} else {
				startX += Mathf.CeilToInt ((float)dist/2f);
				endX -= Mathf.FloorToInt ((float)dist/2f);
			}

			for (int j = startX; j < endX+1; j++) {
				if (i == inI && j == inJ)
					continue;
				if(ValidGridSpot(i, j) && grid[i, j].GetComponent<GridPiece>().GetPieceType() == searchFor){
					result ++;
				}
			}
		}

		return result;
	}

	private bool ValidGridSpot(int i, int j){
		if(i<0 || j <0 ||
			i >= grid.GetLength(0) || j >= grid.GetLength(1)){

			return false;
		}

		return true;
	}

	public bool OutOfBounds(float x, float y){
		if (x < -gridPieceSize / 2f || x > gridUpBoundCorner.x + gridPieceSize / 2f || 
			y < -gridPieceSize / 2f || y > gridUpBoundCorner.y + gridPieceSize / 2f) {
			return true;
		}
		return false;
	}

	public void EndTurn(ref int population, ref int jobs, ref int materials, ref int products, ref int merchandise, ref int culture){
		population = jobs = materials = products = merchandise = culture = 0;

		for (int i = 0; i < grid.GetLength (0); i++) {
			for (int j = 0; j < grid.GetLength (1); j++) {

				GridPiece piece = grid [i, j].GetComponent<GridPiece> ();
				population += piece.GetPopulation ();
				jobs += piece.GetJobs ();
				products += piece.GetProducts ();
				merchandise += piece.GetMerchandise ();
				culture += piece.GetCulture ();

				GridPiece._pieceType pieceType = piece.GetPieceType ();

				// age piece and check culture growth
				if (pieceType == GridPiece._pieceType.RESIDENCE) {
					if(piece.AgePiece () && pieceType != GridPiece._pieceType.CULTURE &&
						CheckNearbyGrid(GridPiece._pieceType.CULTURE, i, j, cultureSeparation) == 0){
						ReplacePieceWith (ref grid [i, j], GridPiece._pieceType.CULTURE);
					}
				}

				// checkrollback culture
				if (pieceType == GridPiece._pieceType.CULTURE && piece.ShouldDeculture ()) {
					ReplacePieceWith (ref grid [i, j], GridPiece._pieceType.RESIDENCE);
				}

				// check if get materials
				if (CheckNearbyGrid (GridPiece._pieceType.INDUSTRY, i, j, 1) > 0) {
					materials += piece.GetMaterials ();
				}
			}
		}

		// create new residential every turn

		int numTries = 0;
		bool found = false;
		Vector2 newPos = Vector2.zero;
		while (numTries < 20 && !found) {
			newPos = ChooseSpot ();
			GridPiece._pieceType pieceType = grid [(int)newPos.y, (int)newPos.x].GetComponent<GridPiece> ().GetPieceType ();
			if (pieceType == GridPiece._pieceType.EMPTY) {
				found = true;
			}
			numTries++;
		}
		if (found) {
		}
			ReplacePieceWith (ref grid [(int)newPos.y, (int)newPos.x], GridPiece._pieceType.RESIDENCE);

	}

	public void JuryRiggedGoBetween(GameObject thingToClick){
		for (int i = 0; i < grid.GetLength (0); i++) {
			for (int j = 0; j < grid.GetLength (1); j++) {
				if (grid [i, j] == thingToClick) {
					Controls.Instance ().Clicked (ref grid [i, j]);
					return;
				}
			}
		}
	}
}
