using DeepFreeze;

namespace DF {

    public struct SystemHeatService : IHeatService
    {
        public double GetSystemTemperature(DeepFreezer callerModule) {
            var shModule = callerModule.part.Modules["ModuleSystemHeat"];
            var foo = shModule.Fields["currentLoopTemperature"];
            return (float)foo.GetValue(shModule);
        }

        public void AddThermalFlux(DeepFreezer callerModule, double heatAmount) {
            PartModule targetModule = callerModule.part.Modules["ModuleSystemHeat"];
            SystemHeatWrapper.AddFlux(targetModule, "DeepFreezeLoopSystemHeat", 30, (float)DeepFreeze.Instance.DFsettings.heatamtMonitoringFrznKerbals * callerModule.TotalFrozen, true);
        }

        public void UpdateHeatFlux(DeepFreezer callerModule) {
            PartModule targetModule = callerModule.part.Modules["ModuleSystemHeat"];
            int isOneFrozen = (callerModule.TotalFrozen > 0) ? 1 : 0;
            SystemHeatWrapper.AddFlux(targetModule, "DeepFreezeLoopSystemHeat", 30*isOneFrozen, (float)DeepFreeze.Instance.DFsettings.heatamtMonitoringFrznKerbals * callerModule.TotalFrozen, true);
        }
    }
}
