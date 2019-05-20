namespace GenericAPI.DAL
{
    using System.Data.Entity;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=DefaultConnection")
        {
        }
    }
}