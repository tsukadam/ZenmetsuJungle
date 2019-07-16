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
            Player.GetComponent<ControllerAttack>().EquipWeapon(0,"GunBullet");
            Player.GetComponent<ControllerAttack>().MakeWeapon(0);
            GameObject Weapon = Player.transform.Find("GunBullet(Clone)").gameObject;
            Weapon.GetComponent<ControllerWeapon>().SetTypeDetail("RodSword");
            string TypeDetail = Weapon.GetComponent<ControllerWeapon>().GetTypeDetail();
            Assert.IsTrue(TypeDetail == "RodSword");
        }
    }
}
