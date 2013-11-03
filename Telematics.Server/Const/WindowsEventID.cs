namespace Telematics.Server.Const
{
    public enum WindowsEventID
    {
        /// <summary>
        /// There should be a minimum number of events logged with this code Id. It will be used for logging DEBUG information, which should not end up in the Windows Event Log
        /// </summary>
        Unspecified = 0,
        /// <summary>
        /// Events related to the validation and indexing of a feed.
        /// </summary>
        Serialization = 100,
        /// <summary>
        /// Events related to the communication 
        /// </summary>
        TelematicsCommunication = 200,
        /// <summary>
        /// Events that don't fall under any of the categories above but belong to a generic category. 
        /// </summary>
        GenericTelematicsEvent = 300,
        /// <summary>
        /// Configuration events. 
        /// </summary>
        TelematicsConfiguration = 400,



    }
}
