using Godot;
using System;
using System.Text.RegularExpressions;

public partial class Player : CharacterBody2D
{
	[Export] private TextureProgressBar _healthBar { get; set; }
	[Export] private AnimationPlayer _animationPlayer { get; set; }

    public const int MaxHealth = 5;
    public int Health { get; set; } = 5;

    public const float Speed = 100.0f;
	
    private Vector2I Facing { get; set; } = Vector2I.Left;

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 input = Input.GetVector("left", "right", "up", "down");

        if (input != Vector2.Zero)
		{
			velocity = input.Normalized() * Speed;

			if (input.X == 1)
			{
				Facing = Vector2I.Left;
			}
            else if (input.X == -1)
            {
                Facing = Vector2I.Right;
            }
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

    public override void _Input(InputEvent @event)
    {
		if (@event.IsActionPressed("attack")) 
		{
			if (Facing == Vector2I.Left)
			{
				_animationPlayer.Play("attackLeft");
			}
            if (Facing == Vector2I.Right)
            {
                _animationPlayer.Play("attackRight");
            }
        }
    }

    public void _HitboxEntered(Area2D area)
	{
		Damage(1);
	}

	public void Damage(int amount)
	{
		Health -= amount;

		_animationPlayer.Play("hurt");

		if (Health <= 0) 
		{
			Health = 0;
			UpdateHealth();
			QueueFree();
		}
	}

	public void Heal(int amount) 
	{
		Health = Math.Clamp(Health + amount, 0, MaxHealth);
        UpdateHealth();
    }

	private void UpdateHealth()
	{
        _healthBar.Value = Health;
    }
}
