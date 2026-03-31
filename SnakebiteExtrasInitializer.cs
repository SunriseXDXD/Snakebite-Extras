using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using SnakebiteExtras.Cards;

namespace SnakebiteExtras
{
    [ModInitializer(nameof(Initialize))]
    public static class SnakebiteExtrasInitializer
    {
        public static void Initialize()
        {
            ModHelper.AddModelToPool(typeof(SilentCardPool), typeof(SnakebiteForm));
            ModHelper.AddModelToPool(typeof(SilentCardPool), typeof(SnakebiteStorm));
            ModHelper.AddModelToPool(typeof(SilentCardPool), typeof(SnakebiteDown));
            ModHelper.AddModelToPool(typeof(SilentCardPool), typeof(SnakebiteUp));
            ModHelper.AddModelToPool(typeof(SilentCardPool), typeof(SnakebiteLanding));
            ModHelper.AddModelToPool(typeof(SilentCardPool), typeof(SnakebiteTrap));
            Log.Info("SnakebiteExtras - 加载成功!");
        }
    }
}