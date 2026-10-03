namespace Minecraft_Alloy_Calculator;

public class SterlingSilver : Alloy
{
    public SterlingSilver()
    {
        Name = EAlloyName.SterlingSilver;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Silver(), [0.6f,0.8f]},
            {new Copper(), [0.2f,0.4f]}
        };
    }
}