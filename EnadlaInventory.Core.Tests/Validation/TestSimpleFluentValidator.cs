using EnadlaInventory.Core.Validation;

namespace EnadlaInventory.Core.Tests.Validation
{
    [TestClass]
    public class TestSimpleFluentValidator
    {
        [TestMethod]
        public void Validating_an_invalid_object_and_get_its_errors()
        {
            SimpleFluentValidator<PersonModelSample> simpleFluentValidator = PersonModelSampleValidator.Instance;
            PersonModelSample invalidObj = new PersonModelSample();
            string[] ruleSets = new string[] { "default", PersonModelSampleValidator.RS_AUTH, PersonModelSampleValidator.RS_NAMES };

            ValidationResult result = simpleFluentValidator.Validate(invalidObj, ruleSets: ruleSets);

            Assert.IsFalse(result.IsValid);

            var expectedErros = new List<ValidationFailure>()
            {
                new ValidationFailure(nameof(PersonModelSample.UID), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.Name), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.LastName), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.Email), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.Password), ErrorCodes.EMPY_VALUE)
            }.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList();
            
            CollectionAssert.AreEquivalent(result.Errors.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList(), expectedErros);
        }

        [TestMethod]
        public void Validate_without_specificate_ruleSets_all_rulesets_are_executed()
        {
            SimpleFluentValidator<PersonModelSample> simpleFluentValidator = PersonModelSampleValidator.Instance;
            PersonModelSample invalidObj = new PersonModelSample();


            ValidationResult result = simpleFluentValidator.Validate(invalidObj);


            Assert.IsFalse(result.IsValid);
            
            string[] executedRuleSetsExpected = new string[] { "default", PersonModelSampleValidator.RS_AUTH, PersonModelSampleValidator.RS_NAMES };
            CollectionAssert.AreEquivalent(executedRuleSetsExpected, result.ExecutedRuleSets);

            List<string> expectedErrors = new List<ValidationFailure>()
            {
                new ValidationFailure(nameof(PersonModelSample.UID), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.Name), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.LastName), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.Email), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.Password), ErrorCodes.EMPY_VALUE)
            }.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList();
            CollectionAssert.AreEquivalent(expectedErrors, result.Errors.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList());
        }

        [TestMethod]
        public void Only_specificated_rulesets_are_executed()
        {
            SimpleFluentValidator<PersonModelSample> simpleFluentValidator = PersonModelSampleValidator.Instance;
            PersonModelSample invalidObj = new PersonModelSample();
            string[] ruleSets = new string[] { "default", PersonModelSampleValidator.RS_NAMES };

            ValidationResult result = simpleFluentValidator.Validate(invalidObj, ruleSets: ruleSets);

            Assert.IsFalse(result.IsValid);

            string[] expectedRuleSets = new string[] { "default", PersonModelSampleValidator.RS_NAMES };
            CollectionAssert.AreEquivalent(expectedRuleSets, result.ExecutedRuleSets);

            List<string> expectedErrors = new List<ValidationFailure>()
            {
                new ValidationFailure(nameof(PersonModelSample.UID), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.Name), ErrorCodes.EMPY_VALUE),
                new ValidationFailure(nameof(PersonModelSample.LastName), ErrorCodes.EMPY_VALUE)
            }.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList();
            CollectionAssert.AreEquivalent(expectedErrors, result.Errors.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList());
        }

        [TestMethod]
        public void Validate_with_specific_PropertyName_Returns_errors_only_from_that_specific_Property()
        {
            SimpleFluentValidator<PersonModelSample> simpleFluentValidator = PersonModelSampleValidator.Instance;
            PersonModelSample invalidObj = new PersonModelSample();

            ValidationResult result = simpleFluentValidator.Validate(invalidObj, nameof(PersonModelSample.Name), PersonModelSampleValidator.RS_NAMES, PersonModelSampleValidator.RS_AUTH);

            Assert.IsFalse(result.IsValid);

            var expectedErros = new List<ValidationFailure>()
            {
                new ValidationFailure(nameof(PersonModelSample.Name), ErrorCodes.EMPY_VALUE)
            }.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList();

            CollectionAssert.AreEquivalent(expectedErros, result.Errors.Select(x => $"{x.PropertyName}-{x.ErrorCode}").ToList());
        }
    }
}
