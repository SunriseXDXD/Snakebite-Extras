using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;


namespace SnakebiteExtras.Cards;

public sealed class SnakebiteTrap : CardModel
{
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain
    ];

    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Snakebite>()
    ];

    public SnakebiteTrap()
        : base(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        IEnumerable<CardModel> enumerable = PileType.Discard.GetPile(base.Owner).Cards.Where((CardModel c) => c is Snakebite).ToList();
        bool flag = true;
        foreach (CardModel item in enumerable)
        {
            if (base.IsUpgraded)
            {
                CardCmd.Upgrade(item, CardPreviewStyle.None);
            }
            await CardCmd.AutoPlay(choiceContext, item, cardPlay.Target, AutoPlayType.Default, skipXCapture: false, !flag);
            flag = false;
        }
    }
}