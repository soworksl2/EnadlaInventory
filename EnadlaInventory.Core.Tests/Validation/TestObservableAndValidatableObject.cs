using EnadlaInventory.Core.Validation;
using Moq;

namespace EnadlaInventory.Core.Tests.Validation
{
    //TODO: change these test method from the london to clasical aproach - Jimy Aguasviva 5-november-2022
    [TestClass]
    public class TestObservableAndValidatableObject
    {
        
        //These method are not necessary because we are using nullabe-context so when somebody try to set null to these methods
        //a warning apears
        //[TestMethod]
        //public void PrepareValidation_WithTargetAsNull_ArgumentNullExceptionRaised()
        //{
        //    var person = new PersonModelSample();
        //    var mockValidator = new Mock<ISimpleValidator<PersonModelSample>>();

        //    Assert.ThrowsException<ArgumentNullException>(() =>
        //    {
        //        person.PrepareValidation(null, mockValidator.Object);
        //    });
        //}

        //[TestMethod]
        //public void PrepareValidation_WithValidatorAsNull_ArgumentNullExceptionRaised()
        //{
        //    var person = new PersonModelSample();

        //    Assert.ThrowsException<ArgumentNullException>(() =>
        //    {
        //        person.PrepareValidation(person, null);
        //    });
        //}

        [TestMethod]
        public void Validate_WithoutCallPrepareValidationBefore_InvalidOperationExceptionRaised()
        {
            PersonModelSample person = new PersonModelSample();

            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                person.Validate();
            });
        }

        [TestMethod]
        public void WhenValidate_InternalErrorsAreUpdatedWithErrorsRaisedFromTheUsedValidator()
        {
            var person = new PersonModelSample();
            var mockValidator = new Mock<ISimpleValidator<PersonModelSample>>();

            mockValidator.Setup(x => x.Validate(It.IsAny<PersonModelSample>(), It.IsAny<string>(), It.IsAny<string[]>()))
                .Returns(new ValidationResult()
                {
                    Errors = new List<ValidationFailure>()
                    {
                        new ValidationFailure("testProperty", "1"),
                        new ValidationFailure("testProperty", "2"),
                        new ValidationFailure("testProperty", "3")
                    }
                });


            person.PrepareValidation(person, mockValidator.Object);
            person.Validate();

            var errorSaved = person.GetErrors(null).Cast<string>().ToArray();

            var expectedErrors = new string[3] { "1", "2", "3" };

            CollectionAssert.AreEquivalent(errorSaved, expectedErrors);
        }

        [TestMethod]
        public void WhenValidateAgain_InternalErrorsAreReplacedWithMostNewErrorsRaisedFromTheUsedValidator()
        {
            var person = new PersonModelSample() { Name = "WrongName1", LastName = "WrongLastName1" };
            var mockValidator = new Mock<ISimpleValidator<PersonModelSample>>();

            mockValidator
                .Setup(x => x.Validate(It.IsAny<PersonModelSample>(), It.IsAny<string>(), It.IsAny<string[]>()))
                .Returns<PersonModelSample, string, string[]>((target, propertyName, rulesets) =>
                {
                    var output = new ValidationResult();

                    if (!string.IsNullOrEmpty(target.Name))
                        output.Errors.Add(new ValidationFailure(nameof(target.Name), target.Name));

                    if (!string.IsNullOrEmpty(target.LastName))
                        output.Errors.Add(new ValidationFailure(nameof(target.LastName), target.LastName));

                    return output;
                });

            person.PrepareValidation(person, mockValidator.Object);
            person.Validate();

            var savedErrors = person.GetErrors(null).Cast<string>().ToArray();
            var expectedErrors = new string[] { "WrongName1", "WrongLastName1" };

            CollectionAssert.AreEquivalent(savedErrors, expectedErrors);

            person.Name = "WrongName2";
            person.LastName = null;

            person.Validate();

            savedErrors = person.GetErrors(null).Cast<string>().ToArray();
            expectedErrors = new string[] { "WrongName2" };

            CollectionAssert.AreEquivalent(savedErrors, expectedErrors);
        }

        [TestMethod]
        public void WhenValidate_ErrorsChangedIsRaisedWithCorrectPropertyName()
        {
            var person = new PersonModelSample() { Name = "WrongName1", LastName = "WrongLastName1" };
            var mockValidator = new Mock<ISimpleValidator<PersonModelSample>>();

            mockValidator
                .Setup(x => x.Validate(It.IsAny<PersonModelSample>(), It.IsAny<string>(), It.IsAny<string[]>()))
                .Returns(new ValidationResult());

            var ErrorsChangedCalls = new List<string?>();

            person.PrepareValidation(person, mockValidator.Object);
            person.ErrorsChanged += (sender, e) => ErrorsChangedCalls.Add(e.PropertyName);

            person.Validate();
            person.Validate(nameof(person.Name));

            var ErrorsChangeCallsExpected = new List<string?>() { null, nameof(person.Name) };

            CollectionAssert.AreEqual(ErrorsChangedCalls, ErrorsChangeCallsExpected);
        }

        [TestMethod]
        [DataRow(new string[] { "RuleSet1", "RuleSet2" }, DisplayName = "WhenDefaultSetIsNotNull")]
        [DataRow(null, DisplayName = "WhenDefaultSetIsNull")]
        public void Validate_WithoutRuleSet_DefaultRuleSetIsUsedWithTheValidator(string[]? defaultRuleSet)
        {
            string[]? ruleSetsUsed = null;

            PersonModelSample person = new PersonModelSample();
            Mock<ISimpleValidator<PersonModelSample>> mockValidator = new Mock<ISimpleValidator<PersonModelSample>>();

            mockValidator
                .Setup(x => x.Validate(It.IsAny<PersonModelSample>(), It.IsAny<string?>(), It.IsAny<string[]?>()))
                .Returns<PersonModelSample, string, string[]?>((obj, propertyName, ruleSets) =>
                {
                    ruleSetsUsed = ruleSets;

                    return new ValidationResult();
                });

            person.PrepareValidation(person, mockValidator.Object);
            person.DefaultRuleSets = defaultRuleSet;

            person.Validate();

            Assert.AreEqual(ruleSetsUsed, defaultRuleSet);
        }

        [TestMethod]
        public void Validate_WithRuleSet_ThatRuleSetWillBeUsedInTheValidator()
        {
            string[]? ruleSetsUsed = null;

            PersonModelSample person = new PersonModelSample();
            Mock<ISimpleValidator<PersonModelSample>> mockValidator = new Mock<ISimpleValidator<PersonModelSample>>();

            mockValidator
                .Setup(x => x.Validate(It.IsAny<PersonModelSample>(), It.IsAny<string?>(), It.IsAny<string[]?>()))
                .Returns<PersonModelSample, string, string[]?>((obj, propertyName, ruleSets) =>
                {
                    ruleSetsUsed = ruleSets;

                    return new ValidationResult();
                });

            string[]? specificsRuleSets = new string[] { "RuleSet1", "RuleSet2" };

            person.PrepareValidation(person, mockValidator.Object);
            person.Validate(ruleSets: specificsRuleSets);

            Assert.AreEqual(ruleSetsUsed, specificsRuleSets);
        }

        [TestMethod]
        public void Validate_WithPropertyName_OnlyErrorsWithSpecificPropertyNameAreUpdate()
        {
            PersonModelSample person = new PersonModelSample();
            Mock<ISimpleValidator<PersonModelSample>> mockValidator = new Mock<ISimpleValidator<PersonModelSample>>();

            mockValidator
                .Setup(x => x.Validate(It.IsAny<PersonModelSample>(), It.IsAny<string?>(), It.IsAny<string[]?>()))
                .Returns<PersonModelSample, string, string[]?>((obj, propertyName, ruleSets) =>
                {
                    List<ValidationFailure> failures = new List<ValidationFailure>();

                    if (obj.Name is not null)
                        failures.Add(new ValidationFailure(nameof(obj.Name), obj.Name));

                    if (obj.LastName is not null)
                        failures.Add(new ValidationFailure(nameof(obj.LastName), obj.LastName));

                    return new ValidationResult()
                    {
                        Errors = failures
                    };
                });

            person.PrepareValidation(person, mockValidator.Object);
            person.Name = "WrongName1";
            person.LastName = "WrongLastName1";

            person.Validate();

            person.Name = "WrongName2";
            person.LastName = "WrongName2";

            person.Validate(propertyName: nameof(person.Name));

            string[] expectedErrors = new string[] { "WrongName2", "WrongLastName1" };

            string[] actualErrors = person.GetErrors().Cast<string>().ToArray();

            CollectionAssert.AreEquivalent(actualErrors, expectedErrors);
        }
    }
}
