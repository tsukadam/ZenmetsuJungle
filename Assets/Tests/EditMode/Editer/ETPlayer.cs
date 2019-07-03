using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ETPlayer
    {
        public GameObject Player;

        [OneTimeSetUp]
        public void ETCharaMoveTestSetUp()
        {
            Player = GameObject.Find("Player");
       
        }
    }
}
