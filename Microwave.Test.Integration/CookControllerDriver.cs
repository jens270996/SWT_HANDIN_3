using Microwave.Classes.Interfaces;

namespace Microwave.Test.Integration
{
    public class CookControllerDriver 
    {
        private IPowerTube myPowerTube;

        public CookControllerDriver(IPowerTube powerTube)
        {
            myPowerTube = powerTube;
        }

        public void StartCooking (int power)
        {
            myPowerTube.TurnOn(power);
        }

        public void Stop()
        {
            myPowerTube.TurnOff();
        }
    }
}