using Godot;
using System;

public partial class Connect : Button
{
	private const string ConnectionPanelPath = "%ConnectionPanel";
	
	public override void _Ready()
	{
		Pressed += pressHandler;
	}
	
	public void pressHandler()
	{
		var node = GetNode<Popup>(ConnectionPanelPath);
		node.PopupCentered();
		node.Size = new Vector2i(250, 110);
	}
}
