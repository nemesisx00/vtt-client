using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public class ResponseData
{
	public int? UserId { get; set; }
	public string Message { get; set; }
}

public partial class DoConnect : Button
{
	private const string ConnectionPanelPath = "%ConnectionPanel";
	private const string ConnectUrlPath = "%ConnectUrl";
	private const string ConnectPortPath = "%ConnectPort";
	private const string DialogPopupName = "DialogPopup";
	private const string PathToDialog = "%DoConnect/{0}";
	
	public override void _Ready()
	{
		Pressed += pressHandler;
		GetNode<HTTPRequest>("%HttpConnect").RequestCompleted += doRequestComplete;
	}
	
	public void pressHandler()
	{
		var appStateManager = GetNode<AppStateManager>(AppStateManager.Path);
		var url = GetNode<TextEdit>(ConnectUrlPath).Text;
		var port = GetNode<TextEdit>(ConnectPortPath).Text;
		var fullurl = String.Format("{0}:{1}{2}", url, port, "/login");
		
		var data = new Dictionary<string, int?> { ["UserId"] = appStateManager.UserId };
		var json = JsonSerializer.Serialize(data);
		
		GetNode<HTTPRequest>("%HttpConnect").Request(
			String.Format("{0}:{1}{2}", url, port, "/login"),
			null,
			true,
			HTTPClient.Method.Post,
			json
		);
	}
	
	public void doRequestComplete(long result, long responseCode, string[] headers, byte[] body)
	{
		if(responseCode == 200)
		{
			ResponseData data = JsonSerializer.Deserialize<ResponseData>(System.Text.Encoding.UTF8.GetString(body));
			GetNode<Popup>(ConnectionPanelPath).Hide();
			if(data.UserId is int)
			{
				GetTree().ChangeSceneToPacked(GD.Load<PackedScene>("scenes/TableTop.tscn"));
			}
			else
				createDialogPopup("Login Failed!", data.Message);
		}
		else
			createDialogPopup("Connection Failed!", "Request timed out.");
	}
	
	public void cleanUpDialog()
	{
		var node = GetNode(String.Format(PathToDialog, DialogPopupName));
		if(node is Node)
			node.QueueFree();
	}
	
	private void createDialogPopup(string title, string text)
	{
		var dialog = new AcceptDialog();
		dialog.Title = title;
		dialog.DialogText = text;
		dialog.GetOkButton().Pressed += cleanUpDialog;
		dialog.UniqueNameInOwner = true;
		dialog.Name = DialogPopupName;
		AddChild(dialog);
		dialog.PopupCentered();
		dialog.Size = new Vector2i(300, 50);
	}
}
