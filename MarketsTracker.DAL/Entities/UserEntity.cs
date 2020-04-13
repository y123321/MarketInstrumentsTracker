using System;
using System.Collections.Generic;
using System.Text;

namespace MarketsTracker.DAL.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ICollection<InstrumentEntity> Instruments { get; set; }
    }
}
