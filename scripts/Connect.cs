using Godot;
using System;

public partial class Connect : Button
{
	private const string url = "http://127.0.0.1";
	private const string port = "8080";
	private const string DialogPopupName = "DialogPopup";
	private const string PathToDialog = "%Connect/{0}";
	
	public override void _Ready()
	{
		Pressed += pressHandler;
		GetNode<HTTPRequest>("%HttpConnect").RequestCompleted += doRequestComplete;
	}
	
	public void pressHandler()
	{
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
		GetNode(String.Format(PathToDialog, DialogPopupName)).QueueFree();
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
		dialog.Size = new Vector2i(300, 50);
		dialog.Position = new Vector2i(640 - 150, 360 - 25);
		dialog.Popup();
	}
}
