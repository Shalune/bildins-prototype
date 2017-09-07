using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridPiece : MonoBehaviour {

	/*		TEMPLATE FOR COPYING TO NEW PIECES

	protected override void InitValues ()
	{
		age = 0;
		gridName = "Commercial";
		pieceType = _pieceType.COMMERCE;
		tooltip = "Commercial: Consumes products and provides merchandise.";
		population = initialPopulation;
		//jobs = 1;
		materials = initialMaterials;
		//products = -1;
		merchandise = initialMerchandise;
		culture = initialCulture;
	}

	to create new gridpiece:
	- create subclass
	- use first 3 lines above, and rest as needed
	- create material and prefab object
	- add to piecelibrary

	 */


	public enum _pieceType {EMPTY, RESOURCE, WATER, TRAVEL, INDUSTRY, COMMERCE, RESIDENCE, CULTURE, GOVERNMENT, NUMELEMENTS};

	protected int age;
	protected int maxAge = 10;
	protected int ageSetback = 3;
	protected int minCulture = 6;

	protected int population;
	protected int jobs;
	protected int materials;
	protected int products;
	protected int merchandise;
	protected int culture;

	protected float cultureFlow = 0f;
	protected float populationChange = 0f;

	protected string gridName;
	protected _pieceType pieceType = _pieceType.NUMELEMENTS;
	protected string tooltip = "NA";

	void Awake(){
		InitValues ();
	}

	protected abstract void InitValues ();

	public _pieceType GetPieceType(){
		return pieceType;
	}

	void OnMouseDown(){
		GameObject pass = gameObject;
		Controls.Instance ().Clicked (ref pass);
	}

	public bool AgePiece(){
		age++;
		if (age > maxAge)
			age = maxAge;
		if (age >= minCulture)
			return true;
		return false;
	}

	public bool SetBackAge(){
		age -= ageSetback;
		if (age < 0)
			age = 0;
		
		if (age < minCulture)
			return true;
		return false;
	}

	// redundant with above - quick fix
	public bool ShouldDeculture(){
		if (age < minCulture)
			return true;
		return false;
	}

	public int GetAge(){ return age; }
	public int GetPopulation(){ return population; }
	public int GetJobs(){ return jobs; }
	public int GetMaterials(){ return materials; }
	public int GetProducts(){ return products; }
	public int GetMerchandise(){ return merchandise; }
	public int GetCulture(){ return culture; }
	public string GetTooltip (){ return tooltip; }
}
