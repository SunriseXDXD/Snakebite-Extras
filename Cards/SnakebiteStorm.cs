using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace SnakebiteExtras.Cards;

public sealed class SnakebiteStorm : CardModel
{
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain
    ];
    
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Snakebite>()
    ];
    
    public SnakebiteStorm()
        : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        IEnumerable<CardModel> enumerable = PileType.Hand.GetPile(base.Owner).Cards.ToList();
        int handSize = enumerable.Count();
        await CardCmd.Discard(choiceContext, enumerable);
        await Cmd.CustomScaledWait(0f, 0.25f);
        
        List<CardModel> snakebites = new List<CardModel>();
        for (int index = 0; index < handSize; ++index)
        {
            CardModel item = base.CombatState.CreateCard<Snakebite>(base.Owner);
            snakebites.Add(item);
        }
        await CardPileCmd.AddGeneratedCardsToCombat(snakebites, PileType.Hand, true);
            
        if (!base.IsUpgraded)
        {
            return;
        }
        foreach (CardModel item in snakebites)
        {
            CardCmd.Upgrade(item);
        }
    }
}