﻿using MonoGamePlus.Resources;
using MonoGamePlus.Systems;

namespace MonoGamePlus.Components;
/// <summary>
/// Describes visual appearance of entity. Entities with this components are rendered via
/// <see cref="RenderSystem"/>.
/// </summary>
public struct Appearance
{
    public Sprite[] Sprites;

    public Appearance(Sprite sprite)
    {
        Sprites = new Sprite[] { sprite };
    }
}
