namespace PolicyWatcher.Domain.Errors
{
    public class UserErrors
    {
        public static Error UserNotFound() => new (
        "Users.NotFound", $"The user with the Id provided was not found");

        public static Error InvalidTier => new(
            "Users.InvalidTier", $"The tier level you selected is invalid");

        public static readonly Error EmptyName = new(
            "Users.NullValue", "Enter a valid name");
    }
}
