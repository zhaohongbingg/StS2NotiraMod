/*using Godot;
using System;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using Notira.Notira.Powers;

public partial class NHistory : Control
{
    public partial class CardView : Control
    {
        private CardModel _card;
        private InfoModPanel _recentPanel;
        private bool _mouseInside;

        public void Setup(CardModel card)
        {
            _card = card;
            // 原有其他初始化...
        }

        public override void _Ready()
        {
            MouseEntered += OnMouseEntered;
            MouseExited += OnMouseExited;
            SetProcess(true);
        }

        private void OnMouseEntered()
        {
            _mouseInside = true;
            ShowRecentCardsPanel();
        }

        private void OnMouseExited()
        {
            _mouseInside = false;
            HideRecentCardsPanel();
        }

        public override void _Process(double delta)
        {
            if (_mouseInside && _recentPanel != null)
            {
                // 让面板跟随鼠标移动（偏移 15,15）
                _recentPanel.GlobalPosition = GetGlobalMousePosition() + new Vector2(15, 15);
            }
        }

        private void ShowRecentCardsPanel()
        {
            if (_recentPanel != null) return;

            var recentCards = RecentCardsManager.GetRecentCards();
            if (recentCards.Count == 0) return;

            _recentPanel = InfoModPanel.Create(
                Config.L.Get("recent_cards.title", "最近打出的卡牌"),
                Config.L.Get("recent_cards.subtitle", "")
            );

            foreach (var card in recentCards)
            {
                var row = new HBoxContainer();
                row.AddThemeConstantOverride("separation", 8);

                // 卡牌图标（如果有）
                if (card.Icon != null)
                {
                    var icon = new TextureRect
                    {
                        Texture = card.Icon,
                        CustomMinimumSize = new Vector2(32, 32),
                        ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
                        StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered
                    };
                    row.AddChild(icon);
                }

                // 卡牌名称 + 升级标记
                var label = new Label();
                string name = card.Title?.GetFormattedText() ?? card.Id.ToString();
                if (card.IsUpgraded) name += "+";
                label.Text = name;
                label.AddThemeColorOverride("font_color", new Color("#EFC851"));
                row.AddChild(label);

                _recentPanel.AddCustom(row);
            }

            AddChild(_recentPanel);
            _recentPanel.GlobalPosition = GetGlobalMousePosition() + new Vector2(15, 15);
            _recentPanel.ZIndex = 500;
        }

        private void HideRecentCardsPanel()
        {
            if (_recentPanel != null)
            {
                _recentPanel.QueueFree();
                _recentPanel = null;
            }
        }
    }
    public static class RecentCardsManager
    {
        private static readonly Queue<CardModel> _history = new Queue<CardModel>();
        private const int MaxHistory = 2;   // 只记录最近2张

        public static void RecordCard(CardModel card)
        {
            _history.Enqueue(card);
            while (_history.Count > MaxHistory)
                _history.Dequeue();
        }

        public static IReadOnlyList<CardModel> GetRecentCards()
        {
            return _history.ToList().AsReadOnly();
        }
    }
}
}
*/