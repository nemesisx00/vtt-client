using Godot;

public partial class SceneChange : Button
{
	[Export(PropertyHint.ResourceType, "PackedScene")]
	private PackedScene ChangeToScene;
	
	public override void _Ready()
	{
		if(!(ChangeToScene is PackedScene))
			ChangeToScene = GD.Load<PackedScene>("scenes/MainMenu.tscn");
		Pressed += handlePress;
	}
	
	public void handlePress()
	{
		if(ChangeToScene is PackedScene)
			GetTree().ChangeSceneToPacked(ChangeToScene);
	}
}
