using MarketsTracker.DAL.Entities;
using MarketsTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MarketsTracker.DAL
{
    static class Mappers
    {
        public static UserEntity MapToUserEntity(IDataReader reader,bool withPasswordData)
        {
            var user = new UserEntity
            {
                FirstName = reader.GetValueOrDefault<string>("firstName"),
                LastName = reader.GetValueOrDefault<string>("lastName"),
                UserId = reader.GetValueOrDefault<int>("userId"),
                UserName = reader.GetValueOrDefault<string>("userName"),
                
            };
            if (withPasswordData)
            {
                user.PasswordHash = reader.GetValueOrDefault<byte[]>("passwordHash");
                user.PasswordSalt = reader.GetValueOrDefault<byte[]>("passwordSalt");
            }
            return user;
        }
        public static User MapToUserDto(this UserEntity entity)
        {
            var dto = new User
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserId = entity.UserId,
                UserName = entity.UserName
            };
            if (entity.Instruments != null)
                dto.Instruments = entity.Instruments.Select(MapToInstrumentDto).ToArray();
            return dto;
        }
        public static InstrumentEntity MapToInstrumentEntity(IDataReader reader)
        {
            var instrument = new InstrumentEntity()
            {
                InstrumentId=reader.GetValueOrDefault<int>("instrumentId"),
                InstrumentType=reader.GetValueOrDefault<string>("instrumentType"),
                Name=reader.GetValueOrDefault<string>("name"),
                Symbol=reader.GetValueOrDefault<string>("symbol")
            };
            return instrument;
        }

        public static Instrument MapToInstrumentDto(this InstrumentEntity entity)
        {
            var dto = new Instrument
            {
                InstrumentId = entity.InstrumentId,
                InstrumentType = entity.InstrumentType,
                Name = entity.Name,
                Symbol = entity.Symbol
            };
            return dto;
        }
        private static T GetValueOrDefault<T>(this IDataReader reader,string name)
        {
            var val = reader[name];
            if (val == DBNull.Value)
                return default(T);
            return (T)val;
        }
    }
}
