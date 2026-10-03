namespace Minecraft_Alloy_Calculator;

public class BlueSteel : Alloy
{
    public BlueSteel()
    {
        Name = EAlloyName.BlueSteel;
        RecipeType = EAlloyRecipeType.Ingot;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new WeakBlueSteelIngot(), [1f,1f]},
            {new BlackSteel(), [1f,1f]}
        };
    }
}