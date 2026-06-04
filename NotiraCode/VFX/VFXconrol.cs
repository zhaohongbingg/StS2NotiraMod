using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using Notira.Notira.VFX;



namespace Notira.Notira.VFX;

public class StanceVfxController(StanceVfxConfig cfg)
{
    private const float AmbienceFadeTime = 0.6f;
    private const float AmbienceVolume = -6f;

    private static Color? _originalModulate;
    private static AudioStreamPlayer? _ambiencePlayer;
    private Node2D? _vfxInstance;
    public void PlaySfx()
    {
        if (cfg.EnterSfxPath != null)
            StanceVfx.PlayStanceSfx(cfg.EnterSfxPath);
    }



    // ── SFX ───────────────────────────────────────

    private void PlayEnterSfx()
    {
        if (cfg.EnterSfxPath != null)
            StanceVfx.PlayStanceSfx(cfg.EnterSfxPath);
    }

    private void PlayScreenFlash()
    {
        if (cfg.ScreenFlashColor != null)
            ScreenFlashEffect.Play(cfg.ScreenFlashColor.Value);
    }

    private void PlayScreenShake()
    {
        if (cfg.ScreenShakeStrength != ShakeStrength.None)
            NGame.Instance?.ScreenShake(cfg.ScreenShakeStrength, ShakeDuration.Short);
    }

    // ── Ambience ──────────────────────────────────

    private void StartAmbience()
    {
        if (cfg.AmbienceLoopPath == null) return;

        if (_ambiencePlayer != null && GodotObject.IsInstanceValid(_ambiencePlayer))
            _ambiencePlayer.QueueFree();

        var combatRoom = NCombatRoom.Instance;
        if (combatRoom == null) return;

        _ambiencePlayer = new AudioStreamPlayer
        {
            Stream = PreloadManager.Cache.GetAsset<AudioStream>(cfg.AmbienceLoopPath),
            Bus = "SFX",
            VolumeDb = -80f
        };

        combatRoom.AddChild(_ambiencePlayer);
        _ambiencePlayer.Play();

        _ambiencePlayer.CreateTween()
            .TweenProperty(_ambiencePlayer, "volume_db", AmbienceVolume, AmbienceFadeTime);
    }

    private static void StopAmbience()
    {
        if (_ambiencePlayer == null || !GodotObject.IsInstanceValid(_ambiencePlayer)) return;

        var player = _ambiencePlayer;
        _ambiencePlayer = null;

        var tween = player.CreateTween();
        tween.TweenProperty(player, "volume_db", -80f, AmbienceFadeTime);
        tween.TweenCallback(Callable.From(() =>
        {
            if (GodotObject.IsInstanceValid(player)) player.QueueFree();
        }));
    }
}