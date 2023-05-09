using Godot;

public partial class Movement : CharacterBody2D, IMovement
{
	[Export]
	public float MoveSpeed { get; set; } = 100f;
	[Export]
	public float JumpForce { get; set; } = -400f;
	[Export]
	private AnimationTree _animationTree;
	[Export]
	private Sprite2D _sprite;

	private bool _isIdling = false;
	private bool _isRunning = false;
	private Vector2 _velocity;
	private const string ANIMATION_PARAMS_ROOT = "parameters/conditions/";
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public enum AnimationParams
	{
		Idle,
		Run,
	}

    // Start
    public override void _Ready()
	{
		base._Ready();	
	}
	// Update
	public override void _Process(double delta)
	{
		base._Process(delta);
        _isRunning = Mathf.Abs(Axis.X) > 0f;
        _isIdling = !_isRunning;
		_animationTree.Set(ANIMATION_PARAMS_ROOT + AnimationParams.Idle, _isIdling);
		_animationTree.Set(ANIMATION_PARAMS_ROOT + AnimationParams.Run, _isRunning);
		_sprite.FlipH = Axis.X < 0f || (Axis.X <= 0f && _sprite.FlipH);
    }

	// FixedUpdate
	public override void _PhysicsProcess(double delta) 
	{
		base._PhysicsProcess(delta);
        //TranslateHorizontally((float)delta);
		_velocity = Velocity;
        _velocity.Y += _gravity * (float)delta;
        TranslateHorizontally((float)delta);
		Velocity = _velocity;
		MoveAndSlide();
    }

    public void TranslateHorizontally(float deltaTime) => _velocity.X = Axis.X * MoveSpeed;
    private Vector2 Axis => new Vector2(Input.GetAxis("Left", "Right"), Input.GetAxis("Up", "Down"));

	public void Jump()
	{

	}
}
