using DeepFreeze;

namespace DF {
    public sealed class StockHeatService : IHeatService
    {
      public double GetSystemTemperature(DeepFreezer callerModule) {
        return callerModule.part.temperature;
      }

      public void AddThermalFlux(DeepFreezer callerModule, double fluxAmount) {
        callerModule.part.AddThermalFlux(fluxAmount);
      }

      public void UpdateHeatFlux(DeepFreezer callerModule) {}
    }
}
