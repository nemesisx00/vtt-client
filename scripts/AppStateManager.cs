using Godot;

public partial class AppStateManager : Node
{
	public override void _Ready()
	{
		GetTree().AutoAcceptQuit = false;
	}
	
	public override void _Notification(long what)
	{
		switch(what)
		{
			//Handle any pre-quit functionality here
			case NotificationWmCloseRequest:
				GetTree().Quit();
				break;
			default:
				break;
		}
	}
}
