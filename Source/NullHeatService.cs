using DeepFreeze;

namespace DF {
    public sealed class NullHeatService : IHeatService
    {
      public double GetSystemTemperature(DeepFreezer callerModule) {
        return 0;
      }

      public void AddThermalFlux(DeepFreezer callerModule, double fluxAmount) {}

      public void UpdateHeatFlux(DeepFreezer callerModule) {}
    }
}
