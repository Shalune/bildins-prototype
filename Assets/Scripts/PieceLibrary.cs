using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceLibrary : MonoBehaviour {

	public const float productPerMaterial = 1f;
	public const float merchandisePerProduct = 2f;

	private static string prefix = "prefabs/";
	private static string commercial = "Commercial";
	private static string culture = "Culture";
	private static string empty = "OpenGround";
	private static string industrial = "Industrial";
	private static string residential = "Residential";
	private static string resource = "Forest";
	private static string water = "Water";

	public static bool GridPieceOfType(GridPiece._pieceType pieceType, ref GameObject gridPiece){

		string suffix;

		switch (pieceType) {
		case GridPiece._pieceType.COMMERCE:
			suffix = commercial;
			break;
		case GridPiece._pieceType.CULTURE:
			suffix = culture;
			break;
		case GridPiece._pieceType.EMPTY:
			suffix = empty;
			break;
		case GridPiece._pieceType.INDUSTRY:
			suffix = industrial;
			break;
		case GridPiece._pieceType.RESIDENCE:
			suffix = residential;
			break;
		case GridPiece._pieceType.RESOURCE:
			suffix = resource;
			break;
		case GridPiece._pieceType.WATER:
			suffix = water;
			break;
		default:
			suffix = "none";
			break;
		}

		if (suffix != "none") {
			gridPiece = Resources.Load (prefix + suffix) as GameObject;
			return true;
		}

		return false;
	}
}
