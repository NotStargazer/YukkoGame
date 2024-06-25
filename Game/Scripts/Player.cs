using Godot;
using System;
using System.Text.RegularExpressions;

public partial class Player : CharacterBody2D
{
	[Export] private TextureProgressBar _healthBar { get; set; }

    public const float Speed = 100.0f;
	public const int MaxHealth = 20;
	public int Health { get; set; } = 20;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 input = Input.GetVector("left", "right", "up", "down");

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

	public void Damage(int amount)
	{
		Health -= amount;

		if (Health <= 0) 
		{
			QueueFree();
		}
	}

	private void UpdateHealth()
	{
        _healthBar.Value = Health;
    }
}
