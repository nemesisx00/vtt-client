using Godot;

public partial class GridCell : Area2D
{
	[Export(PropertyHint.ResourceType, "Texture2D")]
	private Texture2D BackgroundTexture;
	
	private const string BackgroundPath = "%Background";
	
	private Token occupant = null;
	
	public override void _Ready()
	{
		setBackgroundTexture(BackgroundTexture);
		
		BodyEntered += handleBodyEntered;
		BodyExited += handleBodyExited;
	}
	
	public void handleBodyEntered(Node2D body)
	{
		if(body is Token t)
		{
			occupant = t;
			occupant.DraggingEnd += centerOccupant;
		}
	}
	
	public void handleBodyExited(Node2D body)
	{
		if(occupant.Equals(body))
		{
			occupant.DraggingEnd -= centerOccupant;
			occupant = null;
		}
	}
	
	public void centerOccupant()
	{
		if(occupant is Node2D)
			occupant.Position = Position;
	}
	
	public void setBackgroundTexture(Texture2D texture)
	{
		if(texture is Texture2D)
		{
			BackgroundTexture = texture;
			GetNode<Sprite2D>(BackgroundPath).Texture = BackgroundTexture;
		}
	}
}
