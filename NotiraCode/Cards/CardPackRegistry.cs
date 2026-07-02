using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MegaCrit.Sts2.Core.Models;

namespace Notira.Notira.Cards;

public static class CardPackRegistry
{
    private static Dictionary<string, List<Type>> _packs;
    private static bool _initialized;

    public static IReadOnlyDictionary<string, List<Type>> Packs
    {
        get
        {
            EnsureInitialized();
            return _packs;
        }
    }

    public static List<string> AllPackNames
    {
        get
        {
            EnsureInitialized();
            return _packs.Keys.OrderBy(n => n).ToList();
        }
    }

    private static void EnsureInitialized()
    {
        if (_initialized) return;
        Initialize();
    }

    public static void Initialize()
    {
        if (_initialized) return;

        _packs = new Dictionary<string, List<Type>>();

        // 1. Scan [CardPack] attributes on NotiraCard subclasses
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (!type.IsAbstract && type.IsSubclassOf(typeof(NotiraCard)))
            {
                var attr = type.GetCustomAttribute<CardPackAttribute>();
                if (attr == null) continue;

                if (!_packs.ContainsKey(attr.PackName))
                    _packs[attr.PackName] = new List<Type>();
                _packs[attr.PackName].Add(type);
            }
        }

        // 2. Merge definitions from CardPackDefinitions (overrides attributes)
        foreach (var kvp in CardPackDefinitions.GetPacks())
        {
            _packs[kvp.Key] = kvp.Value;
        }

        _initialized = true;
    }

    public static IEnumerable<CardModel> GetCardModelsInPacks(IEnumerable<string> packNames)
    {
        EnsureInitialized();

        var types = new HashSet<Type>();
        foreach (var name in packNames)
        {
            if (_packs.TryGetValue(name, out var cards))
            {
                foreach (var t in cards)
                    types.Add(t);
            }
        }

        return types
            .Select(t => ModelDb.GetById<CardModel>(ModelDb.GetId(t)))
            .Where(c => c != null);
    }
}
