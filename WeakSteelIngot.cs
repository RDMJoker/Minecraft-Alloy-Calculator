namespace Minecraft_Alloy_Calculator;

public class WeakSteelIngot : Alloy
{
    public WeakSteelIngot()
    {
        Name = EAlloyName.WeakSteelIngot;
        alloyInformation = new Dictionary<Alloy, float[]>()
        {
            {new Steel(), [0.5f, 0.70f]},
            {new BlackBronze(), [0.15f, 0.25f]},
            {new Nickel(), [0.15f, 0.25f]}
        };
    }
}