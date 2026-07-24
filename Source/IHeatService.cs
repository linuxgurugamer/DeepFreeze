using DeepFreeze;

namespace DF {
    internal interface IHeatService
    {
        double GetSystemTemperature(DeepFreezer callerModule);

        void AddThermalFlux(DeepFreezer callerModule, double fluxAmount);

        void UpdateHeatFlux(DeepFreezer callerModule);
    }
}
