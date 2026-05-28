using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace SnakebiteExtras.Cards;

public class SnakebiteCry : CardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<Snakebite>()];

    protected override IEnumerable<DynamicVar> CanonicalVars => 
    [
        new PowerVar<PoisonPower>(33m),
        new EnergyVar(2)
    ];

    public SnakebiteCry()
        : base(6, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<PoisonPower>(base.CombatState.HittableEnemies, base.DynamicVars.Poison.BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Poison.UpgradeValueBy(6m);
    }

    public override Task AfterCardEnteredCombat(CardModel card)
    {
        if (card != this)
        {
            return Task.CompletedTask;
        }
        if (base.IsClone)
        {
            return Task.CompletedTask;
        }
        int num = CombatManager.Instance.History.CardPlaysFinished.Count((CardPlayFinishedEntry e) => e is Snakebite && e.CardPlay.Card.Owner == base.Owner);
        base.EnergyCost.AddThisCombat(-num * base.DynamicVars.Energy.IntValue);
        return Task.CompletedTask;
    }

    public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != base.Owner)
        {
            return Task.CompletedTask;
        }
        if (!(cardPlay.Card is Snakebite))
        {
            return Task.CompletedTask;
        }
        base.EnergyCost.AddThisCombat(-base.DynamicVars.Energy.IntValue);
        return Task.CompletedTask;
    }
}