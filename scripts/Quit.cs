using Godot;

public partial class Quit : Button
{
	public override void _Ready()
	{
		Pressed += pressHandler;
	}
	
	private void pressHandler()
	{
		GetTree().Root.PropagateNotification((int)NotificationWmCloseRequest);
	}
}
