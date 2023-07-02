﻿using UnityEditor;
using ATypes = AlephVault.Unity.Support.Generic.Authoring.Types;

namespace AlephVault.Unity.GabTab
{
    namespace Authoring
    {
        namespace Behaviours
        {
            [CustomPropertyDrawer(typeof(Interactors.InteractorsManager.InteractorsDictionary))]
            [CustomPropertyDrawer(typeof(Interactors.ButtonsInteractor.ButtonKeyDictionary))]
            public class DictionaryPropertyDrawer : ATypes.DictionaryPropertyDrawer { }
        }
    }
}