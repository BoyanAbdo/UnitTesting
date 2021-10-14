using CarManager;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CarTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Mercedes", "E350d", 18.2d, 122.2)]
        [TestCase("Dacia", "Sandero", 7.2d, 88.2)]
        [TestCase("Toyota", "Prius", 4.2d, 100)]
        public void CheckCarConstructor
            (string make, string model, double fuelConsumption, double fuelCapacity)
        {
            // Arrange
            var car = new Car(make, model, fuelConsumption, fuelCapacity);

            // Assert
            Assert.AreEqual(car.Make, make);
            Assert.AreEqual(car.Model, model);
            Assert.AreEqual(car.FuelAmount, 0);
            Assert.AreEqual(car.FuelConsumption, fuelConsumption);
            Assert.AreEqual(car.FuelCapacity, fuelCapacity);
        }

        [Test]
        [TestCase("", "E350d", 18.2d, 122.2)]
        [TestCase(null, "E350d", 18.2d, 122.2)]
        public void CarMakeSetterShouldThrowExceptionIfEmptyOrNullInput
            (string make, string model, double fuelConsumption, double fuelCapacity)
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity))
                .Message.Equals("Make cannot be null or empty!");
        }

        [Test]
        [TestCase("Mercedes", "", 18.2d, 122.2)]
        [TestCase("Mercedes", null, 18.2d, 122.2)]
        public void CarModelSetterShouldThrowExceptionIfEmptyOrNullInput
            (string make, string model, double fuelConsumption, double fuelCapacity)
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity))
                .Message.Equals("Model cannot be null or empty!");
        }

        [Test]
        [TestCase("Mercedes", "E350d", 0d, 122.2d)]
        [TestCase("Mercedes", "E350d", -11d, 122.2d)]
        public void CarFuelConsumptionSetterShouldThrowExceptionIfZeroOrNegativeInput
            (string make, string model, double fuelConsumption, double fuelCapacity)
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity))
                .Message.Equals("Fuel consumption cannot be zero or negative!");
        }

        [Test]
        [TestCase("Mercedes", "E350d", 12d, 0d)]
        [TestCase("Mercedes", "E350d", 12d, -11d)]
        public void FuelCapacitySetterShouldThrowExceptionIfZeroOrNegativeInput
            (string make, string model, double fuelConsumption, double fuelCapacity)
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new Car(make, model, fuelConsumption, fuelCapacity))
                .Message.Equals("Fuel capacity cannot be zero or negative!");
        }

        [Test]
        [TestCase(0d)]
        [TestCase(-88d)]
        public void RefuelMethodShouldThrowExceptionIfZeroOrNegativeInput(double fuelToRefuel)
        {
            // Assert
            Assert.Throws<ArgumentException>(
                () => new Car("Car", "Test", 1d, 20d)
                .Refuel(fuelToRefuel))
                .Message.Equals("Fuel amount cannot be zero or negative!");
        }

        [Test]
        [TestCase(20d)]
        [TestCase(30d)]
        public void RefuelMethodShouldIncreaseFuelAmount(double fuelToRefuel)
        {
            // Arrange
            var car = new Car("Car", "Test", 1d, 120d);

            // Act
            car.Refuel(fuelToRefuel);

            // Assert
            Assert.AreEqual(car.FuelAmount, fuelToRefuel);
        }

        [Test]
        [TestCase(200d)]
        [TestCase(300d)]
        public void RefuelWithMoreFuelShouldSetFuelAmountToFuelCapacity(double fuelToRefuel)
        {
            // Arrange
            var car = new Car("Car", "Test", 1d, 120d);

            // Act
            car.Refuel(fuelToRefuel);

            // Assert
            Assert.AreEqual(car.FuelAmount, car.FuelCapacity);
        }

        [Test]
        [TestCase(500d)]
        [TestCase(700d)]
        public void DriveMethodShouldThrowExceptionIfDistanceIsTooLong(double distanceToTravel)
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
                new Car("Car", "Test", 20d, 60d)
               .Drive(distanceToTravel))
               .Message.Equals("You don't have enough fuel to drive!");
        }

        [Test]
        [TestCase(100d)]
        [TestCase(200d)]
        public void DriveMethodShouldDecreaseFuelAmount(double distanceToTravel)
        {
            // Arrange
            var car = new Car("Car", "Test", 10d, 100d);

            // Act
            car.Refuel(100);
            var carInitialFuelAmount = car.FuelAmount;

            car.Drive(distanceToTravel);

            double fuelNeeded = (distanceToTravel / 100) * car.FuelConsumption;

            // Assert
            Assert.That(car.FuelAmount, Is.EqualTo(carInitialFuelAmount - fuelNeeded));
        }
    }
}
