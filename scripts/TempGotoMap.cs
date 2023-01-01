using Godot;
using System;

// This whole button is temporary and will be replaced by a more robust server connection/scene changing process.
public partial class TempGotoMap : Button
{
	public override void _Ready()
	{
		Pressed += pressHandler;
	}
	
	public void pressHandler()
	{
		GetTree().ChangeSceneToFile("scenes/TableTop.tscn");
	}
}
