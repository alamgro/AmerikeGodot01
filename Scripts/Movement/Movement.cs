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
	private bool _isJumping = false;
	private bool _isFalling = false;
	private Vector2 _velocity;
	private const string ANIMATION_PARAMS_ROOT = "parameters/conditions/";
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public enum AnimationParams
	{
		Idle,
		Run,
		Jump,
		Falling,
		IsOnFloor,
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
        _isRunning = Mathf.Abs(Axis.X) > 0f && IsOnFloor();
        _isIdling = !_isRunning;
		_animationTree.Set(ANIMATION_PARAMS_ROOT + AnimationParams.Idle, _isIdling);
		_animationTree.Set(ANIMATION_PARAMS_ROOT + AnimationParams.Run, _isRunning);
		_animationTree.Set(ANIMATION_PARAMS_ROOT + AnimationParams.Jump, _isJumping);
		_animationTree.Set(ANIMATION_PARAMS_ROOT + AnimationParams.Falling, _isFalling);
		_animationTree.Set(ANIMATION_PARAMS_ROOT + AnimationParams.IsOnFloor, IsOnFloor());
		_sprite.FlipH = Axis.X < 0f || (Axis.X <= 0f && _sprite.FlipH);
    }

	// FixedUpdate
	public override void _PhysicsProcess(double delta) 
	{
		base._PhysicsProcess(delta);
        //TranslateHorizontally((float)delta);
		_velocity = Velocity;
        _velocity.Y += _gravity * (float)delta;
		_isFalling = _velocity.Y > 0f;
		_isJumping = !IsOnFloor() && !_isFalling;
        TranslateHorizontally((float)delta);
        if (Input.IsActionPressed("Jump") && IsOnFloor())
		{
            _velocity.Y = JumpForce;
            _isIdling = false;
			_isRunning = false;
            _isJumping = true;
        }
		Velocity = _velocity;
		MoveAndSlide();
    }

    public void TranslateHorizontally(float deltaTime) => _velocity.X = Axis.X * MoveSpeed;
    private Vector2 Axis => new Vector2(Input.GetAxis("Left", "Right"), Input.GetAxis("Up", "Down"));

	public void Jump()
	{

	}
}
