﻿using System;
using TechTalk.SpecFlow;

namespace ProSpec.Acceptance
{
    /// <summary>
    /// Step base implementation that encapsulates Specflow contexts providing easy access to common objects.
    /// </summary>
    public abstract class StepsContextBase<TDriver> : IStepsContext<TDriver> where TDriver : ITestDriver
    {
        private static ScenarioContext ScenarioContext
        {
            get { return ScenarioContext.Current; }
        }

        private static FeatureContext FeatureContext
        {
            get { return FeatureContext.Current; }
        }

        private static GlobalContext GlobalContext
        {
            get { return GlobalContext.Current; }
        }

        /// <summary>
        /// Gets Specflow context depending on the life span specified.
        /// </summary>
        /// <param name="lifeSpan">Life span of the context</param>
        /// <returns>Context</returns>
        protected static SpecFlowContext GetContext(ObjectLifeSpan lifeSpan)
        {
            switch (lifeSpan)
            {
                case ObjectLifeSpan.Global:
                    return GlobalContext;
                case ObjectLifeSpan.Feature:
                    return FeatureContext;
                case ObjectLifeSpan.Scenario:
                default:
                    return ScenarioContext;
            }
        }

        /// <summary>
        /// Gets an object with a specific life span from the context.
        /// </summary>
        /// <typeparam name="T">Type of the object to retrieve</typeparam>
        /// <param name="lifeSpan">Life span of the object</param>
        /// <returns>Object of generic type T</returns>
        public T Get<T>(ObjectLifeSpan lifeSpan)
        {
            T obj;

            if (GetContext(lifeSpan).TryGetValue<T>(out obj))
            {
                return obj;
            }

            return default(T);
        }

        /// <summary>
        /// Gets an object with a specific life span from the context.
        /// </summary>
        /// <typeparam name="T">Type of the object to retrieve</typeparam>
        /// <param name="key">Key of the object stored in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        /// <returns>Object of generic type T</returns>
        public T Get<T>(string key, ObjectLifeSpan lifeSpan)
        {
            T obj;

            if (GetContext(lifeSpan).TryGetValue<T>(key, out obj))
            {
                return obj;
            }

            return default(T);
        }

        /// <summary>
        /// Stores in the context an object with a specific life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="data">Reference to the object to store in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        public void Set<T>(T data, ObjectLifeSpan lifeSpan)
        {
            GetContext(lifeSpan).Set<T>(data);
        }

        /// <summary>
        /// Stores in the context an object with a specific life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="key">Key with which the object is stored in the context</param>
        /// <param name="data">Reference to the object to store in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        public void Set<T>(string key, T data, ObjectLifeSpan lifeSpan)
        {
            GetContext(lifeSpan).Set<T>(data, key);
        }

        /// <summary>
        /// Stores in the context an object with a specific life span.
        /// </summary>
        /// <typeparam name="T">Type of the object to store</typeparam>
        /// <param name="func">Function executed to store the object in the context</param>
        /// <param name="lifeSpan">Life span of the object</param>
        public void Set<T>(Func<T> func, ObjectLifeSpan lifeSpan)
        {
            GetContext(lifeSpan).Set<T>(func());
        }

        /// <summary>
        /// Reference to the driver that is currently active.
        /// </summary>
        public abstract TDriver Driver
        {
            get;
            internal set;
        }
    }
}
