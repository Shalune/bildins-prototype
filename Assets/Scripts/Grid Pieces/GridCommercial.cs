using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCommercial : GridPiece {

	protected override void InitValues ()
	{
		age = 0;
		gridName = "Commercial";
		pieceType = _pieceType.COMMERCE;
		tooltip = "Commercial: Consumes products and provides merchandise.";
		merchandise = 2;
		//jobs = 1;
		//products = -1;
	}
}
