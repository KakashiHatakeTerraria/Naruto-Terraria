using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using Terraria.ID;
using Microsoft.Xna.Framework.Audio;

namespace Naruto
{
    public class MyPlayer : ModPlayer
    {
        #region Variables
        //Player vars
        public float ChakraDamage;
        public float ChakraKbAddition;
        public float ChakraSpeedAddition;
        public int ChakraCrit;
        public int ChakraRegenTimer;
        public int ChakraRegen;

        // ChakraMax is now a property that gets reset when it's accessed and less than or equal to zero, to retro fix nasty bugs
        // there's no point changing this value as it only resets itself if it doesn't line up with Ramen Chakra max.

        public int ChakraMax()
        {
            return GetChakraMaxFromRamen();
        }

        // Chakra max 2 is Chakra max from eqByakuganpment and accessories. there's no point changing this value as it gets reset each frame.
        public int ChakraMax2;

        // progression upgrades increase ChakraMax3. This is the only value that can be changed to have an impact on Chakra max and does not reset.
        public int ChakraMax3;

        // Chakra max mult is a multiplier for Chakra that stacks multiplicatively with other ChakraMaxMult bonuses. It resets to 1f each frame.
        public float ChakraMaxMult = 1f;

        // made ChakraCurrent private forcing everyone to use a method that syncs to clients, centralizing Chakra increase/decrease logic.
        private int ChakraCurrent;

        public int ChakraChargeRate = 1;
        public int OverloadMax = 100;
        public int OverloadCurrent;
        public int OverloadTimer;
        public float chargeMoveSpeed;

        //Transformation vars
        public bool IsTransforming;
        public int SSJAuraBeamTimer;
        public bool hasSSJ1;
        public int TransformCooldown;
        public bool ASSJAchieved;
        public bool USSJAchieved;
        public bool SSJ2Achieved;
        public bool SSJ3Achieved;
        public bool LSSJAchieved = false;
        public bool SSJGAchieved = false;
        public int lssj2timer;
        public bool LSSJ2Achieved = false;
        public bool LSSJGAchieved = false;
        public int RageCurrent = 0;
        public int RageDecreaseTimer = 0;
        public int FormUnlockChance;
        public int OverallFormUnlockChance;
        public bool IsOverloading;

        //Input vars
        public static ModHotKey KaiokenKey;
        public static ModHotKey EnergyCharge;
        public static ModHotKey Transform;
        public static ModHotKey PowerDown;
        public static ModHotKey SpeedToggle;
        public static ModHotKey QuickChakra;
        public static ModHotKey TransMenu;
        //public static ModHotKey ProgressionMenuKey;
        public static ModHotKey FlyToggle;
        public static ModHotKey ArmorBonus;

        //mastery vars
        public float MasteryLevel1 = 0;
        public float MasteryMax1 = 1;
        public bool MasteredMessage1 = false;
        public float MasteryLevel2 = 0;
        public float MasteryMax2 = 1;
        public bool MasteredMessage2 = false;
        public float MasteryLevel3 = 0;
        public float MasteryMax3 = 1;
        public bool MasteredMessage3 = false;
        public float MasteryLevelGod = 0;
        public float MasteryMaxGod = 1;
        public bool MasteredMessageGod = false;
        public float MasteryLevelBlue = 0;
        public float MasteryMaxBlue = 1;
        public bool MasteredMessageBlue = false;
        public float MasteryMaxFlight = 1;
        public float MasteryLevelFlight = 0;

        //unsorted vars
        public int drawX;
        public int drawY;
        public bool SSJ1Achieved;
        public bool scouterT2;
        public bool scouterT3;
        public bool scouterT4;
        public bool scouterT5;
        public bool scouterT6;
        public bool Ramen1;
        public bool Ramen2;
        public bool Ramen3;
        public bool Ramen4;
        public bool Ramen5;
        public bool KaioRamen1;
        public bool KaioRamen2;
        public bool KaioRamen3;
        public bool KaioRamen4;
        public bool ChlorophyteHeadPieceActive;
        public bool KaioAchieved;
        public bool ChakraEssence1;
        public bool ChakraEssence2;
        public bool ChakraEssence3;
        public bool turtleShell;
        public bool ChakraEssence4;
        public bool ChakraEssence5;
        public bool DemonBonusActive;
        public bool spiritualEmblem;
        public float KaiokenTimer = 0.0f;
        public bool chakraLantern;
        public float bonusSpeedMultiplier = 1f;
        public float chakraDrainMulti;
        public bool diamondNecklace;
        public bool emeraldNecklace;
        public bool sapphireNecklace;
        public bool topazNecklace;
        public bool amberNecklace;
        public bool amethystNecklace;
        public bool rubyNecklace;
        public bool dragongemNecklace;
        public bool IsCharging;
        // bool used internally to handle managing effects
        public bool WasCharging;
        public int ChargeSoundTimer;
        public KeyValuePair<uint, SoundEffectInstance> ChargeSoundInfo;
        public int ChargeLimitAdd;
        //public static bool RealismMode = false;
        public bool JungleMessage = false;
        public bool HellMessage = false;
        public bool EvilMessage = false;
        public bool MushroomMessage = false;
        public int ChakraOrbDropChance;
        public bool IsHoldingChakraWeapon;
        public bool wornGloves;
        public bool senzuBag;
        public bool palladiumBonus;
        public bool adamantiteBonus;
        public bool traitChecked = false;
        public string playerTrait = null;
        public bool DemonBonus;
        public int OrbGrabRange;
        public int OrbHealAmount;
        public bool IsFlying;

        public int FlightUsageAdd;
        public float FlightSpeedAdd;
        public bool earthenSigil;
        public bool earthenScarab;
        public bool radiantTotem;
        public int ScarabChargeRateAdd;
        public int ScarabChargeTimer;
        public bool flightUnlocked = false;
        public bool flightDampeningUnlocked = false;
        public bool flightUpgraded = false;
        public int DemonBonusTimer;
        public bool hermitBonus;
        public bool spiritCharm;
        public bool zenkaiCharm;
        public bool zenkaiCharmActive;
        public bool majinNucleus;
        public bool baldurEssentia;
        public bool ChakraChip;
        public bool radiantGlider;
        public bool earthenArcanium;
        public bool legendNecklace;
        public bool legendWaistcape;
        public bool armCannon;
        public bool battleChakrat;
        public bool chuninBonus;
        public bool crystalliteControl;
        public bool crystalliteFlow;
        public bool crystalliteAlleviate;
        public float chargeTimerMaxAdd;
        public int ChakraDrainAddition;
        public float KaiokenDrainMulti;
        public bool kaioCrystal;
        public bool luminousSectum;
        public bool infuserAmber;
        public bool infuserAmethyst;
        public bool infuserDiamond;
        public bool infuserEmerald;
        public bool infuserRainbow;
        public bool infuserRuby;
        public bool infuserSapphire;
        public bool infuserTopaz;
        public bool IsDashing;
        public bool CanUseHeavyHit;
        public bool CanUseFlurry;
        public bool CanUseZanzoken;
        public int BlockState;
        public bool blackDiamondShell;
        public bool buldariumSigmite;
        public bool attunementBracers;
        public bool burningEnergyAmulet;
        public bool iceTalisman;
        public bool pureEnergyCirclet;
        public bool timeRing;
        public bool bloodstainedBandana;
        public bool goblinChakraEnhancer;
        public bool mechanicalAmplifier;
        public bool blackFusionBonus;
        public float blackFusionIncrease = 1f;
        public int blackFusionBonusTimer;
        public bool FirstFourStarDBPickup = false;
        public KeyValuePair<uint, SoundEffectInstance> TransformationSoundInfo;

        // helper int tracks which player my local player is playing audio for
        // useful for preventing the mod from playing too many sounds
        public int PlayerIndexWithLocalAudio = -1;
        #endregion

        // overall Chakra max is now just a formula representing your total Chakra, after all bonuses are applied.
        public int OverallChakraMax()
        {
            return (int)Math.Ceiling(ChakraMax() * ChakraMaxMult + ChakraMax2 + ChakraMax3);
        }

        // all changes to Chakra Current are now made through this method.
        public void AddChakra(int chakraAmount)
        {
            SetChakra(ChakraCurrent + chakraAmount);
        }

        public void SetChakra(int ChakraAmount, bool isSync = false)
        {
            // this might seem weird, but remote clients aren't allowed to set eachothers Chakra. This prevents desync issues.
            if (player.whoAmI == Main.myPlayer || isSync)
            {
                ChakraCurrent = ChakraAmount;
            }
        }

        // return the amount of Chakra the player has, readonly
        public int GetChakra()
        {
            return ChakraCurrent;
        }

        public bool IsChakraDepleted()
        {
            return ChakraCurrent <= 0;
        }

        public bool HasChakra(int chakraAmount)
        {
            return ChakraCurrent >= chakraAmount;
        }

        public const int BASE_CHAKRA_MAX = 1000;

        public int GetChakraMaxFromRamen()
        {
            var chakraMaxValue = BASE_CHAKRA_MAX;
            chakraMaxValue += (Ramen1 ? 1000 : 0);
            chakraMaxValue += (Ramen2 ? 2000 : 0);
            chakraMaxValue += (Ramen3 ? 2000 : 0);
            chakraMaxValue += (Ramen4 ? 2000 : 0);
            chakraMaxValue += (Ramen5 ? 5000 : 0);
            return chakraMaxValue;
        }

        public static MyPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<MyPlayer>();
        }

        public bool IsPlayerRinnegan()
        {
            return playerTrait != null && playerTrait.Equals("Rinnegan");
        }

        public bool IsPlayerImmobilized()
        {
            return player.frozen || player.stoned || player.HasBuff(BuffID.Cursed);
        }

        // tracks whether a player was flying in the previous frame. Allows Flight System to apply Katchin Feet correctly
        private bool WasFlying = false;

        public override void PostUpdate()
        {
            if (LSSJAchieved && !LSSJ2Achieved && player.whoAmI == Main.myPlayer && IsPlayerRinnegan() && NPC.downedFishron && player.statLife <= (player.statLifeMax2 * 0.10))
            {
                lssj2timer++;
                if (lssj2timer >= 300)
                {
                    if (Main.rand.Next(8) == 0)
                    {
                        Main.NewText("Something uncontrollable is coming from deep inside.", Color.Green);
                        player.statLife = player.statLifeMax2 / 2;
                        player.HealEffect(player.statLifeMax2 / 2);
                        LSSJ2Achieved = true;
                        IsTransforming = true;
                        lssj2timer = 0;
                    }
                }
                if (chakraLantern)
                {
                    player.AddBuff(mod.BuffType("ChakraLanternBuff"), 2);
                }
                else
                {
                    player.ClearBuff(mod.BuffType("ChakraLanternBuff"));
                }
            }
        }
    }
}