using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridForest : GridPiece {

	protected override void InitValues ()
	{
		age = 0;
		gridName = "Forest";
		pieceType = _pieceType.RESOURCE;
		tooltip = "Forest, provides access to raw materials.";
		materials = 1;
	}
}
