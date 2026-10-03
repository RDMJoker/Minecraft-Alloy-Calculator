namespace Minecraft_Alloy_Calculator;

public class BismuthBronze : Alloy
{
    public BismuthBronze()
    {
        Name = EAlloyName.BismuthBronze;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Copper(), [0.5f, 0.65f]},
            {new Zinc(), [0.2f, 0.3f]},
            {new Bismuth(), [0.1f, 0.2f]}
        };
    }
}