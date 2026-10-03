namespace Minecraft_Alloy_Calculator;

public class Bronze : Alloy
{
    public Bronze()
    {
        Name = EAlloyName.Bronze;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Copper(), [0.7f,0.8f]},
            {new Tin(), [0.2f,0.3f]}
        };
    }
}