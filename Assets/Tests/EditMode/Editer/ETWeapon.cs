using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ETWeapon
    {
        public GameObject Player;

        [OneTimeSetUp]
        public void ETCharaMoveSetUp()
        {
            Player = GameObject.Find("Player");

        }

        [Test]
        public void TestSetGetTypeDetail()
        {
            Player.GetComponent<ControllerAttack>().EquipWeapon("GunBullet");
            Player.GetComponent<ControllerAttack>().MakeWeapon();
            GameObject Weapon = Player.transform.Find("GunBullet(Clone)").gameObject;
            Weapon.GetComponent<ControllerWeapon>().SetTypeDetail("RodSword");
            string TypeDetail = Weapon.GetComponent<ControllerWeapon>().GetTypeDetail();
            Assert.IsTrue(TypeDetail == "RodSword");
        }
    }
}
