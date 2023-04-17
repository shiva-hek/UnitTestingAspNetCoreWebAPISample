using UserDomain;

namespace UserApplication.ServiceInterfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Add(User user);
    }
}
