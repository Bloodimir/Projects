﻿using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;

namespace Kennen
{
    internal class LastHitA
    {
        public enum AttackSpell
        {
            Q,
            W
        };

        public static AIHeroClient Kennen
        {
            get { return ObjectManager.Player; }
        }

        public static Obj_AI_Base GetEnemy(float range, GameObjectType t)
        {
            switch (t)
            {
                case GameObjectType.AIHeroClient:
                    return EntityManager.Heroes.Enemies.OrderBy(a => a.Health).FirstOrDefault(
                        a => a.Distance(Player.Instance) < range && !a.IsDead && !a.IsInvulnerable);
                default:
                    return EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(a => a.Health).FirstOrDefault(
                        a => a.Distance(Player.Instance) < range && !a.IsDead && !a.IsInvulnerable);
            }
        }
        public static Obj_AI_Base MinionLh(GameObjectType type, AttackSpell spell)
        {
            return EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(a => a.Health).FirstOrDefault(a => a.IsEnemy
                                                                                               && a.Type == type
                                                                                               &&
                                                                                               a.Distance(Kennen) <=
                                                                                               Program.Q.Range
                                                                                               && !a.IsDead
                                                                                               && !a.IsInvulnerable
                                                                                               &&
                                                                                               a.IsValidTarget(
                                                                                                   Program.Q.Range)
                                                                                               &&
                                                                                               a.Health <= Misc.Qcalc(a));
        }

        public static void LastHitB()
        {
            var qcheck = Program.LastHit["LHQ"].Cast<CheckBox>().CurrentValue;
            var qready = Program.Q.IsReady();
            var wcheck = Program.LastHit["LHW"].Cast<CheckBox>().CurrentValue;
            var wready = Program.W.IsReady();

            if (qcheck && qready)
            {
            var minion = (Obj_AI_Minion) MinionLh(GameObjectType.obj_AI_Minion, AttackSpell.Q);
            if (minion != null)
            {
                if (Program.Q.MinimumHitChance >= HitChance.Low)
                {
                    Program.Q.Cast(minion.ServerPosition);
                }
                
                if (wcheck && wready)
                {
                var wminion = (Obj_AI_Minion) MinionLh(GameObjectType.obj_AI_Minion, AttackSpell.W);
                if (wminion != null)
                {
                    if (wminion.HasBuff("kennenmarkofstorm"))
                    {
                        Program.W.Cast();
                    }
                }
            }
        }
    }
    }
}}