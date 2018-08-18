
public class Trash : Dropzone {

	public CardManager CM;

	public override void PlaceCard(Card card)
	{
		CM.TrashAndShuffle(card);
	}
}
