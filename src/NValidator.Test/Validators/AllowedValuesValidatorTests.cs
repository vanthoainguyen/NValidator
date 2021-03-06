﻿
using System.Linq;
using NUnit.Framework;
using NValidator.Test.Models;
using NValidator.Validators;

// ReSharper disable InconsistentNaming
namespace NValidator.Test.Validators
{
    [TestFixture]
    public class AllowedValuesValidatorTests
    {
        class OrderValidator : TypeValidator<Order>
        {
            public OrderValidator()
            {
                RuleFor(x => x.Name).In("N1", "N2", "N3");
            }
        }

        [Test]
        public void GetValidationResult_return_one_error_result_if_the_order_name_is_null()
        {
            // Arrange
            var order = new Order();
            var validator = new OrderValidator();

            // Action
            var results = validator.GetValidationResult(order).ToList();

            // Assert
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("Order.Name", results[0].MemberName);
            Assert.AreEqual("Name must be one of the allowed values. Allowed values: N1, N2, N3.", results[0].Message);
        }

        [Test]
        public void GetValidationResult_return_one_error_result_if_the_order_name_is_not_one_of_the_allowed_values()
        {
            // Arrange
            var order = new Order
            {
                Name = "ABC"
            };
            var validator = new OrderValidator();

            // Action
            var results = validator.GetValidationResult(order).ToList();

            // Assert
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("Order.Name", results[0].MemberName);
            Assert.AreEqual("Name must be one of the allowed values. Allowed values: N1, N2, N3.", results[0].Message);
        }

        [Test]
        public void GetValidationResult_return_no_error_result_if_the_order_name_is_one_of_the_allowed_values()
        {
            // Arrange
            var order = new Order
            {
                Name = "N1"
            };
            var validator = new OrderValidator();

            // Action
            var results = validator.GetValidationResult(order).ToList();

            // Assert
            Assert.AreEqual(0, results.Count());
        }

        [TestCase(null, true)]
        [TestCase("", false)]
        [TestCase("1", true)]
        [TestCase("2", true)]
        [TestCase("3", true)]
        [TestCase("4", false)]
        public void IsValid_should_return_true_if_ignore_null_value(string value, bool result)
        {
            // Arrange
            var validator = new AllowedValuesValidator<string>(new[] {"1", "2", "3"}, true);

            // Action
            var actualResult = validator.IsValid(value);

            // Assert
            Assert.That(actualResult, Is.EqualTo(result));
        }
    }
}
// ReSharper restore InconsistentNaming