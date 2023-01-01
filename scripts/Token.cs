using Godot;

public partial class Token : CharacterBody2D
{
	[Signal]
	public delegate void DraggingBeginEventHandler();
	[Signal]
	public delegate void DraggingEndEventHandler();
	
	[Export(PropertyHint.ResourceType, "Texture2D")]
	private Texture2D AvatarTexture;
	[Export(PropertyHint.ResourceType, "Texture2D")]
	private Texture2D BackgroundTexture;
	
	private const string AvatarPath = "%Avatar";
	private const string BackgroundPath = "%Background";
	
	private bool dragging = false;
	private Vector2? destination = null;
	
	public override void _Ready()
	{
		setAvatarTexture(AvatarTexture);
		setBackgroundTexture(BackgroundTexture);
	}
	
	public override void _InputEvent(Viewport vp, InputEvent e, long shapeIndex)
	{
		if(e is InputEventMouseButton iemb)
		{
			if(iemb.ButtonIndex == MouseButton.Left && iemb.Pressed && !dragging)
			{
				dragging = true;
				EmitSignal(SignalName.DraggingBegin);
			}
			
			if(iemb.ButtonIndex == MouseButton.Left && !iemb.Pressed && dragging)
			{
				dragging = false;
				EmitSignal(SignalName.DraggingEnd);
			}
		}
	}
	
	public override void _Process(double delta)
	{
		if(dragging)
			destination = GetGlobalMousePosition();
		else
			destination = null;
		
		if(destination is Vector2)
			Position = (Vector2)destination;
	}
	
	public void setAvatarTexture(Texture2D texture)
	{
		if(texture is Texture2D)
		{
			AvatarTexture = texture;
			GetNode<Sprite2D>(AvatarPath).Texture = AvatarTexture;
		}
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
