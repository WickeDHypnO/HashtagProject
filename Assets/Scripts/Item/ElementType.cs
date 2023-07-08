
using System.Collections.Generic;

public enum ElementType
{
    None,
    Physical,
    Fire,
    Ice,
    Electric,
    Earth
}

public record Element
{
    public ElementType Type;
    public ElementType Weakness;
    public ElementType Empowers;
}
public static class ElementalConsts
{
    public static readonly IEnumerable<Element> ElementalEmpower = new List<Element>()
    {
        new Element()
        {
            Type = ElementType.Fire,
            Weakness = ElementType.Ice,
            Empowers = ElementType.Earth
        },
        new Element()
        {
            Type = ElementType.Earth,
            Weakness = ElementType.Fire,
            Empowers = ElementType.Electric
        },
        new Element()
        {
            Type = ElementType.Electric, 
            Weakness = ElementType.Earth, 
            Empowers = ElementType.Ice
        },
        new Element()
        {
            Type = ElementType.Ice,
            Weakness = ElementType.Electric,
            Empowers = ElementType.Fire
        }
    };
} 
