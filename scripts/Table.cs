using Godot;
using System;
using System.Collections.Generic;

public partial class Table : Node2D
{
	private const string NameFormat = "GridCell_{0}_{1}";
	public override void _Ready()
	{
		regenerateGrid();
		GetNode<Button>("%TestButton").Pressed += checkStatus;
	}
	
	public void checkStatus()
	{
		var grid = GetNode<Node2D>("%Grid");
		var cells = new List<Node>(grid.GetChildren());
		foreach(GridCell cell in cells)
		{
			if(cell.IsOccupied)
			{
				GD.Print("Found token in cell ", cell.Name, "!");
			}
		}
	}
	
	private void regenerateGrid()
	{
		var grid = GetNode<Node2D>("%Grid");
		//Clean up any existing children first.
		foreach(var node in grid.GetChildren())
		{
			node.QueueFree();
		}
		
		var gridCell = GD.Load<PackedScene>("res://scenes/GridCell.tscn");
		
		//Calculate necessary values
		var instance = gridCell.Instantiate<GridCell>();
		var cellSize = instance.Size;
		var columns = (int)Math.Floor(1280 / cellSize.x);
		var rows = (int)Math.Floor(720 / cellSize.y);
		instance.QueueFree();
		
		//Generate the grid
		var nextPosition = new Vector2(cellSize.x / 2, 0);
		for(var r = 0; r < rows; r++)
		{
			nextPosition.y += cellSize.y;
			for(var c = 0; c < columns; c++)
			{
				nextPosition.x += cellSize.x;
				if(c == 0)
					nextPosition.x = cellSize.x / 2;
				
				instance = gridCell.Instantiate<GridCell>();
				instance.Position = nextPosition;
				instance.Name = String.Format(NameFormat, c, r);
				grid.AddChild(instance);
			}
		}
	}
}
