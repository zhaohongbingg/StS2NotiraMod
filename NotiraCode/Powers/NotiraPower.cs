using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Powers;
using Notira.Notira.Extensions;


namespace Notira.Notira.Powers;

public abstract class NotiraPower : CustomPowerModel
{
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            // 1. 优先使用专用大图标
            var bigPath = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
            if (ResourceLoader.Exists(bigPath))
                return bigPath;

            // 2. 如果没有大图标，则尝试使用小图标（直接调用小图标路径）
            var smallPath = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            if (ResourceLoader.Exists(smallPath))
                return smallPath;

            // 3. 最后回退到默认大图标
            return "power.png".BigPowerImagePath();
        }
    }

}