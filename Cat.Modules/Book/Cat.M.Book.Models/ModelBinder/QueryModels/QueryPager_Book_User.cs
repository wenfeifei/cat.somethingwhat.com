using Cat.Utility.Helper.Lambda;

namespace Cat.M.Book.Models.ModelBinder.QueryModels
{
    public class QueryPager_Book_User : QueryPager
    {
        /// <summary>
        /// User_Id
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string User_Id { get; set; }
        /// <summary>
        /// Openid
        /// </summary>
        [QueryAttribute(Op_Condition.Equal, Link_Condition.And, true)]
        public string Openid { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        [QueryAttribute(Op_Condition.Contains, Link_Condition.And, true)]
        public string NickName { get; set; }
    }
}
