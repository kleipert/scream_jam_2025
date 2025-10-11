using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public enum PlayerItems
    {
        Bible = 0,
        Crucifix = 1,
        HolyWater = 2,
        Shakle = 3,
        Medkit = 4,
        Senser = 5,
        Towel = 6,
        Chalice = 7,
        Chalk = 8,
        None = 9
    }

    public enum Events
    {
        PlayerPossesed = 0,
        DemonThrowsItems = 1,
        PhoneRings = 2,
        UnholyFog = 3,
        FindCursedItem = 4,
        DemonFlies = 5,
        PlayerTeleports = 6,
        LightsOut = 7,
        PortalOpens = 8,
        DemonThrowsUp = 9,
        TieDownDemon = 10,
        GhostsAttack = 11,
        FirstAidForDemon = 12,
        TelevisionTurnsOn = 13,
        SatanistsAttack = 14,
        TrueName = 15,
        None = 16
    }

    public static readonly Dictionary<Events, PlayerItems> EventsToItemsMap = new Dictionary<Events, PlayerItems>
    {
        {Events.PlayerPossesed, PlayerItems.Chalice},
        {Events.DemonThrowsItems, PlayerItems.None},
        {Events.PhoneRings, PlayerItems.None},
        {Events.UnholyFog, PlayerItems.Senser},
        {Events.FindCursedItem, PlayerItems.None},
        {Events.DemonFlies, PlayerItems.Bible},
        {Events.PlayerTeleports, PlayerItems.None},
        {Events.LightsOut, PlayerItems.Towel},
        {Events.PortalOpens, PlayerItems.Chalk},
        {Events.DemonThrowsUp, PlayerItems.None},
        {Events.TieDownDemon, PlayerItems.Shakle},
        {Events.GhostsAttack, PlayerItems.Crucifix},
        {Events.FirstAidForDemon, PlayerItems.Medkit},
        {Events.TelevisionTurnsOn, PlayerItems.None},
        {Events.SatanistsAttack, PlayerItems.HolyWater},
        {Events.TrueName, PlayerItems.None},
        {Events.None, PlayerItems.None},
    };
}


