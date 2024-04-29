using Godot;
using System;
using System.Threading;

public partial class DialogManager : Node
{
	private NodePath DialogBoxScenePath = "res://prefabs/DialogBox.tscn";
	[Export]
	public PackedScene DialogBoxScene = (PackedScene)ResourceLoader.Load("res://prefabs/DialogBox.tscn"); //{ get; private set; }
	public string[] MessageLines = new string[] { };
	private int CurrentLine = 0;
	public DialogBox DialogBox;
	private Vector2 DialogBoxPosition = Vector2.Zero;
	public bool isMessageActive = false;
	private bool canAdvanceMessage = false;
	// public override void _Ready()
	// {
	// 	 DialogBoxScene = (PackedScene)ResourceLoader.Load(DialogBoxScenePath);
	// }

	public void StartMessage(Vector2 position, string[] lines)
	{
		if (this.isMessageActive) return;
		this.MessageLines = lines;
		this.DialogBoxPosition = position;
		ShowText();
		this.isMessageActive = true;
	}

	public void ShowText()
	{
		DialogBox = (DialogBox)this.DialogBoxScene.Instantiate();
		_ = DialogBox.TextDisplayFinishedEventHandler.Combine(OnAllTextDisplayed);
		if (GetTree() != null)
		{
			GetTree().Root.AddChild(DialogBox);
			DialogBox.GlobalPosition = DialogBoxPosition;
			DialogBox.DisplayText(MessageLines[CurrentLine]);
			canAdvanceMessage = false;
		}
	}


	/*
	public void ShowText()
	{
		DialogBox = (DialogBox)this.DialogBoxScene.Instantiate();
		_ = DialogBox.TextDisplayFinishedEventHandler.Combine(OnAllTextDisplayed);
		GetTree().Root.AddChild(DialogBox);
		DialogBox.GlobalPosition = DialogBoxPosition;
		DialogBox.DisplayText(MessageLines[CurrentLine]);
		canAdvanceMessage = false;
	}
	*/
	/*
	public void ShowText()
	{
		DialogBox = (DialogBox)this.DialogBoxScene.Instantiate();
		_ = DialogBox.TextDisplayFinishedEventHandler.Combine(OnAllTextDisplayed);
		if (GetTree() != null)
		{
			GetTree().Root.AddChild(DialogBox);
			DialogBox.GlobalPosition = DialogBoxPosition;
			DialogBox.DisplayText(MessageLines[CurrentLine]);
			canAdvanceMessage = false;
		}
	}
	*/

	public void OnAllTextDisplayed()
	{
		canAdvanceMessage = true;
	}

	public void UnhandledInput(InputEvent inputEvent)
	{
		if (inputEvent.IsActionPressed("advance message") && isMessageActive && canAdvanceMessage)
		{
			DialogBox.QueueFree();
			CurrentLine++;
			if (CurrentLine >= MessageLines.Length)
			{
				isMessageActive = false;
				CurrentLine = 0;
				return;
			}
			ShowText();
		}
	}
}
