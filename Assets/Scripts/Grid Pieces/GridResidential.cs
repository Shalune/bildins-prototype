using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridResidential : GridPiece {

	protected override void InitValues ()
	{
		age = 0;
		gridName = "Residential";
		pieceType = _pieceType.RESIDENCE;
		tooltip = "Housing, raises population.";
		population = 1;
	}
}
