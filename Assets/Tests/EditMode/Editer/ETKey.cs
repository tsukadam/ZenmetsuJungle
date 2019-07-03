using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    [TestFixture]
    public class ETKey : MonoBehaviour
    {
        public GameObject Controller;

        [OneTimeSetUp]
        public void ETKeyTestSetUp() { 
        Controller=GameObject.Find("ControllerGame");

        }
            [Test]
        public void CheckInitNumericKey()//初期値チェック
        {
            var state = new StateKey();
            var stateLeft = state.GetStateLeft();
            var stateRight = state.GetStateRight();
            var stateTop = state.GetStateTop();
            var stateBottom = state.GetStateBottom();
            Assert.AreEqual(stateLeft,0);
            Assert.AreEqual(stateRight, 0);
            Assert.AreEqual(stateTop, 0);
            Assert.AreEqual(stateBottom, 0);
        }
        

    }
}
