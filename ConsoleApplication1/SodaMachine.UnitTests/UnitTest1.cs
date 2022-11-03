using NUnit.Framework;
using ConsoleApplication1;
using ConsoleApplication1.Model;
using System.Collections.Generic;


namespace UnitTests
{
    public class SodaMachineShould
    {
        private SodaMachine _sut;

        public SodaMachineShould()
        {
            _sut = new SodaMachine();
        }

        [Test]
        public void OnAddMoney_MoneyContainCorrectAmount()
        {
            //Arrange

            //Act
            _sut.AddMoney(15);

            //Assert
            Assert.AreEqual(15, _sut.money);
        }

        [Test]
        public void OnRecallMoney_MoneyisReturned()
        {
            //Arrange
            _sut.AddMoney(15);

            //Act
            _sut.RecallMoney();

            //Assert
            Assert.AreEqual(0, _sut.money);
        }

        [Test]
        public void OnOrder_WithEnoughMoney_SubtractNr()
        {
            //Arrange
            List<Soda> newInventory = new List<Soda>() { new Soda() { Name = "coke", Nr = 2, Price = 20 } };
            _sut = new SodaMachine(newInventory);
            _sut.AddMoney(20);

            //Act
            _sut.Order("coke");

            //Assert
            Assert.AreEqual(1, _sut.inventory.Find(x => x.Name.Equals("coke")).Nr);
        }


        [Test]
        public void OnOrder_WithNotEnoughMoney_NotSubtractNr()
        {
            //Arrange
            List<Soda> newInventory = new List<Soda>() { new Soda() { Name = "coke", Nr = 2, Price = 20 } };
            _sut = new SodaMachine(newInventory);
            _sut.AddMoney(15);

            //Act
            _sut.Order("coke");

            //Assert
            Assert.AreEqual(2, _sut.inventory.Find(x => x.Name.Equals("coke")).Nr);
        }

        [Test]
        public void OnAddProduct_AddNewSoda_IncreaseNumberOfAvailableProducts()
        {
            //Arrange
            List<Soda> newInventory = new List<Soda>() { new Soda() { Name = "coke", Nr = 2, Price = 20 } };
            _sut = new SodaMachine(newInventory);

            //Act
            _sut.AddProduct(new Soda() { Name = "Fanta", Nr = 10, Price = 15});

            //Assert
            Assert.AreEqual(2, _sut.inventory.Count);
        }

        [Test]
        public void OnAddProduct_AddExistingSoda_NotIncreaseNumberOfAvailableProducts()
        {
            //Arrange
            List<Soda> newInventory = new List<Soda>() { new Soda() { Name = "coke", Nr = 2, Price = 20 } };
            _sut = new SodaMachine(newInventory);

            //Act
            _sut.AddProduct(new Soda() { Name = "coke", Nr = 10, Price = 15 });

            //Assert
            Assert.AreEqual(1, _sut.inventory.Count);
            Assert.AreEqual(2, _sut.inventory.Find(x => x.Name.Equals("coke")).Nr);
            Assert.AreEqual(20, _sut.inventory.Find(x => x.Name.Equals("coke")).Price);
        }
    }
}