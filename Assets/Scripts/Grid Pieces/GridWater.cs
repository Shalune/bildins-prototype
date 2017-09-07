using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWater : GridPiece {

	protected override void InitValues ()
	{
		age = 0;
		gridName = "Water";
		pieceType = _pieceType.WATER;
		tooltip = "Lake, provides access to raw materials.";
		materials = 1;
	}
}
