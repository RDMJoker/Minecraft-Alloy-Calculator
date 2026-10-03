namespace Minecraft_Alloy_Calculator;

public class BlackSteel : Alloy
{
    public BlackSteel()
    {
        Name = EAlloyName.BlackSteel;
        RecipeType = EAlloyRecipeType.Ingot;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            { new PigIronIngot(), [1f,1f] },
            { new WeakSteelIngot(), [1f,1f] }
        };
    }
}