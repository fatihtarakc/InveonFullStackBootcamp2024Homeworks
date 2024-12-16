namespace Library.Core.Constants
{
    public class Message
    {
        #region Account
        public const string Account_Login_Failed = "Account_Login_Failed";
        public const string Account_Email_Has_Already_Exist = "Account_Email_Has_Already_Exist";
        public const string Account_Username_Has_Already_Exist = "Account_Username_Has_Already_Exist";
        #endregion

        #region Redis
        public const string Redis_Cache_Entity_Was_Added = "Redis_Cache_Entity_Was_Added";
        public const string Redis_Cache_Entity_Was_Found = "Redis_Cache_Entity_Was_Found";
        public const string Redis_Cache_Entity_Was_Not_Added = "Redis_Cache_Entity_Was_Not_Added";
        public const string Redis_Cache_Entity_Was_Not_Found = "Redis_Cache_Entity_Was_Not_Found";
        #endregion

        public const string Account_Not_Found = "ACCOUNT_NOT_FOUND";
        public const string Account_Role_Not_Found_For_User = "Account_Role_Not_Found_For_User";

        public const string Please_Enter_Your_Email = "Please_Enter_Your_Email";
        public const string Please_Enter_Your_Password = "Please_Enter_Your_Password";
        public const string Please_Enter_Your_Email_With_Correct_Format = "Please_Enter_Your_Email_With_Correct_Format";
    }
}