using System;

namespace Notira.Notira.Cards;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class CardPackAttribute : Attribute
{
    public string PackName { get; }

    public CardPackAttribute(string packName)
    {
        PackName = packName;
    }
}
