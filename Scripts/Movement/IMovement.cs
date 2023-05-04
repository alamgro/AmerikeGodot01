public interface IMovement
{
	float MoveSpeed { get; set; }
	float JumpForce { get; set; }
	void TranslateWithArrows(float deltaTime);
	void Jump();
}
