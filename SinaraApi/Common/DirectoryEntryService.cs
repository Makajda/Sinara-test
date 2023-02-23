//using System.DirectoryServices;

namespace SinaraApi.Common;
#pragma warning disable IDE0051 // Remove unused private members
public class DirectoryEntryService : IDirectoryEntryService {
    const string path = "LDAP://OU=Главный цех Пользователи,OU=Основное производство,OU=Екатеринбург,DC=Sinara,DC=oao";
    private readonly Lazy<List<string>> logins;

    public DirectoryEntryService() {
        logins = new Lazy<List<string>>(() => GetLogins());
    }

    private List<string> GetLogins() {
        var result = new List<string>();

        //using var directoryEntry = new DirectoryEntry(path);
        //foreach (DirectoryEntry de in directoryEntry.Children) {
        //    result.Add(de.Properties["sAMAccountName"]?.Value?.ToString() ?? "без имени");
        //}

        return result;
    }

#pragma warning disable IDE0060
    public Task<bool> ValidationLogin(string login) {
        return Task.FromResult(Random.Shared.NextDouble() > 0.2);
    }
#pragma warning restore IDE0060
}
#pragma warning restore IDE0051 // Remove unused private members
