namespace Minecraft_Alloy_Calculator;

public class RoseGold : Alloy
{
    public RoseGold()
    {
        Name = EAlloyName.RoseGold;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Gold(), [0.7f, 0.85f]},
            {new Copper(), [0.15f, 0.3f]}
        };
    }
}