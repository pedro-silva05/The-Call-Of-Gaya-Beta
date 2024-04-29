using Godot;
using System;

public partial class Sign : Node2D
{
	private Sprite2D dotsTexture;
	private NodePath dotsTexturePath = "DotsTexture";
	private Area2D signArea;
	private NodePath signAreaPath = "SignArea";
	private DialogManager dialogManager;

	private readonly string[] lines = new string[]
	{
		"Olá, aventureira!",
		"É muito bom vê-la por aqui",
		"Espero que esteja preparada...",
		"Sua jornada está apenas...",
		"...COMEÇANDO!"
	};

	public override void _Ready()
	{
		dotsTexture = GetNode<Sprite2D>(dotsTexturePath);
		signArea = GetNode<Area2D>(signAreaPath);
		dialogManager = new DialogManager();
	}

	public override void _UnhandledInput(InputEvent inputEvent)
	{
		if (signArea.GetOverlappingBodies().Count > 0)
		{
			dotsTexture.Show();
			if (inputEvent.IsActionPressed("interact") && !dialogManager.isMessageActive)
			{
				this.dotsTexture.Hide();
				this.dialogManager.StartMessage(GlobalPosition, lines);
			}
		}
		else
		{
			this.dotsTexture.Hide();
			if(this.dialogManager.DialogBox != null)
			{
				this.dialogManager.DialogBox.QueueFree();
				this.dialogManager.isMessageActive = false;
			}
		}
	}
}
