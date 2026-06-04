using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Nodes.Rooms;

public static class SfxHelper
{
    /// <summary>
    /// 播放音效（自动缓存 + fallback）
    /// </summary>
    public static void Play(string path, string bus = "SFX")
    {
        if (string.IsNullOrEmpty(path))
        {
            GD.PrintErr("[SfxHelper] 路径为空");
            return;
        }

        AudioStream stream = null;

        // ── ① 优先走 Preload 缓存 ──
        try
        {
            stream = PreloadManager.Cache.GetAsset<AudioStream>(path);
            if (stream != null)
            {
                GD.Print("[SfxHelper] 使用缓存播放: ", path);
            }
        }
        catch
        {
            // 有些情况下 GetAsset 会直接炸
        }

        // ── ② fallback：直接 GD.Load ──
        if (stream == null)
        {
            stream = GD.Load<AudioStream>(path);
            if (stream != null)
            {
                GD.Print("[SfxHelper] fallback GD.Load 成功: ", path);
            }
        }

        // ── ③ 最终失败 ──
        if (stream == null)
        {
            GD.PrintErr("[SfxHelper] 音频加载失败: ", path);
            return;
        }

        // ── ④ 创建播放器 ──
        var player = new AudioStreamPlayer
        {
            Stream = stream,
            Bus = bus
        };

        // ── ⑤ 挂到战斗场景 ──
        var combatRoom = NCombatRoom.Instance;
        if (combatRoom == null)
        {
            GD.PrintErr("[SfxHelper] 当前不在战斗场景");
            return;
        }

        combatRoom.AddChild(player);

        // ── ⑥ 播放 ──
        player.Play();

        // ── ⑦ 播完自动释放 ──
        player.Finished += () =>
        {
            if (GodotObject.IsInstanceValid(player))
                player.QueueFree();
        };
    }
}