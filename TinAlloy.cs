namespace Minecraft_Alloy_Calculator;

public class TinAlloy : Alloy
{
    public TinAlloy()
    {
        Name = EAlloyName.TinAlloy;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Tin(), [0.45f, 0.55f]},
            {new Iron(), [0.45f,0.55f] }
        };
    }
}