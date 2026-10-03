namespace Minecraft_Alloy_Calculator;

public enum EAlloyRecipeType {Percentage, Ingot}

public abstract class Alloy
{
    protected Dictionary<Alloy, float[]> alloyInformation = new Dictionary<Alloy, float[]>();
    public EAlloyName Name;
    public EAlloyRecipeType RecipeType { get; protected set; } = EAlloyRecipeType.Percentage;

    public Dictionary<Alloy, float[]> AlloyInformation
    {
        get => alloyInformation;
        set => alloyInformation = value;
    }
}