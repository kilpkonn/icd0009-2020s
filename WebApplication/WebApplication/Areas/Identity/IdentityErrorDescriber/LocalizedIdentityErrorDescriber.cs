using Microsoft.AspNetCore.Identity;

namespace WebApplication.Areas.Identity.IdentityErrorDescriber
{
    /// <inheritdoc />
    public class LocalizedIdentityErrorDescriber : Microsoft.AspNetCore.Identity.IdentityErrorDescriber
    {
        /// <inheritdoc />
        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .DefaultError
            };
        }

        /// <inheritdoc />
        public override IdentityError ConcurrencyFailure()
        {
            return new IdentityError
            {
                Code = nameof(ConcurrencyFailure),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .ConcurrencyFailure
            };
        }

        /// <inheritdoc />
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .PasswordMismatch
            };
        }

        /// <inheritdoc />
        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = nameof(InvalidToken),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .InvalidToken
            };
        }

        /// <inheritdoc />
        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .LoginAlreadyAssociated
            };
        }

        /// <inheritdoc />
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .InvalidUserName, userName)
            };
        }

        /// <inheritdoc />
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .InvalidEmail, email)
            };
        }

        /// <inheritdoc />
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .DuplicateUserName, userName)
            };
        }

        /// <inheritdoc />
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .DuplicateEmail, email)
            };
        }

        /// <inheritdoc />
        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .InvalidRoleName, role)
            };
        }

        /// <inheritdoc />
        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .DuplicateRoleName, role)
            };
        }

        /// <inheritdoc />
        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .UserAlreadyHasPassword
            };
        }

        /// <inheritdoc />
        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError
            {
                Code = nameof(UserLockoutNotEnabled),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .UserLockoutNotEnabled
            };
        }

        /// <inheritdoc />
        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .UserAlreadyInRole, role)
            };
        }

        /// <inheritdoc />
        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .UserNotInRole, role)
            };
        }

        /// <inheritdoc />
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description =
                    string.Format(
                        Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                            .PasswordTooShort, length)
            };
        }

        /// <inheritdoc />
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .PasswordRequiresNonAlphanumeric
            };
        }

        /// <inheritdoc />
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .PasswordRequiresDigit
            };
        }

        /// <inheritdoc />
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .PasswordRequiresLower
            };
        }

        /// <inheritdoc />
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = Resource.Base.Areas.Identity.IdentityErrorDescriber.LocalizedIdentityErrorDescriber
                    .PasswordRequiresUpper
            };
        }
    }
}