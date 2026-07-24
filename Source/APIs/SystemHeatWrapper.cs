using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Debug = UnityEngine.Debug;

namespace DF
{
    /// <summary>
    /// The Wrapper class to access SystemHeat
    /// </summary>
    public class SystemHeatWrapper
    {
        protected static Type SHType;
        protected static Object actualSH;

        public static SHAPI SHActualAPI;

        public static Boolean AssemblyExists { get { return SHType != null; } }

        public static Boolean InstanceExists { get { return SHActualAPI != null; } }

        private static Boolean _SHWrapped;

        public static Boolean APIReady { get { return _SHWrapped; } }

        public static Boolean InitSystemHeatWrapper()
        {
            _SHWrapped = false;
            actualSH = null;
            LogFormatted_DebugOnly("Attempting to Grab SystemHeat Types...");
            LogFormatted("Attempting to Grab SystemHeat Types...");

            SHType = getType("SystemHeat.ModuleSystemHeat");

            if (SHType == null)
            {
                return false;
            }

            LogFormatted("SystemHeat Version:{0}", SHType.Assembly.GetName().Version.ToString());

            LogFormatted_DebugOnly("Got Instance, Creating Wrapper Objects");
            SHActualAPI = new SHAPI();

            _SHWrapped = true;
            return true;
        }

        internal static Type getType(string name)
        {
            Type type = null;
            AssemblyLoader.loadedAssemblies.TypeOperation(t =>

            {
                if (t.FullName == name)
                    type = t;
            }
            );

            if (type != null)
            {
                return type;
            }
            return null;
        }

        public static void AddFlux(PartModule actualModule, string id, float sourceTemperature, float flux, bool useForNominal) {
          SHActualAPI.AddFlux(actualModule, id, sourceTemperature, flux, useForNominal);
        }

        /// <summary>
        /// The Type that is an analogue of the real SystemHeat. This lets you access all the API-able properties and Methods of SystemHeat
        public class SHAPI
        {
            internal SHAPI()
            {
                LogFormatted_DebugOnly("Getting hook_AddFlux Method");
                SHhook_AddFluxMethod = SHType.GetMethod("AddFlux", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                LogFormatted_DebugOnly("Success: " + (SHhook_AddFluxMethod != null));
            }

            #region Methods

            private MethodInfo SHhook_AddFluxMethod;

            /// <summary>
            /// Add heat flux at a given temperature to system (in SystemHeat)
            /// </summary>
            /// <param name="id">the string ID of the source (should be unique)</param>
            /// <param name="sourceTemperature">the temperature of the source</param>
            /// <param name="flux">the flux of the source</param>
            /// <param name="useForNominal">
            ///   whether this temperature should be used when determining the nominal temperature
            ///   of the loop
            /// </param>
            /// public void AddFlux(string id, float sourceTemperature, float flux, bool useForNominal)
            internal void AddFlux(PartModule actualModule, string id, float sourceTemperature, float flux, bool useForNominal)
            {
                try
                {
                    SHhook_AddFluxMethod.Invoke(actualModule, new System.Object[] { id, sourceTemperature, flux, useForNominal });
                }
                catch (Exception ex)
                {
                    LogFormatted("Unable to invoke SystemHeat AddFlux Method");
                    LogFormatted("Exception: {0}", ex);
                    //throw;
                }
            }
            #endregion Methods
        }


        #region Logging Stuff

        /// <summary>
        /// Some Structured logging to the debug file - ONLY RUNS WHEN DLL COMPILED IN DEBUG MODE
        /// </summary>
        /// <param name="Message">Text to be printed - can be formatted as per String.format</param>
        /// <param name="strParams">Objects to feed into a String.format</param>
        internal static void LogFormatted_DebugOnly(String Message, params Object[] strParams)
        {
            if (RSTUtils.Utilities.debuggingOn)
                LogFormatted(Message, strParams);
        }

        /// <summary>
        /// Some Structured logging to the debug file
        /// </summary>
        /// <param name="Message">Text to be printed - can be formatted as per String.format</param>
        /// <param name="strParams">Objects to feed into a String.format</param>
        internal static void LogFormatted(String Message, params Object[] strParams)
        {
            Message = String.Format(Message, strParams);
            String strMessageLine = String.Format("{0},{2}-{3},{1}",
                DateTime.Now, Message, Assembly.GetExecutingAssembly().GetName().Name,
                MethodBase.GetCurrentMethod().DeclaringType.Name);
            Debug.Log(strMessageLine);
        }

        #endregion Logging Stuff
    }
}
