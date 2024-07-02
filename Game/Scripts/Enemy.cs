using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
    [Export] private AnimationPlayer _animationPlayer { get; set; }
    [Export] private Area2D _detectorBox { get; set; }

    public const int MaxHealth = 3;
    public int Health { get; set; } = 3;

    public override void _PhysicsProcess(double delta)
    { 
        
    }

        public void _HitboxEntered(Area2D area)
    {
        Damage(1);
    }

    public void Damage(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Health = 0;
            _animationPlayer.Play("death");
        }
        else
        {
            _animationPlayer.Play("hurt");
        }
    }

    public void Heal(int amount)
    {
        Health = Math.Clamp(Health + amount, 0, MaxHealth);
    }
}
