using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOpenGround : GridPiece {

	protected override void InitValues ()
	{
		age = 0;
		gridName = "Open Ground";
		pieceType = _pieceType.EMPTY;
		tooltip = "Undeveloped land, no terrain features.";
	}
}
