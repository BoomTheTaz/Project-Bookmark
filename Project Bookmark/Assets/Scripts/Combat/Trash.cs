
public class Trash : Dropzone {

	public Deck PlayerDeck;

	public override void PlaceCard(Card card)
	{
		PlayerDeck.TrashAndShuffle(card);
	}
}
