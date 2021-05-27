using Sandbox;
using System;
using System.Linq;

namespace Sportsbar
{
	partial class MinimalPlayer : Player
	{
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			//
			// Use WalkController for movement (you can make your own PlayerController for 100% control)
			//
			Controller = new WalkController();

			//
			// Use StandardPlayerAnimator  (you can make your own PlayerAnimator for 100% control)
			//
			Animator = new StandardPlayerAnimator();

			//
			// Use ThirdPersonCamera (you can make your own Camera for 100% control)
			//
			Camera = new FirstPersonCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			//
			// If we're running serverside and Attack1 was just pressed, spawn a ragdoll
			//
			if ( IsServer && Input.Pressed( InputButton.Attack1 ) )
			{
				var model = new ModelEntity();
				model.SetModel( "models/fleshterry/fleshterry.vmdl" );
				model.Scale = 1f;
				model.Position = EyePos + EyeRot.Forward * 40;
				model.Rotation = Rotation.LookAt( Vector3.Random.Normal );
				model.SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
				model.SetInteractsAs( CollisionLayer.Debris );
				model.SetInteractsWith( CollisionLayer.WORLD_GEOMETRY );
				model.SetInteractsExclude( CollisionLayer.Player );
				model.PhysicsGroup.Velocity = EyeRot.Forward * 1000;
			}
		}
	}
}
