namespace Acme.TechnicalTest.Domain.Contracts.Shared
{
    public interface IAsyncTasks
    {
        /// <summary>
        /// Send a message for a provider to handle with async tasks send and forget.
        /// </summary>
        /// <typeparam name="T">A serializable class</typeparam>
        /// <param name="obj">Object to be send</param>
        /// <param name="eventName">the name of the event</param>
        Task Send<T>(T obj, string eventName = "") where T: class;
    }
}
