public interface IMovement
{
	float MoveSpeed { get; set; }
	float JumpForce { get; set; }
	void TranslateHorizontally(float deltaTime);
	void Jump();
}
