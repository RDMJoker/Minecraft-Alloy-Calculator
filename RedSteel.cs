namespace Minecraft_Alloy_Calculator;

public class RedSteel : Alloy
{
    public RedSteel()
    {
        Name = EAlloyName.RedSteel;
        RecipeType = EAlloyRecipeType.Ingot;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new WeakRedSteelIngot(), [1f,1f]},
            {new BlackSteel(), [1f,1f]}
        };
    }
}