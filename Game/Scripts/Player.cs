using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 100.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 input = Input.GetVector("left", "right", "up", "down");

		//This will need to be tweaked for sure
		input.Y /= 2;

        if (input != Vector2.Zero)
		{
			velocity = input.Normalized() * Speed;			
		}
		else
		{
			//This could be done better
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
        }

		Velocity = velocity;
		MoveAndSlide();
	}
}
