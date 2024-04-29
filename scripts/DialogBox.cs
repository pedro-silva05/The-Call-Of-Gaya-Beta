using Godot;
using System;
using System.Threading.Tasks;

public partial class DialogBox : MarginContainer
{
	private NodePath labelPath = "LabelMargin/Label";
	public Label Label;
	private NodePath letterTimerDisplayPath = "LetterTimerDisplay";
	public Timer LetterTimerDisplay;
	private NodePath labelMarginPath = "LabelMargin";
	public MarginContainer LabelMargin;
	private const int MAX_WIDTH = 256;
	private string text = "";
	private int letterIndex = 0;
	private float letterDisplayTimer = 0.7f;
	private float spaceDisplayTimer = 0.5f;
	private float punctuationDisplayTimer = 0.2f;
	[Signal]
	public delegate void TextDisplayFinishedEventHandler(); //!

	public override void _Ready()
	{
		this.Label = GetNode<Label>(labelPath);
		this.LetterTimerDisplay = GetNode<Timer>(letterTimerDisplayPath);

		this.LabelMargin = GetNode<MarginContainer>(labelMarginPath);
    this.LabelMargin.Connect("resized", new Callable(this, nameof(OnLabelMarginResized)));
	}

	private async void OnLabelMarginResized()
	{
		Vector2 labelMarginSize = new Vector2(Math.Min(this.LabelMargin.Size.X, MAX_WIDTH), this.LabelMargin.Size.Y);
		this.LabelMargin.Set("custom_minimum_size", labelMarginSize);
		// CODIGO ADICIONADO DEPOIS
		if (this.LabelMargin.Size.X > MAX_WIDTH)
		{
			this.Label.AutowrapMode = TextServer.AutowrapMode.Word;
			await Task.Delay(250);
			this.LabelMargin.Set("custom_minimum_size", this.LabelMargin.Size.Y);
			Vector2 labelGlobalPos = new Vector2(this.LabelMargin.Size.X / 2, this.LabelMargin.Size.Y + 24);
			this.Label.Text = "";
		}
	}

	public void DisplayText(String TextToDisplay)
	{
		this.text = TextToDisplay;
		this.Label.Text = TextToDisplay;
	}
	public void DisplayLetter()
	{
		this.Label.Text += text[this.letterIndex++];

		if (this.letterIndex >= this.text.Length)
		{
			return;
		}

		switch(this.text[this.letterIndex])
		{
			case '!':
				LetterTimerDisplay.Start(this.punctuationDisplayTimer); break;
			case '?':
				LetterTimerDisplay.Start(this.punctuationDisplayTimer); break;
			case ',':
				LetterTimerDisplay.Start(this.punctuationDisplayTimer); break;
			case '.':
				LetterTimerDisplay.Start(this.punctuationDisplayTimer); break;
			case ' ':
				LetterTimerDisplay.Start(this.spaceDisplayTimer); break;
			default:
				LetterTimerDisplay.Start(this.letterDisplayTimer); break;
		}
	}

	public void OnLetterTimerDisplayTimeOut()
	{
		DisplayLetter();
	}
}
