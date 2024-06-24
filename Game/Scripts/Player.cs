using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		//For some reason, you cannot move down-left or down-right with specifically WSAD
		//I've put inquires into this, but maybe its just my keyboard.
		//At the very least, I know its with the input not the code. Check, I also mapped with arrow keys.
		Vector2 input =  Input.GetVector("left", "right", "up", "down");

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
