using Godot;
using System;

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
		var url = GetNode<TextEdit>(ConnectUrlPath).Text;
		var port = GetNode<TextEdit>(ConnectPortPath).Text;
		
		GetNode<HTTPRequest>("%HttpConnect").Request(
			String.Format("{0}:{1}", url, port),
			null,
			true,
			HTTPClient.Method.Get,
			""
		);
	}
	
	public void doRequestComplete(long result, long responseCode, string[] headers, byte[] body)
	{
		if(responseCode == 200)
		{
			GD.Print("Success!");
			GetNode<Popup>(ConnectionPanelPath).Hide();
			createDialogPopup("Connection Succeeded!", System.Text.Encoding.UTF8.GetString(body));
		}
		else
		{
			GD.Print("Fail!");
			createDialogPopup("Connection Failed!", "Request timed out.");
		}
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
