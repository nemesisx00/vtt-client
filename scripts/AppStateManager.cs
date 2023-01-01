using Godot;

public partial class AppStateManager : Node
{
	public const string Path = "/root/AppStateManager";
	
	public int? UserId {get; set; } = null;
	
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
