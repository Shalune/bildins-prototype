using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridIndustrial : GridPiece {

	protected override void InitValues ()
	{
		age = 0;
		gridName = "Industrial";
		pieceType = _pieceType.INDUSTRY;
		tooltip = "Industrial: Consumes materials and provides products.";
		jobs = 2;
		//materials = -2;
		products = 6;
		culture = -2;
	}
}
