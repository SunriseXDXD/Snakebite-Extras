using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using SnakebiteExtras.Powers;

namespace SnakebiteExtras.Cards;

public sealed class SnakebiteDown : CardModel
{   public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain
    ];
    
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Snakebite>()
    ];

    public SnakebiteDown()
        : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<SnakebiteDownPower>(base.Owner.Creature, 2m, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}