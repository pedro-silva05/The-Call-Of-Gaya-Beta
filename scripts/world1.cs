using Godot;

public partial class world1 : Node2D
{
	public NodePath playerPath = "player";
	public NodePath CatPath = "AishaCat";
	private NodePath cameraPath = "camera";
	public Camera2D camera;
	public PackedScene catScene = (PackedScene)ResourceLoader.Load("res://actors/AishaCat.tscn");
	public PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://actors/Player.tscn");

	public override void _Ready()
	{
		this.camera = GetNode<Camera2D>(cameraPath);

		Checkpoint.load_game();
	}
	public override void _Process(double delta)
	{
		if (Player.isTransformedToCat && !AishaCat.isDeath)
		{
			AishaCat.FollowCamera(camera);
		} 
		if (!Player.isTransformedToCat && !Player.isDeath)
		{
		 	Player.FollowCamera(camera);
		}
	}

	// public void AishaToCat()
	// {
	// 	if (!player.isTransformedToCat)
	// 	{
	// 		player.QueueFree();
	// 		AishaCat.Visible = true;
	// 		AishaCat.Position = player.Position;
	// 		player.isTransformedToCat = !player.isTransformedToCat;
	// 		CharacterBody2D characterInstance = catScene.Instantiate<CharacterBody2D>();
	// 		AddChild(characterInstance);
	// 		characterInstance.Position = player.Position;
	// 	}
	// 	else if (player.isTransformedToCat)
	// 	{
	// 		AishaCat.QueueFree();
	// 		CharacterBody2D playerInstance = playerScene.Instantiate<CharacterBody2D>();
	// 		AddChild(playerInstance);
	// 		playerInstance.Position = AishaCat.Position;

	// 	}
	// }
}
