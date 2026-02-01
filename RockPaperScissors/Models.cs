using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public abstract class Item
    {
        public abstract BattleResult Battle(Item item);
    }
    public class Rock : Item
    {
        public override BattleResult Battle(Item item)
        {
            if(item is Paper)
                return BattleResult.Fail;
            if(item is Scissors)
                return BattleResult.Win;
            return BattleResult.Draw;
        }
        public override string ToString()
        {
            return "rock";
        }
    }
    public class Paper : Item
    {
        public override BattleResult Battle(Item item)
        {
            if (item is Scissors)
                return BattleResult.Fail;
            if (item is Rock)
                return BattleResult.Win;
            return BattleResult.Draw;
        }
        public override string ToString()
        {
            return "paper";
        }
    }
    public class Scissors : Item
    {
        public override BattleResult Battle(Item item)
        {
            if (item is Rock)
                return BattleResult.Fail;
            if (item is Paper)
                return BattleResult.Win;
            return BattleResult.Draw;
        }
        public override string ToString()
        {
            return "scissors";
        }
    }
    public static class ItemGenerator
    {
        public static Item Generate()
        {
            int randow = Random.Shared.Next(3);
            return (randow == 0) ? new Rock() :
                (randow == 1) ? new Paper() : new Scissors();
        }
    }
    public static class CacheManager
    {
        public static string GetImagePathByItem(Item item)
        {
            return $"images/{item}.png";
        }
        public static string GetBattleVersusImagePath_Battle()
        {
            return $"images/battle.png";
        }
        public static string GetBattleVersusImagePath_Draw()
        {
            return $"images/draw.png";
        }
        public static string GetBattleVersusImagePath_Fail()
        {
            return $"images/fail.png";
        }
        public static string GetBattleVersusImagePath_Win()
        {
            return $"images/win.png";
        }
    }

    public enum BattleResult
    {
        Win,
        Draw,
        Fail
    }
}
