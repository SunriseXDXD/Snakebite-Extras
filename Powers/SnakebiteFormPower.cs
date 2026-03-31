using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace SnakebiteExtras.Powers;

public sealed class SnakebiteFormPower : PowerModel
{
    
    
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Single;
    
    public override bool IsInstanced => false;
    
    public override bool AllowNegative => false;
    
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != base.Owner.Player)
        {
            return;
        }
        Flash();
    
        // 先记录原卡牌
        var originalCard = cardPlay.Card;
    
        // 创建新卡牌
        CardModel snakebite = base.CombatState.CreateCard<Snakebite>(base.Owner.Player);
    
        // 执行转换（这会改变 originalCard 的状态）
        await CardCmd.Transform(originalCard, snakebite);
    
        // 关键：使用记录的原卡牌进行 Discard
        // 但需要确保 Transform 完全完成
        await CardCmd.Discard(context, originalCard);
    }
}