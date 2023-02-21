//using System.DirectoryServices;

namespace SinaraApi.Common;
public class DirectoryEntryService {
    const string path = "LDAP://OU=Главный цех Пользователи,OU=Основное производство,OU=Москва,DC=Sinara,DC=oao";
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
        return Task.FromResult(Random.Shared.NextDouble() > 0.3);
    }
#pragma warning restore IDE0060
}
