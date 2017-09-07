using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCulture : GridPiece {

	protected override void InitValues ()
	{
		age = minCulture;
		gridName = "Cultural Hub";
		pieceType = _pieceType.CULTURE;
		tooltip = "Cultural hub, produces culture.";
		culture = 4;
	}
}
