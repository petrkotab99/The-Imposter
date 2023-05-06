﻿using Arch.Core.Extensions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MonoGamePlus;
using MonoGamePlus.Components;

using TheImposter.GameStates;

namespace TheImposter.Systems;
internal class PlayerControlSystem : GameSystem
{
    private readonly LevelState levelState;

    private KeyboardState lastKeyboardState;

    public float MinDistance { get; set; } = 100.0f;

    public Keys IdentifyKey { get; set; } = Keys.E;

    public PlayerControlSystem(LevelState levelState)
    {
        this.levelState = levelState;
        lastKeyboardState = Keyboard.GetState();
    }

    protected override void Update(float elapsed)
    {
        KeyboardState current = Keyboard.GetState();

        if (current.IsKeyDown(IdentifyKey) && lastKeyboardState.IsKeyUp(IdentifyKey))
        {
            Vector2 playerPosition = levelState.Player.Get<Transform>().Position;
            Vector2 imposterPosition = levelState.Imposter.Get<Transform>().Position;
            float distance = Vector2.Distance(playerPosition, imposterPosition);

            if (distance <= MinDistance)
            {
                // TODO: stage win
            }
            else
            {
                // TODO: game over
            }
        }

        base.Update(elapsed);
    }
}