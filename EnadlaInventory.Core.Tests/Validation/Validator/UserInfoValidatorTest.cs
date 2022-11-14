using EnadlaInventory.Core.Validation.Validators;
using EnadlaInventory.Core.Communication.Models;
using EnadlaInventory.Core.Validation;

namespace EnadlaInventory.Core.Tests.Validation.Validator
{
    [TestClass]
    public class UserInfoValidatorTest
    {
        public UserInfo GetValidUserInfo()
        {
            return new UserInfo()
            {
                UID = "someUID",
                Email = "JimyAguasviva@gmail.com",
                Password = "JimyAguasviva12345",
                ConfirmPassword = "JimyAguasviva12345",
                IsVerified = true,
                OwnerName = "Jimy Aguasviva",
                ExtraInfo = new ExtraUserInfo()
                {
                    CreationDate = DateTime.Now,
                    CreatorMachine = "someCreatorMachine",
                    UID = "someUID" //should be the same of the parent userInfo
                }
            };
        }

        [TestMethod]
        public void Validating_a_valid_UserInfo_with_all_rulesets()
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo validUserInfo = GetValidUserInfo();

            ValidationResult result = validator.Validate(
                validUserInfo,
                ruleSets: new string[] { UserInfoValidator.RS_AUTH, UserInfoValidator.RS_SIGNUP_EXTRAS });

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Validate_with_AuthRuleSet_and_email_is_empty_is_invalid(string? email)
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.Email = email;

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_AUTH });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.Email) && x.ErrorCode == ErrorCodes.EMPTY_EMAIL).Count() >= 1);
        }

        [TestMethod]
        [DataRow("Jimy.WanerGmail.com")]
        [DataRow("@gmail.com")]
        [DataRow("Jimy.Waner@")]
        public void Validate_with_AuthRuleSet_and_email_without_an_at_in_the_middle_is_invalid(string email)
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.Email = email;

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_AUTH });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.Email) && x.ErrorCode == ErrorCodes.INVALID_EMAIL).Count() >= 1);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void Validate_with_AuthRuleSet_and_with_empty_password_is_invalid(string? password)
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.Password = password;

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_AUTH });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.Password) && x.ErrorCode == ErrorCodes.EMPTY_PASSWORD).Count() >= 1);
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("12")]
        [DataRow("123")]
        [DataRow("1234567")]
        public void Validate_with_AuthRuleSet_and_with_short_password_is_invalid(string password)
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.Password = password;

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_AUTH });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.Password) && x.ErrorCode == ErrorCodes.SHORT_PASSWORD).Count() >= 1);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void Validate_with_ExtraSignUpRuleSet_and_with_empty_OwnerName_is_invalid(string? ownerName)
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.OwnerName = ownerName;

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_SIGNUP_EXTRAS });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.OwnerName) && x.ErrorCode == ErrorCodes.EMPY_VALUE).Count() >= 1);
        }

        [TestMethod]
        [DataRow("j")]
        [DataRow("ji")]
        public void Validate_with_Extra_signUpRuleSet_and_with_short_OwnerName_is_invalid(string ownerName)
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.OwnerName = ownerName;

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_SIGNUP_EXTRAS });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.OwnerName) && x.ErrorCode == ErrorCodes.SHORT_VALUE).Count() >= 1);
        }

        [TestMethod]
        public void Validate_with_ExtraSignUpRuleSet_and_with_diferent_ConfirmPassword_is_invalid()
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.Password = "password";
            userInfo.ConfirmPassword = "confirmPassword";

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_SIGNUP_EXTRAS });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.ConfirmPassword) && x.ErrorCode == ErrorCodes.DIFERENT_CONFIRM_PASSWORD).Count() >= 1);
        }

        [TestMethod]
        public void Validate_with_ExtraSignUpRuleSet_and_without_ExtraUserInfo_is_invalid()
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.ExtraInfo = null;

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_SIGNUP_EXTRAS });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.ExtraInfo) && x.ErrorCode == ErrorCodes.EMPY_VALUE).Count() >= 1);
        }

        [TestMethod]
        public void Validate_with_ExtraSignUpRuleSet_and_with_invalid_ExtraInfo_is_invalid()
        {
            UserInfoValidator validator = new UserInfoValidator();
            UserInfo userInfo = GetValidUserInfo();
            userInfo.ExtraInfo = new ExtraUserInfo() { CreatorMachine = null };

            ValidationResult result = validator.Validate(userInfo, ruleSets: new string[] { UserInfoValidator.RS_SIGNUP_EXTRAS });

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(UserInfo.ExtraInfo) && x.ErrorCode == ErrorCodes.INVALID_VALUE).Count() >= 1);
        }
    }
}
