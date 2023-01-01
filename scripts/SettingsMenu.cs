using Godot;
using System;

public partial class SettingsMenu : Control
{
	private const string PathUserId = "%UserId";
	private const string PathSave = "%SaveButton";
	
	public override void _Ready()
	{
		var stateManager = GetNode<AppStateManager>(AppStateManager.Path);
		if(stateManager.UserId is int)
			GetNode<TextEdit>(PathUserId).Text = stateManager.UserId.ToString();
		
		GetNode<Button>(PathSave).Pressed += handleClick_Save;
	}
	
	public void handleClick_Save()
	{
		var stateManager = GetNode<AppStateManager>(AppStateManager.Path);
		if(GetNode<TextEdit>(PathUserId) is TextEdit input)
		{
			int id = -1;
			if(!String.IsNullOrEmpty(input.Text) && int.TryParse(input.Text, out id))
				stateManager.UserId = id;
			else
				stateManager.UserId = null;
		}
		
		GetTree().ChangeSceneToFile("scenes/MainMenu.tscn");
	}
}
