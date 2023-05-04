using Godot;

public partial class Movement : CharacterBody2D, IMovement
{
	[Export]
	public float MoveSpeed { get; set; } = 100f;
	[Export]
	public float JumpForce { get; set; } = -400f;
	
	// Start
	public override void _Ready()
	{
		base._Ready();	
	}
	// Update
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	// FixedUpdate
	public override void _PhysicsProcess(double delta) 
	{
		base._PhysicsProcess(delta);
        TranslateWithArrows((float)delta);
	}

	public void TranslateWithArrows(float deltaTime)
	{
		var axis = Axis;
		Velocity = axis * MoveSpeed;
		MoveAndSlide();
	}

	private Vector2 Axis => new Vector2(Input.GetAxis("Left", "Right"), Input.GetAxis("Up", "Down"));

	public void Jump()
	{

	}
}
