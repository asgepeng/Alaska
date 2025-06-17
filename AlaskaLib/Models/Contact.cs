using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Alaska.Models;
public class Phone
{
    [JsonPropertyName("id")] public int Id { get; set; } = 0;
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    [JsonPropertyName("number")] public string Number { get; set; } = string.Empty;
    [JsonPropertyName("extension")] public string Extension { get; set; } = string.Empty;
}

public class Address
{
    [JsonPropertyName("id")] public int Id { get; set; } = 0;
    [JsonPropertyName("streetAddress")] public string StreetAddress { get; set; } = string.Empty;
    [JsonPropertyName("city")] public string City { get; set; } = string.Empty;
    [JsonPropertyName("state")] public string State { get; set; } = string.Empty;
    [JsonPropertyName("zipCode")] public string ZipCode { get; set; } = string.Empty;
    [JsonPropertyName("country")] public string Country { get; set; } = string.Empty;
}

public class Email
{
    [JsonPropertyName("id")] public int Id { get; set; } = 0;
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
    [JsonPropertyName("address")] public string Address { get; set; } = string.Empty;
}

public class Contact
{
    [JsonPropertyName("id")] public int Id { get; set; } = 0;
    [JsonPropertyName("firstName")] public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("middleName")] public string MiddleName { get; set; } = string.Empty;
    [JsonPropertyName("lastName")] public string LastName { get; set; } = string.Empty;
    [JsonPropertyName("initials")] public string Initials { get; set; } = string.Empty;
    [JsonPropertyName("emails")] public List<Email> Emails { get; set; } = new List<Email>();
    [JsonPropertyName("phones")] public List<Phone> Phones { get; set; } = new List<Phone>();
    [JsonPropertyName("addresses")] public List<Address> Addresses { get; set; } = new List<Address>();
    [JsonPropertyName("department")] public string Department { get; set; } = string.Empty;
    [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
    [JsonPropertyName("manager")] public string Manager { get; set; } = string.Empty;
    [JsonPropertyName("organization")] public string Organization { get; set; } = string.Empty;
    [JsonPropertyName("notes")] public string Notes { get; set; } = string.Empty;
}
