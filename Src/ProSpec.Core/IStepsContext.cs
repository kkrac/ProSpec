using System;

namespace ProSpec.Core
{
    /// <summary>
    /// Interface to be implemented by step definitions classes.
    /// </summary>
    /// <typeparam name="TDriver"></typeparam>
    public interface IStepsContext<TDriver> where TDriver : ITestDriver
    {
        /// <summary>
        /// Reference to the driver that is active.
        /// </summary>
        TDriver Driver { get; }
        /// <summary>
        /// Gets an object from the scenario context.
        /// </summary>
        /// <typeparam name="T">Type of the object to retrieve</typeparam>
        /// <returns>Object of generic type T</returns>
        T Get<T>();
        /// <summary>
        /// Gets an object from the corresponding context depending on the object's life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to retrieve</typeparam>
        /// <param name="lifeSpan">Life span of the object</param>
        /// <returns>Object of generic type T</returns>
        T Get<T>(ObjectLifeSpan lifeSpan);
        /// <summary>
        /// Gets an object from the scenario context.
        /// </summary>
        /// <typeparam name="T">Type of the object to retrieve</typeparam>
        /// <param name="key">Key of the object stored in the context</param>
        /// <returns>Object of generic type T</returns>
        T Get<T>(string key);
        /// <summary>
        /// Gets an object from the corresponding context depending on the object's life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to retrieve</typeparam>
        /// <param name="key">Key of the object stored in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        /// <returns>Object of generic type T</returns>
        T Get<T>(string key, ObjectLifeSpan lifeSpan);
        /// <summary>
        /// Stores an object in the scenario context.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="data">Reference to the object to store in the context</param>
        void Set<T>(T data);
        /// <summary>
        /// Stores an object in the corresponding context depending on the object's life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="data">Reference to the object to store in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        void Set<T>(T data, ObjectLifeSpan lifeSpan);
        /// <summary>
        /// Stores an object in the scenario context.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="key">Key with which the object is stored in the context</param>
        /// <param name="data">Reference to the object to store in the context</param>
        void Set<T>(string key, T data);
        /// <summary>
        /// Stores an object in the corresponding context depending on the object's life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="key">Key with which the object is stored in the context</param>
        /// <param name="data">Reference to the object to store in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        void Set<T>(string key, T data, ObjectLifeSpan lifeSpan);
        /// <summary>
        /// Stores an object in the scenario context.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="func">Function executed to store the object in the context</param>
        void Set<T>(Func<T> func);
        /// <summary>
        /// Stores an object in the corresponding context depending on the object's life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="func">Function executed to store the object in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        void Set<T>(Func<T> func, ObjectLifeSpan lifeSpan);
    }
}