using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private bool endTurn = false;
	private static GameManager monolith;

	private int population;
	private int jobs;
	private int materials;
	private int products;
	private int merchandise;
	private int culture;
	private float happiness;
	private float wealth;

	private const float wealthPerProduct = 3f;
	private const float wealthPerMerchandise = 1f;

	public static GameManager Instance(){
		if (monolith == null) {
			monolith = new GameManager ();
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



	public void EndTurn(){
		Map.Instance ().EndTurn (ref population, ref jobs, ref materials, ref products, ref merchandise, ref culture);
		CalculateWealth ();
		CalculateHappiness ();
		wealth *= happiness;
		FourxUI.Instance ().UpdateUI (happiness, wealth);
	}

	private void CalculateWealth(){
		products =  (int)Mathf.Min(materials*PieceLibrary.productPerMaterial, products);
		merchandise = (int)Mathf.Min (products*PieceLibrary.merchandisePerProduct, merchandise);

		//expending products used to create merchandise
		products -= (int)Mathf.Floor(merchandise / PieceLibrary.merchandisePerProduct);

		wealth = (products * wealthPerProduct) + (merchandise * wealthPerMerchandise);

		Debug.Log ("materials = " + materials + "    products = " + products + "    merchandise = " + merchandise + "    wealth = " + wealth);
	}

	private void CalculateHappiness(){
		
		float subsistence = population;
		if (jobs < population)
			subsistence -= population - jobs;

		float comfort = 0f;
		comfort += Mathf.Clamp (merchandise, 0f, population);

		float fulfilment = culture;
		if (subsistence < population)
			fulfilment /= 10f;
		else
			fulfilment /= 2f;

		happiness = ((comfort + subsistence) / 4f) + fulfilment;
		if (population > 0)
			happiness /= population;
		else
			happiness = 0;

		if (happiness > 1)
			happiness = 1;

		Debug.Log ("c = " + comfort + "   s = " + subsistence + "   h = " + happiness);
	}

	/*
	public void EndTurn(){
		endTurn = true;
	}
	*/
}
