using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;


namespace SnakebiteExtras.Cards;

public sealed class SnakebiteUp : CardModel
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];

    public SnakebiteUp()
        : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllAllies)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner.Creature)
            where c != null && c.IsAlive && c.IsPlayer
            select c;
        foreach (Creature item in enumerable)
        {
            IEnumerable<CardModel> result = await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, item.Player);
            foreach (var card in result)
            {
                await CardCmd.TransformTo<Snakebite>(card);
            }
            
        }
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }
}