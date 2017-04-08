using System;

using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace EmlSoft.KBSTest.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
            : base("name=SqlContext")
        {
        }
        public SqlContext(string ConnectionString)
            : base(ConnectionString)
        {
        }

        public SqlContext(DbConnection conn)
            : base(conn, true)
        {
        }

        public virtual DbSet<Source> Sources { get; set; }

        public virtual DbSet<Content> Contents { get; set; }
    }
}