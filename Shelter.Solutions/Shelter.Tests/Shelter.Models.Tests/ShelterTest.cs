using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shelter.Models;

namespace Shelter.Models.Tests
{
    [TestClass]
    public class AnimalsTest
    {
        [TestMethod]
        public void ShelterTests()
        {
          DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=shelter_test;";
        }
        [TestMethod]
        public void Dispose()
        {
            Console.WriteLine("Happening?");
            Animal.ClearAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            int result = Animal.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_AddAnimalToDatabase_1()
        {
            Animal newAnimal = new Animal("Fido", "dog", "German Shephard", 1, 0,0,"good dog");
            newAnimal.Save();
            Assert.AreEqual(1, Animal.GetAll().Count);
        }
    }
}
