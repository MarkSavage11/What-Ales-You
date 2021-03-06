﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipes : MonoBehaviour
{
    public static Dictionary<string, Drink> recipes = new Dictionary<string, Drink>
    {
        {"Moscow Mule", new Drink("Moscow Mule", new List<string>() {"Ginger Beer", "Lime Juice", "Vodka"}, "Stirred", "Copper Mug") },
        {"Dark and Stormy", new Drink("Dark and Stormy", new List<string>() {"Ginger Beer", "Lime Juice", "Rum"}, "Stirred", "Highball Glass") },
        {"Tennessee Mule", new Drink("Tennessee Mule", new List<string>() {"Ginger Beer", "Lime Juice", "Whiskey"}, "Stirred", "Copper Mug") },
        {"Jack & Coke", new Drink("Jack & Coke", new List<string>() {"Whiskey", "Coke"}, "Stirred", "Collins Glass") },
        {"Rum & Coke", new Drink("Rum & Coke", new List<string>() {"Rum", "Coke"}, "Stirred", "Collins Glass") },
        {"Whiskey Sour", new Drink("Whiskey Sour", new List<string>() {"Whiskey", "Lemon Juice", "Simple Syrup"}, "Shaken", "Old-Fashioned Glass") },
        {"Boston Sour", new Drink("Boston Sour", new List<string>() {"Whiskey", "Lemon Juice", "Simple Syrup", "Egg White"}, "Shaken", "Old-Fashioned Glass") },
        {"Vodka Cranberry", new Drink("Vodka Cranberry", new List<string>() {"Vodka", "Cranberry Juice", "Lime Juice", "Orange Juice"}, "Stirred", "Highball Glass") },
        {"Screwdriver", new Drink("Screwdriver", new List<string>() {"Vodka", "Orange Juice"}, "Stirred", "Highball Glass") },
    };

}
