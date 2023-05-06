﻿using Arch.Core;
using Arch.Core.Extensions;

using Microsoft.Xna.Framework;

using MonoGamePlus;
using MonoGamePlus.Components;
using MonoGamePlus.Resources;
using MonoGamePlus.Systems;
using MonoGamePlus.UI;
using MonoGamePlus.UI.Elements;

using TheImposter.Systems;

namespace TheImposter.GameStates.Level;
internal class LevelState : GameState
{
    private const float maxImposterDistance = 200.0f;
    private const float playerBaseSpeed = 500.0f;
    private const int npcCount = 100;
    private const float time = 2.0f * 60.0f;

    private readonly Color clearColor = new(70, 70, 70);

    private Entity imposter;
    private EntityControlSystem controlSystem;
    // TODO: remove FPS counter
    private FPSCounterSystem fpsCounterSystem;

    private Graph graph;

    public Entity Player { get; private set; }
    public Statistics Statistics { get; private set; } = new();
    public PlayerUpgrades Upgrades { get; private set; } = new();
    public int Stage { get; private set; } = 1;

    public LevelState() { }

    public LevelState(Statistics statistics, PlayerUpgrades playerUpgrades, int stage)
    {
        Statistics = statistics;
        Upgrades = playerUpgrades;
        Stage = stage;
    }

    public void TryFindImposter()
    {
        Vector2 playerPosition = Player.Get<Transform>().Position;
        Vector2 imposterPosition = imposter.Get<Transform>().Position;
        float distance = Vector2.Distance(playerPosition, imposterPosition);

        if (distance <= maxImposterDistance)
        {
            Statistics.ImpostersFound++;
            Statistics.CompletedStages++;

            Game.RemoveGameState(this);
            Game.AddGameState(new StageWinState(Statistics, Upgrades, Stage));
        }
        else
        {
            Game.RemoveGameState(this);
            Game.AddGameState(new GameOverState(Statistics, "THAT  WAS  NOT  THE  IMPOSTER!"));
        }
    }

    protected override void Initialize()
    {
        CreateEntities();
        CreateSystems();
        CreateUI();

        Game.ClearColor = clearColor;
        Camera.Target = Player;

        base.Initialize();
    }

    private void CreateSystems()
    {
        controlSystem = new EntityControlSystem(Player)
        {
            Speed = playerBaseSpeed + Upgrades.MoveSpeed,
        };
        fpsCounterSystem = new FPSCounterSystem();

        AddUpdateSystem(new TimePlayedTrackingSystem(this));
        AddUpdateSystem(new CameraZoomControlSystem());
        AddUpdateSystem(controlSystem);
        AddUpdateSystem(new RandomGraphWalkSystem(graph));
        AddUpdateSystem(new MovementSystem());
        AddUpdateSystem(new CollisionSystem());
        AddUpdateSystem(new PlayerControlSystem(this));

        AddUpdateSystem(fpsCounterSystem);

        AddUpdateSystem(new AnimationOrientationSystem());
        AddUpdateSystem(new AnimationSystem());
        AddRenderSystem(new RenderSystem());
    }

    private void CreateEntities()
    {
        LevelFactory factory = new(this);
        WorldGenerator generator = new(factory, this);

        generator.Generate(npcCount);

        graph = generator.Graph;
        imposter = generator.Imposter;

        Player = factory.CreatePlayer(generator.Spawn);
    }

    private void CreateUI()
    {
        Timer timer = new()
        {
            Label = new Label(new SpriteText(Game.Fonts["Curse of the Zombie;46"], "", Color.White)),
            Offset = new Vector2(Game.Resolution.X / 2.0f, 60.0f),
            Time = time,
        };
        timer.OnFinish += (sender, e) =>
        {
            Game.RemoveGameState(this);
            Game.AddGameState(new GameOverState(Statistics, "TIME  IS  UP!"));
        };
        UILayer.AddElement(timer);
    }
}
