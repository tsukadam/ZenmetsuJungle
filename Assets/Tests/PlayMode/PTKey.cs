using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class PTKey : MonoBehaviour
    {
        public GameObject Controller;

        [OneTimeSetUp]
        public void PTKeyTestSetUp()
        {
            Controller = GameObject.Find("ControllerGame");

        }

        [UnityTest]
        public IEnumerator CheckMoveLeft()
        {
        

        yield return null;
        }
    }
}
