using Godot;

public partial class GridCell : Area2D
{
	[Export(PropertyHint.ResourceType, "Texture2D")]
	private Texture2D BackgroundTexture;
	
	private const string BackgroundPath = "%Background";
	
	public bool IsOccupied { get; private set; } = false;
	private Token occupant = null;
	
	public Vector2 Size
	{
		get
		{
			var size = new Vector2(0, 0);
			if(BackgroundTexture is Texture2D)
				size = BackgroundTexture.GetSize() * GetNode<Sprite2D>(BackgroundPath).Scale;
			return size;
		}
	}
	
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
			IsOccupied = true;
		}
	}
	
	public void handleBodyExited(Node2D body)
	{
		if(occupant.Equals(body))
		{
			IsOccupied = false;
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
