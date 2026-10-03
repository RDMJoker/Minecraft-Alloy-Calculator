namespace Minecraft_Alloy_Calculator;

public class Brass : Alloy
{
    public Brass()
    {
        Name = EAlloyName.Brass;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Copper(), [0.7f, 0.8f]},
            {new Zinc(), [0.2f, 0.3f]}
        };
    }
}