﻿using System;
using AlephVault.Unity.Support.Generic.Authoring.Types;
using UnityEngine;

namespace GameMeanMachine.Unity.GabTab
{
    namespace Authoring
    {
        namespace Behaviours
        {
            namespace Interactors
            {
                /// <summary>
                ///   This component registers all the (other) components that will be used as interactors.
                /// </summary>
                /// <remarks>
                ///   See the example in <see cref="InteractiveInterface.RunInteraction(Func{InteractorsManager, InteractiveMessage, System.Threading.Tasks.Task})"/> to understand how is this class used. 
                /// </remarks>
                /// <seealso cref="Interactor"/>
                public class InteractorsManager : MonoBehaviour
                {
                    /// <summary>
                    ///   A dictionary of keys and interactor instances. 
                    /// </summary>
                    [Serializable]
                    public class InteractorsDictionary : Dictionary<string, Interactor> { }

                    /// <summary>
                    ///   Registered interactors.
                    /// </summary>
                    /// <remarks>
                    ///   Edit this member in the Inspector to tell which interactors will this instance
                    ///     have access to.
                    /// </remarks>
                    [SerializeField]
                    private InteractorsDictionary interactors = new InteractorsDictionary();

                    /// <summary>
                    ///   Retrieves an interactor to be queried/used.
                    /// </summary>
                    /// <param name="key">The key the target interactor was registered with.</param>
                    /// <returns>A registered <see cref="Interactor"/>.</returns>
                    /// <exception cref="KeyNotFoundException" />
                    public Interactor this[string key]
                    {
                        get { return interactors[key]; }
                    }
                }
            }
        }
    }
}