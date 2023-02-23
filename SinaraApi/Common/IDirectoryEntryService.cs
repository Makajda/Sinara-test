namespace SinaraApi.Common {
    public interface IDirectoryEntryService {
        Task<bool> ValidationLogin(string login);
    }
}