using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Phonebook.Application.Common.Interfaces;
using Phonebook.Application.Persons.Dtos;
using System.Data;

namespace Phonebook.Infrastructure.Data;
public class DapperRepository : IDapperRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public DapperRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public async Task<int> GetPeopleCountAsync()
    {
        var sqlCount = "SELECT COUNT(*) FROM People";

        using (var connection = Connection)
        {
            return await connection.ExecuteScalarAsync<int>(sqlCount);
        }
    }

    public async Task<IEnumerable<PersonDto>> GetPeopleWithPaginationAsync(int offset, int pageSize)
    {
        var sqlData = @"
            SELECT 
                p.Id, 
                p.full_name AS FullName, 
                p.Email, 
                a.Id AS AddressId, 
                a.Type, 
                a.address_detail AS AddressDetail, 
                pn.Id AS PhoneNumberId, 
                pn.Number
            FROM People p
            LEFT JOIN Addresses a ON p.Id = a.Person_Id
            LEFT JOIN Phone_Numbers pn ON a.Id = pn.address_id
            ORDER BY p.Id
            LIMIT @PageSize OFFSET @Offset";

        using (var connection = Connection)
        {
            var personDict = new Dictionary<int, PersonDto>();

            var result = await connection.QueryAsync<PersonDto, AddressDto, PhoneNumberDto, PersonDto>(
                sqlData,
                (person, address, phoneNumber) =>
                {
                    if (!personDict.TryGetValue(person.Id, out var currentPerson))
                    {
                        currentPerson = person;
                        personDict.Add(currentPerson.Id, currentPerson);
                    }

                    if (address != null)
                    {
                        var existingAddress = currentPerson.Addresses.FirstOrDefault(a => a.AddressId == address.AddressId);
                        if (existingAddress == null)
                        {
                            existingAddress = address;
                            currentPerson.Addresses.Add(existingAddress);
                        }

                        if (phoneNumber != null && !existingAddress.PhoneNumbers.Any(pn => pn.PhoneNumberId == phoneNumber.PhoneNumberId))
                        {
                            existingAddress.PhoneNumbers.Add(phoneNumber);
                        }
                    }

                    return currentPerson;
                },
                new { Offset = offset, PageSize = pageSize },
                splitOn: "AddressId,PhoneNumberId"
            );

            return personDict.Values;
        }
    }

    public async Task<List<PersonDto>> GetAllPeopleAsync()
    {
        var sql = @"SELECT p.Id, p.full_name AS FullName, p.Email, 
                           a.Id AS AddressId, a.Type, a.address_detail AS AddressDetail, 
                           pn.Id AS PhoneNumberId, pn.Number
                    FROM People p
                    LEFT JOIN Addresses a ON p.Id = a.Person_Id
                    LEFT JOIN Phone_Numbers pn ON a.Id = pn.address_id";

        using (var connection = Connection)
        {
            var personDict = new Dictionary<int, PersonDto>();

            var result = await connection.QueryAsync<PersonDto, AddressDto, PhoneNumberDto, PersonDto>(
                sql,
                (person, address, phoneNumber) =>
                {
                    if (!personDict.TryGetValue(person.Id, out var currentPerson))
                    {
                        currentPerson = person;
                        personDict.Add(currentPerson.Id, currentPerson);
                    }

                    if (address != null)
                    {
                        var existingAddress = currentPerson.Addresses.FirstOrDefault(a => a.AddressId == address.AddressId);
                        if (existingAddress == null)
                        {
                            existingAddress = address;
                            currentPerson.Addresses.Add(existingAddress);
                        }

                        if (phoneNumber != null && !existingAddress.PhoneNumbers.Any(pn => pn.PhoneNumberId == phoneNumber.PhoneNumberId))
                        {
                            existingAddress.PhoneNumbers.Add(phoneNumber);
                        }
                    }

                    return currentPerson;
                },
                splitOn: "AddressId,PhoneNumberId"
            );

            return result.Distinct().ToList();
        }
    }

    public async Task<PersonDto> GetPersonByIdAsync(int id)
    {
        var sql = @"
                SELECT p.Id, p.full_name AS FullName, p.Email, 
                       a.Id AS AddressId, a.Type, a.address_detail AS AddressDetail, 
                       pn.Id AS PhoneNumberId, pn.Number
                FROM People p
                LEFT JOIN Addresses a ON p.Id = a.Person_Id
                LEFT JOIN Phone_Numbers pn ON a.Id = pn.address_id
                WHERE p.Id = @Id";

        using (var connection = Connection)
        {
            var personDict = new Dictionary<int, PersonDto>();

            var result = await connection.QueryAsync<PersonDto, AddressDto, PhoneNumberDto, PersonDto>(
                sql,
                (person, address, phoneNumber) =>
                {
                    if (!personDict.TryGetValue(person.Id, out var currentPerson))
                    {
                        currentPerson = person;
                        personDict.Add(currentPerson.Id, currentPerson);
                    }

                    if (address != null)
                    {
                        var existingAddress = currentPerson.Addresses.FirstOrDefault(a => a.AddressId == address.AddressId);
                        if (existingAddress == null)
                        {
                            existingAddress = address;
                            currentPerson.Addresses.Add(existingAddress);
                        }

                        if (phoneNumber != null && !existingAddress.PhoneNumbers.Any(pn => pn.PhoneNumberId == phoneNumber.PhoneNumberId))
                        {
                            existingAddress.PhoneNumbers.Add(phoneNumber);
                        }
                    }

                    return currentPerson;
                },
                new { Id = id },
                splitOn: "AddressId,PhoneNumberId"
            );

            return result.First();
        }
    }

}


